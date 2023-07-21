FROM mcr.microsoft.com/dotnet/sdk:7.0 as build-env
WORKDIR /APP
COPY . ./

RUN dotnet restore
RUN dotnet public -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:7.0
WORKDIR /App
COPY --from=buid-env /App/out . 
ENTRYPOINT ["dotnet", "todo-api.dll"]