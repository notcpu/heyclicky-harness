using System;
using System.Runtime.CompilerServices;
using HeyClicky.App.Dock;
using HeyClicky.App.Notch.Peek;
using HeyClicky.Core.Notch;
using Microsoft.UI.Xaml;

namespace HeyClicky.App
{
  // Token: 0x02000079 RID: 121
  [NullableContext(2)]
  [Nullable(0)]
  internal sealed class ArcNotchHomeDock : INotchHomeDock
  {
    // Token: 0x0600047E RID: 1150 RVA: 0x00529AFC File Offset: 0x00529AFC
    [NullableContext(1)]
    public ArcNotchHomeDock(ArcDock dock, Func<NotchTab?> selectedTab)
    {
      this.dock = dock;
      this.selectedTab = selectedTab;
      dock.FrontCardSizeChanged += delegate
      {
        Action frontCardSizeChanged = this.FrontCardSizeChanged;
        if (frontCardSizeChanged == null)
        {
          return;
        }
        frontCardSizeChanged();
      };
      string lastLine = null;
      dock.ContentChanged += delegate
      {
        string statusLine = this.StatusLine;
        if (statusLine == lastLine)
        {
          return;
        }
        lastLine = statusLine;
        Action frontCardSizeChanged2 = this.FrontCardSizeChanged;
        if (frontCardSizeChanged2 == null)
        {
          return;
        }
        frontCardSizeChanged2();
      };
    }

    // Token: 0x17000091 RID: 145
    // (get) Token: 0x0600047F RID: 1151 RVA: 0x00529B58 File Offset: 0x00529B58
    [TupleElementNames(new string[] { "W", "H" })]
    [Nullable(0)]
    public ValueTuple<double, double>? FrontCardSize
    {
      [NullableContext(0)]
      [return: TupleElementNames(new string[] { "W", "H" })]
      get
      {
        return this.dock.TopArcFrontCardSize;
      }
    }

    // Token: 0x14000001 RID: 1
    // (add) Token: 0x06000480 RID: 1152 RVA: 0x00529B68 File Offset: 0x00529B68
    // (remove) Token: 0x06000481 RID: 1153 RVA: 0x00529BA0 File Offset: 0x00529BA0
    public event Action FrontCardSizeChanged;

    // Token: 0x06000482 RID: 1154 RVA: 0x00529BD8 File Offset: 0x00529BD8
    public FrameworkElement CreateHomeSlot()
    {
      return null;
    }

    // Token: 0x17000092 RID: 146
    // (get) Token: 0x06000483 RID: 1155 RVA: 0x00529BDC File Offset: 0x00529BDC
    public string StatusLine
    {
      get
      {
        if (!this.dock.IsRunning)
        {
          return null;
        }
        return this.dock.StatusLine();
      }
    }

    // Token: 0x06000484 RID: 1156 RVA: 0x00529BF8 File Offset: 0x00529BF8
    public void NotchShowsHome(bool showsHome)
    {
      NotchTab? notchTab = this.selectedTab();
      if (!showsHome)
      {
        NotchTab? notchTab2 = notchTab;
        NotchTab notchTab3 = NotchTab.Home;
        if ((notchTab2.GetValueOrDefault() == notchTab3) & (notchTab2 != null))
        {
          this.dock.CloseTopArc("the notch's gear");
          return;
        }
      }
      this.dock.NotchTabChanged(showsHome, showsHome ? null : ((notchTab != null) ? notchTab.GetValueOrDefault().RawValue() : null));
    }

    // Token: 0x06000485 RID: 1157 RVA: 0x00529C68 File Offset: 0x00529C68
    [NullableContext(1)]
    public void NotchFolded(string reason)
    {
      this.dock.CloseTopArc(reason);
    }

    // Token: 0x040000DC RID: 220
    [Nullable(1)]
    private readonly ArcDock dock;

    // Token: 0x040000DD RID: 221
    [Nullable(1)]
    private readonly Func<NotchTab?> selectedTab;
  }
}
