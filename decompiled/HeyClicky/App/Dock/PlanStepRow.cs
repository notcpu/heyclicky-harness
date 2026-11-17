using System;
using System.Numerics;
using System.Runtime.CompilerServices;
using HeyClicky.App.Design;
using HeyClicky.App.UI;
using Microsoft.UI.Composition;
using Microsoft.UI.Text;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Shapes;
using Windows.UI;

namespace HeyClicky.App.Dock
{
  // Token: 0x0200031E RID: 798
  [NullableContext(1)]
  [Nullable(0)]
  public sealed class PlanStepRow : Grid
  {
    // Token: 0x06002277 RID: 8823 RVA: 0x006240D8 File Offset: 0x006240D8
    public PlanStepRow(string words, bool isDone, bool isCurrent, Color ink, bool chat = false)
    {
      this.ink = ink;
      this.chat = chat;
      base.ColumnSpacing = (double)(chat ? 9 : 11);
      base.ColumnDefinitions.Add(new ColumnDefinition
      {
        Width = GridLength.Auto
      });
      base.ColumnDefinitions.Add(new ColumnDefinition
      {
        Width = new GridLength(1.0, GridUnitType.Star)
      });
      PlanTickBox planTickBox;
      if (!chat)
      {
        (planTickBox = new PlanTickBox(isDone, isCurrent, 20.0, 5.0, ink, false)).VerticalAlignment = VerticalAlignment.Center;
      }
      else
      {
        (planTickBox = new PlanTickBox(isDone, isCurrent, 15.0, 4.0, ink, true)).VerticalAlignment = VerticalAlignment.Center;
      }
      this.box = planTickBox;
      this.text = Ui.Text(words, chat ? 12.5 : 17.0, FontWeights.Medium, ink, TextAlignment.Left, 0, false, false);
      this.text.VerticalAlignment = VerticalAlignment.Center;
      if (chat)
      {
        this.text.TextWrapping = TextWrapping.NoWrap;
        this.text.TextTrimming = TextTrimming.CharacterEllipsis;
      }
      this.strike = new Rectangle
      {
        Height = 1.2,
        Fill = Ui.Brush(ink.WithAlpha(chat ? 0.3 : 0.35)),
        HorizontalAlignment = HorizontalAlignment.Left,
        VerticalAlignment = VerticalAlignment.Top,
        IsHitTestVisible = false
      };
      Grid grid = new Grid
      {
        HorizontalAlignment = HorizontalAlignment.Left,
        VerticalAlignment = VerticalAlignment.Center,
        Children = { this.text, this.strike }
      };
      grid.SizeChanged += delegate(object _, SizeChangedEventArgs e)
      {
        this.strike.Width = e.NewSize.Width;
        this.strike.Margin = new Thickness(0.0, e.NewSize.Height * 0.54 - 0.6, 0.0, 0.0);
      };
      Grid.SetColumn(grid, 1);
      base.Children.Add(this.box);
      base.Children.Add(grid);
      this.isDone = !isDone;
      this.Set(isDone, isCurrent, false);
    }

    // Token: 0x06002278 RID: 8824 RVA: 0x006242D0 File Offset: 0x006242D0
    public void Set(bool done, bool current, bool animated = true)
    {
      bool flag = done != this.isDone;
      this.isDone = done;
      this.isCurrent = current;
      this.box.Set(done, current, animated);
      this.text.FontWeight = (current ? FontWeights.SemiBold : FontWeights.Medium);
      this.text.Foreground = Ui.Brush(this.ink.WithAlpha(done ? 0.5 : (current ? (this.chat ? 0.9 : 0.95) : (this.chat ? 0.65 : 0.7))));
      this.strike.CenterPoint = new Vector3(0f, 0f, 0f);
      if (!flag)
      {
        return;
      }
      if (!animated || Anim.ReduceMotion)
      {
        this.strike.Scale = new Vector3(done > false, 1f, 1f);
        return;
      }
      this.strike.Scale = new Vector3(!done, 1f, 1f);
      Compositor compositor = Ui.Compositor;
      Vector3KeyFrameAnimation vector3KeyFrameAnimation = compositor.CreateVector3KeyFrameAnimation();
      vector3KeyFrameAnimation.InsertKeyFrame(1f, new Vector3(done > false, 1f, 1f), Easing.EaseOut(compositor));
      vector3KeyFrameAnimation.Duration = TimeSpan.FromSeconds(0.26);
      vector3KeyFrameAnimation.DelayTime = TimeSpan.FromSeconds(0.08);
      vector3KeyFrameAnimation.Target = "Scale";
      this.strike.StartAnimation(vector3KeyFrameAnimation);
    }

    // Token: 0x040010A7 RID: 4263
    private readonly PlanTickBox box;

    // Token: 0x040010A8 RID: 4264
    private readonly TextBlock text;

    // Token: 0x040010A9 RID: 4265
    private readonly Rectangle strike;

    // Token: 0x040010AA RID: 4266
    private readonly Color ink;

    // Token: 0x040010AB RID: 4267
    private bool isDone;

    // Token: 0x040010AC RID: 4268
    private bool isCurrent;

    // Token: 0x040010AD RID: 4269
    private readonly bool chat;
  }
}
