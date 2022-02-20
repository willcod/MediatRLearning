// See https://aka.ms/new-console-template for more information

using System.Reflection;
using ConsoleApp;
using MediatR;
using MediatR.Pipeline;
using SimpleInjector;

var container = new Container();
container.RegisterSingleton<IMediator, Mediator>();
container.Register(typeof(IRequestHandler<,>), new List<Assembly>()
{
    typeof(IMediator).GetTypeInfo().Assembly,
    typeof(Ping).GetTypeInfo().Assembly
});
container.Register(typeof(IStreamRequestHandler<,>), new List<Assembly>()
{
    typeof(IMediator).GetTypeInfo().Assembly,
    typeof(Ping).GetTypeInfo().Assembly
});

container.Collection.Register(typeof(IPipelineBehavior<,>), new[]
{
    typeof(RequestExceptionProcessorBehavior<,>),
    typeof(RequestExceptionActionProcessorBehavior<,>),
    //typeof(RequestPreProcessorBehavior<,>),
    //typeof(RequestPostProcessorBehavior<,>),
});

container.Collection.Register(typeof(IStreamPipelineBehavior<,>), new List<Assembly>()
{
    typeof(IMediator).GetTypeInfo().Assembly,
    typeof(Ping).GetTypeInfo().Assembly
});



//container.Collection.Register(typeof(IRequestPreProcessor<>), new[] { typeof(GenericRequestPreProcessor<>) });
//container.Collection.Register(typeof(IRequestPostProcessor<,>), new[] { typeof(GenericRequestPostProcessor<,>) });

container.Register(() => new ServiceFactory(container.GetInstance), Lifestyle.Singleton);

container.Verify();

var mediator = container.GetInstance<IMediator>();


var response = await mediator.Send(new Ping());
Console.WriteLine(response);

await mediator.Send(new OneWay());
await mediator.Send(new Hello());

var speeds = mediator.CreateStream(new SpeedStream());
await foreach (var speed in speeds)
{
    Console.WriteLine(speed.Value);
}

Console.ReadLine();