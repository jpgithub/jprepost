/*
 * Server.h
 *
 *  Created on: Jun 11, 2017
 *      Author: master
 */

#ifndef SERVER_H_
#define SERVER_H_
//=================================
// forward declared dependencies
// None

//=================================
// included dependencies
#include <vector>
#include <thread>
// Linux System API
#include <unistd.h>

// Socket
#include <sys/types.h>
#include <sys/socket.h>
#include <netinet/in.h>

// Interface
#include "ITask.h"

typedef int sockfd;

class Server {
	struct sockaddr_in _addr;
	sockfd _fd;
	std::vector<sockfd> connected;
	// Task for server to perform
	void Task();
	std::thread threadobj;
public:
	Server(int portnumber);
	virtual ~Server();
	// Get the socket file descriptor
	sockfd getId() { return _fd; }
	// Get the socket address
	sockaddr_in getAddr() { return _addr; }
	// Add new client connection
	void AddClient(sockfd client);
	// Task for server to perform
	void Run();
};

#endif /* SERVER_H_ */
