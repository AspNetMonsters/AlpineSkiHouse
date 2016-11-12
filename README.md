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
4. Restore packages
  ```
    yarn install
    bower install
    jspm install
  ```

5. Run gulp
```
gulp
```
6. Run the solution in visual studio
