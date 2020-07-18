#pragma once
#include <atomic>
#include <thread>
#include <mutex>
#include <vector>
#include <queue>
#include <functional>
#include "Packet.h"

class BusSystemModule
{
private:
	std::atomic<unsigned int> counter;
	std::atomic<bool> stopcounter;
	std::atomic<bool> stopbc;
	std::mutex mtx;
	std::mutex rt_mtx;
	std::thread clk;
	std::thread masterctrltask;
	std::condition_variable trigger;
	std::condition_variable rt_trigger;
	std::vector<std::thread> devicethreads;

	std::queue<Packet> busfifo;

	void InitClock();
	void ShutDown();
	void InitController();
	void InitDevice(int id);
public:
	BusSystemModule();
	~BusSystemModule();
	void Initialize();
	void PrimaryControllerInitialize();
	void DevicesInitialize();
	void Close();
	bool ReadFifo(Packet& p);
};

