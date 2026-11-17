using System;
using System.Runtime.CompilerServices;
using Microsoft.UI.Xaml.Automation;
using Microsoft.UI.Xaml.Automation.Peers;
using Microsoft.UI.Xaml.Controls;

namespace HeyClicky.App.UI
{
  // Token: 0x020000A1 RID: 161
  [NullableContext(1)]
  [Nullable(0)]
  public class AccessibleGrid : Grid
  {
    // Token: 0x17000113 RID: 275
    // (get) Token: 0x060006B8 RID: 1720 RVA: 0x00531F80 File Offset: 0x00531F80
    // (set) Token: 0x060006B9 RID: 1721 RVA: 0x00531F88 File Offset: 0x00531F88
    public AutomationControlType ControlType { get; set; } = AutomationControlType.Group;

    // Token: 0x060006BA RID: 1722 RVA: 0x00531F94 File Offset: 0x00531F94
    protected override AutomationPeer OnCreateAutomationPeer()
    {
      return new AccessibleGrid.AccessibleGridPeer(this);
    }

    // Token: 0x02000449 RID: 1097
    [Nullable(0)]
    private sealed class AccessibleGridPeer : FrameworkElementAutomationPeer
    {
      // Token: 0x060029DD RID: 10717 RVA: 0x0064D3D0 File Offset: 0x0064D3D0
      public AccessibleGridPeer(AccessibleGrid owner)
        : base(this.<owner>P)
      {
      }

      // Token: 0x060029DE RID: 10718 RVA: 0x0064D3E8 File Offset: 0x0064D3E8
      protected override AutomationControlType GetAutomationControlTypeCore()
      {
        return this.<owner>P.ControlType;
      }

      // Token: 0x060029DF RID: 10719 RVA: 0x0064D3F8 File Offset: 0x0064D3F8
      protected override string GetClassNameCore()
      {
        return "Group";
      }

      // Token: 0x060029E0 RID: 10720 RVA: 0x0064D400 File Offset: 0x0064D400
      protected override bool IsControlElementCore()
      {
        return true;
      }

      // Token: 0x060029E1 RID: 10721 RVA: 0x0064D404 File Offset: 0x0064D404
      protected override bool IsContentElementCore()
      {
        return !string.IsNullOrEmpty(AutomationProperties.GetName(this.<owner>P));
      }

      // Token: 0x04001592 RID: 5522
      [CompilerGenerated]
      private AccessibleGrid <owner>P = owner;
    }
  }
}
