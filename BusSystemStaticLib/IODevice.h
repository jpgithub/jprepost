#pragma once
#include "BusHub.h"
class IODevice :
	public BusHub
{
private:
	int deviceid;
public:
	IODevice(int id);
	~IODevice();
	void WriteToBusHub(const Packet& msg, std::function<void(int)> method);
	void ReadFromBusHub(Packet& msg, std::function<void(int)> method);
	// Inherited via BusHub
	virtual void WriteToBusHub(const Packet& msg) override;
	virtual void ReadFromBusHub(Packet& msg) override;
};

