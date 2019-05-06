FROM microsoft/dotnet:sdk
WORKDIR /app

COPY . ./
COPY ./AddressAnalyzer.Api/appsettings.json ./
RUN dotnet publish -c Release -o ../publish

CMD bash scripts/docker-start.sh
