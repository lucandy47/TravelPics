#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/runtime:6.0 AS base
WORKDIR /app
ARG Environment=Development
ENV DOTNET_ENVIRONMENT=${Environment}
ARG AppConfigConnectionString="Endpoint=https://travelpicsappconfiguration.azconfig.io;Id=/9U6-l0-s0:oHtG9HIFpsW3rua+iYCD;Secret=KcpyeMOfEhyp1/Uds6MXxzBvWl3Yj2SWPTtzvw7LRw8="
ENV ConnectionStrings__AppConfig=${AppConfigConnectionString}

# Install the .NET 6.0 runtime
RUN apt-get update \
    && apt-get install -y --no-install-recommends \
        apt-transport-https \
        ca-certificates \
        curl \
        gnupg \
        lsb-release \
    && curl -SL --output /tmp/packages-microsoft-prod.deb https://packages.microsoft.com/config/debian/11/packages-microsoft-prod.deb \
    && dpkg -i /tmp/packages-microsoft-prod.deb \
    && apt-get update \
    && apt-get install -y --no-install-recommends \
        aspnetcore-runtime-6.0 \
    && rm -rf /var/lib/apt/lists/*

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["TravelPics.Notifications/TravelPics.Notifications/TravelPics.Notifications.csproj", "TravelPics.Notifications/"]
COPY ["TravelPics.Notifications/TravelPics.Notifications.Processors/TravelPics.Notifications.Processors.csproj", "TravelPics.Notifications.Processors/"]
COPY ["TravelPics.Dashboard.API/TravelPics.Domains/TravelPics.Domains.csproj", "TravelPics.Domains/"]
COPY ["TravelPics.Dashboard.API/TravelPics.Abstractions/TravelPics.Abstractions.csproj", "TravelPics.Abstractions/"]
COPY ["TravelPics.Dashboard.API/TravelPics.Documents/TravelPics.Documents.csproj", "TravelPics.Documents/"]
COPY ["TravelPics.Dashboard.API/TravelPics.Notifications.Core/TravelPics.Notifications.Core.csproj", "TravelPics.Notifications.Core/"]
COPY ["TravelPics.Dashboard.API/TravelPics.Users/TravelPics.Users.csproj", "TravelPics.Users/"]
COPY ["TravelPics.MessageClient/TravelPics.MessageClient.csproj", "TravelPics.MessageClient/"]

RUN dotnet restore "TravelPics.Notifications/TravelPics.Notifications.csproj"
COPY . .
WORKDIR "/src/TravelPics.Notifications"
RUN dotnet build "TravelPics.Notifications/TravelPics.Notifications.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "TravelPics.Notifications/TravelPics.Notifications.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TravelPics.Notifications.dll"]