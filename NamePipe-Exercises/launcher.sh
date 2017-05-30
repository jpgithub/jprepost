#!/bin/sh
#if [ $# -ne 1 ]; then
#echo "usage: $0 MIN (example: $0 1)";
#exit 1;
#fi
 
#TIME=$(($1*60))
#CUR=$TIME
 
#while [ $CUR -gt 0 ]
#do
# CUR=$(($CUR - 1))
# clear
# echo $CUR
# sleep 1
#done

#echo "Details for $1";
#ls -lh $1

# Concurrent System
TBA="ProcessHolder"
ID="$(pidof $TBA)"

#CPU Affinity Associated with Process
#sed command (Stream Editor)
CPULIST0="$(taskset -p -c $ID | sed 's/.*list: \([0-9]*\).*/\1/')";

echo $CPULIST0

index=$CPULIST0

# This will autoselect a cpu index that is not on CPULIST
if [ $index -gt $CPULIST0 ] # && [ $index -gt $CPULIST1 ]
then
	index=$index
else
	index=$(($index + 1))
fi
echo $index


#until [$index -gt $CPULIST0]
#do
# index=$(($index + 1))
#done
#echo $index

# shield all CPUs in CPULIST
# shield -a CPULIST
#This shield cpu 0 from irq and local timer and proccess
#shield -a 0
# Runs mycommand on CPU 0 which previously shielded from processes
#run -b 0 ./mycommand
