using Elsa.Extensions;
using Elsa.Workflows;
using Elsa.Workflows.Management;
using Elsa.Workflows.Runtime;
using ElsaConsole;
using Microsoft.Extensions.DependencyInjection;
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
var workflowBuilderFactory = serviceProvider.GetRequiredService<IWorkflowBuilderFactory>();

await Example_MassiveDataSet.Run_Example1(workflowBuilderFactory, workflowRunner);
// await Example_MassiveDataSet.Run_Example2(workflowBuilderFactory, workflowRunner);