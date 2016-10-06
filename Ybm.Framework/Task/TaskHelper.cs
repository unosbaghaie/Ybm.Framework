using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ybm.Framework.Task
{
    public class TaskHelper
    {
        public void StartTask(Action action)
        {
            System.Threading.Tasks.Task.Factory.StartNew(action);
        }
        public void StartThread(Action action)
        {
            System.Threading.Tasks.Task.Factory.StartNew(action,TaskCreationOptions.LongRunning);
        }

        public async System.Threading.Tasks.Task StartThread2(Action action)
        {
            var task1 = System.Threading.Tasks.Task.Factory.StartNew(action);
            var task2 = System.Threading.Tasks.Task.Factory.StartNew(action);

            await System.Threading.Tasks.Task.WhenAll(task1, task2);



        }

    }
}
