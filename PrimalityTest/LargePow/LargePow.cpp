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
		if(r != 0)
		{
			//// Carry out
			b[i+1] += b[i] / base10;
			b[i] = r;
		}
	}
	return b;
}

//// ?
int Multiply(int x, int res[], int res_size)
{
	int carry = 0;
	for(int i = 0; i < res_size; i++)
	{
		int prod = res[i] * x + carry;
		res[i] = prod % 10;
		carry = prod /10;
	}

	while(carry)
	{
		res[res_size] = carry % 10;
		carry = carry/10;
		res_size++;
	}
	return res_size;
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
	
	// one digit multiplication
	//auto one_res = Multiply(res, (USHORT) (many_digit % base10));
		
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
		//for(unsigned int j = 0; j < product.size(); j++)
		//{
		//	cout << product[j] << endl;
		//	//total[j] += product[j];
		//	////// check for carry
		//	//int c = product[j] % base10;
		//	//if(c != 0)
		//	//{
		//	//	total[j+1] += product[j] / base10;
		//	//	total[j] = c;
		//	//}
		//}
	}
	//// descending base 10 power order
	reverse(total.begin(),total.end());
	return total;
}

void Power(ULONG n)
{

}

int _tmain(int argc, _TCHAR* argv[])
{
	cout << "Hello Large Pow" <<endl;
	cout << "2^64-1 has 26 digits ~ 10^19 with the value of 18,446,744,073,709,551,615" <<endl;
	//ULONG max_base = 10e19;

	int res [4] = { 3, 6 };
	vector<USHORT> vres;
	//// 36
	vres.push_back(6);
	vres.push_back(3);

	ULONG input = 421;

	vres = Multiply(vres, input);
	for (auto &i : vres)
	{
		cout << i;
		
	}
	cout << endl;
	////// get the number of digits;
	//double digits = floor(log10(input));

	//// one digit multiplication
	//auto one_res = Multiply(vres, (USHORT) (input % base10));
	//	
	////// Check for more than 1 digits multiplication
	//if(digits > 0)
	//{
	//	input = (input / base10);
	//	//// shifting by add zero
	//	for ( int i = (int)digits; i > 0; i--)
	//	{
	//		vres.insert(vres.begin(),0);
	//		vres = Multiply(vres, input);
	//		/// Large Sum
	//		for(int j = 0; j < one_res.size(); j++)
	//		{
	//			vres[j] += one_res[j];
	//			//// check for carry
	//			int c = vres[j] % base10;
	//			if(c != 0)
	//			{
	//				vres[j+1] += vres[j] / base10;
	//				vres[j] = c;
	//			}
	//		}
	//	}
	//}

	return 0;
}

