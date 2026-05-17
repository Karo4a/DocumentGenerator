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
|GET|api/Product/|Получает список всех товаров| |`ProductApiModel[]`|200 OK|
|GET|api/Product/{id}|Получает товар с идентификатором id| fromRoute: id|`ProductApiModel`|200 OK<br/>404 Not Found|
|POST|api/Product/|Добавляет новый товар|fromBody: `ProductRequestApiModel`|`ProductApiModel`|200 OK<br/>409 Conflict<br/>422 Unprocessable Entity|
|PUT|api/Product/{id}|Редактирует товар с идентификатором id| fromRoute: id <br/>fromBody: `ProductRequestApiModel`|`ProductApiModel`|200 OK<br/>404 Not Found<br/>409 Conflict<br/>422 Unprocessable Entity|
|DELETE|api/Product/{id}|Удаляет товар с идентификатором id| fromRoute: id| |200 OK<br/>404 Not Found|
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
|GET|api/Party/|Получает список всех сторон актов| |`PartyApiModel[]`|200 OK|
|GET|api/Party/{id}|Получает сторону акта с идентификатором id| fromRoute: id|`PartyApiModel`|200 OK<br/>404 Not Found|
|POST|api/Party/|Добавляет новую сторону акта| fromBody: `PartyRequestApiModel`|`PartyApiModel`|200 OK<br/>409 Conflict<br/>422 Unprocessable Entity|
|PUT|api/Party/{id}|Редактирует сторону акта с идентификатором id| fromRoute: id <br/>fromBody: `PartyRequestApiModel`|`PartyApiModel`|200 OK<br/>404 Not Found<br/>409 Conflict<br/>422 Unprocessable Entity|
|DELETE|api/Party/{id}|Удаляет сторону акта с идентификатором id| fromRoute: id| |200 OK<br/>404 Not Found|
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
|GET|api/Document/{id}/export|Экспортирует документ в формате Excel с идентификатором id|fromRoute: id|`file.xlsx`|200 OK<br/>404 Not Found|
|GET|api/Document/|Получает список всех документов| |`DocumentApiModel[]`|200 OK|
|GET|api/Document/{id}|Получает документ с идентификатором id| fromRoute: id|`DocumentApiModel`|200 OK<br/>404 Not Found|
|POST|api/Document/|Добавляет новый документ| fromBody: `DocumentRequestApiModel`|`DocumentApiModel`|200 OK<br/>404 Not Found<br/>409 Conflict|
|PUT|api/Document/{id}|Редактирует документ с идентификатором id| fromRoute: id <br/>fromBody: `DocumentRequestApiModel`|`DocumentApiModel`|200 OK<br/>404 Not Found<br/>409 Conflict<br/>422 Unprocessable Entity|
|DELETE|api/Document/{id}|Удаляет документ с идентификатором id| fromRoute: id| |200 OK<br/>404 Not Found|
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