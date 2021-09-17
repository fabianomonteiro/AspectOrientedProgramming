using FluentInteract;
using FluentInteract.Aspects;
using System;

namespace LoggingAspectWithProxyApiSample.Aspects
{
    public class LoggingAspect : ILoggingAspect
    {
        public bool IsMatch(IInteractor interactor, object input)
        {
            return true;
        }

        public void LogStartExecute(IInteractor interactor, object input, ICallerInstance callerInstance, string memberName, string sourceFilePath, int sourceLineNumber)
        {
            Console.WriteLine("Start Execute");
        }

        public void LogEndExecute(IInteractor interactor, TimeSpan timeSpanExecution, bool executeFromAspect, IAspect aspectExecutedInstance)
        {
            Console.WriteLine("End Execute");
        }

        public void LogExceptionExecute(IInteractor interactor, Exception exception)
        {
            Console.WriteLine("Exception Execute");
        }
    }
}
