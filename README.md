## Introduction
The purpose of this project is to provide a middleware for asp.net core for building an app
relying on Command and Query Resonsibility Segregation :
  - GET = Query
  - POST = Command
  - DELETE/PUT/HEAD doesn't exists here
  - Use of Accept for for determining of the Query resul will be displayed : razor/json/xml/txt ...
  - Use of Content-Type for binding Command and Query
  - Ability to add Filter for filling the Commands and Queries with custom logic (for instance change query based on the user's authorizations)
  - Commands shouldn't return result : we have to manage the command execution state and let the use when the command is over and its outcome
  - Validation layer
  - Every part of the middleware could be overriden via interface implementation and dependency injection
  - Complex url routing for Queries, Simple url routing for Commands (can be changed)


