//
// PersonResultsTest.cs
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
using Altova.Types;

namespace PersonResults
{
	/// <summary>
	/// Summary description for PersonResultsTest.
	/// </summary>
	class PersonResultsTest
	{
		protected static void Example()
		{
			//
			// TODO:
			//   Insert your code here...
			//
			// Example code to create and save a structure:
			//   PersonResultsDoc doc = new PersonResultsDoc();
			//   WebURLType root = new WebURLType(doc, "", "", "WebURL");
			//   doc.SetSchemaLocation("PersonResults.xsd"); // optional
			//   ...
			//   doc.Save("PersonResults1.xml", root);
			//
			// Example code to load and save a structure:
			//   PersonResultsDoc doc = new PersonResultsDoc();
			//   WebURLType root = new WebURLType(doc.Load("PersonResults1.xml"));
			//   ...
			//   doc.Save("PersonResults1.xml", root);
			//
		}

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		public static int Main(string[] args)
		{
			try
			{
				Console.WriteLine("PersonResults Test Application");
				Example();
				Console.WriteLine("OK");
				return 0;
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
				return 1;
			}
		}
	}
}
