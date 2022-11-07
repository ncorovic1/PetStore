## Pet Store

**Host**: .NET Core Web Assembly Hosted  
**Frontend**: Blazor  
**Backend**: .NET Core REST API  

Application for selling (ordering) pet toys with separate administrator access for toys and orders management. 

<ins>Frontend contains three pages:</ins>
+ Index - page where you can see current user token and navigate to swagger page
+ Toys - page where you can see all the toys available in the system
+ Swagger - page where you can interact directly with the API.  

<ins>Backend contains two controllers:</ins>
+ Toy
    + Create (Admin authorized)
    + Search (Authenticated)
    + Patch (Admin authorized)
    + Delete (Admin authorized)
+ Order
    + Create (User authorized)
    + Search (Admin authorized)  

<ins>Authorization is implemented via integration with Auth0 organization and consists of two users:</ins>
+ User
    + Username: user@user.user
    + Password: 1DVATri!
+ Admin
    + Username: admin@admin.admin
    + Password: 1DVATri!  

<ins>Backend is configured to use:</ins>
+ NLog - library built on top of .NET logger to support logging to files
+ Mediatr - library based on mediator (behavioral dessign pattern) which makes the controllers implementation agnostic and thin
+ Fluent validation - library used for validation (configured with mediatr pipeline)
+ Swagger - library for API management
+ Automapper - library for model mapping
+ EF core - library for database configuration implemented as volatile in-memory persistence
+ Auth0 - library that handles integration with Auth0 organization which provides simple setup for OAuth2 security integration
+ XUnit and Moq - librarires for unit test management

## Setup
No setup is needed. However due to integration with Auth0, application needs to work via SSL on port 7239.
Frontend profile for launch is PetStore, and backend profile is PetStore.Server (launchsettings.json).