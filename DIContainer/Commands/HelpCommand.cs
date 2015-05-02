using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DIContainer.Commands
{
    class HelpCommand : BaseCommand
    {
        private readonly Lazy<ICommand[]> commands;
        

        public HelpCommand (Lazy<ICommand[]> commands)
        {
            this.commands = commands;
        }

        public override void Execute () 
        {
            foreach (var com in commands.Value)
            {
                Console.WriteLine(com.Name);
            }
        }
    }
}
