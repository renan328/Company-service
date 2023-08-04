CREATE DATABASE Teste;
USE Teste;

CREATE TABLE Company (
    Id INTEGER UNSIGNED NOT NULL AUTO_INCREMENT,
    Name VARCHAR(50),
    Document VARCHAR(20),
    CreateDate DATETIME,
    UpdateDate DATETIME,
    PRIMARY KEY (id)
);

CREATE TABLE CompanyAddress (
    Id INTEGER UNSIGNED NOT NULL AUTO_INCREMENT,
    CompanyId INTEGER UNSIGNED NOT NULL,
    Street VARCHAR(150),
    Neighborhood VARCHAR(50),
    City VARCHAR(100),
    PostalCode VARCHAR(100),
    Country VARCHAR(50),
    CreateDate DATETIME,
    UpdateDate DATETIME,
    PRIMARY KEY (id),
    FOREIGN KEY (CompanyId)
        REFERENCES Company (id)
);

CREATE TABLE CompanyTelephone (
    Id INTEGER UNSIGNED NOT NULL AUTO_INCREMENT,
    CompanyAddress INTEGER UNSIGNED NOT NULL,
    PhoneNumber VARCHAR(20),
    CreateDate DATETIME,
    UpdateDate DATETIME,
    PRIMARY KEY (id),
    FOREIGN KEY (CompanyAddress)
        REFERENCES CompanyAddress (id)
);