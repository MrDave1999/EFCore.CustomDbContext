# EFCore.CustomDbContext
[![Nuget-Badges](https://buildstats.info/nuget/EFCore.CustomDbContext)](https://www.nuget.org/packages/EFCore.CustomDbContext/)

This library sets automatically the CreatedAt and UpdatedAt properties.

Credits for this page: https://www.entityframeworktutorial.net/faq/set-created-and-modified-date-in-efcore.aspx

## Installation

Execute the following command in the terminal:
```bash
dotnet add package EFCore.CustomDbContext
```

## Usage

Inherits from `CustomBaseEntity` class:
```cs
public class Entity : CustomBaseEntity
{
	public int Id { get; set; }
}
```
And then inherit from `CustomDbContext`:
```cs
public class AppDbContext : CustomDbContext { }
```

By default, the library sets the current date with `DateTime.Now`, this behavior can be changed:
```cs
public class AppDbContext : CustomDbContext 
{ 
	public override DateTime CreatedAt => DateTime.UtcNow;
	public override DateTime UpdatedAt => DateTime.UtcNow;
	
	public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
}
```
And ready, use the `SaveChangesAsync` method freely.