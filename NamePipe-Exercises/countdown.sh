#!/bin/sh
if [ $# -ne 1 ]; then
echo "usage: $0 MIN (example: $0 1)";
exit 1;
fi
 
TIME=$(($1*60))
CUR=$TIME
 
while [ $CUR -gt 0 ]
do
 CUR=$(($CUR - 1))
 clear
 echo $CUR;
 echo 1000000000 > /tmp/fifo_pipe
 sleep 1
done
 
echo "!!!Go for Launch!!!"
bash launcher.sh .