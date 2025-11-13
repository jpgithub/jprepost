#include <stdio.h>
#include <string.h>

int main()
{
        char s[]="Hello, World!";
        // Finding the first occurence of 'o'in string s
        char *res = strchr(s,'o');
        if (res != NULL)
                printf("Character found at: %ld index\n", res - s);
        else
                printf("Character not found\n");

        // Finding the last occurence of 'o' is string s
        res = strrchr(s,'o');

        if (res != NULL)
                printf("Character found at: %ld index\n", res - s);
        else
                printf("Character not found\n");
			
		char s1[30] = "Hello, ";
		char s2[] = "Geeks!";
		
		// Appends "Geeks!" to Hello, "
		strcat(s1, s2);
		printf("%s", s1);
		
		strncat(s1, s2, 4);
		printf("%s", s1);
		
		printf("%lu", strlen(s));
		
		
		// Str Token
		
		char stk[] = "Hello, Geeks, C!";
		// Initializing tokens
		char *t = strtok(stk, ", ");
		
		// Printing rest of the tokens
		while (t != NULL)
		{
			printf("%s\n", t);
			t = strtok(NULL, ", ");
		}

        return 0;
}