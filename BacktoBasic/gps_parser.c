
// C program to Read GPS: GGA, RMC, and GLL NMEA sentence structure Data Using sscanf()

#include <stdio.h>
#include <string.h>

typedef struct {
		//RMC Format
		unsigned int id; // talker identifier
		double timestamp; // timestamp
		char status;  // A or V
		float latitu;
		char lat_direction; // N or S
		float longit;
		char long_direction; // E or W
		float sog;
		float cog;
		char date[6]; // 311019 Date in format ddmmyy
		float MagVar;
		char MagVarDir; // E or W
		char Mode;
		char NavStatus;
		char checksum;
} gps_rmc_format;

int main()
{		
    // Define a string containing the data to be parsed.
    char* str = "Ram Manager 30";
	
    // Define variables to hold the parsed data.
    char name[10], designation[10];
    int age, ret;

    // Use sscanf to parse the string into the variables.
    ret = sscanf(str, "%s %s %d", name, designation, &age);

    // Print the parsed data.
    printf("Name: %s\n", name);
    printf("Designation: %s\n", designation);
    printf("Age: %d\n", age);

    return 0;
}





