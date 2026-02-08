<p align="center">
  <h1 align="center">🍽️ My Restaurant</h1>
  <p align="center">Full-Stack Restaurant Management System</p>
</p>

<p align="center">
  <!-- Frontend -->
  <img src="https://img.shields.io/badge/Frontend-Angular%2017.3.11-DD0031?logo=angular&logoColor=white" />
  <img src="https://img.shields.io/badge/Language-TypeScript-3178C6?logo=typescript&logoColor=white" />
  <img src="https://img.shields.io/badge/HTML5-E34F26?logo=html5&logoColor=white" />
  <img src="https://img.shields.io/badge/CSS3-1572B6?logo=css3&logoColor=white" />

  <!-- Backend -->
  <img src="https://img.shields.io/badge/Backend-.NET%208.0-512BD4" />
  <img src="https://img.shields.io/badge/ORM-EF%20Core-6DB33F" />
  <img src="https://img.shields.io/badge/Auth-JWT-orange?logo=jsonwebtokens&logoColor=white" />
  <img src="https://img.shields.io/badge/Email-MailKit-0078D4" />
  <img src="https://img.shields.io/badge/Database-SQL%20Server-0078D4?logo=microsoftsqlserver&logoColor=white" />

  <!-- Tools -->
  <img src="https://img.shields.io/badge/IDE-VS%20%26%20VS%20Code-007ACC?logo=visualstudiocode&logoColor=white" />
  <img src="https://img.shields.io/badge/Git-Git-F05032?logo=git&logoColor=white" />
  <img src="https://img.shields.io/badge/API%20Testing-Postman-FF6C37?logo=postman&logoColor=white" />
</p>

---

## 🚀 Features

- 🔐 User authentication & authorization with **JWT**
- 📱 Responsive UI compatible with all screen sizes
- CRUD operations for **menu items, orders, and users**
- 📧 Email notifications using **MailKit**
- 🔒 Secure data access with **Entity Framework Core**
- 🏗️ Clean architecture with **dependency injection and layered design**

---

## 🧩 Project Structure

/My-Restaurant-Frontend   -> Angular 17 frontend <br>
/My-Restaurant-API        -> ASP.NET Core 8 backend <br>
---
## 🧰 How to Run
Backend (ASP.NET Core)

1. Open the /My-Restaurant-API folder in Visual Studio.

2. Update RestDbContext.cs seed data for your local setup.

3.Run migrations:
```
  dotnet ef migrations add InitialCreate
  dotnet ef database update
```

4. Update appsettings.json with your SQL Server connection string.

5. Run the API:
 ```
   dotnet run
```
Frontend (Angular)

1. Open /My-Restaurant-Frontend in VS Code.

2. Install dependencies:
```
npm install
```

3. Run the app:
```
ng serve
```

4. Navigate to: http://localhost:4200
## 📧 Contact

Developed by Omar Tabikh
📩 Email: omar.suliman.tabikh@gmail.com

🔗 LinkedIn: linkedin.com/in/omar-suliman-t2000
