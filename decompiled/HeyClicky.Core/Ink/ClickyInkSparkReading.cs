using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

namespace HeyClicky.Core.Ink
{
  // Token: 0x0200013F RID: 319
  [NullableContext(1)]
  [Nullable(0)]
  public sealed class ClickyInkSparkReading : IEquatable<ClickyInkSparkReading>
  {
    // Token: 0x170004B0 RID: 1200
    // (get) Token: 0x06001410 RID: 5136 RVA: 0x003AB684 File Offset: 0x003AB684
    [CompilerGenerated]
    private Type EqualityContract
    {
      [CompilerGenerated]
      get
      {
        return typeof(ClickyInkSparkReading);
      }
    }

    // Token: 0x170004B1 RID: 1201
    // (get) Token: 0x06001411 RID: 5137 RVA: 0x003AB690 File Offset: 0x003AB690
    public List<double> Values { get; }

    // Token: 0x170004B2 RID: 1202
    // (get) Token: 0x06001412 RID: 5138 RVA: 0x003AB698 File Offset: 0x003AB698
    public List<string> Tokens { get; }

    // Token: 0x170004B3 RID: 1203
    // (get) Token: 0x06001413 RID: 5139 RVA: 0x003AB6A0 File Offset: 0x003AB6A0
    [Nullable(2)]
    public List<double> PreviousValues
    {
      [NullableContext(2)]
      get;
    }

    // Token: 0x170004B4 RID: 1204
    // (get) Token: 0x06001414 RID: 5140 RVA: 0x003AB6A8 File Offset: 0x003AB6A8
    public List<string> Labels { get; }

    // Token: 0x06001415 RID: 5141 RVA: 0x003AB6B0 File Offset: 0x003AB6B0
    public ClickyInkSparkReading(ClickyInkDocument.Block block)
    {
      string text = ((block["values"] != null) ? "values" : ((block["points"] != null) ? "points" : ((block["data"] != null) ? "data" : "series")));
      this.Tokens = ClickyInkSeries.Tokens(block[text]);
      IEnumerable<string> tokens = this.Tokens;
      Func<string, ClickyInkNumber?> func;
      if ((func = ClickyInkSparkReading.<>O.<0>__Parse) == null)
      {
        func = (ClickyInkSparkReading.<>O.<0>__Parse = new Func<string, ClickyInkNumber?>(ClickyInkNumber.Parse));
      }
      this.Values = (from n in tokens.Select<string, ClickyInkNumber?>(func)
        where n != null
        select n.Value.Value).ToList<double>();
      string text2 = ClickyInkChange.Previous(block, text);
      this.PreviousValues = ((text2 == null) ? null : ClickyInkSeries.Values(text2));
      this.previousTokens = ((text2 == null) ? new List<string>() : ClickyInkSeries.Tokens(text2));
      this.Labels = ClickyInkSeries.Labels(block["labels"]);
    }

    // Token: 0x170004B5 RID: 1205
    // (get) Token: 0x06001416 RID: 5142 RVA: 0x003AB7D8 File Offset: 0x003AB7D8
    [Nullable(2)]
    public string Last
    {
      [NullableContext(2)]
      get
      {
        if (this.Tokens.Count <= 0)
        {
          return null;
        }
        List<string> tokens = this.Tokens;
        return tokens[tokens.Count - 1];
      }
    }

    // Token: 0x170004B6 RID: 1206
    // (get) Token: 0x06001417 RID: 5143 RVA: 0x003AB800 File Offset: 0x003AB800
    [Nullable(2)]
    public string PreviousLast
    {
      [NullableContext(2)]
      get
      {
        if (this.previousTokens.Count <= 0)
        {
          return null;
        }
        List<string> list = this.previousTokens;
        return list[list.Count - 1];
      }
    }

    // Token: 0x170004B7 RID: 1207
    // (get) Token: 0x06001418 RID: 5144 RVA: 0x003AB828 File Offset: 0x003AB828
    [TupleElementNames(new string[] { "Text", "Value" })]
    [Nullable(new byte[] { 0, 1 })]
    public ValueTuple<string, double>? Step
    {
      [return: TupleElementNames(new string[] { "Text", "Value" })]
      [return: Nullable(new byte[] { 0, 1 })]
      get
      {
        if (this.Tokens.Count <= 1)
        {
          return null;
        }
        List<string> tokens = this.Tokens;
        string text = tokens[tokens.Count - 2];
        List<string> tokens2 = this.Tokens;
        return ClickyInkNumber.Difference(text, tokens2[tokens2.Count - 1]);
      }
    }

    // Token: 0x06001419 RID: 5145 RVA: 0x003AB878 File Offset: 0x003AB878
    [CompilerGenerated]
    public override string ToString()
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("ClickyInkSparkReading");
      stringBuilder.Append(" { ");
      if (this.PrintMembers(stringBuilder))
      {
        stringBuilder.Append(' ');
      }
      stringBuilder.Append('}');
      return stringBuilder.ToString();
    }

    // Token: 0x0600141A RID: 5146 RVA: 0x003AB8C4 File Offset: 0x003AB8C4
    [CompilerGenerated]
    private bool PrintMembers(StringBuilder builder)
    {
      RuntimeHelpers.EnsureSufficientExecutionStack();
      builder.Append("Values = ");
      builder.Append(this.Values);
      builder.Append(", Tokens = ");
      builder.Append(this.Tokens);
      builder.Append(", PreviousValues = ");
      builder.Append(this.PreviousValues);
      builder.Append(", Labels = ");
      builder.Append(this.Labels);
      builder.Append(", Last = ");
      builder.Append(this.Last);
      builder.Append(", PreviousLast = ");
      builder.Append(this.PreviousLast);
      builder.Append(", Step = ");
      builder.Append(this.Step.ToString());
      return true;
    }

    // Token: 0x0600141B RID: 5147 RVA: 0x003AB994 File Offset: 0x003AB994
    [NullableContext(2)]
    [CompilerGenerated]
    public static bool operator !=(ClickyInkSparkReading left, ClickyInkSparkReading right)
    {
      return !(left == right);
    }

    // Token: 0x0600141C RID: 5148 RVA: 0x003AB9A0 File Offset: 0x003AB9A0
    [NullableContext(2)]
    [CompilerGenerated]
    public static bool operator ==(ClickyInkSparkReading left, ClickyInkSparkReading right)
    {
      return left == right || (left != null && left.Equals(right));
    }

    // Token: 0x0600141D RID: 5149 RVA: 0x003AB9B4 File Offset: 0x003AB9B4
    [CompilerGenerated]
    public override int GetHashCode()
    {
      return ((((EqualityComparer<Type>.Default.GetHashCode(this.EqualityContract) * -1521134295 + EqualityComparer<List<double>>.Default.GetHashCode(this.<Values>k__BackingField)) * -1521134295 + EqualityComparer<List<string>>.Default.GetHashCode(this.<Tokens>k__BackingField)) * -1521134295 + EqualityComparer<List<double>>.Default.GetHashCode(this.<PreviousValues>k__BackingField)) * -1521134295 + EqualityComparer<List<string>>.Default.GetHashCode(this.<Labels>k__BackingField)) * -1521134295 + EqualityComparer<List<string>>.Default.GetHashCode(this.previousTokens);
    }

    // Token: 0x0600141E RID: 5150 RVA: 0x003ABA44 File Offset: 0x003ABA44
    [NullableContext(2)]
    [CompilerGenerated]
    public override bool Equals(object obj)
    {
      return this.Equals(obj as ClickyInkSparkReading);
    }

    // Token: 0x0600141F RID: 5151 RVA: 0x003ABA54 File Offset: 0x003ABA54
    [NullableContext(2)]
    [CompilerGenerated]
    public bool Equals(ClickyInkSparkReading other)
    {
      return this == other || (other != null && this.EqualityContract == other.EqualityContract && EqualityComparer<List<double>>.Default.Equals(this.<Values>k__BackingField, other.<Values>k__BackingField) && EqualityComparer<List<string>>.Default.Equals(this.<Tokens>k__BackingField, other.<Tokens>k__BackingField) && EqualityComparer<List<double>>.Default.Equals(this.<PreviousValues>k__BackingField, other.<PreviousValues>k__BackingField) && EqualityComparer<List<string>>.Default.Equals(this.<Labels>k__BackingField, other.<Labels>k__BackingField) && EqualityComparer<List<string>>.Default.Equals(this.previousTokens, other.previousTokens));
    }

    // Token: 0x06001421 RID: 5153 RVA: 0x003ABB04 File Offset: 0x003ABB04
    [CompilerGenerated]
    private ClickyInkSparkReading(ClickyInkSparkReading original)
    {
      this.Values = original.<Values>k__BackingField;
      this.Tokens = original.<Tokens>k__BackingField;
      this.PreviousValues = original.<PreviousValues>k__BackingField;
      this.Labels = original.<Labels>k__BackingField;
      this.previousTokens = original.previousTokens;
    }

    // Token: 0x040008E9 RID: 2281
    private readonly List<string> previousTokens;

    // Token: 0x020004C5 RID: 1221
    [CompilerGenerated]
    private static class <>O
    {
      // Token: 0x04001DE3 RID: 7651
      [Nullable(0)]
      public static Func<string, ClickyInkNumber?> <0>__Parse;
    }
  }
}
