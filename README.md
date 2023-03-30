# Lagalt-Backend
Final case assignment for the noroff accelerate .NET course (backenside)

Lagalt.no Backend is a sophisticated ASP.NET 6 Web API, developed using C# and serving as the foundation for the Lagalt.no 
application. The API seamlessly integrates with Keycloak for user authentication, hosted on Cloud-IAM, and leverages Swagger 
for comprehensive documentation of all endpoints. A code-first relational database powered by SQL Express Server is utilized 
for efficient data management. The project adheres to the repository pattern, incorporating controllers, services with interfaces, 
and custom mock data to facilitate testing of all API endpoints. Query parameters are available for select GET endpoints.

## Contents:

Overview
Prerequisites
Getting Started
Usage Instructions
Database Diagram
License Information

## Overview:
The Lagalt.no Backend offers an array of essential features, including:
- Keycloak integration for robust user authentication
- Swagger documentation for seamless API endpoint exploration
- Code-first relational database with SQL Express Server
- Repository pattern implementation with controllers and services
- Custom mock data for thorough testing
- Query parameter support for select GET endpoints
- Docker and Azure compatibility
Link to frontend part of the application

## Prerequisites:
Before commencing, ensure your development environment satisfies the following prerequisites:
- .NET 6 SDK
- SQL Express Server
- A configured Keycloak server on Cloud-IAM

## Getting Started:
To set up and run the Lagalt.no Backend locally, follow these steps:
steps:
1. Clone the repository: https://github.com/Ekkara/Lagalt-Backend.git
2. Restore the required dependencies:
3. Configure the appropriate environment variables or modify the appsettings.json file 
   with the necessary values for Keycloak integration and database connection.
4. Apply the database migrations:
5. Launch the development server
The API should now be accessible at: https://localhost:7132/swagger/index.html

## Usage Instructions:
Open your preferred web browser and navigate to https://localhost:7132/swagger/index.html
Authenticate using your Keycloak credentials (if required).
Explore and test the API endpoints using the provided mock data and query parameters.

## Database Diagram:
A detailed database diagram illustrating the relationship between tables and
entities is available within the project documentation.

![LagaltNo-CaseDigram](https://user-images.githubusercontent.com/60743602/228863642-43f4996e-de33-4137-b283-c4dedb9539a3.PNG)

## License Information:
Refer to the LICENSE file within the project repository for information regarding the licensing and usage of the Lagalt.no Backend.

Made by: Erik Gustafsson, Mikael Niazi and Tommy Pham
