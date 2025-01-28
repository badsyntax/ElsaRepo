using System.Diagnostics;
using System.Text.Json;
using Elsa.Extensions;
using Elsa.Workflows;
using Elsa.Workflows.Activities;
using Elsa.Workflows.Activities.Flowchart.Activities;

namespace ElsaConsole
{
    public class Pokemon
    {
        public required string Name { get; set; }
        public required string Url { get; set; }
    }

    public class CustomActivity : CodeActivity<string>
    {
        protected override async ValueTask ExecuteAsync(ActivityExecutionContext context)
        {
            var currentValue = context.GetVariable<string>("CurrentValue");
            context.SetResult(currentValue);
            await Task.CompletedTask;
        }
    }

    public class Example_MassiveDataSet
    {
        private static readonly JsonSerializerOptions _jsonSerializerOptions = new()
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        };

        public static async Task Run_Example1(
            IWorkflowBuilderFactory workflowBuilderFactory,
            IWorkflowRunner workflowRunner)
        {
            var pokemonJson = await File.ReadAllTextAsync("pokemonApiResponse.json");
            var pokemon = JsonSerializer.Deserialize<List<Pokemon>>(pokemonJson, _jsonSerializerOptions)!;

            var customActivity = new CustomActivity();

            var innerForEach = new ForEach<string>(
                [
                    "item1",
                    "item2",
                ])
            {
                Body = new Sequence
                {
                    Activities = [
                        customActivity,
                        new Inline((ActivityExecutionContext context) =>
                        {
                            var currentValue = context.GetVariable<string>("CurrentValue");
                            var activityResult = context.GetResult(customActivity);
                            Debug.Assert(currentValue == activityResult, 
                                "The output of CustomActivity does not match the current value in the ForEach");
                        })
                    ]
                }
            };

            var workflowBuilder = workflowBuilderFactory.CreateBuilder();
            workflowBuilder.Root = innerForEach;
            var workflow = await workflowBuilder.BuildWorkflowAsync();
            var output = await workflowRunner.RunAsync(workflow);
        }

    }
}