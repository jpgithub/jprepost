// Online C compiler to run C program online
#include <stdio.h>
#include <string.h>
#include <stdlib.h>

char* getCharArray() {
    char* arr = (char*)malloc(128 * sizeof(char)); // Dynamically allocate memory
    if (arr == NULL) {
        return NULL; // Handle memory allocation failure
    }
    strcpy(arr, "Hello World");
    //Avoid overwriting the pointer. Instead, copy the string into the allocated memory using strcpy or similar functions:
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
	// mysubstr doesn't value doesn't exist anymore
    printf("My string: %s\n", mysubstr);
	printf("Starting Exercise Two: printing-tokens \n");
	/*Exercise Two:
	// SRC - https://www.hackerrank.com/challenges/printing-tokens-/problem?isFullScreen=true
	*/
	char *s;
    s = malloc(1024 * sizeof(char));
    scanf("%[^\n]", s);
	printf("Base Address: %p before realloc call\n", s);
    s = realloc(s, strlen(s) + 1);
	printf("Base Address: %p after realloc call\n", s);
	// Does realloc change the base pointer address?
	// replace all space with newline character ascii encoding.
	for (int i=0; i < strlen(s); i++)
	{
		if (s[i] == ' ')
		{
			s[i] = '\n';
		}
	}
	printf("%s",s);
    return 0;
}

















