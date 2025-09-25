# DocumentGenerator
- ФИО: Иванов Тимофей Михайлович @Karo4a
- Группа: ИП-23-3
- Задание AspNetCore WEB API "Акт приема передачи товаров"

## Акт приёма передачи товаров
![Акт приёма передачи товаров](document.jpg "Документ")

## Схема базы данных
```mermaid
erDiagram
  Party {
    Guid Id
    String Name
    String Job
    String TaxId
   }
  Product {
    Guid Id
    String Name
    Decimal Cost
   }
  DocumentProduct {
    Guid Id
    Guid ProductId
    Guid DocumentId
    Int Quantity
    Decimal Cost
   }
  Document {
    Guid Id
    String DocumentNumber
    String ContractNumber
    DateOnly Date
    Guid SellerId
    Guid BuyerId
   }
  Document ||--o{ Party : "signs by"
  DocumentProduct }o--|| Product : "references to"
  Document ||--o{  DocumentProduct : "contains"
```

## Реализация API
### CRUD товаров
|verb|url|description|request|response|codes|
|-|-|-|-|-|-|
|GET|api/products/|Получает список всех товаров| |`ProductApiModel[]`|200 OK|
|GET|api/products/{id}|Получает товар с идентификатором id| fromRoute: id|`ProductApiModel`|200 OK<br/>404 Not Found|
|POST|api/products/|Добавляет новый товар|fromBody: `ProductRequestApiModel`|`ProductApiModel`|200 OK<br/>409 Conflict<br/>422 Unprocessable Entity|
|PUT|api/products/{id}|Редактирует товар с идентификатором id| fromRoute: id <br/>fromBody: `ProductRequestApiModel`|`ProductApiModel`|200 OK<br/>404 Not Found<br/>409 Conflict<br/>422 Unprocessable Entity|
|DELETE|api/products/{id}|Удаляет товар с идентификатором id| fromRoute: id| |200 OK<br/>404 Not Found|
```javascript
// ProductApiModel
{
  id: "3fa85f64-5717-4562-b3fc-2c963f66afa6",
  name: "Товар 1",
  cost: 1
}
```
```javascript
// ProductRequestApiModel
{
  name: "Товар 1",
  cost: 1
}
```

### CRUD стороны акта
|verb|url|description|request|response|codes|
|-|-|-|-|-|-|
|GET|api/party/|Получает список всех сторон актов| |`PartyApiModel[]`|200 OK|
|GET|api/party/{id}|Получает сторону акта с идентификатором id| fromRoute: id|`PartyApiModel`|200 OK<br/>404 Not Found|
|POST|api/party/|Добавляет новую сторону акта| fromBody: `PartyRequestApiModel`|`PartyApiModel`|200 OK<br/>409 Conflict<br/>422 Unprocessable Entity|
|PUT|api/party/{id}|Редактирует сторону акта с идентификатором id| fromRoute: id <br/>fromBody: `PartyRequestApiModel`|`PartyApiModel`|200 OK<br/>404 Not Found<br/>409 Conflict<br/>422 Unprocessable Entity|
|DELETE|api/party/{id}|Удаляет сторону акта с идентификатором id| fromRoute: id| |200 OK<br/>404 Not Found|
```javascript
// PartyApiModel
{
  id: "3fa85f64-5717-4562-b3fc-2c963f66afa6",
  name: "ФИО стороны акта",
  job: "Работа стороны акта",
  taxId: "1234567891"
}
```
```javascript
// PartyRequestApiModel
{
  name: "ФИО стороны акта",
  job: "Работа стороны акта",
  taxId: "1234567891"
}
```

### CRUD документа
|verb|url|description|request|response|codes|
|-|-|-|-|-|-|
|GET|api/document/{id}/export|Экспортирует документ в формате Excel с идентификатором id|fromRoute: id|`file.xlsx`|200 OK<br/>404 Not Found|
|GET|api/document/|Получает список всех документов| |`DocumentApiModel[]`|200 OK|
|GET|api/document/{id}|Получает документ с идентификатором id| fromRoute: id|`DocumentApiModel`|200 OK<br/>404 Not Found|
|POST|api/document/|Добавляет новый документ| fromBody: `DocumentRequestApiModel`|`DocumentApiModel`|200 OK<br/>404 Not Found<br/>409 Conflict|
|PUT|api/document/{id}|Редактирует документ с идентификатором id| fromRoute: id <br/>fromBody: `DocumentRequestApiModel`|`DocumentApiModel`|200 OK<br/>404 Not Found<br/>409 Conflict<br/>422 Unprocessable Entity|
|DELETE|api/document/{id}|Удаляет документ с идентификатором id| fromRoute: id| |200 OK<br/>404 Not Found|
```javascript
// DocumentProductApiModel
{
  id: "3fa85f64-5717-4562-b3fc-2c963f66afa6",
  product: {
    id: "3fa85f64-5717-4562-b3fc-2c963f66afa6",
    name: "Товар 1",
    cost: 1
  },
  quantity: 1,
  cost: 1
}
```
```javascript
// DocumentProductRequestApiModel
{
  productId: "3fa85f64-5717-4562-b3fc-2c963f66afa6",
  quantity: 1,
  cost: 1
}
```
```javascript
// DocumentApiModel
{
  id: "3fa85f64-5717-4562-b3fc-2c963f66afa6",
  documentNumber: "1",
  contractNumber: "1",
  date: "2025-09-11",
  seller: {
    id: "3fa85f64-5717-4562-b3fc-2c963f66afa6",
    name: "ФИО продавца",
    job: "Работа продавца",
    taxId: "1234567891"
  },
  buyer: {
    id: "3fa85f64-5717-4562-b3fc-2c963f66afa6",
    name: "ФИО покупателя",
    job: "Работа покупателя",
    taxId: "1234567891"
  },
  products: [
    {
      id: "3fa85f64-5717-4562-b3fc-2c963f66afa6",
      product: {
        id: "3fa85f64-5717-4562-b3fc-2c963f66afa6",
        name: "Товар 1",
        cost: 1
      },
      quantity: 1,
      cost: 1
    }
  ]
}
```
```javascript
// DocumentRequestApiModel
{
  documentNumber: "1",
  contractNumber: "1",
  date: "2025-09-11",
  sellerId: "3fa85f64-5717-4562-b3fc-2c963f66afa6",
  buyerId: "3fa85f64-5717-4562-b3fc-2c963f66afa6",
  products: [
    {
      productId: "3fa85f64-5717-4562-b3fc-2c963f66afa6",
      quantity: 1,
      cost: 1
    }
  ]
}
```