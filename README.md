# Vehicle Tracker API

The solutions need to be able to track vehicles position using GPS navigation. A device emboarded in a vehicle,
will communicate with your API to register the vehicle and update its position.

This Solution targets the ASP.NET Core 5.0.

## Setup

Clone the Project
```
$ git clone etc
```
Navigate to the project directory
```
$ cd src/VehicleTracker.API
```
Restore all dependencies using the command below

```
$ dotnet restore
```
Build the Project using the command below

```
$ dotnet build
```


## Database Setup
**Note:** Before running the Project, make sure you have MongoDB Setup or installed in your computer. 

I'm using MongoDB, running as a docker container. 
![Alt text](https://github.com/Chibuikekenneth/VehicleTracker.API/blob/main/Images/trackerAPI.PNG?raw=true "Title")

I'm also using Robo 3T as my Database GUI.
![Alt text](Images/trackerDB.png?raw=true "Title")

**Note:** If you're using docker, be sure to map the Port with same as the one in the connection string


## Running the Project
Once the database is up and connected successfully, Just go ahead and run the application.
```
dotnet run
```
 Navigate to the swagger generated API documentation
 ![Alt text](Images/trackerAPI.png?raw=true "Title")
