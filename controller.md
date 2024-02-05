#Controller

	- Controllers will use MediatR
	- Controller Methods will be async and accept the Command or Query from a feature
	- Controller Methods will return a Task<{FeatureResult}> where {FeatureResult} is the response defined in the feature that inherits from Reply or Reply<T>
	- an example of a controller can be found here (https://github.com/jbogard/contosouniversitydocker/blob/7709196ef1a5369618ac128e26125f7ea2f32bf7/ContosoUniversity/Features/Students/StudentsController.cs)