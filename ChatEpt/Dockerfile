FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["ChatEpt/ChatEpt.csproj", "ChatEpt/"]
RUN dotnet restore "ChatEpt/ChatEpt.csproj"
COPY . .
WORKDIR "/src/ChatEpt"
RUN dotnet build "ChatEpt.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ChatEpt.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ChatEpt.dll"]
