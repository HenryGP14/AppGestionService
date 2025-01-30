# Descripción del Proyecto

Este proyecto es una API desarrollada en .NET 9 con C# 13.0 que proporciona varios servicios para la gestión de usuarios, clientes, contratos, dispositivos, turnos, servicios, y más. La API está protegida con autenticación y autorización, y utiliza diferentes controladores para manejar las solicitudes HTTP.

## Endpoints

### CashController

- **POST /api/cash**
  - Descripción: Obtiene una lista de cajas.
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
  - Descripción: Crea una nueva caja.
  - Request:
    {
  "Description": "Caja 1",
  "Status": "Activa"
}
- **POST /api/cash/{castId:int}**
  - Descripción: Obtiene una caja por su ID.
  - Request: No requiere body.

- **PUT /api/cash/edit/{cashId:int}**
  - Descripción: Actualiza una caja existente.
  - Request:
- **POST /api/cash/{castId:int}**
  - Descripción: Obtiene una caja por su ID.
  - Request: No requiere body.

- **PUT /api/cash/edit/{cashId:int}**
  - Descripción: Actualiza una caja existente.
  - Request:
  - {
  "UserId": 1,
  "CashId": 1
}
  - 
### ClientController

- **POST /api/cliente**
  - Descripción: Obtiene una lista de clientes.
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
  - Descripción: Obtiene un cliente por su ID.
  - Request: No requiere body.

- **POST /api/cliente/create**
  - Descripción: Crea un nuevo cliente.
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
  - Descripción: Actualiza un cliente existente.
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
  - Descripción: Crea un nuevo contrato para un cliente.
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
  - Descripción: Obtiene el contrato de un cliente por su ID.
  - Request: No requiere body.

### ContractController

- **PUT /api/contract/upgrade/service**
  - Descripción: Actualiza el servicio de un contrato.
  - Request:
    {
  "ContractId": 1,
  "ServiceId": 2,
  "StateContractId": 1
}
  - 
- **PUT /api/contract/change/status**
  - Descripción: Cambia el estado de un contrato.
  - Request:
    {
  "ContractId": 1,
  "ServiceId": 2,
  "StateContractId": 1
}
  - ### DeviceController

- **POST /api/service/{serviceId:int}/device/create**
  - Descripción: Crea un nuevo dispositivo para un servicio.
  - Request:
    {
  "Devicename": "Dispositivo 1"
}
  - **POST /api/service/{serviceId:int}/device/list**
  - Descripción: Obtiene una lista de dispositivos por ID de servicio.
  - Request: No requiere body.

- **PUT /api/service/{serviceId:int}/device/edit/{deviceId:int}**
  - Descripción: Actualiza un dispositivo existente.
  - Request:
    {
  "Devicename": "Dispositivo 1 actualizado"
}
  -### LisSelectController

- **GET /api/list/attentionstatus**
  - Descripción: Obtiene una lista de estados de atención.
  - Request: No requiere body.

- **GET /api/list/attentiontype**
  - Descripción: Obtiene una lista de tipos de atención.
  - Request: No requiere body.

- **GET /api/list/methodpayment**
  - Descripción: Obtiene una lista de métodos de pago.
  - Request: No requiere body.

- **GET /api/list/rol**
  - Descripción: Obtiene una lista de roles.
  - Request: No requiere body.

- **GET /api/list/statuscontract**
  - Descripción: Obtiene una lista de estados de contrato.
  - Request: No requiere body.

- **GET /api/list/userstatus**
  - Descripción: Obtiene una lista de estados de usuario.
  - Request: No requiere body.

- **GET /api/list/cash/user**
  - Descripción: Obtiene una lista de cajas por usuario.
  - Request: No requiere body.

### ServiceController

- **POST /api/service**
  - Descripción: Obtiene una lista de servicios.
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
  - Descripción: Obtiene un servicio por su ID.
  - Request: No requiere body.

- **POST /api/service/create**
  - Descripción: Crea un nuevo servicio.
  - Request:
    {
  "Name": "Servicio 1",
  "Description": "Descripción del servicio 1",
  "Velocity": 100,
  "Price": 50.0
}
  - 
- **PUT /api/service/update/{idService:int}**
  - Descripción: Actualiza un servicio existente.
  - Request:
    {
  "Name": "Servicio 1 actualizado",
  "Description": "Descripción actualizada del servicio 1",
  "Velocity": 200,
  "Price": 75.0
}
  - 
### TurnController

- **POST /api/turn**
  - Descripción: Obtiene una lista de turnos.
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
  - Descripción: Crea un nuevo turno.
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
  - Descripción: Obtiene una lista de usuarios.
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
  - Descripción: Inicia sesión y obtiene un token.
  - Request:
    {
  "UserName": "usuario",
  "Password": "contraseña"
}
  - {
  "UserName": "usuario",
  "Password": "contraseña"
}
  - - **POST /api/user/create**
  - Descripción: Crea un nuevo usuario.
  - Request:
    {
  "Username": "usuario",
  "Email": "usuario@example.com",
  "Password": "contraseña",
  "Rolid": 1,
  "Statusid": 1
}
  - 
- **PUT /api/user/approve/{idUser:int}**
  - Descripción: Aprueba un usuario.
  - Request: No requiere body.