using System;
using System.IO;
using System.Linq;
using DIContainer.Commands;
using Ninject;

namespace DIContainer
{
    public class Program
    {
        private readonly CommandLineArgs arguments;
        private readonly ICommand[] commands;

        public ICommand[] GetCommand()
        {
            return commands;
        }

        public Program(CommandLineArgs arguments, params ICommand[] commands)
        {
            this.arguments = arguments;
            this.commands = commands;
        
        }

        static void Main(string[] args)
        {
            var container = new StandardKernel();
            container.Bind<ICommand>().To<TimerCommand>();
            container.Bind<ICommand>().To<PrintTimeCommand>();
            container.Bind<ICommand>().To<HelpCommand>();
            container.Bind<TextWriter>().ToConstant(Console.Out);
            container.Bind<CommandLineArgs>().ToConstant(new CommandLineArgs(args));

            var arguments = container.Get<CommandLineArgs>();
            var commands = container.GetAll<ICommand>().ToArray();
            container.Get<Program>().Run();
        }

        public void Run()
        {
            TextWriter writer = TextWriter.Null;
            if (arguments.Command == null)
            {
                writer.WriteLine("Please specify <command> as the first command line argument");
                return;
            }
            var command =
                commands.FirstOrDefault(
                    c => c.Name.Equals(arguments.Command, StringComparison.InvariantCultureIgnoreCase));
            if (command == null)
                writer.WriteLine("Sorry. Unknown command {0}", arguments.Command);
            else
                command.Execute();
        }
    }
}
