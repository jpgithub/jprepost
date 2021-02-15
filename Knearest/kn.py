# https://www.lfd.uci.edu/~gohlke/pythonlibs/#opencv
import cv2 as cv
import math
import numpy as np
import matplotlib.pyplot as plt

A = [5.5, 2.4, 3.8, 1.1]
B = [6.7, 2.5, 5.8, 1.8]
traindata = np.random.randint(0,100,(25,2)).astype(np.float32)
responses = np.random.randint(0,2,(25,1)).astype(np.float32)

def L1Distance(array_a,array_b):
    total = 0
    if len(array_a) != len(array_b):
        return -1
    else:
        for i in range(len(array_a)):
            total += abs(array_a[i] - array_b[i])
    return total


def L2Distance(array_a,array_b):
    total = 0
    if len(array_a) != len(array_b):
        return -1
    else:
        for i in range(len(array_a)):
            total += math.pow((array_a[i] - array_b[i]), 2)
    return math.sqrt(total)

#print(L1Distance(A, B))

#print(L2Distance(A, B))

# http://www.math.buffalo.edu/~badzioch/MTH337/PT/PT-boolean_numpy_arrays/PT-boolean_numpy_arrays.html
# select all elements of the train data in which responses flatten array equal 0
red = traindata[responses.ravel() == 0]
plt.scatter(red[:,0],red[:,1], 80,'r', '^')
# s# select all elements of the train data in which responses flatten array equal 1
blue = traindata[responses.ravel() == 1]
plt.scatter(blue[:,0],blue[:,1],80, 'b', 's')

knn = cv.ml.KNearest_create()
knn.train(traindata, cv.ml.ROW_SAMPLE, responses)
ktype = knn.getAlgorithmType()
plt.show()