Sure! Here's a sample README file for your project:

# Authentication API

The Authentication API is a .NET-based web API that provides user authentication and profile management functionalities. It allows users to register, log in, manage their profiles, and integrate with Google Sign-In for authentication.

## Prerequisites

To run the Authentication API, make sure you have the following prerequisites installed on your machine:

- [.NET SDK 6.0](https://dotnet.microsoft.com/download/dotnet/6.0)

## Getting Started

Follow the steps below to get the Authentication API up and running:

1. Clone the repository:

   ```bash
   git clone <repository-url>
   ```

2. Navigate to the `AuthenticationApi` folder:

   ```bash
   cd AuthenticationApi
   ```

3. Restore the project dependencies:

   ```bash
   dotnet restore
   ```

4. Configure the application settings:

   - Open the `appsettings.json` file and update the necessary configurations, such as database connection string, JWT secret key, and Google API credentials.

5. Run the API:

   ```bash
   dotnet run
   ```

6. The API should now be running on `http://localhost:5000`.

## API Documentation

The Authentication API is documented using Swagger. You can access the Swagger UI by navigating to `http://localhost:5000/swagger` in your browser. The Swagger UI provides an interactive interface to explore and test the API endpoints.

## Usage

The Authentication API provides the following endpoints:

- `POST /api/register`: Register a new user.
- `POST /api/login`: Log in with username and password to obtain a JWT token.
- `GET /api/user/profile`: Get the current user's profile.
- `PUT /api/user/profile`: Update the current user's profile.

Make sure to include the appropriate headers, such as `Authorization`, when accessing protected endpoints that require authentication.

## Testing

The project includes unit tests for the API endpoints and service methods. To run the tests, execute the following command from the root directory:

```bash
dotnet test
```

## Docker

The Authentication API can be run inside a Docker container. To build and run the application using Docker, follow these steps:

1. Build the Docker image:

   ```bash
   docker build -t authentication-api .
   ```

2. Run the Docker container:

   ```bash
   docker run -d -p 5000:80 authentication-api
   ```

3. The API should now be accessible at `http://localhost:5000`.

## Contributing

Contributions to the Authentication API are welcome! If you find any issues or have suggestions for improvements, please create a new issue or submit a pull request.

## License

This project is licensed under the [MIT License](LICENSE).

---

Feel free to update the README file with more specific instructions, additional sections, or any other relevant information about your project.