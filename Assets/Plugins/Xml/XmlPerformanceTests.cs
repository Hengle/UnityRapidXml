//
using UnityEngine;
using System;
using System.Xml;
using Mono.Xml;
using RapidXml;
using System.Security;

public class XmlPerformanceTests : MonoBehaviour
{
    [HideInInspector]
    public string XmlContent;

    RapidXmlParser xmlRapid = new RapidXmlParser();
    XmlDocument xmlSystem = new XmlDocument();
    SecurityParser xmlMono = new SecurityParser();

    void Awake()
    {
        string path = System.IO.Path.Combine(Application.dataPath, @"Plugins/Xml/Tests/Test.xml");

        Debug.Log(path);

        XmlContent = System.Text.Encoding.UTF8.GetString(System.IO.File.ReadAllBytes(path));

        xmlRapid.Load(XmlContent);
        xmlSystem.LoadXml(XmlContent);
        xmlMono.LoadXml(XmlContent);
    }

    void OnDestroy()
    {
        xmlRapid.Dispose();
    }

    void Update()
    {
     //   TestParse();
        TestVisit();
    }

    void TestParse()
    {
        for (int i = 0; i < 10; ++i)
        {
            TestParse_Rapid();
            TestParse_System();
            TestParse_Mono();
        }
    }

    void TestParse_Rapid()
    {
        using (RapidXmlParser xml = new RapidXmlParser())
        {
            xml.Load(XmlContent);


        }
    }

    void TestParse_System()
    {
        XmlDocument xml = new XmlDocument();

        xml.LoadXml(XmlContent);


    }

    void TestParse_Mono()
    {
        SecurityParser xml = new SecurityParser();

        xml.LoadXml(XmlContent);
    }

    void TestVisit()
    {
        for( int i=0; i<100; ++i )
        {
            TestVisit_Rapid();
            TestVisit_System();
            TestVisit_Mono();
        }
    }

    void TestVisit_Rapid()
    {
        NodeElement RootNode = xmlRapid.FirstNode();

        NodeElement Node = RootNode.FirstNode();

        while( Node.IsValid() )
        {
          //  Debug.Log("Rapid " + Node.GetName());

            Node = Node.NextSibling();
        }
    }

    void TestVisit_System()
    {
        XmlNode RootElement = xmlSystem.ChildNodes[0];
        var Iter = RootElement.GetEnumerator();
        while( Iter.MoveNext() )
        {
            XmlNode Node = Iter.Current as XmlNode;
         //   Debug.Log("System " + Node.Name);
        }
    }

    void TestVisit_Mono()
    {
        for( int i=0; i< xmlMono.ToXml().Children.Count; ++i )
        {
            SecurityElement Node = xmlMono.ToXml().Children[i] as SecurityElement;

         //   Debug.Log("Mono " + Node.Tag);
        }
    }
}