using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluentTask
{

    internal abstract class Action
    {
        public abstract void Act();
    }

    internal class Say : Action
    {
    }

    class Behavior
    {
        //List<string> actions = new List<string>();
        public readonly List<Action> actions; 
        public Behavior(IEnumerable<Action> action)
        {
            this.actions = action.ToList();
        }


        public Behavior Say(string message)
        {
            return new Behavior(
                actions.Concat(
                new Action[]{() => Console.WriteLine(message)}));
            throw new NotImplementedException();

        }

        public void UntilKeyPressed()
        {
            throw new NotImplementedException();
        }

        public void Delay(TimeSpan time)
        {

        }

        public void Execute()
        {
            foreach (var action in actions)
            {

            }
        }

    }
}
