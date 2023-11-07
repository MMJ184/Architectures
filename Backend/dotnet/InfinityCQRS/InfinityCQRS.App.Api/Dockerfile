#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

#Depending on the operating system of the host machines(s) that will build or run the containers, the image specified in the FROM statement may need to be changed.
#For more information, please see https://aka.ms/containercompat

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["InfinityCQRS.App.Api/InfinityCQRS.App.Api.csproj", "InfinityCQRS.App.Api/"]
COPY ["InfinityCQRS.App.CommandResults/InfinityCQRS.App.CommandResults.csproj", "InfinityCQRS.App.CommandResults/"]
COPY ["InfinityCQRS.App.Handlers/InfinityCQRS.App.Handlers.csproj", "InfinityCQRS.App.Handlers/"]
COPY ["InfinityCQRS.App.Queries/InfinityCQRS.App.Queries.csproj", "InfinityCQRS.App.Queries/"]
COPY ["InfinityCQRS.App.Repository/InfinityCQRS.App.Repository.csproj", "InfinityCQRS.App.Repository/"]
COPY ["InfinityCQRS.App.Commands/InfinityCQRS.App.Commands.csproj", "InfinityCQRS.App.Commands/"]
COPY ["InfinityCQRS.App.Database/InfinityCQRS.App.Database.csproj", "InfinityCQRS.App.Database/"]
COPY ["InfinityCQRS.App.Models/InfinityCQRS.App.Models.csproj", "InfinityCQRS.App.Models/"]
COPY ["InfinityCQRS.App.Validators/InfinityCQRS.App.Validators.csproj", "InfinityCQRS.App.Validators/"]
RUN dotnet restore "InfinityCQRS.App.Api/InfinityCQRS.App.Api.csproj"
COPY . .
WORKDIR "/src/InfinityCQRS.App.Api"
RUN dotnet build "InfinityCQRS.App.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "InfinityCQRS.App.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "InfinityCQRS.App.Api.dll"]