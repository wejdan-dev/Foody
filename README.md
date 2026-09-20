# 🍔 Foody — Restaurant Management System

A modern restaurant management web application built with **ASP.NET Core 8**, featuring authentication, role-based authorization, and a clean MVC architecture.

![.NET](https://img.shields.io/badge/.NET-8.0-512BD4?logo=dotnet&logoColor=white)
![ASP.NET Core](https://img.shields.io/badge/ASP.NET%20Core-MVC-0078D4?logo=asp.net&logoColor=white)
![Entity Framework](https://img.shields.io/badge/Entity%20Framework-Core-FF6F00?logo=nuget&logoColor=white)
![SQL Server](https://img.shields.io/badge/SQL%20Server-2022-CC2927?logo=microsoft-sql-server&logoColor=white)
![License](https://img.shields.io/badge/license-MIT-green)
![Status](https://img.shields.io/badge/status-Active%20Development-brightgreen)

---

## 📌 Overview

**Foody** is a web-based restaurant management platform that demonstrates a complete ASP.NET Core 8 application following the MVC pattern with Entity Framework Core for data access and ASP.NET Core Identity for authentication.

The project is structured to be educational and easy to extend — perfect for showcasing full-stack development skills.

---

## ✨ Features

- 🔐 **Authentication & Authorization** — Full Identity system with login, registration, and role-based access control
- 📦 **Clean MVC Architecture** — Separated Controllers, Models, Views, ViewModels, and Services
- 🗄️ **Entity Framework Core** — Code-first migrations with SQL Server
- 🎨 **Responsive Frontend** — Bootstrap + jQuery for modern UI interactions
- 🧩 **Areas Pattern** — Identity pages isolated under `Areas/Identity`
- 🔧 **Service Layer** — Business logic encapsulated in dedicated service classes
- 🛡️ **Security-First** — Uses the latest Azure.Identity (1.21.0) and System.Formats.Asn1 (8.0.1) to patch known CVEs

---

## 🛠️ Tech Stack

| Layer | Technology |
|-------|-----------|
| **Framework** | ASP.NET Core 8.0 (MVC) |
| **Language** | C# 12 |
| **ORM** | Entity Framework Core 8.0.8 |
| **Database** | Microsoft SQL Server (LocalDB supported) |
| **Authentication** | ASP.NET Core Identity + EntityFrameworkCore |
| **Frontend** | Razor Views, Bootstrap, jQuery, SCSS |
| **Build Tool** | MSBuild / `dotnet` CLI |

---

## ⚙️ Getting Started

### Prerequisites

Before running this project, make sure you have installed:

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [Visual Studio 2022](https://visualstudio.microsoft.com/) (Community edition works) **OR** [Visual Studio Code](https://code.visualstudio.com/) with C# extension
- [SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads) Express edition or LocalDB (typically bundled with Visual Studio)

### 🔧 Setup Steps

1. **Clone the repository**
   ```bash
   git clone https://github.com/weqdan-dev/Foody.git
   cd Foody
   ```

2. **Open the Solution**
   - Open `Foody.sln` in Visual Studio, **OR**
   - Navigate to the `Front` folder and run from CLI:
     ```bash
     cd Front
     dotnet restore
     ```

3. **Configure the Database Connection**

   Edit `Front/appsettings.json` and update the `ConnectionStrings.DefaultConnection`:
   ```json
   "DefaultConnection": "Server=YOUR_SERVER;Database=FoodyDb;Trusted_Connection=True;TrustServerCertificate=True;MultipleActiveResultSets=true"
   ```

   For a default SQL Server Express install, use:
   ```json
   "DefaultConnection": "Server=localhost\\SQLEXPRESS;Database=FoodyDb;Trusted_Connection=True;TrustServerCertificate=True"
   ```

4. **Apply Database Migrations**
   ```bash
   dotnet ef database update
   ```
   This creates the `FoodyDb` database with all required tables (Identity tables + domain tables).

5. **Run the Application**
   ```bash
   dotnet run
   ```
   Then open `https://localhost:5001` or `http://localhost:5000` in your browser.

---

## 📂 Project Structure

```
Foody/
├── Foody.sln                          ← Solution file
└── Front/                             ← Main web application
    ├── Areas/                         ← Feature-based areas (e.g., Identity)
    ├── Controllers/                   ← MVC Controllers (request handlers)
    ├── Views/                         ← Razor views (.cshtml)
    ├── ViewModels/                    ← View-specific DTOs
    ├── Models/                        ← Domain entities
    ├── Services/                      ← Business logic & external integrations
    ├── Data/                          ← EF Core DbContext & database config
    ├── Migrations/                    ← EF Core code-first migrations
    ├── Helpers/                       ← Helper utilities (auth, extensions)
    ├── Properties/                    ← Launch settings & profiles
    ├── wwwroot/                       ← Static files (CSS, JS, images, lib)
    ├── appsettings.json               ← General configuration
    ├── appsettings.Development.json   ← Development overrides
    └── Program.cs                     ← Application entry point
```

---

## 🗄️ Database

The project uses **Entity Framework Core** with code-first migrations. All schema changes are recorded in `Migrations/` and can be applied via:

```bash
dotnet ef migrations add MigrationName
dotnet ef database update
```

Key tables created by Identity migrations:
- `AspNetUsers`, `AspNetRoles`, `AspNetUserRoles`
- `AspNetUserClaims`, `AspNetRoleClaims`
- `AspNetUserLogins`, `AspNetUserTokens`

---

## 🔒 Security Notes

- `appsettings.Development.json` is **gitignored** by default
- Connection strings and API keys should never be committed to the repository
- Always use [User Secrets](https://learn.microsoft.com/en-us/aspnet/core/security/app-secrets) during local development:
  ```bash
  dotnet user-secrets init
  dotnet user-secrets set "ConnectionStrings:DefaultConnection" "your-connection-string"
  ```

---

## 🧑‍💻 About the Developer

**Weqdan Al-Hunaty** — Junior Full-Stack Developer
specializing in C#, ASP.NET Core MVC & SQL Server, CIS & Correlation.

[![GitHub](https://img.shields.io/badge/GitHub-weqdan--dev-181717?logo=github)](https://github.com/weqdan-dev)

📧 [weqdan.al.hunaty@gmail.com](mailto:weqdan.al.hunaty@gmail.com)

---

## 📜 License

This project is licensed under the **MIT License** — see the [LICENSE](LICENSE) file for details.

---

## 🤝 Contributing

Contributions, issues and feature requests are welcome! Feel free to check the [issues page](https://github.com/weqdan-dev/Foody/issues).

---

⭐ If you found this project useful, consider giving it a star on GitHub!
