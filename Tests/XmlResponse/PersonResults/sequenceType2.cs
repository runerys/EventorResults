//
// sequenceType2.cs
//
// This file was generated by XMLSpy 2006sp2 Enterprise Edition.
//
// YOU SHOULD NOT MODIFY THIS FILE, BECAUSE IT WILL BE
// OVERWRITTEN WHEN YOU RE-RUN CODE GENERATION.
//
// Refer to the XMLSpy Documentation for further details.
// http://www.altova.com/xmlspy
//


using Altova.Types;

namespace PersonResults
{

	public class sequenceType2 : SchemaInt
	{
		public static string[] sEnumValues = {
			"1",
		};

		public enum EnumValues
		{
			e1 = 0, /* 1 */
			EnumValueCount
		};

		public sequenceType2() : base()
		{
		}

		public sequenceType2(string newValue) : base(newValue)
		{
			Validate();
		}

		public sequenceType2(SchemaInt newValue) : base(newValue)
		{
			Validate();
		}

		public static int GetEnumerationCount()
		{
			return sEnumValues.Length;
		}

		public static string GetEnumerationValue(int index)
		{
			return sEnumValues[index];
		}

		public static bool IsValidEnumerationValue(string val)
		{
			foreach (string s in sEnumValues)
			{
				if (val == s)
					return true;
			}
			return false;
		}

		public void Validate()
		{

			if (!IsValidEnumerationValue(ToString()))
				throw new System.Exception("Value of sequence is invalid.");
		}
	}
}