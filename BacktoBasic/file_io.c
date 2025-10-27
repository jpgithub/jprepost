#include <stdio.h>
#include <stdlib.h>

int main()
{
   char* sentence;
   FILE *fptr;

   // use appropriate location if you are using MacOS or Linux
   fptr = fopen("example.txt","w");

   if(fptr == NULL)
   {
      printf("Error!");   
      exit(1);             
   }

   printf("Enter string: ");
   scanf("%[^\n]",sentence);

   fprintf(fptr,"%s",sentence);
   fclose(fptr);
   
   //read back after writing
   fptr = fopen("example.txt", "rb"); // Open in binary mode
   if (fptr == NULL) {
       printf("Error: Could not open file.\n");
       return 1;
   }
   char buffer[1024];
   size_t bytesRead;
   while ((bytesRead = fread(buffer, 1, sizeof(buffer), fptr)) > 0) {
       fwrite(buffer, 1, bytesRead, stdout); // Write to stdout
   }
   fclose(fptr);
   printf("\n");
   return 0;
}