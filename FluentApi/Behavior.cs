using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FluentTask
{
    public class Behavior
    {
        private List<Action> actions;

        public Behavior()
        {
            actions = new List<Action>();
        }

        public void Execute()
        {
            foreach (var action in actions)
            {
                action.Invoke();
            }
        }

        public Behavior Say(string message)
        {
            actions.Add(() =>
            {
                Console.WriteLine(message);
            });
            return this;
        }

        public Behavior UntilKeyPressed(Func<Behavior, Behavior> inner)
        {
            actions.Add(() =>
            {
                while (!Console.KeyAvailable)
                {
                    var behaviour = new Behavior();
                    inner.Invoke(behaviour);
                    behaviour.Execute();
                    Thread.Sleep(TimeSpan.FromSeconds(1));
                }
                Console.ReadKey(true);
            });
            return this;
        }

        public Behavior Jump(JumpHeight height)
        {
            actions.Add(() =>
            {
                if (height.Equals(JumpHeight.Low))
                {
                    Console.WriteLine("Прыгнул низко!");
                }
                else
                {
                    Console.WriteLine("Прыгнул высоко!");
                }
            });
            return this;
        }

        public Behavior Delay(TimeSpan time)
        {
            actions.Add(() =>
            {
                Thread.Sleep(time);
            });
            return this;
        }
    }
}