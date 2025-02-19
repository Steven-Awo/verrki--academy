# Bank Account API

## Overview

The Bank Account API is a simple RESTful API built using ASP.NET Core that allows users to manage bank accounts. It provides endpoints for creating accounts, depositing money, withdrawing money, and checking account balances. This API is designed to demonstrate basic CRUD operations and handle common banking transactions.

## Features

- **Create Account**: Allows users to create a new bank account.
- **Deposit**: Enables users to deposit money into their accounts.
- **Withdraw**: Allows users to withdraw money from their accounts.
- **Get Balance**: Provides the current balance of a specified account.

## Endpoints

<!-- ### 1. Create Account

- **URL**: `/api/bank/create`
- **Method**: `POST`
- **Request Body**:
    ```json
    {
        "AccountNumber": "string",
        "AccountHolder": "string"
    }
    ```
- **Response**:
    - **201 Created**: Account successfully created.
    - **400 Bad Request**: Invalid account details.
    - **409 Conflict**: Account already exists. -->

### 2. Deposit

- **URL**: `/api/bank/deposit`
- **Method**: `POST`
- **Request Body**:
    ```json
    {
        "AccountNumber": "string",
        "Amount": decimal
    }
    ```
- **Response**:
    - **200 OK**: Deposit successful with updated balance.
    - **400 Bad Request**: Invalid request parameters.
    - **404 Not Found**: Account not found.

### 3. Withdraw

- **URL**: `/api/bank/withdraw`
- **Method**: `POST`
- **Request Body**:
    ```json
    {
        "AccountNumber": "string",
        "Amount": decimal
    }
    ```
- **Response**:
    - **200 OK**: Withdrawal successful with updated balance.
    - **400 Bad Request**: Invalid request parameters or insufficient balance.
    - **404 Not Found**: Account not found.

### 4. Get Balance

- **URL**: `/api/bank/balance/{accountNumber}`
- **Method**: `GET`
- **Response**:
    - **200 OK**: Returns the current balance of the account.
    - **400 Bad Request**: Invalid account number.
    - **404 Not Found**: Account not found.

## Models

### BankAccount

Represents a bank account with the following properties:

- `AccountNumber`: The unique identifier for the account (required).
- `AccountHolder`: The name of the account holder (required).
- `Balance`: The current balance of the account (default is 0).

### TransactionRequest

Represents a transaction request with the following properties:

- `AccountNumber`: The unique identifier for the account (required).
- `Amount`: The amount to be deposited or withdrawn.

## Getting Started

1. Clone the repository:
   ```bash
   git clone <repository-url>
   ```

2. Navigate to the project directory:
   ```bash
   cd BankAccountAPI
   ```

3. Restore the dependencies:
   ```bash
   dotnet restore
   ```

4. Run the application:
   ```bash
   dotnet run
   ```

5. The API will be available at `http://localhost:5000/api/bank`.

## Testing with Postman

You can test the API using Postman by sending requests to the endpoints listed above. Make sure to set the appropriate HTTP method, URL, and request body.

## Contributing

Contributions are welcome! Please feel free to submit a pull request or open an issue for any enhancements or bug fixes.

## License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.