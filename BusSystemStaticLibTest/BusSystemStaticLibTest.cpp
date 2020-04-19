#include "pch.h"
#include "CppUnitTest.h"
#include "../BusSystemStaticLib/BusSystemStaticLib.cpp"

using namespace Microsoft::VisualStudio::CppUnitTestFramework;

namespace BusSystemStaticLibTest
{
	TEST_CLASS(BusSystemStaticLibTest)
	{
	public:
		
		TEST_METHOD(fnBusSystemStaticLibMethod)
		{
			std::string expect = "Hello World";
			std::string actual = fnBusSystemStaticLib();
			Assert::AreEqual(expect, actual);
		}
	};
}
