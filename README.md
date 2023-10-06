# MoviesAppBack

### 1. se debe crear la base da datos en Sql server, con el siguiente script
```sql

create database DB_Movies
use DB_Movies

create table usuario(
id int identity(1,1) primary key,
nam varchar(50),
username varchar(20) unique,
pass varchar(16))

create table listFavorites(
id_user int,
id_movie int,
primary key (id_user, id_movie),
foreign key (id_user) references usuario(id))

```

### 2. en el archivo appsettings.json tienes que cambiar "cadenaSQL" por el numbre de tu servidor
```json
  "ConnectionStrings": {
    "cadenaSQL": "Server={Nombre_de_tu_servidor_SQL}; Database=DB_Movies; Trusted_Connection=True; TrustServerCertificate=True;"
  }
```

### 3. tambien, se debe cambiar en el archivo Models/DbMovieContext.cs

```csharp

protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
 
    => optionsBuilder.UseSqlServer("Server={Nombre_de_tu_servidor_SQL};Database=DB_Movies;Trusted_Connection=True;TrustServerCertificate=True;");

```

### 4. Ejecutar, si encuentra problemas con los paquetes hay que instalar paquetes NuGet tales como "Microsoft.EntityFrameworkCore.SqlServer" y Microsoft.EntityFrameworkCore.Tools






