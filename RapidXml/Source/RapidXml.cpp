//
// export for dll wrapper
//
#include "rapidxml.hpp"
#include <string>
#include <assert.h>

#if defined(WIN32) || defined(_WIN32)
#define EXPORT_API __declspec(dllexport)
#else
#define EXPORT_API   
#endif

rapidxml::xml_document<>* GThisDocument = NULL;
std::string GDocumentContent;
std::string GLastErrorMessage = "";

extern "C"
{
    EXPORT_API void* LoadFromString(const char* pInContent)
    {
        if (!pInContent)
        {
            GLastErrorMessage = "内容为空";
            // 
            return NULL;
        }

        if (GThisDocument)
        {
            GLastErrorMessage = "之前的加载并未正确释放!";
            return NULL;
        }

        GThisDocument = new rapidxml::xml_document<>();

        // rapid xml需要自己保存内存
        GDocumentContent = pInContent;

        try
        {
            GThisDocument->parse<0>((char*)GDocumentContent.c_str());
        }
        catch (std::exception& e)
        {
            GLastErrorMessage = e.what();
            delete GThisDocument;

            return NULL;
        }        

        return GThisDocument;
    }

    EXPORT_API char* GetLastLoadError()
    {
        return (char*)GLastErrorMessage.c_str();
    }

    EXPORT_API void DisposeThis(void* InPtr)
    {
        assert(InPtr == (void*)GThisDocument);

        if (GThisDocument == InPtr && GThisDocument)
        {
            delete GThisDocument;
            GThisDocument = NULL;
        }
    }

    EXPORT_API void* SelectSingleNodePtr(void* InDocument, const char* pName)
    {
        assert(InDocument == GThisDocument);

        if (!InDocument || !pName || InDocument != GThisDocument)
        {
            return NULL;
        }

        rapidxml::xml_node<>* ThisNode = GThisDocument->first_node(pName);

        return ThisNode;
    }

    EXPORT_API void* SearchForChildByTagPtr(void* InDocument, void* InNodePtr, const char* pTag)
    {
        assert(InDocument && InNodePtr && pTag && InDocument == GThisDocument);

        if (!InDocument || !InNodePtr || !pTag || InDocument != GThisDocument)
        {
            return NULL;
        }

        rapidxml::xml_node<>* ThisNode = (rapidxml::xml_node<>*)InNodePtr;

        return ThisNode->first_node(pTag);
    }

    EXPORT_API bool HasAttribute(void* InDocument, void* InNodePtr, const char* pInName)
    {
        assert(InDocument && InNodePtr && pInName && InDocument == GThisDocument);

        if (!InDocument || !InNodePtr || !pInName || InDocument != GThisDocument)
        {
            return false;
        }

        rapidxml::xml_node<>* ThisNode = (rapidxml::xml_node<>*)InNodePtr;

        return ThisNode->first_attribute(pInName) != NULL;
    }

    EXPORT_API bool AttributeBool(void* InDocument, void* InNodePtr, const char* pInName)
    {
        assert(InDocument && InNodePtr && pInName && InDocument == GThisDocument);

        if (!InDocument || !InNodePtr || !pInName || InDocument != GThisDocument)
        {
            return false;
        }

        rapidxml::xml_node<>* ThisNode = (rapidxml::xml_node<>*)InNodePtr;

        rapidxml::xml_attribute<>* AttributeNode = ThisNode->first_attribute(pInName);

        if (!AttributeNode)
        {
            return false;
        }

        char* Value = AttributeNode->value();

        assert(Value != NULL);

#ifdef _MSC_VER
        return _stricmp(Value, "true") == 0;
#else
        return stricmp(Value, "true") == 0;
#endif
    }

    EXPORT_API int AttributeInt(void* InDocument, void* InNodePtr, const char* pInName)
    {
        assert(InDocument && InNodePtr && pInName && InDocument == GThisDocument);

        if (!InDocument || !InNodePtr || !pInName || InDocument != GThisDocument)
        {
            return 0;
        }

        rapidxml::xml_node<>* ThisNode = (rapidxml::xml_node<>*)InNodePtr;

        rapidxml::xml_attribute<>* AttributeNode = ThisNode->first_attribute(pInName);

        if (!AttributeNode)
        {
            return 0;
        }

        char* Value = AttributeNode->value();

        assert(Value != NULL);

        return atoi(Value);
    }

    EXPORT_API unsigned AttributeUInt(void* InDocument, void* InNodePtr, const char* pInName)
    {
        assert(InDocument && InNodePtr && pInName && InDocument == GThisDocument);

        if (!InDocument || !InNodePtr || !pInName || InDocument != GThisDocument)
        {
            return 0;
        }

        rapidxml::xml_node<>* ThisNode = (rapidxml::xml_node<>*)InNodePtr;

        rapidxml::xml_attribute<>* AttributeNode = ThisNode->first_attribute(pInName);

        if (!AttributeNode)
        {
            return 0;
        }

        char* Value = AttributeNode->value();

        assert(Value != NULL);

        return (unsigned)atoll(Value);
    }

    EXPORT_API float AttributeFloat(void* InDocument, void* InNodePtr, const char* pInName)
    {
        assert(InDocument && InNodePtr && pInName && InDocument == GThisDocument);

        if (!InDocument || !InNodePtr || !pInName || InDocument != GThisDocument)
        {
            return 0;
        }

        rapidxml::xml_node<>* ThisNode = (rapidxml::xml_node<>*)InNodePtr;

        rapidxml::xml_attribute<>* AttributeNode = ThisNode->first_attribute(pInName);

        if (!AttributeNode)
        {
            return 0;
        }

        char* Value = AttributeNode->value();

        assert(Value != NULL);

        return (float)atof(Value);
    }

    EXPORT_API void* AttributeStringPtr(void* InDocument, void* InNodePtr, const char* pInName)
    {
        assert(InDocument && InNodePtr && pInName && InDocument == GThisDocument);

        if (!InDocument || !InNodePtr || !pInName || InDocument != GThisDocument)
        {
            return NULL;
        }

        rapidxml::xml_node<>* ThisNode = (rapidxml::xml_node<>*)InNodePtr;

        rapidxml::xml_attribute<>* AttributeNode = ThisNode->first_attribute(pInName);

        if (!AttributeNode)
        {
            return NULL;
        }

        char* Value = AttributeNode->value();

        assert(Value != NULL);

        return Value;
    }

    EXPORT_API void* FirstChildNodePtr(void* InDocument, void* InNodePtr)
    {
        assert(InDocument && InNodePtr && InDocument == GThisDocument);

        if (!InDocument || !InNodePtr || InDocument != GThisDocument)
        {
            return NULL;
        }

        rapidxml::xml_node<>* ThisNode = (rapidxml::xml_node<>*)InNodePtr;

        return ThisNode->first_node();
    }

    EXPORT_API void* NextSiblingPtr(void* InDocument, void* InNodePtr)
    {
        assert(InDocument && InNodePtr && InDocument == GThisDocument);

        if (!InDocument || !InNodePtr || InDocument != GThisDocument)
        {
            return NULL;
        }

        rapidxml::xml_node<>* ThisNode = (rapidxml::xml_node<>*)InNodePtr;

        return ThisNode->next_sibling();
    }

    EXPORT_API void* NodeTagPtr(void* InDocument, void* InNodePtr)
    {
        assert(InDocument && InNodePtr && InDocument == GThisDocument);

        if (!InDocument || !InNodePtr || InDocument != GThisDocument)
        {
            return NULL;
        }

        rapidxml::xml_node<>* ThisNode = (rapidxml::xml_node<>*)InNodePtr;

        return ThisNode->name();
    }

    EXPORT_API int GetChildCount(void* InDocument, void* InNodePtr)
    {
        assert(InDocument && InNodePtr && InDocument == GThisDocument);

        if (!InDocument || !InNodePtr || InDocument != GThisDocument)
        {
            return NULL;
        }

        rapidxml::xml_node<>* ThisNode = (rapidxml::xml_node<>*)InNodePtr;
        rapidxml::xml_node<>* Child = ThisNode->first_node();

        int Result = 0;

        while (Child)
        {
            ++Result;

            Child = Child->next_sibling();
        }

        return Result;
        
    }
}
