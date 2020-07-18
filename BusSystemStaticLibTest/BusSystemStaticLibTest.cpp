#include "pch.h"
#include "CppUnitTest.h"
#include "../BusSystemStaticLib/BusSystemModule.h"
#include "../BusSystemStaticLib/Packet.h"

using namespace Microsoft::VisualStudio::CppUnitTestFramework;

namespace BusSystemStaticLibTest
{
	TEST_CLASS(BusSystemStaticLibTest)
	{
	public:
		
		/*TEST_METHOD(fnBusSystemStaticLibMethod)
		{
			std::string expect = "Hello World";
			std::string actual = fnBusSystemStaticLib();
			Assert::AreEqual(expect, actual);
		}*/

		TEST_METHOD(BusSytemModuleMethod)
		{
			BusSystemModule target;

			target.Initialize();
			//controller.PrimaryControllerInitialize();
			//controller.DevicesInitialize();
			unsigned int cc[1000];

			unsigned int i = 0;

			while (i < 1000)
			{
				Packet p;
				if (target.ReadFifo(p))
				{
					cc[i] = p.Data.at(0);
					i++;
				}

			}
			
			
			i = 0;
			while (i < 1000)
			{
				Assert::AreEqual(i, cc[i]);
				i++;
			}
			target.Close();
		}
	};
}
