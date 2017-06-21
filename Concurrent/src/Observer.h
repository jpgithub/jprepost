/*
 * Observer.h
 *
 *  Created on: Jun 17, 2017
 *      Author: master
 */

#ifndef OBSERVER_H_
#define OBSERVER_H_

class Observer {
protected:
	Observer();
public:
	virtual ~Observer();
	virtual void Update() = 0;
};

#endif /* OBSERVER_H_ */
