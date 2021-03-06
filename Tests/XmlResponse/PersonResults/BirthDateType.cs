//
// BirthDateType.cs
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
using System.Collections;
using System.Xml;
using Altova.Types;

namespace PersonResults
{
	public class BirthDateType : Altova.Xml.Node
	{
		#region Forward constructors

		public BirthDateType(XmlDocument doc) : base(doc) { SetCollectionParents(); }
		public BirthDateType(XmlNode node) : base(node) { SetCollectionParents(); }
		public BirthDateType(Altova.Xml.Node node) : base(node) { SetCollectionParents(); }
		public BirthDateType(Altova.Xml.Document doc, string namespaceURI, string prefix, string name) : base(doc, namespaceURI, prefix, name) { SetCollectionParents(); }
		#endregion // Forward constructors

		public override void AdjustPrefix()
		{

		    for (	XmlNode DOMNode = GetDomFirstChild( NodeType.Element, "", "Date" );
					DOMNode != null; 
					DOMNode = GetDomNextChild( NodeType.Element, "", "Date", DOMNode )
				)
			{
				InternalAdjustPrefix(DOMNode, true);
			}
		}



		#region Date accessor methods
		public static int GetDateMinCount()
		{
			return 1;
		}

		public static int DateMinCount
		{
			get
			{
				return 1;
			}
		}

		public static int GetDateMaxCount()
		{
			return 1;
		}

		public static int DateMaxCount
		{
			get
			{
				return 1;
			}
		}

		public int GetDateCount()
		{
			return DomChildCount(NodeType.Element, "", "Date");
		}

		public int DateCount
		{
			get
			{
				return DomChildCount(NodeType.Element, "", "Date");
			}
		}

		public bool HasDate()
		{
			return HasDomChild(NodeType.Element, "", "Date");
		}

		public DateType NewDate()
		{
			return new DateType();
		}

		public DateType GetDateAt(int index)
		{
			return new DateType(GetDomNodeValue(GetDomChildAt(NodeType.Element, "", "Date", index)));
		}

		public XmlNode GetStartingDateCursor()
		{
			return GetDomFirstChild( NodeType.Element, "", "Date" );
		}

		public XmlNode GetAdvancedDateCursor( XmlNode curNode )
		{
			return GetDomNextChild( NodeType.Element, "", "Date", curNode );
		}

		public DateType GetDateValueAtCursor( XmlNode curNode )
		{
			if( curNode == null )
				  throw new Altova.Xml.XmlException("Out of range");
			else
				return new DateType( curNode.InnerText );
		}


		public DateType GetDate()
		{
			return GetDateAt(0);
		}

		public DateType Date
		{
			get
			{
				return GetDateAt(0);
			}
		}

		public void RemoveDateAt(int index)
		{
			RemoveDomChildAt(NodeType.Element, "", "Date", index);
		}

		public void RemoveDate()
		{
			while (HasDate())
				RemoveDateAt(0);
		}

		public void AddDate(DateType newValue)
		{
			if( newValue.IsNull() == false )
				AppendDomChild(NodeType.Element, "", "Date", newValue.ToString());
		}

		public void InsertDateAt(DateType newValue, int index)
		{
			if( newValue.IsNull() == false )
				InsertDomChildAt(NodeType.Element, "", "Date", index, newValue.ToString());
		}

		public void ReplaceDateAt(DateType newValue, int index)
		{
			ReplaceDomChildAt(NodeType.Element, "", "Date", index, newValue.ToString());
		}
		#endregion // Date accessor methods

		#region Date collection
        public DateCollection	MyDates = new DateCollection( );

        public class DateCollection: IEnumerable
        {
            BirthDateType parent;
            public BirthDateType Parent
			{
				set
				{
					parent = value;
				}
			}
			public DateEnumerator GetEnumerator() 
			{
				return new DateEnumerator(parent);
			}
		
			IEnumerator IEnumerable.GetEnumerator() 
			{
				return GetEnumerator();
			}
        }

        public class DateEnumerator: IEnumerator 
        {
			int nIndex;
			BirthDateType parent;
			public DateEnumerator(BirthDateType par) 
			{
				parent = par;
				nIndex = -1;
			}
			public void Reset() 
			{
				nIndex = -1;
			}
			public bool MoveNext() 
			{
				nIndex++;
				return(nIndex < parent.DateCount );
			}
			public DateType  Current 
			{
				get 
				{
					return(parent.GetDateAt(nIndex));
				}
			}
			object IEnumerator.Current 
			{
				get 
				{
					return(Current);
				}
			}
    	}

        #endregion // Date collection

        private void SetCollectionParents()
        {
            MyDates.Parent = this; 
	}
}
}
