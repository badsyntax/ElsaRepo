using System.Text.Json;
using Elsa.Workflows.Activities;
using Elsa.Workflows.Activities.Flowchart.Activities;
using Elsa.Workflows.Contracts;

namespace ElsaConsole
{
    public class Pokemon
    {
        public required string Name { get; set; }
        public required string Url { get; set; }
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
            var watch = System.Diagnostics.Stopwatch.StartNew();

            var innerForEach = new ForEach<string>(
                [
                    "item1",
                    "item2",
                    "item3"
                ])
            {
                Body = new Sequence
                {
                    Activities = [
                        new Inline(context =>
                        {
                            //
                        }),
                        new Inline(context =>
                        {
                            //
                        })
                    ]
                }
            };

            var forEachBodyFlowChart = new Flowchart();
            forEachBodyFlowChart.Activities.Add(innerForEach);


            var forEachActivity = new ForEach<Pokemon>(pokemon)
            {
                Body = forEachBodyFlowChart
            };

            var workflowBuilder = workflowBuilderFactory.CreateBuilder();
            workflowBuilder.Root = forEachActivity;
            var workflow = await workflowBuilder.BuildWorkflowAsync();
            var output = await workflowRunner.RunAsync(workflow);
            watch.Stop();
            var elapsedMs = watch.ElapsedMilliseconds;

            Console.WriteLine($"done in {elapsedMs}ms");
        }

        public static async Task Run_Example2(
            IWorkflowBuilderFactory workflowBuilderFactory,
            IWorkflowRunner workflowRunner)
        {
            var pokemonJson = await File.ReadAllTextAsync("pokemonApiResponse.json");
            var pokemon = JsonSerializer.Deserialize<List<Pokemon>>(pokemonJson, _jsonSerializerOptions)!;
            var watch = System.Diagnostics.Stopwatch.StartNew();



            var forEachActivity = new ForEach<Pokemon>(pokemon)
            {
                Body = new Inline(context =>
                    {
                        //
                    }),
            };

            var workflowBuilder = workflowBuilderFactory.CreateBuilder();
            workflowBuilder.Root = forEachActivity;
            var workflow = await workflowBuilder.BuildWorkflowAsync();
            var output = await workflowRunner.RunAsync(workflow);
            watch.Stop();
            var elapsedMs = watch.ElapsedMilliseconds;

            Console.WriteLine($"done in {elapsedMs}ms");
        }
    }
}