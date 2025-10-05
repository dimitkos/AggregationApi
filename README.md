
![Build](https://github.com/dimitkos/AggregationApi/actions/workflows/dotnet.yml/badge.svg)
![.NET](https://img.shields.io/badge/.NET-8.0-blueviolet)
![Lint](https://github.com/dimitkos/AggregationApi/actions/workflows/codequality.yml/badge.svg)

## API Aggregation Service (.NET 8)

This project is an **API Aggregation Service** built with **ASP.NET Core (.NET 8)**.  
It fetches and combines data from multiple external APIs (comments, recipes, weather), stores it in a local database, caches the results, and exposes a single unified API endpoint to access the aggregated data 
and another endpoint that previews the statistics of the external APIs.

There is also a background job that detects anomalies in the external api calls response time, comparing with the average time

In the branch feature/jwt, there are some extra functionalities
- Register a user
- Login
- If you are an authenticated user, you can use the endpoints

## 🚀 How to Run

## Master branch 

Here is the whole functionality, except JWT
You just need to run the API, and you will be redirected directly to http://localhost:51655/swagger/index.html

You can test the API directly through Swagger UI, or
Use Postman and send requests to the provided endpoints.


## feature/jwt branch

This branch is based on master, but additionally has the JWT implementation.

To run this, has some additional steps
When you redirected to http://localhost:51655/swagger/index.html

1. If you don't have a user, register a new one, adding a valid email and a password
<img width="1466" height="597" alt="image" src="https://github.com/user-attachments/assets/968f0c21-e23d-412e-8bda-ca562c8bbe1f" />

2. Then try to login 
<img width="1443" height="581" alt="image" src="https://github.com/user-attachments/assets/e1be1973-8ca4-4454-8edc-fd5ddb0068d0" />

3. When you login successfully 
<img width="1560" height="540" alt="image" src="https://github.com/user-attachments/assets/103e8d39-4552-402a-ae7a-7ba1750969db" />

4. You are ready to use the API

