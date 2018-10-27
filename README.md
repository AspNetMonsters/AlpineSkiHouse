# Alpine Ski House

This is the example project for the Microsoft Press book ASP.Net Core Application Development: Building an Application in Four Sprints. You can [buy the book](https://www.amazon.ca/ASP-NET-Core-Application-Development-application/dp/1509304061/ref=sr_1_3?ie=UTF8&qid=1478979203&sr=8-3&keywords=asp.net+core.
) off Amazon.

## Setup

1. Download the code.
1. Open the solution file.
1. Add a user secrets like :

    ```json
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

1. Run commands to restore JavaScript packages in the `src/AlpineSkiHouse.Web` directory :

    ```bash
    yarn install
    bower install
    jspm install
    ```

    _Note:_ This step requires that yarn, bower and jpsm are all installed globally. This is typically done using the node package manager (npm) that ships as part of [node](https://nodejs.org/en/).

    ```bash
    npm install yarn -g
    npm install bower -g
    npm install jspm -g
    ```

    _Note the second:_ Some people have reported issues running against 6.x.x versions of node. The image minification plugin has an upstream dependency on a 4.x.x series node. To solve this you can install a 4.x.x version of node (I'd recommend using https://github.com/creationix/nvm to manage node versions) **OR** you can strip out the image minificaiton from the gulp file and `packages.json` file. The bug can be found at https://github.com/AspNetMonsters/AlpineSkiHouse/issues/101

1. Run gulp :
    ```bash
    gulp
    ```

    _Note:_ This step requires that gulp is installed globally: `npm install gulp -g`

1. Run the solution in Visual Studio.

## Creating and updating the database

From the src/AlpineSkiHouse.Web folder, run the `dotnet ef database update command` for each DbContext.

```bash
dotnet ef database update --context AlpineSkiHouse.Data.ApplicationUserContext
dotnet ef database update --context AlpineSkiHouse.Data.ResortContext
dotnet ef database update --context AlpineSkiHouse.Data.PassTypeContext
dotnet ef database update --context AlpineSkiHouse.Data.SkiCardContext
dotnet ef database update --context AlpineSkiHouse.Data.PassContext
```

## Image Attribution

Images on this project found by Creative Common search on [Bing Image Search](http://www.bing.com/images/search?pq=mountain+ski+resort&sc=0-16&sp=-1&sk=&q=mountain+ski+resort&qft=+filterui:licenseType-Any+filterui:imagesize-large&FORM=R5IR3) and used under [CC BY The ASP.NET Monsters](https://creativecommons.org/licenses/by/2.0/).

Artwork on this project is a derivative of images found by Creative Common search on [Bing Image Search](http://www.bing.com/images/search?pq=mountain+ski+resort&sc=0-16&sp=-1&sk=&q=mountain+ski+resort&qft=+filterui:licenseType-Any+filterui:imagesize-large&FORM=R5IR3), and licensed under [CC BY The ASP.NET Monsters](https://creativecommons.org/licenses/by/2.0/).
