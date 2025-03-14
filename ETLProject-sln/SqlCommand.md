** Sql query: **

CREATE TABLE TaxiMetrics (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    TpepPickupDateTime DATETIME NOT NULL,
    TpepDropoffDateTime DATETIME NOT NULL,
    PassengerCount INT NOT NULL,
    TripDistance FLOAT NOT NULL,
    StoreAndFwdFlag NVARCHAR(3) NOT NULL,
    PULocationID INT NOT NULL,
    DOLocationID INT NOT NULL,
    FareAmount DECIMAL(10, 2) NOT NULL,
    TipAmount DECIMAL(10, 2) NOT NULL
);

CREATE NONCLUSTERED INDEX Index_PULocationID_TipAmount
ON TaxiMetrics (PULocationID, TipAmount);

CREATE NONCLUSTERED INDEX Index_TripDistance
ON TaxiMetrics (TripDistance DESC);

CREATE NONCLUSTERED INDEX Index_TripDuration
ON TaxiMetrics (TpepDropoffDateTime, TpepPickupDateTime);

