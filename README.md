# HotelManager
## Introduction
### What is **HotelManager**?
HotelManager is an Windows application, used for digitalizing hotel management.  
This also is a project in studying **Introduction to Software Engineering** at **UIT - VNUHCM**.
### Technologies, Libraries and Frameworks
- **C#** .NET Framework 4.6.1
- **MySQL** Database
- [**Material Design in XAML**](http://materialdesigninxaml.net)
- **NuGET** Package Manager
- [**Dapper** ORM](https://dapper-tutorial.net/)
- Bcrypt.NET,...
### Contributors
We currently have 4 contributors / members in our working team.
- Bành Thanh Sơn [@takashato](https://github.com/takashato)
- Phạm Trần Chính [@PTChinh](https://github.com/PTChinh)
- Nguyễn Huỳnh Lợi [@loia5tqd001](https://github.com/loia5tqd001)
- Đào Mạnh Dũng [@manhdung99](https://github.com/manhdung99)
## Installation
If you are using pre-built excutable application with remote MySQL (in GitHub's release section), the following steps are redundant.
### Preparing
1. [Visual Studio 2018 / 2019](https://visualstudio.microsoft.com/)
2. MySQL Server. We suggest using [XAMPP](https://www.apachefriends.org/index.html) for quick installation
3. [.NET Framework 4.6.1 Runtime and Developer Pack](https://dotnet.microsoft.com/download/dotnet-framework/net461)
### Solution for Visual Studio
This repository is written using Visual Studio 2017 / 2019. So, just simple open the solution in Visual Studio, everything will be done automatically.
### Install database structure
1. Open the source file `./db/DatabaseManager.cs`
2. Find the code line and change the values to your MySQL information.
```csharp
private const string CONN_STR = "server=localhost;user=root;database=hotelmanager;port=3306;password=";
```
3. Use any MySQL Manager. If you are using **XAMPP**, turn on *Apache* in **XAMPP Control Panel**. Open the link http://localhost/phpmyadmin
4. Within MySQL Manager, create database then import SQL file `./sql/hotelmanager.sql`
### Build and Run
Just press **Build Solution** or **Start** button in **Visual Studio**. NuGET will GET all dependencies.
