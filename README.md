# OverheidioApi.Net [![Build status](https://ci.appveyor.com/api/projects/status/2vww4qykint9wnr5?svg=true)](https://ci.appveyor.com/project/jfversluis/overheidioapi-net) [![NuGet version](https://badge.fury.io/nu/OverheidioApi.Net.svg)](https://badge.fury.io/nu/OverheidioApi.Net)
A C#/.net wrapper for the Overheid.io API (only KvK data for now)

# Getting started
Just create a new `OverheidioClient` with your own API key.
API keys are available on [overheid.io](https://www.overheid.io) check the rest of the API documentation [here (in Dutch)](https://overheid.io/documentatie)

```
var _overheidioClient = new OverheidioClient("w98ef7e7w98fds789f9d8s7fd7");
```

Now you can start getting data.

## FindCorporations
This method provides you with the functionality to perform search operations on the dataset.
Check out the IntelliSense for the options on limiting your find results.

```
var resultCorporations = await client.Find();

foreach (var r in resultCorporations.Results.Corporations)
{
    Console.WriteLine(r.Tradename);
}
```

## GetCorporation
Gets the details from a specific Corporation by the dossiernumber and subdossiernumber

```
var oneResult = await client.GetCorporation("24237338", "0000");
Console.WriteLine(oneResult.Tradename + " " + oneResult.Street);
```

## GetSuggestions
Provides autocomplete suggestions based on the entered query

```
var suggestions = await client.GetSuggestions("Verslu");

Console.WriteLine("Suggestions " + suggestions.Tradenames.Length + " " + suggestions.Streets.Length);
```

## FindVehicles
This method provides you with the functionality to perform search operations on the dataset.
Check out the IntelliSense for the options on limiting your find results.

```
var resultVehicles = await client.Find();

foreach (var r in resultVehicles.Results.Vehicles)
{
    Console.WriteLine(r.Brand);
}
```

## GetVehicle
Gets the details from a specific Vehicle by licenseplate

```
var oneResult = await client.GetVehicle("4-TFL-24");
Console.WriteLine(oneResult.Brand);
```
