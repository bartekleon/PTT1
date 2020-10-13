# Goal
- Implement selected business process, namely: library, shop, warehouse, etc. using layered application - only logic and data is required.
- Define data layer using the .NET collection in an example application to:
  - create a catalog, e.g. product catalog, list of clients
  - register events, e.g. a list of invoices
  - maintain the state description, e.g. library status, inventory status
- Define logic layer to implement all relevant operation
- Define API for the library (public declarations)
- Use the unit test to prove the correctness of the proposed solution

# Description

### `Data` layer
The data layer should be implemented in a library project. This layer must contain all the classes representing the data relevant to the selected process. The class's name should be meaningful, i.e. represent the contained data. Use references to model the data relationship.

The following information should be represented:

- Users: a collection of all actors relevant to the implemented business process (e.g.: readers, customers, suppliers, etc)
- Catalog: a dictionary of the goods descriptions (e.g.: books, good )
- Process state: description of the current process state (e.g: the current content of the shop, library, etc.)
- Events: description of all events contributing to the process state change in time.

### `Logic` layer
This layer should provide all functionality relevant to the selected business process.

### `Unit` Tests
Use the Unit Test to prove the correctness of the proposed solution During the preparation phase the test must initialize the data being processed. Dependency Injection is very helpful to implement this functionality.

### Notes
1. There should be provided at least two data generation method.
2. Logic and Data must be implemented in the library.
3. There is no need to implement the presentation layer, but if implemented must be in a separate executable project.