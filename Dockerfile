# Stage 1: Build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy csproj and restore as distinct layers
COPY ["AlternativeMedicine.App/AlternativeMedicine.App.csproj", "AlternativeMedicine.App/"]
RUN dotnet restore "AlternativeMedicine.App/AlternativeMedicine.App.csproj"

# Copy the rest of the code
COPY . .
WORKDIR "/src/AlternativeMedicine.App"
RUN dotnet publish -c Release -o /app/publish

# Stage 2: Runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=build /app/publish .

# Railway-specific configuration
ENV ASPNETCORE_URLS=http://+:${PORT}
ENV PORT=8080
EXPOSE $PORT

ENTRYPOINT ["dotnet", "AlternativeMedicine.App.dll"]