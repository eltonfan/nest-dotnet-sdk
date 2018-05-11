# Nest DotNet SDK(Elton.Nest) [![Build status](https://ci.appveyor.com/api/projects/status/nhhvlxjf504ft5be?svg=true)](https://ci.appveyor.com/project/eltonfan/nest-dotnet-sdk)

> (Unofficial) .NET Standard SDK for Nest's REST and Streaming APIs. Monitoring and control Nest Cam, Thermostat and Protect using C#.

## Documentation

This unofficial .net library is very similar to [Android-SDK](https://github.com/nestlabs/android-sdk), but you can happily use it on Windows, UWP, Xamarin.iOS, Xamarin.Android, Xamarin.macOS etc.

.NET Core console example: https://github.com/eltonfan/nest-dotnet-sdk/tree/master/examples/NestMonitoringConsole

Examples are also available in this README below.

## Install

NuGet: `PM> Install-Package Elton.Nest` 

## Quickstart

Setup your Nest instance, preparing it for Authorization / Authentication.

```csharp
// TODO:
```

## Authorization / Authentication

Before we can get started making requests to the Nest API, we must first get authorization from the
user. This authorization is in the form of an **access token**.

### Acquiring an access token

We must launch an OAuth 2.0 flow in order to get an access token initially. Then later, we can
re-use that token.

```csharp
// TODO:
```

### Re-using an existing access token

If you already have an access token, you can authenticate using that immediately.

```csharp
// TODO:
```

## Get values and listen for changes

### Listen for changes to everything

This includes all devices, structures and metadata.

```csharp
// TODO:
```

### Listen for changes to all devices

This includes all thermostats, smoke alarms and cameras.

```csharp
// TODO:
```

### Listen for changes to specific device types

#### All thermostats:

```csharp
// TODO:
```

#### All smoke alarms:

```csharp
// TODO:
```

#### All cameras:

```csharp
// TODO:
```

### Listen to changes to all structures

```csharp
// TODO:
```

### Listen to metadata changes

This includes the access token and client version.

```csharp
// TODO:
```

## Stop listening to changes

Remove a specific listener.

```csharp
// TODO:
```

Remove all listeners.

```csharp
// TODO:
```

## Set values and update devices / structures

Updating values on devices and structures is easy. Here are a few examples.

### Thermostat example

[See the full list of possible Thermostat methods here.](https://nestlabs.github.io/android-sdk/index.html?com/nestlabs/sdk/ThermostatSetter.html)

```csharp
// TODO:
```

### Thermostat example with callback

[See the full list of possible Thermostat methods here.](https://nestlabs.github.io/android-sdk/index.html?com/nestlabs/sdk/ThermostatSetter.html)

```csharp
// TODO:
```

### Camera example

[See the full list of possible Camera methods here.](https://nestlabs.github.io/android-sdk/index.html?com/nestlabs/sdk/CameraSetter.html)

```csharp
// TODO:
```

### Camera example with callback

[See the full list of possible Camera methods here.](https://nestlabs.github.io/android-sdk/index.html?com/nestlabs/sdk/CameraSetter.html)

```csharp
// TODO:
```

## License

Apache 2.0 - See [LICENSE](LICENSE) for more information.