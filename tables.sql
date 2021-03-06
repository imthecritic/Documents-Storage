use DocumentApp

IF OBJECT_ID ('dbo.usersfiles', 'U') IS NOT NULL  
   DROP TABLE usersfiles;

IF OBJECT_ID ('dbo.users', 'U') IS NOT NULL  
   DROP TABLE users;  

IF OBJECT_ID ('dbo.files', 'U') IS NOT NULL  
   DROP TABLE files;

GO  
CREATE TABLE users  
(  
    UserID int IDENTITY(1,1) PRIMARY KEY NOT NULL,
    PasswordHash NVARCHAR(MAX) NOT NULL,
    FirstName NVARCHAR(40) NOT NULL,
    LastName NVARCHAR(40) NOT NULL,
	Email NVARCHAR(50) NOT NULL
 
);  

CREATE TABLE files
(
    FileID INT IDENTITY(1,1) PRIMARY KEY NOT NULL,
	FileName NVARCHAR(40) NOT NULL,
	FilePath NVARCHAR(255) NOT NULL,
	Downloads INT NOT NULL,
	Created DATETIME NOT NULL,
	Active BIT NOT NULL
	
);

CREATE TABLE usersfiles
(
	RowID INT IDENTITY(1,1) NOT NULL,
	UserID INT FOREIGN KEY REFERENCES users(UserID) NOT NULL,
	FileID INT FOREIGN KEY REFERENCES files(FileID) NOT NULL

);