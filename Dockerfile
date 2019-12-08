FROM mcr.microsoft.com/dotnet/core/sdk:3.0 AS build
WORKDIR /app
EXPOSE 80
EXPOSE 443
COPY *.csproj .
RUN dotnet restore -c Release

# copy everything else and build app
COPY . .
RUN dotnet publish -c Release -o out


FROM mcr.microsoft.com/dotnet/core/aspnet:3.0 AS runtime
COPY --from=build /app/out ./
ENTRYPOINT ["dotnet", "VegaIoTApi.dll"]