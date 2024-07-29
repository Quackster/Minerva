FROM mcr.microsoft.com/dotnet/sdk:8.0 AS builder
WORKDIR /sources
COPY . .

RUN apt update && apt install -y unzip
RUN dotnet publish -c Release --output build Minerva.sln --self-contained false
RUN unzip -qq ./tools/figuredata-shockwave.zip -d build
RUN unzip -qq ./tools/badges.zip -d build

FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /minerva
COPY --from=builder /sources/build .
ENTRYPOINT ["/minerva/Minerva"]
