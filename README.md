# Webapi Generator

This is a visual studio extension in making which will generate Models, ViewModels and Controllers through edmx file

## Project Structure

The solution contains 3 projects

1. Generator
2. EdmxParser
3. WebApplication

Generator contains the core logic of generating files and utilizes the edmx parser to parse the edmx file. Generator also contains the `DummyModel.edmx` file that I use to test the generators. The WebApplication project is only there to test the generators the file are getting generated it this project. Here, generator parses the edmx file and generates IEnumerable<EntityType> which contains the data of entities propertiies and their association, then I utilize the Scriban as templating engine and interpret this entities into require files.

