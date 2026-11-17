using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using Microsoft.UI.Xaml.Markup;
using WinRT;
using WinRT.HeyClickyVtableClasses;

namespace HeyClicky.App.HeyClicky_App_XamlTypeInfo
{
  // Token: 0x02000090 RID: 144
  [GeneratedCode("Microsoft.UI.Xaml.Markup.Compiler", " 3.0.0.2609")]
  [DebuggerNonUserCode]
  [WinRTRuntimeClassName("Microsoft.UI.Xaml.Markup.IXamlMember")]
  [WinRTExposedType(typeof(HeyClicky_App_HeyClicky_App_XamlTypeInfo_XamlMemberWinRTTypeDetails))]
  internal class XamlMember : IXamlMember
  {
    // Token: 0x06000585 RID: 1413 RVA: 0x0052D0E0 File Offset: 0x0052D0E0
    public XamlMember(XamlTypeInfoProvider provider, string name, string typeName)
    {
      this._name = name;
      this._typeName = typeName;
      this._provider = provider;
    }

    // Token: 0x170000CB RID: 203
    // (get) Token: 0x06000586 RID: 1414 RVA: 0x0052D100 File Offset: 0x0052D100
    public string Name
    {
      get
      {
        return this._name;
      }
    }

    // Token: 0x170000CC RID: 204
    // (get) Token: 0x06000587 RID: 1415 RVA: 0x0052D108 File Offset: 0x0052D108
    public IXamlType Type
    {
      get
      {
        return this._provider.GetXamlTypeByName(this._typeName);
      }
    }

    // Token: 0x06000588 RID: 1416 RVA: 0x0052D11C File Offset: 0x0052D11C
    public void SetTargetTypeName(string targetTypeName)
    {
      this._targetTypeName = targetTypeName;
    }

    // Token: 0x170000CD RID: 205
    // (get) Token: 0x06000589 RID: 1417 RVA: 0x0052D128 File Offset: 0x0052D128
    public IXamlType TargetType
    {
      get
      {
        return this._provider.GetXamlTypeByName(this._targetTypeName);
      }
    }

    // Token: 0x0600058A RID: 1418 RVA: 0x0052D13C File Offset: 0x0052D13C
    public void SetIsAttachable()
    {
      this._isAttachable = true;
    }

    // Token: 0x170000CE RID: 206
    // (get) Token: 0x0600058B RID: 1419 RVA: 0x0052D148 File Offset: 0x0052D148
    public bool IsAttachable
    {
      get
      {
        return this._isAttachable;
      }
    }

    // Token: 0x0600058C RID: 1420 RVA: 0x0052D150 File Offset: 0x0052D150
    public void SetIsDependencyProperty()
    {
      this._isDependencyProperty = true;
    }

    // Token: 0x170000CF RID: 207
    // (get) Token: 0x0600058D RID: 1421 RVA: 0x0052D15C File Offset: 0x0052D15C
    public bool IsDependencyProperty
    {
      get
      {
        return this._isDependencyProperty;
      }
    }

    // Token: 0x0600058E RID: 1422 RVA: 0x0052D164 File Offset: 0x0052D164
    public void SetIsReadOnly()
    {
      this._isReadOnly = true;
    }

    // Token: 0x170000D0 RID: 208
    // (get) Token: 0x0600058F RID: 1423 RVA: 0x0052D170 File Offset: 0x0052D170
    public bool IsReadOnly
    {
      get
      {
        return this._isReadOnly;
      }
    }

    // Token: 0x170000D1 RID: 209
    // (get) Token: 0x06000590 RID: 1424 RVA: 0x0052D178 File Offset: 0x0052D178
    // (set) Token: 0x06000591 RID: 1425 RVA: 0x0052D180 File Offset: 0x0052D180
    public Getter Getter { get; set; }

    // Token: 0x06000592 RID: 1426 RVA: 0x0052D18C File Offset: 0x0052D18C
    public object GetValue(object instance)
    {
      if (this.Getter != null)
      {
        return this.Getter(instance);
      }
      throw new InvalidOperationException("GetValue");
    }

    // Token: 0x170000D2 RID: 210
    // (get) Token: 0x06000593 RID: 1427 RVA: 0x0052D1B0 File Offset: 0x0052D1B0
    // (set) Token: 0x06000594 RID: 1428 RVA: 0x0052D1B8 File Offset: 0x0052D1B8
    public Setter Setter { get; set; }

    // Token: 0x06000595 RID: 1429 RVA: 0x0052D1C4 File Offset: 0x0052D1C4
    public void SetValue(object instance, object value)
    {
      if (this.Setter != null)
      {
        this.Setter(instance, value);
        return;
      }
      throw new InvalidOperationException("SetValue");
    }

    // Token: 0x0400011B RID: 283
    private XamlTypeInfoProvider _provider;

    // Token: 0x0400011C RID: 284
    private string _name;

    // Token: 0x0400011D RID: 285
    private bool _isAttachable;

    // Token: 0x0400011E RID: 286
    private bool _isDependencyProperty;

    // Token: 0x0400011F RID: 287
    private bool _isReadOnly;

    // Token: 0x04000120 RID: 288
    private string _typeName;

    // Token: 0x04000121 RID: 289
    private string _targetTypeName;
  }
}
