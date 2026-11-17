using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using Microsoft.UI.Xaml.Markup;
using WinRT;
using WinRT.HeyClickyVtableClasses;

namespace HeyClicky.App.HeyClicky_App_XamlTypeInfo
{
  // Token: 0x02000086 RID: 134
  [GeneratedCode("Microsoft.UI.Xaml.Markup.Compiler", " 3.0.0.2609")]
  [DebuggerNonUserCode]
  [WinRTRuntimeClassName("Microsoft.UI.Xaml.Markup.IXamlType")]
  [WinRTExposedType(typeof(HeyClicky_App_HeyClicky_App_XamlTypeInfo_XamlSystemBaseTypeWinRTTypeDetails))]
  internal class XamlSystemBaseType : IXamlType
  {
    // Token: 0x06000523 RID: 1315 RVA: 0x0052CC38 File Offset: 0x0052CC38
    public XamlSystemBaseType(string fullName, Type underlyingType)
    {
      this._fullName = fullName;
      this._underlyingType = underlyingType;
    }

    // Token: 0x170000A9 RID: 169
    // (get) Token: 0x06000524 RID: 1316 RVA: 0x0052CC50 File Offset: 0x0052CC50
    public string FullName
    {
      get
      {
        return this._fullName;
      }
    }

    // Token: 0x170000AA RID: 170
    // (get) Token: 0x06000525 RID: 1317 RVA: 0x0052CC58 File Offset: 0x0052CC58
    public Type UnderlyingType
    {
      get
      {
        return this._underlyingType;
      }
    }

    // Token: 0x170000AB RID: 171
    // (get) Token: 0x06000526 RID: 1318 RVA: 0x0052CC60 File Offset: 0x0052CC60
    public virtual IXamlType BaseType
    {
      get
      {
        throw new NotImplementedException();
      }
    }

    // Token: 0x170000AC RID: 172
    // (get) Token: 0x06000527 RID: 1319 RVA: 0x0052CC68 File Offset: 0x0052CC68
    public virtual IXamlMember ContentProperty
    {
      get
      {
        throw new NotImplementedException();
      }
    }

    // Token: 0x06000528 RID: 1320 RVA: 0x0052CC70 File Offset: 0x0052CC70
    public virtual IXamlMember GetMember(string name)
    {
      throw new NotImplementedException();
    }

    // Token: 0x170000AD RID: 173
    // (get) Token: 0x06000529 RID: 1321 RVA: 0x0052CC78 File Offset: 0x0052CC78
    public virtual bool IsArray
    {
      get
      {
        throw new NotImplementedException();
      }
    }

    // Token: 0x170000AE RID: 174
    // (get) Token: 0x0600052A RID: 1322 RVA: 0x0052CC80 File Offset: 0x0052CC80
    public virtual bool IsCollection
    {
      get
      {
        throw new NotImplementedException();
      }
    }

    // Token: 0x170000AF RID: 175
    // (get) Token: 0x0600052B RID: 1323 RVA: 0x0052CC88 File Offset: 0x0052CC88
    public virtual bool IsConstructible
    {
      get
      {
        throw new NotImplementedException();
      }
    }

    // Token: 0x170000B0 RID: 176
    // (get) Token: 0x0600052C RID: 1324 RVA: 0x0052CC90 File Offset: 0x0052CC90
    public virtual bool IsDictionary
    {
      get
      {
        throw new NotImplementedException();
      }
    }

    // Token: 0x170000B1 RID: 177
    // (get) Token: 0x0600052D RID: 1325 RVA: 0x0052CC98 File Offset: 0x0052CC98
    public virtual bool IsMarkupExtension
    {
      get
      {
        throw new NotImplementedException();
      }
    }

    // Token: 0x170000B2 RID: 178
    // (get) Token: 0x0600052E RID: 1326 RVA: 0x0052CCA0 File Offset: 0x0052CCA0
    public virtual bool IsBindable
    {
      get
      {
        throw new NotImplementedException();
      }
    }

    // Token: 0x170000B3 RID: 179
    // (get) Token: 0x0600052F RID: 1327 RVA: 0x0052CCA8 File Offset: 0x0052CCA8
    public virtual bool IsReturnTypeStub
    {
      get
      {
        throw new NotImplementedException();
      }
    }

    // Token: 0x170000B4 RID: 180
    // (get) Token: 0x06000530 RID: 1328 RVA: 0x0052CCB0 File Offset: 0x0052CCB0
    public virtual bool IsLocalType
    {
      get
      {
        throw new NotImplementedException();
      }
    }

    // Token: 0x170000B5 RID: 181
    // (get) Token: 0x06000531 RID: 1329 RVA: 0x0052CCB8 File Offset: 0x0052CCB8
    public virtual IXamlType ItemType
    {
      get
      {
        throw new NotImplementedException();
      }
    }

    // Token: 0x170000B6 RID: 182
    // (get) Token: 0x06000532 RID: 1330 RVA: 0x0052CCC0 File Offset: 0x0052CCC0
    public virtual IXamlType KeyType
    {
      get
      {
        throw new NotImplementedException();
      }
    }

    // Token: 0x170000B7 RID: 183
    // (get) Token: 0x06000533 RID: 1331 RVA: 0x0052CCC8 File Offset: 0x0052CCC8
    public virtual IXamlType BoxedType
    {
      get
      {
        throw new NotImplementedException();
      }
    }

    // Token: 0x06000534 RID: 1332 RVA: 0x0052CCD0 File Offset: 0x0052CCD0
    public virtual object ActivateInstance()
    {
      throw new NotImplementedException();
    }

    // Token: 0x06000535 RID: 1333 RVA: 0x0052CCD8 File Offset: 0x0052CCD8
    public virtual void AddToMap(object instance, object key, object item)
    {
      throw new NotImplementedException();
    }

    // Token: 0x06000536 RID: 1334 RVA: 0x0052CCE0 File Offset: 0x0052CCE0
    public virtual void AddToVector(object instance, object item)
    {
      throw new NotImplementedException();
    }

    // Token: 0x06000537 RID: 1335 RVA: 0x0052CCE8 File Offset: 0x0052CCE8
    public virtual void RunInitializer()
    {
      throw new NotImplementedException();
    }

    // Token: 0x06000538 RID: 1336 RVA: 0x0052CCF0 File Offset: 0x0052CCF0
    public virtual object CreateFromString(string input)
    {
      throw new NotImplementedException();
    }

    // Token: 0x04000106 RID: 262
    private string _fullName;

    // Token: 0x04000107 RID: 263
    private Type _underlyingType;
  }
}
