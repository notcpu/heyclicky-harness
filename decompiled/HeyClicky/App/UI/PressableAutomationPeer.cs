using System;
using System.Runtime.CompilerServices;
using Microsoft.UI.Xaml.Automation;
using Microsoft.UI.Xaml.Automation.Peers;
using Microsoft.UI.Xaml.Automation.Provider;
using Microsoft.UI.Xaml.Controls;

namespace HeyClicky.App.UI
{
  // Token: 0x020000A0 RID: 160
  [NullableContext(1)]
  [Nullable(0)]
  public sealed class PressableAutomationPeer : FrameworkElementAutomationPeer, IInvokeProvider, IToggleProvider
  {
    // Token: 0x060006AD RID: 1709 RVA: 0x00531E00 File Offset: 0x00531E00
    public PressableAutomationPeer(Pressable owner)
      : base(owner)
    {
      this.owner = owner;
    }

    // Token: 0x060006AE RID: 1710 RVA: 0x00531E10 File Offset: 0x00531E10
    protected override AutomationControlType GetAutomationControlTypeCore()
    {
      if (this.owner.AccessibleToggleState != null)
      {
        return AutomationControlType.CheckBox;
      }
      return AutomationControlType.Button;
    }

    // Token: 0x060006AF RID: 1711 RVA: 0x00531E38 File Offset: 0x00531E38
    protected override string GetClassNameCore()
    {
      if (this.owner.AccessibleToggleState != null)
      {
        return "ToggleSwitch";
      }
      return "Button";
    }

    // Token: 0x060006B0 RID: 1712 RVA: 0x00531E68 File Offset: 0x00531E68
    protected override bool IsControlElementCore()
    {
      return true;
    }

    // Token: 0x060006B1 RID: 1713 RVA: 0x00531E6C File Offset: 0x00531E6C
    protected override bool IsContentElementCore()
    {
      return true;
    }

    // Token: 0x060006B2 RID: 1714 RVA: 0x00531E70 File Offset: 0x00531E70
    protected override bool IsEnabledCore()
    {
      return this.owner.Enabled;
    }

    // Token: 0x060006B3 RID: 1715 RVA: 0x00531E80 File Offset: 0x00531E80
    protected override string GetNameCore()
    {
      string nameCore = base.GetNameCore();
      if (!string.IsNullOrWhiteSpace(nameCore))
      {
        return nameCore;
      }
      string text = ToolTipService.GetToolTip(this.owner) as string;
      if (text != null && !string.IsNullOrWhiteSpace(text))
      {
        return text;
      }
      return Accessibility.TextWithin(this.owner, 120);
    }

    // Token: 0x060006B4 RID: 1716 RVA: 0x00531ECC File Offset: 0x00531ECC
    [NullableContext(2)]
    protected override object GetPatternCore(PatternInterface patternInterface)
    {
      if (patternInterface != PatternInterface.Invoke)
      {
        if (patternInterface == PatternInterface.Toggle)
        {
          if (this.owner.AccessibleToggleState != null)
          {
            return this;
          }
        }
      }
      else if (this.owner.AccessibleToggleState == null)
      {
        return this;
      }
      return base.GetPatternCore(patternInterface);
    }

    // Token: 0x060006B5 RID: 1717 RVA: 0x00531F20 File Offset: 0x00531F20
    public void Invoke()
    {
      if (this.owner.Enabled)
      {
        this.owner.RaiseClick();
      }
    }

    // Token: 0x17000112 RID: 274
    // (get) Token: 0x060006B6 RID: 1718 RVA: 0x00531F3C File Offset: 0x00531F3C
    public ToggleState ToggleState
    {
      get
      {
        if (!this.owner.AccessibleToggleState.GetValueOrDefault())
        {
          return ToggleState.Off;
        }
        return ToggleState.On;
      }
    }

    // Token: 0x060006B7 RID: 1719 RVA: 0x00531F64 File Offset: 0x00531F64
    public void Toggle()
    {
      if (this.owner.Enabled)
      {
        this.owner.RaiseClick();
      }
    }

    // Token: 0x040001A4 RID: 420
    private readonly Pressable owner;
  }
}
