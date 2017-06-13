/*
 * ITask.h
 *
 *  Created on: Jun 11, 2017
 *      Author: master
 */

#ifndef ITASK_H_
#define ITASK_H_

class ITask {
public:
	ITask();
	virtual ~ITask();
	virtual int Create();
	virtual int Start();
	virtual void Run();
	virtual int Stop();
};

#endif /* ITASK_H_ */
