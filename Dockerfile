FROM microsoft/dotnet:sdk
WORKDIR /app

COPY . ./
COPY ./AddressAnalyzer.Api/appsettings.json ./
RUN dotnet publish -c Release -o ../publish

EXPOSE 8080
ENV ASPNETCORE_URLS=http://*:8080

CMD bash scripts/docker-start.sh
