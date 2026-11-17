using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

namespace HeyClicky.Core.Ink
{
  // Token: 0x0200013B RID: 315
  [NullableContext(2)]
  [Nullable(0)]
  public sealed class ClickyInkRunLine : IEquatable<ClickyInkRunLine>
  {
    // Token: 0x170004AB RID: 1195
    // (get) Token: 0x060013F6 RID: 5110 RVA: 0x003AA9E0 File Offset: 0x003AA9E0
    [Nullable(1)]
    [CompilerGenerated]
    private Type EqualityContract
    {
      [NullableContext(1)]
      [CompilerGenerated]
      get
      {
        return typeof(ClickyInkRunLine);
      }
    }

    // Token: 0x170004AC RID: 1196
    // (get) Token: 0x060013F7 RID: 5111 RVA: 0x003AA9EC File Offset: 0x003AA9EC
    public string Number { get; }

    // Token: 0x170004AD RID: 1197
    // (get) Token: 0x060013F8 RID: 5112 RVA: 0x003AA9F4 File Offset: 0x003AA9F4
    public string RanAt { get; }

    // Token: 0x170004AE RID: 1198
    // (get) Token: 0x060013F9 RID: 5113 RVA: 0x003AA9FC File Offset: 0x003AA9FC
    public string Next { get; }

    // Token: 0x170004AF RID: 1199
    // (get) Token: 0x060013FA RID: 5114 RVA: 0x003AAA04 File Offset: 0x003AAA04
    public string Extra { get; }

    // Token: 0x060013FB RID: 5115 RVA: 0x003AAA0C File Offset: 0x003AAA0C
    [NullableContext(1)]
    public ClickyInkRunLine(ClickyInkDocument.Block block)
    {
      ClickyInkRunLine.<>c__DisplayClass14_0 CS$<>8__locals1;
      CS$<>8__locals1.a = block.Attributes;
      string text;
      if ((text = ClickyInkRunLine.<.ctor>g__Get|14_0("n", ref CS$<>8__locals1)) == null && (text = ClickyInkRunLine.<.ctor>g__Get|14_0("run", ref CS$<>8__locals1)) == null)
      {
        text = ClickyInkRunLine.<.ctor>g__Get|14_0("number", ref CS$<>8__locals1) ?? ClickyInkRunLine.<.ctor>g__Get|14_0("edition", ref CS$<>8__locals1);
      }
      string text2 = text;
      string text3 = ((text2 != null) ? text2.TrimWs() : null);
      this.Number = (string.IsNullOrEmpty(text3) ? null : text3);
      string text4;
      if ((text4 = ClickyInkRunLine.<.ctor>g__Get|14_0("at", ref CS$<>8__locals1)) == null && (text4 = ClickyInkRunLine.<.ctor>g__Get|14_0("date", ref CS$<>8__locals1)) == null)
      {
        text4 = ClickyInkRunLine.<.ctor>g__Get|14_0("when", ref CS$<>8__locals1) ?? ClickyInkRunLine.<.ctor>g__Get|14_0("ran", ref CS$<>8__locals1);
      }
      string text5 = text4;
      this.RanAt = (string.IsNullOrEmpty(text5) ? null : text5);
      string text6 = ClickyInkRunLine.<.ctor>g__Get|14_0("next", ref CS$<>8__locals1);
      this.Next = (string.IsNullOrEmpty(text6) ? null : text6);
      string text7 = block.Text.TrimWs();
      if (this.Number == null && text7.Lowered().HasPrefix("run"))
      {
        string text8 = text7;
        string text9 = text8.Substring(3, text8.Length - 3).SplitCharacters((string c) => !InkText.IsNumber(c)).FirstOrDefault<string>();
        if (text9 != null)
        {
          this.Number = text9;
          List<string> list = (from p in text7.Split('·', StringSplitOptions.None)
            select p.TrimWs()).ToList<string>();
          text7 = string.Join(" · ", list.Skip<string>(1));
          if (this.RanAt == null && text7.Length > 0)
          {
            this.RanAt = text7;
            text7 = "";
          }
        }
      }
      string text10 = ClickyInkRunLine.<.ctor>g__Get|14_0("note", ref CS$<>8__locals1);
      string text11 = ((text10 != null) ? text10.TrimWs() : null);
      string text12 = string.Join(" · ", new string[]
      {
        (text7.Length == 0) ? null : text7,
        string.IsNullOrEmpty(text11) ? null : text11
      }.Where<string>((string s) => s != null));
      this.Extra = ((text12.Length == 0) ? null : text12);
    }

    // Token: 0x060013FC RID: 5116 RVA: 0x003AAC64 File Offset: 0x003AAC64
    [NullableContext(1)]
    [CompilerGenerated]
    public override string ToString()
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("ClickyInkRunLine");
      stringBuilder.Append(" { ");
      if (this.PrintMembers(stringBuilder))
      {
        stringBuilder.Append(' ');
      }
      stringBuilder.Append('}');
      return stringBuilder.ToString();
    }

    // Token: 0x060013FD RID: 5117 RVA: 0x003AACB0 File Offset: 0x003AACB0
    [NullableContext(1)]
    [CompilerGenerated]
    private bool PrintMembers(StringBuilder builder)
    {
      RuntimeHelpers.EnsureSufficientExecutionStack();
      builder.Append("Number = ");
      builder.Append(this.Number);
      builder.Append(", RanAt = ");
      builder.Append(this.RanAt);
      builder.Append(", Next = ");
      builder.Append(this.Next);
      builder.Append(", Extra = ");
      builder.Append(this.Extra);
      return true;
    }

    // Token: 0x060013FE RID: 5118 RVA: 0x003AAD28 File Offset: 0x003AAD28
    [CompilerGenerated]
    public static bool operator !=(ClickyInkRunLine left, ClickyInkRunLine right)
    {
      return !(left == right);
    }

    // Token: 0x060013FF RID: 5119 RVA: 0x003AAD34 File Offset: 0x003AAD34
    [CompilerGenerated]
    public static bool operator ==(ClickyInkRunLine left, ClickyInkRunLine right)
    {
      return left == right || (left != null && left.Equals(right));
    }

    // Token: 0x06001400 RID: 5120 RVA: 0x003AAD48 File Offset: 0x003AAD48
    [CompilerGenerated]
    public override int GetHashCode()
    {
      return (((EqualityComparer<Type>.Default.GetHashCode(this.EqualityContract) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Number>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<RanAt>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Next>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Extra>k__BackingField);
    }

    // Token: 0x06001401 RID: 5121 RVA: 0x003AADC4 File Offset: 0x003AADC4
    [CompilerGenerated]
    public override bool Equals(object obj)
    {
      return this.Equals(obj as ClickyInkRunLine);
    }

    // Token: 0x06001402 RID: 5122 RVA: 0x003AADD4 File Offset: 0x003AADD4
    [CompilerGenerated]
    public bool Equals(ClickyInkRunLine other)
    {
      return this == other || (other != null && this.EqualityContract == other.EqualityContract && EqualityComparer<string>.Default.Equals(this.<Number>k__BackingField, other.<Number>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<RanAt>k__BackingField, other.<RanAt>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<Next>k__BackingField, other.<Next>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<Extra>k__BackingField, other.<Extra>k__BackingField));
    }

    // Token: 0x06001404 RID: 5124 RVA: 0x003AAE68 File Offset: 0x003AAE68
    [CompilerGenerated]
    private ClickyInkRunLine([Nullable(1)] ClickyInkRunLine original)
    {
      this.Number = original.<Number>k__BackingField;
      this.RanAt = original.<RanAt>k__BackingField;
      this.Next = original.<Next>k__BackingField;
      this.Extra = original.<Extra>k__BackingField;
    }

    // Token: 0x06001405 RID: 5125 RVA: 0x003AAEA0 File Offset: 0x003AAEA0
    [NullableContext(1)]
    [CompilerGenerated]
    [return: Nullable(2)]
    internal static string <.ctor>g__Get|14_0(string key, ref ClickyInkRunLine.<>c__DisplayClass14_0 A_1)
    {
      string text;
      if (!A_1.a.TryGetValue(key, out text))
      {
        return null;
      }
      return text;
    }
  }
}
