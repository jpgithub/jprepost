// LargePow.cpp : Defines the entry point for the console application.
//

#include "stdafx.h"
#include <iostream>
#include <vector>
using namespace std;

typedef unsigned long long ULONG;
typedef unsigned short USHORT;

bool check_overflow(ULONG p)
{
	ULONG ov = 1;
	if((numeric_limits<ULONG>::max() - p < ov))
	{
		return true;
	}
	else
	{
		return false;
	}
}

vector<USHORT> Digits_Tokenize(ULONG many_digit)
{
	const USHORT base10 = 10;
	USHORT digits_count = 0;
	vector<USHORT> digits_tokenize;
	ULONG md = many_digit;
	while (md != 0)
	{
		USHORT d = (USHORT)(md % base10);
		if(d != 0)
		{
			digits_tokenize.push_back(d);
		}
		md = md / base10;
		digits_count++;
	}
	return digits_tokenize;
}

vector<USHORT> Large_Sum(vector<USHORT> a, vector<USHORT> b)
{
	const USHORT base10 = 10;
	unsigned int length = a.size();
	
	//// resize the shorter length;
	if(a.size() > b.size())
	{
		length = b.size();
		a.swap(b);
	}

	for(unsigned int i = 0; i < length; i++)
	{
		b[i] += a[i];
		int r = b[i] % base10;
		if(b[i] >= base10 && r != 0)
		{
			//// Carry out
			b[i+1] += b[i] / base10;
			b[i] = r;
		}
	}
	return b;
}

vector<USHORT> Multiply(vector<USHORT> res, USHORT single_digit)
{
	USHORT carry = 0;
	const USHORT base10 = 10;
	for(unsigned int i = 0; i < res.size(); i++)
	{
		USHORT p = res[i] * single_digit + carry;

		if(check_overflow(p))
		{
			break;
		}

		//// Store the ones digit
		res[i] = p % base10;

		/// Carry the 1
		carry = p / base10;
	}
	while(carry > 0)
	{
		res.push_back( carry % base10);
		carry /= base10;
	}
	return res;
}

vector<USHORT> Multiply(vector<USHORT> res, ULONG many_digit)
{
	const USHORT base10 = 10;

	//// get the number of digits;
	USHORT digits_count = 0;
	vector<USHORT> digits_tokenize = Digits_Tokenize(many_digit);
	digits_count = digits_tokenize.size();

	/*ULONG md = many_digit;
	while (md != 0)
	{
		USHORT d = (USHORT)(md % base10);
		if(d != 0)
		{
			digits_tokenize.push_back(d);
		}
		md = md / base10;
		digits_count++;
	}*/
	
	//// Check for more than 1 digits multiplication
	//USHORT input = (USHORT)(many_digit / base10);
	vector<USHORT> total (digits_count + 1, 0);	
	//// shifting by add zero
	for ( int i = 0; i < digits_count; i++)
	{
		if(i > 0)
		{
			res.insert(res.begin(),0);			
		}

		auto product = Multiply(res, digits_tokenize[i]);
		
		/// Large Sum
		total = Large_Sum(product,total);
	}
	//// descending base 10 power order
	reverse(total.begin(),total.end());
	return total;
}

vector<USHORT> Power(ULONG base, ULONG exponent)
{
	//// Convert
	vector<USHORT> pow_res = Digits_Tokenize(base);
	USHORT m = (USHORT)base;
	for(unsigned int i = 0; i < (exponent - 1); i++)
	{
		pow_res = Multiply(pow_res, m);
	}
	reverse(pow_res.begin(),pow_res.end());
	return pow_res;
}

int _tmain(int argc, _TCHAR* argv[])
{
	cout << "Hello Large Pow" <<endl;
	cout << "2^64-1 has 26 digits ~ 10^19 with the value of 18,446,744,073,709,551,615" <<endl;
	//ULONG max_base = 10e19;

	int res [4] = { 3, 6 };
	vector<USHORT> vres;
	//// 36
	//vres.push_back(6);
	//vres.push_back(3);

	//// 421
	vres.push_back(1);
	vres.push_back(2);
	vres.push_back(4);

	//ULONG input = 421;
	ULONG input = 36;

	vres = Multiply(vres, input);

	for (auto &i : vres)
	{
		cout << i;
	}
	cout << endl;

	auto tres = Power(2,100);

	for (auto &i : tres)
	{
		cout << i;
	}
	cout << endl;

	
	return 0;
}

