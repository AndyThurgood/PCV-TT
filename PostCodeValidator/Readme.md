## NHS Digital Technical Assessment

## Prequisites:

* Visual Studio 2015 Update 3;
* Import_data.csv file or internet connection.

## Task 1
To run the task 1 unit tests:
* Compile the solution within Visual Studio;
* Open Test Explorer;
* Run all unit tests, all tests should succeed.

To run the task 1 integration tests:
* Compile the solution within Visual Studio;
* Open Test Explorer;
* Run all integration tests, all tests should succeed.

## Task 2
To run the task 2 execution and produce output:
* Compile / build the solution within Visual Studio;
* Set the PostCodeValidator Console application as the start-up project  (right click, set as start up);
* (Optional) Copy the import_data.csv file into the folder "PostCodeData", this folder is found in the root of the solution folder structure 
  (e.g. C:\Development\Postcode Validator\PostCodeData\), the solution will attempt to download the import file if it is not present in the date directory;
* The file location and uri location can be configured in the applicaiton.config file.
* Start the application from within Visual Studio;
* When prompted in the console, input 2 and then press enter;
* The application will process the postcode data and write the failed_validation.csv file to the PostCodeData folder.

## Task 3
To run the task 3 execution and produce output:
* Compile / build the solution within Visual Studio;
* Set the PostCodeValidator Console application as the start-up project  (right click, set as start up);
* (Optional) Copy the import_data.csv file into the folder "PostCodeData", this folder is found in the root of the solution folder structure 
  (e.g. C:\Development\Postcode Validator\PostCodeData\), the solution will attempt to download the import file if it is not present in the date directory;
  * The file location and uri location can be configured in the applicaiton.config file.
* Start the application from within Visual Studio;
* When prompted in the console, input 3 and then press enter;
* The application will process the postcode data and write the failed_validation.csv file to the PostCodeData folder.
* The application will process the postcode data and write the succeeded_validation.csv file to the PostCodeData folder.

