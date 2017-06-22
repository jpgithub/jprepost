/*
 * Server.cpp
 *
 *  Created on: Jun 11, 2017
 *      Author: master
 */

#include "Server.h"


using namespace std;

Server::Server(int portnumber) {
	int reuse = 1;
	_fd = socket(AF_INET, SOCK_STREAM, 0);
	//Specifies that the rules used in validating addresses supplied to
	//bind() should allow reuse of local addresses, if this is supported by the protocol.
	// In other words it free the ports up for other programs after this process die
	setsockopt(_fd, SOL_SOCKET, SO_REUSEADDR, &reuse, sizeof(int));
	_addr.sin_family = AF_INET;
	_addr.sin_addr.s_addr = INADDR_ANY;
	_addr.sin_port = htons(portnumber);
}

Server::~Server() {
	if(threadobj.joinable())
		threadobj.join();
	// Close all client connections
	for (std::vector<sockfd>::iterator it = connected.begin(); it != connected.end(); ++it)
	{
		close(*it);
	}
	// Server Shutdown
	close(_fd);
}

void Server::AddClient(sockfd client){
	// Update client connection list
	connected.push_back(client);

}

void Server::Task(){
	int client = connected.front();
	// Receive with timeout
	struct timeval tv;
	tv.tv_sec = 30; //Timeout 1 min
	tv.tv_usec = 0;
	setsockopt(client, SOL_SOCKET, SO_RCVTIMEO, (const char*)&tv,sizeof(struct timeval));

	//Spawn Client Worker Thread
	std::cout << "Server ID: "<< _fd <<" Added New Client ID: "<< client << std::endl;
	char buffer[256];
	size_t len = sizeof(buffer)/sizeof(char);
	read(client,buffer,len);
	std::cout << "TimeOut" << std::endl;

}
void Server::Run(){
	threadobj = std::thread (&Server::Task,this);
}

