// See https://aka.ms/new-console-template for more information


#if !DEBUG
Environment.Exit(0);
#endif
using ClientGenerator.Models;

if (args == null || args.Count() == 0)
{
    Console.WriteLine("expects the following arguments:");
    Console.WriteLine("Path to assembly that contains controllers");
    Console.WriteLine("Client Type, ex typescript");
    Console.WriteLine("Optional output folder relative from the solution file- will default to typescript-clients");
    Console.WriteLine("ex:");
    Console.WriteLine(".\\..\\tools\\Debug\\ClientGenerator.exe .\\..\\CodePad\\CodePad\\bin\\Debug\\net6.0\\CodePad.dll Typescript \\CodePad\\ClientApp\\src\\clients");
}

//System.Diagnostics.Debugger.Launch();
var assemblyPath = args[0];
var clientType = args[1];
var folder = string.Empty;
if (args.Length > 1)
    folder = args[2];

var generator = new GeneratorFactory().GetGenerator(assemblyPath, clientType, folder);
generator.GenerateModels();
generator.GenerateClients();
//TODO: add support for other client types, flutter

