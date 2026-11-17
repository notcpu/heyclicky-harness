using System;
using System.Numerics;
using System.Runtime.CompilerServices;
using HeyClicky.App.Design;
using HeyClicky.App.UI;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Automation;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Hosting;
using Microsoft.UI.Xaml.Media;
using Windows.Foundation;

namespace HeyClicky.App.Ink
{
  // Token: 0x020001F6 RID: 502
  [NullableContext(1)]
  [Nullable(0)]
  public sealed class ClickySketchToggle : Pressable
  {
    // Token: 0x14000062 RID: 98
    // (add) Token: 0x06001850 RID: 6224 RVA: 0x005CA624 File Offset: 0x005CA624
    // (remove) Token: 0x06001851 RID: 6225 RVA: 0x005CA65C File Offset: 0x005CA65C
    [Nullable(2)]
    [method: NullableContext(2)]
    [field: Nullable(2)]
    public event Action<bool> Toggled;

    // Token: 0x06001852 RID: 6226 RVA: 0x005CA694 File Offset: 0x005CA694
    public ClickySketchToggle(bool isOn, long seed = 6L, string accessibilityTitle = "")
      : base(true)
    {
      this.seed = seed;
      this.isOn = isOn;
      base.Width = ClickySketchToggle.SwitchSize.Width + 6.0;
      base.Height = ClickySketchToggle.SwitchSize.Height + 6.0;
      base.HorizontalAlignment = HorizontalAlignment.Left;
      base.VerticalAlignment = VerticalAlignment.Top;
      Grid grid = new Grid
      {
        Margin = new Thickness(3.0)
      };
      grid.Children.Add(this.track);
      grid.Children.Add(this.knob);
      base.Children.Add(grid);
      this.Draw(false);
      base.Click += delegate
      {
        this.IsOn = !this.isOn;
      };
      AutomationProperties.SetName(this, accessibilityTitle);
      base.AccessibleToggleState = new bool?(isOn);
    }

    // Token: 0x1700044B RID: 1099
    // (get) Token: 0x06001853 RID: 6227 RVA: 0x005CA7B4 File Offset: 0x005CA7B4
    // (set) Token: 0x06001854 RID: 6228 RVA: 0x005CA7BC File Offset: 0x005CA7BC
    public bool IsOn
    {
      get
      {
        return this.isOn;
      }
      set
      {
        if (this.isOn == value)
        {
          return;
        }
        this.isOn = value;
        base.AccessibleToggleState = new bool?(value);
        this.Draw(true);
        Action<bool> toggled = this.Toggled;
        if (toggled == null)
        {
          return;
        }
        toggled(value);
      }
    }

    // Token: 0x06001855 RID: 6229 RVA: 0x005CA7F4 File Offset: 0x005CA7F4
    private void Draw(bool animated)
    {
      Size switchSize = ClickySketchToggle.SwitchSize;
      this.track.Children.Clear();
      PaperPath paperPath = new ClickySketchRect(switchSize.Height / 2.0, this.seed, 0.4).PathIn(new Rect(0.0, 0.0, switchSize.Width, switchSize.Height));
      this.track.Fill(paperPath, this.isOn ? ClickySketchInk.Teal : PaperColor.Hex("#E9EFF7"));
      this.track.Stroke(paperPath, new PaperStroke(this.isOn ? ClickySketchInk.TealLine : PaperColor.Hex("#97ACC6"), 1.3, PenLineCap.Flat, PenLineJoin.Miter, null));
      this.knob.Children.Clear();
      PaperPath paperPath2 = new ClickySketchRect(9.0, this.seed + 2L, 0.35).PathIn(new Rect(0f, 0f, 18f, 18f));
      this.knob.Children.Add(new PaperDrawing().Fill(paperPath2, PaperColor.Hex("#FBFDFF")).Stroke(paperPath2, new PaperStroke(this.isOn ? ClickySketchInk.TealLine : PaperColor.Hex("#97ACC6"), 1.3, PenLineCap.Flat, PenLineJoin.Miter, null)));
      Vector3 vector = new Vector3((float)(this.isOn ? (switchSize.Width - 22.0) : 4.0), 3f, 0f);
      if (animated && !Anim.ReduceMotion)
      {
        Anim.TranslationSpring(this.knob, vector, new SwiftSpring(0.28, 0.75));
        return;
      }
      ElementCompositionPreview.SetIsTranslationEnabled(this.knob, true);
      this.knob.Translation = vector;
    }

    // Token: 0x04000C04 RID: 3076
    public static readonly Size SwitchSize = new Size(46f, 24f);

    // Token: 0x04000C05 RID: 3077
    private readonly long seed;

    // Token: 0x04000C06 RID: 3078
    private readonly PaperDrawing track = new PaperDrawing();

    // Token: 0x04000C07 RID: 3079
    private readonly Grid knob = new Grid
    {
      Width = 18.0,
      Height = 18.0,
      HorizontalAlignment = HorizontalAlignment.Left,
      VerticalAlignment = VerticalAlignment.Top
    };

    // Token: 0x04000C08 RID: 3080
    private bool isOn;
  }
}
