//
// ShortNameType.cs
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
	public class ShortNameType : Altova.Xml.Node
	{
		#region Forward constructors

		public ShortNameType(XmlDocument doc) : base(doc) { SetCollectionParents(); }
		public ShortNameType(XmlNode node) : base(node) { SetCollectionParents(); }
		public ShortNameType(Altova.Xml.Node node) : base(node) { SetCollectionParents(); }
		public ShortNameType(Altova.Xml.Document doc, string namespaceURI, string prefix, string name) : base(doc, namespaceURI, prefix, name) { SetCollectionParents(); }
		#endregion // Forward constructors

		#region Value accessor methods
		public SchemaString GetValue()
		{
			return new SchemaString(GetDomNodeValue(domNode));
		}

		public void SetValue(ISchemaType newValue)
		{
			SetDomNodeValue(domNode, newValue.ToString());
		}

		public void Assign(ISchemaType newValue)
		{
			SetValue(newValue);
		}

		public SchemaString Value
		{
			get
			{
				return new SchemaString(GetDomNodeValue(domNode));
			}
			set
			{
				SetDomNodeValue(domNode, value.ToString());
			}
		}
		#endregion // Value accessor methods

		public override void AdjustPrefix()
		{

		    for (	XmlNode DOMNode = GetDomFirstChild( NodeType.Attribute, "", "languageId" );
					DOMNode != null; 
					DOMNode = GetDomNextChild( NodeType.Attribute, "", "languageId", DOMNode )
				)
			{
				InternalAdjustPrefix(DOMNode, false);
			}
		}



		#region languageId accessor methods
		public static int GetlanguageIdMinCount()
		{
			return 1;
		}

		public static int languageIdMinCount
		{
			get
			{
				return 1;
			}
		}

		public static int GetlanguageIdMaxCount()
		{
			return 1;
		}

		public static int languageIdMaxCount
		{
			get
			{
				return 1;
			}
		}

		public int GetlanguageIdCount()
		{
			return DomChildCount(NodeType.Attribute, "", "languageId");
		}

		public int languageIdCount
		{
			get
			{
				return DomChildCount(NodeType.Attribute, "", "languageId");
			}
		}

		public bool HaslanguageId()
		{
			return HasDomChild(NodeType.Attribute, "", "languageId");
		}

		public languageIdType3 NewlanguageId()
		{
			return new languageIdType3();
		}

		public languageIdType3 GetlanguageIdAt(int index)
		{
			return new languageIdType3(GetDomNodeValue(GetDomChildAt(NodeType.Attribute, "", "languageId", index)));
		}

		public XmlNode GetStartinglanguageIdCursor()
		{
			return GetDomFirstChild( NodeType.Attribute, "", "languageId" );
		}

		public XmlNode GetAdvancedlanguageIdCursor( XmlNode curNode )
		{
			return GetDomNextChild( NodeType.Attribute, "", "languageId", curNode );
		}

		public languageIdType3 GetlanguageIdValueAtCursor( XmlNode curNode )
		{
			if( curNode == null )
				  throw new Altova.Xml.XmlException("Out of range");
			else
				return new languageIdType3( curNode.Value );
		}


		public languageIdType3 GetlanguageId()
		{
			return GetlanguageIdAt(0);
		}

		public languageIdType3 languageId
		{
			get
			{
				return GetlanguageIdAt(0);
			}
		}

		public void RemovelanguageIdAt(int index)
		{
			RemoveDomChildAt(NodeType.Attribute, "", "languageId", index);
		}

		public void RemovelanguageId()
		{
			while (HaslanguageId())
				RemovelanguageIdAt(0);
		}

		public void AddlanguageId(languageIdType3 newValue)
		{
			if( newValue.IsNull() == false )
				AppendDomChild(NodeType.Attribute, "", "languageId", newValue.ToString());
		}

		public void InsertlanguageIdAt(languageIdType3 newValue, int index)
		{
			if( newValue.IsNull() == false )
				InsertDomChildAt(NodeType.Attribute, "", "languageId", index, newValue.ToString());
		}

		public void ReplacelanguageIdAt(languageIdType3 newValue, int index)
		{
			ReplaceDomChildAt(NodeType.Attribute, "", "languageId", index, newValue.ToString());
		}
		#endregion // languageId accessor methods

		#region languageId collection
        public languageIdCollection	MylanguageIds = new languageIdCollection( );

        public class languageIdCollection: IEnumerable
        {
            ShortNameType parent;
            public ShortNameType Parent
			{
				set
				{
					parent = value;
				}
			}
			public languageIdEnumerator GetEnumerator() 
			{
				return new languageIdEnumerator(parent);
			}
		
			IEnumerator IEnumerable.GetEnumerator() 
			{
				return GetEnumerator();
			}
        }

        public class languageIdEnumerator: IEnumerator 
        {
			int nIndex;
			ShortNameType parent;
			public languageIdEnumerator(ShortNameType par) 
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
				return(nIndex < parent.languageIdCount );
			}
			public languageIdType3  Current 
			{
				get 
				{
					return(parent.GetlanguageIdAt(nIndex));
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

        #endregion // languageId collection

		public void AddTextNode(SchemaString newValue)
		{
			AppendDomChild(NodeType.Text, "", "", newValue.ToString());
		}
		public void AddProcessingInstruction(SchemaString name, SchemaString newValue)
		{
			AppendDomChild(NodeType.ProcessingInstruction , "", name.ToString(), newValue.ToString());
		}
		public void AddCDataNode(SchemaString newValue)
		{
			AppendDomChild(NodeType.CData, "", "", newValue.ToString());
		}
		public void AddComment(SchemaString newValue)
		{
			AppendDomChild(NodeType.Comment, "", "", newValue.ToString());
		}
        private void SetCollectionParents()
        {
            MylanguageIds.Parent = this; 
	}
}
}