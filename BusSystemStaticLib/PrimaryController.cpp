#include "pch.h"
#include "PrimaryController.h"

PrimaryController::PrimaryController()
{
	if (!AddPrimaryController(std::this_thread::get_id()).second)
	{
		throw "Primary Controller Exist Already";
	}
}

PrimaryController::~PrimaryController()
{
	/*while (!housekeeping.empty())
	{
		housekeeping.pop();
	}*/
}

//PrimaryController* PrimaryController::Promote()
//{
//	housekeeping.push(new PrimaryController());
//	return housekeeping.top();
//}
//
//bool PrimaryController::Demote(PrimaryController* currentController)
//{
//	return currentController->RemovePrimaryController();
//}

void PrimaryController::WriteToBusHub(const Packet& msg)
{
	WriteBusPacket(Drx::MS, msg);
}

void PrimaryController::ReadFromBusHub(Packet& msg)
{
	if (ObserveBus(Drx::SM, msg))
	{
		TryTakeBusPacket(Drx::SM);
	}
}

void PrimaryController::WriteToBusHub(const Packet& msg, std::function<void(int)> method)
{
	WriteToBusHub(msg);
	/// Capture traffic
	// invoke method
	//std::invoke(method, 0);
}

void PrimaryController::ReadFromBusHub(Packet& msg, std::function<void(int)> method)
{
	ReadFromBusHub(msg);
	/// Capture traffic
	// invoke method
	//std::invoke(method, 1);
}

