#include <string.h>
#include <stdlib.h>
char* addBinary(char* a, char* b) {
    const int ascii_offset = 0x30;
    // Get the length for the a and b
    const int a_len = strlen(a);
    const int b_len = strlen(b);
	int size = a_len + 1;// +1 to include the null terminator
	if (a_len > b_len)
	{
		size += 1;
	}
	else{
		size = b_len + 2;
	}
    char* sum = (char*)malloc(size * sizeof(char));
    // carry over digit and  +1 to include the null terminator
     if (sum == NULL) {
        return NULL; // Handle memory allocation failure
    }
	int length = size-1;
    sum[size - 1]= '\0';
	int j = a_len-1;
	int k = b_len-1;
	int carry = 0;
    for (i = length-1; i > -1; i--)
    {
		int A = 0;
		int B = 0;
		if (j >= 0)
		{
			A = (a[j]-ascii_offset);
		}
		if (k >= 0)
		{
			B = (b[k]-ascii_offset);
		}
		int ans = A + B + carry;
       if (ans == 2)
       {
		   // 1 + 1 = 10
          carry = 1;
          sum[i] = ascii_offset;
       }
	   else if(ans==3)
	   {
		   // 1 + 1 + 1 = 11
		   carry = 1;
		   sum[i] = ascii_offset + 0x1;
	   }
	   else
	   {
          carry = 0;
          sum[i] = ans + ascii_offset;
       }
	   k--;
	   j--;
    }
	
	// Check for leading zero and make adjustment
	if (sum[0] == '0')
	{
		return &sum[1];
	}
	else
	{
		return sum;
	}
	
}