//
// ClassResultType.cs
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
	public class ClassResultType : Altova.Xml.Node
	{
		#region Forward constructors

		public ClassResultType(XmlDocument doc) : base(doc) { SetCollectionParents(); }
		public ClassResultType(XmlNode node) : base(node) { SetCollectionParents(); }
		public ClassResultType(Altova.Xml.Node node) : base(node) { SetCollectionParents(); }
		public ClassResultType(Altova.Xml.Document doc, string namespaceURI, string prefix, string name) : base(doc, namespaceURI, prefix, name) { SetCollectionParents(); }
		#endregion // Forward constructors

		public override void AdjustPrefix()
		{

		    for (	XmlNode DOMNode = GetDomFirstChild( NodeType.Element, "", "EventClass" );
					DOMNode != null; 
					DOMNode = GetDomNextChild( NodeType.Element, "", "EventClass", DOMNode )
				)
			{
				InternalAdjustPrefix(DOMNode, true);
				new EventClassType(DOMNode).AdjustPrefix();
			}

		    for (	XmlNode DOMNode = GetDomFirstChild( NodeType.Element, "", "PersonResult" );
					DOMNode != null; 
					DOMNode = GetDomNextChild( NodeType.Element, "", "PersonResult", DOMNode )
				)
			{
				InternalAdjustPrefix(DOMNode, true);
				new PersonResultType(DOMNode).AdjustPrefix();
			}
		}



		#region EventClass accessor methods
		public static int GetEventClassMinCount()
		{
			return 1;
		}

		public static int EventClassMinCount
		{
			get
			{
				return 1;
			}
		}

		public static int GetEventClassMaxCount()
		{
			return 1;
		}

		public static int EventClassMaxCount
		{
			get
			{
				return 1;
			}
		}

		public int GetEventClassCount()
		{
			return DomChildCount(NodeType.Element, "", "EventClass");
		}

		public int EventClassCount
		{
			get
			{
				return DomChildCount(NodeType.Element, "", "EventClass");
			}
		}

		public bool HasEventClass()
		{
			return HasDomChild(NodeType.Element, "", "EventClass");
		}

		public EventClassType NewEventClass()
		{
			return new EventClassType(domNode.OwnerDocument.CreateElement("EventClass", ""));
		}

		public EventClassType GetEventClassAt(int index)
		{
			return new EventClassType(GetDomChildAt(NodeType.Element, "", "EventClass", index));
		}

		public XmlNode GetStartingEventClassCursor()
		{
			return GetDomFirstChild( NodeType.Element, "", "EventClass" );
		}

		public XmlNode GetAdvancedEventClassCursor( XmlNode curNode )
		{
			return GetDomNextChild( NodeType.Element, "", "EventClass", curNode );
		}

		public EventClassType GetEventClassValueAtCursor( XmlNode curNode )
		{
			if( curNode == null )
				  throw new Altova.Xml.XmlException("Out of range");
			else
				return new EventClassType( curNode );
		}


		public EventClassType GetEventClass()
		{
			return GetEventClassAt(0);
		}

		public EventClassType EventClass
		{
			get
			{
				return GetEventClassAt(0);
			}
		}

		public void RemoveEventClassAt(int index)
		{
			RemoveDomChildAt(NodeType.Element, "", "EventClass", index);
		}

		public void RemoveEventClass()
		{
			while (HasEventClass())
				RemoveEventClassAt(0);
		}

		public void AddEventClass(EventClassType newValue)
		{
			AppendDomElement("", "EventClass", newValue);
		}

		public void InsertEventClassAt(EventClassType newValue, int index)
		{
			InsertDomElementAt("", "EventClass", index, newValue);
		}

		public void ReplaceEventClassAt(EventClassType newValue, int index)
		{
			ReplaceDomElementAt("", "EventClass", index, newValue);
		}
		#endregion // EventClass accessor methods

		#region EventClass collection
        public EventClassCollection	MyEventClasss = new EventClassCollection( );

        public class EventClassCollection: IEnumerable
        {
            ClassResultType parent;
            public ClassResultType Parent
			{
				set
				{
					parent = value;
				}
			}
			public EventClassEnumerator GetEnumerator() 
			{
				return new EventClassEnumerator(parent);
			}
		
			IEnumerator IEnumerable.GetEnumerator() 
			{
				return GetEnumerator();
			}
        }

        public class EventClassEnumerator: IEnumerator 
        {
			int nIndex;
			ClassResultType parent;
			public EventClassEnumerator(ClassResultType par) 
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
				return(nIndex < parent.EventClassCount );
			}
			public EventClassType  Current 
			{
				get 
				{
					return(parent.GetEventClassAt(nIndex));
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

        #endregion // EventClass collection

		#region PersonResult accessor methods
		public static int GetPersonResultMinCount()
		{
			return 1;
		}

		public static int PersonResultMinCount
		{
			get
			{
				return 1;
			}
		}

		public static int GetPersonResultMaxCount()
		{
			return 1;
		}

		public static int PersonResultMaxCount
		{
			get
			{
				return 1;
			}
		}

		public int GetPersonResultCount()
		{
			return DomChildCount(NodeType.Element, "", "PersonResult");
		}

		public int PersonResultCount
		{
			get
			{
				return DomChildCount(NodeType.Element, "", "PersonResult");
			}
		}

		public bool HasPersonResult()
		{
			return HasDomChild(NodeType.Element, "", "PersonResult");
		}

		public PersonResultType NewPersonResult()
		{
			return new PersonResultType(domNode.OwnerDocument.CreateElement("PersonResult", ""));
		}

		public PersonResultType GetPersonResultAt(int index)
		{
			return new PersonResultType(GetDomChildAt(NodeType.Element, "", "PersonResult", index));
		}

		public XmlNode GetStartingPersonResultCursor()
		{
			return GetDomFirstChild( NodeType.Element, "", "PersonResult" );
		}

		public XmlNode GetAdvancedPersonResultCursor( XmlNode curNode )
		{
			return GetDomNextChild( NodeType.Element, "", "PersonResult", curNode );
		}

		public PersonResultType GetPersonResultValueAtCursor( XmlNode curNode )
		{
			if( curNode == null )
				  throw new Altova.Xml.XmlException("Out of range");
			else
				return new PersonResultType( curNode );
		}


		public PersonResultType GetPersonResult()
		{
			return GetPersonResultAt(0);
		}

		public PersonResultType PersonResult
		{
			get
			{
				return GetPersonResultAt(0);
			}
		}

		public void RemovePersonResultAt(int index)
		{
			RemoveDomChildAt(NodeType.Element, "", "PersonResult", index);
		}

		public void RemovePersonResult()
		{
			while (HasPersonResult())
				RemovePersonResultAt(0);
		}

		public void AddPersonResult(PersonResultType newValue)
		{
			AppendDomElement("", "PersonResult", newValue);
		}

		public void InsertPersonResultAt(PersonResultType newValue, int index)
		{
			InsertDomElementAt("", "PersonResult", index, newValue);
		}

		public void ReplacePersonResultAt(PersonResultType newValue, int index)
		{
			ReplaceDomElementAt("", "PersonResult", index, newValue);
		}
		#endregion // PersonResult accessor methods

		#region PersonResult collection
        public PersonResultCollection	MyPersonResults = new PersonResultCollection( );

        public class PersonResultCollection: IEnumerable
        {
            ClassResultType parent;
            public ClassResultType Parent
			{
				set
				{
					parent = value;
				}
			}
			public PersonResultEnumerator GetEnumerator() 
			{
				return new PersonResultEnumerator(parent);
			}
		
			IEnumerator IEnumerable.GetEnumerator() 
			{
				return GetEnumerator();
			}
        }

        public class PersonResultEnumerator: IEnumerator 
        {
			int nIndex;
			ClassResultType parent;
			public PersonResultEnumerator(ClassResultType par) 
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
				return(nIndex < parent.PersonResultCount );
			}
			public PersonResultType  Current 
			{
				get 
				{
					return(parent.GetPersonResultAt(nIndex));
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

        #endregion // PersonResult collection

        private void SetCollectionParents()
        {
            MyEventClasss.Parent = this; 
            MyPersonResults.Parent = this; 
	}
}
}