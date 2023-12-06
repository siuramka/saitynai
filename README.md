## Sistemos paskirtis
Automatizavimo programų pardavimo portalas. 

Sistema skirta palengvinti Automatizavimo programų pardavimus. Pardavėjas gali sukurti parduotuvę ir sukurti joje parduodamas programas. Vartotojas gali užsisakyti prenumeratą šios programos.

## Funkciniai reikalavimai
## Svečias gali:
    Prisiregistruti
        Kaip vartotojas
        Kaip pardavėjas
    Prisijungti
Pardavėjas gali:
    • Sukurti parduotuvę
    • Sukurti pardudamą programą
        o Programa gali būti ir nemokama
    • Matyti savo parduotuves
    • Matyti savo parduotuvės programas
    • Redaguoti programą
    • Redaguoti parduotuvę

Vartotojas gali:
    • Matyti visas parduotuves
    • Matyti parduotuvės parduodamas programas
    • Pasirinkti parduodamą program
        o Įdėti/Išimti prekes į krepšelį
        o Prenumeruoti programą
    • Matyti savo prenumeratas
        o Redaguoti prenumeratą
    ▪ Keisti terminą
    ▪ Istrinti prenumeratą

Administratrius gali:
• Matyti visas programas
    o Ištrinti programą
• Matyti visas parduotuves
    o Ištrinti parduotuvę

## Sistems architektūra

    Backend - .NET 7, EF Core, PostgreSQL

    Frontend – React Vite Typescript

    Deployment - Docker Compose, NGINX, Ubuntu, Neon
    
## Deplyment diagrama


![uml](./diagrams/uml.png)

# BackendApi

### /api/auth/register/user

#### POST
##### Responses

| Code | Description |
| ---- | ----------- |
| 200 | Success |

```http
GET /api/auth/register/user
```

Parameters
```
{
  "email": "agasg@gmail.com",
  "password": "!@5gwrG#$315"
}
```

Response

```
{
    "accessToken": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9lbWFpbGFkZHJlc3MiOiJhZ2FzZ0BnbWFpbC5jb20iLCJ1c2VySWQiOiJhNWZiZjQ0Zi1jNThlLTQ4ZTMtYTFhOS1hNWVlZjQ1ZmUwZjEiLCJqdGkiOiI1Yzk1MmI5Ny1kMzA1LTQ0YTctODI5OC0xOGFjMTdlYmRjZDMiLCJodHRwOi8vc2NoZW1hcy5taWNyb3NvZnQuY29tL3dzLzIwMDgvMDYvaWRlbnRpdHkvY2xhaW1zL3JvbGUiOiJTaG9wU2VsbGVyIiwiZXhwIjoxNzAxODg1MDU5LCJpc3MiOiJuYXNhLmdvdiIsImF1ZCI6IlRydXN0ZWRDbGllbnQifQ.KDXYGbNMgUtoLo3zsbXuN97bLtpLoPt1zbCRjOem4tQ",
    "refreshToken": "SKvEkgs8vJWLrGhlhVdl2E7tSbnaBknC2TqE+fEZNhE="
}
```

### /api/auth/register/seller

#### POST
##### Responses

| Code | Description |
| ---- | ----------- |
| 200 | Success |
| 400 | Bad request |

```http
GET /api/auth/register/seller
```

Parameters
```
{
  "email": "agasg11@gmail.co",
  "password": "!@5gwrG#$315"
}
```

Response

```
{
    "accessToken": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9lbWFpbGFkZHJlc3MiOiJhZ2FzZzExQGdtYWlsLmNvbSIsInVzZXJJZCI6ImJkOWMxMTVkLTYyODctNDZhYS1iYjMzLTJhZjRlNTc5ZDQ2MyIsImp0aSI6IjhmMTAzZTU5LTVkNWYtNDVhZC04ZWI1LTJiYWVhYmRiZjE0NSIsImh0dHA6Ly9zY2hlbWFzLm1pY3Jvc29mdC5jb20vd3MvMjAwOC8wNi9pZGVudGl0eS9jbGFpbXMvcm9sZSI6IlNob3BTZWxsZXIiLCJleHAiOjE3MDE4ODUxNDMsImlzcyI6Im5hc2EuZ292IiwiYXVkIjoiVHJ1c3RlZENsaWVudCJ9.oU5M1W9xv3iCXI0tEliBDNCWGAYqrj-Rp1dgHfJRnro",
    "refreshToken": "C0iZI/6bd/cHdrtr3BU0GoMUxrD8Hk3sEiUs792uu3I="
}
```

### /api/auth/login

#### POST
##### Responses

| Code | Description |
| ---- | ----------- |
| 200 | Success |

```http
GET /api/auth/login
```

Parameters
```
{
  "email": "agasg11@gmail.co",
  "password": "!@5gwrG#$315"
}
```

Response

```
{
    "accessToken": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9lbWFpbGFkZHJlc3MiOiJhZ2FzZzExQGdtYWlsLmNvbSIsInVzZXJJZCI6ImJkOWMxMTVkLTYyODctNDZhYS1iYjMzLTJhZjRlNTc5ZDQ2MyIsImp0aSI6IjhmMTAzZTU5LTVkNWYtNDVhZC04ZWI1LTJiYWVhYmRiZjE0NSIsImh0dHA6Ly9zY2hlbWFzLm1pY3Jvc29mdC5jb20vd3MvMjAwOC8wNi9pZGVudGl0eS9jbGFpbXMvcm9sZSI6IlNob3BTZWxsZXIiLCJleHAiOjE3MDE4ODUxNDMsImlzcyI6Im5hc2EuZ292IiwiYXVkIjoiVHJ1c3RlZENsaWVudCJ9.oU5M1W9xv3iCXI0tEliBDNCWGAYqrj-Rp1dgHfJRnro",
    "refreshToken": "C0iZI/6bd/cHdrtr3BU0GoMUxrD8Hk3sEiUs792uu3I="
}
```

### /api/auth/refresh

#### POST
##### Responses

| Code | Description |
| ---- | ----------- |
| 200 | Success |

#
```http
GET /api/auth/refresh
```

Parameters
```
{
  "accessToken": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9lbWFpbGFkZHJlc3MiOiJhZ2FzZzExQGdtYWlsLmNvbSIsInVzZXJJZCI6ImJkOWMxMTVkLTYyODctNDZhYS1iYjMzLTJhZjRlNTc5ZDQ2MyIsImp0aSI6IjhmMTAzZTU5LTVkNWYtNDVhZC04ZWI1LTJiYWVhYmRiZjE0NSIsImh0dHA6Ly9zY2hlbWFzLm1pY3Jvc29mdC5jb20vd3MvMjAwOC8wNi9pZGVudGl0eS9jbGFpbXMvcm9sZSI6IlNob3BTZWxsZXIiLCJleHAiOjE3MDE4ODUxNDMsImlzcyI6Im5hc2EuZ292IiwiYXVkIjoiVHJ1c3RlZENsaWVudCJ9.oU5M1W9xv3iCXI0tEliBDNCWGAYqrj-Rp1dgHfJRnro",
  "refreshToken": "C0iZI/6bd/cHdrtr3BU0GoMUxrD8Hk3sEiUs792uu3I="
}
```

Response

```
{
    "accessToken": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9lbWFpbGFkZHJlc3MiOiJhZ2FzZzExQGdtYWlsLmNvbSIsInVzZXJJZCI6ImJkOWMxMTVkLTYyODctNDZhYS1iYjMzLTJhZjRlNTc5ZDQ2MyIsImp0aSI6IjMyZTBjMDU4LTZlNzktNDM4OC1hM2Y0LTM0YjFlMjhlZGVmZiIsImh0dHA6Ly9zY2hlbWFzLm1pY3Jvc29mdC5jb20vd3MvMjAwOC8wNi9pZGVudGl0eS9jbGFpbXMvcm9sZSI6IlNob3BTZWxsZXIiLCJleHAiOjE3MDE4ODUyNDksImlzcyI6Im5hc2EuZ292IiwiYXVkIjoiVHJ1c3RlZENsaWVudCJ9.No_KvIXzE5VgyLCPuAkzumRBeE-a_aGUEcE1NZtD620",
    "refreshToken": "vthqUVZ3y4gBO5LXrRPn7P4juYVXnDaLn0HRejxeWkc="
}
```



### /api/shops

#### GET
##### Parameters

| Name | Located in | Description | Required | Schema |
| ---- | ---------- | ----------- | -------- | ---- |
| PageNumber | query |  | No | integer |
| PageSize | query |  | No | integer |
| OrderBy | query |  | No | string |

### Authorization
### * Admin, User, Seller

##### Responses

| Code | Description |
| ---- | ----------- |
| 200 | Success |

```
[
  {
    "id": 1,
    "name": "shopName",
    "description": "description",
    "contactInformation": "@contactme"
  }
]
```

#### POST
##### Responses

| Code | Description |
| ---- | ----------- |
| 201 | Created |

```
{
  "name": "PC Shop",
  "description": "Selling PC Software",
  "contactInformation": "@obama"
}
```

```
{
    "id": 3,
    "name": "PC Shop",
    "description": "Selling PC Software",
    "contactInformation": "@obama"
}
```



### /api/shops/{id}

#### GET
##### Parameters

| Name | Located in | Description | Required | Schema |
| ---- | ---------- | ----------- | -------- | ---- |
| id | path |  | Yes | integer |

##### Responses

| Code | Description |
| ---- | ----------- |
| 200 | Success |
| 404 | Not Found |


/api/shops/3
```
{
    "id": 3,
    "name": "PC Shop",
    "description": "Selling PC Software",
    "contactInformation": "@obama"
}
```

#### Example

#### PUT
##### Parameters

| Name | Located in | Description | Required | Schema |
| ---- | ---------- | ----------- | -------- | ---- |
| id | path |  | Yes | integer |

##### Responses

| Code | Description |
| ---- | ----------- |
| 200 | Success |

/api/shops/3
```
{
  "name": "PC Shop New",
  "description": "Selling PC Software",
  "contactInformation": "@obama"
}
```


```
{
    "id": 3,
    "name": "PC Shop New",
    "description": "Selling PC Software",
    "contactInformation": "@obama"
}
```

#### DELETE
##### Parameters

| Name | Located in | Description | Required | Schema |
| ---- | ---------- | ----------- | -------- | ---- |
| id | path |  | Yes | integer |

##### Responses

| Code | Description |
| ---- | ----------- |
| 204 | No Content |
| 404 | Not found |


/api/shops/4

```
204
```


### /api/shops/{shopId}/softwares

#### GET
##### Parameters

| Name | Located in | Description | Required | Schema |
| ---- | ---------- | ----------- | -------- | ---- |
| PageNumber | query |  | No | integer |
| PageSize | query |  | No | integer |
| OrderBy | query |  | No | string |
| shopId | path |  | Yes | integer |

api/shops/4/softwares/3

```
api/shops/4/softwares/3 200

{
    "id": 3,
    "name": "Windows 10",
    "description": "Windows 10 PRO LICENCE KEY",
    "priceMonthly": 15,
    "website": "microsoft.com",
    "instructions": "1. Download 2. Buy"
}
```


##### Responses

| Code | Description |
| ---- | ----------- |
| 200 | Success |

#### POST
##### Parameters

| Name | Located in | Description | Required | Schema |
| ---- | ---------- | ----------- | -------- | ---- |
| shopId | path |  | Yes | integer |

##### Responses

| Code | Description |
| ---- | ----------- |
| 201 | Success |
| 404 | Success |

```
{
  "Name": "Windows 10",
  "Description": "Windows 10 PRO LICENCE KEY",
  "PriceMonthly": "15",
  "Website": "microsoft.com",
  "Instructions": "1. Download 2. Buy"
}
```


```
201
{
    "id": 3,
    "name": "Windows 10",
    "description": "Windows 10 PRO LICENCE KEY",
    "priceMonthly": 15,
    "website": "microsoft.com",
    "instructions": "1. Download 2. Buy"
}
```



### /api/shops/{shopId}/softwares/{softwareId}

#### GET
##### Parameters

| Name | Located in | Description | Required | Schema |
| ---- | ---------- | ----------- | -------- | ---- |
| softwareId | path |  | Yes | integer |
| shopId | path |  | Yes | integer |

##### Responses

| Code | Description |
| ---- | ----------- |
| 200 | Success |


```
GET shops/4/softwares/3 

200
{
    "id": 3,
    "name": "Windows 10",
    "description": "Windows 10 PRO LICENCE KEY",
    "priceMonthly": 15,
    "website": "microsoft.com",
    "instructions": "1. Download 2. Buy"
}
```

#### PUT
##### Parameters

| Name | Located in | Description | Required | Schema |
| ---- | ---------- | ----------- | -------- | ---- |
| softwareId | path |  | Yes | integer |
| shopId | path |  | Yes | integer |

##### Responses

| Code | Description |
| ---- | ----------- |
| 200 | Success |

```
{
  "Name": "Windows 10 Updated",
  "Description": "Windows 10 PRO LICENCE KEY",
  "PriceMonthly": 15,
  "Website": "microsoft.com",
  "Instructions": "1. Download 2. Buy"
}
```

```
{
    "id": 3,
    "name": "Windows 10 Updated",
    "description": "Windows 10 PRO LICENCE KEY",
    "priceMonthly": 15,
    "website": "microsoft.com",
    "instructions": "1. Download 2. Buy"
}
```

#### DELETE
##### Parameters

| Name | Located in | Description | Required | Schema |
| ---- | ---------- | ----------- | -------- | ---- |
| softwareId | path |  | Yes | integer |
| shopId | path |  | Yes | integer |

##### Responses

| Code | Description |
| ---- | ----------- |
| 204 | No Content |


shops/4/softwares/3

```
204
```


### /api/shops/{shopId}/softwares/{softwareId}/subscriptions/{subscriptionId}

#### GET
##### Parameters

| Name | Located in | Description | Required | Schema |
| ---- | ---------- | ----------- | -------- | ---- |
| subscriptionId | path |  | Yes | integer |
| softwareId | path |  | Yes | integer |
| shopId | path |  | Yes | integer |

##### Responses

| Code | Description |
| ---- | ----------- |
| 200 | Success |

#### PUT
##### Parameters

| Name | Located in | Description | Required | Schema |
| ---- | ---------- | ----------- | -------- | ---- |
| subscriptionId | path |  | Yes | integer |
| softwareId | path |  | Yes | integer |
| shopId | path |  | Yes | integer |

##### Responses

| Code | Description |
| ---- | ----------- |
| 200 | Success |

```
shops/4/softwares/4/subscriptions/3

{
  "TermInMonths": "6",
  "IsCanceled": true,
  "Id": 3
}
```


```
{
    "termInMonths":6,
    "isCanceled":true
}
```



#### DELETE
##### Parameters

| Name | Located in | Description | Required | Schema |
| ---- | ---------- | ----------- | -------- | ---- |
| subscriptionId | path |  | Yes | integer |
| shopId | path |  | Yes | integer |
| softwareId | path |  | Yes | integer |

##### Responses

| Code | Description |
| ---- | ----------- |
| 200 | Success |


```
shops/4/softwares/4/subscriptions/3
```


```
204 No Content
```

### /api/shops/{shopId}/softwares/{softwareId}/subscriptions

#### POST
##### Parameters

| Name | Located in | Description | Required | Schema |
| ---- | ---------- | ----------- | -------- | ---- |
| softwareId | path |  | Yes | integer |
| shopId | path |  | Yes | integer |



shops/4/softwares/4/subscriptions

```
{
  "TermInMonths": 1
}
```


```
{
    "id": 3,
    "termInMonths": 1,
    "start": "2023-12-06T18:13:13.3934922Z",
    "end": "2024-01-06T18:13:13.3936091Z",
    "totalPrice": 15,
    "isCanceled": false
}
```

##### Responses

| Code | Description |
| ---- | ----------- |
| 200 | Success |

