using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace iHostedServiceDemo.Services
{
    public class WriteToFileHostedService2 : IHostedService, IDisposable
    {
        private readonly IHostingEnvironment environment;
        private readonly string fileName = "File2.txt";

        //se inyecta dependencias
        public WriteToFileHostedService2(IHostingEnvironment environment)
        {
            this.environment = environment;

        }

        private void WriteToFile(string message)
        {
            // environment.ContentRootPath indica en donde se encuentra mi aplicacion corriendo
            // ojo con los espacios
            var path = $@"{ environment.ContentRootPath }\wwwroot\{fileName }";
            using (StreamWriter writer = new StreamWriter(path, append: true))
            {
                writer.WriteLine(message);
            }
        }

        // implementacion de funcion recurrente, solo para que se entienda puse todo aca
        private Timer timer;

        private void DoWork(object state)
        {
            WriteToFile("WriteToFileHostedService: haciendo algo en " + DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss"));
        }

        //----------------------------------------------------------------
        // las task creadas por defecto
        Task IHostedService.StartAsync(CancellationToken cancellationToken)
        {
            // se escribe en el archivo cuando la aplicacion empiece.
            WriteToFile("WriteToFileHostedService: Process Started");

            //se inicializa el timer para la funcion dowork
            timer = new Timer(DoWork, null, TimeSpan.Zero, TimeSpan.FromSeconds(7));

            //throw new NotImplementedException();
            return Task.CompletedTask;
        }

        //segun la documentacion de microsoft, este evento es posible que no se ejecute
        Task IHostedService.StopAsync(CancellationToken cancellationToken)
        {
            WriteToFile("WriteToFileHostedService: Process Stopped");
            //throw new NotImplementedException();
            // se desactiva el timer
            timer?.Change(Timeout.Infinite, 0);

            return Task.CompletedTask;
        }

        public void Dispose()
        {
            //throw new NotImplementedException();
            // se coloca el ? para que destruya el timer si es que existe.
            timer?.Dispose();
        }
    
    }
}
