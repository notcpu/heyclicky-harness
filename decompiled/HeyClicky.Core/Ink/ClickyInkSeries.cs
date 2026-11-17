using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

namespace HeyClicky.Core.Ink
{
  // Token: 0x02000133 RID: 307
  [NullableContext(1)]
  [Nullable(0)]
  public static class ClickyInkSeries
  {
    // Token: 0x060013D3 RID: 5075 RVA: 0x003A87EC File Offset: 0x003A87EC
    public static List<string> Tokens([Nullable(2)] string text)
    {
      string text2 = ((text != null) ? text.TrimWs() : null) ?? "";
      if (text2.Length == 0)
      {
        return new List<string>();
      }
      string text3 = text2.Replace("→", " ").Replace("->", " ").Replace("|", " ")
        .Replace(";", ", ");
      text3 = text3.TrimSet("[]() ");
      List<string> list;
      if (text3.Contains(", ", StringComparison.Ordinal) || (text3.Contains(' ') && !text3.Contains(',')))
      {
        list = text3.Components(", ").SelectMany<string, string>((string p) => p.SplitOmittingEmpty(' ')).ToList<string>();
      }
      else
      {
        list = text3.Components(",").ToList<string>();
      }
      List<string> list2 = (from p in list
        select p.TrimWs() into p
        where ClickyInkNumber.Parse(p) != null
        select p).ToList<string>();
      return list2.Skip<string>(Math.Max(0, list2.Count - 60)).ToList<string>();
    }

    // Token: 0x060013D4 RID: 5076 RVA: 0x003A893C File Offset: 0x003A893C
    public static List<double> Values([Nullable(2)] string text)
    {
      IEnumerable<string> enumerable = ClickyInkSeries.Tokens(text);
      Func<string, ClickyInkNumber?> func;
      if ((func = ClickyInkSeries.<>O.<0>__Parse) == null)
      {
        func = (ClickyInkSeries.<>O.<0>__Parse = new Func<string, ClickyInkNumber?>(ClickyInkNumber.Parse));
      }
      return (from n in enumerable.Select<string, ClickyInkNumber?>(func)
        where n != null
        select n.Value.Value).ToList<double>();
    }

    // Token: 0x060013D5 RID: 5077 RVA: 0x003A89BC File Offset: 0x003A89BC
    public static List<string> Labels([Nullable(2)] string text)
    {
      string text2 = ((text != null) ? text.TrimWs() : null) ?? "";
      if (text2.Length == 0)
      {
        return new List<string>();
      }
      return (from p in text2.Components(",")
        select p.TrimWs() into p
        where p.Length > 0
        select p).ToList<string>();
    }

    // Token: 0x060013D6 RID: 5078 RVA: 0x003A8A48 File Offset: 0x003A8A48
    public static ClickyInkSeries.Change? ChangeBetween(IReadOnlyList<double> old, IReadOnlyList<double> @new)
    {
      if (old.Count == 0 || old.SequenceEqual<double>(@new))
      {
        return null;
      }
      if (@new.Count > old.Count && @new.Take<double>(old.Count).SequenceEqual<double>(old))
      {
        return new ClickyInkSeries.Change?(new ClickyInkSeries.Change(@new.Count - old.Count, 0));
      }
      for (int i = 1; i < Math.Min(old.Count, 8); i++)
      {
        List<double> list = old.Skip<double>(i).ToList<double>();
        if (list.Count > 0 && @new.Count >= list.Count && @new.Take<double>(list.Count).SequenceEqual<double>(list) && @new.Count - list.Count >= 1)
        {
          return new ClickyInkSeries.Change?(new ClickyInkSeries.Change(@new.Count - list.Count, i));
        }
      }
      return null;
    }

    // Token: 0x040008CE RID: 2254
    public const int MaximumPoints = 60;

    // Token: 0x020004B4 RID: 1204
    [NullableContext(0)]
    public readonly struct Change : IEquatable<ClickyInkSeries.Change>
    {
      // Token: 0x06002FC6 RID: 12230 RVA: 0x004332E8 File Offset: 0x004332E8
      public Change(int Added, int Slid)
      {
        this.Added = Added;
        this.Slid = Slid;
      }

      // Token: 0x17000890 RID: 2192
      // (get) Token: 0x06002FC7 RID: 12231 RVA: 0x004332F8 File Offset: 0x004332F8
      // (set) Token: 0x06002FC8 RID: 12232 RVA: 0x00433300 File Offset: 0x00433300
      public int Added { get; set; }

      // Token: 0x17000891 RID: 2193
      // (get) Token: 0x06002FC9 RID: 12233 RVA: 0x0043330C File Offset: 0x0043330C
      // (set) Token: 0x06002FCA RID: 12234 RVA: 0x00433314 File Offset: 0x00433314
      public int Slid { get; set; }

      // Token: 0x06002FCB RID: 12235 RVA: 0x00433320 File Offset: 0x00433320
      [CompilerGenerated]
      public override string ToString()
      {
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.Append("Change");
        stringBuilder.Append(" { ");
        if (this.PrintMembers(stringBuilder))
        {
          stringBuilder.Append(' ');
        }
        stringBuilder.Append('}');
        return stringBuilder.ToString();
      }

      // Token: 0x06002FCC RID: 12236 RVA: 0x0043336C File Offset: 0x0043336C
      [CompilerGenerated]
      private bool PrintMembers(StringBuilder builder)
      {
        builder.Append("Added = ");
        builder.Append(this.Added.ToString());
        builder.Append(", Slid = ");
        builder.Append(this.Slid.ToString());
        return true;
      }

      // Token: 0x06002FCD RID: 12237 RVA: 0x004333C8 File Offset: 0x004333C8
      [CompilerGenerated]
      public static bool operator !=(ClickyInkSeries.Change left, ClickyInkSeries.Change right)
      {
        return !(left == right);
      }

      // Token: 0x06002FCE RID: 12238 RVA: 0x004333D4 File Offset: 0x004333D4
      [CompilerGenerated]
      public static bool operator ==(ClickyInkSeries.Change left, ClickyInkSeries.Change right)
      {
        return left.Equals(right);
      }

      // Token: 0x06002FCF RID: 12239 RVA: 0x004333E0 File Offset: 0x004333E0
      [CompilerGenerated]
      public override int GetHashCode()
      {
        return EqualityComparer<int>.Default.GetHashCode(this.<Added>k__BackingField) * -1521134295 + EqualityComparer<int>.Default.GetHashCode(this.<Slid>k__BackingField);
      }

      // Token: 0x06002FD0 RID: 12240 RVA: 0x0043340C File Offset: 0x0043340C
      [CompilerGenerated]
      public override bool Equals(object obj)
      {
        return obj is ClickyInkSeries.Change && this.Equals((ClickyInkSeries.Change)obj);
      }

      // Token: 0x06002FD1 RID: 12241 RVA: 0x00433424 File Offset: 0x00433424
      [CompilerGenerated]
      public bool Equals(ClickyInkSeries.Change other)
      {
        return EqualityComparer<int>.Default.Equals(this.<Added>k__BackingField, other.<Added>k__BackingField) && EqualityComparer<int>.Default.Equals(this.<Slid>k__BackingField, other.<Slid>k__BackingField);
      }

      // Token: 0x06002FD2 RID: 12242 RVA: 0x00433458 File Offset: 0x00433458
      [CompilerGenerated]
      public void Deconstruct(out int Added, out int Slid)
      {
        Added = this.Added;
        Slid = this.Slid;
      }
    }

    // Token: 0x020004B5 RID: 1205
    [CompilerGenerated]
    private static class <>O
    {
      // Token: 0x04001DB5 RID: 7605
      [Nullable(0)]
      public static Func<string, ClickyInkNumber?> <0>__Parse;
    }
  }
}
