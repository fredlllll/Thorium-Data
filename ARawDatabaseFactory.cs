using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Thorium.Data
{
    public abstract class ARawDatabaseFactory
    {
        Dictionary<int, IRawDatabase> instances = new Dictionary<int, IRawDatabase>();

        async void AwaitThreadEnd(object arg)
        {
            Thread thread = (Thread)arg;
            while(thread.IsAlive)
            {
                await Task.Delay(5000);
            }
            lock(instances)
            {
                instances.Remove(thread.ManagedThreadId);
            }
        }

        void StartWatchingThread(Thread thread)
        {
            var task = new Task(AwaitThreadEnd, thread);
            task.Start();
        }

        public IRawDatabase GetDatabase()
        {
            lock(instances)
            {
                int id = Thread.CurrentThread.ManagedThreadId;
                if(instances.TryGetValue(id, out IRawDatabase db))
                {
                    //if we already have one for this thread, return it
                    return db;
                }
                //if we dont, make a new one and watch the thread so we can remove it once the thread ends
                var instance = GetNewDatabaseInstance();
                instances[id] = instance;
                StartWatchingThread(Thread.CurrentThread);
                return instance;
            }
        }

        protected abstract IRawDatabase GetNewDatabaseInstance();
    }
}
