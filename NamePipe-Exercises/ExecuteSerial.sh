#!/bin/sh
if [ $# -ne 1 ]; then
echo "usage: $0 processname (example: $0 processname)";
exit 1;
fi

echo "Compiling:"
g++ "$1.cpp" -o $1; 

pipe="/tmp/fifo_pipe"
mkfifo $pipe
  
echo "Launch $1"
./$1 0 $pipe &

ID="$(pidof $1)"

#CPU Affinity Associated with Process
#sed command (Stream Editor)
CPULIST0="$(taskset -p -c $ID | sed 's/.*list: \([0-9]*\).*/\1/')";

echo $CPULIST0

index=$CPULIST0

# This will autoselect a cpu index that is not on CPULIST
if [ $index -gt $CPULIST0 ] # && [ $index != $CPULIST1 ]
then
	index=$index
else
	index=$(($index + 1))
fi
echo $index


#until [$index -gt $CPULIST0] && [ $index -gt $CPULIST1 ]
#do
# index=$(($index + 1))
#done
#echo $index