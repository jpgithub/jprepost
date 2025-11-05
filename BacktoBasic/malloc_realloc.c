// Online C compiler to run C program online
#include <stdio.h>
#include <string.h>
#include <stdlib.h>

char* getCharArray() {
    char* arr = (char*)malloc(128 * sizeof(char)); // Dynamically allocate memory
    if (arr == NULL) {
        return NULL; // Handle memory allocation failure
    }
    arr = "Hello World";
    return arr;
}

char* print_string()
{
    char *mystr = getCharArray();
    char *myptr_one = &mystr[1];
    printf("My substring: %s\n", mystr);
	free(mystr);
    return myptr_one;
}


int main()
{
    char *mysubstr = print_string();
    printf("My string: %s\n", mysubstr);
}

















