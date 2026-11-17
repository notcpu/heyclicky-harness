using System;
using System.Runtime.CompilerServices;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

namespace HeyClicky.App.Ink
{
  // Token: 0x020001EE RID: 494
  [NullableContext(1)]
  [Nullable(0)]
  public sealed class ClickySketchCard : Grid
  {
    // Token: 0x17000448 RID: 1096
    // (get) Token: 0x06001840 RID: 6208 RVA: 0x005C9F8C File Offset: 0x005C9F8C
    public StackPanel Content { get; } = new StackPanel
    {
      Orientation = Orientation.Vertical,
      Spacing = 0.0
    };

    // Token: 0x06001841 RID: 6209 RVA: 0x005C9F94 File Offset: 0x005C9F94
    [NullableContext(2)]
    public ClickySketchCard(ClickySketchTone tone, string label = null, long seed = 1L, bool isFocus = false)
    {
      base.Children.Add(new ClickySketchShapeView(tone.Fill(), tone.Pencil(), 3.0, seed, isFocus ? 2.0 : 1.3, true, true));
      this.Content.Margin = new Thickness(22.0, 18.0, 22.0, 18.0);
      this.Content.VerticalAlignment = VerticalAlignment.Top;
      this.Content.HorizontalAlignment = HorizontalAlignment.Stretch;
      if (label != null)
      {
        this.Content.Children.Add(new ClickySketchLabel(label, tone));
      }
      base.Children.Add(this.Content);
      this.Tone = tone;
    }

    // Token: 0x17000449 RID: 1097
    // (get) Token: 0x06001842 RID: 6210 RVA: 0x005CA088 File Offset: 0x005CA088
    public ClickySketchTone Tone { get; }

    // Token: 0x06001843 RID: 6211 RVA: 0x005CA090 File Offset: 0x005CA090
    public ClickySketchCard With(params UIElement[] content)
    {
      foreach (UIElement uielement in content)
      {
        this.Content.Children.Add(uielement);
      }
      return this;
    }
  }
}
