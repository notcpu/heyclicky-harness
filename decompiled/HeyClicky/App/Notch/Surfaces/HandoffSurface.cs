using System;
using System.Runtime.CompilerServices;
using HeyClicky.App.Design;
using HeyClicky.App.UI;
using Microsoft.UI.Text;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

namespace HeyClicky.App.Notch.Surfaces
{
  // Token: 0x0200015C RID: 348
  [NullableContext(1)]
  [Nullable(0)]
  public sealed class HandoffSurface : NotchSurface
  {
    // Token: 0x06001355 RID: 4949 RVA: 0x005A04DC File Offset: 0x005A04DC
    public HandoffSurface(NotchLiveEvent e)
    {
      NotchLiveEvent.HandoffQueued handoffQueued = e as NotchLiveEvent.HandoffQueued;
      ValueTuple<string, string, string> valueTuple;
      if (handoffQueued == null)
      {
        NotchLiveEvent.HandoffSent handoffSent = e as NotchLiveEvent.HandoffSent;
        if (handoffSent == null)
        {
          valueTuple = new ValueTuple<string, string, string>("\ue724", "", "");
        }
        else
        {
          valueTuple = new ValueTuple<string, string, string>("\ue724", "Sent", "Pasted into " + handoffSent.TargetDisplayName + ".");
        }
      }
      else
      {
        string text = "\ue710";
        string text2;
        if (handoffQueued.Count != 1)
        {
          DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(15, 1);
          defaultInterpolatedStringHandler.AppendFormatted<int>(handoffQueued.Count);
          defaultInterpolatedStringHandler.AppendLiteral(" regions queued");
          text2 = defaultInterpolatedStringHandler.ToStringAndClear();
        }
        else
        {
          text2 = "Region queued";
        }
        valueTuple = new ValueTuple<string, string, string>(text, text2, "Pick text or voice to send.");
      }
      ValueTuple<string, string, string> valueTuple2 = valueTuple;
      string item = valueTuple2.Item1;
      string item2 = valueTuple2.Item2;
      string item3 = valueTuple2.Item3;
      TextBlock textBlock = Icons.Glyph(item, 11.0, DS.Colors.Blue300);
      textBlock.FontWeight = FontWeights.Medium;
      StackPanel stackPanel = new StackPanel
      {
        Spacing = 1.0,
        VerticalAlignment = VerticalAlignment.Center,
        Children = 
        {
          Ui.Text(item2, 11.0, FontWeights.Medium, DS.Colors.TextPrimary, TextAlignment.Left, 0, false, false),
          Ui.Text(item3, 10.0, FontWeights.Normal, DS.Colors.TextSecondary, TextAlignment.Left, 1, false, false)
        }
      };
      this.view = Ui.H(8.0, new UIElement[] { textBlock, stackPanel });
      this.view.Margin = new Thickness(12.0, 8.0, 12.0, 8.0);
      this.view.HorizontalAlignment = HorizontalAlignment.Left;
    }

    // Token: 0x17000380 RID: 896
    // (get) Token: 0x06001356 RID: 4950 RVA: 0x005A06A0 File Offset: 0x005A06A0
    public override VerticalAlignment ContentAlignment
    {
      get
      {
        return VerticalAlignment.Center;
      }
    }

    // Token: 0x17000381 RID: 897
    // (get) Token: 0x06001357 RID: 4951 RVA: 0x005A06A4 File Offset: 0x005A06A4
    public override FrameworkElement View
    {
      get
      {
        return this.view;
      }
    }

    // Token: 0x040008F2 RID: 2290
    private const string QueuedGlyph = "\ue710";

    // Token: 0x040008F3 RID: 2291
    private readonly StackPanel view;
  }
}
