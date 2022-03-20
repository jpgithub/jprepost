// RCIMSim.h : Include file for standard system include files,
// or project specific include files.

#pragma once

#include <iostream>
#include <unistd.h>
#include <time.h>
#include <csignal>
#include <thread>
#include <mutex>
#include <condition_variable>
#include <string>
#include <chrono>

#define CLOCKID CLOCK_REALTIME
/*
The Linux kernel supports a range of 32 different real-time signals, numbered 33 to 64. 
However, the glibc POSIX threads implementation internally uses two (for NPTL) or three (for LinuxThreads) real-time signals (see pthreads(7)), and adjusts the value of SIGRTMIN suitably (to 34 or 35).
Because the range of available real-time signals varies according to the glibc threading implementation (and this variation can occur at run time according to the available kernel and glibc), 
and indeed the range of real-time signals varies across UNIX systems, programs should never refer to real-time signals using hard-coded numbers, 
but instead should always refer to real-time signals using the notation SIGRTMIN+n, and include suitable (run-time) checks that SIGRTMIN+n does not exceed SIGRTMAX.
*/
#define SIG SIGRTMIN

#define errExit(msg)	do { \
	perror(msg); \
	exit(EXIT_FAILURE); \
} while(0)

typedef struct {
	timer_t timerId;
}Info_t;

void RtInit(Info_t* info, long long freq_nanosecs);
