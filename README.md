# Appointment System - API (ASP.NET Core + SQL Server)

A backend API for managing appointments, users, and authentication, built using **ASP.NET Core** and **SQL Server**.

---

## 📌 Features

✅ User authentication and authorization (ASP.NET Identity)\
✅ Role-based access (Admin, Staff, Customer)\
✅ Secure JWT authentication\
✅ RESTful API for appointment management\
✅ Database interactions via Entity Framework Core\
✅ Email notifications (optional)

---

## 🏗️ Technologies Used

- ASP.NET Core Web API (.NET 8)
- Entity Framework Core (EF Core) with SQL Server
- ASP.NET Identity for authentication
- JWT Authentication & Authorization

---

## 🚀 Setup Instructions

### **1. Clone the Repository**

```sh
git clone https://github.com/yourusername/appointment-api.git
cd appointment-api
```

### **2. Configure the Application**

- Create an `appsettings.json` file and configure:
  - SQL Server connection string
  - JWT secret key
- Example:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=YOUR_SERVER;Database=AppointmentDB;Trusted_Connection=True;"
  },
  "Jwt": {
    "Key": "your-secret-key",
    "Issuer": "your-api",
    "Audience": "your-client"
  }
}
```

### **3. Apply Database Migrations**

```sh
dotnet ef database update
```

### **4. Run the API**

```sh
dotnet run
```

---

## 📌 API Endpoints

| Method | Endpoint                 | Description                     |
| ------ | ------------------------ | ------------------------------- |
| POST   | `/api/auth/register`     | User registration               |
| POST   | `/api/auth/login`        | User login & JWT token issuance |
| GET    | `/api/appointments`      | Get all appointments            |
| POST   | `/api/appointments`      | Create a new appointment        |
| PUT    | `/api/appointments/{id}` | Update an appointment           |
| DELETE | `/api/appointments/{id}` | Delete an appointment           |

---

## 📜 License

This project is licensed under the MIT License.

---

## 👨‍💻 Author

[Your Name](https://github.com/yourusername)

For any questions or suggestions, feel free to open an issue! 🚀

