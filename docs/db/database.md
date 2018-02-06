Database deployment script processing


```csharp
Kick.Start(c => c
    .IncludeAssemblyFor<UserModule>()
    .UseDatabase(d => d
        .Scripts(s => s
            .Embedded(e => e
                .StartsWith("Script")
                .Contains("Scripts")
            )
            .Directory(path)
            .Script<T>()
        )
        .SqlServer(s => s
            .Connection(connectionString)
            .Transaction(None|PerScript|Full)
            .Journal(table, schema)
        )
        .PostgreSQL(s => s
            .Connection(connectionString)
            .Transaction(None|PerScript|Full)
            .Journal(table, schema)
        )
        .MySql(s => s
            .Connection(connectionString)
            .Transaction(None|PerScript|Full)
            .Journal(table, schema)
        )
        .SQLite(s => s
            .Connection(connectionString)
            .Transaction(None|PerScript|Full)
            .Journal(table)
        )
    )
);


```