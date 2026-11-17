using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

namespace HeyClicky.Core.Ink
{
  // Token: 0x02000175 RID: 373
  public static class ClickyInkFigureRules
  {
    // Token: 0x06001691 RID: 5777 RVA: 0x003BF6B0 File Offset: 0x003BF6B0
    [NullableContext(2)]
    public static ClickyInkFigureRules.Delta Read(string delta)
    {
      if (string.IsNullOrEmpty(delta))
      {
        return null;
      }
      string lower = delta.ToLowerInvariant();
      Func<char, bool> func;
      if ((func = ClickyInkFigureRules.<>O.<0>__IsDigit) == null)
      {
        func = (ClickyInkFigureRules.<>O.<0>__IsDigit = new Func<char, bool>(char.IsDigit));
      }
      if (!delta.Any<char>(func) && !new string[] { "up", "down", "fell", "rose" }.Any<string>((string w) => lower.Contains(w, StringComparison.Ordinal)))
      {
        return new ClickyInkFigureRules.Delta(delta, true, false);
      }
      bool flag = delta.HasPrefix("-") || delta.HasPrefix("−") || lower.Contains("down", StringComparison.Ordinal);
      return new ClickyInkFigureRules.Delta((flag ? "↓ " : "↑ ") + delta.TrimSet("+-−"), false, flag);
    }

    // Token: 0x02000548 RID: 1352
    [NullableContext(1)]
    [Nullable(0)]
    public sealed class Delta : IEquatable<ClickyInkFigureRules.Delta>
    {
      // Token: 0x060033A6 RID: 13222 RVA: 0x0043DDCC File Offset: 0x0043DDCC
      public Delta(string Text, bool IsCaption, bool IsDown)
      {
        this.Text = Text;
        this.IsCaption = IsCaption;
        this.IsDown = IsDown;
        base..ctor();
      }

      // Token: 0x17000930 RID: 2352
      // (get) Token: 0x060033A7 RID: 13223 RVA: 0x0043DDEC File Offset: 0x0043DDEC
      [CompilerGenerated]
      private Type EqualityContract
      {
        [CompilerGenerated]
        get
        {
          return typeof(ClickyInkFigureRules.Delta);
        }
      }

      // Token: 0x17000931 RID: 2353
      // (get) Token: 0x060033A8 RID: 13224 RVA: 0x0043DDF8 File Offset: 0x0043DDF8
      // (set) Token: 0x060033A9 RID: 13225 RVA: 0x0043DE00 File Offset: 0x0043DE00
      public string Text { get; set; }

      // Token: 0x17000932 RID: 2354
      // (get) Token: 0x060033AA RID: 13226 RVA: 0x0043DE0C File Offset: 0x0043DE0C
      // (set) Token: 0x060033AB RID: 13227 RVA: 0x0043DE14 File Offset: 0x0043DE14
      public bool IsCaption { get; set; }

      // Token: 0x17000933 RID: 2355
      // (get) Token: 0x060033AC RID: 13228 RVA: 0x0043DE20 File Offset: 0x0043DE20
      // (set) Token: 0x060033AD RID: 13229 RVA: 0x0043DE28 File Offset: 0x0043DE28
      public bool IsDown { get; set; }

      // Token: 0x060033AE RID: 13230 RVA: 0x0043DE34 File Offset: 0x0043DE34
      [CompilerGenerated]
      public override string ToString()
      {
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.Append("Delta");
        stringBuilder.Append(" { ");
        if (this.PrintMembers(stringBuilder))
        {
          stringBuilder.Append(' ');
        }
        stringBuilder.Append('}');
        return stringBuilder.ToString();
      }

      // Token: 0x060033AF RID: 13231 RVA: 0x0043DE80 File Offset: 0x0043DE80
      [CompilerGenerated]
      private bool PrintMembers(StringBuilder builder)
      {
        RuntimeHelpers.EnsureSufficientExecutionStack();
        builder.Append("Text = ");
        builder.Append(this.Text);
        builder.Append(", IsCaption = ");
        builder.Append(this.IsCaption.ToString());
        builder.Append(", IsDown = ");
        builder.Append(this.IsDown.ToString());
        return true;
      }

      // Token: 0x060033B0 RID: 13232 RVA: 0x0043DEFC File Offset: 0x0043DEFC
      [NullableContext(2)]
      [CompilerGenerated]
      public static bool operator !=(ClickyInkFigureRules.Delta left, ClickyInkFigureRules.Delta right)
      {
        return !(left == right);
      }

      // Token: 0x060033B1 RID: 13233 RVA: 0x0043DF08 File Offset: 0x0043DF08
      [NullableContext(2)]
      [CompilerGenerated]
      public static bool operator ==(ClickyInkFigureRules.Delta left, ClickyInkFigureRules.Delta right)
      {
        return left == right || (left != null && left.Equals(right));
      }

      // Token: 0x060033B2 RID: 13234 RVA: 0x0043DF1C File Offset: 0x0043DF1C
      [CompilerGenerated]
      public override int GetHashCode()
      {
        return ((EqualityComparer<Type>.Default.GetHashCode(this.EqualityContract) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Text>k__BackingField)) * -1521134295 + EqualityComparer<bool>.Default.GetHashCode(this.<IsCaption>k__BackingField)) * -1521134295 + EqualityComparer<bool>.Default.GetHashCode(this.<IsDown>k__BackingField);
      }

      // Token: 0x060033B3 RID: 13235 RVA: 0x0043DF80 File Offset: 0x0043DF80
      [NullableContext(2)]
      [CompilerGenerated]
      public override bool Equals(object obj)
      {
        return this.Equals(obj as ClickyInkFigureRules.Delta);
      }

      // Token: 0x060033B4 RID: 13236 RVA: 0x0043DF90 File Offset: 0x0043DF90
      [NullableContext(2)]
      [CompilerGenerated]
      public bool Equals(ClickyInkFigureRules.Delta other)
      {
        return this == other || (other != null && this.EqualityContract == other.EqualityContract && EqualityComparer<string>.Default.Equals(this.<Text>k__BackingField, other.<Text>k__BackingField) && EqualityComparer<bool>.Default.Equals(this.<IsCaption>k__BackingField, other.<IsCaption>k__BackingField) && EqualityComparer<bool>.Default.Equals(this.<IsDown>k__BackingField, other.<IsDown>k__BackingField));
      }

      // Token: 0x060033B6 RID: 13238 RVA: 0x0043E00C File Offset: 0x0043E00C
      [CompilerGenerated]
      private Delta(ClickyInkFigureRules.Delta original)
      {
        this.Text = original.<Text>k__BackingField;
        this.IsCaption = original.<IsCaption>k__BackingField;
        this.IsDown = original.<IsDown>k__BackingField;
      }

      // Token: 0x060033B7 RID: 13239 RVA: 0x0043E038 File Offset: 0x0043E038
      [CompilerGenerated]
      public void Deconstruct(out string Text, out bool IsCaption, out bool IsDown)
      {
        Text = this.Text;
        IsCaption = this.IsCaption;
        IsDown = this.IsDown;
      }
    }

    // Token: 0x02000549 RID: 1353
    [CompilerGenerated]
    private static class <>O
    {
      // Token: 0x04002021 RID: 8225
      public static Func<char, bool> <0>__IsDigit;
    }
  }
}
