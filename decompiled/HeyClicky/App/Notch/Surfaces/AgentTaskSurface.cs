using System;
using System.Runtime.CompilerServices;
using HeyClicky.App.Portraits;
using HeyClicky.App.UI;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Windows.UI;

namespace HeyClicky.App.Notch.Surfaces
{
  // Token: 0x02000158 RID: 344
  [NullableContext(1)]
  [Nullable(0)]
  public sealed class AgentTaskSurface : NotchSurface
  {
    // Token: 0x060012FC RID: 4860 RVA: 0x0059C964 File Offset: 0x0059C964
    public static string StripTitle(string title)
    {
      string text = title.Trim();
      if (text.Length > 25)
      {
        return text.Substring(0, 24) + "…";
      }
      return text;
    }

    // Token: 0x060012FD RID: 4861 RVA: 0x0059C998 File Offset: 0x0059C998
    public AgentTaskSurface(NotchLiveEvent.AgentTask e, Color agentColor)
    {
      PortraitView portraitView = ClickyFace.NotchBadge(e.TaskId, 26.0);
      this.view = Ui.Row(10.0, new UIElement[] { portraitView }, new UIElement[] { ActivityIndicators.AgentDots(agentColor) }, 6.0);
      this.view.Margin = new Thickness(16.0, 0.0, 16.0, 0.0);
      ToolTipService.SetToolTip(this.view, e.Title);
    }

    // Token: 0x17000373 RID: 883
    // (get) Token: 0x060012FE RID: 4862 RVA: 0x0059CA3C File Offset: 0x0059CA3C
    public override VerticalAlignment ContentAlignment
    {
      get
      {
        return VerticalAlignment.Center;
      }
    }

    // Token: 0x17000374 RID: 884
    // (get) Token: 0x060012FF RID: 4863 RVA: 0x0059CA40 File Offset: 0x0059CA40
    public override FrameworkElement View
    {
      get
      {
        return this.view;
      }
    }

    // Token: 0x040008B4 RID: 2228
    private readonly Grid view;
  }
}
