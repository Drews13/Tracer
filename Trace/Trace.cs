using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;

namespace Tracer
{
    public class Trace : ITracer
    {
        TraceResult traceResult = new TraceResult();
        private ConcurrentDictionary<int, Stack<Methods>> concurrentDictionary = new ConcurrentDictionary<int, Stack<Methods>>();
        public void StartTrace()
        {
            Methods method = new Methods();
            method.Class = new StackFrame(1).GetMethod().DeclaringType.Name;
            method.Name = new StackFrame(1).GetMethod().Name;
            if (traceResult.Threads == null)
            {
                traceResult.Threads = new List<Threads>();
            }
            int CurrThreadId = Thread.CurrentThread.ManagedThreadId;

            if (concurrentDictionary.TryAdd(CurrThreadId, new Stack<Methods>()))
            {
                traceResult.Threads.Add(new Threads(CurrThreadId));
            }

            if (concurrentDictionary[CurrThreadId].Count != 0 )
            {
                Methods HigherMethod = concurrentDictionary[CurrThreadId].Peek();
                if (HigherMethod.Methods == null)
                {
                    HigherMethod.Methods = new List<Methods>();
                }
                HigherMethod.Methods.Add(method);
            }
            else
            {
                int ListInd = traceResult.Threads.FindIndex(_thread => _thread.ID == CurrThreadId);
                if (traceResult.Threads[ListInd].Methods == null)
                {
                    traceResult.Threads[ListInd].Methods = new List<Methods>();
                }
                traceResult.Threads[ListInd].Methods.Add(method);                
            }
            method.stopwatch = new Stopwatch();
            method.stopwatch.Start();
            concurrentDictionary[CurrThreadId].Push(method);
        }

        public void StopTrace()
        {
            int CurrThreadID = Thread.CurrentThread.ManagedThreadId;
            Methods method = concurrentDictionary[CurrThreadID].Pop();
            method.stopwatch.Stop();
            method.Time = method.stopwatch.ElapsedMilliseconds;
            if (concurrentDictionary[CurrThreadID].Count == 0)
            {
                int ListInd = traceResult.Threads.FindIndex(_thread => _thread.ID == CurrThreadID);
                traceResult.Threads[ListInd].Time += method.Time;
            }
        }

        public TraceResult GetTraceResult()
        {
            return traceResult;
        }
    }
}
