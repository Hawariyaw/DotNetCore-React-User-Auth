# DotNetCore-React-User-Auth
DotNetCore-React-User-Auth

Getting Started with .Net Core Server
```bash
git clone https://github.com/Hawariyaw/DotNetCore-React-User-Auth.git
dotnet restore
dotnet run 
```
Swagger URL for Server API's: https://localhost:5001/swagger/index.html

Getting Started with React UI
```bash
npm i
npm run
```
URL for React UI: http://localhost:3001/

User must aquire SendGrid Api to make the application send email on registration.
```bash
    "AppSettings": {
        "Secret": "USER_REGISTRATION_KEY",
        "SendGridKey": "SEND GRID API KEY"
    }
 ```
