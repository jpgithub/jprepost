#include "pch.h"
#include "BusSystemModule.h"
#include "PrimaryController.h"
#include "IODevice.h"

using namespace std;

void BusSystemModule::InitClock()
{	
	while (!stopcounter)
	{
		trigger.notify_one();
		std::this_thread::sleep_for(std::chrono::microseconds(2500));
		counter++;
	}
}

void BusSystemModule::ShutDown()
{
	stopcounter = true;
	clk.join();
	masterctrltask.join();
	//std::cout << "synchronizing all threads...\n";
	for (auto& th : devicethreads) th.join();
}

void BusSystemModule::InitController()
{
	std::unique_lock<std::mutex> lck(mtx);
	PrimaryController masterctrls;
	while (!stopcounter)
	{
		trigger.wait(lck);
		/// Do something by invoke givne method and pass an in master id;
		//std::invoke(method,0);
		Packet p;
		masterctrls.WriteToBusHub(p);
		//// Read some response
	}
}

void BusSystemModule::InitDevice(int id)
{
	std::unique_lock<std::mutex> lck(mtx);
	IODevice slavectrls(id);
	while (!stopcounter)
	{
		trigger.wait(lck);
		/// Do something by invoke givne method and pass an in master id;
		//std::invoke(method, 1);
		Packet p;
		slavectrls.ReadFromBusHub(p);
		//// DO some more stuff
	}
}

BusSystemModule::BusSystemModule()
{
	stopcounter = false;
}

BusSystemModule::~BusSystemModule()
{
	ShutDown();
}

void BusSystemModule::Initialize()
{
	// start the clock thread;
	clk = thread(&BusSystemModule::InitClock,this);
}

void BusSystemModule::PrimaryControllerInitialize()
{
	// start the master thread;
	masterctrltask = thread(&BusSystemModule::InitController, this);
}

void BusSystemModule::DevicesInitialize()
{
	// start 10 device thread;
	for (int devicecounter = 1; devicecounter <= 10; ++devicecounter)
	{
		devicethreads.push_back(thread(&BusSystemModule::InitDevice, this, devicecounter));
	}		
}

void BusSystemModule::Close()
{
	ShutDown();
}
