use DbEmployee
create table Employees(
Id int primary key identity,
Name varchar(30) not null,
Age int not null)

GO -- select all emmployees
create procedure proc_SelectAllEmployee 
as select * from Employees

go --select employee by id
create procedure proc_SelectEmployeeById(
@Id int)
as select * from Employees where Id = @Id

Go --insert procedure with 3 parameters
create procedure proc_InsertEmployee(
@Name varchar(30),
@Age int)
as insert into Employees values(@Name,@Age)

go -- update employee
create procedure proc_UpdateEmploee(
@Id int,
@Name varchar(30),
@Age int)
as update Employees set Name=@Name, Age=@Age where Id = @Id

go -- delete employee
create procedure proc_deleteEmployee(@Id int)
as delete from Employees where Id = @Id





