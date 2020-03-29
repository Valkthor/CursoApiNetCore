using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiModulo4.Helpers
{
    public class MiFiltroDeAccion: IActionFilter
    {
        private readonly ILogger<MiFiltroDeAccion> logger;

        // se inyecta dependencia a los filtros de accion
        public MiFiltroDeAccion(ILogger<MiFiltroDeAccion> logger)
        {
            this.logger = logger;
        }

        // se ejecuta antes de una accion
        public void OnActionExecuting(ActionExecutingContext context)
        {
            logger.LogError("OnActionExecuting");
        }

        // se ejecuta despues de una accion
        public void OnActionExecuted(ActionExecutedContext context)
        {
            logger.LogError("OnActionExecuted");
        }

    }
}
