using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace HeyClicky.App.Ink
{
  // Token: 0x020001A7 RID: 423
  [NullableContext(2)]
  [Nullable(0)]
  public sealed class ClickyCardFace : IEquatable<ClickyCardFace>
  {
    // Token: 0x060016CF RID: 5839 RVA: 0x005BCA38 File Offset: 0x005BCA38
    [NullableContext(1)]
    public ClickyCardFace(string Title, ClickyPaperGlyph Glyph, ClickyPaperStock Stock)
    {
      this.Title = Title;
      this.Glyph = Glyph;
      this.Stock = Stock;
      base..ctor();
    }

    // Token: 0x17000407 RID: 1031
    // (get) Token: 0x060016D0 RID: 5840 RVA: 0x005BCA58 File Offset: 0x005BCA58
    [Nullable(1)]
    [CompilerGenerated]
    private Type EqualityContract
    {
      [NullableContext(1)]
      [CompilerGenerated]
      get
      {
        return typeof(ClickyCardFace);
      }
    }

    // Token: 0x17000408 RID: 1032
    // (get) Token: 0x060016D1 RID: 5841 RVA: 0x005BCA64 File Offset: 0x005BCA64
    // (set) Token: 0x060016D2 RID: 5842 RVA: 0x005BCA6C File Offset: 0x005BCA6C
    [Nullable(1)]
    public string Title
    {
      [NullableContext(1)]
      get;
      [NullableContext(1)]
      set;
    }

    // Token: 0x17000409 RID: 1033
    // (get) Token: 0x060016D3 RID: 5843 RVA: 0x005BCA78 File Offset: 0x005BCA78
    // (set) Token: 0x060016D4 RID: 5844 RVA: 0x005BCA80 File Offset: 0x005BCA80
    public ClickyPaperGlyph Glyph { get; set; }

    // Token: 0x1700040A RID: 1034
    // (get) Token: 0x060016D5 RID: 5845 RVA: 0x005BCA8C File Offset: 0x005BCA8C
    // (set) Token: 0x060016D6 RID: 5846 RVA: 0x005BCA94 File Offset: 0x005BCA94
    public ClickyPaperStock Stock { get; set; }

    // Token: 0x1700040B RID: 1035
    // (get) Token: 0x060016D7 RID: 5847 RVA: 0x005BCAA0 File Offset: 0x005BCAA0
    // (set) Token: 0x060016D8 RID: 5848 RVA: 0x005BCAA8 File Offset: 0x005BCAA8
    public string Caps { get; set; }

    // Token: 0x1700040C RID: 1036
    // (get) Token: 0x060016D9 RID: 5849 RVA: 0x005BCAB4 File Offset: 0x005BCAB4
    // (set) Token: 0x060016DA RID: 5850 RVA: 0x005BCABC File Offset: 0x005BCABC
    public ClickyCardFace.CardHeadline Headline { get; set; }

    // Token: 0x1700040D RID: 1037
    // (get) Token: 0x060016DB RID: 5851 RVA: 0x005BCAC8 File Offset: 0x005BCAC8
    // (set) Token: 0x060016DC RID: 5852 RVA: 0x005BCAD0 File Offset: 0x005BCAD0
    public ClickyCardFace.CardDetail Detail { get; set; }

    // Token: 0x1700040E RID: 1038
    // (get) Token: 0x060016DD RID: 5853 RVA: 0x005BCADC File Offset: 0x005BCADC
    // (set) Token: 0x060016DE RID: 5854 RVA: 0x005BCAE4 File Offset: 0x005BCAE4
    public string Foot { get; set; }

    // Token: 0x1700040F RID: 1039
    // (get) Token: 0x060016DF RID: 5855 RVA: 0x005BCAF0 File Offset: 0x005BCAF0
    // (set) Token: 0x060016E0 RID: 5856 RVA: 0x005BCAF8 File Offset: 0x005BCAF8
    public ClickyCardFace.CardFootTone FootTone { get; set; }

    // Token: 0x17000410 RID: 1040
    // (get) Token: 0x060016E1 RID: 5857 RVA: 0x005BCB04 File Offset: 0x005BCB04
    // (set) Token: 0x060016E2 RID: 5858 RVA: 0x005BCB0C File Offset: 0x005BCB0C
    public ClickyPaperStickerKind? Sticker { get; set; }

    // Token: 0x060016E3 RID: 5859 RVA: 0x005BCB18 File Offset: 0x005BCB18
    [NullableContext(1)]
    [CompilerGenerated]
    public override string ToString()
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("ClickyCardFace");
      stringBuilder.Append(" { ");
      if (this.PrintMembers(stringBuilder))
      {
        stringBuilder.Append(' ');
      }
      stringBuilder.Append('}');
      return stringBuilder.ToString();
    }

    // Token: 0x060016E4 RID: 5860 RVA: 0x005BCB64 File Offset: 0x005BCB64
    [NullableContext(1)]
    [CompilerGenerated]
    private bool PrintMembers(StringBuilder builder)
    {
      RuntimeHelpers.EnsureSufficientExecutionStack();
      builder.Append("Title = ");
      builder.Append(this.Title);
      builder.Append(", Glyph = ");
      builder.Append(this.Glyph.ToString());
      builder.Append(", Stock = ");
      builder.Append(this.Stock.ToString());
      builder.Append(", Caps = ");
      builder.Append(this.Caps);
      builder.Append(", Headline = ");
      builder.Append(this.Headline);
      builder.Append(", Detail = ");
      builder.Append(this.Detail);
      builder.Append(", Foot = ");
      builder.Append(this.Foot);
      builder.Append(", FootTone = ");
      builder.Append(this.FootTone.ToString());
      builder.Append(", Sticker = ");
      builder.Append(this.Sticker.ToString());
      return true;
    }

    // Token: 0x060016E5 RID: 5861 RVA: 0x005BCC90 File Offset: 0x005BCC90
    [CompilerGenerated]
    public static bool operator !=(ClickyCardFace left, ClickyCardFace right)
    {
      return !(left == right);
    }

    // Token: 0x060016E6 RID: 5862 RVA: 0x005BCC9C File Offset: 0x005BCC9C
    [CompilerGenerated]
    public static bool operator ==(ClickyCardFace left, ClickyCardFace right)
    {
      return left == right || (left != null && left.Equals(right));
    }

    // Token: 0x060016E7 RID: 5863 RVA: 0x005BCCB0 File Offset: 0x005BCCB0
    [CompilerGenerated]
    public override int GetHashCode()
    {
      return ((((((((EqualityComparer<Type>.Default.GetHashCode(this.EqualityContract) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Title>k__BackingField)) * -1521134295 + EqualityComparer<ClickyPaperGlyph>.Default.GetHashCode(this.<Glyph>k__BackingField)) * -1521134295 + EqualityComparer<ClickyPaperStock>.Default.GetHashCode(this.<Stock>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Caps>k__BackingField)) * -1521134295 + EqualityComparer<ClickyCardFace.CardHeadline>.Default.GetHashCode(this.<Headline>k__BackingField)) * -1521134295 + EqualityComparer<ClickyCardFace.CardDetail>.Default.GetHashCode(this.<Detail>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Foot>k__BackingField)) * -1521134295 + EqualityComparer<ClickyCardFace.CardFootTone>.Default.GetHashCode(this.<FootTone>k__BackingField)) * -1521134295 + EqualityComparer<ClickyPaperStickerKind?>.Default.GetHashCode(this.<Sticker>k__BackingField);
    }

    // Token: 0x060016E8 RID: 5864 RVA: 0x005BCD9C File Offset: 0x005BCD9C
    [CompilerGenerated]
    public override bool Equals(object obj)
    {
      return this.Equals(obj as ClickyCardFace);
    }

    // Token: 0x060016E9 RID: 5865 RVA: 0x005BCDAC File Offset: 0x005BCDAC
    [CompilerGenerated]
    public bool Equals(ClickyCardFace other)
    {
      return this == other || (other != null && this.EqualityContract == other.EqualityContract && EqualityComparer<string>.Default.Equals(this.<Title>k__BackingField, other.<Title>k__BackingField) && EqualityComparer<ClickyPaperGlyph>.Default.Equals(this.<Glyph>k__BackingField, other.<Glyph>k__BackingField) && EqualityComparer<ClickyPaperStock>.Default.Equals(this.<Stock>k__BackingField, other.<Stock>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<Caps>k__BackingField, other.<Caps>k__BackingField) && EqualityComparer<ClickyCardFace.CardHeadline>.Default.Equals(this.<Headline>k__BackingField, other.<Headline>k__BackingField) && EqualityComparer<ClickyCardFace.CardDetail>.Default.Equals(this.<Detail>k__BackingField, other.<Detail>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<Foot>k__BackingField, other.<Foot>k__BackingField) && EqualityComparer<ClickyCardFace.CardFootTone>.Default.Equals(this.<FootTone>k__BackingField, other.<FootTone>k__BackingField) && EqualityComparer<ClickyPaperStickerKind?>.Default.Equals(this.<Sticker>k__BackingField, other.<Sticker>k__BackingField));
    }

    // Token: 0x060016EB RID: 5867 RVA: 0x005BCEC8 File Offset: 0x005BCEC8
    [CompilerGenerated]
    private ClickyCardFace([Nullable(1)] ClickyCardFace original)
    {
      this.Title = original.<Title>k__BackingField;
      this.Glyph = original.<Glyph>k__BackingField;
      this.Stock = original.<Stock>k__BackingField;
      this.Caps = original.<Caps>k__BackingField;
      this.Headline = original.<Headline>k__BackingField;
      this.Detail = original.<Detail>k__BackingField;
      this.Foot = original.<Foot>k__BackingField;
      this.FootTone = original.<FootTone>k__BackingField;
      this.Sticker = original.<Sticker>k__BackingField;
    }

    // Token: 0x060016EC RID: 5868 RVA: 0x005BCF48 File Offset: 0x005BCF48
    [NullableContext(1)]
    [CompilerGenerated]
    public void Deconstruct(out string Title, out ClickyPaperGlyph Glyph, out ClickyPaperStock Stock)
    {
      Title = this.Title;
      Glyph = this.Glyph;
      Stock = this.Stock;
    }

    // Token: 0x02000766 RID: 1894
    [NullableContext(1)]
    [Nullable(0)]
    public abstract class CardHeadline : IEquatable<ClickyCardFace.CardHeadline>
    {
      // Token: 0x170007B1 RID: 1969
      // (get) Token: 0x06003525 RID: 13605 RVA: 0x0067B9EC File Offset: 0x0067B9EC
      [CompilerGenerated]
      protected virtual Type EqualityContract
      {
        [CompilerGenerated]
        get
        {
          return typeof(ClickyCardFace.CardHeadline);
        }
      }

      // Token: 0x06003526 RID: 13606 RVA: 0x0067B9F8 File Offset: 0x0067B9F8
      [CompilerGenerated]
      public override string ToString()
      {
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.Append("CardHeadline");
        stringBuilder.Append(" { ");
        if (this.PrintMembers(stringBuilder))
        {
          stringBuilder.Append(' ');
        }
        stringBuilder.Append('}');
        return stringBuilder.ToString();
      }

      // Token: 0x06003527 RID: 13607 RVA: 0x0067BA44 File Offset: 0x0067BA44
      [CompilerGenerated]
      protected virtual bool PrintMembers(StringBuilder builder)
      {
        return false;
      }

      // Token: 0x06003528 RID: 13608 RVA: 0x0067BA48 File Offset: 0x0067BA48
      [NullableContext(2)]
      [CompilerGenerated]
      public static bool operator !=(ClickyCardFace.CardHeadline left, ClickyCardFace.CardHeadline right)
      {
        return !(left == right);
      }

      // Token: 0x06003529 RID: 13609 RVA: 0x0067BA54 File Offset: 0x0067BA54
      [NullableContext(2)]
      [CompilerGenerated]
      public static bool operator ==(ClickyCardFace.CardHeadline left, ClickyCardFace.CardHeadline right)
      {
        return left == right || (left != null && left.Equals(right));
      }

      // Token: 0x0600352A RID: 13610 RVA: 0x0067BA68 File Offset: 0x0067BA68
      [CompilerGenerated]
      public override int GetHashCode()
      {
        return EqualityComparer<Type>.Default.GetHashCode(this.EqualityContract);
      }

      // Token: 0x0600352B RID: 13611 RVA: 0x0067BA7C File Offset: 0x0067BA7C
      [NullableContext(2)]
      [CompilerGenerated]
      public override bool Equals(object obj)
      {
        return this.Equals(obj as ClickyCardFace.CardHeadline);
      }

      // Token: 0x0600352C RID: 13612 RVA: 0x0067BA8C File Offset: 0x0067BA8C
      [NullableContext(2)]
      [CompilerGenerated]
      public virtual bool Equals(ClickyCardFace.CardHeadline other)
      {
        return this == other || (other != null && this.EqualityContract == other.EqualityContract);
      }

      // Token: 0x0600352E RID: 13614 RVA: 0x0067BAAC File Offset: 0x0067BAAC
      [CompilerGenerated]
      protected CardHeadline(ClickyCardFace.CardHeadline original)
      {
      }

      // Token: 0x0600352F RID: 13615 RVA: 0x0067BAB4 File Offset: 0x0067BAB4
      protected CardHeadline()
      {
      }

      // Token: 0x02000AEA RID: 2794
      [Nullable(0)]
      public sealed class Big : ClickyCardFace.CardHeadline, IEquatable<ClickyCardFace.CardHeadline.Big>
      {
        // Token: 0x06004281 RID: 17025 RVA: 0x006CE9FC File Offset: 0x006CE9FC
        public Big(string Text)
        {
          this.Text = Text;
          base..ctor();
        }

        // Token: 0x17000881 RID: 2177
        // (get) Token: 0x06004282 RID: 17026 RVA: 0x006CEA0C File Offset: 0x006CEA0C
        [CompilerGenerated]
        protected override Type EqualityContract
        {
          [CompilerGenerated]
          get
          {
            return typeof(ClickyCardFace.CardHeadline.Big);
          }
        }

        // Token: 0x17000882 RID: 2178
        // (get) Token: 0x06004283 RID: 17027 RVA: 0x006CEA18 File Offset: 0x006CEA18
        // (set) Token: 0x06004284 RID: 17028 RVA: 0x006CEA20 File Offset: 0x006CEA20
        public string Text { get; set; }

        // Token: 0x06004285 RID: 17029 RVA: 0x006CEA2C File Offset: 0x006CEA2C
        [CompilerGenerated]
        public override string ToString()
        {
          StringBuilder stringBuilder = new StringBuilder();
          stringBuilder.Append("Big");
          stringBuilder.Append(" { ");
          if (this.PrintMembers(stringBuilder))
          {
            stringBuilder.Append(' ');
          }
          stringBuilder.Append('}');
          return stringBuilder.ToString();
        }

        // Token: 0x06004286 RID: 17030 RVA: 0x006CEA78 File Offset: 0x006CEA78
        [CompilerGenerated]
        protected override bool PrintMembers(StringBuilder builder)
        {
          if (base.PrintMembers(builder))
          {
            builder.Append(", ");
          }
          builder.Append("Text = ");
          builder.Append(this.Text);
          return true;
        }

        // Token: 0x06004287 RID: 17031 RVA: 0x006CEAAC File Offset: 0x006CEAAC
        [NullableContext(2)]
        [CompilerGenerated]
        public static bool operator !=(ClickyCardFace.CardHeadline.Big left, ClickyCardFace.CardHeadline.Big right)
        {
          return !(left == right);
        }

        // Token: 0x06004288 RID: 17032 RVA: 0x006CEAB8 File Offset: 0x006CEAB8
        [NullableContext(2)]
        [CompilerGenerated]
        public static bool operator ==(ClickyCardFace.CardHeadline.Big left, ClickyCardFace.CardHeadline.Big right)
        {
          return left == right || (left != null && left.Equals(right));
        }

        // Token: 0x06004289 RID: 17033 RVA: 0x006CEACC File Offset: 0x006CEACC
        [CompilerGenerated]
        public override int GetHashCode()
        {
          return base.GetHashCode() * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Text>k__BackingField);
        }

        // Token: 0x0600428A RID: 17034 RVA: 0x006CEAEC File Offset: 0x006CEAEC
        [NullableContext(2)]
        [CompilerGenerated]
        public override bool Equals(object obj)
        {
          return this.Equals(obj as ClickyCardFace.CardHeadline.Big);
        }

        // Token: 0x0600428B RID: 17035 RVA: 0x006CEAFC File Offset: 0x006CEAFC
        [NullableContext(2)]
        [CompilerGenerated]
        public sealed override bool Equals(ClickyCardFace.CardHeadline other)
        {
          return this.Equals(other);
        }

        // Token: 0x0600428C RID: 17036 RVA: 0x006CEB08 File Offset: 0x006CEB08
        [NullableContext(2)]
        [CompilerGenerated]
        public bool Equals(ClickyCardFace.CardHeadline.Big other)
        {
          return this == other || (base.Equals(other) && EqualityComparer<string>.Default.Equals(this.<Text>k__BackingField, other.<Text>k__BackingField));
        }

        // Token: 0x0600428E RID: 17038 RVA: 0x006CEB3C File Offset: 0x006CEB3C
        [CompilerGenerated]
        private Big(ClickyCardFace.CardHeadline.Big original)
          : base(original)
        {
          this.Text = original.<Text>k__BackingField;
        }

        // Token: 0x0600428F RID: 17039 RVA: 0x006CEB54 File Offset: 0x006CEB54
        [CompilerGenerated]
        public void Deconstruct(out string Text)
        {
          Text = this.Text;
        }
      }

      // Token: 0x02000AEB RID: 2795
      [Nullable(0)]
      public sealed class Count : ClickyCardFace.CardHeadline, IEquatable<ClickyCardFace.CardHeadline.Count>
      {
        // Token: 0x06004290 RID: 17040 RVA: 0x006CEB60 File Offset: 0x006CEB60
        public Count(string Number, string Word)
        {
          this.Number = Number;
          this.Word = Word;
          base..ctor();
        }

        // Token: 0x17000883 RID: 2179
        // (get) Token: 0x06004291 RID: 17041 RVA: 0x006CEB78 File Offset: 0x006CEB78
        [CompilerGenerated]
        protected override Type EqualityContract
        {
          [CompilerGenerated]
          get
          {
            return typeof(ClickyCardFace.CardHeadline.Count);
          }
        }

        // Token: 0x17000884 RID: 2180
        // (get) Token: 0x06004292 RID: 17042 RVA: 0x006CEB84 File Offset: 0x006CEB84
        // (set) Token: 0x06004293 RID: 17043 RVA: 0x006CEB8C File Offset: 0x006CEB8C
        public string Number { get; set; }

        // Token: 0x17000885 RID: 2181
        // (get) Token: 0x06004294 RID: 17044 RVA: 0x006CEB98 File Offset: 0x006CEB98
        // (set) Token: 0x06004295 RID: 17045 RVA: 0x006CEBA0 File Offset: 0x006CEBA0
        public string Word { get; set; }

        // Token: 0x06004296 RID: 17046 RVA: 0x006CEBAC File Offset: 0x006CEBAC
        [CompilerGenerated]
        public override string ToString()
        {
          StringBuilder stringBuilder = new StringBuilder();
          stringBuilder.Append("Count");
          stringBuilder.Append(" { ");
          if (this.PrintMembers(stringBuilder))
          {
            stringBuilder.Append(' ');
          }
          stringBuilder.Append('}');
          return stringBuilder.ToString();
        }

        // Token: 0x06004297 RID: 17047 RVA: 0x006CEBF8 File Offset: 0x006CEBF8
        [CompilerGenerated]
        protected override bool PrintMembers(StringBuilder builder)
        {
          if (base.PrintMembers(builder))
          {
            builder.Append(", ");
          }
          builder.Append("Number = ");
          builder.Append(this.Number);
          builder.Append(", Word = ");
          builder.Append(this.Word);
          return true;
        }

        // Token: 0x06004298 RID: 17048 RVA: 0x006CEC50 File Offset: 0x006CEC50
        [NullableContext(2)]
        [CompilerGenerated]
        public static bool operator !=(ClickyCardFace.CardHeadline.Count left, ClickyCardFace.CardHeadline.Count right)
        {
          return !(left == right);
        }

        // Token: 0x06004299 RID: 17049 RVA: 0x006CEC5C File Offset: 0x006CEC5C
        [NullableContext(2)]
        [CompilerGenerated]
        public static bool operator ==(ClickyCardFace.CardHeadline.Count left, ClickyCardFace.CardHeadline.Count right)
        {
          return left == right || (left != null && left.Equals(right));
        }

        // Token: 0x0600429A RID: 17050 RVA: 0x006CEC70 File Offset: 0x006CEC70
        [CompilerGenerated]
        public override int GetHashCode()
        {
          return (base.GetHashCode() * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Number>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Word>k__BackingField);
        }

        // Token: 0x0600429B RID: 17051 RVA: 0x006CECA8 File Offset: 0x006CECA8
        [NullableContext(2)]
        [CompilerGenerated]
        public override bool Equals(object obj)
        {
          return this.Equals(obj as ClickyCardFace.CardHeadline.Count);
        }

        // Token: 0x0600429C RID: 17052 RVA: 0x006CECB8 File Offset: 0x006CECB8
        [NullableContext(2)]
        [CompilerGenerated]
        public sealed override bool Equals(ClickyCardFace.CardHeadline other)
        {
          return this.Equals(other);
        }

        // Token: 0x0600429D RID: 17053 RVA: 0x006CECC4 File Offset: 0x006CECC4
        [NullableContext(2)]
        [CompilerGenerated]
        public bool Equals(ClickyCardFace.CardHeadline.Count other)
        {
          return this == other || (base.Equals(other) && EqualityComparer<string>.Default.Equals(this.<Number>k__BackingField, other.<Number>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<Word>k__BackingField, other.<Word>k__BackingField));
        }

        // Token: 0x0600429F RID: 17055 RVA: 0x006CED18 File Offset: 0x006CED18
        [CompilerGenerated]
        private Count(ClickyCardFace.CardHeadline.Count original)
          : base(original)
        {
          this.Number = original.<Number>k__BackingField;
          this.Word = original.<Word>k__BackingField;
        }

        // Token: 0x060042A0 RID: 17056 RVA: 0x006CED3C File Offset: 0x006CED3C
        [CompilerGenerated]
        public void Deconstruct(out string Number, out string Word)
        {
          Number = this.Number;
          Word = this.Word;
        }
      }

      // Token: 0x02000AEC RID: 2796
      [Nullable(0)]
      public sealed class Sentence : ClickyCardFace.CardHeadline, IEquatable<ClickyCardFace.CardHeadline.Sentence>
      {
        // Token: 0x060042A1 RID: 17057 RVA: 0x006CED50 File Offset: 0x006CED50
        public Sentence(string Text)
        {
          this.Text = Text;
          base..ctor();
        }

        // Token: 0x17000886 RID: 2182
        // (get) Token: 0x060042A2 RID: 17058 RVA: 0x006CED60 File Offset: 0x006CED60
        [CompilerGenerated]
        protected override Type EqualityContract
        {
          [CompilerGenerated]
          get
          {
            return typeof(ClickyCardFace.CardHeadline.Sentence);
          }
        }

        // Token: 0x17000887 RID: 2183
        // (get) Token: 0x060042A3 RID: 17059 RVA: 0x006CED6C File Offset: 0x006CED6C
        // (set) Token: 0x060042A4 RID: 17060 RVA: 0x006CED74 File Offset: 0x006CED74
        public string Text { get; set; }

        // Token: 0x060042A5 RID: 17061 RVA: 0x006CED80 File Offset: 0x006CED80
        [CompilerGenerated]
        public override string ToString()
        {
          StringBuilder stringBuilder = new StringBuilder();
          stringBuilder.Append("Sentence");
          stringBuilder.Append(" { ");
          if (this.PrintMembers(stringBuilder))
          {
            stringBuilder.Append(' ');
          }
          stringBuilder.Append('}');
          return stringBuilder.ToString();
        }

        // Token: 0x060042A6 RID: 17062 RVA: 0x006CEDCC File Offset: 0x006CEDCC
        [CompilerGenerated]
        protected override bool PrintMembers(StringBuilder builder)
        {
          if (base.PrintMembers(builder))
          {
            builder.Append(", ");
          }
          builder.Append("Text = ");
          builder.Append(this.Text);
          return true;
        }

        // Token: 0x060042A7 RID: 17063 RVA: 0x006CEE00 File Offset: 0x006CEE00
        [NullableContext(2)]
        [CompilerGenerated]
        public static bool operator !=(ClickyCardFace.CardHeadline.Sentence left, ClickyCardFace.CardHeadline.Sentence right)
        {
          return !(left == right);
        }

        // Token: 0x060042A8 RID: 17064 RVA: 0x006CEE0C File Offset: 0x006CEE0C
        [NullableContext(2)]
        [CompilerGenerated]
        public static bool operator ==(ClickyCardFace.CardHeadline.Sentence left, ClickyCardFace.CardHeadline.Sentence right)
        {
          return left == right || (left != null && left.Equals(right));
        }

        // Token: 0x060042A9 RID: 17065 RVA: 0x006CEE20 File Offset: 0x006CEE20
        [CompilerGenerated]
        public override int GetHashCode()
        {
          return base.GetHashCode() * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Text>k__BackingField);
        }

        // Token: 0x060042AA RID: 17066 RVA: 0x006CEE40 File Offset: 0x006CEE40
        [NullableContext(2)]
        [CompilerGenerated]
        public override bool Equals(object obj)
        {
          return this.Equals(obj as ClickyCardFace.CardHeadline.Sentence);
        }

        // Token: 0x060042AB RID: 17067 RVA: 0x006CEE50 File Offset: 0x006CEE50
        [NullableContext(2)]
        [CompilerGenerated]
        public sealed override bool Equals(ClickyCardFace.CardHeadline other)
        {
          return this.Equals(other);
        }

        // Token: 0x060042AC RID: 17068 RVA: 0x006CEE5C File Offset: 0x006CEE5C
        [NullableContext(2)]
        [CompilerGenerated]
        public bool Equals(ClickyCardFace.CardHeadline.Sentence other)
        {
          return this == other || (base.Equals(other) && EqualityComparer<string>.Default.Equals(this.<Text>k__BackingField, other.<Text>k__BackingField));
        }

        // Token: 0x060042AE RID: 17070 RVA: 0x006CEE90 File Offset: 0x006CEE90
        [CompilerGenerated]
        private Sentence(ClickyCardFace.CardHeadline.Sentence original)
          : base(original)
        {
          this.Text = original.<Text>k__BackingField;
        }

        // Token: 0x060042AF RID: 17071 RVA: 0x006CEEA8 File Offset: 0x006CEEA8
        [CompilerGenerated]
        public void Deconstruct(out string Text)
        {
          Text = this.Text;
        }
      }

      // Token: 0x02000AED RID: 2797
      [Nullable(0)]
      public sealed class Message : ClickyCardFace.CardHeadline, IEquatable<ClickyCardFace.CardHeadline.Message>
      {
        // Token: 0x060042B0 RID: 17072 RVA: 0x006CEEB4 File Offset: 0x006CEEB4
        public Message(string From, string Text, string Draft)
        {
          this.From = From;
          this.Text = Text;
          this.Draft = Draft;
          base..ctor();
        }

        // Token: 0x17000888 RID: 2184
        // (get) Token: 0x060042B1 RID: 17073 RVA: 0x006CEED4 File Offset: 0x006CEED4
        [CompilerGenerated]
        protected override Type EqualityContract
        {
          [CompilerGenerated]
          get
          {
            return typeof(ClickyCardFace.CardHeadline.Message);
          }
        }

        // Token: 0x17000889 RID: 2185
        // (get) Token: 0x060042B2 RID: 17074 RVA: 0x006CEEE0 File Offset: 0x006CEEE0
        // (set) Token: 0x060042B3 RID: 17075 RVA: 0x006CEEE8 File Offset: 0x006CEEE8
        public string From { get; set; }

        // Token: 0x1700088A RID: 2186
        // (get) Token: 0x060042B4 RID: 17076 RVA: 0x006CEEF4 File Offset: 0x006CEEF4
        // (set) Token: 0x060042B5 RID: 17077 RVA: 0x006CEEFC File Offset: 0x006CEEFC
        public string Text { get; set; }

        // Token: 0x1700088B RID: 2187
        // (get) Token: 0x060042B6 RID: 17078 RVA: 0x006CEF08 File Offset: 0x006CEF08
        // (set) Token: 0x060042B7 RID: 17079 RVA: 0x006CEF10 File Offset: 0x006CEF10
        public string Draft { get; set; }

        // Token: 0x060042B8 RID: 17080 RVA: 0x006CEF1C File Offset: 0x006CEF1C
        [CompilerGenerated]
        public override string ToString()
        {
          StringBuilder stringBuilder = new StringBuilder();
          stringBuilder.Append("Message");
          stringBuilder.Append(" { ");
          if (this.PrintMembers(stringBuilder))
          {
            stringBuilder.Append(' ');
          }
          stringBuilder.Append('}');
          return stringBuilder.ToString();
        }

        // Token: 0x060042B9 RID: 17081 RVA: 0x006CEF68 File Offset: 0x006CEF68
        [CompilerGenerated]
        protected override bool PrintMembers(StringBuilder builder)
        {
          if (base.PrintMembers(builder))
          {
            builder.Append(", ");
          }
          builder.Append("From = ");
          builder.Append(this.From);
          builder.Append(", Text = ");
          builder.Append(this.Text);
          builder.Append(", Draft = ");
          builder.Append(this.Draft);
          return true;
        }

        // Token: 0x060042BA RID: 17082 RVA: 0x006CEFD8 File Offset: 0x006CEFD8
        [NullableContext(2)]
        [CompilerGenerated]
        public static bool operator !=(ClickyCardFace.CardHeadline.Message left, ClickyCardFace.CardHeadline.Message right)
        {
          return !(left == right);
        }

        // Token: 0x060042BB RID: 17083 RVA: 0x006CEFE4 File Offset: 0x006CEFE4
        [NullableContext(2)]
        [CompilerGenerated]
        public static bool operator ==(ClickyCardFace.CardHeadline.Message left, ClickyCardFace.CardHeadline.Message right)
        {
          return left == right || (left != null && left.Equals(right));
        }

        // Token: 0x060042BC RID: 17084 RVA: 0x006CEFF8 File Offset: 0x006CEFF8
        [CompilerGenerated]
        public override int GetHashCode()
        {
          return ((base.GetHashCode() * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<From>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Text>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Draft>k__BackingField);
        }

        // Token: 0x060042BD RID: 17085 RVA: 0x006CF050 File Offset: 0x006CF050
        [NullableContext(2)]
        [CompilerGenerated]
        public override bool Equals(object obj)
        {
          return this.Equals(obj as ClickyCardFace.CardHeadline.Message);
        }

        // Token: 0x060042BE RID: 17086 RVA: 0x006CF060 File Offset: 0x006CF060
        [NullableContext(2)]
        [CompilerGenerated]
        public sealed override bool Equals(ClickyCardFace.CardHeadline other)
        {
          return this.Equals(other);
        }

        // Token: 0x060042BF RID: 17087 RVA: 0x006CF06C File Offset: 0x006CF06C
        [NullableContext(2)]
        [CompilerGenerated]
        public bool Equals(ClickyCardFace.CardHeadline.Message other)
        {
          return this == other || (base.Equals(other) && EqualityComparer<string>.Default.Equals(this.<From>k__BackingField, other.<From>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<Text>k__BackingField, other.<Text>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<Draft>k__BackingField, other.<Draft>k__BackingField));
        }

        // Token: 0x060042C1 RID: 17089 RVA: 0x006CF0D8 File Offset: 0x006CF0D8
        [CompilerGenerated]
        private Message(ClickyCardFace.CardHeadline.Message original)
          : base(original)
        {
          this.From = original.<From>k__BackingField;
          this.Text = original.<Text>k__BackingField;
          this.Draft = original.<Draft>k__BackingField;
        }

        // Token: 0x060042C2 RID: 17090 RVA: 0x006CF108 File Offset: 0x006CF108
        [CompilerGenerated]
        public void Deconstruct(out string From, out string Text, out string Draft)
        {
          From = this.From;
          Text = this.Text;
          Draft = this.Draft;
        }
      }
    }

    // Token: 0x02000767 RID: 1895
    [NullableContext(1)]
    [Nullable(0)]
    public abstract class CardDetail : IEquatable<ClickyCardFace.CardDetail>
    {
      // Token: 0x170007B2 RID: 1970
      // (get) Token: 0x06003530 RID: 13616 RVA: 0x0067BABC File Offset: 0x0067BABC
      [CompilerGenerated]
      protected virtual Type EqualityContract
      {
        [CompilerGenerated]
        get
        {
          return typeof(ClickyCardFace.CardDetail);
        }
      }

      // Token: 0x06003531 RID: 13617 RVA: 0x0067BAC8 File Offset: 0x0067BAC8
      [CompilerGenerated]
      public override string ToString()
      {
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.Append("CardDetail");
        stringBuilder.Append(" { ");
        if (this.PrintMembers(stringBuilder))
        {
          stringBuilder.Append(' ');
        }
        stringBuilder.Append('}');
        return stringBuilder.ToString();
      }

      // Token: 0x06003532 RID: 13618 RVA: 0x0067BB14 File Offset: 0x0067BB14
      [CompilerGenerated]
      protected virtual bool PrintMembers(StringBuilder builder)
      {
        return false;
      }

      // Token: 0x06003533 RID: 13619 RVA: 0x0067BB18 File Offset: 0x0067BB18
      [NullableContext(2)]
      [CompilerGenerated]
      public static bool operator !=(ClickyCardFace.CardDetail left, ClickyCardFace.CardDetail right)
      {
        return !(left == right);
      }

      // Token: 0x06003534 RID: 13620 RVA: 0x0067BB24 File Offset: 0x0067BB24
      [NullableContext(2)]
      [CompilerGenerated]
      public static bool operator ==(ClickyCardFace.CardDetail left, ClickyCardFace.CardDetail right)
      {
        return left == right || (left != null && left.Equals(right));
      }

      // Token: 0x06003535 RID: 13621 RVA: 0x0067BB38 File Offset: 0x0067BB38
      [CompilerGenerated]
      public override int GetHashCode()
      {
        return EqualityComparer<Type>.Default.GetHashCode(this.EqualityContract);
      }

      // Token: 0x06003536 RID: 13622 RVA: 0x0067BB4C File Offset: 0x0067BB4C
      [NullableContext(2)]
      [CompilerGenerated]
      public override bool Equals(object obj)
      {
        return this.Equals(obj as ClickyCardFace.CardDetail);
      }

      // Token: 0x06003537 RID: 13623 RVA: 0x0067BB5C File Offset: 0x0067BB5C
      [NullableContext(2)]
      [CompilerGenerated]
      public virtual bool Equals(ClickyCardFace.CardDetail other)
      {
        return this == other || (other != null && this.EqualityContract == other.EqualityContract);
      }

      // Token: 0x06003539 RID: 13625 RVA: 0x0067BB7C File Offset: 0x0067BB7C
      [CompilerGenerated]
      protected CardDetail(ClickyCardFace.CardDetail original)
      {
      }

      // Token: 0x0600353A RID: 13626 RVA: 0x0067BB84 File Offset: 0x0067BB84
      protected CardDetail()
      {
      }

      // Token: 0x02000AEE RID: 2798
      [Nullable(0)]
      public sealed class Sparkline : ClickyCardFace.CardDetail, IEquatable<ClickyCardFace.CardDetail.Sparkline>
      {
        // Token: 0x060042C3 RID: 17091 RVA: 0x006CF124 File Offset: 0x006CF124
        public Sparkline(double[] Values, bool CircledEnd)
        {
          this.Values = Values;
          this.CircledEnd = CircledEnd;
          base..ctor();
        }

        // Token: 0x1700088C RID: 2188
        // (get) Token: 0x060042C4 RID: 17092 RVA: 0x006CF13C File Offset: 0x006CF13C
        [CompilerGenerated]
        protected override Type EqualityContract
        {
          [CompilerGenerated]
          get
          {
            return typeof(ClickyCardFace.CardDetail.Sparkline);
          }
        }

        // Token: 0x1700088D RID: 2189
        // (get) Token: 0x060042C5 RID: 17093 RVA: 0x006CF148 File Offset: 0x006CF148
        // (set) Token: 0x060042C6 RID: 17094 RVA: 0x006CF150 File Offset: 0x006CF150
        public double[] Values { get; set; }

        // Token: 0x1700088E RID: 2190
        // (get) Token: 0x060042C7 RID: 17095 RVA: 0x006CF15C File Offset: 0x006CF15C
        // (set) Token: 0x060042C8 RID: 17096 RVA: 0x006CF164 File Offset: 0x006CF164
        public bool CircledEnd { get; set; }

        // Token: 0x060042C9 RID: 17097 RVA: 0x006CF170 File Offset: 0x006CF170
        [CompilerGenerated]
        public override string ToString()
        {
          StringBuilder stringBuilder = new StringBuilder();
          stringBuilder.Append("Sparkline");
          stringBuilder.Append(" { ");
          if (this.PrintMembers(stringBuilder))
          {
            stringBuilder.Append(' ');
          }
          stringBuilder.Append('}');
          return stringBuilder.ToString();
        }

        // Token: 0x060042CA RID: 17098 RVA: 0x006CF1BC File Offset: 0x006CF1BC
        [CompilerGenerated]
        protected override bool PrintMembers(StringBuilder builder)
        {
          if (base.PrintMembers(builder))
          {
            builder.Append(", ");
          }
          builder.Append("Values = ");
          builder.Append(this.Values);
          builder.Append(", CircledEnd = ");
          builder.Append(this.CircledEnd.ToString());
          return true;
        }

        // Token: 0x060042CB RID: 17099 RVA: 0x006CF220 File Offset: 0x006CF220
        [NullableContext(2)]
        [CompilerGenerated]
        public static bool operator !=(ClickyCardFace.CardDetail.Sparkline left, ClickyCardFace.CardDetail.Sparkline right)
        {
          return !(left == right);
        }

        // Token: 0x060042CC RID: 17100 RVA: 0x006CF22C File Offset: 0x006CF22C
        [NullableContext(2)]
        [CompilerGenerated]
        public static bool operator ==(ClickyCardFace.CardDetail.Sparkline left, ClickyCardFace.CardDetail.Sparkline right)
        {
          return left == right || (left != null && left.Equals(right));
        }

        // Token: 0x060042CD RID: 17101 RVA: 0x006CF240 File Offset: 0x006CF240
        [CompilerGenerated]
        public override int GetHashCode()
        {
          return (base.GetHashCode() * -1521134295 + EqualityComparer<double[]>.Default.GetHashCode(this.<Values>k__BackingField)) * -1521134295 + EqualityComparer<bool>.Default.GetHashCode(this.<CircledEnd>k__BackingField);
        }

        // Token: 0x060042CE RID: 17102 RVA: 0x006CF278 File Offset: 0x006CF278
        [NullableContext(2)]
        [CompilerGenerated]
        public override bool Equals(object obj)
        {
          return this.Equals(obj as ClickyCardFace.CardDetail.Sparkline);
        }

        // Token: 0x060042CF RID: 17103 RVA: 0x006CF288 File Offset: 0x006CF288
        [NullableContext(2)]
        [CompilerGenerated]
        public sealed override bool Equals(ClickyCardFace.CardDetail other)
        {
          return this.Equals(other);
        }

        // Token: 0x060042D0 RID: 17104 RVA: 0x006CF294 File Offset: 0x006CF294
        [NullableContext(2)]
        [CompilerGenerated]
        public bool Equals(ClickyCardFace.CardDetail.Sparkline other)
        {
          return this == other || (base.Equals(other) && EqualityComparer<double[]>.Default.Equals(this.<Values>k__BackingField, other.<Values>k__BackingField) && EqualityComparer<bool>.Default.Equals(this.<CircledEnd>k__BackingField, other.<CircledEnd>k__BackingField));
        }

        // Token: 0x060042D2 RID: 17106 RVA: 0x006CF2E8 File Offset: 0x006CF2E8
        [CompilerGenerated]
        private Sparkline(ClickyCardFace.CardDetail.Sparkline original)
          : base(original)
        {
          this.Values = original.<Values>k__BackingField;
          this.CircledEnd = original.<CircledEnd>k__BackingField;
        }

        // Token: 0x060042D3 RID: 17107 RVA: 0x006CF30C File Offset: 0x006CF30C
        [CompilerGenerated]
        public void Deconstruct(out double[] Values, out bool CircledEnd)
        {
          Values = this.Values;
          CircledEnd = this.CircledEnd;
        }
      }

      // Token: 0x02000AEF RID: 2799
      [Nullable(0)]
      public sealed class Rows : ClickyCardFace.CardDetail, IEquatable<ClickyCardFace.CardDetail.Rows>
      {
        // Token: 0x060042D4 RID: 17108 RVA: 0x006CF320 File Offset: 0x006CF320
        public Rows(ClickyCardFace.CardRow[] Items)
        {
          this.Items = Items;
          base..ctor();
        }

        // Token: 0x1700088F RID: 2191
        // (get) Token: 0x060042D5 RID: 17109 RVA: 0x006CF330 File Offset: 0x006CF330
        [CompilerGenerated]
        protected override Type EqualityContract
        {
          [CompilerGenerated]
          get
          {
            return typeof(ClickyCardFace.CardDetail.Rows);
          }
        }

        // Token: 0x17000890 RID: 2192
        // (get) Token: 0x060042D6 RID: 17110 RVA: 0x006CF33C File Offset: 0x006CF33C
        // (set) Token: 0x060042D7 RID: 17111 RVA: 0x006CF344 File Offset: 0x006CF344
        public ClickyCardFace.CardRow[] Items { get; set; }

        // Token: 0x060042D8 RID: 17112 RVA: 0x006CF350 File Offset: 0x006CF350
        [CompilerGenerated]
        public override string ToString()
        {
          StringBuilder stringBuilder = new StringBuilder();
          stringBuilder.Append("Rows");
          stringBuilder.Append(" { ");
          if (this.PrintMembers(stringBuilder))
          {
            stringBuilder.Append(' ');
          }
          stringBuilder.Append('}');
          return stringBuilder.ToString();
        }

        // Token: 0x060042D9 RID: 17113 RVA: 0x006CF39C File Offset: 0x006CF39C
        [CompilerGenerated]
        protected override bool PrintMembers(StringBuilder builder)
        {
          if (base.PrintMembers(builder))
          {
            builder.Append(", ");
          }
          builder.Append("Items = ");
          builder.Append(this.Items);
          return true;
        }

        // Token: 0x060042DA RID: 17114 RVA: 0x006CF3D0 File Offset: 0x006CF3D0
        [NullableContext(2)]
        [CompilerGenerated]
        public static bool operator !=(ClickyCardFace.CardDetail.Rows left, ClickyCardFace.CardDetail.Rows right)
        {
          return !(left == right);
        }

        // Token: 0x060042DB RID: 17115 RVA: 0x006CF3DC File Offset: 0x006CF3DC
        [NullableContext(2)]
        [CompilerGenerated]
        public static bool operator ==(ClickyCardFace.CardDetail.Rows left, ClickyCardFace.CardDetail.Rows right)
        {
          return left == right || (left != null && left.Equals(right));
        }

        // Token: 0x060042DC RID: 17116 RVA: 0x006CF3F0 File Offset: 0x006CF3F0
        [CompilerGenerated]
        public override int GetHashCode()
        {
          return base.GetHashCode() * -1521134295 + EqualityComparer<ClickyCardFace.CardRow[]>.Default.GetHashCode(this.<Items>k__BackingField);
        }

        // Token: 0x060042DD RID: 17117 RVA: 0x006CF410 File Offset: 0x006CF410
        [NullableContext(2)]
        [CompilerGenerated]
        public override bool Equals(object obj)
        {
          return this.Equals(obj as ClickyCardFace.CardDetail.Rows);
        }

        // Token: 0x060042DE RID: 17118 RVA: 0x006CF420 File Offset: 0x006CF420
        [NullableContext(2)]
        [CompilerGenerated]
        public sealed override bool Equals(ClickyCardFace.CardDetail other)
        {
          return this.Equals(other);
        }

        // Token: 0x060042DF RID: 17119 RVA: 0x006CF42C File Offset: 0x006CF42C
        [NullableContext(2)]
        [CompilerGenerated]
        public bool Equals(ClickyCardFace.CardDetail.Rows other)
        {
          return this == other || (base.Equals(other) && EqualityComparer<ClickyCardFace.CardRow[]>.Default.Equals(this.<Items>k__BackingField, other.<Items>k__BackingField));
        }

        // Token: 0x060042E1 RID: 17121 RVA: 0x006CF460 File Offset: 0x006CF460
        [CompilerGenerated]
        private Rows(ClickyCardFace.CardDetail.Rows original)
          : base(original)
        {
          this.Items = original.<Items>k__BackingField;
        }

        // Token: 0x060042E2 RID: 17122 RVA: 0x006CF478 File Offset: 0x006CF478
        [CompilerGenerated]
        public void Deconstruct(out ClickyCardFace.CardRow[] Items)
        {
          Items = this.Items;
        }
      }

      // Token: 0x02000AF0 RID: 2800
      [Nullable(0)]
      public sealed class Bullets : ClickyCardFace.CardDetail, IEquatable<ClickyCardFace.CardDetail.Bullets>
      {
        // Token: 0x060042E3 RID: 17123 RVA: 0x006CF484 File Offset: 0x006CF484
        public Bullets(string[] Items)
        {
          this.Items = Items;
          base..ctor();
        }

        // Token: 0x17000891 RID: 2193
        // (get) Token: 0x060042E4 RID: 17124 RVA: 0x006CF494 File Offset: 0x006CF494
        [CompilerGenerated]
        protected override Type EqualityContract
        {
          [CompilerGenerated]
          get
          {
            return typeof(ClickyCardFace.CardDetail.Bullets);
          }
        }

        // Token: 0x17000892 RID: 2194
        // (get) Token: 0x060042E5 RID: 17125 RVA: 0x006CF4A0 File Offset: 0x006CF4A0
        // (set) Token: 0x060042E6 RID: 17126 RVA: 0x006CF4A8 File Offset: 0x006CF4A8
        public string[] Items { get; set; }

        // Token: 0x060042E7 RID: 17127 RVA: 0x006CF4B4 File Offset: 0x006CF4B4
        [CompilerGenerated]
        public override string ToString()
        {
          StringBuilder stringBuilder = new StringBuilder();
          stringBuilder.Append("Bullets");
          stringBuilder.Append(" { ");
          if (this.PrintMembers(stringBuilder))
          {
            stringBuilder.Append(' ');
          }
          stringBuilder.Append('}');
          return stringBuilder.ToString();
        }

        // Token: 0x060042E8 RID: 17128 RVA: 0x006CF500 File Offset: 0x006CF500
        [CompilerGenerated]
        protected override bool PrintMembers(StringBuilder builder)
        {
          if (base.PrintMembers(builder))
          {
            builder.Append(", ");
          }
          builder.Append("Items = ");
          builder.Append(this.Items);
          return true;
        }

        // Token: 0x060042E9 RID: 17129 RVA: 0x006CF534 File Offset: 0x006CF534
        [NullableContext(2)]
        [CompilerGenerated]
        public static bool operator !=(ClickyCardFace.CardDetail.Bullets left, ClickyCardFace.CardDetail.Bullets right)
        {
          return !(left == right);
        }

        // Token: 0x060042EA RID: 17130 RVA: 0x006CF540 File Offset: 0x006CF540
        [NullableContext(2)]
        [CompilerGenerated]
        public static bool operator ==(ClickyCardFace.CardDetail.Bullets left, ClickyCardFace.CardDetail.Bullets right)
        {
          return left == right || (left != null && left.Equals(right));
        }

        // Token: 0x060042EB RID: 17131 RVA: 0x006CF554 File Offset: 0x006CF554
        [CompilerGenerated]
        public override int GetHashCode()
        {
          return base.GetHashCode() * -1521134295 + EqualityComparer<string[]>.Default.GetHashCode(this.<Items>k__BackingField);
        }

        // Token: 0x060042EC RID: 17132 RVA: 0x006CF574 File Offset: 0x006CF574
        [NullableContext(2)]
        [CompilerGenerated]
        public override bool Equals(object obj)
        {
          return this.Equals(obj as ClickyCardFace.CardDetail.Bullets);
        }

        // Token: 0x060042ED RID: 17133 RVA: 0x006CF584 File Offset: 0x006CF584
        [NullableContext(2)]
        [CompilerGenerated]
        public sealed override bool Equals(ClickyCardFace.CardDetail other)
        {
          return this.Equals(other);
        }

        // Token: 0x060042EE RID: 17134 RVA: 0x006CF590 File Offset: 0x006CF590
        [NullableContext(2)]
        [CompilerGenerated]
        public bool Equals(ClickyCardFace.CardDetail.Bullets other)
        {
          return this == other || (base.Equals(other) && EqualityComparer<string[]>.Default.Equals(this.<Items>k__BackingField, other.<Items>k__BackingField));
        }

        // Token: 0x060042F0 RID: 17136 RVA: 0x006CF5C4 File Offset: 0x006CF5C4
        [CompilerGenerated]
        private Bullets(ClickyCardFace.CardDetail.Bullets original)
          : base(original)
        {
          this.Items = original.<Items>k__BackingField;
        }

        // Token: 0x060042F1 RID: 17137 RVA: 0x006CF5DC File Offset: 0x006CF5DC
        [CompilerGenerated]
        public void Deconstruct(out string[] Items)
        {
          Items = this.Items;
        }
      }

      // Token: 0x02000AF1 RID: 2801
      [Nullable(0)]
      public sealed class Waiting : ClickyCardFace.CardDetail, IEquatable<ClickyCardFace.CardDetail.Waiting>
      {
        // Token: 0x060042F2 RID: 17138 RVA: 0x006CF5E8 File Offset: 0x006CF5E8
        public Waiting(string[] Names)
        {
          this.Names = Names;
          base..ctor();
        }

        // Token: 0x17000893 RID: 2195
        // (get) Token: 0x060042F3 RID: 17139 RVA: 0x006CF5F8 File Offset: 0x006CF5F8
        [CompilerGenerated]
        protected override Type EqualityContract
        {
          [CompilerGenerated]
          get
          {
            return typeof(ClickyCardFace.CardDetail.Waiting);
          }
        }

        // Token: 0x17000894 RID: 2196
        // (get) Token: 0x060042F4 RID: 17140 RVA: 0x006CF604 File Offset: 0x006CF604
        // (set) Token: 0x060042F5 RID: 17141 RVA: 0x006CF60C File Offset: 0x006CF60C
        public string[] Names { get; set; }

        // Token: 0x060042F6 RID: 17142 RVA: 0x006CF618 File Offset: 0x006CF618
        [CompilerGenerated]
        public override string ToString()
        {
          StringBuilder stringBuilder = new StringBuilder();
          stringBuilder.Append("Waiting");
          stringBuilder.Append(" { ");
          if (this.PrintMembers(stringBuilder))
          {
            stringBuilder.Append(' ');
          }
          stringBuilder.Append('}');
          return stringBuilder.ToString();
        }

        // Token: 0x060042F7 RID: 17143 RVA: 0x006CF664 File Offset: 0x006CF664
        [CompilerGenerated]
        protected override bool PrintMembers(StringBuilder builder)
        {
          if (base.PrintMembers(builder))
          {
            builder.Append(", ");
          }
          builder.Append("Names = ");
          builder.Append(this.Names);
          return true;
        }

        // Token: 0x060042F8 RID: 17144 RVA: 0x006CF698 File Offset: 0x006CF698
        [NullableContext(2)]
        [CompilerGenerated]
        public static bool operator !=(ClickyCardFace.CardDetail.Waiting left, ClickyCardFace.CardDetail.Waiting right)
        {
          return !(left == right);
        }

        // Token: 0x060042F9 RID: 17145 RVA: 0x006CF6A4 File Offset: 0x006CF6A4
        [NullableContext(2)]
        [CompilerGenerated]
        public static bool operator ==(ClickyCardFace.CardDetail.Waiting left, ClickyCardFace.CardDetail.Waiting right)
        {
          return left == right || (left != null && left.Equals(right));
        }

        // Token: 0x060042FA RID: 17146 RVA: 0x006CF6B8 File Offset: 0x006CF6B8
        [CompilerGenerated]
        public override int GetHashCode()
        {
          return base.GetHashCode() * -1521134295 + EqualityComparer<string[]>.Default.GetHashCode(this.<Names>k__BackingField);
        }

        // Token: 0x060042FB RID: 17147 RVA: 0x006CF6D8 File Offset: 0x006CF6D8
        [NullableContext(2)]
        [CompilerGenerated]
        public override bool Equals(object obj)
        {
          return this.Equals(obj as ClickyCardFace.CardDetail.Waiting);
        }

        // Token: 0x060042FC RID: 17148 RVA: 0x006CF6E8 File Offset: 0x006CF6E8
        [NullableContext(2)]
        [CompilerGenerated]
        public sealed override bool Equals(ClickyCardFace.CardDetail other)
        {
          return this.Equals(other);
        }

        // Token: 0x060042FD RID: 17149 RVA: 0x006CF6F4 File Offset: 0x006CF6F4
        [NullableContext(2)]
        [CompilerGenerated]
        public bool Equals(ClickyCardFace.CardDetail.Waiting other)
        {
          return this == other || (base.Equals(other) && EqualityComparer<string[]>.Default.Equals(this.<Names>k__BackingField, other.<Names>k__BackingField));
        }

        // Token: 0x060042FF RID: 17151 RVA: 0x006CF728 File Offset: 0x006CF728
        [CompilerGenerated]
        private Waiting(ClickyCardFace.CardDetail.Waiting original)
          : base(original)
        {
          this.Names = original.<Names>k__BackingField;
        }

        // Token: 0x06004300 RID: 17152 RVA: 0x006CF740 File Offset: 0x006CF740
        [CompilerGenerated]
        public void Deconstruct(out string[] Names)
        {
          Names = this.Names;
        }
      }
    }

    // Token: 0x02000768 RID: 1896
    [NullableContext(1)]
    [Nullable(0)]
    public sealed class CardRow : IEquatable<ClickyCardFace.CardRow>
    {
      // Token: 0x0600353B RID: 13627 RVA: 0x0067BB8C File Offset: 0x0067BB8C
      public CardRow(string Label, string Value)
      {
        this.Label = Label;
        this.Value = Value;
        base..ctor();
      }

      // Token: 0x170007B3 RID: 1971
      // (get) Token: 0x0600353C RID: 13628 RVA: 0x0067BBA4 File Offset: 0x0067BBA4
      [CompilerGenerated]
      private Type EqualityContract
      {
        [CompilerGenerated]
        get
        {
          return typeof(ClickyCardFace.CardRow);
        }
      }

      // Token: 0x170007B4 RID: 1972
      // (get) Token: 0x0600353D RID: 13629 RVA: 0x0067BBB0 File Offset: 0x0067BBB0
      // (set) Token: 0x0600353E RID: 13630 RVA: 0x0067BBB8 File Offset: 0x0067BBB8
      public string Label { get; set; }

      // Token: 0x170007B5 RID: 1973
      // (get) Token: 0x0600353F RID: 13631 RVA: 0x0067BBC4 File Offset: 0x0067BBC4
      // (set) Token: 0x06003540 RID: 13632 RVA: 0x0067BBCC File Offset: 0x0067BBCC
      public string Value { get; set; }

      // Token: 0x06003541 RID: 13633 RVA: 0x0067BBD8 File Offset: 0x0067BBD8
      [CompilerGenerated]
      public override string ToString()
      {
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.Append("CardRow");
        stringBuilder.Append(" { ");
        if (this.PrintMembers(stringBuilder))
        {
          stringBuilder.Append(' ');
        }
        stringBuilder.Append('}');
        return stringBuilder.ToString();
      }

      // Token: 0x06003542 RID: 13634 RVA: 0x0067BC24 File Offset: 0x0067BC24
      [CompilerGenerated]
      private bool PrintMembers(StringBuilder builder)
      {
        RuntimeHelpers.EnsureSufficientExecutionStack();
        builder.Append("Label = ");
        builder.Append(this.Label);
        builder.Append(", Value = ");
        builder.Append(this.Value);
        return true;
      }

      // Token: 0x06003543 RID: 13635 RVA: 0x0067BC60 File Offset: 0x0067BC60
      [NullableContext(2)]
      [CompilerGenerated]
      public static bool operator !=(ClickyCardFace.CardRow left, ClickyCardFace.CardRow right)
      {
        return !(left == right);
      }

      // Token: 0x06003544 RID: 13636 RVA: 0x0067BC6C File Offset: 0x0067BC6C
      [NullableContext(2)]
      [CompilerGenerated]
      public static bool operator ==(ClickyCardFace.CardRow left, ClickyCardFace.CardRow right)
      {
        return left == right || (left != null && left.Equals(right));
      }

      // Token: 0x06003545 RID: 13637 RVA: 0x0067BC80 File Offset: 0x0067BC80
      [CompilerGenerated]
      public override int GetHashCode()
      {
        return (EqualityComparer<Type>.Default.GetHashCode(this.EqualityContract) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Label>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Value>k__BackingField);
      }

      // Token: 0x06003546 RID: 13638 RVA: 0x0067BCC0 File Offset: 0x0067BCC0
      [NullableContext(2)]
      [CompilerGenerated]
      public override bool Equals(object obj)
      {
        return this.Equals(obj as ClickyCardFace.CardRow);
      }

      // Token: 0x06003547 RID: 13639 RVA: 0x0067BCD0 File Offset: 0x0067BCD0
      [NullableContext(2)]
      [CompilerGenerated]
      public bool Equals(ClickyCardFace.CardRow other)
      {
        return this == other || (other != null && this.EqualityContract == other.EqualityContract && EqualityComparer<string>.Default.Equals(this.<Label>k__BackingField, other.<Label>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<Value>k__BackingField, other.<Value>k__BackingField));
      }

      // Token: 0x06003549 RID: 13641 RVA: 0x0067BD34 File Offset: 0x0067BD34
      [CompilerGenerated]
      private CardRow(ClickyCardFace.CardRow original)
      {
        this.Label = original.<Label>k__BackingField;
        this.Value = original.<Value>k__BackingField;
      }

      // Token: 0x0600354A RID: 13642 RVA: 0x0067BD54 File Offset: 0x0067BD54
      [CompilerGenerated]
      public void Deconstruct(out string Label, out string Value)
      {
        Label = this.Label;
        Value = this.Value;
      }
    }

    // Token: 0x02000769 RID: 1897
    [NullableContext(0)]
    public enum CardFootTone
    {
      // Token: 0x0400208D RID: 8333
      Deep,
      // Token: 0x0400208E RID: 8334
      Good,
      // Token: 0x0400208F RID: 8335
      Quiet
    }
  }
}
