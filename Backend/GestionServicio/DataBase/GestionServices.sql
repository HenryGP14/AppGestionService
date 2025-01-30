/*
* Author: Henry Perez
* Email: perezh010@gmail.com
* Fecha: 26/01/2025 
*/

CREATE DATABASE GestionServices;
GO

USE GestionServices;
GO

-- Creación de tablas

-- Tabla userstatus
CREATE TABLE userstatus (
    statusid INT PRIMARY KEY IDENTITY(1,1),
    description VARCHAR(50) NOT NULL,
	datecreation DATETIMEOFFSET NOT NULL,
	dateupdate DATETIMEOFFSET,
	datedelete DATETIMEOFFSET,
);

-- Tabla rol
CREATE TABLE rol (
    rolid INT PRIMARY KEY IDENTITY(1,1),
    rolname VARCHAR(50) NOT NULL,
	datecreation DATETIMEOFFSET NOT NULL,
	dateupdate DATETIMEOFFSET,
	datedelete DATETIMEOFFSET,
);

-- Tabla cash
CREATE TABLE cash (
    cashid INT PRIMARY KEY IDENTITY(1,1),
    cashdescription VARCHAR(50) NOT NULL,
    active VARCHAR(1) NOT NULL,
	datecreation DATETIMEOFFSET NOT NULL,
	dateupdate DATETIMEOFFSET,
	datedelete DATETIMEOFFSET,
);

-- Tabla client
CREATE TABLE client (
    clientid INT PRIMARY KEY IDENTITY(1,1),
    name VARCHAR(50) NOT NULL,
    lastname VARCHAR(50) NOT NULL,
    identification VARCHAR(13) NOT NULL,
    email VARCHAR(120),
    phonenumber VARCHAR(13),
    address VARCHAR(100),
    referenceaddress VARCHAR(100),
	datecreation DATETIMEOFFSET NOT NULL,
	dateupdate DATETIMEOFFSET,
	datedelete DATETIMEOFFSET,
);

-- Tabla methodpayment
CREATE TABLE methodpayment (
    methodpaymentid INT PRIMARY KEY IDENTITY(1,1),
    description VARCHAR(50) NOT NULL,
	datecreation DATETIMEOFFSET NOT NULL,
	dateupdate DATETIMEOFFSET,
	datedelete DATETIMEOFFSET,
);

-- Tabla service
CREATE TABLE service (
    serviceid INT PRIMARY KEY IDENTITY(1,1),
    servicename VARCHAR(100) NOT NULL,
    servicedescription VARCHAR(150),
    price VARCHAR(10) NOT NULL,
	datecreation DATETIMEOFFSET NOT NULL,
	dateupdate DATETIMEOFFSET,
	datedelete DATETIMEOFFSET,
);

-- Tabla statuscontract
CREATE TABLE statuscontract (
    statusid INT PRIMARY KEY IDENTITY(1,1),
    description VARCHAR(50) NOT NULL,
	datecreation DATETIMEOFFSET NOT NULL,
	dateupdate DATETIMEOFFSET,
	datedelete DATETIMEOFFSET,
);

-- Tabla attentiontype
CREATE TABLE attentiontype (
    attentiontypeid INT PRIMARY KEY IDENTITY(1,1),
    description VARCHAR(100) NOT NULL,
	datecreation DATETIMEOFFSET NOT NULL,
	dateupdate DATETIMEOFFSET,
	datedelete DATETIMEOFFSET,
);

-- Tabla attentionstatus
CREATE TABLE attentionstatus (
    statusid INT PRIMARY KEY IDENTITY(1,1),
    description VARCHAR(30) NOT NULL,
	datecreation DATETIMEOFFSET NOT NULL,
	dateupdate DATETIMEOFFSET,
	datedelete DATETIMEOFFSET,
);

-- Tabla user
CREATE TABLE [user] (
    userid INT PRIMARY KEY IDENTITY(1,1),
    username VARCHAR(50) NOT NULL,
    email VARCHAR(100) NOT NULL,
    password VARCHAR(100) NOT NULL,
    rol_rolid INT NOT NULL,
    creationdate DATETIMEOFFSET,
    usercreate INT,
    userapproval INT,
    dateapproval DATETIMEOFFSET,
    userstatus_statusid INT NOT NULL,
	datecreation DATETIMEOFFSET NOT NULL,
	dateupdate DATETIMEOFFSET,
	datedelete DATETIMEOFFSET,
    FOREIGN KEY (rol_rolid) REFERENCES rol(rolid),
    FOREIGN KEY (userstatus_statusid) REFERENCES userstatus(statusid)
);

-- Tabla device
CREATE TABLE device (
    deviceid INT PRIMARY KEY IDENTITY(1,1),
    devicename VARCHAR(50) NOT NULL,
    service_serviceid INT NOT NULL,
	datecreation DATETIMEOFFSET NOT NULL,
	dateupdate DATETIMEOFFSET,
	datedelete DATETIMEOFFSET,
    FOREIGN KEY (service_serviceid) REFERENCES service(serviceid)
);

-- Tabla contract
CREATE TABLE contract (
    contractid INT PRIMARY KEY IDENTITY(1,1),
    startdate DATETIMEOFFSET NOT NULL,
    enddate DATETIMEOFFSET NOT NULL,
    service_serviceid INT NOT NULL,
    statuscontract_statusid INT NOT NULL,
    client_clientid INT NOT NULL,
    methodpayment_methodpaymentid INT NOT NULL,
	datecreation DATETIMEOFFSET NOT NULL,
	dateupdate DATETIMEOFFSET,
	datedelete DATETIMEOFFSET,
    FOREIGN KEY (service_serviceid) REFERENCES service(serviceid),
    FOREIGN KEY (statuscontract_statusid) REFERENCES statuscontract(statusid),
    FOREIGN KEY (client_clientid) REFERENCES client(clientid),
    FOREIGN KEY (methodpayment_methodpaymentid) REFERENCES methodpayment(methodpaymentid)
);

-- Tabla payments
CREATE TABLE payments (
    paymentid INT PRIMARY KEY IDENTITY(1,1),
    paymentdate DATETIMEOFFSET NOT NULL,
    client_clientid INT NOT NULL,
	datecreation DATETIMEOFFSET NOT NULL,
	dateupdate DATETIMEOFFSET,
	datedelete DATETIMEOFFSET,
    FOREIGN KEY (client_clientid) REFERENCES client(clientid)
);

-- Tabla turn
CREATE TABLE turn (
    turnid INT PRIMARY KEY IDENTITY(1,1),
    description VARCHAR(7) NOT NULL,
    date DATETIMEOFFSET NOT NULL,
    cash_cashid INT NOT NULL,
    usergestorid INT NOT NULL,
	datecreation DATETIMEOFFSET NOT NULL,
	dateupdate DATETIMEOFFSET,
	datedelete DATETIMEOFFSET,
    FOREIGN KEY (cash_cashid) REFERENCES cash(cashid)
);

-- Tabla attention
CREATE TABLE attention (
    attentionid INT PRIMARY KEY IDENTITY(1,1),
    turn_turnid INT NOT NULL,
    client_clientid INT NOT NULL,
    attentiontype_attentiontypeid INT NOT NULL,
    attentionstatus_statusid INT NOT NULL,
	datecreation DATETIMEOFFSET NOT NULL,
	dateupdate DATETIMEOFFSET,
	datedelete DATETIMEOFFSET,
    FOREIGN KEY (turn_turnid) REFERENCES turn(turnid),
    FOREIGN KEY (client_clientid) REFERENCES client(clientid)
);

-- Tabla usercash (relación muchos a muchos)
CREATE TABLE usercash (
    user_userid INT,
    cash_cashid INT,
	datecreation DATETIMEOFFSET NOT NULL,
	dateupdate DATETIMEOFFSET,
	datedelete DATETIMEOFFSET,
    PRIMARY KEY (user_userid, cash_cashid),
    FOREIGN KEY (user_userid) REFERENCES [user](userid),
    FOREIGN KEY (cash_cashid) REFERENCES cash(cashid)
);

-- Tabla de errors
CREATE TABLE errors(
	error_id INT PRIMARY KEY IDENTITY(1,1),
	nameprocess TEXT NOT NULL,
	description TEXT NOT NULL,
	datecreation DATETIMEOFFSET NOT NULL,
	usercreation VARCHAR(20) NOT NULL,
	ipcreation VARCHAR(11) NOT NULL
);

-- Creación de procedimiento
GO
CREATE PROCEDURE InsertErrorRecord
(
    @nameprocess TEXT,
    @description TEXT,
    @usercreation VARCHAR(20),
    @ipcreation VARCHAR(11),
    @result INT OUTPUT
)
AS
BEGIN
    SET NOCOUNT ON;

    BEGIN TRY
        INSERT INTO errors (nameprocess, description, datecreation, usercreation, ipcreation)
        VALUES (@nameprocess, @description, SYSDATETIMEOFFSET(), @usercreation, @ipcreation);

        SET @result = 1; -- Éxito
    END TRY
    BEGIN CATCH
        SET @result = 0; -- Error
    END CATCH
END;

-- Insersión de datos iniciales
INSERT INTO rol (rolname, datecreation) VALUES ('Administrador', SYSDATETIMEOFFSET()), ('Cajero', SYSDATETIMEOFFSET()), ('Cliente', SYSDATETIMEOFFSET()), ('Gestor', SYSDATETIMEOFFSET());

INSERT INTO userstatus (description, datecreation) VALUES ('Activo',SYSDATETIMEOFFSET()), ('Inactivo', SYSDATETIMEOFFSET());

INSERT INTO methodpayment (description, datecreation) VALUES ('Tarjeta de Crédito', SYSDATETIMEOFFSET()), ('Efectivo', SYSDATETIMEOFFSET());

INSERT INTO statuscontract (description, datecreation) VALUES ('Nuevo', SYSDATETIMEOFFSET()), ('Renovación', SYSDATETIMEOFFSET()), ('Cancelado', SYSDATETIMEOFFSET()), ('Finalizado', SYSDATETIMEOFFSET()), ('Finalizado por upgrade', SYSDATETIMEOFFSET());

INSERT INTO attentiontype (description, datecreation) VALUES ('General', SYSDATETIMEOFFSET()), ('Especializada', SYSDATETIMEOFFSET());

INSERT INTO attentionstatus (description, datecreation) VALUES ('Pendiente', SYSDATETIMEOFFSET()), ('Finalizado', SYSDATETIMEOFFSET());

INSERT INTO [user] (username, email, password, rol_rolid, creationdate, usercreate, userapproval, dateapproval, userstatus_statusid, datecreation)
VALUES ('admin', 'admin', '$2a$11$HjsCrv3ajMtxCs6duSUDA.G3SKc5d2tes7tYgu.yMAdqXP0B3qM6a', 1, SYSDATETIMEOFFSET(), 1, 1, SYSDATETIMEOFFSET(), 1, SYSDATETIMEOFFSET());
