#pragma once
#include "BusHub.h"
#include <stack>
class PrimaryController :
	public BusHub
{
//private:
//	static std::stack<PrimaryController*> housekeeping;
public:
	PrimaryController();
	~PrimaryController();
	/*static PrimaryController* Promote();
	static bool Demote(PrimaryController* currentController);*/

	void WriteToBusHub(const Packet& msg, std::function<void(int)> method);
	void ReadFromBusHub(Packet& msg, std::function<void(int)> method);
	// Inherited via BusHub
	virtual void WriteToBusHub(const Packet& msg) override;
	virtual void ReadFromBusHub(Packet& msg) override;
};

