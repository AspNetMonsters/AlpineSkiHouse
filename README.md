# Alpine Ski House

This is the example project for the Microsoft Press book ASP.Net Core Application Development: Building an Application in Four Sprints. You can buy the book off Amazon at https://www.amazon.ca/ASP-NET-Core-Application-Development-application/dp/1509304061/ref=sr_1_3?ie=UTF8&qid=1478979203&sr=8-3&keywords=asp.net+core.

## Setup

1. Download the code
2. Open the solution file
3. Add a user secrets like

  ```
  {
    "Authentication": {
      "Facebook": {
        "AppId": "some app id",
        "AppSecret": "some secret"
      },
      "Twitter": {
        "ConsumerKey": "some consumer key",
        "ConsumerSecret": "some consumer secret"
      }
    }
  }
  ```
4. Restore packages. 
  ```
    yarn install
    bower install
    jspm install
  ```
  _Note:_ This step requires that yarn, bower and jpsm are all installed globally. This is typically done using the node package manager (npm) that ships as part of [node](https://nodejs.org/en/).

  ```
    npm install yarn -g
    npm install bower -g
    npm install jspm -g
  ```

5. Run gulp
  ```
  gulp
    ```
  _Note:_ This step requires that gulp is installed globally. 

  ```
     npm install gulp -g
  ```

6. Run the solution in visual studio

## Creating and updating the database
From the src/AlpineSkiHouse.Web folder, run the `dotnet ef database update command` for each DbContext.

```
dotnet ef database update --context AlpineSkiHouse.Data.ApplicationUserContext
dotnet ef database update --context AlpineSkiHouse.Data.ResortContext
dotnet ef database update --context AlpineSkiHouse.Data.PasTypeContext
dotnet ef database update --context AlpineSkiHouse.Data.SkiCardContext
dotnet ef database update --context AlpineSkiHouse.Data.PassContext
```



