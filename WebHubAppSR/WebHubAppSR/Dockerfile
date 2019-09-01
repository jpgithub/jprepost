FROM microsoft/dotnet:2.2-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80

FROM microsoft/dotnet:2.2-sdk AS build
WORKDIR /src
COPY ["WebHubAppSR/WebHubAppSR.csproj", "WebHubAppSR/"]
RUN dotnet restore "WebHubAppSR/WebHubAppSR.csproj"
COPY . .
WORKDIR "/src/WebHubAppSR"
RUN dotnet build "WebHubAppSR.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "WebHubAppSR.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "WebHubAppSR.dll"]