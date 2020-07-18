#include "pch.h"
#include "BusHub.h"

bool BusHub::ObserveBus(Drx direction, Packet& msg)
{
	if (direction == Drx::MS && !bus_ms_direction.empty())
	{
		msg = bus_ms_direction.front();
		return true;
	}

	if (direction == Drx::SM && !bus_sm_direction.empty())
	{
		msg = bus_sm_direction.front();
		return true;
	}
	return false;
}

bool BusHub::TryTakeBusPacket(Drx direction)
{
	if (direction == Drx::MS)
	{
		if (ms_mtx.try_lock()) 
		{
			bus_ms_direction.pop();
			ms_mtx.unlock();
			return true;
		}
		else 
		{
			return false;
		}
	}

	if (direction == Drx::SM)
	{
		bus_sm_direction.pop();
		return true;
	}

	return false;
}

bool BusHub::WriteBusPacket(Drx direction, const Packet& msg)
{
	if (direction == Drx::MS)
	{
		bus_ms_direction.push(msg);
		return true;
	}

	if (direction == Drx::SM)
	{
		if (sm_mtx.try_lock())
		{
			bus_sm_direction.push(msg);
			sm_mtx.unlock();
			return true;
		}
		else
		{
			return false;
		}
	}

	return false;
}

std::pair<std::map<int, std::thread::id>::iterator, bool> BusHub::ConnectToBusHub(int id, std::thread::id thread_id)
{
	return connections.insert(std::pair<int, std::thread::id>(id, thread_id));
}

bool BusHub::DisconnectToBusHub(int id)
{
	if (connections.find(id) != connections.end())
	{
		connections.erase(id);
		return true;
	}
	return false;
}

std::pair<std::map<int, std::thread::id>::iterator, bool> BusHub::AddPrimaryController(std::thread::id thread_id)
{
	return connections.insert(std::pair<int, std::thread::id>(primarycontrollerid, thread_id));
}

bool BusHub::RemovePrimaryController()
{
	if (connections.find(primarycontrollerid) != connections.end())
	{
		connections.erase(primarycontrollerid);
		return true;
	}

	return false;
}
