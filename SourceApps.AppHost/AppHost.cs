var builder = DistributedApplication.CreateBuilder(args);

builder.AddProject<Projects.WaitingApp>("waitingapp");

builder.Build().Run();
