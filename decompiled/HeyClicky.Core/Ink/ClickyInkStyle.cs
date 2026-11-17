using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using Windows.UI;

namespace HeyClicky.Core.Ink
{
  // Token: 0x02000181 RID: 385
  [NullableContext(2)]
  [Nullable(0)]
  public sealed class ClickyInkStyle : IEquatable<ClickyInkStyle>
  {
    // Token: 0x17000536 RID: 1334
    // (get) Token: 0x060016FD RID: 5885 RVA: 0x003C1E40 File Offset: 0x003C1E40
    [Nullable(1)]
    [CompilerGenerated]
    private Type EqualityContract
    {
      [NullableContext(1)]
      [CompilerGenerated]
      get
      {
        return typeof(ClickyInkStyle);
      }
    }

    // Token: 0x17000537 RID: 1335
    // (get) Token: 0x060016FE RID: 5886 RVA: 0x003C1E4C File Offset: 0x003C1E4C
    // (set) Token: 0x060016FF RID: 5887 RVA: 0x003C1E54 File Offset: 0x003C1E54
    public Color Ink { get; set; }

    // Token: 0x17000538 RID: 1336
    // (get) Token: 0x06001700 RID: 5888 RVA: 0x003C1E60 File Offset: 0x003C1E60
    // (set) Token: 0x06001701 RID: 5889 RVA: 0x003C1E68 File Offset: 0x003C1E68
    public Color Quiet { get; set; }

    // Token: 0x17000539 RID: 1337
    // (get) Token: 0x06001702 RID: 5890 RVA: 0x003C1E74 File Offset: 0x003C1E74
    // (set) Token: 0x06001703 RID: 5891 RVA: 0x003C1E7C File Offset: 0x003C1E7C
    public Color Fresh { get; set; }

    // Token: 0x1700053A RID: 1338
    // (get) Token: 0x06001704 RID: 5892 RVA: 0x003C1E88 File Offset: 0x003C1E88
    // (set) Token: 0x06001705 RID: 5893 RVA: 0x003C1E90 File Offset: 0x003C1E90
    public Color Highlighter { get; set; }

    // Token: 0x1700053B RID: 1339
    // (get) Token: 0x06001706 RID: 5894 RVA: 0x003C1E9C File Offset: 0x003C1E9C
    // (set) Token: 0x06001707 RID: 5895 RVA: 0x003C1EA4 File Offset: 0x003C1EA4
    public int? LatestEdit { get; set; }

    // Token: 0x1700053C RID: 1340
    // (get) Token: 0x06001708 RID: 5896 RVA: 0x003C1EB0 File Offset: 0x003C1EB0
    // (set) Token: 0x06001709 RID: 5897 RVA: 0x003C1EB8 File Offset: 0x003C1EB8
    public ClickyInkPenSchedule PenSchedule { get; set; }

    // Token: 0x1700053D RID: 1341
    // (get) Token: 0x0600170A RID: 5898 RVA: 0x003C1EC4 File Offset: 0x003C1EC4
    // (set) Token: 0x0600170B RID: 5899 RVA: 0x003C1ECC File Offset: 0x003C1ECC
    public bool? IsWorking { get; set; }

    // Token: 0x0600170C RID: 5900 RVA: 0x003C1ED8 File Offset: 0x003C1ED8
    public ClickyInkStyle(Color ink, Color quiet, Color? fresh = null, int? latestEdit = null)
    {
      this.Fresh = ClickyInkStyle.ClickyBlue;
      this.Highlighter = Color.FromArgb((byte)Math.Round(140.25), byte.MaxValue, 226, 122);
      base..ctor();
      this.Ink = ink;
      this.Quiet = quiet;
      this.Fresh = fresh ?? ClickyInkStyle.ClickyBlue;
      this.LatestEdit = latestEdit;
    }

    // Token: 0x0600170D RID: 5901 RVA: 0x003C1F54 File Offset: 0x003C1F54
    [NullableContext(1)]
    public ClickyInkStyle Resting(ClickyInkLiveNote note, bool? isWorking = null)
    {
      ClickyInkStyle clickyInkStyle = this.<Clone>$();
      clickyInkStyle.PenSchedule = ClickyInkPenSchedule.Of(note);
      clickyInkStyle.IsWorking = isWorking;
      return clickyInkStyle;
    }

    // Token: 0x0600170E RID: 5902 RVA: 0x003C1F70 File Offset: 0x003C1F70
    public double? RestingSince(double clock)
    {
      ClickyInkPenSchedule penSchedule = this.PenSchedule;
      if (penSchedule == null)
      {
        return null;
      }
      return penSchedule.RestingSince(clock, this.IsWorking);
    }

    // Token: 0x0600170F RID: 5903 RVA: 0x003C1FA0 File Offset: 0x003C1FA0
    public bool IsPenResting(double clock)
    {
      return this.RestingSince(clock) != null;
    }

    // Token: 0x06001710 RID: 5904 RVA: 0x003C1FC4 File Offset: 0x003C1FC4
    [NullableContext(1)]
    public static bool LooksTheSame(ClickyInkStyle lhs, ClickyInkStyle rhs)
    {
      if (lhs.Ink == rhs.Ink && lhs.Quiet == rhs.Quiet && lhs.Fresh == rhs.Fresh && lhs.Highlighter == rhs.Highlighter)
      {
        int? latestEdit = lhs.LatestEdit;
        int? latestEdit2 = rhs.LatestEdit;
        if ((latestEdit.GetValueOrDefault() == latestEdit2.GetValueOrDefault()) & (latestEdit != null == (latestEdit2 != null)))
        {
          bool? isWorking = lhs.IsWorking;
          bool? isWorking2 = rhs.IsWorking;
          if ((isWorking.GetValueOrDefault() == isWorking2.GetValueOrDefault()) & (isWorking != null == (isWorking2 != null)))
          {
            ClickyInkPenSchedule penSchedule = lhs.PenSchedule;
            IEnumerable<InkRange> enumerable = ((penSchedule != null) ? penSchedule.Busy : null) ?? Array.Empty<InkRange>();
            ClickyInkPenSchedule penSchedule2 = rhs.PenSchedule;
            if (enumerable.SequenceEqual<InkRange>(((penSchedule2 != null) ? penSchedule2.Busy : null) ?? Array.Empty<InkRange>()))
            {
              ClickyInkPenSchedule penSchedule3 = lhs.PenSchedule;
              IEnumerable<double> enumerable2 = ((penSchedule3 != null) ? penSchedule3.Arrivals : null) ?? Array.Empty<double>();
              ClickyInkPenSchedule penSchedule4 = rhs.PenSchedule;
              if (enumerable2.SequenceEqual<double>(((penSchedule4 != null) ? penSchedule4.Arrivals : null) ?? Array.Empty<double>()))
              {
                return lhs.PenSchedule == null == (rhs.PenSchedule == null);
              }
            }
          }
        }
      }
      return false;
    }

    // Token: 0x06001711 RID: 5905 RVA: 0x003C2124 File Offset: 0x003C2124
    [NullableContext(1)]
    [CompilerGenerated]
    public override string ToString()
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("ClickyInkStyle");
      stringBuilder.Append(" { ");
      if (this.PrintMembers(stringBuilder))
      {
        stringBuilder.Append(' ');
      }
      stringBuilder.Append('}');
      return stringBuilder.ToString();
    }

    // Token: 0x06001712 RID: 5906 RVA: 0x003C2170 File Offset: 0x003C2170
    [NullableContext(1)]
    [CompilerGenerated]
    private bool PrintMembers(StringBuilder builder)
    {
      RuntimeHelpers.EnsureSufficientExecutionStack();
      builder.Append("Ink = ");
      builder.Append(this.Ink.ToString());
      builder.Append(", Quiet = ");
      builder.Append(this.Quiet.ToString());
      builder.Append(", Fresh = ");
      builder.Append(this.Fresh.ToString());
      builder.Append(", Highlighter = ");
      builder.Append(this.Highlighter.ToString());
      builder.Append(", LatestEdit = ");
      builder.Append(this.LatestEdit.ToString());
      builder.Append(", PenSchedule = ");
      builder.Append(this.PenSchedule);
      builder.Append(", IsWorking = ");
      builder.Append(this.IsWorking.ToString());
      return true;
    }

    // Token: 0x06001713 RID: 5907 RVA: 0x003C2288 File Offset: 0x003C2288
    [CompilerGenerated]
    public static bool operator !=(ClickyInkStyle left, ClickyInkStyle right)
    {
      return !(left == right);
    }

    // Token: 0x06001714 RID: 5908 RVA: 0x003C2294 File Offset: 0x003C2294
    [CompilerGenerated]
    public static bool operator ==(ClickyInkStyle left, ClickyInkStyle right)
    {
      return left == right || (left != null && left.Equals(right));
    }

    // Token: 0x06001715 RID: 5909 RVA: 0x003C22A8 File Offset: 0x003C22A8
    [CompilerGenerated]
    public override int GetHashCode()
    {
      return ((((((EqualityComparer<Type>.Default.GetHashCode(this.EqualityContract) * -1521134295 + EqualityComparer<Color>.Default.GetHashCode(this.<Ink>k__BackingField)) * -1521134295 + EqualityComparer<Color>.Default.GetHashCode(this.<Quiet>k__BackingField)) * -1521134295 + EqualityComparer<Color>.Default.GetHashCode(this.<Fresh>k__BackingField)) * -1521134295 + EqualityComparer<Color>.Default.GetHashCode(this.<Highlighter>k__BackingField)) * -1521134295 + EqualityComparer<int?>.Default.GetHashCode(this.<LatestEdit>k__BackingField)) * -1521134295 + EqualityComparer<ClickyInkPenSchedule>.Default.GetHashCode(this.<PenSchedule>k__BackingField)) * -1521134295 + EqualityComparer<bool?>.Default.GetHashCode(this.<IsWorking>k__BackingField);
    }

    // Token: 0x06001716 RID: 5910 RVA: 0x003C2368 File Offset: 0x003C2368
    [CompilerGenerated]
    public override bool Equals(object obj)
    {
      return this.Equals(obj as ClickyInkStyle);
    }

    // Token: 0x06001717 RID: 5911 RVA: 0x003C2378 File Offset: 0x003C2378
    [CompilerGenerated]
    public bool Equals(ClickyInkStyle other)
    {
      return this == other || (other != null && this.EqualityContract == other.EqualityContract && EqualityComparer<Color>.Default.Equals(this.<Ink>k__BackingField, other.<Ink>k__BackingField) && EqualityComparer<Color>.Default.Equals(this.<Quiet>k__BackingField, other.<Quiet>k__BackingField) && EqualityComparer<Color>.Default.Equals(this.<Fresh>k__BackingField, other.<Fresh>k__BackingField) && EqualityComparer<Color>.Default.Equals(this.<Highlighter>k__BackingField, other.<Highlighter>k__BackingField) && EqualityComparer<int?>.Default.Equals(this.<LatestEdit>k__BackingField, other.<LatestEdit>k__BackingField) && EqualityComparer<ClickyInkPenSchedule>.Default.Equals(this.<PenSchedule>k__BackingField, other.<PenSchedule>k__BackingField) && EqualityComparer<bool?>.Default.Equals(this.<IsWorking>k__BackingField, other.<IsWorking>k__BackingField));
    }

    // Token: 0x06001719 RID: 5913 RVA: 0x003C2460 File Offset: 0x003C2460
    [CompilerGenerated]
    private ClickyInkStyle([Nullable(1)] ClickyInkStyle original)
    {
      this.Ink = original.<Ink>k__BackingField;
      this.Quiet = original.<Quiet>k__BackingField;
      this.Fresh = original.<Fresh>k__BackingField;
      this.Highlighter = original.<Highlighter>k__BackingField;
      this.LatestEdit = original.<LatestEdit>k__BackingField;
      this.PenSchedule = original.<PenSchedule>k__BackingField;
      this.IsWorking = original.<IsWorking>k__BackingField;
    }

    // Token: 0x04000A02 RID: 2562
    public static readonly Color ClickyBlue = Color.FromArgb(byte.MaxValue, 45, 91, 227);
  }
}
