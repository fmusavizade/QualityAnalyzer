# QualityAnalyzer
This is a WPF application which is created with net framework 4.7, and the class libraries with .net standard 2.
It’s a simple application that contains 4 steps leading to processing an input file and analyze its json records. There is a small sample input file with the project that you can test the process with it, but this application has been tested with a file containing 700,000 records and passed the test.


Variance formula:
I have used this formula to calculate variance:
 
 ![image](https://user-images.githubusercontent.com/115587214/201155272-5e19d8d5-301c-4b88-9ebb-d4d0a03b4eb3.png)

The logic of outliers detection:
A standard way of checking for outliers is to use quartiles and the interquartile range for defining thresholds. Take the median to divide your data in two halves, then use the medians of the upper and lower halves to divide your data into four parts. The resulting 3 numbers are your quartiles.

Sample input : [15, 17, 19, 16, 14, 58]

In sample case you get
Q1 = 15
Q2 = 16.5
Q3 = 19
The interquartile range is IQR = Q3 − Q1 = 4.
Common thresholds to detect outliers would be
Tlo= Q1 − 1.5 ⋅IQR = 9
 And
 Thi= Q3 + 1.5 ⋅ IQR = 25
Any values less than Tlo or greater than Thi are classified as outliers.
 So the value 58 would clearly be detected as an outlier.

Logging
I have used Nlog package for logging messages. If any exceptions throws the log of the exception will be found  in :  D:/Logs/${shortdate}/${date:format=HH}.txt or {basedir}/Logs/${shortdate}/${date:format=HH}.txt addresses
