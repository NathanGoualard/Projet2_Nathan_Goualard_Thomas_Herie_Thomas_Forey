CREATE DATABASE IF NOT EXISTS bibliotheque;
USE bibliotheque;

CREATE TABLE IF NOT EXISTS Auteurs(
   Id_Auteurs INT AUTO_INCREMENT,
   Nom VARCHAR(50),
   Prenom VARCHAR(50),
   PRIMARY KEY(Id_Auteurs)
);

CREATE TABLE IF NOT EXISTS Genres(
   Id_Genres INT AUTO_INCREMENT,
   Nom VARCHAR(50),
   PRIMARY KEY(Id_Genres)
);

CREATE TABLE IF NOT EXISTS Roles(
   Id_Roles INT AUTO_INCREMENT,
   Nom VARCHAR(50),
   PRIMARY KEY(Id_Roles)
);

CREATE TABLE IF NOT EXISTS Livres(
   Id_Livres INT AUTO_INCREMENT,
   Nom VARCHAR(50),
   Annee DATE,
   Id_Auteurs INT NOT NULL,
   Id_Genres INT NOT NULL,
   PRIMARY KEY(Id_Livres),
   FOREIGN KEY(Id_Auteurs) REFERENCES Auteurs(Id_Auteurs),
   FOREIGN KEY(Id_Genres) REFERENCES Genres(Id_Genres)
);

CREATE TABLE IF NOT EXISTS Stock(
   Id_Stock INT AUTO_INCREMENT,
   Nb INT,
   Id_Livres INT NOT NULL,
   PRIMARY KEY(Id_Stock),
   FOREIGN KEY(Id_Livres) REFERENCES Livres(Id_Livres)
);

CREATE TABLE IF NOT EXISTS Utilisateurs(
   Id_Utilisateurs INT AUTO_INCREMENT,
   Nom VARCHAR(50),
   Prenom VARCHAR(50),
   Id_Roles INT NOT NULL,
   PRIMARY KEY(Id_Utilisateurs),
   FOREIGN KEY(Id_Roles) REFERENCES Roles(Id_Roles)
);

CREATE TABLE IF NOT EXISTS Retours(
   Id_Retours INT AUTO_INCREMENT,
   Date_ DATE,
   Nb INT,
   Id_Stock INT NOT NULL,
   Id_Utilisateurs INT NOT NULL,
   PRIMARY KEY(Id_Retours),
   FOREIGN KEY(Id_Stock) REFERENCES Stock(Id_Stock),
   FOREIGN KEY(Id_Utilisateurs) REFERENCES Utilisateurs(Id_Utilisateurs)
);

CREATE TABLE IF NOT EXISTS Emprunts(
   Id_Emprunts INT AUTO_INCREMENT,
   Date_ DATETIME,
   Nb INT,
   Id_Stock INT NOT NULL,
   Id_Utilisateurs INT NOT NULL,
   PRIMARY KEY(Id_Emprunts),
   FOREIGN KEY(Id_Stock) REFERENCES Stock(Id_Stock),
   FOREIGN KEY(Id_Utilisateurs) REFERENCES Utilisateurs(Id_Utilisateurs)
);
