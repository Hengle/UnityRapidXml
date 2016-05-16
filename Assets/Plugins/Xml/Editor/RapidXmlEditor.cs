// Tests editor
using UnityEngine;
using UnityEditor;

namespace RapidXml
{
    public class RapidXmlTests
    {
        [MenuItem("Tests/测试XML")]
        static void TestRapidXml()
        {
            using (RapidXmlParser xml = new RapidXmlParser())
            {
                try
                {
                    string InContent = System.Text.Encoding.UTF8.GetString(System.IO.File.ReadAllBytes(@"G:\xxx.xml"));

                    xml.Load(InContent);

                    NodeElement projectNode = xml.FirstNode("Project");

                    NodeElement.EditorAssert(projectNode.IsValid());

                    NodeElement templateObjectListNode = projectNode.FirstNode("TemplateObjectList");
                    NodeElement actionNode = projectNode.FirstNode("Action");
                    NodeElement refParamNode = projectNode.FirstNode("RefParamList");

                    NodeElement.EditorAssert(templateObjectListNode.IsValid());
                    NodeElement.EditorAssert(actionNode.IsValid());
                    NodeElement.EditorAssert(refParamNode.IsValid());
                }
                catch (System.Exception e)
                {
                    UnityEngine.Debug.LogError(e.Message);
                }
            }
        }
    }

}

