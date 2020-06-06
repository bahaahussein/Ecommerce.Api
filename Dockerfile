# NuGet restore
FROM mcr.microsoft.com/dotnet/core/sdk:2.2 AS build
WORKDIR /src
COPY *.sln .
COPY Ecommerce.Api/*.csproj Ecommerce.Api/
COPY ECommerce.Api.UnitTests/*.csproj ECommerce.Api.UnitTests/
COPY Ecommerce.AutoMapper.Profiles/*.csproj Ecommerce.AutoMapper.Profiles/
COPY Ecommerce.Entities/*.csproj Ecommerce.Entities/
COPY ECommerce.Helpers/*.csproj ECommerce.Helpers/
COPY ECommerce.Models/*.csproj ECommerce.Models/
COPY Ecommerce.Repository/*.csproj Ecommerce.Repository/
COPY Ecommerce.Repository.UnitTests/*.csproj Ecommerce.Repository.UnitTests/
COPY Ecommerce.Service/*.csproj Ecommerce.Service/
COPY ECommerce.Service.UnitTests/*.csproj ECommerce.Service.UnitTests/
COPY Ecommerce.SqlData/*.csproj Ecommerce.SqlData/
RUN dotnet restore
COPY . .

# testing
FROM build AS testing
WORKDIR /src/Ecommerce.Api
RUN dotnet build
WORKDIR /src/ECommerce.Api.UnitTests
RUN dotnet test

FROM build AS testing1
WORKDIR /src/Ecommerce.Repository
RUN dotnet build
WORKDIR /src/Ecommerce.Repository.UnitTests
RUN dotnet test

FROM build AS testing2
WORKDIR /src/Ecommerce.Service
RUN dotnet build
WORKDIR /src/ECommerce.Service.UnitTests
RUN dotnet test

# publish
FROM build AS publish
WORKDIR /src/Ecommerce.Api
RUN dotnet publish -c Release -o /src/publish

FROM mcr.microsoft.com/dotnet/core/aspnet:2.2 AS runtime
WORKDIR /app
COPY --from=publish /src/publish .
ENTRYPOINT ["dotnet", "Ecommerce.Api.dll"]
# heroku uses the following
# CMD ASPNETCORE_URLS=http://*:$PORT dotnet Colors.API.dll