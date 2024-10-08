# Candidate Hub API

## Description
This project is a web application with a REST API for storing job candidate information. It supports adding and updating candidate data using their email as the unique identifier.

## Technologies Used
- .NET 7
- Entity Framework Core
- AutoMapper
- Swagger
- xUnit
- Moq

## Getting Started
1. Clone the repository.
2. Open the solution in Visual Studio.
3. Update the DatabaseType and the corresponding connection string in `appsettings.json`.
4. Run the application.

## API Endpoint
### Add or Update Candidate
- **URL:** `/api/candidates`
- **Method:** `POST`
- **Request Body:**
```json
{
  "firstName": "string",
  "lastName": "string",
  "email": "user@example.com",
  "comment": "string",
  "phoneNumber": "string",
  "callInterval": "string",
  "linkedInUrl": "string",
  "gitHubUrl": "string"
}
```

## Assumptions
- **SQLite is currently used for storage, but you can use any of the availabe Database providers available in the `appsettings.json`.**
- **The application is designed to be extendable for future storage migrations.**

## Improvments
- **Implement more detailed validation and error handling.**
- **Utilize caching for frequently accessed data.**
- **Expand unit test coverage to include more edge cases.**

## Time Spent
**Total:** 5 hours
