/*
 * IObservee.cpp
 *
 *  Created on: Jun 17, 2017
 *      Author: master
 */

#include "IObservee.h"

IObservee::IObservee() {

}

IObservee::~IObservee() {

}

// Provide default implementation
void IObservee::Notify(){
	IObservee::Observers::iterator it = this->viewerlist.begin();
	while(it != this->viewerlist.end()){
		(*it)->Update();
		it++;
	}
}

void IObservee::Attach(class Observer *observer) {
	this->viewerlist.push_back(observer);
}

