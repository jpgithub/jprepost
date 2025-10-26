#include <stdio.h>
#include <stdlib.h>
#include <complex.h>
#include <stdbool.h>

int main(){
		bool isC23 = true;
        if (isC23)
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
        return 0;
}