using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Observations.Interfaces
{
    public interface IAsyncInitalisation
    {
        Task Initialisation { get; }
    }
}
