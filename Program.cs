using Dapper;
using Npgsql;
using DapperWithPostgresSQL;

string connectionString = "Server=tiny.db.elephantsql.com ;Port=5432;Database=znshpmlq;UserId=znshpmlq;Password=EMEIxMo2NpTDcz4rsKbgvUn2hyNqRJWi";
var connection = new NpgsqlConnection(connectionString);
connection.Open();


string CREATE_CUSTOMERS_TABLE = @"create table if not exists customers(
  ID serial NOT NULL,
  FirstName varchar(50) NOT NULL,
  City varchar(50) NOT NULL,
  Age int CHECK(Age>18),
  PRIMARY KEY(ID)
);";

connection.Execute(CREATE_CUSTOMERS_TABLE);

connection.Execute(@"
insert into 
customers (FirstName, City, Age)
values 
(@FirstName, @City, @Age);",
new object[]{
new Customers()
{
  FirstName = "Rethabile",
  City = "Bloemfontein",
  Age = 19
},
new Customers()
{
  FirstName = "Phumza",
  City = "Cape Town",
  Age = 23
},
new Customers()
{
  FirstName = "Sinelizwi",
  City = "George",
  Age = 32
},
new Customers()
{
  FirstName = "Lona",
  City = "East London",
  Age = 27
},
new Customers()
{
  FirstName = "Asive",
  City = "Gqeberha",
  Age = 30
},
new Customers()
{
  FirstName = "Anela",
  City = "Durban",
  Age = 20
},
});

Console.WriteLine("*****************");

var customers = connection.Query<Customers>(@"select * from customers");
foreach (var names in customers)
{
  Console.WriteLine(names.FirstName);
}
Console.WriteLine(customers.Count());
