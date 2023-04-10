# Synectics Sales Analytics

The console application calculates statistics based on files containing millions of sales records.
The format of the records is
```
31/03/2021##245.39
```
## Supported statistics

* Average of earnings for a range of years.
* Standard deviation of earnings within a specific year.
* Standard deviation of earnings for a range of years

![01](https://user-images.githubusercontent.com/5530344/230882320-9d83d2ba-aed0-40d9-878a-c06d5daff0a0.png)
![02](https://user-images.githubusercontent.com/5530344/230882316-fbba658a-fba6-407a-8f3a-c5e501c7afe8.png)
![03](https://user-images.githubusercontent.com/5530344/230882313-35447238-57ed-4265-a2e5-f557a0c5aefc.png)
## Other Features:

* [Spectre.Console](https://spectreconsole.net/) based command line interface for enhanced user experience
* The record format can be changed in the application settings (both for analytics and for the generator)
* Weak cohesion
* Unit tests
* Stab File Generator Tool for generating test data.

## Assumptions

* The resources of the user's machine can process entries from all the files in memory
* The number of files to be processed does not require optimization

## Setting up

The format of the date, amount, delimiter and path can be configurable in appsettings.json file.

```
    "dateFormat": "dd/MM/yyyy",
    "decimalSymbol": ".",
    "delimiter": "##",
    "path":  ""
```
By default path is `../%user%/Documents` looking for files with .dat extension.

## Getting Started

Build and run Stab Files Generator Tool to get test data if requred.

Build and Run Analytics.
