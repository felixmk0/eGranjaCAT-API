# NastrafarmSIGE API Project Architecture

Currently, the project is designed with a single-tenant per database approach, meaning each client has their own deployed instance with a dedicated database. This ensures complete data isolation and simplifies security and customization for each client.


### Technologies Used:

- Backend built with .NET 9
- SQL Server as the database
- Authentication using JWT (JSON Web Tokens)
- Authorization based on roles and policies that control access to different endpoints according to user permissions
- Background jobs via Cron Expressions
- API documentation with Swagger
