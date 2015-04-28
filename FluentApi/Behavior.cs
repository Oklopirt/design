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
        private Queue<Action> actions;

        public Behavior()
        {
            actions = new Queue<Action>();
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
            actions.Enqueue(() =>
            {
                Console.WriteLine(message);
            });
            return this;
        }

        public Behavior UntilKeyPressed(Func<Behavior, Behavior> inner)
        {
            actions.Enqueue(() =>
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
            actions.Enqueue(() =>
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
            actions.Enqueue(() =>
            {
                Thread.Sleep(time);
            });
            return this;
        }
    }
}