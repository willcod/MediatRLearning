using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace ConsoleApp
{
    public class SpeedStream : IStreamRequest<Speed>
    {

    }

    public class Speed
    {
        public int Value { get; set; }
    }

    public class MotionMonitor : IStreamRequestHandler<SpeedStream, Speed>
    {
        public async IAsyncEnumerable<Speed> Handle(SpeedStream request, [EnumeratorCancellation] CancellationToken cancellationToken)
        {
            for (var i = 0; i < 10; i++)
            {
                yield return await Task.FromResult(new Speed() { Value = i });
                await Task.Delay(500, cancellationToken);
            }
        }
    }
}
