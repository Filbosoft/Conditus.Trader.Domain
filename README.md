# Conditus Trader Domain
A NuGet package containing the database entities, view models and enums used in the Conditus Trader backend.  

The intention of this package is to have a common package each element/service/function of the backend can include and always be aligned.

## Getting the package
**This package isn't currently hosted.**  

To implement this package:
1. Pull this repository
2. Follow the steps in [Development.md](Development.md#How-to-create-new-version), minus the steps altering the code and updating the version nr.

## General attribute explanations
### Prefixed entity attributes?
**Why is some of the entity attributes prefixed with the entity name?**  

When querying DynamoDB there's some keywords such as Name, Type and Status, meaning that DynamoDB will misinterpret the intention of the query if they are used.  
To avoid that, the attributes with keyword names are prefixed with the entity name.

### OwnerId
OwnerId attributes references the userId of the user who owns the given element/record.  
The value is taken from the Sub claim in the users Id Token.