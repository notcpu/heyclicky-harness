using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Xml;

namespace HeyClicky.Core.Documents.Spreadsheet
{
  // Token: 0x020001AE RID: 430
  [NullableContext(1)]
  [Nullable(0)]
  public sealed class SpreadsheetXmlNode
  {
    // Token: 0x06001941 RID: 6465 RVA: 0x003CF32C File Offset: 0x003CF32C
    public SpreadsheetXmlNode(string name, Dictionary<string, string> attributes)
    {
    }

    // Token: 0x170005A0 RID: 1440
    // (get) Token: 0x06001942 RID: 6466 RVA: 0x003CF358 File Offset: 0x003CF358
    public string Name { get; } = name;

    // Token: 0x170005A1 RID: 1441
    // (get) Token: 0x06001943 RID: 6467 RVA: 0x003CF360 File Offset: 0x003CF360
    public Dictionary<string, string> Attributes { get; } = attributes;

    // Token: 0x170005A2 RID: 1442
    // (get) Token: 0x06001944 RID: 6468 RVA: 0x003CF368 File Offset: 0x003CF368
    public List<SpreadsheetXmlNode> ChildNodes { get; } = new List<SpreadsheetXmlNode>();

    // Token: 0x170005A3 RID: 1443
    // (get) Token: 0x06001945 RID: 6469 RVA: 0x003CF370 File Offset: 0x003CF370
    // (set) Token: 0x06001946 RID: 6470 RVA: 0x003CF378 File Offset: 0x003CF378
    public string Text { get; set; } = "";

    // Token: 0x06001947 RID: 6471 RVA: 0x003CF384 File Offset: 0x003CF384
    [return: Nullable(2)]
    public string Attribute(string key)
    {
      string text;
      if (!this.Attributes.TryGetValue(key, out text))
      {
        return null;
      }
      return text;
    }

    // Token: 0x06001948 RID: 6472 RVA: 0x003CF3A4 File Offset: 0x003CF3A4
    [return: Nullable(2)]
    public SpreadsheetXmlNode FirstChild(string localName)
    {
      return this.ChildNodes.FirstOrDefault<SpreadsheetXmlNode>((SpreadsheetXmlNode c) => c.Name == localName);
    }

    // Token: 0x06001949 RID: 6473 RVA: 0x003CF3D8 File Offset: 0x003CF3D8
    public List<SpreadsheetXmlNode> Children(string localName)
    {
      return this.ChildNodes.Where<SpreadsheetXmlNode>((SpreadsheetXmlNode c) => c.Name == localName).ToList<SpreadsheetXmlNode>();
    }

    // Token: 0x0600194A RID: 6474 RVA: 0x003CF410 File Offset: 0x003CF410
    [return: Nullable(2)]
    public static SpreadsheetXmlNode Parse(byte[] data)
    {
      SpreadsheetXmlNode spreadsheetXmlNode = null;
      List<SpreadsheetXmlNode> list = new List<SpreadsheetXmlNode>();
      try
      {
        using (XmlReader xmlReader = SpreadsheetXlsxReader.CreateReader(data))
        {
          while (xmlReader.Read())
          {
            XmlNodeType nodeType = xmlReader.NodeType;
            if (nodeType <= XmlNodeType.CDATA)
            {
              if (nodeType != XmlNodeType.Element)
              {
                if (nodeType - XmlNodeType.Text > 1)
                {
                  continue;
                }
              }
              else
              {
                bool isEmptyElement = xmlReader.IsEmptyElement;
                SpreadsheetXmlNode spreadsheetXmlNode2 = new SpreadsheetXmlNode(SpreadsheetXlsxReader.LocalName(xmlReader.Name), SpreadsheetXlsxReader.Attributes(xmlReader));
                if (list.Count > 0)
                {
                  List<SpreadsheetXmlNode> list2 = list;
                  list2[list2.Count - 1].ChildNodes.Add(spreadsheetXmlNode2);
                }
                else
                {
                  spreadsheetXmlNode = spreadsheetXmlNode2;
                }
                if (!isEmptyElement)
                {
                  list.Add(spreadsheetXmlNode2);
                  continue;
                }
                continue;
              }
            }
            else if (nodeType - XmlNodeType.Whitespace > 1)
            {
              if (nodeType != XmlNodeType.EndElement)
              {
                continue;
              }
              if (list.Count > 0)
              {
                list.RemoveAt(list.Count - 1);
                continue;
              }
              continue;
            }
            if (list.Count > 0)
            {
              List<SpreadsheetXmlNode> list3 = list;
              SpreadsheetXmlNode spreadsheetXmlNode3 = list3[list3.Count - 1];
              spreadsheetXmlNode3.Text += xmlReader.Value;
            }
          }
        }
      }
      catch (XmlException)
      {
      }
      return spreadsheetXmlNode;
    }
  }
}
