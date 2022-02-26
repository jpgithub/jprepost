// Client side C/C++ program to demonstrate Socket programming
#include <stdio.h>
#include <sys/socket.h>
#include <arpa/inet.h>
#include <unistd.h>
#include <string.h>
#include <mutex>
#define PORT 13000

std::mutex mtx;

void Device_Read(int sock)
{
	char buffer[1024] = {0};
	int valread = read( sock , buffer, 1024);
	if(valread != -1)
	{
		printf("Read: %s\n", buffer);
		printf("Bytes Read: %d \n",valread);
	}
	else
	{
		perror("Read Failure");
	}
}

void Device_Write(int sock, const char * cmd)
{
	int retval = send(sock , cmd , strlen(cmd) , 0 );
	//printf("Hello message sent\n");
	//valread = read( sock , buffer, 1024);
	printf("%d, Len: %ld\n",retval, strlen(cmd));
		
	//retval = send(sock , cmd2 , strlen(cmd2) , 0 );
	//printf("Hello message sent\n");
	//valread = read( sock , buffer, 1024);
	//printf("%d, Len: %ld\n",retval, strlen(cmd2));
	
}

void Query_Loop(int sock_handler)
{
	int loop_count = 0;	
	const char *cmd1 = "*TST1?";
	const char *cmd2 = "SYST:ERR?";
	
	std::string cmd(cmd1);
	while(loop_count < 100)
	{	
		std::unique_lock<std::mutex> lck (mtx);
		Device_Write(sock_handler, cmd.c_str());
		Device_Read(sock_handler);
		cmd.clear();
		cmd.append("SYST:ERR?");
		Device_Write(sock_handler, cmd.c_str());
		Device_Read(sock_handler);
		loop_count++;
		
		
		cmd.clear();
		cmd.append(cmd1);
	}
}

int main(int argc, char const *argv[])
{
	int sock = 0;
	struct sockaddr_in serv_addr;
	if ((sock = socket(AF_INET, SOCK_STREAM, 0)) < 0)
	{
		printf("\n Socket creation error \n");
		return -1;
	}

	serv_addr.sin_family = AF_INET;
	serv_addr.sin_port = htons(PORT);
	
	// Convert IPv4 and IPv6 addresses from text to binary form
	if(inet_pton(AF_INET, "192.168.30.114", &serv_addr.sin_addr)<=0)
	{
		printf("\nInvalid address/ Address not supported \n");
		return -1;
	}

	if (connect(sock, (struct sockaddr *)&serv_addr, sizeof(serv_addr)) < 0)
	{
		printf("\nConnection Failed \n");
		return -1;
	}
	
	Query_Loop(sock);
	
	close(sock);
	return 0;
}
