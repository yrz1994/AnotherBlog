#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0-buster-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0-buster-slim AS build
WORKDIR /src
COPY ["AnotherBlog.CommentAPI/AnotherBlog.CommentAPI.csproj", "AnotherBlog.CommentAPI/"]
COPY ["AnotherBlog.Infra.ConsulRegister/AnotherBlog.Infra.ConsulRegister.csproj", "AnotherBlog.Infra.ConsulRegister/"]
RUN dotnet restore "AnotherBlog.CommentAPI/AnotherBlog.CommentAPI.csproj"
COPY . .
WORKDIR "/src/AnotherBlog.CommentAPI"
RUN dotnet build "AnotherBlog.CommentAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "AnotherBlog.CommentAPI.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "AnotherBlog.CommentAPI.dll"]