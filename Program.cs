using Elsa.Extensions;
using Elsa.Workflows;
using Elsa.Workflows.Management;
using Elsa.Workflows.Middleware.Workflows;
using Elsa.Workflows.Runtime;
using ElsaConsole;
using Microsoft.Extensions.DependencyInjection;
var services = new ServiceCollection();

// Add Elsa services to the container.
services.AddElsa(elsa =>
{
//    elsa.UseWorkflows(workflows =>
//     {
//         workflows.WithWorkflowExecutionPipeline(pipeline => pipeline
//             .Reset()
//             .UseEngineExceptionHandling()
//             .UseExceptionHandling()
//             .UseDefaultActivityScheduler());
//     });
});

// Build the service container.
var serviceProvider = services.BuildServiceProvider();

var registriesPopulator = serviceProvider.GetRequiredService<IRegistriesPopulator>();
await registriesPopulator.PopulateAsync();

var workflowRunner = serviceProvider.GetRequiredService<IWorkflowRunner>(); // Or inject it in your constructor.
var workflowBuilderFactory = serviceProvider.GetRequiredService<IWorkflowBuilderFactory>();

// await Example_MassiveDataSet.Run_Example1(workflowBuilderFactory, workflowRunner);
await Example_MassiveDataSet.Run_Example1(workflowBuilderFactory, workflowRunner);