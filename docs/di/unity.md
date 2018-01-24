# Unity

The Unity extension allows registration of types to be resolved by running all instances of `IUnityRegistration`.

Basic usage

```csharp
Kick.Start(config => config
    .IncludeAssemblyFor<UserRepository>() // where to look
    .UseUnity () // initialize Unity
);
```

To install Unity extension, run the following command in the Package Manager Console

    PM> Install-Package KickStart.Unity