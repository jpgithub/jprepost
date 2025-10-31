import cv2
import numpy as np
from scipy.ndimage import laplace, convolve
from typing import Optional

class bitmapfileheader:
    def __init__(self, bytes_array):
        self.header_field = int.from_bytes(bytes_array[0:2], byteorder='big')
        self.size_bmp_file_bytes = int.from_bytes(bytes_array[2:6], byteorder='little')
        self.reserved = int.from_bytes(bytes_array[6:10], byteorder='big')
        self.starting_address_bitmap_image_data = int.from_bytes(bytes_array[10:], byteorder='little')

    def get_header_field(self):
        return self.header_field

    def get_size_bmp_file(self):
        return self.size_bmp_file_bytes

"""
File Header

424d - 2 bytes BMP and DIB file.

36840300 - 4 bytes size of the bmp file in bytes, Correct for endianness 00038436

0000 - Reserved 2 bytes

0000 - Reserved 2 bytes

36000000 -> 4 bytes --> 0000 0036 starting address of the byte where bitmap image data pixel array

DIB Header (bitmap information header)

28000000 -> 4 bytes ( size of the header in bytes)

40010000 -> 00000140 -> bitmap width 
f0000000 -> 000000f0 -> 240 - bitmap height

0100 --> 0001 -> color Planes
1800 --> 0018 -> 24 bit per pixel

00000000 --> compression method used
00840300 --> image size (the size of the raw bitmap data)
c40e0000 --> pixel per meter horizontal
c40e0000 --> pixel per meter vertical
00000000 --> number of colors in the color palette
00000000 --> number of important colors used
"""

class bitmapinformationheader:
    def __init__(self, bytes_array):
        self.header_size = int.from_bytes(bytes_array[:4], byteorder='little')
        self.bitmap_width = int.from_bytes(bytes_array[4:8], byteorder='little')
        self.bitmap_height = int.from_bytes(bytes_array[8:12], byteorder='little')


def read_bmp(name)->Optional[tuple]:
    # Open the BMP file in binary mode
    with open(name, 'rb') as f:
        # Skip the BMP header (typically 54 bytes for standard BMP files)
        header = f.read(54)
        bfh = bitmapfileheader(header[:14])
        bmpfile_size_in_bytes = bfh.get_size_bmp_file()
        print(bmpfile_size_in_bytes)
        dip_header = bitmapinformationheader(header[14:])
        print(dip_header.bitmap_width)
        print(dip_header.bitmap_height)


        # Read the raw pixel data
        pixel_data = np.frombuffer(f.read(), dtype=np.uint8)

    if pixel_data is None:
        return None
    # Assuming the BMP is 24-bit (RGB) and you know its dimensions (e.g., width and height)
    width, height = dip_header.bitmap_width, dip_header.bitmap_height
    image = pixel_data.reshape((height, width, 3))  # Reshape to (height, width, 3) for RGB

    # Now `image` is a NumPy array representing the BMP image
    print(image.shape)
    return header, image

def get_grayscale(image_array):
    # Extract RGB channels
    r, g, b = image_array[:, :, 2], image_array[:, :, 1], image_array[:, :, 0]
    # Calculate using luminosity method
    grayscale_array = 0.299 * r + 0.587 * g + 0.114 * b
    # Example: Average luminosity
    #average_luminosity = np.mean(luminosity)
    #print("Average Luminosity:", average_luminosity)
    return grayscale_array

def get_variance(image_array):
    print(image_array.var())
    var = np.mean(np.abs(image_array-image_array.mean())**2)
    print(var)

def auto_adjust_brightness(image):
    # Convert image to grayscale for histogram analysis
    gray = cv2.cvtColor(image, cv2.COLOR_BGR2GRAY)
    # calculate histogram
    hist = cv2.calcHist([gray],[0], None, [256], [0,256])
    # Find the mean intensity
    mean_intensity = np.sum(np.arange(256) * hist.flatten())/np.sum(hist)
    # Determine a gamma correction factor based on the mean intensity
    gamma = 1.0 + (128 - mean_intensity) / 128.0
    # Apply gamma correction
    gamma_table = np.array([((i/255.0) ** gamma) * 256 for i in np.arange(0, 256)]).astype("uint8")
    adjusted_image = cv2.LUT(image, gamma_table)
    return adjusted_image

def focus_score_laplacian(image):
    gray = cv2.cvtColor(image, cv2.COLOR_BGR2GRAY)
    laplacian = cv2.Laplacian(gray, cv2.CV_64F)
    return laplacian.var()


def run_it(name):
    # Use a breakpoint in the code line below to debug your script.
    print(f'Run, {name}')  # Press Ctrl+F8 to toggle the breakpoint.
    header, image_array = read_bmp("test_image_bmp.bmp")
    # convert back to image type
    grayscale = get_grayscale(image_array)
    laplacian = laplace(grayscale)
    #print("\nLaplacian-np-var:")
    #print(np.var(laplacian))
    # numpy laplacian not working after apply two gradient opted to use defined kernel and using scipy 2d convolve

    # Laplacian kernel
    laplacian_kernel = np.array([[0, 1, 0],
                                 [1, -4, 1],
                                 [0, 1, 0]])

    # Apply the Laplacian filter
    laplacian_image = convolve(grayscale, laplacian_kernel)

    print("\nLaplacian of the Image:")
    print(np.var(laplacian_image))


    # Example usage
#    image = cv2.imread("test_image_bmp.bmp")
#    gray = cv2.cvtColor(image, cv2.COLOR_BGR2GRAY)
#    score = focus_score_laplacian(image)
#    print("Focus Score (Laplacian):", score)
    #print(gray[0])
    #print(grayscale[239])
    #cv2.imshow("Original", image)
    #cv2.imshow("GrayScale", gray)
    #cv2.waitKey(0)
    #cv2.destroyAllWindows()

    # Load and image
    # image = cv2.imread("test_image.png")
    # adjusted_image = auto_adjust_brightness(image)
    # cv2.imshow('adjusted image', adjusted_image)
    # cv2.waitKey(0)
    # cv2.destroyAllWindows()




# Press the green button in the gutter to run the script.
if __name__ == '__main__':
    run_it('read bmp')

# See PyCharm help at https://www.jetbrains.com/help/pycharm/
