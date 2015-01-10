using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace UIThreadSample
{
    class Model
    {
        [UIThreadPolicy]
        public async Task<IList<string>> Fetch()
        {
            return await Task.Run(() => DoFetch());
        }

        [WorkerThreadPolicy]
        private IList<string> DoFetch()
        {
            Thread.Sleep(1000);
            return new[] { "Hello" };
        }
    }
}
