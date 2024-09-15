# README 
## Project link
https://chatreenbitclient-cvffbvh5fwbndvhy.eastus-01.azurewebsites.net/
## Українська версія (English version below)
### ВАЖЛИВЕ ПОПЕРЕДЖЕННЯ! 
Якщо під час перегляду проєкту ви тривалий час бачите екран завантаження, навіть отримавши сповіщення "Connected to the server", спробуйте перезавантажити сторінку. Подібні тривалі завантаження трапляються вперше після завантаження сторінки.
### Структура
Загалом сервіс складається із фронтенд і бекенд веб-проєктів. Бекенд-проєкт знаходиться у гілці `dev`, а фронтенд-проєкт розташований у гілці `client` цього ж репозиторія
### Функціонал
1. **Можливість підключатись до чату обираючи ім'я користувача**
2. **Можливість обмінюватись повідомленнями між користувачами одного чату**
3. **Аналіз контексту повідомлення в реальному часі. Визначення емоційного забарвлення повідомлення засобами нейромереж Azure Cognitive Services.**
### Використані технології
1. ASP.NET Core
2. SignalR
3. Blazor Server
4. MS SQL database (Azure SQL)
5. Entity Framework Core 
6. Azure Cognitive Services Text Analytics
### Структура бекенд проєкту
Бекенд-проєкт складається із 3-х бібліотек:
1. ChatSignalR.Server - веб-проєкт, що містить хаб чату із усіма фукнціями, які може викликати при роботі із клієнтським проєктом.
2. ChatSignalR.Core - бібліотека класів, що містить набори моделей, DTO, інтерфейси та сервіси із бізнес-логікою
3. ChatSignalR.DataAccess.AzureSQL - бібліотека класів, що відповідає за взаємодію із базою даних MS SQL, використовуючи EF Core.

### Приклад роботи проекту
![image](https://github.com/user-attachments/assets/b95a05c3-3629-4a74-8423-c138dc189a1b)

## English version
### IMPORTANT NOTICE! 
If, while viewing the project, you see the loading screen for an extended period, even after receiving the notification "Connected to the server," try refreshing the page. Such prolonged loading may occur for the first time after the page loads.
### Structure
The service consists of both frontend and backend web projects. The backend project is located in the `dev` branch, while the frontend project is located in the `client` branch of this repository.
### Features
1. **Ability to connect to the chat by choosing a username.**
2. **Ability to exchange messages between users in the same chat.**
3. **Real-time message context analysis. Determination of the emotional tone of the message using Azure Cognitive Services neural networks.**
### Technologies Used
1. ASP.NET Core
2. SignalR
3. Blazor Server
4. MS SQL database (Azure SQL)
5. Entity Framework Core 
6. Azure Cognitive Services Text Analytics
### Backend Project Structure
The backend project consists of three libraries:
1. **ChatSignalR.Server** - a web project that contains the chat hub with all the functions that can be invoked when working with the client project.
2. **ChatSignalR.Core** - a class library that contains models, DTOs, interfaces, and services with business logic.
3. **ChatSignalR.DataAccess.AzureSQL** - a class library responsible for interacting with the MS SQL database using EF Core.

### Project working example
![image](https://github.com/user-attachments/assets/b95a05c3-3629-4a74-8423-c138dc189a1b)
