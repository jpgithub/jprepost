#include <stdio.h>
#include <stdlib.h>
#include <complex.h>
#include <stdbool.h>

int main(){
		bool flag = true;
        if (flag)
        {
			#if __STDC_VERSION__ < 202311l
				printf("C23 Not Supported buit-in!\n");
			#else
				printf("C23 supported buit-in!\n");
			#endif	
        }
        short short_int = 0;
        printf("The short value %d and bytes size %ld\n", short_int, sizeof(short_int));
        int n = 5;
        int array[n];
        for (int i = 0; i < n; i++)
        {
                array[i] = i;
                printf("%d ", array[i]);
        }
        printf("\n");

/*         struct foo{
                int x;
                int y[];
        };
        printf("Size of struct foo:%ld \n", sizeof(struct foo));

        struct foo* f = malloc(sizeof(struct foo) + n * sizeof(int));
*/
		// Use typedef for better readability
		typedef struct{
			int x;
			int y[];
		}Foo;
		
		printf("Size of struct foo:%ld \n", sizeof(Foo));
		Foo* f = malloc(sizeof(Foo) + n * sizeof(int));
		if (f == NULL)
		{
			printf("Memory allocation failed\n");
			return -1;
		}
        printf("The memory address of foo ptr: %p\n", &f);
        f->x = 42;
        for  (int i = 0; i < n; i++)
        {
                f->y[i]=i;
                printf("%d ", f->y[i]);
        }
        printf("\n");

        double complex z = 3.0 + 4.0*I;
        printf("Real part: %f\n", creal(z));
        printf("Imaginary part: %f\n", cimag(z));

        inline int add(int x, int y)
        {
			return x + y;
        }
        printf("Result: %d\n", add(2,3));

        int* restrict  a = malloc(n * sizeof(int));
        int* restrict  b = malloc(n * sizeof(int));
        for (int i = 0; i < n; i++)
        {
                a[i] = i;
                b[i] = n - i - 1;
        }
        int sum = 0;
        for (int i = 0; i < n; i++){
                sum += a[i] * b[i];
        }
        printf("Result: %d\n", sum);
		free(f)
		
		// Data Type boundary
		int int_32 = 0x7FFFFFFF; // -2^31 to 2^31 - 1
		long int l_int_32 = 0xFFFFFFFF;  // -2^63 to 2^63 - 1
		long long int ll_int_64 = 0x7FFFFFFFFFFFFFFF;  // -2^63 to 2^63 - 1
		long double ld = 0xFFFFFFFFFFFFFFFF;  // -2^64 to 2^64 - 1
		printf("int range value, %i\n",  int_32);
		// Turns out this is also 8 bytes on ubuntu x86 
		printf("long int range value, %li\n",  l_int_32);
		//  Turn out this is also 8 bytes on ubuntu x86
		printf("long long int range value, %lli\n",  ll_int_64);
		printf("long long double range value, %Lf\n",  ld);
		/*
		1011 = -5
	+	1110 = -2
(1).....1001
		*/
		// Memory over flow detection
		unsigned int mask = 0x1;
        mask <<= (sizeof(int_32) * 8) - 1;
        printf("Mask value: %x\n", mask);
        unsigned int carry_bit = mask & int_32;
        printf("carry in, %x\n",carry_bit);
        carry_bit = mask & (int_32 + -1);
        printf("carry out, %x\n", carry_bit);

        int neg_int_32 = 0x80000000; // This is -2^31
        unsigned int carry_in = mask & neg_int_32;
        printf("carry in, %x\n", carry_in);
        unsigned int carry_out = mask & (neg_int_32 + -1);
        printf("carry out, %x\n", carry_out);
        if (carry_out != carry_in)
        {
                printf("Overflow Detected\n");
        }
        return 0;
}