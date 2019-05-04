[![Build Status](https://travis-ci.org/JacobHeater/address-analyzer.svg?branch=master)](https://travis-ci.org/JacobHeater/address-analyzer)

# Address Analyzer

Address Analyzer provides a simple API to query multiple
endpoints to retrieve information about a domain, or an
IP address. This service is divided into microservices,
each of which are responsible for communicating with
the intended service that it is retrieving data for.

The main AddressAnalyzer.Api project is responsible for
aggregating this data, and returning it to the caller
in JSON format. The API allows for selecting a set of
services that the caller wants to retrieve data from,
or a simple `HTTP GET` endpoint that will retrieve
data from a predefined set of services &mdash; Ping,
GeoIP, and RDAP.

## API Usage

There are a few endpoints that are of note.

### Heartbeat

- `GET /api/v1/heartbeat` -> Returns a string
   indicating connectivity to the API.

- `GET /api/v1/analyze/{address}` -> Returns JSON from
   the default set of services.

   **Parameter** - `address`
       
    - Type `{string}` - IP Address or Domain
      
      - Example: `8.8.8.8` or `google.com`

- `GET /api/v1/analyze/{servicelist}/{address}` ->
   Returns JSON from the specified set of services
   in the `servicelist` parameter.

   **Parameter** - `servicelist`

   - Type `{string}` - Comma separated list of strings

     - Accepatable Values:
    
       - `vt`   - Virus Total
       - `rdap` - RDAP
       - `rdns` - Reverse DNS
       - `ping` - Ping
       - `geo`  - GeoIP

    - Example: `vt,rdap,geo`

  **Parameter** - `address`
       
    - Type `{string}` - IP Address or Domain
      
      - Example: `8.8.8.8` or `google.com`

## Running the App

### Prerequisites

- .NET Core SDK 2.2
- Visual Studio

### Building the App

#### From the Terminal

```shell
cd <project root dir>

dotnet build
```

#### From Visual Studio

You can build from the VS GUI.

### Testing the Code

#### From the Terminal

```shell
cd <project root dir>

dotnet test
```

#### From Visual Studio

You can run Unit Tests from the VS GUI.
