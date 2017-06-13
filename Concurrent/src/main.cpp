/*
 * main.cpp
 *
 *  Created on: Jun 11, 2017
 *      Author: master
 */
#include <stdlib.h>
#include <stdio.h>
#include <cstring>
#include <iostream>
#include <sys/types.h>
#include <signal.h>
#include "Server.h"

#define INVALID_SOCKET -1
#define handle_error(msg) \
	do { \
		perror(msg); \
		exit(EXIT_FAILURE); \
	} while(0)

using namespace std;

sig_atomic_t signaled = 0;
void (*prev_handler)(int);
void shutdown_handler(int param)
{
  signaled = 1;
}

int network_accept_any(Server *fds[], unsigned int count, struct sockaddr *addr, socklen_t *addrlen);

int main (int argc, char* argv[])
{
	if (argc < 4){
		cout<<"usage: ExProcess [port1] [port2] [port3]"<<endl;
		return -1;
	}
	//inet_addr("128.235.44.71"),inet_ntoa(), inet_aton(),

	// Setup signal for clean shutdown
	// System command kill -s 2 pidof
	prev_handler = signal(SIGINT, shutdown_handler);

	Server *servers[argc-1];

	// Create socket for each port a.k.a creating many servers
	for (int i = 1; i < argc; i++){
		int portno = atoi(argv[i]);
		cout << " Port Number: "<< portno << endl;
		servers[i-1] = new Server(portno);

		struct sockaddr_in serv_addr = servers[i-1]->getAddr();
		sockfd serv_fd = servers[i-1]->getId();
		//  Bind to socket
		if ( bind(serv_fd,(struct sockaddr*)&serv_addr, sizeof(serv_addr)) < 0 )
		{
			handle_error("bind");
		}

		if ( listen(serv_fd,10) < 0)
		{
			handle_error("listen");
		}
	}


	socklen_t clilen;
	struct sockaddr_in cli_addr;
	long count = 0l;
	cout<< "Waiting for Clients.."<<endl;
	while(true)
	{
		if (signaled)
		{
			break;
		}

		memset(&cli_addr, 0, sizeof(cli_addr));
		clilen = sizeof(cli_addr);
		//Check each server for incoming client
		int status = network_accept_any(servers,argc-1,(struct sockaddr *) &cli_addr, &clilen);
		if (status)
			cout << count << endl;
		else
		{
			cout<< "No client connection within 1 seconds" <<endl;
		}
		count++;
	}

	cout<< "Shutdown.."<<endl;

	for ( int i = 0; i < (argc - 1); i++)
		delete servers[i];

	return 0;
}

int network_accept_any(Server *fds[], unsigned int count, struct sockaddr *addr, socklen_t *addrlen)
{
	fd_set readfds;
	sockfd maxfd, fd;
	unsigned int i;
	int status;
	fd = INVALID_SOCKET;

	// Set timeout for select function
	struct timeval timeout;
	timeout.tv_sec = 1l;
	timeout.tv_usec = 10l;

	// Clear file descriptor set
	FD_ZERO(&readfds);
	maxfd = -1;

	for (i = 0; i < count; i++)
	{
		// Iterate through Servers List
		sockfd serv_fd = fds[i]->getId();
		FD_SET(serv_fd, &readfds);
		if (serv_fd >= maxfd)
		{
			//cout<< "Socket File Descriptor Number: " << serv_fd <<endl;
			maxfd = serv_fd + 1;
		}

	}

	//cout<< "Checking for incoming client connection on Server Number: " << maxfd <<endl;
	status = select(maxfd, &readfds, NULL, NULL, &timeout);

	if(status == -1)
	{
		//perror("select()");
		// select call error
		return INVALID_SOCKET;
	}

	if(status)
	{
		for (i = 0; i < count; i++)
		{
			// Iterate through Servers List
			sockfd serv_fd = fds[i]->getId();

			if (FD_ISSET(serv_fd, &readfds))
			{
				fd = serv_fd;
				fds[i]->AddClient(accept(fd, addr, addrlen));
				break;
			}
		}
	}
	return status;
}




