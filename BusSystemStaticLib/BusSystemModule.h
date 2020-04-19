#pragma once
#include <atomic>
#include <thread>
#include <mutex>
#include <vector>
#include <functional>

class BusSystemModule
{
private:
	std::atomic<unsigned int> counter;
	std::atomic<bool> stopcounter;
	std::mutex mtx;
	std::thread clk;
	std::thread masterctrltask;
	std::condition_variable trigger;

	std::vector<std::thread> devicethreads;

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
};

