using System;
using System.Collections;
using System.Dynamic;
using System.Linq;
using System.Xml.Linq;

namespace EventorResults.Core
{
    public class DynamicXml : DynamicObject, IEnumerable
    {
        readonly XElement _element;

        public DynamicXml(string xml)
        {
            _element = XElement.Parse(xml);
        }

        public DynamicXml(XElement xElement)
        {
            _element = xElement;
        }

        public override bool TryGetMember(GetMemberBinder binder, out object result)
        {
            if (_element == null)
            {
                result = null;
                return false;
            }

            if (binder.Name == "XElementName")
            {
                result = _element.Name;
                return true;
            }

            var sub = _element.Element(binder.Name);

            if (sub == null)
            {
                var att = _element.Attribute(binder.Name);

                if (att != null)
                {
                    result = att.Value;
                    return true;
                }

                result = null;

                var ignoreMissing = binder.Name == "TimeDiff" || 
                    binder.Name == "ResultPosition" || 
                    binder.Name == "Time" || 
                    binder.Name == "BirthDate" || 
                    binder.Name == "noOfEntries" ||
                    binder.Name == "noOfStarts" ||
                    binder.Name == "sex" ||
                    binder.Name == "OrganisationId";

                return ignoreMissing;
            }
            
            result = new DynamicXml(sub);
            return true;
        }

        public override string ToString()
        {
            return _element != null ? _element.Value : string.Empty;
        }

        public IEnumerator GetEnumerator()
        {
            return _element.Elements().Select(child => new DynamicXml(child)).GetEnumerator();
        }

        public override bool TryConvert(ConvertBinder binder, out object result)
        {
            try
            {
                result = Convert.ChangeType(_element.Value, binder.ReturnType);
                return true;
            }
            catch { }

            return base.TryConvert(binder, out result);
        }
    }
}