//
using UnityEngine;
using System;
using System.Xml;
using Mono.Xml;
using RapidXml;

public class XmlPerformanceTests : MonoBehaviour
{
    [HideInInspector]
    public string XmlContent;

    void Awake()
    {
        Debug.Log(Application.dataPath);

        string path = System.IO.Path.Combine(Application.dataPath, @"Plugins/Xml/Tests/Test.xml");

        Debug.Log(path);

        XmlContent = System.Text.Encoding.UTF8.GetString(System.IO.File.ReadAllBytes(path));
    }

    void Update()
    {
        for( int i=0; i<100; ++i )
        {
            TestRapid();
            TestSystem();
            TestMono();
        }
    }

    void TestRapid()
    {
        using (RapidXmlParser xml = new RapidXmlParser())
        {
            xml.Load(XmlContent);


        }
    }

    void TestSystem()
    {
        XmlDocument xml = new XmlDocument();

        xml.LoadXml(XmlContent);


    }

    void TestMono()
    {
        SecurityParser xml = new SecurityParser();

        xml.LoadXml(XmlContent);

    }
}