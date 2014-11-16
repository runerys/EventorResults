//
// ModifiedByType.cs
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
	public class ModifiedByType : Altova.Xml.Node
	{
		#region Forward constructors

		public ModifiedByType(XmlDocument doc) : base(doc) { SetCollectionParents(); }
		public ModifiedByType(XmlNode node) : base(node) { SetCollectionParents(); }
		public ModifiedByType(Altova.Xml.Node node) : base(node) { SetCollectionParents(); }
		public ModifiedByType(Altova.Xml.Document doc, string namespaceURI, string prefix, string name) : base(doc, namespaceURI, prefix, name) { SetCollectionParents(); }
		#endregion // Forward constructors

		public override void AdjustPrefix()
		{

		    for (	XmlNode DOMNode = GetDomFirstChild( NodeType.Element, "", "PersonId" );
					DOMNode != null; 
					DOMNode = GetDomNextChild( NodeType.Element, "", "PersonId", DOMNode )
				)
			{
				InternalAdjustPrefix(DOMNode, true);
				new PersonIdType(DOMNode).AdjustPrefix();
			}
		}



		#region PersonId accessor methods
		public static int GetPersonIdMinCount()
		{
			return 1;
		}

		public static int PersonIdMinCount
		{
			get
			{
				return 1;
			}
		}

		public static int GetPersonIdMaxCount()
		{
			return 1;
		}

		public static int PersonIdMaxCount
		{
			get
			{
				return 1;
			}
		}

		public int GetPersonIdCount()
		{
			return DomChildCount(NodeType.Element, "", "PersonId");
		}

		public int PersonIdCount
		{
			get
			{
				return DomChildCount(NodeType.Element, "", "PersonId");
			}
		}

		public bool HasPersonId()
		{
			return HasDomChild(NodeType.Element, "", "PersonId");
		}

		public PersonIdType NewPersonId()
		{
			return new PersonIdType(domNode.OwnerDocument.CreateElement("PersonId", ""));
		}

		public PersonIdType GetPersonIdAt(int index)
		{
			return new PersonIdType(GetDomChildAt(NodeType.Element, "", "PersonId", index));
		}

		public XmlNode GetStartingPersonIdCursor()
		{
			return GetDomFirstChild( NodeType.Element, "", "PersonId" );
		}

		public XmlNode GetAdvancedPersonIdCursor( XmlNode curNode )
		{
			return GetDomNextChild( NodeType.Element, "", "PersonId", curNode );
		}

		public PersonIdType GetPersonIdValueAtCursor( XmlNode curNode )
		{
			if( curNode == null )
				  throw new Altova.Xml.XmlException("Out of range");
			else
				return new PersonIdType( curNode );
		}


		public PersonIdType GetPersonId()
		{
			return GetPersonIdAt(0);
		}

		public PersonIdType PersonId
		{
			get
			{
				return GetPersonIdAt(0);
			}
		}

		public void RemovePersonIdAt(int index)
		{
			RemoveDomChildAt(NodeType.Element, "", "PersonId", index);
		}

		public void RemovePersonId()
		{
			while (HasPersonId())
				RemovePersonIdAt(0);
		}

		public void AddPersonId(PersonIdType newValue)
		{
			AppendDomElement("", "PersonId", newValue);
		}

		public void InsertPersonIdAt(PersonIdType newValue, int index)
		{
			InsertDomElementAt("", "PersonId", index, newValue);
		}

		public void ReplacePersonIdAt(PersonIdType newValue, int index)
		{
			ReplaceDomElementAt("", "PersonId", index, newValue);
		}
		#endregion // PersonId accessor methods

		#region PersonId collection
        public PersonIdCollection	MyPersonIds = new PersonIdCollection( );

        public class PersonIdCollection: IEnumerable
        {
            ModifiedByType parent;
            public ModifiedByType Parent
			{
				set
				{
					parent = value;
				}
			}
			public PersonIdEnumerator GetEnumerator() 
			{
				return new PersonIdEnumerator(parent);
			}
		
			IEnumerator IEnumerable.GetEnumerator() 
			{
				return GetEnumerator();
			}
        }

        public class PersonIdEnumerator: IEnumerator 
        {
			int nIndex;
			ModifiedByType parent;
			public PersonIdEnumerator(ModifiedByType par) 
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
				return(nIndex < parent.PersonIdCount );
			}
			public PersonIdType  Current 
			{
				get 
				{
					return(parent.GetPersonIdAt(nIndex));
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

        #endregion // PersonId collection

        private void SetCollectionParents()
        {
            MyPersonIds.Parent = this; 
	}
}
}