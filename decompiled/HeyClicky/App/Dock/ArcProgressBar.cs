using System;
using System.Runtime.CompilerServices;
using HeyClicky.Agents.Jobs;
using HeyClicky.App.Design;
using HeyClicky.App.UI;
using Microsoft.UI.Text;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Documents;
using Windows.UI;

namespace HeyClicky.App.Dock
{
  // Token: 0x02000324 RID: 804
  [NullableContext(2)]
  [Nullable(0)]
  public sealed class ArcProgressBar : Grid
  {
    // Token: 0x0600228F RID: 8847 RVA: 0x00625A48 File Offset: 0x00625A48
    public ArcProgressBar(ClickyArcPlan plan, Color ink, bool isLarge, bool showsLabel = true, bool isCompact = false)
    {
      this.isLarge = isLarge;
      this.showsLabel = showsLabel;
      this.isCompact = isCompact;
      this.ink = ink;
      this.Build(plan);
    }

    // Token: 0x17000662 RID: 1634
    // (get) Token: 0x06002290 RID: 8848 RVA: 0x00625A78 File Offset: 0x00625A78
    private double TroughHeight
    {
      get
      {
        return (double)(this.isLarge ? 16 : (this.isCompact ? 9 : 12));
      }
    }

    // Token: 0x17000663 RID: 1635
    // (get) Token: 0x06002291 RID: 8849 RVA: 0x00625A98 File Offset: 0x00625A98
    private double LabelSize
    {
      get
      {
        return (double)(this.isLarge ? 21 : (this.isCompact ? 11 : 14));
      }
    }

    // Token: 0x06002292 RID: 8850 RVA: 0x00625AB8 File Offset: 0x00625AB8
    private void Build(ClickyArcPlan plan)
    {
      base.Children.Clear();
      base.RowDefinitions.Clear();
      base.ColumnDefinitions.Clear();
      bool flag = plan != null && !plan.IsEmpty;
      this.hadPlan = flag;
      this.track = (flag ? new GelTrackView(plan.Steps.Count, plan.DoneCount, plan.CurrentIndex, this.TroughHeight, this.ink, false) : new GelTrackView(1, 0, null, this.TroughHeight, this.ink, true));
      string text;
      if (this.showsLabel)
      {
        if (!flag)
        {
          text = "Working";
        }
        else if (!this.isLarge)
        {
          DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(1, 2);
          defaultInterpolatedStringHandler.AppendFormatted<int>(plan.DoneCount);
          defaultInterpolatedStringHandler.AppendLiteral("/");
          defaultInterpolatedStringHandler.AppendFormatted<int>(plan.Steps.Count);
          text = defaultInterpolatedStringHandler.ToStringAndClear();
        }
        else
        {
          DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(10, 2);
          defaultInterpolatedStringHandler.AppendFormatted<int>(plan.DoneCount);
          defaultInterpolatedStringHandler.AppendLiteral(" of ");
          defaultInterpolatedStringHandler.AppendFormatted<int>(plan.Steps.Count);
          defaultInterpolatedStringHandler.AppendLiteral(" steps");
          text = defaultInterpolatedStringHandler.ToStringAndClear();
        }
      }
      else
      {
        text = null;
      }
      string text2 = text;
      this.label = ((text2 == null) ? null : Ui.Text(text2, this.LabelSize, FontWeights.SemiBold, this.ink.WithAlpha(this.isLarge ? 0.6 : 0.55), TextAlignment.Left, 0, false, false));
      if (this.label != null)
      {
        this.label.FontFamily = DS.Fonts.Display;
        Typography.SetNumeralAlignment(this.label, FontNumeralAlignment.Tabular);
      }
      if (this.isLarge)
      {
        base.RowSpacing = 10.0;
        base.RowDefinitions.Add(new RowDefinition
        {
          Height = GridLength.Auto
        });
        base.RowDefinitions.Add(new RowDefinition
        {
          Height = GridLength.Auto
        });
        base.Children.Add(this.track);
        if (this.label != null)
        {
          this.label.HorizontalAlignment = HorizontalAlignment.Center;
          Grid.SetRow(this.label, 1);
          base.Children.Add(this.label);
        }
        return;
      }
      base.ColumnSpacing = (double)(this.isCompact ? 9 : 12);
      base.ColumnDefinitions.Add(new ColumnDefinition
      {
        Width = new GridLength(1.0, GridUnitType.Star)
      });
      base.ColumnDefinitions.Add(new ColumnDefinition
      {
        Width = GridLength.Auto
      });
      this.track.VerticalAlignment = VerticalAlignment.Center;
      base.Children.Add(this.track);
      if (this.label != null)
      {
        this.label.VerticalAlignment = VerticalAlignment.Center;
        Grid.SetColumn(this.label, 1);
        base.Children.Add(this.label);
      }
    }

    // Token: 0x06002293 RID: 8851 RVA: 0x00625D9C File Offset: 0x00625D9C
    public void Update(ClickyArcPlan plan)
    {
      bool flag = plan != null && !plan.IsEmpty;
      if (flag != this.hadPlan || this.track == null)
      {
        this.Build(plan);
        return;
      }
      if (!flag)
      {
        return;
      }
      this.track.Update(plan.Steps.Count, plan.DoneCount, plan.CurrentIndex);
      if (this.label != null)
      {
        TextBlock textBlock = this.label;
        string text;
        if (!this.isLarge)
        {
          DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(1, 2);
          defaultInterpolatedStringHandler.AppendFormatted<int>(plan.DoneCount);
          defaultInterpolatedStringHandler.AppendLiteral("/");
          defaultInterpolatedStringHandler.AppendFormatted<int>(plan.Steps.Count);
          text = defaultInterpolatedStringHandler.ToStringAndClear();
        }
        else
        {
          DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(10, 2);
          defaultInterpolatedStringHandler.AppendFormatted<int>(plan.DoneCount);
          defaultInterpolatedStringHandler.AppendLiteral(" of ");
          defaultInterpolatedStringHandler.AppendFormatted<int>(plan.Steps.Count);
          defaultInterpolatedStringHandler.AppendLiteral(" steps");
          text = defaultInterpolatedStringHandler.ToStringAndClear();
        }
        textBlock.Text = text;
      }
    }

    // Token: 0x040010C5 RID: 4293
    private readonly bool isLarge;

    // Token: 0x040010C6 RID: 4294
    private readonly bool showsLabel;

    // Token: 0x040010C7 RID: 4295
    private readonly bool isCompact;

    // Token: 0x040010C8 RID: 4296
    private readonly Color ink;

    // Token: 0x040010C9 RID: 4297
    private GelTrackView track;

    // Token: 0x040010CA RID: 4298
    private TextBlock label;

    // Token: 0x040010CB RID: 4299
    private bool hadPlan;
  }
}
