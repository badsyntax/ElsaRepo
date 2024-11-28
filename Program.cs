using Elsa.Extensions;
using Elsa.Workflows.Contracts;
using Elsa.Workflows.Runtime.Contracts;
using ElsaConsole;
using Microsoft.Extensions.DependencyInjection;
using Elsa.Workflows.Management.Contracts;
var services = new ServiceCollection();

// Add Elsa services to the container.
services.AddElsa(elsa =>
{
   
});

// Build the service container.
var serviceProvider = services.BuildServiceProvider();

var registriesPopulator = serviceProvider.GetRequiredService<IRegistriesPopulator>();
await registriesPopulator.PopulateAsync();

var workflowRunner = serviceProvider.GetRequiredService<IWorkflowRunner>(); // Or inject it in your constructor.

var workflowSerializer = serviceProvider.GetRequiredService<IWorkflowSerializer>();
var workflowBuilderFactory = serviceProvider.GetRequiredService<IWorkflowBuilderFactory>();

await Example_MassiveDataSet.Run_Example1(workflowBuilderFactory, workflowRunner);
await Example_MassiveDataSet.Run_Example2(workflowBuilderFactory, workflowRunner);