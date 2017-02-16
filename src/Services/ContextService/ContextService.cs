using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetDiTest1.Services
{
    public class ContextService : IContextService
    {
        public string RequestVariable { get; set; }
    }
}
