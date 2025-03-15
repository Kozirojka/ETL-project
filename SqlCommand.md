
# SQL Table and Query Optimization

## Create Table

**SQL Query:**
```sql
CREATE TABLE Trips (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    PickupDatetime DATETIME NOT NULL,       
    DropoffDatetime DATETIME NOT NULL,       
    PassengerCount INT NOT NULL,             
    TripDistance DECIMAL(10,2) NOT NULL,     
    StoreAndFwdFlag VARCHAR(3) NOT NULL,     
    PULocationID INT NOT NULL,               
    DOLocationID INT NOT NULL,               
    FareAmount DECIMAL(10,2) NOT NULL,       
    TipAmount DECIMAL(10,2) NOT NULL         
);
```
## Query Optimization

### 1. Find the top 100 longest fares in terms of `TripDistance`.

In this situation, the best solution is creating an index on `TripDistance`.

```sql
CREATE NONCLUSTERED INDEX idx_TripDistance ON Trips(TripDistance);

------

SELECT TOP 100 
FROM Trips
ORDER BY TripDistance DESC;
```

### 2. Find the top 100 longest fares in terms of time spent traveling.

We should create a complex index to work more quickly with time:

```sql
CREATE NONCLUSTERED INDEX idx_trip_time ON Trips(tpep_pickup_datetime, tpep_dropoff_datetime);
```
But I'm not sure.

### 3. Search, where part of the conditions is `PULocationID`.

We should create an index on `PULocationID`.

```sql
CREATE INDEX idx_pulocationid ON Trips (PULocationID);
```

### 4. Find which PULocationID has the highest average `TipAmount`

```sql
SELECT TOP 1 PULocationID, AVG(TipAmount) 
FROM TaxiTrips
GROUP BY PULocationID 
ORDER BY AVG(TipAmount) DESC;
```
PULocationID groups all trips by their pick up location, and AVG(TipAmount) calculates avarage tips for each zone.

