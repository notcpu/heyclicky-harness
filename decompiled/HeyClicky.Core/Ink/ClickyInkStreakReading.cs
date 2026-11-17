using System;
using System.Collections.Generic;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Text;

namespace HeyClicky.Core.Ink
{
  // Token: 0x02000141 RID: 321
  [NullableContext(1)]
  [Nullable(0)]
  public sealed class ClickyInkStreakReading : IEquatable<ClickyInkStreakReading>
  {
    // Token: 0x170004BB RID: 1211
    // (get) Token: 0x0600142D RID: 5165 RVA: 0x003ABEA4 File Offset: 0x003ABEA4
    [CompilerGenerated]
    private Type EqualityContract
    {
      [CompilerGenerated]
      get
      {
        return typeof(ClickyInkStreakReading);
      }
    }

    // Token: 0x170004BC RID: 1212
    // (get) Token: 0x0600142E RID: 5166 RVA: 0x003ABEB0 File Offset: 0x003ABEB0
    public List<ClickyInkStreakDay> Days { get; }

    // Token: 0x170004BD RID: 1213
    // (get) Token: 0x0600142F RID: 5167 RVA: 0x003ABEB8 File Offset: 0x003ABEB8
    public string Shown { get; }

    // Token: 0x170004BE RID: 1214
    // (get) Token: 0x06001430 RID: 5168 RVA: 0x003ABEC0 File Offset: 0x003ABEC0
    public string Unit { get; }

    // Token: 0x170004BF RID: 1215
    // (get) Token: 0x06001431 RID: 5169 RVA: 0x003ABEC8 File Offset: 0x003ABEC8
    [Nullable(2)]
    public string PreviousShown
    {
      [NullableContext(2)]
      get;
    }

    // Token: 0x170004C0 RID: 1216
    // (get) Token: 0x06001432 RID: 5170 RVA: 0x003ABED0 File Offset: 0x003ABED0
    public int? PreviousCount { get; }

    // Token: 0x06001433 RID: 5171 RVA: 0x003ABED8 File Offset: 0x003ABED8
    public ClickyInkStreakReading(ClickyInkDocument.Block block)
    {
      string text = ((block["days"] != null) ? "days" : ((block["marks"] != null) ? "marks" : "week"));
      this.Days = ClickyInkStreak.Days(block[text]);
      string text2 = block["count"];
      long num2;
      if (text2 != null)
      {
        ClickyInkNumber? clickyInkNumber = ClickyInkNumber.Parse(text2);
        double? num = ((clickyInkNumber != null) ? new double?(clickyInkNumber.GetValueOrDefault().Value) : null);
        if (num != null)
        {
          double valueOrDefault = num.GetValueOrDefault();
          num2 = InkText.InkClamping(valueOrDefault);
          goto IL_00B0;
        }
      }
      num2 = (long)ClickyInkStreak.CurrentStreak(this.Days);
      IL_00B0:
      long num3 = num2;
      this.Shown = num3.ToString(CultureInfo.InvariantCulture);
      this.Unit = block["unit"] ?? ((num3 == 1L) ? "day in a row" : "days in a row");
      string text3 = ClickyInkChange.Previous(block, text);
      if (text3 != null)
      {
        List<ClickyInkStreakDay> list = ClickyInkStreak.Days(text3);
        this.PreviousCount = new int?(list.Count);
        string text4 = ClickyInkChange.Previous(block, "count");
        long num4;
        if (text4 != null)
        {
          ClickyInkNumber? clickyInkNumber = ClickyInkNumber.Parse(text4);
          double? num = ((clickyInkNumber != null) ? new double?(clickyInkNumber.GetValueOrDefault().Value) : null);
          if (num != null)
          {
            double valueOrDefault2 = num.GetValueOrDefault();
            num4 = InkText.InkClamping(valueOrDefault2);
            goto IL_017F;
          }
        }
        num4 = (long)ClickyInkStreak.CurrentStreak(list);
        IL_017F:
        long num5 = num4;
        this.PreviousShown = num5.ToString(CultureInfo.InvariantCulture);
      }
    }

    // Token: 0x06001434 RID: 5172 RVA: 0x003AC078 File Offset: 0x003AC078
    [CompilerGenerated]
    public override string ToString()
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("ClickyInkStreakReading");
      stringBuilder.Append(" { ");
      if (this.PrintMembers(stringBuilder))
      {
        stringBuilder.Append(' ');
      }
      stringBuilder.Append('}');
      return stringBuilder.ToString();
    }

    // Token: 0x06001435 RID: 5173 RVA: 0x003AC0C4 File Offset: 0x003AC0C4
    [CompilerGenerated]
    private bool PrintMembers(StringBuilder builder)
    {
      RuntimeHelpers.EnsureSufficientExecutionStack();
      builder.Append("Days = ");
      builder.Append(this.Days);
      builder.Append(", Shown = ");
      builder.Append(this.Shown);
      builder.Append(", Unit = ");
      builder.Append(this.Unit);
      builder.Append(", PreviousShown = ");
      builder.Append(this.PreviousShown);
      builder.Append(", PreviousCount = ");
      builder.Append(this.PreviousCount.ToString());
      return true;
    }

    // Token: 0x06001436 RID: 5174 RVA: 0x003AC164 File Offset: 0x003AC164
    [NullableContext(2)]
    [CompilerGenerated]
    public static bool operator !=(ClickyInkStreakReading left, ClickyInkStreakReading right)
    {
      return !(left == right);
    }

    // Token: 0x06001437 RID: 5175 RVA: 0x003AC170 File Offset: 0x003AC170
    [NullableContext(2)]
    [CompilerGenerated]
    public static bool operator ==(ClickyInkStreakReading left, ClickyInkStreakReading right)
    {
      return left == right || (left != null && left.Equals(right));
    }

    // Token: 0x06001438 RID: 5176 RVA: 0x003AC184 File Offset: 0x003AC184
    [CompilerGenerated]
    public override int GetHashCode()
    {
      return ((((EqualityComparer<Type>.Default.GetHashCode(this.EqualityContract) * -1521134295 + EqualityComparer<List<ClickyInkStreakDay>>.Default.GetHashCode(this.<Days>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Shown>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Unit>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<PreviousShown>k__BackingField)) * -1521134295 + EqualityComparer<int?>.Default.GetHashCode(this.<PreviousCount>k__BackingField);
    }

    // Token: 0x06001439 RID: 5177 RVA: 0x003AC214 File Offset: 0x003AC214
    [NullableContext(2)]
    [CompilerGenerated]
    public override bool Equals(object obj)
    {
      return this.Equals(obj as ClickyInkStreakReading);
    }

    // Token: 0x0600143A RID: 5178 RVA: 0x003AC224 File Offset: 0x003AC224
    [NullableContext(2)]
    [CompilerGenerated]
    public bool Equals(ClickyInkStreakReading other)
    {
      return this == other || (other != null && this.EqualityContract == other.EqualityContract && EqualityComparer<List<ClickyInkStreakDay>>.Default.Equals(this.<Days>k__BackingField, other.<Days>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<Shown>k__BackingField, other.<Shown>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<Unit>k__BackingField, other.<Unit>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<PreviousShown>k__BackingField, other.<PreviousShown>k__BackingField) && EqualityComparer<int?>.Default.Equals(this.<PreviousCount>k__BackingField, other.<PreviousCount>k__BackingField));
    }

    // Token: 0x0600143C RID: 5180 RVA: 0x003AC2D4 File Offset: 0x003AC2D4
    [CompilerGenerated]
    private ClickyInkStreakReading(ClickyInkStreakReading original)
    {
      this.Days = original.<Days>k__BackingField;
      this.Shown = original.<Shown>k__BackingField;
      this.Unit = original.<Unit>k__BackingField;
      this.PreviousShown = original.<PreviousShown>k__BackingField;
      this.PreviousCount = original.<PreviousCount>k__BackingField;
    }
  }
}
