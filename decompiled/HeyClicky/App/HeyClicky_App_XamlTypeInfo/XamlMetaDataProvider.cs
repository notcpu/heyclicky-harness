using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using Microsoft.UI.Xaml.Markup;
using Windows.Foundation.Metadata;
using WinRT;
using WinRT.HeyClickyVtableClasses;

namespace HeyClicky.App.HeyClicky_App_XamlTypeInfo
{
  // Token: 0x02000084 RID: 132
  [GeneratedCode("Microsoft.UI.Xaml.Markup.Compiler", " 3.0.0.2609")]
  [DebuggerNonUserCode]
  [WinRTRuntimeClassName("Microsoft.UI.Xaml.Markup.IXamlMetadataProvider")]
  [WinRTExposedType(typeof(HeyClicky_App_HeyClicky_App_XamlTypeInfo_XamlMetaDataProviderWinRTTypeDetails))]
  public sealed class XamlMetaDataProvider : IXamlMetadataProvider
  {
    // Token: 0x170000A7 RID: 167
    // (get) Token: 0x060004FF RID: 1279 RVA: 0x0052C098 File Offset: 0x0052C098
    private XamlTypeInfoProvider Provider
    {
      get
      {
        if (this._provider == null)
        {
          this._provider = new XamlTypeInfoProvider();
        }
        return this._provider;
      }
    }

    // Token: 0x06000500 RID: 1280 RVA: 0x0052C0B4 File Offset: 0x0052C0B4
    [DefaultOverload]
    public IXamlType GetXamlType(Type type)
    {
      return this.Provider.GetXamlTypeByType(type);
    }

    // Token: 0x06000501 RID: 1281 RVA: 0x0052C0C4 File Offset: 0x0052C0C4
    public IXamlType GetXamlType(string fullName)
    {
      return this.Provider.GetXamlTypeByName(fullName);
    }

    // Token: 0x06000502 RID: 1282 RVA: 0x0052C0D4 File Offset: 0x0052C0D4
    public XmlnsDefinition[] GetXmlnsDefinitions()
    {
      return new XmlnsDefinition[0];
    }

    // Token: 0x040000FF RID: 255
    private XamlTypeInfoProvider _provider;
  }
}
