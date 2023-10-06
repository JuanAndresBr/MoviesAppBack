# MoviesAppBack
```sql
-- create database DB_Movies
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

