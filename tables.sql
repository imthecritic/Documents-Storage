use DocumentApp

IF OBJECT_ID ('dbo.users', 'U') IS NOT NULL  
   DROP TABLE users;  
GO  
CREATE TABLE users  
(  
    UserID INT IDENTITY(1,1) NOT NULL,
    UserName NVARCHAR(40) NOT NULL,
    PasswordHash BINARY(64) NOT NULL,
    FirstName NVARCHAR(40) NOT NULL,
    LastName NVARCHAR(40) NOT NULL,
	Email NVARCHAR(50) NOT NULL
 
);  