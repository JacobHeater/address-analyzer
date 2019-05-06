#!/usr/bin/bash

echo "Starting Address Analyzer and Background Services\n"

dotnet ./publish/AddressAnalyzer.GeoIp.Api.dll &
dotnet ./publish/AddressAnalyzer.Ping.Api.dll &
dotnet ./publish/AddressAnalyzer.Rdap.Api.dll &
dotnet ./publish/AddressAnalyzer.ReverseDns.Api.dll &
dotnet ./publish/AddressAnalyzer.VirusTotal.Api.dll &
dotnet ./publish/AddressAnalyzer.Api.dll &