// MathLibrary.h - Contains declarations of math functions
#pragma once

#ifdef MATHLIBRARY_EXPORTS
#define MATHLIBRARY_API __declspec(dllexport)
#else
#define MATHLIBRARY_API __declspec(dllimport)
#endif

typedef struct matrixobject
{
    bool istransposed;
    int data;
};

typedef struct matrixdata
{
    int data[128];
};

// The Fibonacci recurrence relation describes a sequence F
// where F(n) is { n = 0, a
//               { n = 1, b
//               { n > 1, F(n-2) + F(n-1)
// for some initial integral values a and b.
// If the sequence is initialized F(0) = 1, F(1) = 1,
// then this relation produces the well-known Fibonacci
// sequence: 1, 1, 2, 3, 5, 8, 13, 21, 34, ...

// Initialize a Fibonacci relation sequence
// such that F(0) = a, F(1) = b.
// This function must be called before any other function.
extern "C" MATHLIBRARY_API void fibonacci_init(
    const unsigned long long a, const unsigned long long b);

// Produce the next value in the sequence.
// Returns true on success and updates current value and index;
// false on overflow, leaves current value and index unchanged.
extern "C" MATHLIBRARY_API bool fibonacci_next();

// Get the current value in the sequence.
extern "C" MATHLIBRARY_API unsigned long long fibonacci_current();

// Get the position of the current value in the sequence.
extern "C" MATHLIBRARY_API unsigned fibonacci_index();

// Input string equation
extern "C" MATHLIBRARY_API int equation(const char* expression);

extern "C" MATHLIBRARY_API int matrixopertion(struct matrixobject* arrayofmd);

extern "C" MATHLIBRARY_API int matrixopertionA(struct matrixobject* arrayofmd, int sizeofarray);

extern "C" MATHLIBRARY_API int matrixdataoperation(struct matrixdata* md, int size);

extern "C" MATHLIBRARY_API int GetIndexedComputationSetSize(int* computeset, int index);

extern "C" MATHLIBRARY_API int GetIndexedComputationSet(int* computeset, int buffer[], int bufferlength, int index);

extern "C" MATHLIBRARY_API HANDLE AsyncComputation(int *computeset, int index);

DWORD WINAPI CallbackTriggerThreadProc(LPVOID lpParam);

DWORD WINAPI ComputationThread(LPVOID lpParam);

