using AspNetDiTest1.Services;
using Microsoft.AspNetCore.Mvc;

namespace AspNetDiTest1.Controllers
{
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {
        private readonly IMasterService master;
        private readonly IContextService context;

        public ValuesController(IMasterService master, IContextService context)
        {
            this.master = master;
            this.context = context;
        }

        [HttpGet]
        public string Get()
        {
            return $"Controller says context = {context.RequestVariable} \n" +
                $"Master says context = {master.ReturnContext()}";
        }
    }
}
