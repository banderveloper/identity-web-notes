FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["IdentityWebNotes.WebApi/IdentityWebNotes.WebApi.csproj", "IdentityWebNotes.WebApi/"]
RUN dotnet restore "IdentityWebNotes.WebApi/IdentityWebNotes.WebApi.csproj"
COPY . .
WORKDIR "/src/IdentityWebNotes.WebApi"
RUN dotnet build "IdentityWebNotes.WebApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "IdentityWebNotes.WebApi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "IdentityWebNotes.WebApi.dll"]
