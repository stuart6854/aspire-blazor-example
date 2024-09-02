var builder = DistributedApplication.CreateBuilder(args);

var apiService = builder.AddProject<Projects.AspireBlazorApp_ApiService>("apiservice");

builder.AddProject<Projects.AspireBlazorApp_Web>("webfrontend")
	.WithExternalHttpEndpoints()
	.WithReference(apiService);

builder.Build().Run();
