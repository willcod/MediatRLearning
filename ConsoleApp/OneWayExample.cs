using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace ConsoleApp
{
    public class OneWay : IRequest
    {
    }

    public class OneWayHandlerWithBaseClass : AsyncRequestHandler<OneWay>
    {
        protected override Task Handle(OneWay request, CancellationToken cancellationToken)
        {
            Console.WriteLine("OneWay called");
            return Task.CompletedTask;
        }
    }
}
