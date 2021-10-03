// ConsoleApplication2.cpp : Defines the entry point for the console application.
//

#include "stdafx.h"
#include <iostream>
#include <cmath>
#include <vector>
using namespace std;

typedef unsigned long long ULONG;

ULONG trial_division(ULONG n)
{
	//// prime factor
	ULONG pf = 1;
	
    ULONG end = (ULONG)sqrt(n);
	for(ULONG i = 2; i <= end; i++)
    {
		if(i == 2 && n % i == 0)
		{
			//// return 2 as one of the prime factor
			pf = i;
			break;
		}
		else
		{
			//// skip all even numbers
            if(i % 2 == 0)
            {
                continue;
            }
			else
			{
				if(n % i == 0)
				{
					pf = i;
					break;
				}
			}
		}
	}
	return pf;
}

ULONG factorization(ULONG n)
{
	vector<ULONG> primefactorlist;
	ULONG pf;
	ULONG v = n;
	while (true)
	{
		pf = trial_division(v);
		if(pf == 1)
		{
			primefactorlist.push_back(v);
			break;
		}
		else
		{
			v /= pf;
			primefactorlist.push_back(pf);
		}
	}

	//// Test Assertion
	ULONG sum = 1;
	ULONG gpf = 1;
	for (auto &f : primefactorlist)
	{
		sum *= f;
		if(gpf < f)
		{
			gpf = f;
		}
	}

	if(sum == n)
	{
		return gpf;
	}
	else
	{
		throw new exception("Trial Division Assertion Failure");
	}
}

int _tmain(int argc, _TCHAR* argv[])
{
	try
	{
		cout << "Largest Prime Factor Number:" <<endl;
		cout << factorization(600851475143) <<endl;
	}
	catch(exception e)
	{
		cout << e.what() <<endl;
	}
	return 0;
}

