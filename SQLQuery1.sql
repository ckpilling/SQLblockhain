DROP SCHEMA IF EXISTS wesdog;
GO

CREATE SCHEMA wesdog;
GO
CREATE TABLE customers (
id INT NOT NULL PRIMARY KEY IDENTITY,
name VARCHAR(100) NOT NULL,
email VARCHAR (150) NOT NULL UNIQUE,
phone VARCHAR (20) NULL,
address VARCHAR(100) NULL,
created_at DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP
)

 WITH (
 SYSTEM_VERSIONING = ON (HISTORY_TABLE = [wesdog].[customerHistory]),
 LEDGER = ON
);


INSERT INTO clients (name, email, phone, address)
VALUES
('bob', 'bob@myemail.com', '0161 789 4652', 'Manchester'),
('alice', 'alice@mymail.net', '01942 616263', 'Leigh')



CREATE TABLE clients
   (
      [VehicleID] INT NOT NULL,
      [Serialnumber] NVARCHAR (1024) NULL,
	  [GAN] NVARCHAR (1024) NULL,
      [DateLeftFoggia] Datetime2 NULL
   )
 WITH (
 -- SYSTEM_VERSIONING = ON (HISTORY_TABLE = [Iveco].[VehicleHistory]),
 -- LEDGER = ON
	LEDGER = ON (APPEND_ONLY = ON)
 );



 -- --------------------------------------------------------------------------




SELECT
 t.[commit_time] AS [CommitTime] 
 , t.[principal_name] AS [UserName]
 , l.[name]
 , l.[email]
 , l.[phone]
 , l.[address]
 , l.[ledger_operation_type_desc] AS Operation
 FROM [dbo].[customers_Ledger] l
 JOIN sys.database_ledger_transactions t
 ON t.transaction_id = l.ledger_transaction_id
 ORDER BY t.commit_time DESC;


SELECT 
ts.[name] + '.' + t.[name] AS [ledger_table_name]
, hs.[name] + '.' + h.[name] AS [history_table_name]
, vs.[name] + '.' + v.[name] AS [ledger_view_name]
FROM sys.tables AS t
JOIN sys.tables AS h ON (h.[object_id] = t.[history_table_id])
JOIN sys.views v ON (v.[object_id] = t.[ledger_view_id])
JOIN sys.schemas ts ON (ts.[schema_id] = t.[schema_id])
JOIN sys.schemas hs ON (hs.[schema_id] = h.[schema_id])
JOIN sys.schemas vs ON (vs.[schema_id] = v.[schema_id])
WHERE t.[name] = 'customers';



SELECT [id]
   ,[name]
   ,[email]
   ,[phone]
   ,[ledger_start_transaction_id]
   ,[ledger_end_transaction_id]
   ,[ledger_start_sequence_number]
   ,[ledger_end_sequence_number]
 FROM [dbo].[customers];  




 EXECUTE usp_listHistory
