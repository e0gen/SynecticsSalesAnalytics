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
