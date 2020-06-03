using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WoodGroveGroceriesWebApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InsightController : ControllerBase
    {
        [HttpGet]
        public IAsyncResult ThrowError()
        {
            throw new ApplicationException("Oula la grosse erreur");
        }
    }
}