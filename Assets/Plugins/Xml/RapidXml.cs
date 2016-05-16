//
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Diagnostics;

#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;
#endif

public struct NodeElement
{
    public RapidXml Document;
    public IntPtr NativeNodePtr;

    public bool IsValid()
    {
        return Document != null && NativeNodePtr != IntPtr.Zero;
    }

    [Conditional("UNITY_EDITOR")]
    public static void EditorAssert(bool bInCondition)
    {
        if(!bInCondition)
        {
            UnityEngine.Debug.DebugBreak();
        }
    }

    public NodeElement SearchForChildByTag(string InTag)
    {
        EditorAssert(IsValid());

        return Document.SearchForChildByTag(NativeNodePtr, InTag);
    }

    public NodeElement FirstChildNode()
    {
        EditorAssert(IsValid());

        return Document.FirstChildNode(NativeNodePtr);
    }

    public NodeElement NextSibling()
    {
        EditorAssert(IsValid());

        return Document.NextSibling(NativeNodePtr);
    }

    public bool HasAttribute(String InName)
    {
        EditorAssert(IsValid());

        return RapidXml.HasAttribute(Document.NativeDocumentPtr, NativeNodePtr, InName);
    }

    public bool AttributeBool(String InName)
    {
        EditorAssert(IsValid());

        return RapidXml.AttributeBool(Document.NativeDocumentPtr, NativeNodePtr, InName);
    }

    public int AttributeInt(string InName)
    {
        EditorAssert(IsValid());

        return RapidXml.AttributeInt(Document.NativeDocumentPtr, NativeNodePtr, InName);
    }

    public uint AttributeUInt(string InName)
    {
        EditorAssert(IsValid());

        return RapidXml.AttributeUInt(Document.NativeDocumentPtr, NativeNodePtr, InName);
    }

    public float AttributeFloat(String InName)
    {
        EditorAssert(IsValid());

        return RapidXml.AttributeFloat(Document.NativeDocumentPtr, NativeNodePtr, InName);
    }

    public string AttributeString(string InName)
    {
        EditorAssert(IsValid());

        return RapidXml.AttributeString(Document.NativeDocumentPtr, NativeNodePtr, InName);
    }

    public string Attribute(string InName)
    {
        EditorAssert(IsValid());

        return RapidXml.AttributeString(Document.NativeDocumentPtr, NativeNodePtr, InName);
    }

    public string Tag
    {
        get
        {
            EditorAssert(IsValid());

            return RapidXml.NodeTag(Document.NativeDocumentPtr, NativeNodePtr);
        }
    }

    public int Count
    {
        get
        {
            EditorAssert(IsValid());

            return RapidXml.GetChildCount(Document.NativeDocumentPtr, NativeNodePtr);
        }
    }
}

public class RapidXml : IDisposable    
{
    public const string PluginName = "RapidXml";

    public IntPtr NativeDocumentPtr = IntPtr.Zero;

    public void Load(string InContent)
    {
        NativeDocumentPtr = LoadFromString(InContent);

        if( NativeDocumentPtr == IntPtr.Zero )
        {
            // load error
            string ErrorMessage = Marshal.PtrToStringAnsi(GetLastLoadError());             

            throw new Exception(ErrorMessage);
        }
    }

    public void Dispose()
    {
        if(NativeDocumentPtr!=IntPtr.Zero)
        {
            DisposeThis(NativeDocumentPtr);
            NativeDocumentPtr = IntPtr.Zero;
        }
    }

    public NodeElement SelectSingleNode(string InName)
    {
        NodeElement Element = new NodeElement();

        Element.Document = this;
        Element.NativeNodePtr = SelectSingleNodePtr(NativeDocumentPtr, InName);

        return Element;
    }

    public NodeElement SearchForChildByTag(IntPtr InPtr, string InTag)
    {
        NodeElement Element = new NodeElement();
        Element.Document = this;
        Element.NativeNodePtr = SearchForChildByTagPtr(NativeDocumentPtr, InPtr, InTag);

        return Element;
    }

    public NodeElement FirstChildNode(IntPtr InPtr)
    {
        NodeElement Element = new NodeElement();
        Element.Document = this;
        Element.NativeNodePtr = FirstChildNodePtr(NativeDocumentPtr, InPtr);

        return Element;
    }

    public NodeElement NextSibling(IntPtr InPtr)
    {
        NodeElement Element = new NodeElement();
        Element.Document = this;
        Element.NativeNodePtr = NextSiblingPtr(NativeDocumentPtr, InPtr);

        return Element;
    }

    public static string AttributeString(IntPtr InDocument, IntPtr InNativePtr, String InName)
    {
        IntPtr Result = AttributeStringPtr(InDocument, InNativePtr, InName);
        return Result != IntPtr.Zero?Marshal.PtrToStringAnsi(Result):""; 
    }
    
    public static string NodeTag(IntPtr InDocument, IntPtr InNativePtr)
    {
        IntPtr Result = NodeTagPtr(InDocument, InNativePtr);
        return Result != IntPtr.Zero ? Marshal.PtrToStringAnsi(Result) : "";
    }

#if UNITY_IPHONE && !UNITY_EDITOR
    [DllImport("__Internal")]
#else
    [DllImport(PluginName, CallingConvention = CallingConvention.Cdecl)]
#endif
    private static extern IntPtr LoadFromString([MarshalAs(UnmanagedType.LPStr)]string InContent);

#if UNITY_IPHONE && !UNITY_EDITOR
    [DllImport("__Internal")]
#else
    [DllImport(PluginName, CallingConvention = CallingConvention.Cdecl)]
#endif
    private static extern IntPtr GetLastLoadError();

#if UNITY_IPHONE && !UNITY_EDITOR
    [DllImport("__Internal")]
#else
    [DllImport(PluginName, CallingConvention = CallingConvention.Cdecl)]
#endif
    private static extern void DisposeThis(IntPtr InDocument);

#if UNITY_IPHONE && !UNITY_EDITOR
    [DllImport("__Internal")]
#else
    [DllImport(PluginName, CallingConvention = CallingConvention.Cdecl)]
#endif
    private static extern IntPtr SelectSingleNodePtr(IntPtr InDocument, [MarshalAs(UnmanagedType.LPStr)]string InName);

#if UNITY_IPHONE && !UNITY_EDITOR
    [DllImport("__Internal")]
#else
    [DllImport(PluginName, CallingConvention = CallingConvention.Cdecl)]
#endif
    private static extern IntPtr SearchForChildByTagPtr(IntPtr InDocument, IntPtr InElementPtr, [MarshalAs(UnmanagedType.LPStr)]string InTag);

#if UNITY_IPHONE && !UNITY_EDITOR
    [DllImport("__Internal")]
#else
    [DllImport(PluginName, CallingConvention = CallingConvention.Cdecl)]
#endif
    public static extern bool HasAttribute(IntPtr InDocument, IntPtr InNativePtr, String InName);

#if UNITY_IPHONE && !UNITY_EDITOR
    [DllImport("__Internal")]
#else
    [DllImport(PluginName, CallingConvention = CallingConvention.Cdecl)]
#endif
    public static extern bool AttributeBool(IntPtr InDocument, IntPtr InNativePtr, String InName);

#if UNITY_IPHONE && !UNITY_EDITOR
    [DllImport("__Internal")]
#else
    [DllImport(PluginName, CallingConvention = CallingConvention.Cdecl)]
#endif
    public static extern int AttributeInt(IntPtr InDocument, IntPtr InNativePtr, String InName);

#if UNITY_IPHONE && !UNITY_EDITOR
    [DllImport("__Internal")]
#else
    [DllImport(PluginName, CallingConvention = CallingConvention.Cdecl)]
#endif
    public static extern uint AttributeUInt(IntPtr InDocument, IntPtr InNativePtr, String InName);

#if UNITY_IPHONE && !UNITY_EDITOR
    [DllImport("__Internal")]
#else
    [DllImport(PluginName, CallingConvention = CallingConvention.Cdecl)]
#endif
    public static extern float AttributeFloat(IntPtr InDocument, IntPtr InNativePtr, String InName);

#if UNITY_IPHONE && !UNITY_EDITOR
    [DllImport("__Internal")]
#else
    [DllImport(PluginName, CallingConvention = CallingConvention.Cdecl)]
#endif
    private static extern IntPtr AttributeStringPtr(IntPtr InDocument, IntPtr InNativePtr, String InName);

#if UNITY_IPHONE && !UNITY_EDITOR
    [DllImport("__Internal")]
#else
    [DllImport(PluginName, CallingConvention = CallingConvention.Cdecl)]
#endif
    private static extern IntPtr FirstChildNodePtr(IntPtr InDocument, IntPtr InNativePtr);

#if UNITY_IPHONE && !UNITY_EDITOR
    [DllImport("__Internal")]
#else
    [DllImport(PluginName, CallingConvention = CallingConvention.Cdecl)]
#endif
    private static extern IntPtr NextSiblingPtr(IntPtr InDocument, IntPtr InNativePtr);

#if UNITY_IPHONE && !UNITY_EDITOR
    [DllImport("__Internal")]
#else
    [DllImport(PluginName, CallingConvention = CallingConvention.Cdecl)]
#endif
    private static extern IntPtr NodeTagPtr(IntPtr InDocument, IntPtr InNativePtr);

#if UNITY_IPHONE && !UNITY_EDITOR
    [DllImport("__Internal")]
#else
    [DllImport(PluginName, CallingConvention = CallingConvention.Cdecl)]
#endif
    public static extern int GetChildCount(IntPtr InDocument, IntPtr InNativePtr);
}


#if UNITY_EDITOR

public class RapidXmlTests
{
    [MenuItem("SGameTools/测试XML")]
    static void TestRapidXml()
    {
        using (RapidXml xml = new RapidXml())
        {
            try
            {
                string InContent = System.Text.Encoding.UTF8.GetString(System.IO.File.ReadAllBytes(@"G:\hurt.xml"));

                xml.Load(InContent);

                NodeElement projectNode = xml.SelectSingleNode("Project");

                NodeElement.EditorAssert(projectNode.IsValid());

                NodeElement templateObjectListNode = projectNode.SearchForChildByTag("TemplateObjectList");
                NodeElement actionNode = projectNode.SearchForChildByTag("Action");
                NodeElement refParamNode = projectNode.SearchForChildByTag("RefParamList");

                NodeElement.EditorAssert(templateObjectListNode.IsValid());
                NodeElement.EditorAssert(actionNode.IsValid());
                NodeElement.EditorAssert(refParamNode.IsValid());
            }
            catch(Exception e)
            {
                UnityEngine.Debug.LogError(e.Message);
            }
        }
    }
}

#endif