## API Aggregation Service (.NET 8)

This project is an **API Aggregation Service** built with **ASP.NET Core (.NET 8)**.  
It fetches and combines data from multiple external APIs (comments, recipes, weather), stores it in a local database, caches the results, and exposes a single unified API endpoint to access the aggregated data 
and another endpoint that previews the statistics of the external APIs.


## 🚀 How to Run

You just need to run the API, and you will be redirected directly to http://localhost:51655/swagger/index.html

You can test the API directly through Swagger UI, or
Use Postman and send requests to the provided endpoints.

The main implementation is in the master branch.


## JWT
The implementation of the JWT exists in a separate branch feature/jwt
