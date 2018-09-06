# images

A POC project to see how we can run automated tests with Rapid7

This repo contains the services and tests, a sister repo contains the Angular 4+ client.

## requirements

* dotnet 2.1
* sql server
* angular / Npm / Node (see the client repo)

## runnning the test

in order to run the tests using rapid 7, we will compile and run the service and client in seperate processes from the tests.

clone both repo's (this one and the client repo)


1. compile the images application

```
  cd ./src
  dotnet build -c Release
  xcopy "./PetStore.Images/bin/Release/netcoreapp2.0" "./compiled-service/" /E
```

2. make the 'test runner execute-able'

```
  cd ./src/PetStore.Images.Tests.Runner
  dotnet publish -c Release -r win7-x64
  cd ..
  xcopy "./PetStore.Images/bin/Release/netcoreapp2.0" "./compiled-tests/" /E
```

3. setup the database

* in Sql Server create an images database

```
  CREATE DATABASE Images

  USE [Images]
  GO
  
  SET ANSI_NULLS ON
  GO
  
  SET QUOTED_IDENTIFIER ON
  GO
  
  CREATE TABLE [dbo].[Assets](
	  [Id] [varchar](128) NOT NULL,
	  [Content] [varbinary](max) NULL,
	  [Name] [varchar](max) NULL,
   CONSTRAINT [PK_Assets] PRIMARY KEY CLUSTERED 
  (
	  [Id] ASC
  )WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
  ) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
  GO
```


4. set environment varibles and host

`environment`

``` 
  IsCi = true
  driver = chrome
  Port = 5000
  proxy = true
  ConnectionString = connection string to the database which has read/write access to the images database
```

`host | DNS`

```
  images = server which is runnning the PetStore.Images (127.0.0.1)
  sut = server which is runnning the PetStore.Images (127.0.0.1)
  proxy = server which is runnning the attack proxy (127.0.0.1) 
```

5. setup rapid 7 (appspider)

    * Main tab (add urls)

        - http://images:5000
        - http://sut:5000
        - http://sut:4200

    * Selenium Recordings

        - location of the of the 'test runner execute-able' from step 2


6. Running

* in 1 command prompt, move to the `compiled-service` directory

```
  cd <<base-path>>/compiled-service
  dotnet PetStore.Images.dll 
```

* in another command prompt open the client project's folder in the other repo

```
  cd <<base-path>>/client/petstore-client
  npm install
  ng serve --host 0.0.0.0 --disable-host-check --watch
```

* run the the AppSpider application, this will invoke the tests for us, while collecting secuitry risk information.


# important note
the code in this project is POC, it is not production level, and contains known hacking issues.
