# Suppliers solution exercise
#### Time limit: 1 hour

The current solution contains a controller with basic CRUD actions over an in-memory Supplier entity.

Please complete the following actions:

1.	Right now the application doesn't load correctly due to runtime issue. 
	Please fix the issue.
2.	The GetSupplier and GetSuppliers endpoints only return the supplier information without including emails or phones. 
	Please make the appropriate modifications to include both in the response of each method.
3.	When adding new suppliers, no validation is performed. 
	Please add following validations:
	- Activation date must be tomorrow or later.
	- Email address has to be a valid format.
	- Phone number has to be numeric and with a max length of 10.
4.	No unit tests have been created for the solution. 
	Please add unit tests for the following classes: SupplierController, SupplierService and SupplierExtensions. 
	Bear in mind the code coverage should be as high as possible.



