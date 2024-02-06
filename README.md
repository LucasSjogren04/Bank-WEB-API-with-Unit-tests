This is a web api build using ASP.net core. It makes use of JWT token to store claims for user and admin authentication. It uses Dapper to interact with a t-sql relational database.

In this application:
A logged in admin can create customers and bank accounts for the customers. The admin can also give out loans to customers.

A User can transfer money between bank accounts, get an overview of their bank accounts, view transfer history on one of their bank account and create an additional bank account. 

There are also unit tests for some of the functions in the service layer. They are made with Xunit and Moq.
