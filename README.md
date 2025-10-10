## Technologies Used

Frontend:

Angular 17.3.11

TypeScript, HTML, CSS

Responsive UI with modern layout & components

Backend:

ASP.NET Core 8.0 (Web API)

Entity Framework Core (Code First)

MailKit (for sending emails & notifications)

JWT Authentication (for secure login)

SQL Server Database

Other Tools:

Visual Studio & VS Code

Git & GitHub

Postman for API testing

🚀 Features

User authentication & authorization using JWT tokens

Responsive UI compatible with all screen sizes

CRUD operations for menu items, orders, and users

Email notifications using MailKit

Secure data access with Entity Framework

Organized architecture with dependency injection and layered design

🧩 Project Structure
/My-Restruant-Frontend  -> Angular 17 frontend  
/My-Resturant-API    -> ASP.NET Core 8 backend  

🧰 How to Run

1. Open the '/My-Restaurant-API' folder in **Visual Studio**.  
2. Open **'RestDbContext.cs'** and modify the **seed data** to fit your local setup.  
3. Run the following commands in the **Package Manager Console** or terminal:
   dotnet ef migrations add InitialCreate
   dotnet ef database update
This will apply migrations and create the database with your seed data.
4. Update appsettings.json with your SQL Server connection string and update the program.cs with your SQL Server connection.
5. Run the API using:
dotnet run

Frontend:

Open the /My-Restruant-Frontend folder in VS Code.

Install dependencies: npm install

Run the app: ng serve

Navigate to: http://localhost:4200

📧 Contact

Developed by Omar Tabikh
📩 omarsit20004031@gmail.com

🔗 LinkedIn Profile : www.linkedin.com/in/omar-suliman-t2000

To get more help on the Angular CLI use 'ng help' or go check out the [Angular CLI Overview and Command Reference](https://angular.io/cli) page.
