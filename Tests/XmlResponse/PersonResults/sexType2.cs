//
// sexType2.cs
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

	public class sexType2 : SchemaString
	{
		public static string[] sEnumValues = {
			"M",
		};

		public enum EnumValues
		{
			eM = 0, /* M */
			EnumValueCount
		};

		public sexType2() : base()
		{
		}

		public sexType2(string newValue) : base(newValue)
		{
			Validate();
		}

		public sexType2(SchemaString newValue) : base(newValue)
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
				throw new System.Exception("Value of sex is invalid.");
		}
	}
}
