using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

namespace HeyClicky.Core.Ink
{
  // Token: 0x0200013A RID: 314
  [NullableContext(1)]
  [Nullable(0)]
  public static class ClickyInkPipeline
  {
    // Token: 0x060013F3 RID: 5107 RVA: 0x003AA718 File Offset: 0x003AA718
    public static List<ClickyInkPipeline.Stage> Stages([Nullable(2)] string input)
    {
      string text = ((input != null) ? input.TrimWs() : null) ?? "";
      if (text.Length == 0)
      {
        return new List<ClickyInkPipeline.Stage>();
      }
      foreach (string text2 in new string[] { "›", "»", "→", "->", ">", "|", ";" })
      {
        text = text.Replace(text2, "\u0001");
      }
      if (!text.Contains('\u0001'))
      {
        text = text.Replace(", ", "\u0001");
      }
      IEnumerable<string> enumerable = from p in text.Split('\u0001', StringSplitOptions.None)
        select p.TrimWs() into p
        where p.Length > 0
        select p;
      Func<string, ClickyInkPipeline.Stage?> func;
      if ((func = ClickyInkPipeline.<>O.<0>__StageOf) == null)
      {
        func = (ClickyInkPipeline.<>O.<0>__StageOf = new Func<string, ClickyInkPipeline.Stage?>(ClickyInkPipeline.StageOf));
      }
      return (from s in enumerable.Select<string, ClickyInkPipeline.Stage?>(func)
        where s != null
        select s.Value).Take<ClickyInkPipeline.Stage>(6).ToList<ClickyInkPipeline.Stage>();
    }

    // Token: 0x060013F4 RID: 5108 RVA: 0x003AA884 File Offset: 0x003AA884
    public static ClickyInkPipeline.Stage? StageOf(string piece)
    {
      int num = piece.LastIndexOf(':');
      if (num >= 0)
      {
        string text = piece.Substring(0, num).TrimWs();
        int num2 = num + 1;
        string text2 = piece.Substring(num2, piece.Length - num2).TrimWs();
        if (text.Length > 0)
        {
          return new ClickyInkPipeline.Stage?(new ClickyInkPipeline.Stage(text, text2));
        }
      }
      List<string> list = piece.SplitOmittingEmpty(' ');
      if (list.Count > 1)
      {
        List<string> list2 = list;
        if (ClickyInkNumber.Parse(list2[list2.Count - 1]) != null)
        {
          string text3 = string.Join(" ", list.Take<string>(list.Count - 1));
          List<string> list3 = list;
          return new ClickyInkPipeline.Stage?(new ClickyInkPipeline.Stage(text3, list3[list3.Count - 1]));
        }
      }
      if (list.Count > 1 && ClickyInkNumber.Parse(list[0]) != null)
      {
        return new ClickyInkPipeline.Stage?(new ClickyInkPipeline.Stage(string.Join(" ", list.Skip<string>(1)), list[0]));
      }
      if (piece.Length != 0)
      {
        return new ClickyInkPipeline.Stage?(new ClickyInkPipeline.Stage(piece, ""));
      }
      return null;
    }

    // Token: 0x060013F5 RID: 5109 RVA: 0x003AA9AC File Offset: 0x003AA9AC
    public static string Attribute(IEnumerable<ClickyInkPipeline.Stage> stages)
    {
      return string.Join(" › ", stages.Select<ClickyInkPipeline.Stage, string>(delegate(ClickyInkPipeline.Stage s)
      {
        if (s.Value.Length != 0)
        {
          return s.Label + ": " + s.Value;
        }
        return s.Label;
      }));
    }

    // Token: 0x040008E0 RID: 2272
    public const int MaximumStages = 6;

    // Token: 0x020004BC RID: 1212
    [Nullable(0)]
    public readonly struct Stage : IEquatable<ClickyInkPipeline.Stage>
    {
      // Token: 0x06002FF8 RID: 12280 RVA: 0x00433804 File Offset: 0x00433804
      public Stage(string Label, string Value)
      {
        this.Label = Label;
        this.Value = Value;
      }

      // Token: 0x17000896 RID: 2198
      // (get) Token: 0x06002FF9 RID: 12281 RVA: 0x00433814 File Offset: 0x00433814
      // (set) Token: 0x06002FFA RID: 12282 RVA: 0x0043381C File Offset: 0x0043381C
      public string Label { get; set; }

      // Token: 0x17000897 RID: 2199
      // (get) Token: 0x06002FFB RID: 12283 RVA: 0x00433828 File Offset: 0x00433828
      // (set) Token: 0x06002FFC RID: 12284 RVA: 0x00433830 File Offset: 0x00433830
      public string Value { get; set; }

      // Token: 0x06002FFD RID: 12285 RVA: 0x0043383C File Offset: 0x0043383C
      [NullableContext(0)]
      [CompilerGenerated]
      public override string ToString()
      {
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.Append("Stage");
        stringBuilder.Append(" { ");
        if (this.PrintMembers(stringBuilder))
        {
          stringBuilder.Append(' ');
        }
        stringBuilder.Append('}');
        return stringBuilder.ToString();
      }

      // Token: 0x06002FFE RID: 12286 RVA: 0x00433888 File Offset: 0x00433888
      [NullableContext(0)]
      [CompilerGenerated]
      private bool PrintMembers(StringBuilder builder)
      {
        builder.Append("Label = ");
        builder.Append(this.Label);
        builder.Append(", Value = ");
        builder.Append(this.Value);
        return true;
      }

      // Token: 0x06002FFF RID: 12287 RVA: 0x004338C0 File Offset: 0x004338C0
      [CompilerGenerated]
      public static bool operator !=(ClickyInkPipeline.Stage left, ClickyInkPipeline.Stage right)
      {
        return !(left == right);
      }

      // Token: 0x06003000 RID: 12288 RVA: 0x004338CC File Offset: 0x004338CC
      [CompilerGenerated]
      public static bool operator ==(ClickyInkPipeline.Stage left, ClickyInkPipeline.Stage right)
      {
        return left.Equals(right);
      }

      // Token: 0x06003001 RID: 12289 RVA: 0x004338D8 File Offset: 0x004338D8
      [CompilerGenerated]
      public override int GetHashCode()
      {
        return EqualityComparer<string>.Default.GetHashCode(this.<Label>k__BackingField) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Value>k__BackingField);
      }

      // Token: 0x06003002 RID: 12290 RVA: 0x00433904 File Offset: 0x00433904
      [NullableContext(0)]
      [CompilerGenerated]
      public override bool Equals(object obj)
      {
        return obj is ClickyInkPipeline.Stage && this.Equals((ClickyInkPipeline.Stage)obj);
      }

      // Token: 0x06003003 RID: 12291 RVA: 0x0043391C File Offset: 0x0043391C
      [CompilerGenerated]
      public bool Equals(ClickyInkPipeline.Stage other)
      {
        return EqualityComparer<string>.Default.Equals(this.<Label>k__BackingField, other.<Label>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<Value>k__BackingField, other.<Value>k__BackingField);
      }

      // Token: 0x06003004 RID: 12292 RVA: 0x00433950 File Offset: 0x00433950
      [CompilerGenerated]
      public void Deconstruct(out string Label, out string Value)
      {
        Label = this.Label;
        Value = this.Value;
      }
    }

    // Token: 0x020004BD RID: 1213
    [CompilerGenerated]
    private static class <>O
    {
      // Token: 0x04001DCF RID: 7631
      [Nullable(0)]
      public static Func<string, ClickyInkPipeline.Stage?> <0>__StageOf;
    }
  }
}
