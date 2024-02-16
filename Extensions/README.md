# IQueryable Search Extension

This is a C# extension method that allows you to perform a search operation on a collection of objects represented by `IQueryable<T>`. The search is performed based on specific properties of the objects.

## Usage

1. **Add the Extensions Class to Your Project**: Copy the provided `Extensions` class into your C# project.

2. **Using the Extension Method**:
    ```csharp
    using AveryBusiness;
    using System.Linq;
    
    // Your IQueryable<T> source
    IQueryable<YourType> source = ...;

    // Define search parameters
    string searchQuery = "YourSearchQuery";
    string[] searchParameters = { "Property1", "Property2.NestedProperty" }; // Properties to search

    // Perform the search
    var result = source.Search(searchQuery, searchParameters).ToList();
    ```

    Replace `YourType`, `"Property1"`, `"Property2.NestedProperty"`, and `"YourSearchQuery"` with appropriate types, property names, and search query.

## Parameters

- **source**: The `IQueryable<T>` representing the collection of objects to search within.
- **search**: The string to search for within the specified properties.
- **parameters**: An array of strings representing the names of the properties to search within. Nested properties can be specified using dot notation (e.g., `"Property1.NestedProperty"`).

## How It Works

The extension method `Search<TSource>()` dynamically generates a predicate expression based on the search query and properties specified. It builds an expression tree that represents the search condition and applies it to filter the `IQueryable<T>` source.

The method `SearchPredicate<T>()` generates the main predicate expression, while `PredicateFromProperties<T>()` creates individual expressions for each property specified in the search parameters.
