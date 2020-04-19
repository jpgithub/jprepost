#pragma once
#include <vector>
#include <string>

enum class Drx
{
	//// Master to Slave
	MS = 0,
	//// Slave to Master
	SM = 1,
};

class Packet
{
public:		
	std::string Sender;
	std::string Receiver;
	std::vector<unsigned int> Data;
};

