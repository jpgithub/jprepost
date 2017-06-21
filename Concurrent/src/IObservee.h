/*
 * IObservee.h
 *
 *  Created on: Jun 17, 2017
 *      Author: master
 */

#ifndef IOBSERVEE_H_
#define IOBSERVEE_H_

#include <vector>
#include "Observer.h"

class IObservee {
protected:
	std::vector <class Observer *> viewerlist;
	IObservee();
public:
	typedef std::vector <class Observer *> Observers;
	virtual ~IObservee();
	virtual void Notify() = 0;
	virtual void Attach(class Observer *observer) = 0;
};

#endif /* IOBSERVEE_H_ */
