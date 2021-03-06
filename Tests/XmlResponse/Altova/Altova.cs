//
// Altova.cs
//
// This file was generated by XMLSpy 2006sp2 Enterprise Edition.
//
// YOU SHOULD NOT MODIFY THIS FILE, BECAUSE IT WILL BE
// OVERWRITTEN WHEN YOU RE-RUN CODE GENERATION.
//
// Refer to the XMLSpy Documentation for further details.
// http://www.altova.com/xmlspy
//


using System;

namespace Altova 
{
	/// <summary>
	/// Base class for all exceptions thrown by functions of the Altova-library..
	/// </summary>
	public class AltovaException : Exception 
	{
		protected Exception	innerException;
		protected string	message;

		public AltovaException(string text) : base(text) 
		{
			innerException = null;
			message = text;
		}

		public AltovaException(Exception other) : base("", other)
		{
			innerException = other;
			message = other.Message;
		}

		virtual public string GetMessage() 
		{
			return message;
		}

		public Exception GetInnerException() 
		{
			return innerException;
		}
	}
	
	/// <summary>
	/// Exception that can be thrown by the user.
	/// </summary>
	public class UserException : AltovaException
	{
		public UserException (string text) : base(text)
		{}
	}

	/// <summary>
	/// Interface to print TRACE and result output generated by the application.
	/// </summary>
	public interface TraceTarget 
	{
		void WriteTrace(string info);
	}

	/// <summary>
	/// Abstract class to be derived by the application for printing TRACE- and result-output generated by the application.
	/// </summary>
	public abstract class TraceProvider 
	{
		protected TraceTarget traceTarget = null;

		protected void WriteTrace(string info) 
		{
			if (traceTarget != null)
				traceTarget.WriteTrace(info);
		}

		public void RegisterTraceTarget(TraceTarget newTraceTarget) 
		{
			traceTarget = newTraceTarget;
		}

		public void UnregisterTraceTarget() 
		{
			traceTarget = null;
		}
	}
}