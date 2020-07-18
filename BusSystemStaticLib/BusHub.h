#pragma once
#include "Packet.h"
#include <functional>
#include <queue>
#include <mutex>
#include <map>
#include <thread>
class BusHub
{
private:
	//// master send to slave
	std::queue<Packet> bus_ms_direction;
	std::mutex ms_mtx;
	//// slaves send to master
	std::queue<Packet> bus_sm_direction;
	std::mutex sm_mtx;
	std::map<int, std::thread::id> connections;
	const int primarycontrollerid = 0;
protected:
	bool ObserveBus(Drx direction, Packet& msg);
	bool TryTakeBusPacket(Drx direction);
	bool WriteBusPacket(Drx direction, const Packet& msg);
	std::pair<std::map<int, std::thread::id>::iterator, bool> ConnectToBusHub(int id, std::thread::id thread_id);
	bool DisconnectToBusHub(int id);
	std::pair<std::map<int, std::thread::id>::iterator, bool> AddPrimaryController(std::thread::id thread_id);
	bool RemovePrimaryController();
public:	
	virtual void WriteToBusHub(const Packet& msg) = 0;;
	virtual void ReadFromBusHub(Packet& msg) = 0;
	/*
	virtual void WriteToBusHub(const Packet& msg, std::function<void(int)> method) = 0;
	virtual void ReadFromBusHub(Packet& msg, std::function<void(int)> method) = 0;*/

};

