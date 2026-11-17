using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.UI.Xaml.Markup;
using WinRT;
using WinRT.HeyClickyVtableClasses;

namespace HeyClicky.App.HeyClicky_App_XamlTypeInfo
{
  // Token: 0x0200008D RID: 141
  [GeneratedCode("Microsoft.UI.Xaml.Markup.Compiler", " 3.0.0.2609")]
  [DebuggerNonUserCode]
  [WinRTRuntimeClassName("Microsoft.UI.Xaml.Markup.IXamlType")]
  [WinRTExposedType(typeof(HeyClicky_App_HeyClicky_App_XamlTypeInfo_XamlSystemBaseTypeWinRTTypeDetails))]
  internal class XamlUserType : XamlSystemBaseType, IXamlType
  {
    // Token: 0x06000551 RID: 1361 RVA: 0x0052CCF8 File Offset: 0x0052CCF8
    public XamlUserType(XamlTypeInfoProvider provider, string fullName, Type fullType, IXamlType baseType)
      : base(fullName, fullType)
    {
      this._provider = provider;
      this._baseType = baseType;
    }

    // Token: 0x170000B8 RID: 184
    // (get) Token: 0x06000552 RID: 1362 RVA: 0x0052CD14 File Offset: 0x0052CD14
    public override IXamlType BaseType
    {
      get
      {
        return this._baseType;
      }
    }

    // Token: 0x170000B9 RID: 185
    // (get) Token: 0x06000553 RID: 1363 RVA: 0x0052CD1C File Offset: 0x0052CD1C
    public override bool IsArray
    {
      get
      {
        return this._isArray;
      }
    }

    // Token: 0x170000BA RID: 186
    // (get) Token: 0x06000554 RID: 1364 RVA: 0x0052CD24 File Offset: 0x0052CD24
    public override bool IsCollection
    {
      get
      {
        return this.CollectionAdd != null;
      }
    }

    // Token: 0x170000BB RID: 187
    // (get) Token: 0x06000555 RID: 1365 RVA: 0x0052CD30 File Offset: 0x0052CD30
    public override bool IsConstructible
    {
      get
      {
        return this.Activator != null;
      }
    }

    // Token: 0x170000BC RID: 188
    // (get) Token: 0x06000556 RID: 1366 RVA: 0x0052CD3C File Offset: 0x0052CD3C
    public override bool IsDictionary
    {
      get
      {
        return this.DictionaryAdd != null;
      }
    }

    // Token: 0x170000BD RID: 189
    // (get) Token: 0x06000557 RID: 1367 RVA: 0x0052CD48 File Offset: 0x0052CD48
    public override bool IsMarkupExtension
    {
      get
      {
        return this._isMarkupExtension;
      }
    }

    // Token: 0x170000BE RID: 190
    // (get) Token: 0x06000558 RID: 1368 RVA: 0x0052CD50 File Offset: 0x0052CD50
    public override bool IsBindable
    {
      get
      {
        return this._isBindable;
      }
    }

    // Token: 0x170000BF RID: 191
    // (get) Token: 0x06000559 RID: 1369 RVA: 0x0052CD58 File Offset: 0x0052CD58
    public override bool IsReturnTypeStub
    {
      get
      {
        return this._isReturnTypeStub;
      }
    }

    // Token: 0x170000C0 RID: 192
    // (get) Token: 0x0600055A RID: 1370 RVA: 0x0052CD60 File Offset: 0x0052CD60
    public override bool IsLocalType
    {
      get
      {
        return this._isLocalType;
      }
    }

    // Token: 0x170000C1 RID: 193
    // (get) Token: 0x0600055B RID: 1371 RVA: 0x0052CD68 File Offset: 0x0052CD68
    public override IXamlType BoxedType
    {
      get
      {
        return this._boxedType;
      }
    }

    // Token: 0x170000C2 RID: 194
    // (get) Token: 0x0600055C RID: 1372 RVA: 0x0052CD70 File Offset: 0x0052CD70
    public override IXamlMember ContentProperty
    {
      get
      {
        return this._provider.GetMemberByLongName(this._contentPropertyName);
      }
    }

    // Token: 0x170000C3 RID: 195
    // (get) Token: 0x0600055D RID: 1373 RVA: 0x0052CD84 File Offset: 0x0052CD84
    public override IXamlType ItemType
    {
      get
      {
        return this._provider.GetXamlTypeByName(this._itemTypeName);
      }
    }

    // Token: 0x170000C4 RID: 196
    // (get) Token: 0x0600055E RID: 1374 RVA: 0x0052CD98 File Offset: 0x0052CD98
    public override IXamlType KeyType
    {
      get
      {
        return this._provider.GetXamlTypeByName(this._keyTypeName);
      }
    }

    // Token: 0x0600055F RID: 1375 RVA: 0x0052CDAC File Offset: 0x0052CDAC
    public override IXamlMember GetMember(string name)
    {
      if (this._memberNames == null)
      {
        return null;
      }
      string text;
      if (this._memberNames.TryGetValue(name, out text))
      {
        return this._provider.GetMemberByLongName(text);
      }
      return null;
    }

    // Token: 0x06000560 RID: 1376 RVA: 0x0052CDE4 File Offset: 0x0052CDE4
    public override object ActivateInstance()
    {
      return this.Activator();
    }

    // Token: 0x06000561 RID: 1377 RVA: 0x0052CDF4 File Offset: 0x0052CDF4
    public override void AddToMap(object instance, object key, object item)
    {
      this.DictionaryAdd(instance, key, item);
    }

    // Token: 0x06000562 RID: 1378 RVA: 0x0052CE04 File Offset: 0x0052CE04
    public override void AddToVector(object instance, object item)
    {
      this.CollectionAdd(instance, item);
    }

    // Token: 0x06000563 RID: 1379 RVA: 0x0052CE14 File Offset: 0x0052CE14
    public override void RunInitializer()
    {
      this.StaticInitializer();
    }

    // Token: 0x06000564 RID: 1380 RVA: 0x0052CE24 File Offset: 0x0052CE24
    public override object CreateFromString(string input)
    {
      if (this.BoxedType != null)
      {
        return this.BoxInstance(this.BoxedType.CreateFromString(input));
      }
      if (this.CreateFromStringMethod != null)
      {
        return this.CreateFromStringMethod(input);
      }
      if (this._enumValues != null)
      {
        long num = 0L;
        foreach (string text in input.Split(',', StringSplitOptions.None))
        {
          long num2 = 0L;
          try
          {
            object obj;
            if (this._enumValues.TryGetValue(text.Trim(), out obj))
            {
              num2 = Convert.ToInt64(obj);
            }
            else
            {
              try
              {
                num2 = Convert.ToInt64(text.Trim());
              }
              catch (FormatException)
              {
                foreach (string text2 in this._enumValues.Keys)
                {
                  if (string.Compare(text.Trim(), text2, StringComparison.OrdinalIgnoreCase) == 0 && this._enumValues.TryGetValue(text2.Trim(), out obj))
                  {
                    num2 = Convert.ToInt64(obj);
                    break;
                  }
                }
              }
            }
            num |= num2;
          }
          catch (FormatException)
          {
            throw new ArgumentException(input, base.FullName);
          }
        }
        return Convert.ChangeType(num, Enum.GetUnderlyingType(base.UnderlyingType));
      }
      throw new ArgumentException(input, base.FullName);
    }

    // Token: 0x170000C5 RID: 197
    // (get) Token: 0x06000565 RID: 1381 RVA: 0x0052CF94 File Offset: 0x0052CF94
    // (set) Token: 0x06000566 RID: 1382 RVA: 0x0052CF9C File Offset: 0x0052CF9C
    public Activator Activator { get; set; }

    // Token: 0x170000C6 RID: 198
    // (get) Token: 0x06000567 RID: 1383 RVA: 0x0052CFA8 File Offset: 0x0052CFA8
    // (set) Token: 0x06000568 RID: 1384 RVA: 0x0052CFB0 File Offset: 0x0052CFB0
    public StaticInitializer StaticInitializer { get; set; }

    // Token: 0x170000C7 RID: 199
    // (get) Token: 0x06000569 RID: 1385 RVA: 0x0052CFBC File Offset: 0x0052CFBC
    // (set) Token: 0x0600056A RID: 1386 RVA: 0x0052CFC4 File Offset: 0x0052CFC4
    public AddToCollection CollectionAdd { get; set; }

    // Token: 0x170000C8 RID: 200
    // (get) Token: 0x0600056B RID: 1387 RVA: 0x0052CFD0 File Offset: 0x0052CFD0
    // (set) Token: 0x0600056C RID: 1388 RVA: 0x0052CFD8 File Offset: 0x0052CFD8
    public AddToDictionary DictionaryAdd { get; set; }

    // Token: 0x170000C9 RID: 201
    // (get) Token: 0x0600056D RID: 1389 RVA: 0x0052CFE4 File Offset: 0x0052CFE4
    // (set) Token: 0x0600056E RID: 1390 RVA: 0x0052CFEC File Offset: 0x0052CFEC
    public CreateFromStringMethod CreateFromStringMethod { get; set; }

    // Token: 0x170000CA RID: 202
    // (get) Token: 0x0600056F RID: 1391 RVA: 0x0052CFF8 File Offset: 0x0052CFF8
    // (set) Token: 0x06000570 RID: 1392 RVA: 0x0052D000 File Offset: 0x0052D000
    public BoxInstanceMethod BoxInstance { get; set; }

    // Token: 0x06000571 RID: 1393 RVA: 0x0052D00C File Offset: 0x0052D00C
    public void SetContentPropertyName(string contentPropertyName)
    {
      this._contentPropertyName = contentPropertyName;
    }

    // Token: 0x06000572 RID: 1394 RVA: 0x0052D018 File Offset: 0x0052D018
    public void SetIsArray()
    {
      this._isArray = true;
    }

    // Token: 0x06000573 RID: 1395 RVA: 0x0052D024 File Offset: 0x0052D024
    public void SetIsMarkupExtension()
    {
      this._isMarkupExtension = true;
    }

    // Token: 0x06000574 RID: 1396 RVA: 0x0052D030 File Offset: 0x0052D030
    public void SetIsBindable()
    {
      this._isBindable = true;
    }

    // Token: 0x06000575 RID: 1397 RVA: 0x0052D03C File Offset: 0x0052D03C
    public void SetIsReturnTypeStub()
    {
      this._isReturnTypeStub = true;
    }

    // Token: 0x06000576 RID: 1398 RVA: 0x0052D048 File Offset: 0x0052D048
    public void SetIsLocalType()
    {
      this._isLocalType = true;
    }

    // Token: 0x06000577 RID: 1399 RVA: 0x0052D054 File Offset: 0x0052D054
    public void SetItemTypeName(string itemTypeName)
    {
      this._itemTypeName = itemTypeName;
    }

    // Token: 0x06000578 RID: 1400 RVA: 0x0052D060 File Offset: 0x0052D060
    public void SetKeyTypeName(string keyTypeName)
    {
      this._keyTypeName = keyTypeName;
    }

    // Token: 0x06000579 RID: 1401 RVA: 0x0052D06C File Offset: 0x0052D06C
    public void SetBoxedType(IXamlType boxedType)
    {
      this._boxedType = boxedType;
    }

    // Token: 0x0600057A RID: 1402 RVA: 0x0052D078 File Offset: 0x0052D078
    public object BoxType<T>(object instance) where T : struct
    {
      return (T)((object)instance);
    }

    // Token: 0x0600057B RID: 1403 RVA: 0x0052D088 File Offset: 0x0052D088
    public void AddMemberName(string shortName)
    {
      if (this._memberNames == null)
      {
        this._memberNames = new Dictionary<string, string>();
      }
      this._memberNames.Add(shortName, base.FullName + "." + shortName);
    }

    // Token: 0x0600057C RID: 1404 RVA: 0x0052D0BC File Offset: 0x0052D0BC
    public void AddEnumValue(string name, object value)
    {
      if (this._enumValues == null)
      {
        this._enumValues = new Dictionary<string, object>();
      }
      this._enumValues.Add(name, value);
    }

    // Token: 0x04000108 RID: 264
    private XamlTypeInfoProvider _provider;

    // Token: 0x04000109 RID: 265
    private IXamlType _baseType;

    // Token: 0x0400010A RID: 266
    private IXamlType _boxedType;

    // Token: 0x0400010B RID: 267
    private bool _isArray;

    // Token: 0x0400010C RID: 268
    private bool _isMarkupExtension;

    // Token: 0x0400010D RID: 269
    private bool _isBindable;

    // Token: 0x0400010E RID: 270
    private bool _isReturnTypeStub;

    // Token: 0x0400010F RID: 271
    private bool _isLocalType;

    // Token: 0x04000110 RID: 272
    private string _contentPropertyName;

    // Token: 0x04000111 RID: 273
    private string _itemTypeName;

    // Token: 0x04000112 RID: 274
    private string _keyTypeName;

    // Token: 0x04000113 RID: 275
    private Dictionary<string, string> _memberNames;

    // Token: 0x04000114 RID: 276
    private Dictionary<string, object> _enumValues;
  }
}
