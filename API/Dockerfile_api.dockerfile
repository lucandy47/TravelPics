#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
ARG Environment=Development
ENV DOTNET_ENVIRONMENT=${Environment}
ARG AppConfigConnectionString="Endpoint=https://travelpicsappconfiguration.azconfig.io;Id=/9U6-l0-s0:oHtG9HIFpsW3rua+iYCD;Secret=KcpyeMOfEhyp1/Uds6MXxzBvWl3Yj2SWPTtzvw7LRw8="
ENV ConnectionStrings__AppConfig=${AppConfigConnectionString}
EXPOSE 80
EXPOSE 443


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
COPY ["TravelPics.Dashboard.API/TravelPics.Dashboard.API/TravelPics.Dashboard.API.csproj", "TravelPics.Dashboard.API/"]
COPY ["TravelPics.Dashboard.API/TravelPics.Domains/TravelPics.Domains.csproj", "TravelPics.Domains/"]
COPY ["TravelPics.Dashboard.API/TravelPics.Abstractions/TravelPics.Abstractions.csproj", "TravelPics.Abstractions/"]
COPY ["TravelPics.Dashboard.API/TravelPics.Documents/TravelPics.Documents.csproj", "TravelPics.Documents/"]
COPY ["TravelPics.Dashboard.API/TravelPics.Notifications.Core/TravelPics.Notifications.Core.csproj", "TravelPics.Notifications.Core/"]
COPY ["TravelPics.Dashboard.API/TravelPics.Users/TravelPics.Users.csproj", "TravelPics.Users/"]
COPY ["TravelPics.Dashboard.API/TravelPics.LookupItems/TravelPics.LookupItems.csproj", "TravelPics.LookupItems/"]
COPY ["TravelPics.Dashboard.API/TravelPics.Posts/TravelPics.Posts.csproj", "TravelPics.Posts/"]
COPY ["TravelPics.Dashboard.API/TravelPics.Security/TravelPics.Security.csproj", "TravelPics.Security/"]
COPY ["TravelPics.Dashboard.API/TravelPics.Location/TravelPics.Locations.csproj", "TravelPics.Locations/"]
COPY ["TravelPics.MessageClient/TravelPics.MessageClient.csproj", "TravelPics.MessageClient/"]

RUN dotnet restore "TravelPics.Dashboard.API/TravelPics.Dashboard.API.csproj"
COPY . .
WORKDIR "/src/TravelPics.Dashboard.API"
RUN dotnet build "TravelPics.Dashboard.API/TravelPics.Dashboard.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "TravelPics.Dashboard.API/TravelPics.Dashboard.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TravelPics.Dashboard.API.dll"]