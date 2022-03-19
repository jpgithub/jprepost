// RCIMSim.cpp : Defines the entry point for the application.
//

#include "RCIMSim.h"

using namespace std;


// Resource:
// https://linux.die.net/man/7/signal
// https://wiki.sei.cmu.edu/confluence/display/c/SIG31-C.+Do+not+access+shared+objects+in+signal+handlers
// maximum portability, compliant code
/*
you are required to use volatile sig_atomic_t if the variable may be accessed asynchronously 
(i.e., the variable is accessed both inside and outside of the signal handler).
*/
volatile sig_atomic_t e_flag = 0;

mutex m;
condition_variable cv;

string data;
bool ready = false;
bool processed = false;

static void print_siginfo(siginfo_t* si)
{
	timer_t* tidp;
	int oor;
	tidp = (timer_t*)si->si_value.sival_ptr;

}

static void handler(int sig, siginfo_t* si, void* uc)
{
	signal(sig, SIG_IGN);
	//cv.notify_one(); 
}

void my_handler(int param)
{
	e_flag = 1;
}


/*
https://en.cppreference.com/w/cpp/thread/condition_variable/wait
*/
void worker_thread()
{
	//TODO: Add time stamp
	
	// Block until signaled
	unique_lock<mutex> lk(m);
	cv.wait(lk, [] { return ready; });

	// after the wait, we own the lock.
	std::cout << "Worker thread is processing data\n";
	data += " after processing";

	// Send data back to main()
	processed = true;
	std::cout << "Worker thread signals data processing completed\n";

	// Optional
	// Manual unlocking is done before notifying, to avoid waking up
	// the waiting thread only to block again (see notify_one for details)
	//lk.unlock();
	//cv.notify_one();
}

int main(int argc, char* argv[])
{
	/*
	thread worker(worker_thread);
	data = "Example data";

	{
		lock_guard<mutex> lk(m);
		ready = true;
		// main() signals data ready for processing
	}

	cv.notify_one(); //TODO Call by the signal handler;

	// Optional
	//{
	//	unique_lock<mutex> lk(m);
	//	cv.wait(lk, [] { return processed; });
	//}
	// Back in main(), data = " << data << '\n
	
	worker.join();
	*/

	//read https://linux.die.net/man/3/pthread_sigmask
	//https://linoxide.com/signal-handling-linux-signal-function/
	/*
	* The signal() Function Limitations
	Though the signal() function is the oldest and the easiest way to handle signals, it has a couple of major limitations :

	Other signals are not blocked by the signal() function while the signal handler is executing for current signal. This may produce undesired results.
	The signal action for a particular signal is reset to its default value i.e., SIG_DFL as soon as the signal is delivered. This means that even if the signal handler again sets the signal action as the first step, there is a possible time window in which the signal may occur again while its action is set to SIG_DFL.
	Due to these major limitations, it is now advised to use the function sigaction() that overcomes all these limitations.
	
	void (*prev_handler)(int);
	prev_handler = signal(SIGINT, my_handler);
	raise(SIGINT);
	printf ("Signaled is %d.\n", signaled);
	*/

	timer_t timerid;
	struct sigevent sev;
	struct itimerspec its;
	long long freq_nanosecs;
	sigset_t mask;
	struct sigaction sa;

	if (argc != 3)
	{
		fprintf(stderr, "Usage: %s <sleep-secs> <freq-nanosecs>\n", argv[0]);
		exit(EXIT_FAILURE);
	}

	/* Establish handler for timer signal */
	/*
	If SA_SIGINFO is specified in sa_flags, then sa_sigaction (instead of sa_handler) specifies the signal-handling function for signum.
	This function receives the signal number as its first argument, 
	a pointer to a siginfo_t as its second argument and
	a pointer to a ucontext_t (cast to void *) as its third argument.
	(Commonly, the handler function doesn't make any use of the third argument. 
	See getcontext(3) for further information about ucontext_t.)
	*/
	sa.sa_flags = SA_SIGINFO;
	sa.sa_sigaction = handler;
	sigemptyset(&sa.sa_mask);

	if (sigaction(SIG, &sa, NULL) == -1)
	{
		errExit("sigaction");
	}

	/* Block timer signal temporarily */

	sigemptyset(&mask);
	sigaddset(&mask, SIG);
	if (sigprocmask(SIG_SETMASK, &mask, NULL) == -1)
	{
		errExit("sigprocmask");
	}

	/* Create the timer */

	sev.sigev_notify = SIGEV_SIGNAL;
	sev.sigev_signo = SIG;
	sev.sigev_value.sival_ptr = &timerid;
	//if(timer_create())


	return 0;
}
