import matplotlib.pyplot as plt
from sklearn import datasets
from sklearn.model_selection import train_test_split
import numpy as np

# Nearest neighbor algorithm using euclidean distance
class NearestNeighbor(object):
    def __init__(self, X_train, y_train):
        self.X_train = X_train
        self.y_train = y_train

    def query(self, x):
        distances = np.zeros((self.X_train.shape[0]), dtype=np.float64)

        # calculate euclidean distance - is equivalent to p = 2 for Minkowski distance
        # https://en.wikipedia.org/wiki/Minkowski_distance
        product = np.power(np.subtract(x, self.X_train), 2)

        for i, row in enumerate(product):
            distances[i] = np.sqrt(np.sum(row))

            # Index to closest neighbour
            # return the indices of the minimum values in distance array
        min_index = np.argmin(distances)

        # Return predicted class
        return self.y_train[min_index]


digits = datasets.load_digits()

_, axes = plt.subplots(nrows=1, ncols=4, figsize=(10, 3))
for ax, image, label in zip(axes, digits.images, digits.target):
    ax.set_axis_off()
    ax.imshow(image, cmap=plt.cm.gray_r, interpolation='nearest')
    ax.set_title('Training: %i' % label)

n_samples = len(digits.images)
print(n_samples)
data = digits.images.reshape((n_samples, -1))

# Split data into 50% train and 50% test subsets
X_train, X_test, y_train, y_test = train_test_split(
    data, digits.target, test_size=0.5, shuffle=False)

# There are 898 image in X_train set and 898 Labels in y_train set
# Each image data correspond to a label e.g y_train[0]'s label value is for X_train[0]'s image
nn = NearestNeighbor(X_train,y_train)

# store the machine best guess of the corresponding label for a given image in X_test
results = []

for i, example in enumerate(X_test):
    results.append(nn.query(example))
