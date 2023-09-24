create database CRUDWEBAPI;

use CRUDWEBAPI;
use inframart;

CREATE TABLE Employee (
    EmpId int PRIMARY KEY,
    EmpName NVARCHAR(50),
    EmpEmail NVARCHAR(50),
    EmpPassword NVARCHAR(50),
    EmpAddress NVARCHAR(50)
);


INSERT INTO Employee (EmpId, EmpName, EmpEmail, EmpPassword, EmpAddress)
VALUES
    (1, 'Akshay Shinde', 'akshay@gmail.com', 'Akshay@123', 'Pune'),
    (2, 'Ajay Shinde', 'ajay@gmail.com', 'Ajay@123', 'Solapur');
    


select * From Employee;


drop table employee;
DELETE FROM Employee WHERE EmpId=77;


CREATE PROCEDURE GetAllEmp
AS
BEGIN
    SELECT [EmpId], [EmpName], [EmpEmail], [EmpAddress],[EmpPassword]
    FROM [dbo].[Employee];
END;


CREATE PROCEDURE InsertEmp
    @EmpId NVARCHAR(50),
    @EmpName NVARCHAR(100),
    @EmpEmail NVARCHAR(100),
    @EmpPassword NVARCHAR(100),
    @EmpAddress NVARCHAR(200)
AS
BEGIN
    INSERT INTO Employee (EmpId, EmpName, EmpEmail, EmpPassword, EmpAddress)
    VALUES (@EmpId, @EmpName, @EmpEmail, @EmpPassword, @EmpAddress)
END




CREATE PROCEDURE [dbo].[UpdateEmp]
    @EmpId INT,
    @EmpName VARCHAR(250),
    @EmpEmail VARCHAR(250),
    @EmpPassword VARCHAR(250),
    @EmpAddress VARCHAR(250)
AS
BEGIN
    UPDATE [Employee]
    SET
        EmpName = @EmpName,
        EmpEmail = @EmpEmail,
        [EmpPassword] = @EmpPassword,
        [EmpAddress] = @EmpAddress
    WHERE
        EmpId = @EmpId;
END;




create PROCEDURE [dbo].[DeleteEmp]
    @EmpId INT
   
As
BEGIN
    DELETE FROM [Employee] WHERE EmpId = @EmpId
END;


Create PROCEDURE [dbo].Logins
     
    @UserName VARCHAR(250),
   
    @Password VARCHAR(250)
    
AS
BEGIN
    INSERT INTO [Login] ( UserName, Passwords )
    VALUES ( @UserName, @Password);
END;






