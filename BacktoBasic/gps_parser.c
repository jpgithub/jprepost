
// C program to Read GPS: GGA, RMC, and GLL NMEA sentence structure Data Using sscanf()
// Reference Source https://tavotech.com/gps-nmea-sentence-structure/

#include <stdio.h>
#include <string.h>

typedef struct {
		//RMC Format
		char talker_id[10]; // talker identifier
		float timestamp; // timestamp
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
		unsigned int checksum;
} gps_rmc_format;

typedef struct {
		//GGA Format
		char talker_id[10]; // talker identifier
		float timestamp; // timestamp
		float latitu;
		char lat_direction; // N or S
		float longit;
		char long_direction; // E or W
		short GPSQual;
		short Sats;
		float HDOP; // Horizontal Dilution of  precision
		float Alt; // Height above mean sea level
		char AltVal; //  reference unit for  altitude
		char GeoSep[3]; //  Geoidal separation measures in meters
		char Geoval; // Reference unit for geoidal separation
		char DGPSAge[3];
		char DGPSRef[4];
		unsigned int checksum;
} gps_gga_format;

typedef struct {
		//GLL Format
		char talker_id[10]; // talker identifier
		float latitu;
		char lat_direction; // N or S
		float longit;
		char long_direction; // E or W
		float timestamp; // timestamp
		char Status;
		char ModeInd;
		unsigned int checksum;
} gps_gll_format;

int main()
{		
    // Define a string containing the data to be parsed.
	char* GGA_data = "$GNGGA,062735.00,3150.788156,N,11711.922383,E,1,12,2.0,90.0,M,,M,,*55";
	char* RMC_data = "$GNRMC,060512.00,A,3150.788156,N,11711.922383,E,0.0,,311019,,,A,V*1B";
	char* GLL_data = "$GNGLL,3150.788156,N,11711.922383,E,062735.00,A,A*76";

	/*
	Scanset Characters
In C, scanf() provides a feature called scanset characters using %[] that lets you read a sequence of characters until a certain condition.
	*/
    char* str = "Ram,Manager,30";

    // Define variables to hold the parsed data.
    char name[10], designation[10];
    int age, ret;

    // Use sscanf to parse the string into the variables.
    ret = sscanf(str, "%[^,],%[^,],%d", name, designation, &age);
    printf("Ret Value: %i\n", ret);
    // Print the parsed data.
    printf("Name: %s\n", name);
    printf("Designation: %s\n", designation);
    printf("Age: %d\n", age);

    return 0;
}





