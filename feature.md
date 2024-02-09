# Features

## Required Namespaces

- Add a using for `CodePad.Server.Models`
- Add a using for `MediatR`
- Add a using for `System.Threading`
- Add a using for `System.Threading.Tasks`
- Add a using for `Microsoft.AspNetCore.Mvc`
- Add other relevant namespaces as per your requirements

## File Structure

	- The file should contain two classes, the feature and the controller.

## Feature Structure

- Features are the sum of the work that has to be done in every layer that is involved in getting a specific feature working.
- Features are represented by a single class with a simple name, for example Create.
- The class will contain 3 additional classes a request, a handler and a result.
- The request will be named Command (or Query for Get Operations)
- The result will be named Result and inherit from [Reply or Reply`T](/CodePad.Srever/Models/Reply.cs)

## Controller Structure

- An additional class, the controller, should be added at the bottom of the file.
- The controller class should use MediatR.
- The controller should have a route attribute [Route("api/{ResourceName}")] where {ResourceName} is generally the folder name
- Controllers should have the [ApiController] attribute
- Controllers should inherit from ControllerBase
- The controller should have a single method that returns Task<T> where T is the result of the feature.
- The return type of the controller method should not contain ActionResult or IActionResult.
- The parameter of the controller method should use the [FromBody] attribute for Post and Put and [FromQuery] for Get and Delete
- The method name of the controller method should contain the Http Verb, Get, GetAll, Post, Put, Patch and Delete are acceptable names.

## Code Style

- Fields should be private and use camelCase with no _

## Examples

- An example of a feature can be found at https://github.com/jbogard/contosouniversitydocker/blob/7709196ef1a5369618ac128e26125f7ea2f32bf7/ContosoUniversity/Features/Students/Create.cs
- An example of a controller can be found at https://github.com/jbogard/contosouniversitydocker/blob/7709196ef1a5369618ac128e26125f7ea2f32bf7/ContosoUniversity/Features/Students/StudentsController.cs