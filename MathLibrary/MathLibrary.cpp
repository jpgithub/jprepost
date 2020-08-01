// MathLibrary.cpp : Defines the exported functions for the DLL.
#include "pch.h" // use stdafx.h in Visual Studio 2017 and earlier
#include <utility>
#include <limits.h>
#include <windows.h>
#include <iostream>
#include "MathLibrary.h"


HANDLE ghWriteEvent;
HANDLE ghThread;
DWORD dwThreadID;

/// <summary>
///  Computation Thread Driver
/// </summary>
HANDLE ctThread;
DWORD ctThreadID;

static int computesetsize;

static int* computesetptr;

// DLL internal state variables:
static unsigned long long previous_;  // Previous value, if any
static unsigned long long current_;   // Current sequence value
static unsigned index_;               // Current seq. position




// Initialize a Fibonacci relation sequence
// such that F(0) = a, F(1) = b.
// This function must be called before any other function.
void fibonacci_init(
    const unsigned long long a,
    const unsigned long long b)
{
    index_ = 0;
    current_ = a;
    previous_ = b; // see special case when initialized
}

// Produce the next value in the sequence.
// Returns true on success, false on overflow.
bool fibonacci_next()
{
    // check to see if we'd overflow result or position
    if ((ULLONG_MAX - previous_ < current_) ||
        (UINT_MAX == index_))
    {
        return false;
    }

    // Special case when index == 0, just return b value
    if (index_ > 0)
    {
        // otherwise, calculate next sequence value
        previous_ += current_;
    }
    std::swap(current_, previous_);
    ++index_;
    return true;
}

// Get the current value in the sequence.
unsigned long long fibonacci_current()
{
    return current_;
}

// Get the current index position in the sequence.
unsigned fibonacci_index()
{
    return index_;
}

int equation(const char* expression)
{
    std::cout << expression << std::endl;
    return 0;
}

int matrixopertion(matrixobject* arrayofmd)
{
    int status = 0;
    try
    {
        arrayofmd->istransposed = true;
        arrayofmd->data = 10;        
        //for (int i = 0; i < arrayofmd->data; i++)
        //{
        //    if (((i + 1) % 2) == 0)
        //    {
        //        //matrixdata s;
        //        //s.istransposed = 0;              
        //        arrayofmd->data[i] = (i+1);
        //    }
        //    else
        //    {
        //        //matrixdata s;
        //        //s.istransposed = 1;
        //        arrayofmd->data[i] = (i + 1);
        //    }
        //}
    }
    catch(...)
    {
        status = -1;
    }   

    //*arrayofmd = *amd;
    return status;
}

int matrixopertionA(matrixobject* arrayofmd, int sizeofarray)
{
    int status = 0;
    try
    {
        for (int i = 0; i < sizeofarray; i++)
        {
            matrixopertion(&arrayofmd[i]);
        }
    }
    catch (...)
    {
        status = -1;
    }

    //*arrayofmd = *amd;
    return status;
}

int matrixdataoperation(matrixdata* md, int size)
{
    md->data[0] = 1;
    return 0;
}

int GetIndexedComputationSetSize(int* computeset, int index)
{
    if (*computeset == 0xA0A0 && index == 0)
    {
        return computesetsize;
    }
    return -1;
}

int GetIndexedComputationSet(int* computeset, int buffer[], int bufferlength, int index)
{
    int remainder = 0;

    if (bufferlength > computesetsize)
    {
        for (int i = 0; i < computesetsize; i++)
        {
            buffer[i] = computesetptr[i];
        }
    }
    else 
    {
        for (int i = 0; i < bufferlength; i++)
        {
            buffer[i] = computesetptr[i];
        }
        remainder = computesetsize - bufferlength;
    }
    return remainder;
}

HANDLE AsyncComputation(int* computeset, int index)
{
    ghWriteEvent = CreateEvent(
        NULL,               // default security attributes
        TRUE,               // manual-reset event
        FALSE,              // initial state is nonsignaled
        TEXT("ComputationEvent")  // object name
    );

    if (ghWriteEvent == NULL)
    {
        printf("CreateEvent failed (%d)\n", GetLastError());
        return NULL;
    }

    // Create callback thread to read from the complete computation value.
    //ghThread = CreateThread(
    //    NULL,              // default security
    //    0,                 // default stack size
    //    CallbackTriggerThreadProc,        // name of the thread function
    //    NULL,              // no thread parameters
    //    0,                 // default startup flags
    //    &dwThreadID);


    // Create callback thread to signal from the complete computation value.
    ctThread = CreateThread(
        NULL,              // default security
        0,                 // default stack size
        ComputationThread,        // name of the thread function
        NULL,              // no thread parameters
        0,                 // default startup flags
        &ctThreadID);


    if (ctThread == NULL)
    {
        printf("CreateThread failed (%d)\n", GetLastError());
        return NULL;
    }


    return ghWriteEvent;
}

DWORD WINAPI CallbackTriggerThreadProc(LPVOID lpParam)
{
    // lpParam not used in this example.
    UNREFERENCED_PARAMETER(lpParam);

    DWORD dwWaitResult;

    printf("Thread %d waiting for write event...\n", GetCurrentThreadId());

    dwWaitResult = WaitForSingleObject(
        ghWriteEvent, // event handle
        INFINITE);    // indefinite wait

    switch (dwWaitResult)
    {
        // Event object was signaled
    case WAIT_OBJECT_0:
        //
        // TODO: Read from the shared buffer
        //
        printf("Thread %d reading from buffer\n",
            GetCurrentThreadId());
        break;

        // An error occurred
    default:
        printf("Wait error (%d)\n", GetLastError());
        return 0;
    }

    // Now that we are done reading the buffer, we could use another
    // event to signal that this thread is no longer reading. This
    // example simply uses the thread handle for synchronization (the
    // handle is signaled when the thread terminates.)

    printf("Thread %d exiting\n", GetCurrentThreadId());
    return 1;
}

DWORD WINAPI ComputationThread(LPVOID lpParam)
{
    // lpParam not used in this example.
    UNREFERENCED_PARAMETER(lpParam);

    printf("C++ Computation thread writing to the shared buffer...\n");

    HANDLE hTimer = NULL;
    LARGE_INTEGER liDueTime;
    DWORD mss = 60000;
    liDueTime.QuadPart = -300000000LL;

    // Create an unnamed waitable timer.
    hTimer = CreateWaitableTimer(NULL, TRUE, NULL);
    if (NULL == hTimer)
    {
        printf("CreateWaitableTimer failed (%d)\n", GetLastError());
        return 1;
    }

    //printf("Waiting for 30 seconds...\n");

    // Set a timer to wait for 30 seconds.
    if (!SetWaitableTimer(hTimer, &liDueTime, 0, NULL, NULL, 0))
    {
        printf("SetWaitableTimer failed (%d)\n", GetLastError());
        return 2;
    }

    // Wait for the timer.
    // Sleep for 1 min and trigger alert within 30 sec
    if (WaitForSingleObjectEx(hTimer,mss,TRUE) != WAIT_OBJECT_0)
    {
        printf("WaitForSingleObject failed (%d)\n", GetLastError());
    }
    else 
    {
        computesetsize = 30;

        computesetptr = new int[computesetsize];

        for (int i = 0; i < computesetsize; i++)
        {
            computesetptr[i] = i + 1;
        }

        printf("C# was signaled by C++.\n");
        // Set to signaled
        if (!SetEvent(ghWriteEvent))
        {
            printf("SetEvent failed (%d)\n", GetLastError());
        }
    }
}
