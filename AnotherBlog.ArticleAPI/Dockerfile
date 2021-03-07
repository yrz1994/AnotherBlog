#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0-buster-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0-buster-slim AS build
WORKDIR /src
COPY ["AnotherBlog.ArticleAPI/AnotherBlog.ArticleAPI.csproj", "AnotherBlog.ArticleAPI/"]
RUN dotnet restore "AnotherBlog.ArticleAPI/AnotherBlog.ArticleAPI.csproj"
COPY . .
WORKDIR "/src/AnotherBlog.ArticleAPI"
RUN dotnet build "AnotherBlog.ArticleAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "AnotherBlog.ArticleAPI.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "AnotherBlog.ArticleAPI.dll"]
CMD ["--scheme http --ip localhost --port 5002"]