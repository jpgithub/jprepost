
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
		char date[7]; // 311019 Date in format ddmmyy
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
		char GeoSep[4]; //  Geoidal separation measures in meters,  //  Geoidal separation measures in meters, e.g X.X\0 escape character is needed
		char Geoval; // Reference unit for geoidal separation
		char DGPSAge[4];
		char DGPSRef[5];
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
	/*
	Scanset Characters
In C, scanf() provides a feature called scanset characters using %[] that lets you read a sequence of characters until a certain condition.
	*/
    // Define a string containing the data to be parsed.
    char* GGA_data = "$GNGGA,062735.00,3150.788156,N,11711.922383,E,1,12,2.0,90.0,M,X.X,M,X.X,XXXX*55";
    //char gga_data[] = "$GNGGA,062735.00,3150.788156,N,11711.922383,E,1,12,2.0,90.0,M,,M,,*55";
    char* RMC_data = "$GNRMC,060512.00,A,3150.788156,N,11711.922383,E,0.0,0.0,311019,,,A,V*1B";
	// This parsing error cause from a series of null value ,,, after 311019
    char* GLL_data = "$GNGLL,3150.788156,N,11711.922383,E,062735.00,A,A*76";

    // Define variables to hold the parsed data.
    int ret;
    gps_gll_format gll_pos;
    gps_gga_format gga_pos;
    gps_rmc_format rmc_pos;

    // Use sscanf to parse the string into the variables.
    ret = sscanf(GLL_data,
                    "$%[^,],%f,%c,%f,%c,%f,%c,%c*%u",
                    gll_pos.talker_id, &gll_pos.latitu, &gll_pos.lat_direction, &gll_pos.longit,
                    &gll_pos.long_direction,&gll_pos.timestamp,&gll_pos.Status,&gll_pos.ModeInd,&gll_pos.checksum);
    // Print the parsed data.
    printf("Talker ID: %s\n", gll_pos.talker_id);
    printf("Latitude : %f\n", gll_pos.latitu);
    printf("Latitude Direction:  %c\n", gll_pos.lat_direction);
    printf("Longtitude :  %f\n",  gll_pos.longit);
    printf("Longtitude Direction: %c\n", gll_pos.long_direction);
    printf("Timestamp : %f\n", gll_pos.timestamp);
    printf("Status: %c\n", gll_pos.Status);
    printf("ModeInd: %c\n", gll_pos.ModeInd);
    printf("Checksum: %u\n", gll_pos.checksum);
    printf("Ret Value: %i\n", ret);

    ret = sscanf(GGA_data,
                    "$%[^,],%f,%f,%c,%f,%c,%hd,%hd,%f,%f,%c,%[^,],%c,%[^,],%[^*]*%u",
                    gga_pos.talker_id, &gga_pos.timestamp, &gga_pos.latitu, &gga_pos.lat_direction,
                    &gga_pos.longit,&gga_pos.long_direction,&gga_pos.GPSQual,&gga_pos.Sats,&gga_pos.HDOP,
                    &gga_pos.Alt,&gga_pos.AltVal,gga_pos.GeoSep,&gga_pos.Geoval,gga_pos.DGPSAge,gga_pos.DGPSRef,&gga_pos.checksum);
					
    printf("Talker ID: %s\n", gga_pos.talker_id);
    printf("Latitude : %f\n", gga_pos.latitu);
    printf("Latitude Direction:  %c\n", gga_pos.lat_direction);
    printf("Longtitude :  %f\n",  gga_pos.longit);
    printf("Longtitude Direction: %c\n", gga_pos.long_direction);
    printf("Timestamp : %f\n", gga_pos.timestamp);
    printf("GPSQual: %hd\n", gga_pos.GPSQual);
    printf("Sats: %hd\n", gga_pos.Sats);
    printf("HDOP: %f\n", gga_pos.HDOP);
    printf("Alt: %f\n", gga_pos.Alt);
    printf("Alt Value: %c\n", gga_pos.AltVal);
    printf("Geo Value: %c\n",  gga_pos.Geoval);
    printf("Geo Sep: %3s\n", gga_pos.GeoSep);
    printf("DGPSAge: %3s\n", gga_pos.DGPSAge);
    printf("DGPSRef: %4s\n", gga_pos.DGPSRef);
    printf("Checksum: %u\n", gga_pos.checksum);
    printf("Ret Value: %i\n", ret);


     ret  = sscanf(RMC_data,
                     "$%[^,],%f,%c,%f,%c,%f,%c,%f,%f,%[^,],%f,%c,%c,%c*%u",
                     rmc_pos.talker_id,&rmc_pos.timestamp,&rmc_pos.status,&rmc_pos.latitu,
                     &rmc_pos.lat_direction,&rmc_pos.longit,&rmc_pos.long_direction,
                     &rmc_pos.sog,&rmc_pos.cog,rmc_pos.date,&rmc_pos.MagVar,&rmc_pos.MagVarDir,
                     &rmc_pos.Mode,&rmc_pos.NavStatus,&rmc_pos.checksum);
     printf("Talker ID: %s\n", rmc_pos.talker_id);
     printf("Latitude : %f\n", rmc_pos.latitu);
     printf("Latitude Direction:  %c\n", rmc_pos.lat_direction);
     printf("Longtitude :  %f\n",  rmc_pos.longit);
     printf("Longtitude Direction: %c\n", rmc_pos.long_direction);
     printf("Timestamp : %f\n", rmc_pos.timestamp);
	 printf("Status : %c\n", rmc_pos.status);
     printf("SOG : %f\n", rmc_pos.sog);
     printf("COG : %f\n", rmc_pos.cog);
     printf("Date : %s\n", rmc_pos.date);
     printf("Ret Value: %i\n",  ret);

    // Tokenizate
    //char*  t = strtok(gga_data,",");
    //while (t != NULL)
    //{
//          printf("%s\n",t);
//          t = strtok(NULL,",");
//    }
    return 0;
}





