# WeatherGraph
This repository contains a description and implementation of the programming task used to evaluate software developers.

## Task

Must use:
* ASP.NET CORE MVC
* C#
* Typescript
* React
* SQL.
  
Achieve:
* Using any public weather API receive data (country, city, temperature) from 2 cities in 2-3
countries - with periodical update 1/min.
* Store this data in the database and show in graphs: Min and Max temperature
(Country\City\Temperature\Last update time).

## Running project locally

This project uses SQLite as database engine.

Run backend locally:

```
dotnet run
```


Run frontend client locally:

```
npm install
npm run dev
```


Frontend accessible at

```
http://localhost:5000/
```


Public endpoint for weather data accessible at

```
http://localhost:5000/weather/GetWeatherData
```


![image](https://github.com/user-attachments/assets/fd738b2f-d3ad-44f1-973f-bd590c436ae6)
