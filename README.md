# 🚀 FinNovaTech Microservices

## 📌 Descripción
FinNovaTech es una arquitectura basada en **microservicios** desarrollada en **.NET 8** con **Kafka**, **Ocelot API Gateway**, y **Autenticación con JWT + Refresh Tokens**. Está diseñada para gestionar usuarios, cuentas, autenticación, y transacciones de manera escalable y segura.

---
## 📂 Arquitectura del Proyecto

### 🏛 **Microservicios**
| Servicio          | Puerto | Descripción                                      |
|------------------|--------|--------------------------------------------------|
| **UserService**  | 7232   | Gestión de usuarios y roles                     |
| **AuthService**  | 7156   | Autenticación con JWT y refresh tokens          |
| **AccountService** | 7141 | Administración de cuentas bancarias             |
| **TransactionService** | 7196 | Procesamiento de transacciones                |
| **ApiGateway**   | 7079   | Puerta de entrada para todos los microservicios |

---
## ⚙️ **Tecnologías y Herramientas**
- **.NET 8**
- **Kafka (Mensajería Asíncrona)**
- **Ocelot (API Gateway)**
- **Entity Framework Core**
- **SQL Server 2022**
- **Docker**
- **Argon2 (Hashing de Contraseñas)**
- **JWT + Refresh Tokens**
- **Swagger (Documentación de API)**

---
## 🛠 **Configuración del Proyecto**

### 1️⃣ **Clonar el repositorio**
```sh
 git clone https://github.com/soygustavofernandez/FinNovaTech.git
```

### 2️⃣ **Levantar los servicios con Docker**
```sh
cd FinNovaTech/docker
docker-compose up -d
```
Esto iniciará los contenedores de **Kafka, Redis, SQL Server, ElasticSearch y Kibana**.

### 3️⃣ **Configurar las bases de datos**
Asegúrate de que cada microservicio esté apuntando a su respectiva base de datos en `appsettings.json`.

Ejemplo para `UserService`:
```json
"ConnectionStrings": {
  "DefaultConnection": "Server=localhost,14333;Database=FinNova_UserDB;User Id=sa;Password=JeFe#0799;TrustServerCertificate=True"
}
```

### 4️⃣ **Ejecutar los microservicios**
Desde Visual Studio o CLI:
```sh
dotnet run --project UserService/UserService.csproj
dotnet run --project AuthService/AuthService.csproj
dotnet run --project AccountService/AccountService.csproj
dotnet run --project TransactionService/TransactionService.csproj
dotnet run --project ApiGateway/ApiGateway.csproj
```

### 5️⃣ **Acceder a la API**
📌 **Swagger** está habilitado para cada microservicio en:

- UserService ➡️ `https://localhost:7232/swagger`
- AuthService ➡️ `https://localhost:7156/swagger`
- AccountService ➡️ `https://localhost:7141/swagger`
- TransactionService ➡️ `https://localhost:7196/swagger`
- **API Gateway** ➡️ `https://localhost:7079/swagger`

Puedes consumir las APIs desde **Postman** o cualquier cliente HTTP.

---
## 🔄 **Mensajería con Kafka**
Los eventos en Kafka permiten comunicación asíncrona entre los microservicios.

### **📤 Productores:**
- `UserService` publica eventos cuando se crea un usuario en el tópico `user-created`.
- `AccountService` envía solicitudes de validación a `user-validation-request`.

### **📥 Consumidores:**
- `AuthService` escucha `user-created` y almacena credenciales en su BD.
- `UserService` consume `user-validation-request` y responde en `user-validation-response`.

#### 📌 **Verificar tópicos en Kafka**
```sh
docker exec -it kafka kafka-topics --list --bootstrap-server localhost:9092
```

#### 📌 **Probar mensajes manualmente**
```sh
docker exec -it kafka kafka-console-producer --broker-list localhost:9092 --topic user-created
```

---
## 🔐 **Autenticación con JWT + Refresh Tokens**
### 1️⃣ **Registro de Usuario**
```http
POST /api/auth/register
Content-Type: application/json
{
  "name": "usuario1",
  "email": "usuario1@gmail.com",
  "password": "secreto",
  "role": 1005
}
```

### 2️⃣ **Inicio de Sesión**
```http
POST /api/auth/login
Content-Type: application/json
{
  "email": "usuario1@gmail.com",
  "password": "secreto"
}
```
Respuesta:
```json
{
  "token": "jwt-access-token",
  "refreshToken": "jwt-refresh-token"
}
```

### 3️⃣ **Renovación del Token**
```http
POST /api/auth/refresh
{
  "refreshToken": "jwt-refresh-token"
}
```

---
## 🔀 **API Gateway (Ocelot)**
El API Gateway centraliza las solicitudes a los microservicios.

📌 **Ejemplo de configuración en `ocelot.json`**:
```json
{
  "Routes": [
    {
      "DownstreamPathTemplate": "/api/Users",
      "DownstreamScheme": "https",
      "DownstreamHostAndPorts": [{ "Host": "localhost", "Port": 7232 }],
      "UpstreamPathTemplate": "/api/users",
      "UpstreamHttpMethod": [ "Get", "Post", "Put", "Delete" ]
    }
  ]
}
```

📌 **Ejemplo de uso a través del Gateway:**
```http
GET https://localhost:7079/api/users
```

---
## 🚀 **TODO y Mejoras Futuras**
✅ Implementación de Kafka para eventos.
✅ Implementación de API Gateway con Ocelot.
✅ Seguridad con JWT y Refresh Tokens.
⏳ Implementar Rate Limiting en el Gateway.
⏳ Monitoreo con Prometheus y Grafana.
⏳ Implementar Circuit Breaker con Polly.

📌 **Autor:** [Gustavo Fernández](https://github.com/soygustavofernandez)

---
### 🎯 **Listo para contribuir?**
Si tienes ideas o mejoras, ¡haz un fork y envía un PR! 💪🚀

