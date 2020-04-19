#include "pch.h"
#include "IODevice.h"

IODevice::IODevice(int id = 1)
{
	if (!ConnectToBusHub(id, std::this_thread::get_id()).second)
	{
		throw "IO Device ID Already Exist";
	}
	deviceid = id;
}

IODevice::~IODevice()
{
	DisconnectToBusHub(deviceid);
}

void IODevice::WriteToBusHub(const Packet& msg)
{
	WriteBusPacket(Drx::SM, msg);
}

void IODevice::ReadFromBusHub(Packet& msg)
{
	msg = ObserveBus(Drx::MS);
	if (msg.Receiver == "me")
	{
		int retry = 0;
		while (!TryTakeBusPacket(Drx::MS))
		{
			//retry
			retry++;

			if (retry > 3)
			{
				break;
			}
		};
	}	
}

void IODevice::WriteToBusHub(const Packet& msg, std::function<void(int)> method)
{
	WriteToBusHub(msg);
	/// Capture traffic
	// invoke method
	//std::invoke(method, 1);
}

void IODevice::ReadFromBusHub(Packet& msg, std::function<void(int)> method)
{
	ReadFromBusHub(msg);
	/// Capture traffic
	// invoke method
	//std::invoke(method, 0);
}
