using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetDiTest1.Services
{
    public interface IContextService
    {
        string RequestVariable { get; set; }
    }
}
