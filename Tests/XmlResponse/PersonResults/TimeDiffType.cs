//
// TimeDiffType.cs
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

	public class TimeDiffType : SchemaString
	{
		public static string[] sEnumValues = {
			"0:00",
			"10:58",
			"11:16",
			"11:33",
			"11:51",
			"13:47",
			"14:06",
			"16:47",
			"1:55",
			"2:59",
			"7:45",
			"8:38",
		};

		public enum EnumValues
		{
			e0_00 = 0, /* 0:00 */
			e10_58 = 1, /* 10:58 */
			e11_16 = 2, /* 11:16 */
			e11_33 = 3, /* 11:33 */
			e11_51 = 4, /* 11:51 */
			e13_47 = 5, /* 13:47 */
			e14_06 = 6, /* 14:06 */
			e16_47 = 7, /* 16:47 */
			e1_55 = 8, /* 1:55 */
			e2_59 = 9, /* 2:59 */
			e7_45 = 10, /* 7:45 */
			e8_38 = 11, /* 8:38 */
			EnumValueCount
		};

		public TimeDiffType() : base()
		{
		}

		public TimeDiffType(string newValue) : base(newValue)
		{
			Validate();
		}

		public TimeDiffType(SchemaString newValue) : base(newValue)
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
				throw new System.Exception("Value of TimeDiff is invalid.");
		}
	}
}