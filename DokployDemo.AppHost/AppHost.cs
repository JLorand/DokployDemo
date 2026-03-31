var builder = DistributedApplication.CreateBuilder(args);

builder.AddDockerComposeEnvironment("Production");

var apiService = builder.AddProject<Projects.DokployDemo_ApiService>("apiservice")
    .WithHttpHealthCheck("/health");

builder.AddProject<Projects.DokployDemo_Web>("webfrontend")
    .WithExternalHttpEndpoints()
    .WithHttpHealthCheck("/health")
    .WithReference(apiService)
    .WaitFor(apiService);

builder.Build().Run();
