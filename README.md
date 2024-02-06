This is a web API built using ASP.NET Core. It makes use of JWT tokens to store claims for user and admin authentication. It uses Dapper to interact with a T-SQL relational database.

In this application:

A logged-in admin can create customers and bank accounts for the customers. The admin can also give out loans to customers.

A user can transfer money between bank accounts, get an overview of their bank accounts, view transfer history on one of their bank accounts, and create an additional bank account.

There are also unit tests for some of the functions in the service layer. They are made with xUnit and Moq.
