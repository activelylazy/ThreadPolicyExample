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
        public async Task<IList<string>> Fetch()
        {
            return await Task.Run(() => DoFetch());
        }

        private IList<string> DoFetch()
        {
            Thread.Sleep(1000);
            return new[] { "Hello" };
        }
    }
}
