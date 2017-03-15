## NHS Digital Technical Assessment Analysis

## Postcode Validation
The supplied regex does not validate all of the test case postcodes, 6 test cases fail when attempting to use this regex. This seems to be due to missing
caret's within the regex. After some initial experimentation with an online regex validation tool the regex was updated to correctly validate the test case
postcodes in line with the assessment criteria and expected outcomes.

As per the wiki entry: https://en.wikipedia.org/wiki/Postcodes_in_the_United_Kingdom#Validation

The supplied regex does not account for a number of UK Postcodes such as overseas, British forces and special postcodes. It would be recommended
that these edge cases are validated using dedicated regex definitions. A more robust solution would be to leverage one of the postcode validation
services such as: 

http://postcodes.io/
http://www.royalmail.com/business/services/marketing/data-optimisation/paf

Or to leverage a 3rd party solution that provides comprehensive validation.

## Performance Optimisations and Future improvements
The source code to process, validate and provide sorted output files for both successful and failed validation attempts uses parallel processing
to validate and write the outcomes rather than synchronously processing the postcode process as per task 2. This optimisation 
improves performance slightly.

Using a stopwatch object to measure and log the total time taken to process the data file, marginal gains were seen by making use of the frameworks parallel functionality, where
task 2's end to end processing time was ~6 seconds, a synchronous version of Task 3 took ~8 seconds with a parallel process version of Task 3 milleseconds
faster.

A 2 second performance gain was seen when compiling the regex, this was the simplest improvement made to the code base in order to improve end to end performance.

Further methods to optimise this code would be to leverage the more performant language implementations where handling and enumerating lists / collections, replacing the LINQ
implementation with standard For loops and making use of the frameworks concurrent features. Also lowering the number of times the list of postcodes is iterated would also see performance gains.

The ideal solution would be to read the data into memory and then validate, split, sort and write the data in as few enumerations as possible.
