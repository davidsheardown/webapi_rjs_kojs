# .NET 4.5 WebApi 2 and RequireJS/KnockoutJS frontend
Very much WIP !

Just to provide a starting point of using WebApi 2 (c#) to provide REST services for a loosely coupled frontend.
This project has the following examples so far:

1. Loosely coupled framework (controllers, service layer, ORM)
2. Entity Framework 6.x for the ORM/Data Layer using model/DB first
3. User credentials (password) is hashed/salted before writing to the DB, and has
   a verify password function to enable the hashed/salted password to be checked
4. WebApi filters controller actions to ensure a user is logged in
5. Unity Dependency Container for DI
6. CORS enabled so the WebApi REST project runs completely separate to the frontend
7. RequireJS in the frontend to enable modular building of Javascript
8. KnockoutJS for the viewmodel / JS data binding

Lots more to work on for sure, not to mention adding tests both backend/frontend.  At least a working
framework as a base.

