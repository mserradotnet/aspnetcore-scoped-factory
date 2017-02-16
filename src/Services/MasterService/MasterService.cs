using System;

namespace AspNetDiTest1.Services
{
    public class MasterService : IMasterService
    {

        private readonly Func<IContextService> contextFactory;

        public MasterService(Func<IContextService> contextFactory)
        {
            this.contextFactory = contextFactory;
        }

        private IContextService Context => this.contextFactory();

        public string ReturnContext()
        {
            return Context.RequestVariable;
        }
    }
}
