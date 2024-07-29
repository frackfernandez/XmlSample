using System.Xml;


//UsingXmlWriter();
//UsingXmlDocument();

//string path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location),@"Resources\example.xml");
string path = @"C:\Users\User\source\repos\Xml Sample\Xml Sample\Resources\example.xml";

//UsingXmlReader(path);
//ReadUsingXmlDocument(path);
UsingXmlDocumentWithXPath(path);

void UsingXmlWriter()
{
    XmlWriter xmlWriter = XmlWriter.Create("usingXmlWriter.xml");

    xmlWriter.WriteStartDocument();

    xmlWriter.WriteStartElement("Contacts");
    xmlWriter.WriteStartElement("Contact");
    xmlWriter.WriteAttributeString("Phone", "0234587");
    xmlWriter.WriteString("John Wick");
    xmlWriter.WriteEndElement();

    xmlWriter.WriteStartElement("Contact");
    xmlWriter.WriteAttributeString("Phone", "0333333");
    xmlWriter.WriteAttributeString("Work_Phone", "023456789");
    xmlWriter.WriteString("Paul Anderson");

    xmlWriter.WriteEndDocument();
    xmlWriter.Close();
}
void UsingXmlDocument()
{
    XmlDocument xmlDoc = new XmlDocument();
    XmlNode rootNode = xmlDoc.CreateElement("Contacts");
    xmlDoc.AppendChild(rootNode);

    XmlNode contactNode = xmlDoc.CreateElement("Contact");
    XmlAttribute attribute = xmlDoc.CreateAttribute("Phone");
    attribute.Value = "02344534";
    contactNode.Attributes.Append(attribute);
    contactNode.InnerText = "John Wick";
    rootNode.AppendChild(contactNode);

    contactNode = xmlDoc.CreateElement("Contact");
    attribute = xmlDoc.CreateAttribute("Phone");
    attribute.Value = "023444444";
    contactNode.Attributes.Append(attribute);
    attribute = xmlDoc.CreateAttribute("Work_Phone");
    attribute.Value = "023333332";
    contactNode.Attributes.Append(attribute);

    contactNode.InnerText = "Paul Anderson";
    rootNode.AppendChild(contactNode);

    xmlDoc.Save("usingXmlDocument.xml");
}


void UsingXmlReader(string path)
{
    XmlReader xmlReader = XmlReader.Create(path);

    while (xmlReader.Read())
    {
        if ((xmlReader.NodeType == XmlNodeType.Element) && (xmlReader.Name == "project"))
        {
            if (xmlReader.HasAttributes)
                Console.WriteLine(xmlReader.GetAttribute("name") + " : " + xmlReader.GetAttribute("launch"));
        }
        else if ((xmlReader.NodeType == XmlNodeType.Element) && (xmlReader.Name == "edition"))
        {
            if (xmlReader.HasAttributes)
                Console.WriteLine(xmlReader.GetAttribute("language"));
        }
    }
    Console.ReadKey();
}
void ReadUsingXmlDocument(string path)
{
    XmlDocument xmlDoc = new XmlDocument();
    xmlDoc.Load(path);

    foreach (XmlNode xmlNode in xmlDoc.DocumentElement.ChildNodes[0].ChildNodes)
    {
        Console.WriteLine(xmlNode.Attributes["name"].Value + " : " + xmlNode.Attributes["launch"].Value);

        foreach (XmlNode xmlNodeItem in xmlNode.FirstChild.ChildNodes)
        {
            Console.WriteLine(xmlNodeItem.Attributes["language"].Value);

            Console.WriteLine("Inner Text: " + xmlNodeItem.InnerText);
            Console.WriteLine("Inner Xml: " + xmlNodeItem.InnerXml);
            Console.WriteLine("Outer Xmk: " + xmlNodeItem.OuterXml);
        }

    }
}
void UsingXmlDocumentWithXPath(string path)
{
    XmlDocument xmlDoc = new XmlDocument();
    xmlDoc.Load(path);

    XmlNodeList itemNodes = xmlDoc.SelectNodes("/wikimedia//projects//project");
    foreach (XmlNode itemNode in itemNodes)
    {
        Console.WriteLine(itemNode.Attributes["name"].Value + " : " + itemNode.Attributes["launch"].Value);

        foreach (XmlNode item in itemNode.SelectSingleNode("editions"))
        {
            Console.WriteLine(item.Attributes["language"].Value);
        }
    }

    Console.ReadKey();
}
