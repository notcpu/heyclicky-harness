using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Markup;
using Microsoft.UI.Xaml.XamlTypeInfo;

namespace HeyClicky.App.HeyClicky_App_XamlTypeInfo
{
  // Token: 0x02000085 RID: 133
  [GeneratedCode("Microsoft.UI.Xaml.Markup.Compiler", " 3.0.0.2609")]
  [DebuggerNonUserCode]
  internal class XamlTypeInfoProvider
  {
    // Token: 0x06000504 RID: 1284 RVA: 0x0052C0E4 File Offset: 0x0052C0E4
    public IXamlType GetXamlTypeByType(Type type)
    {
      Dictionary<Type, IXamlType> xamlTypeCacheByType = this._xamlTypeCacheByType;
      IXamlType xamlType;
      lock (xamlTypeCacheByType)
      {
        if (this._xamlTypeCacheByType.TryGetValue(type, out xamlType))
        {
          return xamlType;
        }
        int num = this.LookupTypeIndexByType(type);
        if (num != -1)
        {
          xamlType = this.CreateXamlType(num);
        }
        XamlUserType xamlUserType = xamlType as XamlUserType;
        if (xamlType == null || (xamlUserType != null && xamlUserType.IsReturnTypeStub && !xamlUserType.IsLocalType))
        {
          IXamlType xamlType2 = this.CheckOtherMetadataProvidersForType(type);
          if (xamlType2 != null && (xamlType2.IsConstructible || xamlType == null))
          {
            xamlType = xamlType2;
          }
        }
        if (xamlType != null)
        {
          this._xamlTypeCacheByName.Add(xamlType.FullName, xamlType);
          this._xamlTypeCacheByType.Add(xamlType.UnderlyingType, xamlType);
        }
      }
      return xamlType;
    }

    // Token: 0x06000505 RID: 1285 RVA: 0x0052C1B4 File Offset: 0x0052C1B4
    public IXamlType GetXamlTypeByName(string typeName)
    {
      if (string.IsNullOrEmpty(typeName))
      {
        return null;
      }
      Dictionary<Type, IXamlType> xamlTypeCacheByType = this._xamlTypeCacheByType;
      IXamlType xamlType;
      lock (xamlTypeCacheByType)
      {
        if (this._xamlTypeCacheByName.TryGetValue(typeName, out xamlType))
        {
          return xamlType;
        }
        int num = this.LookupTypeIndexByName(typeName);
        if (num != -1)
        {
          xamlType = this.CreateXamlType(num);
        }
        XamlUserType xamlUserType = xamlType as XamlUserType;
        if (xamlType == null || (xamlUserType != null && xamlUserType.IsReturnTypeStub && !xamlUserType.IsLocalType))
        {
          IXamlType xamlType2 = this.CheckOtherMetadataProvidersForName(typeName);
          if (xamlType2 != null && (xamlType2.IsConstructible || xamlType == null))
          {
            xamlType = xamlType2;
          }
        }
        if (xamlType != null)
        {
          this._xamlTypeCacheByName.Add(xamlType.FullName, xamlType);
          this._xamlTypeCacheByType.Add(xamlType.UnderlyingType, xamlType);
        }
      }
      return xamlType;
    }

    // Token: 0x06000506 RID: 1286 RVA: 0x0052C28C File Offset: 0x0052C28C
    public IXamlMember GetMemberByLongName(string longMemberName)
    {
      if (string.IsNullOrEmpty(longMemberName))
      {
        return null;
      }
      Dictionary<string, IXamlMember> xamlMembers = this._xamlMembers;
      IXamlMember xamlMember;
      lock (xamlMembers)
      {
        if (this._xamlMembers.TryGetValue(longMemberName, out xamlMember))
        {
          return xamlMember;
        }
        xamlMember = this.CreateXamlMember(longMemberName);
        if (xamlMember != null)
        {
          this._xamlMembers.Add(longMemberName, xamlMember);
        }
      }
      return xamlMember;
    }

    // Token: 0x06000507 RID: 1287 RVA: 0x0052C300 File Offset: 0x0052C300
    private void InitTypeTables()
    {
      this._typeNameTable = new string[8];
      this._typeNameTable[0] = "Microsoft.UI.Xaml.Controls.XamlControlsResources";
      this._typeNameTable[1] = "Microsoft.UI.Xaml.ResourceDictionary";
      this._typeNameTable[2] = "Object";
      this._typeNameTable[3] = "Boolean";
      this._typeNameTable[4] = "Microsoft.UI.Xaml.Controls.TreeViewNode";
      this._typeNameTable[5] = "Microsoft.UI.Xaml.DependencyObject";
      this._typeNameTable[6] = "System.Collections.Generic.IList`1<Microsoft.UI.Xaml.Controls.TreeViewNode>";
      this._typeNameTable[7] = "Int32";
      this._typeTable = new Type[8];
      this._typeTable[0] = typeof(XamlControlsResources);
      this._typeTable[1] = typeof(ResourceDictionary);
      this._typeTable[2] = typeof(object);
      this._typeTable[3] = typeof(bool);
      this._typeTable[4] = typeof(TreeViewNode);
      this._typeTable[5] = typeof(DependencyObject);
      this._typeTable[6] = typeof(IList<TreeViewNode>);
      this._typeTable[7] = typeof(int);
    }

    // Token: 0x06000508 RID: 1288 RVA: 0x0052C420 File Offset: 0x0052C420
    private int LookupTypeIndexByName(string typeName)
    {
      if (this._typeNameTable == null)
      {
        this.InitTypeTables();
      }
      for (int i = 0; i < this._typeNameTable.Length; i++)
      {
        if (string.CompareOrdinal(this._typeNameTable[i], typeName) == 0)
        {
          return i;
        }
      }
      return -1;
    }

    // Token: 0x06000509 RID: 1289 RVA: 0x0052C464 File Offset: 0x0052C464
    private int LookupTypeIndexByType(Type type)
    {
      if (this._typeTable == null)
      {
        this.InitTypeTables();
      }
      for (int i = 0; i < this._typeTable.Length; i++)
      {
        if (type == this._typeTable[i])
        {
          return i;
        }
      }
      return -1;
    }

    // Token: 0x0600050A RID: 1290 RVA: 0x0052C4A8 File Offset: 0x0052C4A8
    private object Activate_0_XamlControlsResources()
    {
      return new XamlControlsResources();
    }

    // Token: 0x0600050B RID: 1291 RVA: 0x0052C4B0 File Offset: 0x0052C4B0
    private object Activate_4_TreeViewNode()
    {
      return new TreeViewNode();
    }

    // Token: 0x0600050C RID: 1292 RVA: 0x0052C4B8 File Offset: 0x0052C4B8
    private void StaticInitializer_0_XamlControlsResources()
    {
      RuntimeHelpers.RunClassConstructor(typeof(XamlControlsResources).TypeHandle);
    }

    // Token: 0x0600050D RID: 1293 RVA: 0x0052C4D0 File Offset: 0x0052C4D0
    private void StaticInitializer_4_TreeViewNode()
    {
      RuntimeHelpers.RunClassConstructor(typeof(TreeViewNode).TypeHandle);
    }

    // Token: 0x0600050E RID: 1294 RVA: 0x0052C4E8 File Offset: 0x0052C4E8
    private void StaticInitializer_6_IList()
    {
      RuntimeHelpers.RunClassConstructor(typeof(IList<TreeViewNode>).TypeHandle);
    }

    // Token: 0x0600050F RID: 1295 RVA: 0x0052C500 File Offset: 0x0052C500
    private void MapAdd_0_XamlControlsResources(object instance, object key, object item)
    {
      ((IDictionary<object, object>)instance).Add(key, item);
    }

    // Token: 0x06000510 RID: 1296 RVA: 0x0052C520 File Offset: 0x0052C520
    private void VectorAdd_6_IList(object instance, object item)
    {
      ICollection<TreeViewNode> collection = (ICollection<TreeViewNode>)instance;
      TreeViewNode treeViewNode = (TreeViewNode)item;
      collection.Add(treeViewNode);
    }

    // Token: 0x06000511 RID: 1297 RVA: 0x0052C540 File Offset: 0x0052C540
    private IXamlType CreateXamlType(int typeIndex)
    {
      XamlSystemBaseType xamlSystemBaseType = null;
      string text = this._typeNameTable[typeIndex];
      Type type = this._typeTable[typeIndex];
      switch (typeIndex)
      {
      case 0:
      {
        XamlUserType xamlUserType = new XamlUserType(this, text, type, this.GetXamlTypeByName("Microsoft.UI.Xaml.ResourceDictionary"));
        xamlUserType.Activator = new Activator(this.Activate_0_XamlControlsResources);
        xamlUserType.StaticInitializer = new StaticInitializer(this.StaticInitializer_0_XamlControlsResources);
        xamlUserType.DictionaryAdd = new AddToDictionary(this.MapAdd_0_XamlControlsResources);
        xamlUserType.AddMemberName("UseCompactResources");
        xamlSystemBaseType = xamlUserType;
        break;
      }
      case 1:
        xamlSystemBaseType = new XamlSystemBaseType(text, type);
        break;
      case 2:
        xamlSystemBaseType = new XamlSystemBaseType(text, type);
        break;
      case 3:
        xamlSystemBaseType = new XamlSystemBaseType(text, type);
        break;
      case 4:
      {
        XamlUserType xamlUserType2 = new XamlUserType(this, text, type, this.GetXamlTypeByName("Microsoft.UI.Xaml.DependencyObject"));
        xamlUserType2.Activator = new Activator(this.Activate_4_TreeViewNode);
        xamlUserType2.StaticInitializer = new StaticInitializer(this.StaticInitializer_4_TreeViewNode);
        xamlUserType2.AddMemberName("Children");
        xamlUserType2.AddMemberName("Content");
        xamlUserType2.AddMemberName("Depth");
        xamlUserType2.AddMemberName("HasChildren");
        xamlUserType2.AddMemberName("HasUnrealizedChildren");
        xamlUserType2.AddMemberName("IsExpanded");
        xamlUserType2.AddMemberName("Parent");
        xamlUserType2.SetIsBindable();
        xamlSystemBaseType = xamlUserType2;
        break;
      }
      case 5:
        xamlSystemBaseType = new XamlSystemBaseType(text, type);
        break;
      case 6:
      {
        XamlUserType xamlUserType3 = new XamlUserType(this, text, type, null);
        xamlUserType3.StaticInitializer = new StaticInitializer(this.StaticInitializer_6_IList);
        xamlUserType3.CollectionAdd = new AddToCollection(this.VectorAdd_6_IList);
        xamlUserType3.SetIsReturnTypeStub();
        xamlSystemBaseType = xamlUserType3;
        break;
      }
      case 7:
        xamlSystemBaseType = new XamlSystemBaseType(text, type);
        break;
      }
      return xamlSystemBaseType;
    }

    // Token: 0x170000A8 RID: 168
    // (get) Token: 0x06000512 RID: 1298 RVA: 0x0052C6E4 File Offset: 0x0052C6E4
    private List<IXamlMetadataProvider> OtherProviders
    {
      get
      {
        if (this._otherProviders == null)
        {
          List<IXamlMetadataProvider> list = new List<IXamlMetadataProvider>();
          IXamlMetadataProvider xamlMetadataProvider = new XamlControlsXamlMetaDataProvider();
          list.Add(xamlMetadataProvider);
          this._otherProviders = list;
        }
        return this._otherProviders;
      }
    }

    // Token: 0x06000513 RID: 1299 RVA: 0x0052C71C File Offset: 0x0052C71C
    private IXamlType CheckOtherMetadataProvidersForName(string typeName)
    {
      IXamlType xamlType = null;
      foreach (IXamlMetadataProvider xamlMetadataProvider in this.OtherProviders)
      {
        IXamlType xamlType2 = xamlMetadataProvider.GetXamlType(typeName);
        if (xamlType2 != null)
        {
          if (xamlType2.IsConstructible)
          {
            return xamlType2;
          }
          xamlType = xamlType2;
        }
      }
      return xamlType;
    }

    // Token: 0x06000514 RID: 1300 RVA: 0x0052C788 File Offset: 0x0052C788
    private IXamlType CheckOtherMetadataProvidersForType(Type type)
    {
      IXamlType xamlType = null;
      foreach (IXamlMetadataProvider xamlMetadataProvider in this.OtherProviders)
      {
        IXamlType xamlType2 = xamlMetadataProvider.GetXamlType(type);
        if (xamlType2 != null)
        {
          if (xamlType2.IsConstructible)
          {
            return xamlType2;
          }
          xamlType = xamlType2;
        }
      }
      return xamlType;
    }

    // Token: 0x06000515 RID: 1301 RVA: 0x0052C7F4 File Offset: 0x0052C7F4
    private object get_0_XamlControlsResources_UseCompactResources(object instance)
    {
      return ((XamlControlsResources)instance).UseCompactResources;
    }

    // Token: 0x06000516 RID: 1302 RVA: 0x0052C808 File Offset: 0x0052C808
    private void set_0_XamlControlsResources_UseCompactResources(object instance, object Value)
    {
      ((XamlControlsResources)instance).UseCompactResources = (bool)Value;
    }

    // Token: 0x06000517 RID: 1303 RVA: 0x0052C81C File Offset: 0x0052C81C
    private object get_1_TreeViewNode_Children(object instance)
    {
      return ((TreeViewNode)instance).Children;
    }

    // Token: 0x06000518 RID: 1304 RVA: 0x0052C82C File Offset: 0x0052C82C
    private object get_2_TreeViewNode_Content(object instance)
    {
      return ((TreeViewNode)instance).Content;
    }

    // Token: 0x06000519 RID: 1305 RVA: 0x0052C83C File Offset: 0x0052C83C
    private void set_2_TreeViewNode_Content(object instance, object Value)
    {
      ((TreeViewNode)instance).Content = Value;
    }

    // Token: 0x0600051A RID: 1306 RVA: 0x0052C84C File Offset: 0x0052C84C
    private object get_3_TreeViewNode_Depth(object instance)
    {
      return ((TreeViewNode)instance).Depth;
    }

    // Token: 0x0600051B RID: 1307 RVA: 0x0052C860 File Offset: 0x0052C860
    private object get_4_TreeViewNode_HasChildren(object instance)
    {
      return ((TreeViewNode)instance).HasChildren;
    }

    // Token: 0x0600051C RID: 1308 RVA: 0x0052C874 File Offset: 0x0052C874
    private object get_5_TreeViewNode_HasUnrealizedChildren(object instance)
    {
      return ((TreeViewNode)instance).HasUnrealizedChildren;
    }

    // Token: 0x0600051D RID: 1309 RVA: 0x0052C888 File Offset: 0x0052C888
    private void set_5_TreeViewNode_HasUnrealizedChildren(object instance, object Value)
    {
      ((TreeViewNode)instance).HasUnrealizedChildren = (bool)Value;
    }

    // Token: 0x0600051E RID: 1310 RVA: 0x0052C89C File Offset: 0x0052C89C
    private object get_6_TreeViewNode_IsExpanded(object instance)
    {
      return ((TreeViewNode)instance).IsExpanded;
    }

    // Token: 0x0600051F RID: 1311 RVA: 0x0052C8B0 File Offset: 0x0052C8B0
    private void set_6_TreeViewNode_IsExpanded(object instance, object Value)
    {
      ((TreeViewNode)instance).IsExpanded = (bool)Value;
    }

    // Token: 0x06000520 RID: 1312 RVA: 0x0052C8C4 File Offset: 0x0052C8C4
    private object get_7_TreeViewNode_Parent(object instance)
    {
      return ((TreeViewNode)instance).Parent;
    }

    // Token: 0x06000521 RID: 1313 RVA: 0x0052C8D4 File Offset: 0x0052C8D4
    private IXamlMember CreateXamlMember(string longMemberName)
    {
      XamlMember xamlMember = null;
      if (longMemberName != null)
      {
        int length = longMemberName.Length;
        switch (length)
        {
        case 45:
          if (longMemberName == "Microsoft.UI.Xaml.Controls.TreeViewNode.Depth")
          {
            XamlUserType xamlUserType = (XamlUserType)this.GetXamlTypeByName("Microsoft.UI.Xaml.Controls.TreeViewNode");
            xamlMember = new XamlMember(this, "Depth", "Int32");
            xamlMember.SetIsDependencyProperty();
            xamlMember.Getter = new Getter(this.get_3_TreeViewNode_Depth);
            xamlMember.SetIsReadOnly();
          }
          break;
        case 46:
          if (longMemberName == "Microsoft.UI.Xaml.Controls.TreeViewNode.Parent")
          {
            XamlUserType xamlUserType2 = (XamlUserType)this.GetXamlTypeByName("Microsoft.UI.Xaml.Controls.TreeViewNode");
            xamlMember = new XamlMember(this, "Parent", "Microsoft.UI.Xaml.Controls.TreeViewNode");
            xamlMember.Getter = new Getter(this.get_7_TreeViewNode_Parent);
            xamlMember.SetIsReadOnly();
          }
          break;
        case 47:
          if (longMemberName == "Microsoft.UI.Xaml.Controls.TreeViewNode.Content")
          {
            XamlUserType xamlUserType3 = (XamlUserType)this.GetXamlTypeByName("Microsoft.UI.Xaml.Controls.TreeViewNode");
            xamlMember = new XamlMember(this, "Content", "Object");
            xamlMember.SetIsDependencyProperty();
            xamlMember.Getter = new Getter(this.get_2_TreeViewNode_Content);
            xamlMember.Setter = new Setter(this.set_2_TreeViewNode_Content);
          }
          break;
        case 48:
          if (longMemberName == "Microsoft.UI.Xaml.Controls.TreeViewNode.Children")
          {
            XamlUserType xamlUserType4 = (XamlUserType)this.GetXamlTypeByName("Microsoft.UI.Xaml.Controls.TreeViewNode");
            xamlMember = new XamlMember(this, "Children", "System.Collections.Generic.IList`1<Microsoft.UI.Xaml.Controls.TreeViewNode>");
            xamlMember.Getter = new Getter(this.get_1_TreeViewNode_Children);
            xamlMember.SetIsReadOnly();
          }
          break;
        case 49:
          break;
        case 50:
          if (longMemberName == "Microsoft.UI.Xaml.Controls.TreeViewNode.IsExpanded")
          {
            XamlUserType xamlUserType5 = (XamlUserType)this.GetXamlTypeByName("Microsoft.UI.Xaml.Controls.TreeViewNode");
            xamlMember = new XamlMember(this, "IsExpanded", "Boolean");
            xamlMember.SetIsDependencyProperty();
            xamlMember.Getter = new Getter(this.get_6_TreeViewNode_IsExpanded);
            xamlMember.Setter = new Setter(this.set_6_TreeViewNode_IsExpanded);
          }
          break;
        case 51:
          if (longMemberName == "Microsoft.UI.Xaml.Controls.TreeViewNode.HasChildren")
          {
            XamlUserType xamlUserType6 = (XamlUserType)this.GetXamlTypeByName("Microsoft.UI.Xaml.Controls.TreeViewNode");
            xamlMember = new XamlMember(this, "HasChildren", "Boolean");
            xamlMember.SetIsDependencyProperty();
            xamlMember.Getter = new Getter(this.get_4_TreeViewNode_HasChildren);
            xamlMember.SetIsReadOnly();
          }
          break;
        default:
          if (length != 61)
          {
            if (length == 68)
            {
              if (longMemberName == "Microsoft.UI.Xaml.Controls.XamlControlsResources.UseCompactResources")
              {
                XamlUserType xamlUserType7 = (XamlUserType)this.GetXamlTypeByName("Microsoft.UI.Xaml.Controls.XamlControlsResources");
                xamlMember = new XamlMember(this, "UseCompactResources", "Boolean");
                xamlMember.SetIsDependencyProperty();
                xamlMember.Getter = new Getter(this.get_0_XamlControlsResources_UseCompactResources);
                xamlMember.Setter = new Setter(this.set_0_XamlControlsResources_UseCompactResources);
              }
            }
          }
          else if (longMemberName == "Microsoft.UI.Xaml.Controls.TreeViewNode.HasUnrealizedChildren")
          {
            XamlUserType xamlUserType8 = (XamlUserType)this.GetXamlTypeByName("Microsoft.UI.Xaml.Controls.TreeViewNode");
            xamlMember = new XamlMember(this, "HasUnrealizedChildren", "Boolean");
            xamlMember.Getter = new Getter(this.get_5_TreeViewNode_HasUnrealizedChildren);
            xamlMember.Setter = new Setter(this.set_5_TreeViewNode_HasUnrealizedChildren);
          }
          break;
        }
      }
      return xamlMember;
    }

    // Token: 0x04000100 RID: 256
    private Dictionary<string, IXamlType> _xamlTypeCacheByName = new Dictionary<string, IXamlType>();

    // Token: 0x04000101 RID: 257
    private Dictionary<Type, IXamlType> _xamlTypeCacheByType = new Dictionary<Type, IXamlType>();

    // Token: 0x04000102 RID: 258
    private Dictionary<string, IXamlMember> _xamlMembers = new Dictionary<string, IXamlMember>();

    // Token: 0x04000103 RID: 259
    private string[] _typeNameTable;

    // Token: 0x04000104 RID: 260
    private Type[] _typeTable;

    // Token: 0x04000105 RID: 261
    private List<IXamlMetadataProvider> _otherProviders;
  }
}
