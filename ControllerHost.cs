using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArchLab6
{
    public class ControllerHost : IHostedService
    {
        private IServiceProvider _provider;

        public ControllerHost(IServiceProvider provider)
        {
            _provider = provider;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            using (var scope = _provider.CreateScope())
            {
                Command? command = null;   

                
               
                do {
                    var _controller = scope.ServiceProvider.GetService<Controller>();

                    command = _controller.ReadCommand();
                    _controller.ProcessCommand(command);
                } while (command != Command.Exit) ;

            }
                return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        internal void ProcessCommand(Command command)
        {
        }

    }
}
