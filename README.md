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

You need to implement OAuth flow to getting the authorization code, because different platforms have different ways.
[NestMonitoringConsole](https://github.com/eltonfan/nest-dotnet-sdk/tree/master/examples/NestMonitoringConsole) is an example of Windows platform.

```csharp
// A request code you can verify later.
string AUTH_TOKEN_REQUEST_CODE = "123";

// Set the configuration values.
var nestConfig = new NestConfig(
    clientId: "client-id",
    clientSecret: "client-secret",
    redirectUrl: "https://redirect-url",
    state: AUTH_TOKEN_REQUEST_CODE);
var oauth2 = new Oauth2FlowHandler(nestConfig);

// Launch the auth flow if you don't already have a token.
var requestUrl = oauth2.GetClientCodeUrl();

//TODO: GET requestUrl in webbrowser. different way on different platform
// If authorized, you can receive resulting url that contains the authorization code.
string resultingUrl = null;
var authorizationCode = oauth2.ParseAuthorizationCode(resultingUrl);

//receive the token
var token = oauth2.CreateToken(authorizationCode);
// Save the token to a safe place here so it can be re-used later.
```

### Re-using an existing access token

If you already have an access token, you can authenticate using that immediately.

```csharp
var nest = new NestClient();

// Get the token string from your safe place.
var token = "abc123";

// Authenticate with an existing token.
nest.StartWithToken(token);

nest.WhenValueChanged()
    .Subscribe(args =>
    {
        // Handle exceptions here.
    });
nest.WhenAuthRevoked()
    .Subscribe(args =>
    {
        // Your previously authenticated connection has become unauthenticated.
        // Recommendation: Relaunch an auth flow with nest.launchAuthFlow().
    });
```

## Get values and listen for changes

### Listen for changes to everything

This includes all devices, structures and metadata.

```csharp
//Subscribe to event
var subscription = nest.WhenAnyUpdated().Subscribe(update =>
{
    var metadata = update.Metadata;
    var cameras = update.Cameras;
    var smokeCOAlarms = update.SmokeCOAlarms;
    var thermostats = update.Thermostats;
    var structures = update.Structures;

    // Handle updates here.
});

//Unsubscribe from event
subscription.Dispose();
```

### Listen for changes to all devices

This includes all thermostats, smoke alarms and cameras.

```csharp
//Subscribe to event
var subscription = nest.WhenDeviceUpdated().Subscribe(update =>
{
    var cameras = update.Cameras;
    var smokeCOAlarms = update.SmokeCOAlarms;
    var thermostats = update.Thermostats;

    // Handle updates here.
});

//Unsubscribe from event
subscription.Dispose();
```

### Listen for changes to specific device types

```csharp
var listSubscriptions = new List<IDisposable>();
IDisposable subscription;

//All thermostats:
subscription = nest.WhenThermostatUpdated().Subscribe(thermostats =>
{
    // Handle thermostat update...
});
listSubscriptions.Add(subscription);

//All smoke alarms:
subscription = nest.WhenSmokeCOAlarmUpdated().Subscribe(thermostats =>
{
    // Handle smoke+co alarm update...
});
listSubscriptions.Add(subscription);

//All cameras:
subscription = nest.WhenCameraUpdated().Subscribe(thermostats =>
{
    // Handle camera update...
});
listSubscriptions.Add(subscription);

//Unsubscribe from events
foreach (var item in listSubscriptions)
    subscription.Dispose();
```

### Listen to changes to all structures

```csharp
//Subscribe to event
var subscription = nest.WhenStructureUpdated().Subscribe(structures =>
{
    // Handle structure update...
});

//Unsubscribe from event
subscription.Dispose();
```

### Listen to metadata changes

This includes the access token and client version.

```csharp
//Subscribe to event
var subscription = nest.WhenMetadataUpdated().Subscribe(data =>
{
    // Handle metadata update... do action.
});

//Unsubscribe from event
subscription.Dispose();
```

## Set values and update devices / structures

Updating values on devices and structures is easy. Here are a few examples.

### Thermostat example

[See the full list of possible Thermostat methods here.](https://nestlabs.github.io/android-sdk/index.html?com/nestlabs/sdk/ThermostatSetter.html)

```csharp
// Get id from Thermostat#getDeviceId
String thermostatId = thermostat.DeviceId;

// The temperature in Farhenheit to set. (Note: type long)
long newTempF = 75;

// Set thermostat target temp (in degrees F).
nest.Thermostats.SetTargetTemperatureF(thermostatId, newTempF);

float newTempC = 23.5F;

//Set thermostat target temp, in half degrees Celsius
nest.Thermostats.SetTargetTemperatureC(thermostatId, newTempC);
```

### Thermostat example with callback

[See the full list of possible Thermostat methods here.](https://nestlabs.github.io/android-sdk/index.html?com/nestlabs/sdk/ThermostatSetter.html)

```csharp
// Get id from Thermostat#getDeviceId
string thermostatId = myThermostat.DeviceId;

// The temperature in Celsius to set. (Note: type double)
double newTempC = 22.5;

// Set thermostat target temp (in degrees C) with an optional success callback.
nest.Thermostats.SetTargetTemperatureC(thermostatId, newTempC, new Callback()
{
    OnSuccess = () =>
    {
        // The update to the thermostat succeeded.
    },
    OnFailure = (ex) =>
    {
        // The update to the thermostat failed.
    },
});
```

### Camera example

[See the full list of possible Camera methods here.](https://nestlabs.github.io/android-sdk/index.html?com/nestlabs/sdk/CameraSetter.html)

```csharp
// Get id from Camera#getDeviceId.
string camId = myCamera.DeviceId;

// Set camera to start streaming.
nest.Cameras.SetIsStreaming(camId, true);
```

### Camera example with callback

[See the full list of possible Camera methods here.](https://nestlabs.github.io/android-sdk/index.html?com/nestlabs/sdk/CameraSetter.html)

```csharp
// Get id from Camera#getDeviceId.
string camId = myCamera.DeviceId;

// Set camera to start streaming with an optional success callback.
nest.Cameras.SetIsStreaming(camId, true, new Callback()
{
    OnSuccess = () =>
    {
        // The update to the camera succeeded.
    },
    OnFailure = (ex) =>
    {
        // The update to the camera failed.
    },
});
```

## License

Apache 2.0 - See [LICENSE](LICENSE) for more information.