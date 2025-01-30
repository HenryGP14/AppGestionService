# Descripci�n del Proyecto

Este proyecto es una API desarrollada en .NET 9 con C# 13.0 que proporciona varios servicios para la gesti�n de usuarios, clientes, contratos, dispositivos, turnos, servicios, y m�s. La API est� protegida con autenticaci�n y autorizaci�n, y utiliza diferentes controladores para manejar las solicitudes HTTP.

## Endpoints

### CashController

- **POST /api/cash**
  - Descripci�n: Obtiene una lista de cajas.
  - Request:
    {
  "NumFilter": 1,
  "TextFilter": "example",
  "StateFilter": 1,
  "StartDate": "2023-01-01",
  "EndDate": "2023-12-31",
  "NumPage": 1,
  "NumRecordsPage": 10,
  "Order": "asc",
  "Sort": "name"
}
- **POST /api/cash/create**
  - Descripci�n: Crea una nueva caja.
  - Request:
    {
  "Description": "Caja 1",
  "Status": "Activa"
}
- **POST /api/cash/{castId:int}**
  - Descripci�n: Obtiene una caja por su ID.
  - Request: No requiere body.

- **PUT /api/cash/edit/{cashId:int}**
  - Descripci�n: Actualiza una caja existente.
  - Request:
- **POST /api/cash/{castId:int}**
  - Descripci�n: Obtiene una caja por su ID.
  - Request: No requiere body.

- **PUT /api/cash/edit/{cashId:int}**
  - Descripci�n: Actualiza una caja existente.
  - Request:
  - {
  "UserId": 1,
  "CashId": 1
}
  - 
### ClientController

- **POST /api/cliente**
  - Descripci�n: Obtiene una lista de clientes.
  - Request:
    {
  "NumFilter": 1,
  "TextFilter": "example",
  "StateFilter": 1,
  "StartDate": "2023-01-01",
  "EndDate": "2023-12-31",
  "NumPage": 1,
  "NumRecordsPage": 10,
  "Order": "asc",
  "Sort": "name"
}
  - **POST /api/cliente/{idClient:int}**
  - Descripci�n: Obtiene un cliente por su ID.
  - Request: No requiere body.

- **POST /api/cliente/create**
  - Descripci�n: Crea un nuevo cliente.
  - Request:
    {
  "Name": "Juan",
  "Lastname": "Perez",
  "Email": "juan.perez@example.com",
  "Identification": "123456789",
  "Phonenumber": "0934567890",
  "Address": "Calle Falsa 123",
  "Referenceaddress": "Cerca del parque"
}
- **PUT /api/cliente/editar/{idClient:int}**
  - Descripci�n: Actualiza un cliente existente.
  - Request:{
  "Name": "Juan",
  "Lastname": "Perez",
  "Email": "juan.perez@example.com",
  "Identification": "123456789",
  "Phonenumber": "1234567890",
  "Address": "Calle Falsa 123",
  "Referenceaddress": "Cerca del parque"
}
- **POST /api/cliente/createContract**
  - Descripci�n: Crea un nuevo contrato para un cliente.
  - Request:
    {
  "Startdate": "2023-01-01T00:00:00Z",
  "Enddate": "2023-12-31T23:59:59Z",
  "ServiceServiceid": 1,
  "StatuscontractStatusid": 1,
  "ClientClientid": 1,
  "MethodpaymentMethodpaymentid": 1
}
  - **POST /api/cliente/{idClient:int}/view/contract**
  - Descripci�n: Obtiene el contrato de un cliente por su ID.
  - Request: No requiere body.

### ContractController

- **PUT /api/contract/upgrade/service**
  - Descripci�n: Actualiza el servicio de un contrato.
  - Request:
    {
  "ContractId": 1,
  "ServiceId": 2,
  "StateContractId": 1
}
  - 
- **PUT /api/contract/change/status**
  - Descripci�n: Cambia el estado de un contrato.
  - Request:
    {
  "ContractId": 1,
  "ServiceId": 2,
  "StateContractId": 1
}
  - ### DeviceController

- **POST /api/service/{serviceId:int}/device/create**
  - Descripci�n: Crea un nuevo dispositivo para un servicio.
  - Request:
    {
  "Devicename": "Dispositivo 1"
}
  - **POST /api/service/{serviceId:int}/device/list**
  - Descripci�n: Obtiene una lista de dispositivos por ID de servicio.
  - Request: No requiere body.

- **PUT /api/service/{serviceId:int}/device/edit/{deviceId:int}**
  - Descripci�n: Actualiza un dispositivo existente.
  - Request:
    {
  "Devicename": "Dispositivo 1 actualizado"
}
  -### LisSelectController

- **GET /api/list/attentionstatus**
  - Descripci�n: Obtiene una lista de estados de atenci�n.
  - Request: No requiere body.

- **GET /api/list/attentiontype**
  - Descripci�n: Obtiene una lista de tipos de atenci�n.
  - Request: No requiere body.

- **GET /api/list/methodpayment**
  - Descripci�n: Obtiene una lista de m�todos de pago.
  - Request: No requiere body.

- **GET /api/list/rol**
  - Descripci�n: Obtiene una lista de roles.
  - Request: No requiere body.

- **GET /api/list/statuscontract**
  - Descripci�n: Obtiene una lista de estados de contrato.
  - Request: No requiere body.

- **GET /api/list/userstatus**
  - Descripci�n: Obtiene una lista de estados de usuario.
  - Request: No requiere body.

- **GET /api/list/cash/user**
  - Descripci�n: Obtiene una lista de cajas por usuario.
  - Request: No requiere body.

### ServiceController

- **POST /api/service**
  - Descripci�n: Obtiene una lista de servicios.
  - Request:
 {
  "NumFilter": 1,
  "TextFilter": "example",
  "StateFilter": 1,
  "StartDate": "2023-01-01",
  "EndDate": "2023-12-31",
  "NumPage": 1,
  "NumRecordsPage": 10,
  "Order": "asc",
  "Sort": "name"
}   
  - **POST /api/service/{idService:int}**
  - Descripci�n: Obtiene un servicio por su ID.
  - Request: No requiere body.

- **POST /api/service/create**
  - Descripci�n: Crea un nuevo servicio.
  - Request:
    {
  "Name": "Servicio 1",
  "Description": "Descripci�n del servicio 1",
  "Velocity": 100,
  "Price": 50.0
}
  - 
- **PUT /api/service/update/{idService:int}**
  - Descripci�n: Actualiza un servicio existente.
  - Request:
    {
  "Name": "Servicio 1 actualizado",
  "Description": "Descripci�n actualizada del servicio 1",
  "Velocity": 200,
  "Price": 75.0
}
  - 
### TurnController

- **POST /api/turn**
  - Descripci�n: Obtiene una lista de turnos.
  - Request:
    {
  "NumFilter": 1,
  "TextFilter": "example",
  "StateFilter": 1,
  "StartDate": "2023-01-01",
  "EndDate": "2023-12-31",
  "NumPage": 1,
  "NumRecordsPage": 10,
  "Order": "asc",
  "Sort": "name"
}
  - 
- **POST /api/turn/create**
  - Descripci�n: Crea un nuevo turno.
  - Request:
  - {
  "Description": "SP02145",
  "Date": "2023-01-01T00:00:00Z",
  "CashId": 1,
  "ClientId": 1,
  "AttentionTypeId": 1,
  "AttentionStateId": 1
}
  - ### UserController

- **POST /api/user**
  - Descripci�n: Obtiene una lista de usuarios.
  - Request:
    {
  "NumFilter": 1,
  "TextFilter": "example",
  "StateFilter": 1,
  "StartDate": "2023-01-01",
  "EndDate": "2023-12-31",
  "NumPage": 1,
  "NumRecordsPage": 10,
  "Order": "asc",
  "Sort": "name"
}
  - - **POST /api/user/login**
  - Descripci�n: Inicia sesi�n y obtiene un token.
  - Request:
    {
  "UserName": "usuario",
  "Password": "contrase�a"
}
  - {
  "UserName": "usuario",
  "Password": "contrase�a"
}
  - - **POST /api/user/create**
  - Descripci�n: Crea un nuevo usuario.
  - Request:
    {
  "Username": "usuario",
  "Email": "usuario@example.com",
  "Password": "contrase�a",
  "Rolid": 1,
  "Statusid": 1
}
  - 
- **PUT /api/user/approve/{idUser:int}**
  - Descripci�n: Aprueba un usuario.
  - Request: No requiere body.