
Overview: This is an ASP.NET Core API utilizing Entity Framework with
          REACT as the frontend javascript framework to consume the API.
          The database is In Memory and is seeded on startup of the app.
          
Prerequisites:
    - .NET Core 2.1
    - npm for installing node modules with react

Building the project:
    - The project uses an In Memory database and Entity Framework
      so you should be able to load it into visual studio and run it 
      on IIS and the react app should start after npm install runs

API:
    - api/People?searchString=<searchString>
      - Method: GET
      - Payload: None
      - Response: JSON of all people matching the search string
      - Giving a blank string for <searchString> returns all people
    
    - api/People/<id>
      - Method: GET
      - Body: None
      - Response: {
          "Id": "<id>",
          "FirstName": "<first name>",
          "LastName": "<last name>",
          "Age": "<age>",
          "StreetAddress": "<street address>",
          "City": "<city>",
          "State": "<state>",
          "ZipCode": "<zip code>",
          "Photograph": "<base64 encoded binary string>"
      }

    - api/People/<id>
      - Method: PUT 
      - Body: {
          "Id": "<id>",
          "FirstName": "<first name>",
          "LastName": "<last name>",
          "Age": "<age>",
          "StreetAddress": "<street address>",
          "City": "<city>",
          "State": "<state>",
          "ZipCode": "<zip code>",
          "Photograph": "<base64 encoded binary string>"
        }
      - Response: {
          "Id": "<id>",
          "FirstName": "<first name>",
          "LastName": "<last name>",
          "Age": "<age>",
          "StreetAddress": "<street address>",
          "City": "<city>",
          "State": "<state>",
          "ZipCode": "<zip code>",
          "Photograph": "<base64 encoded binary string>"
        }
    
    - api/People
      - Method: POST
      - Body: {
          "FirstName": "<first name>",
          "LastName": "<last name>",
          "Age": "<age>",
          "StreetAddress": "<street address>",
          "City": "<city>",
          "State": "<state>",
          "ZipCode": "<zip code>",
          "Photograph": "<base64 encoded binary string>"
        }
      - Response: {
          "Id": "<id>",
          "FirstName": "<first name>",
          "LastName": "<last name>",
          "Age": "<age>",
          "StreetAddress": "<street address>",
          "City": "<city>",
          "State": "<state>",
          "ZipCode": "<zip code>",
          "Photograph": "<base64 encoded binary string>"
        }

    - api/People/<id>
      - Method: DELETE
      - Body: None
      - Response: "<person> has been deleted"
    

