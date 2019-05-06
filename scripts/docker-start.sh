#!/usr/bin/bash

echo "Starting Address Analyzer and Background Services\n"

echo "Starting GeoIP"
dotnet ./publish/AddressAnalyzer.GeoIp.Api.dll &
echo "Starting Ping"
dotnet ./publish/AddressAnalyzer.Ping.Api.dll &
echo "Starting RDAP"
dotnet ./publish/AddressAnalyzer.Rdap.Api.dll &
echo "Starting Reverse DNS"
dotnet ./publish/AddressAnalyzer.ReverseDns.Api.dll &
echo "Starting Virus Total"
dotnet ./publish/AddressAnalyzer.VirusTotal.Api.dll &
echo "Starting Address Analyzer"
dotnet ./publish/AddressAnalyzer.Api.dll
