# Vehicle Tracker API

The solutions need to be able to track vehicles position using GPS navigation. A device emboarded in a vehicle,
will communicate with your API to register the vehicle and update its position.

This Solution targets the ASP.NET Core 5.0.

## Setup

Clone the Project
```
$ git clone https://github.com/Chibuikekenneth/VehicleTracker.API.git
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
![Alt text](https://github.com/Chibuikekenneth/VehicleTracker.API/blob/main/Images/trackerDocker.PNG?raw=true "Title")

I'm also using Robo 3T as my Database GUI.
![Alt text](https://github.com/Chibuikekenneth/VehicleTracker.API/blob/main/Images/trackerDB.PNG?raw=true "Title")

**Note:** If you're using docker, be sure to map the Port with same as the one in the connection string


## Running the Project
Once the database is up and connected successfully, Just go ahead and run the application. (If you're using Visual studio, just click on the play Button)
```
dotnet run
```
 Navigate to the swagger generated API documentation `localhost:5001/swagger/index.html`
 ![Alt text](https://github.com/Chibuikekenneth/VehicleTracker.API/blob/main/Images/trackerAPI.PNG?raw=true "Title")
...



## Here are some Thought processes and Notes I Jotted when writing this API

#### Database / scalability
I started with using both Entity framework core and dapper (SQL Server Database), (basically using EF core for migration and Dapper for fast/raw queries respectively) => I ended up using Mongo db (NOSQL) and mongo Driver for C#. Reason beign that I was considering scalability and speed as well. Considering we are having so many writes in the database. Although they all have their tradeOffs

* NOSQL Databases like Mongo DB are very easy to scale, especially horizontally
* Enables faster access of the data due to its nature of using internal memory for storage.
* MongoDB sacrifices structure for greater speed

#### Data Extensibility
Also, condering the fact that you can easily add more properties to either the vehicle(Like fuel, Speed etc), devise or location object easily, without the need for migarations, or manually creating a new column In the database. These are some of the few benefits and reasons I used MongoDB

**Note** Note: one important factor I considered before using mongo DB is Data Model Extensibility, where you can easily add any object to your data model, without the need for migration or table creation, and still retrieve your values. 

#### Security and Exceptions
As Stated in the requirement, there are checks which ensure that a devise does not update the location/position of another vehicle.
Further more, I'm using a global exception handler, which provides detailed/formatted exceptions and error messages etc.