using System;
using System.Numerics;
using System.Runtime.CompilerServices;
using HeyClicky.App.Design;
using Microsoft.UI.Text;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media;

namespace HeyClicky.App.UI
{
  // Token: 0x020000AF RID: 175
  [NullableContext(1)]
  [Nullable(0)]
  public sealed class GelIconButton : Pressable
  {
    // Token: 0x0600072C RID: 1836 RVA: 0x0053616C File Offset: 0x0053616C
    public GelIconButton(string glyphText, GelAppearance appearance, double symbolSize, double diameter, string tip)
      : base(true)
    {
      GelIconButton <>4__this = this;
      this.appearance = appearance;
      base.Width = diameter;
      base.Height = diameter;
      bool flag = appearance == GelAppearance.SecondaryOutline;
      if (!flag)
      {
        base.Children.Add(new ShadowLayer(diameter / 2.0, DS.Black(0.11), 5.0, 4.0, 0.0));
      }
      this.gel = new GelSurface(appearance, diameter / 2.0);
      base.Children.Add(this.gel);
      if (!flag)
      {
        this.highlight = Icons.Glyph(glyphText, symbolSize, DS.White(0.8));
        this.highlight.FontWeight = FontWeights.Bold;
        this.highlight.RenderTransform = new TranslateTransform
        {
          Y = 1.0
        };
        base.Children.Add(this.highlight);
      }
      this.glyph = Icons.Glyph(glyphText, symbolSize, flag ? DS.White(0.86) : DS.Black(0.85));
      this.glyph.FontWeight = FontWeights.Bold;
      base.Children.Add(this.glyph);
      base.HoverChanged += delegate(bool h)
      {
        <>4__this.gel.Hovered = h && <>4__this.Enabled;
        if (<>4__this.Enabled)
        {
          Anim.Scale(<>4__this, h ? 1.05f : 1f, 0.15, null, new Vector3?(new Vector3((float)diameter / 2f, (float)diameter / 2f, 0f)));
        }
      };
      this.Tip(tip);
    }

    // Token: 0x0600072D RID: 1837 RVA: 0x005362FC File Offset: 0x005362FC
    public void SetEnabled(bool on)
    {
      base.Enabled = on;
      this.gel.Enabled = on;
      bool flag = this.appearance == GelAppearance.SecondaryOutline;
      this.glyph.Foreground = Ui.Brush(flag ? DS.White(on ? 0.86 : 0.4) : (on ? DS.Black(0.85) : DS.Colors.GelDisabledContent));
      if (this.highlight != null)
      {
        this.highlight.Foreground = Ui.Brush(DS.White(on ? 0.8 : 0.18));
      }
    }

    // Token: 0x04000221 RID: 545
    private readonly GelSurface gel;

    // Token: 0x04000222 RID: 546
    private readonly TextBlock glyph;

    // Token: 0x04000223 RID: 547
    [Nullable(2)]
    private readonly TextBlock highlight;

    // Token: 0x04000224 RID: 548
    private readonly GelAppearance appearance;
  }
}
