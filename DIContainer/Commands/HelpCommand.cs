using System;
using System.IO;

namespace DIContainer.Commands
{
    class HelpCommand : BaseCommand
    {
        private readonly Lazy<ICommand[]> commands;
        private readonly TextWriter tw;

        public HelpCommand(Lazy<ICommand[]> commands, TextWriter tw)
        {
            this.commands = commands;
            this.tw = tw;
        }

        public override void Execute()
        {
            foreach (var command in commands.Value)
            {
                tw.WriteLine(command.Name);
            }
        }
    }
}
