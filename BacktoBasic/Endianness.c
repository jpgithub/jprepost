//  Raw endianess check
//  Big Endian - Little Order Address Comes first (Ascending Addressing)
// Little Endian - Descend Addressing , High Order Address Comes  first
// https://en.wikipedia.org/wiki/Endianness
#include <stdio.h>
#include <stdlib.h>


int main()
{
        // Testing endiness
        //long long unsigned int value = 0xFFFF00000000FFFF;
        //int *ptr = (int*)&value;
        int value  = 0x0A0B0C0D;
        char *ptr = (char*)&value;
        printf("\n%x\n %p\n",*ptr, ptr);
        printf("\n%x\n %p\n",*(ptr+1),ptr+1);

        if (ptr+1 > ptr) && (0D == *ptr)
                printf("True\n");
        else
                printf("False\n");
}