FROM microsoft/dotnet:1.0.0-preview2-sdk

COPY . /app

WORKDIR /app

RUN ["dotnet", "restore"]

RUN ["dotnet", "build"]

RUN ["dotnet", "ef", "database","update", "--context=ApplicationUserContext"]
RUN ["dotnet", "ef", "database","update", "--context=PassContext"]
RUN ["dotnet", "ef", "database","update", "--context=PassTypeUserContext"]
RUN ["dotnet", "ef", "database","update", "--context=ResortContext"]
RUN ["dotnet", "ef", "database","update", "--context=SkiCardContext"]

EXPOSE 5000/tcp

ENV Authentication:Facebook:AppSecret FacebookAppSecret
ENV Authentication:Facebook:AppId FacebookAppId
ENV Authentication:Twitter:ConsumerSecret TwitterSecret
ENV Authentication:Twitter:ConsumerKey TwitterKey
ENV ASPNETCORE_ENVIRONMENT Development

ENTRYPOINT ["dotnet", "run", "--server.urls", "http://0.0.0.0:5000"]