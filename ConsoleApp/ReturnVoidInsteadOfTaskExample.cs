using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace ConsoleApp
{
    public class Hello : IRequest{}
    public class SyncHandler : RequestHandler<Hello>
    {
        protected override void Handle(Hello request)
        {
            Console.WriteLine("Hello");
        }
    }
}
