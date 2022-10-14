#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0-alpine AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0-alpine AS build
WORKDIR /src
COPY ["message.api/message.api.csproj", "message.api/"]
COPY ["message.dto/message.dto.csproj", "message.dto/"]
COPY ["message.handlers/message.handlers.csproj", "message.handlers/"]
COPY ["message.services/message.services.csproj", "message.services/"]
COPY ["message.models/message.models.csproj", "message.models/"]
COPY ["chat.shared/chat.shared.csproj", "chat.shared/"]
COPY ["message.utils/message.utils.csproj", "message.utils/"]
COPY ["message.profile/message.profile.csproj", "message.profile/"]
RUN dotnet restore "message.api/message.api.csproj"
COPY . .
WORKDIR "/src/message.api"
RUN dotnet build "message.api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "message.api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "message.api.dll"]