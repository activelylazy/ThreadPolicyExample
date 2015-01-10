using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using PostSharp;
using PostSharp.Aspects;

namespace UIThreadSample
{
    [Serializable]
    class UIThreadPolicy : OnMethodBoundaryAspect, IOnStateMachineBoundaryAspect
    {
        public override void OnEntry(MethodExecutionArgs args)
        {
#if DEBUG
            if (Thread.CurrentThread.IsBackground)
            {
                Console.WriteLine("*** Thread policy warning: \n" + Environment.StackTrace);
            }
#endif
        }
    }
}
