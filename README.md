# WebApi Generator - Visual Studio Extension

## In Development

This is a visual studio extension in making which will generate Models, ViewModels and Controllers from `edmx` file.

## Project Structure

The solution contains 3 projects

1. Generator
2. EdmxParser
3. WebApplication

`Generator` contains the core logic of generating files and utilizes the `EdmxParser` to parse the [edmx](https://www.entityframeworktutorial.net/model-first-with-entity-framework.aspx) file. `Generator` also contains the `DummyModel.edmx` file to test the generators. The `WebApplication` project is only there to test the output files. In console app generator parses the edmx file and generates `IEnumerable<EntityType>` which contains the data of entities, their properties, and association, which then interpreted require files using [Scriban](https://github.com/lunet-io/scriban) templating engine. Check out [BaseControllerGenerator.cs](https://github.com/kudchikarsk/webapi-generator/blob/master/Generator/ControllerGenerator/BaseControllerGenerator.cs) and you will get some idea. Also, you may find [EdmxConverter.cs](https://github.com/kudchikarsk/webapi-generator/blob/master/EdmxParser/EdmxConverter.cs) a good place to start studying the project.

I will keep updating this doc on a regular basis any contribution or suggestion is warmly welcome!



