# GenericCommerceApi


A simple product and basket API with added consumption library



This solution is intended as a means of learning the ins and outs of Web API development within .Net Core while also demonstrating my coding style.



The solution models a catalogue of products within a store along with a set of orders for these products related to a customer.



The Products API is intended for store staff to manage the list of available products as well as their price.



The Orders API allows Customers to manage their orders.



I've also included a client library that interacts with these APIs using HttpClient.



The data for this solution is handled via entity framework and is currently configured to operate in memory and is seeded with some initial data on startup.




**Features**



- DBContext Repository - After reading many heated debates on the subject I opted not to use a generic repository pattern. I ended up siding with the argument that a DBContext already implements the Repository and Unit of Work patterns, with an additional repository being an unnecessary abstraction. The service layer interacts with the DBContext carrying out all CRUD and business operations.



- Data Transformation Objects - When posting a new order, one could potentially post new products and prices to the order list. To remedy this, I created an order DTO class which is a less detailed version of the order class. The DTO is validated and converted to a proper Order object on post.



- Dependency Injection - I have tried to use dependency injection where possible. The DBContext is injected into the service layer which is in turn injected into the controllers.




**Current Issues/Next Steps**
 


- Authorisation - The current elephant in the room is the absence of authorisation on the API methods. It is intended that staff users will only be able to manage products with the rest of the methods being open to customers. User Role based authorisation around methods should remedy this situation.



- Unit Testing - The unit tests are rather meagre in their current form. This is mostly down to my lack of exposure to TDD.



- Line Item Management - An additional controller and services will be added to allow customers to add or remove products from their orders as well managing the quantity of said products.



- A proper database - This should run off a proper SQL Server Database, with the in-memory one used only for unit testing.

- A front end - The solution is entirely server side in its current state.
