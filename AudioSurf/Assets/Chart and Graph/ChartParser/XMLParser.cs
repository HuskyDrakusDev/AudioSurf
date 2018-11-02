using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;

namespace ChartAndGraph
{
    class XMLParser : ChartParser
    {
        XmlDocument mXmlDoc;
        XmlElement mRelativeElement;

        public XMLParser(string xml)
        {
            mXmlDoc = new XmlDocument();
            using (var reader = new StringReader(xml))
            {
                mXmlDoc.Load(reader);
            }
            mRelativeElement = mXmlDoc.DocumentElement;
        }

        public override int GetArraySize(object arr)
        {
            var element = arr as XmlElement;
            if (element == null)
                return 0;
            return element.ChildNodes.Count;
        }

        public override object GetChildObject(object obj, string name)
        {
            var node = (XmlElement)obj;
            if (name.Length <= 0)
                return obj;
            if (char.IsDigit(name[0]))  // if it is a number then find by order , atag name cannot start with a digit
            {
                int index = 0;
                if (int.TryParse(name,out index) == false)  // try parsing the number
                    return null;
                if (index < 0 || index >= node.ChildNodes.Count)
                    return null;
                return node.ChildNodes[index];
            }
            return node.SelectSingleNode(name);
        }

        public override object GetItemObject(object arr, int item)
        {
            var element = arr as XmlElement;
            if (element == null)
                return null;
            var child = element.ChildNodes[item] as XmlElement;
            return child;
        }

        object GetObjectFromRoot(XmlElement root,string name)
        {
            string[] parents = name.Split('>');
            object current = root;
            for (int i = 0; current!=null && i < parents.Length; i++)
            {
                string nextNode = parents[i];
                current = GetChildObject(current,nextNode);
            }
            return current;
        }

        public override object GetObject(string name)
        {
            return GetObjectFromRoot(mRelativeElement, name);
        }

        public override bool SetPathRelativeTo(string pathObject)
        {
            mRelativeElement = (XmlElement)GetObjectFromRoot(mXmlDoc.DocumentElement, pathObject);
            if (mRelativeElement == null)
            {
                mRelativeElement = mXmlDoc.DocumentElement;
                return false;
            }
            return true;
        }

        public override string GetChildObjectValue(object obj, string name)
        {
            var element = obj as XmlElement;
            if (element == null)
                return null;
            var child = element.SelectSingleNode(name) as XmlElement;
            if (child == null)
                return null;
            return ObjectValue(child);
        }

        public override string GetItem(object arr, int item)
        {
            var element = arr as XmlElement;
            if (element == null)
                return null;
            var child = element.ChildNodes[item] as XmlElement;
            if (child == null)
                return null;
            return ObjectValue(child);
        }

        public override string ObjectValue(object obj)
        {
            var element = obj as XmlElement;
            if (element == null)
                return null;
           return element.InnerText;
        }
    }
}
