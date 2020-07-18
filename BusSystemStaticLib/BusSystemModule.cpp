#include "pch.h"
#include "BusSystemModule.h"
#include "PrimaryController.h"
#include "IODevice.h"

using namespace std;

void BusSystemModule::InitClock()
{	
	std::this_thread::sleep_for(std::chrono::seconds(1));
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
	stopbc = true;
	trigger.notify_all();
	if (clk.joinable())
		clk.join();
	if (masterctrltask.joinable())
		masterctrltask.join();

	//std::cout << "synchronizing all threads...\n";
	/*for (auto& th : devicethreads)
	{
		if (th.joinable()) {
			th.join();
		}
	}*/
}

void BusSystemModule::InitController()
{
	//unsigned int ccc = 0;
	PrimaryController masterctrls;
	while (!stopbc)
	{		
		std::unique_lock<std::mutex> lck(mtx);
		trigger.wait(lck);
		/// Do something by invoke givne method and pass an in master id;
		//std::invoke(method,0);
		Packet p;
		p.Data.push_back(counter);
		masterctrls.WriteToBusHub(p);
		busfifo.push(p);
		//// Read some response
		//trigger.notify_one();
	}
}

void BusSystemModule::InitDevice(int id)
{
	
	IODevice slavectrls(id);
	while (!stopbc)
	{
		std::unique_lock<std::mutex> lck(rt_mtx);
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
	counter = 0;
	stopbc = false;
}

BusSystemModule::~BusSystemModule()
{
	ShutDown();
}

void BusSystemModule::Initialize()
{
	// start the clock thread;
	clk = thread(&BusSystemModule::InitClock,this);

	// start the master thread;
	masterctrltask = thread(&BusSystemModule::InitController, this);

	//// start 10 device thread;
	//for (int devicecounter = 1; devicecounter <= 10; ++devicecounter)
	//{
	//	devicethreads.push_back(thread(&BusSystemModule::InitDevice, this, devicecounter));
	//}
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

bool BusSystemModule::ReadFifo(Packet& p)
{
	if (!busfifo.empty())
	{
		p = busfifo.front();
		busfifo.pop();
		return true;
	}
	return false;
}
