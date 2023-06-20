FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 16377
EXPOSE 4311

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src

# Install protobuf tools
RUN apt-get update && \
    apt-get install -y protobuf-compiler

# Install Google.Protobuf NuGet package
RUN dotnet add package Google.Protobuf --version 3.18.1

# Copy and compile the Protos directory
COPY Protos /src/Protos
RUN protoc -I=/src/Protos --csharp_out=/src/Protos /src/Protos/*.proto

COPY . .
WORKDIR "/src/."
RUN dotnet build "ReservationService.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ReservationService.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ReservationService.dll"]

# Add gRPC related lines
ENV ASPNETCORE_URLS=http://*:16377;https://*:4311
EXPOSE 16377
EXPOSE 4311