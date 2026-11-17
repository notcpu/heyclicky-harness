using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace HeyClicky.App.Ink
{
  // Token: 0x020001BF RID: 447
  [NullableContext(1)]
  [Nullable(0)]
  public sealed class ClickyNoteFace : IEquatable<ClickyNoteFace>
  {
    // Token: 0x0600177F RID: 6015 RVA: 0x005C27E4 File Offset: 0x005C27E4
    public ClickyNoteFace(string Title, ClickyPaperGlyph Glyph, ClickyNoteFace.NoteBody Body)
    {
      this.Title = Title;
      this.Glyph = Glyph;
      this.Body = Body;
      this.Footer = new ClickyNoteFace.NoteFooter.None();
      base..ctor();
    }

    // Token: 0x17000430 RID: 1072
    // (get) Token: 0x06001780 RID: 6016 RVA: 0x005C280C File Offset: 0x005C280C
    [CompilerGenerated]
    private Type EqualityContract
    {
      [CompilerGenerated]
      get
      {
        return typeof(ClickyNoteFace);
      }
    }

    // Token: 0x17000431 RID: 1073
    // (get) Token: 0x06001781 RID: 6017 RVA: 0x005C2818 File Offset: 0x005C2818
    // (set) Token: 0x06001782 RID: 6018 RVA: 0x005C2820 File Offset: 0x005C2820
    public string Title { get; set; }

    // Token: 0x17000432 RID: 1074
    // (get) Token: 0x06001783 RID: 6019 RVA: 0x005C282C File Offset: 0x005C282C
    // (set) Token: 0x06001784 RID: 6020 RVA: 0x005C2834 File Offset: 0x005C2834
    public ClickyPaperGlyph Glyph { get; set; }

    // Token: 0x17000433 RID: 1075
    // (get) Token: 0x06001785 RID: 6021 RVA: 0x005C2840 File Offset: 0x005C2840
    // (set) Token: 0x06001786 RID: 6022 RVA: 0x005C2848 File Offset: 0x005C2848
    public ClickyNoteFace.NoteBody Body { get; set; }

    // Token: 0x17000434 RID: 1076
    // (get) Token: 0x06001787 RID: 6023 RVA: 0x005C2854 File Offset: 0x005C2854
    // (set) Token: 0x06001788 RID: 6024 RVA: 0x005C285C File Offset: 0x005C285C
    [Nullable(2)]
    public string Meta
    {
      [NullableContext(2)]
      get;
      [NullableContext(2)]
      set;
    }

    // Token: 0x17000435 RID: 1077
    // (get) Token: 0x06001789 RID: 6025 RVA: 0x005C2868 File Offset: 0x005C2868
    // (set) Token: 0x0600178A RID: 6026 RVA: 0x005C2870 File Offset: 0x005C2870
    public ClickyNoteFace.NoteFooter Footer { get; set; }

    // Token: 0x17000436 RID: 1078
    // (get) Token: 0x0600178B RID: 6027 RVA: 0x005C287C File Offset: 0x005C287C
    // (set) Token: 0x0600178C RID: 6028 RVA: 0x005C2884 File Offset: 0x005C2884
    public ClickyThingStatus? Stamp { get; set; }

    // Token: 0x17000437 RID: 1079
    // (get) Token: 0x0600178D RID: 6029 RVA: 0x005C2890 File Offset: 0x005C2890
    // (set) Token: 0x0600178E RID: 6030 RVA: 0x005C2898 File Offset: 0x005C2898
    public bool IsIdea { get; set; }

    // Token: 0x17000438 RID: 1080
    // (get) Token: 0x0600178F RID: 6031 RVA: 0x005C28A4 File Offset: 0x005C28A4
    // (set) Token: 0x06001790 RID: 6032 RVA: 0x005C28AC File Offset: 0x005C28AC
    public ClickyPaperGlyph? MetaLogo { get; set; }

    // Token: 0x17000439 RID: 1081
    // (get) Token: 0x06001791 RID: 6033 RVA: 0x005C28B8 File Offset: 0x005C28B8
    // (set) Token: 0x06001792 RID: 6034 RVA: 0x005C28C0 File Offset: 0x005C28C0
    public bool MetaIsClicky { get; set; }

    // Token: 0x06001793 RID: 6035 RVA: 0x005C28CC File Offset: 0x005C28CC
    [CompilerGenerated]
    public override string ToString()
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("ClickyNoteFace");
      stringBuilder.Append(" { ");
      if (this.PrintMembers(stringBuilder))
      {
        stringBuilder.Append(' ');
      }
      stringBuilder.Append('}');
      return stringBuilder.ToString();
    }

    // Token: 0x06001794 RID: 6036 RVA: 0x005C2918 File Offset: 0x005C2918
    [CompilerGenerated]
    private bool PrintMembers(StringBuilder builder)
    {
      RuntimeHelpers.EnsureSufficientExecutionStack();
      builder.Append("Title = ");
      builder.Append(this.Title);
      builder.Append(", Glyph = ");
      builder.Append(this.Glyph.ToString());
      builder.Append(", Body = ");
      builder.Append(this.Body);
      builder.Append(", Meta = ");
      builder.Append(this.Meta);
      builder.Append(", Footer = ");
      builder.Append(this.Footer);
      builder.Append(", Stamp = ");
      builder.Append(this.Stamp.ToString());
      builder.Append(", IsIdea = ");
      builder.Append(this.IsIdea.ToString());
      builder.Append(", MetaLogo = ");
      builder.Append(this.MetaLogo.ToString());
      builder.Append(", MetaIsClicky = ");
      builder.Append(this.MetaIsClicky.ToString());
      return true;
    }

    // Token: 0x06001795 RID: 6037 RVA: 0x005C2A54 File Offset: 0x005C2A54
    [NullableContext(2)]
    [CompilerGenerated]
    public static bool operator !=(ClickyNoteFace left, ClickyNoteFace right)
    {
      return !(left == right);
    }

    // Token: 0x06001796 RID: 6038 RVA: 0x005C2A60 File Offset: 0x005C2A60
    [NullableContext(2)]
    [CompilerGenerated]
    public static bool operator ==(ClickyNoteFace left, ClickyNoteFace right)
    {
      return left == right || (left != null && left.Equals(right));
    }

    // Token: 0x06001797 RID: 6039 RVA: 0x005C2A74 File Offset: 0x005C2A74
    [CompilerGenerated]
    public override int GetHashCode()
    {
      return ((((((((EqualityComparer<Type>.Default.GetHashCode(this.EqualityContract) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Title>k__BackingField)) * -1521134295 + EqualityComparer<ClickyPaperGlyph>.Default.GetHashCode(this.<Glyph>k__BackingField)) * -1521134295 + EqualityComparer<ClickyNoteFace.NoteBody>.Default.GetHashCode(this.<Body>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Meta>k__BackingField)) * -1521134295 + EqualityComparer<ClickyNoteFace.NoteFooter>.Default.GetHashCode(this.<Footer>k__BackingField)) * -1521134295 + EqualityComparer<ClickyThingStatus?>.Default.GetHashCode(this.<Stamp>k__BackingField)) * -1521134295 + EqualityComparer<bool>.Default.GetHashCode(this.<IsIdea>k__BackingField)) * -1521134295 + EqualityComparer<ClickyPaperGlyph?>.Default.GetHashCode(this.<MetaLogo>k__BackingField)) * -1521134295 + EqualityComparer<bool>.Default.GetHashCode(this.<MetaIsClicky>k__BackingField);
    }

    // Token: 0x06001798 RID: 6040 RVA: 0x005C2B60 File Offset: 0x005C2B60
    [NullableContext(2)]
    [CompilerGenerated]
    public override bool Equals(object obj)
    {
      return this.Equals(obj as ClickyNoteFace);
    }

    // Token: 0x06001799 RID: 6041 RVA: 0x005C2B70 File Offset: 0x005C2B70
    [NullableContext(2)]
    [CompilerGenerated]
    public bool Equals(ClickyNoteFace other)
    {
      return this == other || (other != null && this.EqualityContract == other.EqualityContract && EqualityComparer<string>.Default.Equals(this.<Title>k__BackingField, other.<Title>k__BackingField) && EqualityComparer<ClickyPaperGlyph>.Default.Equals(this.<Glyph>k__BackingField, other.<Glyph>k__BackingField) && EqualityComparer<ClickyNoteFace.NoteBody>.Default.Equals(this.<Body>k__BackingField, other.<Body>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<Meta>k__BackingField, other.<Meta>k__BackingField) && EqualityComparer<ClickyNoteFace.NoteFooter>.Default.Equals(this.<Footer>k__BackingField, other.<Footer>k__BackingField) && EqualityComparer<ClickyThingStatus?>.Default.Equals(this.<Stamp>k__BackingField, other.<Stamp>k__BackingField) && EqualityComparer<bool>.Default.Equals(this.<IsIdea>k__BackingField, other.<IsIdea>k__BackingField) && EqualityComparer<ClickyPaperGlyph?>.Default.Equals(this.<MetaLogo>k__BackingField, other.<MetaLogo>k__BackingField) && EqualityComparer<bool>.Default.Equals(this.<MetaIsClicky>k__BackingField, other.<MetaIsClicky>k__BackingField));
    }

    // Token: 0x0600179B RID: 6043 RVA: 0x005C2C8C File Offset: 0x005C2C8C
    [CompilerGenerated]
    private ClickyNoteFace(ClickyNoteFace original)
    {
      this.Title = original.<Title>k__BackingField;
      this.Glyph = original.<Glyph>k__BackingField;
      this.Body = original.<Body>k__BackingField;
      this.Meta = original.<Meta>k__BackingField;
      this.Footer = original.<Footer>k__BackingField;
      this.Stamp = original.<Stamp>k__BackingField;
      this.IsIdea = original.<IsIdea>k__BackingField;
      this.MetaLogo = original.<MetaLogo>k__BackingField;
      this.MetaIsClicky = original.<MetaIsClicky>k__BackingField;
    }

    // Token: 0x0600179C RID: 6044 RVA: 0x005C2D0C File Offset: 0x005C2D0C
    [CompilerGenerated]
    public void Deconstruct(out string Title, out ClickyPaperGlyph Glyph, out ClickyNoteFace.NoteBody Body)
    {
      Title = this.Title;
      Glyph = this.Glyph;
      Body = this.Body;
    }

    // Token: 0x0200077A RID: 1914
    [Nullable(0)]
    public abstract class NoteBody : IEquatable<ClickyNoteFace.NoteBody>
    {
      // Token: 0x170007BA RID: 1978
      // (get) Token: 0x06003587 RID: 13703 RVA: 0x0067C7DC File Offset: 0x0067C7DC
      [CompilerGenerated]
      protected virtual Type EqualityContract
      {
        [CompilerGenerated]
        get
        {
          return typeof(ClickyNoteFace.NoteBody);
        }
      }

      // Token: 0x06003588 RID: 13704 RVA: 0x0067C7E8 File Offset: 0x0067C7E8
      [CompilerGenerated]
      public override string ToString()
      {
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.Append("NoteBody");
        stringBuilder.Append(" { ");
        if (this.PrintMembers(stringBuilder))
        {
          stringBuilder.Append(' ');
        }
        stringBuilder.Append('}');
        return stringBuilder.ToString();
      }

      // Token: 0x06003589 RID: 13705 RVA: 0x0067C834 File Offset: 0x0067C834
      [CompilerGenerated]
      protected virtual bool PrintMembers(StringBuilder builder)
      {
        return false;
      }

      // Token: 0x0600358A RID: 13706 RVA: 0x0067C838 File Offset: 0x0067C838
      [NullableContext(2)]
      [CompilerGenerated]
      public static bool operator !=(ClickyNoteFace.NoteBody left, ClickyNoteFace.NoteBody right)
      {
        return !(left == right);
      }

      // Token: 0x0600358B RID: 13707 RVA: 0x0067C844 File Offset: 0x0067C844
      [NullableContext(2)]
      [CompilerGenerated]
      public static bool operator ==(ClickyNoteFace.NoteBody left, ClickyNoteFace.NoteBody right)
      {
        return left == right || (left != null && left.Equals(right));
      }

      // Token: 0x0600358C RID: 13708 RVA: 0x0067C858 File Offset: 0x0067C858
      [CompilerGenerated]
      public override int GetHashCode()
      {
        return EqualityComparer<Type>.Default.GetHashCode(this.EqualityContract);
      }

      // Token: 0x0600358D RID: 13709 RVA: 0x0067C86C File Offset: 0x0067C86C
      [NullableContext(2)]
      [CompilerGenerated]
      public override bool Equals(object obj)
      {
        return this.Equals(obj as ClickyNoteFace.NoteBody);
      }

      // Token: 0x0600358E RID: 13710 RVA: 0x0067C87C File Offset: 0x0067C87C
      [NullableContext(2)]
      [CompilerGenerated]
      public virtual bool Equals(ClickyNoteFace.NoteBody other)
      {
        return this == other || (other != null && this.EqualityContract == other.EqualityContract);
      }

      // Token: 0x06003590 RID: 13712 RVA: 0x0067C89C File Offset: 0x0067C89C
      [CompilerGenerated]
      protected NoteBody(ClickyNoteFace.NoteBody original)
      {
      }

      // Token: 0x06003591 RID: 13713 RVA: 0x0067C8A4 File Offset: 0x0067C8A4
      protected NoteBody()
      {
      }

      // Token: 0x02000AF2 RID: 2802
      [Nullable(0)]
      public sealed class Lines : ClickyNoteFace.NoteBody, IEquatable<ClickyNoteFace.NoteBody.Lines>
      {
        // Token: 0x06004301 RID: 17153 RVA: 0x006CF74C File Offset: 0x006CF74C
        public Lines(ClickyNoteFace.NoteLine[] Items)
        {
          this.Items = Items;
          base..ctor();
        }

        // Token: 0x17000895 RID: 2197
        // (get) Token: 0x06004302 RID: 17154 RVA: 0x006CF75C File Offset: 0x006CF75C
        [CompilerGenerated]
        protected override Type EqualityContract
        {
          [CompilerGenerated]
          get
          {
            return typeof(ClickyNoteFace.NoteBody.Lines);
          }
        }

        // Token: 0x17000896 RID: 2198
        // (get) Token: 0x06004303 RID: 17155 RVA: 0x006CF768 File Offset: 0x006CF768
        // (set) Token: 0x06004304 RID: 17156 RVA: 0x006CF770 File Offset: 0x006CF770
        public ClickyNoteFace.NoteLine[] Items { get; set; }

        // Token: 0x06004305 RID: 17157 RVA: 0x006CF77C File Offset: 0x006CF77C
        [CompilerGenerated]
        public override string ToString()
        {
          StringBuilder stringBuilder = new StringBuilder();
          stringBuilder.Append("Lines");
          stringBuilder.Append(" { ");
          if (this.PrintMembers(stringBuilder))
          {
            stringBuilder.Append(' ');
          }
          stringBuilder.Append('}');
          return stringBuilder.ToString();
        }

        // Token: 0x06004306 RID: 17158 RVA: 0x006CF7C8 File Offset: 0x006CF7C8
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

        // Token: 0x06004307 RID: 17159 RVA: 0x006CF7FC File Offset: 0x006CF7FC
        [NullableContext(2)]
        [CompilerGenerated]
        public static bool operator !=(ClickyNoteFace.NoteBody.Lines left, ClickyNoteFace.NoteBody.Lines right)
        {
          return !(left == right);
        }

        // Token: 0x06004308 RID: 17160 RVA: 0x006CF808 File Offset: 0x006CF808
        [NullableContext(2)]
        [CompilerGenerated]
        public static bool operator ==(ClickyNoteFace.NoteBody.Lines left, ClickyNoteFace.NoteBody.Lines right)
        {
          return left == right || (left != null && left.Equals(right));
        }

        // Token: 0x06004309 RID: 17161 RVA: 0x006CF81C File Offset: 0x006CF81C
        [CompilerGenerated]
        public override int GetHashCode()
        {
          return base.GetHashCode() * -1521134295 + EqualityComparer<ClickyNoteFace.NoteLine[]>.Default.GetHashCode(this.<Items>k__BackingField);
        }

        // Token: 0x0600430A RID: 17162 RVA: 0x006CF83C File Offset: 0x006CF83C
        [NullableContext(2)]
        [CompilerGenerated]
        public override bool Equals(object obj)
        {
          return this.Equals(obj as ClickyNoteFace.NoteBody.Lines);
        }

        // Token: 0x0600430B RID: 17163 RVA: 0x006CF84C File Offset: 0x006CF84C
        [NullableContext(2)]
        [CompilerGenerated]
        public sealed override bool Equals(ClickyNoteFace.NoteBody other)
        {
          return this.Equals(other);
        }

        // Token: 0x0600430C RID: 17164 RVA: 0x006CF858 File Offset: 0x006CF858
        [NullableContext(2)]
        [CompilerGenerated]
        public bool Equals(ClickyNoteFace.NoteBody.Lines other)
        {
          return this == other || (base.Equals(other) && EqualityComparer<ClickyNoteFace.NoteLine[]>.Default.Equals(this.<Items>k__BackingField, other.<Items>k__BackingField));
        }

        // Token: 0x0600430E RID: 17166 RVA: 0x006CF88C File Offset: 0x006CF88C
        [CompilerGenerated]
        private Lines(ClickyNoteFace.NoteBody.Lines original)
          : base(original)
        {
          this.Items = original.<Items>k__BackingField;
        }

        // Token: 0x0600430F RID: 17167 RVA: 0x006CF8A4 File Offset: 0x006CF8A4
        [CompilerGenerated]
        public void Deconstruct(out ClickyNoteFace.NoteLine[] Items)
        {
          Items = this.Items;
        }
      }

      // Token: 0x02000AF3 RID: 2803
      [Nullable(0)]
      public sealed class Chart : ClickyNoteFace.NoteBody, IEquatable<ClickyNoteFace.NoteBody.Chart>
      {
        // Token: 0x06004310 RID: 17168 RVA: 0x006CF8B0 File Offset: 0x006CF8B0
        public Chart(double[] Series, bool ShowsTrend)
        {
          this.Series = Series;
          this.ShowsTrend = ShowsTrend;
          base..ctor();
        }

        // Token: 0x17000897 RID: 2199
        // (get) Token: 0x06004311 RID: 17169 RVA: 0x006CF8C8 File Offset: 0x006CF8C8
        [CompilerGenerated]
        protected override Type EqualityContract
        {
          [CompilerGenerated]
          get
          {
            return typeof(ClickyNoteFace.NoteBody.Chart);
          }
        }

        // Token: 0x17000898 RID: 2200
        // (get) Token: 0x06004312 RID: 17170 RVA: 0x006CF8D4 File Offset: 0x006CF8D4
        // (set) Token: 0x06004313 RID: 17171 RVA: 0x006CF8DC File Offset: 0x006CF8DC
        public double[] Series { get; set; }

        // Token: 0x17000899 RID: 2201
        // (get) Token: 0x06004314 RID: 17172 RVA: 0x006CF8E8 File Offset: 0x006CF8E8
        // (set) Token: 0x06004315 RID: 17173 RVA: 0x006CF8F0 File Offset: 0x006CF8F0
        public bool ShowsTrend { get; set; }

        // Token: 0x06004316 RID: 17174 RVA: 0x006CF8FC File Offset: 0x006CF8FC
        [CompilerGenerated]
        public override string ToString()
        {
          StringBuilder stringBuilder = new StringBuilder();
          stringBuilder.Append("Chart");
          stringBuilder.Append(" { ");
          if (this.PrintMembers(stringBuilder))
          {
            stringBuilder.Append(' ');
          }
          stringBuilder.Append('}');
          return stringBuilder.ToString();
        }

        // Token: 0x06004317 RID: 17175 RVA: 0x006CF948 File Offset: 0x006CF948
        [CompilerGenerated]
        protected override bool PrintMembers(StringBuilder builder)
        {
          if (base.PrintMembers(builder))
          {
            builder.Append(", ");
          }
          builder.Append("Series = ");
          builder.Append(this.Series);
          builder.Append(", ShowsTrend = ");
          builder.Append(this.ShowsTrend.ToString());
          return true;
        }

        // Token: 0x06004318 RID: 17176 RVA: 0x006CF9AC File Offset: 0x006CF9AC
        [NullableContext(2)]
        [CompilerGenerated]
        public static bool operator !=(ClickyNoteFace.NoteBody.Chart left, ClickyNoteFace.NoteBody.Chart right)
        {
          return !(left == right);
        }

        // Token: 0x06004319 RID: 17177 RVA: 0x006CF9B8 File Offset: 0x006CF9B8
        [NullableContext(2)]
        [CompilerGenerated]
        public static bool operator ==(ClickyNoteFace.NoteBody.Chart left, ClickyNoteFace.NoteBody.Chart right)
        {
          return left == right || (left != null && left.Equals(right));
        }

        // Token: 0x0600431A RID: 17178 RVA: 0x006CF9CC File Offset: 0x006CF9CC
        [CompilerGenerated]
        public override int GetHashCode()
        {
          return (base.GetHashCode() * -1521134295 + EqualityComparer<double[]>.Default.GetHashCode(this.<Series>k__BackingField)) * -1521134295 + EqualityComparer<bool>.Default.GetHashCode(this.<ShowsTrend>k__BackingField);
        }

        // Token: 0x0600431B RID: 17179 RVA: 0x006CFA04 File Offset: 0x006CFA04
        [NullableContext(2)]
        [CompilerGenerated]
        public override bool Equals(object obj)
        {
          return this.Equals(obj as ClickyNoteFace.NoteBody.Chart);
        }

        // Token: 0x0600431C RID: 17180 RVA: 0x006CFA14 File Offset: 0x006CFA14
        [NullableContext(2)]
        [CompilerGenerated]
        public sealed override bool Equals(ClickyNoteFace.NoteBody other)
        {
          return this.Equals(other);
        }

        // Token: 0x0600431D RID: 17181 RVA: 0x006CFA20 File Offset: 0x006CFA20
        [NullableContext(2)]
        [CompilerGenerated]
        public bool Equals(ClickyNoteFace.NoteBody.Chart other)
        {
          return this == other || (base.Equals(other) && EqualityComparer<double[]>.Default.Equals(this.<Series>k__BackingField, other.<Series>k__BackingField) && EqualityComparer<bool>.Default.Equals(this.<ShowsTrend>k__BackingField, other.<ShowsTrend>k__BackingField));
        }

        // Token: 0x0600431F RID: 17183 RVA: 0x006CFA74 File Offset: 0x006CFA74
        [CompilerGenerated]
        private Chart(ClickyNoteFace.NoteBody.Chart original)
          : base(original)
        {
          this.Series = original.<Series>k__BackingField;
          this.ShowsTrend = original.<ShowsTrend>k__BackingField;
        }

        // Token: 0x06004320 RID: 17184 RVA: 0x006CFA98 File Offset: 0x006CFA98
        [CompilerGenerated]
        public void Deconstruct(out double[] Series, out bool ShowsTrend)
        {
          Series = this.Series;
          ShowsTrend = this.ShowsTrend;
        }
      }
    }

    // Token: 0x0200077B RID: 1915
    [Nullable(0)]
    public sealed class NoteLine : IEquatable<ClickyNoteFace.NoteLine>
    {
      // Token: 0x06003592 RID: 13714 RVA: 0x0067C8AC File Offset: 0x0067C8AC
      public NoteLine(string Text)
      {
        this.Text = Text;
        this.Mark = new ClickyNoteFace.LineMark.None();
        base..ctor();
      }

      // Token: 0x170007BB RID: 1979
      // (get) Token: 0x06003593 RID: 13715 RVA: 0x0067C8C8 File Offset: 0x0067C8C8
      [CompilerGenerated]
      private Type EqualityContract
      {
        [CompilerGenerated]
        get
        {
          return typeof(ClickyNoteFace.NoteLine);
        }
      }

      // Token: 0x170007BC RID: 1980
      // (get) Token: 0x06003594 RID: 13716 RVA: 0x0067C8D4 File Offset: 0x0067C8D4
      // (set) Token: 0x06003595 RID: 13717 RVA: 0x0067C8DC File Offset: 0x0067C8DC
      public string Text { get; set; }

      // Token: 0x170007BD RID: 1981
      // (get) Token: 0x06003596 RID: 13718 RVA: 0x0067C8E8 File Offset: 0x0067C8E8
      // (set) Token: 0x06003597 RID: 13719 RVA: 0x0067C8F0 File Offset: 0x0067C8F0
      public ClickyNoteFace.LineMark Mark { get; set; }

      // Token: 0x170007BE RID: 1982
      // (get) Token: 0x06003598 RID: 13720 RVA: 0x0067C8FC File Offset: 0x0067C8FC
      // (set) Token: 0x06003599 RID: 13721 RVA: 0x0067C904 File Offset: 0x0067C904
      public ClickyNoteFace.LineTone Tone { get; set; }

      // Token: 0x170007BF RID: 1983
      // (get) Token: 0x0600359A RID: 13722 RVA: 0x0067C910 File Offset: 0x0067C910
      // (set) Token: 0x0600359B RID: 13723 RVA: 0x0067C918 File Offset: 0x0067C918
      [Nullable(2)]
      public string Value
      {
        [NullableContext(2)]
        get;
        [NullableContext(2)]
        set;
      }

      // Token: 0x170007C0 RID: 1984
      // (get) Token: 0x0600359C RID: 13724 RVA: 0x0067C924 File Offset: 0x0067C924
      // (set) Token: 0x0600359D RID: 13725 RVA: 0x0067C92C File Offset: 0x0067C92C
      public bool IsCircled { get; set; }

      // Token: 0x170007C1 RID: 1985
      // (get) Token: 0x0600359E RID: 13726 RVA: 0x0067C938 File Offset: 0x0067C938
      // (set) Token: 0x0600359F RID: 13727 RVA: 0x0067C940 File Offset: 0x0067C940
      [Nullable(new byte[] { 2, 1 })]
      public ClickyNoteFace.LineRun[] Runs
      {
        [return: Nullable(new byte[] { 2, 1 })]
        get;
        [param: Nullable(new byte[] { 2, 1 })]
        set;
      }

      // Token: 0x170007C2 RID: 1986
      // (get) Token: 0x060035A0 RID: 13728 RVA: 0x0067C94C File Offset: 0x0067C94C
      // (set) Token: 0x060035A1 RID: 13729 RVA: 0x0067C954 File Offset: 0x0067C954
      public bool HasCaret { get; set; }

      // Token: 0x060035A2 RID: 13730 RVA: 0x0067C960 File Offset: 0x0067C960
      [CompilerGenerated]
      public override string ToString()
      {
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.Append("NoteLine");
        stringBuilder.Append(" { ");
        if (this.PrintMembers(stringBuilder))
        {
          stringBuilder.Append(' ');
        }
        stringBuilder.Append('}');
        return stringBuilder.ToString();
      }

      // Token: 0x060035A3 RID: 13731 RVA: 0x0067C9AC File Offset: 0x0067C9AC
      [CompilerGenerated]
      private bool PrintMembers(StringBuilder builder)
      {
        RuntimeHelpers.EnsureSufficientExecutionStack();
        builder.Append("Text = ");
        builder.Append(this.Text);
        builder.Append(", Mark = ");
        builder.Append(this.Mark);
        builder.Append(", Tone = ");
        builder.Append(this.Tone.ToString());
        builder.Append(", Value = ");
        builder.Append(this.Value);
        builder.Append(", IsCircled = ");
        builder.Append(this.IsCircled.ToString());
        builder.Append(", Runs = ");
        builder.Append(this.Runs);
        builder.Append(", HasCaret = ");
        builder.Append(this.HasCaret.ToString());
        return true;
      }

      // Token: 0x060035A4 RID: 13732 RVA: 0x0067CA98 File Offset: 0x0067CA98
      [NullableContext(2)]
      [CompilerGenerated]
      public static bool operator !=(ClickyNoteFace.NoteLine left, ClickyNoteFace.NoteLine right)
      {
        return !(left == right);
      }

      // Token: 0x060035A5 RID: 13733 RVA: 0x0067CAA4 File Offset: 0x0067CAA4
      [NullableContext(2)]
      [CompilerGenerated]
      public static bool operator ==(ClickyNoteFace.NoteLine left, ClickyNoteFace.NoteLine right)
      {
        return left == right || (left != null && left.Equals(right));
      }

      // Token: 0x060035A6 RID: 13734 RVA: 0x0067CAB8 File Offset: 0x0067CAB8
      [CompilerGenerated]
      public override int GetHashCode()
      {
        return ((((((EqualityComparer<Type>.Default.GetHashCode(this.EqualityContract) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Text>k__BackingField)) * -1521134295 + EqualityComparer<ClickyNoteFace.LineMark>.Default.GetHashCode(this.<Mark>k__BackingField)) * -1521134295 + EqualityComparer<ClickyNoteFace.LineTone>.Default.GetHashCode(this.<Tone>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Value>k__BackingField)) * -1521134295 + EqualityComparer<bool>.Default.GetHashCode(this.<IsCircled>k__BackingField)) * -1521134295 + EqualityComparer<ClickyNoteFace.LineRun[]>.Default.GetHashCode(this.<Runs>k__BackingField)) * -1521134295 + EqualityComparer<bool>.Default.GetHashCode(this.<HasCaret>k__BackingField);
      }

      // Token: 0x060035A7 RID: 13735 RVA: 0x0067CB78 File Offset: 0x0067CB78
      [NullableContext(2)]
      [CompilerGenerated]
      public override bool Equals(object obj)
      {
        return this.Equals(obj as ClickyNoteFace.NoteLine);
      }

      // Token: 0x060035A8 RID: 13736 RVA: 0x0067CB88 File Offset: 0x0067CB88
      [NullableContext(2)]
      [CompilerGenerated]
      public bool Equals(ClickyNoteFace.NoteLine other)
      {
        return this == other || (other != null && this.EqualityContract == other.EqualityContract && EqualityComparer<string>.Default.Equals(this.<Text>k__BackingField, other.<Text>k__BackingField) && EqualityComparer<ClickyNoteFace.LineMark>.Default.Equals(this.<Mark>k__BackingField, other.<Mark>k__BackingField) && EqualityComparer<ClickyNoteFace.LineTone>.Default.Equals(this.<Tone>k__BackingField, other.<Tone>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<Value>k__BackingField, other.<Value>k__BackingField) && EqualityComparer<bool>.Default.Equals(this.<IsCircled>k__BackingField, other.<IsCircled>k__BackingField) && EqualityComparer<ClickyNoteFace.LineRun[]>.Default.Equals(this.<Runs>k__BackingField, other.<Runs>k__BackingField) && EqualityComparer<bool>.Default.Equals(this.<HasCaret>k__BackingField, other.<HasCaret>k__BackingField));
      }

      // Token: 0x060035AA RID: 13738 RVA: 0x0067CC70 File Offset: 0x0067CC70
      [CompilerGenerated]
      private NoteLine(ClickyNoteFace.NoteLine original)
      {
        this.Text = original.<Text>k__BackingField;
        this.Mark = original.<Mark>k__BackingField;
        this.Tone = original.<Tone>k__BackingField;
        this.Value = original.<Value>k__BackingField;
        this.IsCircled = original.<IsCircled>k__BackingField;
        this.Runs = original.<Runs>k__BackingField;
        this.HasCaret = original.<HasCaret>k__BackingField;
      }

      // Token: 0x060035AB RID: 13739 RVA: 0x0067CCD8 File Offset: 0x0067CCD8
      [CompilerGenerated]
      public void Deconstruct(out string Text)
      {
        Text = this.Text;
      }
    }

    // Token: 0x0200077C RID: 1916
    [Nullable(0)]
    public abstract class LineMark : IEquatable<ClickyNoteFace.LineMark>
    {
      // Token: 0x170007C3 RID: 1987
      // (get) Token: 0x060035AC RID: 13740 RVA: 0x0067CCE4 File Offset: 0x0067CCE4
      [CompilerGenerated]
      protected virtual Type EqualityContract
      {
        [CompilerGenerated]
        get
        {
          return typeof(ClickyNoteFace.LineMark);
        }
      }

      // Token: 0x060035AD RID: 13741 RVA: 0x0067CCF0 File Offset: 0x0067CCF0
      [CompilerGenerated]
      public override string ToString()
      {
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.Append("LineMark");
        stringBuilder.Append(" { ");
        if (this.PrintMembers(stringBuilder))
        {
          stringBuilder.Append(' ');
        }
        stringBuilder.Append('}');
        return stringBuilder.ToString();
      }

      // Token: 0x060035AE RID: 13742 RVA: 0x0067CD3C File Offset: 0x0067CD3C
      [CompilerGenerated]
      protected virtual bool PrintMembers(StringBuilder builder)
      {
        return false;
      }

      // Token: 0x060035AF RID: 13743 RVA: 0x0067CD40 File Offset: 0x0067CD40
      [NullableContext(2)]
      [CompilerGenerated]
      public static bool operator !=(ClickyNoteFace.LineMark left, ClickyNoteFace.LineMark right)
      {
        return !(left == right);
      }

      // Token: 0x060035B0 RID: 13744 RVA: 0x0067CD4C File Offset: 0x0067CD4C
      [NullableContext(2)]
      [CompilerGenerated]
      public static bool operator ==(ClickyNoteFace.LineMark left, ClickyNoteFace.LineMark right)
      {
        return left == right || (left != null && left.Equals(right));
      }

      // Token: 0x060035B1 RID: 13745 RVA: 0x0067CD60 File Offset: 0x0067CD60
      [CompilerGenerated]
      public override int GetHashCode()
      {
        return EqualityComparer<Type>.Default.GetHashCode(this.EqualityContract);
      }

      // Token: 0x060035B2 RID: 13746 RVA: 0x0067CD74 File Offset: 0x0067CD74
      [NullableContext(2)]
      [CompilerGenerated]
      public override bool Equals(object obj)
      {
        return this.Equals(obj as ClickyNoteFace.LineMark);
      }

      // Token: 0x060035B3 RID: 13747 RVA: 0x0067CD84 File Offset: 0x0067CD84
      [NullableContext(2)]
      [CompilerGenerated]
      public virtual bool Equals(ClickyNoteFace.LineMark other)
      {
        return this == other || (other != null && this.EqualityContract == other.EqualityContract);
      }

      // Token: 0x060035B5 RID: 13749 RVA: 0x0067CDA4 File Offset: 0x0067CDA4
      [CompilerGenerated]
      protected LineMark(ClickyNoteFace.LineMark original)
      {
      }

      // Token: 0x060035B6 RID: 13750 RVA: 0x0067CDAC File Offset: 0x0067CDAC
      protected LineMark()
      {
      }

      // Token: 0x02000AF4 RID: 2804
      [Nullable(0)]
      public sealed class None : ClickyNoteFace.LineMark, IEquatable<ClickyNoteFace.LineMark.None>
      {
        // Token: 0x1700089A RID: 2202
        // (get) Token: 0x06004321 RID: 17185 RVA: 0x006CFAAC File Offset: 0x006CFAAC
        [CompilerGenerated]
        protected override Type EqualityContract
        {
          [CompilerGenerated]
          get
          {
            return typeof(ClickyNoteFace.LineMark.None);
          }
        }

        // Token: 0x06004322 RID: 17186 RVA: 0x006CFAB8 File Offset: 0x006CFAB8
        [CompilerGenerated]
        public override string ToString()
        {
          StringBuilder stringBuilder = new StringBuilder();
          stringBuilder.Append("None");
          stringBuilder.Append(" { ");
          if (this.PrintMembers(stringBuilder))
          {
            stringBuilder.Append(' ');
          }
          stringBuilder.Append('}');
          return stringBuilder.ToString();
        }

        // Token: 0x06004323 RID: 17187 RVA: 0x006CFB04 File Offset: 0x006CFB04
        [CompilerGenerated]
        protected override bool PrintMembers(StringBuilder builder)
        {
          return base.PrintMembers(builder);
        }

        // Token: 0x06004324 RID: 17188 RVA: 0x006CFB10 File Offset: 0x006CFB10
        [NullableContext(2)]
        [CompilerGenerated]
        public static bool operator !=(ClickyNoteFace.LineMark.None left, ClickyNoteFace.LineMark.None right)
        {
          return !(left == right);
        }

        // Token: 0x06004325 RID: 17189 RVA: 0x006CFB1C File Offset: 0x006CFB1C
        [NullableContext(2)]
        [CompilerGenerated]
        public static bool operator ==(ClickyNoteFace.LineMark.None left, ClickyNoteFace.LineMark.None right)
        {
          return left == right || (left != null && left.Equals(right));
        }

        // Token: 0x06004326 RID: 17190 RVA: 0x006CFB30 File Offset: 0x006CFB30
        [CompilerGenerated]
        public override int GetHashCode()
        {
          return base.GetHashCode();
        }

        // Token: 0x06004327 RID: 17191 RVA: 0x006CFB38 File Offset: 0x006CFB38
        [NullableContext(2)]
        [CompilerGenerated]
        public override bool Equals(object obj)
        {
          return this.Equals(obj as ClickyNoteFace.LineMark.None);
        }

        // Token: 0x06004328 RID: 17192 RVA: 0x006CFB48 File Offset: 0x006CFB48
        [NullableContext(2)]
        [CompilerGenerated]
        public sealed override bool Equals(ClickyNoteFace.LineMark other)
        {
          return this.Equals(other);
        }

        // Token: 0x06004329 RID: 17193 RVA: 0x006CFB54 File Offset: 0x006CFB54
        [NullableContext(2)]
        [CompilerGenerated]
        public bool Equals(ClickyNoteFace.LineMark.None other)
        {
          return this == other || base.Equals(other);
        }

        // Token: 0x0600432B RID: 17195 RVA: 0x006CFB6C File Offset: 0x006CFB6C
        [CompilerGenerated]
        private None(ClickyNoteFace.LineMark.None original)
          : base(original)
        {
        }

        // Token: 0x0600432C RID: 17196 RVA: 0x006CFB78 File Offset: 0x006CFB78
        public None()
        {
        }
      }

      // Token: 0x02000AF5 RID: 2805
      [Nullable(0)]
      public sealed class Done : ClickyNoteFace.LineMark, IEquatable<ClickyNoteFace.LineMark.Done>
      {
        // Token: 0x1700089B RID: 2203
        // (get) Token: 0x0600432D RID: 17197 RVA: 0x006CFB80 File Offset: 0x006CFB80
        [CompilerGenerated]
        protected override Type EqualityContract
        {
          [CompilerGenerated]
          get
          {
            return typeof(ClickyNoteFace.LineMark.Done);
          }
        }

        // Token: 0x0600432E RID: 17198 RVA: 0x006CFB8C File Offset: 0x006CFB8C
        [CompilerGenerated]
        public override string ToString()
        {
          StringBuilder stringBuilder = new StringBuilder();
          stringBuilder.Append("Done");
          stringBuilder.Append(" { ");
          if (this.PrintMembers(stringBuilder))
          {
            stringBuilder.Append(' ');
          }
          stringBuilder.Append('}');
          return stringBuilder.ToString();
        }

        // Token: 0x0600432F RID: 17199 RVA: 0x006CFBD8 File Offset: 0x006CFBD8
        [CompilerGenerated]
        protected override bool PrintMembers(StringBuilder builder)
        {
          return base.PrintMembers(builder);
        }

        // Token: 0x06004330 RID: 17200 RVA: 0x006CFBE4 File Offset: 0x006CFBE4
        [NullableContext(2)]
        [CompilerGenerated]
        public static bool operator !=(ClickyNoteFace.LineMark.Done left, ClickyNoteFace.LineMark.Done right)
        {
          return !(left == right);
        }

        // Token: 0x06004331 RID: 17201 RVA: 0x006CFBF0 File Offset: 0x006CFBF0
        [NullableContext(2)]
        [CompilerGenerated]
        public static bool operator ==(ClickyNoteFace.LineMark.Done left, ClickyNoteFace.LineMark.Done right)
        {
          return left == right || (left != null && left.Equals(right));
        }

        // Token: 0x06004332 RID: 17202 RVA: 0x006CFC04 File Offset: 0x006CFC04
        [CompilerGenerated]
        public override int GetHashCode()
        {
          return base.GetHashCode();
        }

        // Token: 0x06004333 RID: 17203 RVA: 0x006CFC0C File Offset: 0x006CFC0C
        [NullableContext(2)]
        [CompilerGenerated]
        public override bool Equals(object obj)
        {
          return this.Equals(obj as ClickyNoteFace.LineMark.Done);
        }

        // Token: 0x06004334 RID: 17204 RVA: 0x006CFC1C File Offset: 0x006CFC1C
        [NullableContext(2)]
        [CompilerGenerated]
        public sealed override bool Equals(ClickyNoteFace.LineMark other)
        {
          return this.Equals(other);
        }

        // Token: 0x06004335 RID: 17205 RVA: 0x006CFC28 File Offset: 0x006CFC28
        [NullableContext(2)]
        [CompilerGenerated]
        public bool Equals(ClickyNoteFace.LineMark.Done other)
        {
          return this == other || base.Equals(other);
        }

        // Token: 0x06004337 RID: 17207 RVA: 0x006CFC40 File Offset: 0x006CFC40
        [CompilerGenerated]
        private Done(ClickyNoteFace.LineMark.Done original)
          : base(original)
        {
        }

        // Token: 0x06004338 RID: 17208 RVA: 0x006CFC4C File Offset: 0x006CFC4C
        public Done()
        {
        }
      }

      // Token: 0x02000AF6 RID: 2806
      [Nullable(0)]
      public sealed class Now : ClickyNoteFace.LineMark, IEquatable<ClickyNoteFace.LineMark.Now>
      {
        // Token: 0x1700089C RID: 2204
        // (get) Token: 0x06004339 RID: 17209 RVA: 0x006CFC54 File Offset: 0x006CFC54
        [CompilerGenerated]
        protected override Type EqualityContract
        {
          [CompilerGenerated]
          get
          {
            return typeof(ClickyNoteFace.LineMark.Now);
          }
        }

        // Token: 0x0600433A RID: 17210 RVA: 0x006CFC60 File Offset: 0x006CFC60
        [CompilerGenerated]
        public override string ToString()
        {
          StringBuilder stringBuilder = new StringBuilder();
          stringBuilder.Append("Now");
          stringBuilder.Append(" { ");
          if (this.PrintMembers(stringBuilder))
          {
            stringBuilder.Append(' ');
          }
          stringBuilder.Append('}');
          return stringBuilder.ToString();
        }

        // Token: 0x0600433B RID: 17211 RVA: 0x006CFCAC File Offset: 0x006CFCAC
        [CompilerGenerated]
        protected override bool PrintMembers(StringBuilder builder)
        {
          return base.PrintMembers(builder);
        }

        // Token: 0x0600433C RID: 17212 RVA: 0x006CFCB8 File Offset: 0x006CFCB8
        [NullableContext(2)]
        [CompilerGenerated]
        public static bool operator !=(ClickyNoteFace.LineMark.Now left, ClickyNoteFace.LineMark.Now right)
        {
          return !(left == right);
        }

        // Token: 0x0600433D RID: 17213 RVA: 0x006CFCC4 File Offset: 0x006CFCC4
        [NullableContext(2)]
        [CompilerGenerated]
        public static bool operator ==(ClickyNoteFace.LineMark.Now left, ClickyNoteFace.LineMark.Now right)
        {
          return left == right || (left != null && left.Equals(right));
        }

        // Token: 0x0600433E RID: 17214 RVA: 0x006CFCD8 File Offset: 0x006CFCD8
        [CompilerGenerated]
        public override int GetHashCode()
        {
          return base.GetHashCode();
        }

        // Token: 0x0600433F RID: 17215 RVA: 0x006CFCE0 File Offset: 0x006CFCE0
        [NullableContext(2)]
        [CompilerGenerated]
        public override bool Equals(object obj)
        {
          return this.Equals(obj as ClickyNoteFace.LineMark.Now);
        }

        // Token: 0x06004340 RID: 17216 RVA: 0x006CFCF0 File Offset: 0x006CFCF0
        [NullableContext(2)]
        [CompilerGenerated]
        public sealed override bool Equals(ClickyNoteFace.LineMark other)
        {
          return this.Equals(other);
        }

        // Token: 0x06004341 RID: 17217 RVA: 0x006CFCFC File Offset: 0x006CFCFC
        [NullableContext(2)]
        [CompilerGenerated]
        public bool Equals(ClickyNoteFace.LineMark.Now other)
        {
          return this == other || base.Equals(other);
        }

        // Token: 0x06004343 RID: 17219 RVA: 0x006CFD14 File Offset: 0x006CFD14
        [CompilerGenerated]
        private Now(ClickyNoteFace.LineMark.Now original)
          : base(original)
        {
        }

        // Token: 0x06004344 RID: 17220 RVA: 0x006CFD20 File Offset: 0x006CFD20
        public Now()
        {
        }
      }

      // Token: 0x02000AF7 RID: 2807
      [Nullable(0)]
      public sealed class Todo : ClickyNoteFace.LineMark, IEquatable<ClickyNoteFace.LineMark.Todo>
      {
        // Token: 0x1700089D RID: 2205
        // (get) Token: 0x06004345 RID: 17221 RVA: 0x006CFD28 File Offset: 0x006CFD28
        [CompilerGenerated]
        protected override Type EqualityContract
        {
          [CompilerGenerated]
          get
          {
            return typeof(ClickyNoteFace.LineMark.Todo);
          }
        }

        // Token: 0x06004346 RID: 17222 RVA: 0x006CFD34 File Offset: 0x006CFD34
        [CompilerGenerated]
        public override string ToString()
        {
          StringBuilder stringBuilder = new StringBuilder();
          stringBuilder.Append("Todo");
          stringBuilder.Append(" { ");
          if (this.PrintMembers(stringBuilder))
          {
            stringBuilder.Append(' ');
          }
          stringBuilder.Append('}');
          return stringBuilder.ToString();
        }

        // Token: 0x06004347 RID: 17223 RVA: 0x006CFD80 File Offset: 0x006CFD80
        [CompilerGenerated]
        protected override bool PrintMembers(StringBuilder builder)
        {
          return base.PrintMembers(builder);
        }

        // Token: 0x06004348 RID: 17224 RVA: 0x006CFD8C File Offset: 0x006CFD8C
        [NullableContext(2)]
        [CompilerGenerated]
        public static bool operator !=(ClickyNoteFace.LineMark.Todo left, ClickyNoteFace.LineMark.Todo right)
        {
          return !(left == right);
        }

        // Token: 0x06004349 RID: 17225 RVA: 0x006CFD98 File Offset: 0x006CFD98
        [NullableContext(2)]
        [CompilerGenerated]
        public static bool operator ==(ClickyNoteFace.LineMark.Todo left, ClickyNoteFace.LineMark.Todo right)
        {
          return left == right || (left != null && left.Equals(right));
        }

        // Token: 0x0600434A RID: 17226 RVA: 0x006CFDAC File Offset: 0x006CFDAC
        [CompilerGenerated]
        public override int GetHashCode()
        {
          return base.GetHashCode();
        }

        // Token: 0x0600434B RID: 17227 RVA: 0x006CFDB4 File Offset: 0x006CFDB4
        [NullableContext(2)]
        [CompilerGenerated]
        public override bool Equals(object obj)
        {
          return this.Equals(obj as ClickyNoteFace.LineMark.Todo);
        }

        // Token: 0x0600434C RID: 17228 RVA: 0x006CFDC4 File Offset: 0x006CFDC4
        [NullableContext(2)]
        [CompilerGenerated]
        public sealed override bool Equals(ClickyNoteFace.LineMark other)
        {
          return this.Equals(other);
        }

        // Token: 0x0600434D RID: 17229 RVA: 0x006CFDD0 File Offset: 0x006CFDD0
        [NullableContext(2)]
        [CompilerGenerated]
        public bool Equals(ClickyNoteFace.LineMark.Todo other)
        {
          return this == other || base.Equals(other);
        }

        // Token: 0x0600434F RID: 17231 RVA: 0x006CFDE8 File Offset: 0x006CFDE8
        [CompilerGenerated]
        private Todo(ClickyNoteFace.LineMark.Todo original)
          : base(original)
        {
        }

        // Token: 0x06004350 RID: 17232 RVA: 0x006CFDF4 File Offset: 0x006CFDF4
        public Todo()
        {
        }
      }

      // Token: 0x02000AF8 RID: 2808
      [Nullable(0)]
      public sealed class Number : ClickyNoteFace.LineMark, IEquatable<ClickyNoteFace.LineMark.Number>
      {
        // Token: 0x06004351 RID: 17233 RVA: 0x006CFDFC File Offset: 0x006CFDFC
        public Number(int Value)
        {
          this.Value = Value;
          base..ctor();
        }

        // Token: 0x1700089E RID: 2206
        // (get) Token: 0x06004352 RID: 17234 RVA: 0x006CFE0C File Offset: 0x006CFE0C
        [CompilerGenerated]
        protected override Type EqualityContract
        {
          [CompilerGenerated]
          get
          {
            return typeof(ClickyNoteFace.LineMark.Number);
          }
        }

        // Token: 0x1700089F RID: 2207
        // (get) Token: 0x06004353 RID: 17235 RVA: 0x006CFE18 File Offset: 0x006CFE18
        // (set) Token: 0x06004354 RID: 17236 RVA: 0x006CFE20 File Offset: 0x006CFE20
        public int Value { get; set; }

        // Token: 0x06004355 RID: 17237 RVA: 0x006CFE2C File Offset: 0x006CFE2C
        [CompilerGenerated]
        public override string ToString()
        {
          StringBuilder stringBuilder = new StringBuilder();
          stringBuilder.Append("Number");
          stringBuilder.Append(" { ");
          if (this.PrintMembers(stringBuilder))
          {
            stringBuilder.Append(' ');
          }
          stringBuilder.Append('}');
          return stringBuilder.ToString();
        }

        // Token: 0x06004356 RID: 17238 RVA: 0x006CFE78 File Offset: 0x006CFE78
        [CompilerGenerated]
        protected override bool PrintMembers(StringBuilder builder)
        {
          if (base.PrintMembers(builder))
          {
            builder.Append(", ");
          }
          builder.Append("Value = ");
          builder.Append(this.Value.ToString());
          return true;
        }

        // Token: 0x06004357 RID: 17239 RVA: 0x006CFEC4 File Offset: 0x006CFEC4
        [NullableContext(2)]
        [CompilerGenerated]
        public static bool operator !=(ClickyNoteFace.LineMark.Number left, ClickyNoteFace.LineMark.Number right)
        {
          return !(left == right);
        }

        // Token: 0x06004358 RID: 17240 RVA: 0x006CFED0 File Offset: 0x006CFED0
        [NullableContext(2)]
        [CompilerGenerated]
        public static bool operator ==(ClickyNoteFace.LineMark.Number left, ClickyNoteFace.LineMark.Number right)
        {
          return left == right || (left != null && left.Equals(right));
        }

        // Token: 0x06004359 RID: 17241 RVA: 0x006CFEE4 File Offset: 0x006CFEE4
        [CompilerGenerated]
        public override int GetHashCode()
        {
          return base.GetHashCode() * -1521134295 + EqualityComparer<int>.Default.GetHashCode(this.<Value>k__BackingField);
        }

        // Token: 0x0600435A RID: 17242 RVA: 0x006CFF04 File Offset: 0x006CFF04
        [NullableContext(2)]
        [CompilerGenerated]
        public override bool Equals(object obj)
        {
          return this.Equals(obj as ClickyNoteFace.LineMark.Number);
        }

        // Token: 0x0600435B RID: 17243 RVA: 0x006CFF14 File Offset: 0x006CFF14
        [NullableContext(2)]
        [CompilerGenerated]
        public sealed override bool Equals(ClickyNoteFace.LineMark other)
        {
          return this.Equals(other);
        }

        // Token: 0x0600435C RID: 17244 RVA: 0x006CFF20 File Offset: 0x006CFF20
        [NullableContext(2)]
        [CompilerGenerated]
        public bool Equals(ClickyNoteFace.LineMark.Number other)
        {
          return this == other || (base.Equals(other) && EqualityComparer<int>.Default.Equals(this.<Value>k__BackingField, other.<Value>k__BackingField));
        }

        // Token: 0x0600435E RID: 17246 RVA: 0x006CFF54 File Offset: 0x006CFF54
        [CompilerGenerated]
        private Number(ClickyNoteFace.LineMark.Number original)
          : base(original)
        {
          this.Value = original.<Value>k__BackingField;
        }

        // Token: 0x0600435F RID: 17247 RVA: 0x006CFF6C File Offset: 0x006CFF6C
        [CompilerGenerated]
        public void Deconstruct(out int Value)
        {
          Value = this.Value;
        }
      }
    }

    // Token: 0x0200077D RID: 1917
    [NullableContext(0)]
    public enum LineTone
    {
      // Token: 0x040020C3 RID: 8387
      Ink,
      // Token: 0x040020C4 RID: 8388
      Clicky,
      // Token: 0x040020C5 RID: 8389
      Quiet
    }

    // Token: 0x0200077E RID: 1918
    [Nullable(0)]
    public sealed class LineRun : IEquatable<ClickyNoteFace.LineRun>
    {
      // Token: 0x060035B7 RID: 13751 RVA: 0x0067CDB4 File Offset: 0x0067CDB4
      public LineRun(string Text, bool IsClicky)
      {
        this.Text = Text;
        this.IsClicky = IsClicky;
        base..ctor();
      }

      // Token: 0x170007C4 RID: 1988
      // (get) Token: 0x060035B8 RID: 13752 RVA: 0x0067CDCC File Offset: 0x0067CDCC
      [CompilerGenerated]
      private Type EqualityContract
      {
        [CompilerGenerated]
        get
        {
          return typeof(ClickyNoteFace.LineRun);
        }
      }

      // Token: 0x170007C5 RID: 1989
      // (get) Token: 0x060035B9 RID: 13753 RVA: 0x0067CDD8 File Offset: 0x0067CDD8
      // (set) Token: 0x060035BA RID: 13754 RVA: 0x0067CDE0 File Offset: 0x0067CDE0
      public string Text { get; set; }

      // Token: 0x170007C6 RID: 1990
      // (get) Token: 0x060035BB RID: 13755 RVA: 0x0067CDEC File Offset: 0x0067CDEC
      // (set) Token: 0x060035BC RID: 13756 RVA: 0x0067CDF4 File Offset: 0x0067CDF4
      public bool IsClicky { get; set; }

      // Token: 0x060035BD RID: 13757 RVA: 0x0067CE00 File Offset: 0x0067CE00
      [CompilerGenerated]
      public override string ToString()
      {
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.Append("LineRun");
        stringBuilder.Append(" { ");
        if (this.PrintMembers(stringBuilder))
        {
          stringBuilder.Append(' ');
        }
        stringBuilder.Append('}');
        return stringBuilder.ToString();
      }

      // Token: 0x060035BE RID: 13758 RVA: 0x0067CE4C File Offset: 0x0067CE4C
      [CompilerGenerated]
      private bool PrintMembers(StringBuilder builder)
      {
        RuntimeHelpers.EnsureSufficientExecutionStack();
        builder.Append("Text = ");
        builder.Append(this.Text);
        builder.Append(", IsClicky = ");
        builder.Append(this.IsClicky.ToString());
        return true;
      }

      // Token: 0x060035BF RID: 13759 RVA: 0x0067CEA0 File Offset: 0x0067CEA0
      [NullableContext(2)]
      [CompilerGenerated]
      public static bool operator !=(ClickyNoteFace.LineRun left, ClickyNoteFace.LineRun right)
      {
        return !(left == right);
      }

      // Token: 0x060035C0 RID: 13760 RVA: 0x0067CEAC File Offset: 0x0067CEAC
      [NullableContext(2)]
      [CompilerGenerated]
      public static bool operator ==(ClickyNoteFace.LineRun left, ClickyNoteFace.LineRun right)
      {
        return left == right || (left != null && left.Equals(right));
      }

      // Token: 0x060035C1 RID: 13761 RVA: 0x0067CEC0 File Offset: 0x0067CEC0
      [CompilerGenerated]
      public override int GetHashCode()
      {
        return (EqualityComparer<Type>.Default.GetHashCode(this.EqualityContract) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Text>k__BackingField)) * -1521134295 + EqualityComparer<bool>.Default.GetHashCode(this.<IsClicky>k__BackingField);
      }

      // Token: 0x060035C2 RID: 13762 RVA: 0x0067CF00 File Offset: 0x0067CF00
      [NullableContext(2)]
      [CompilerGenerated]
      public override bool Equals(object obj)
      {
        return this.Equals(obj as ClickyNoteFace.LineRun);
      }

      // Token: 0x060035C3 RID: 13763 RVA: 0x0067CF10 File Offset: 0x0067CF10
      [NullableContext(2)]
      [CompilerGenerated]
      public bool Equals(ClickyNoteFace.LineRun other)
      {
        return this == other || (other != null && this.EqualityContract == other.EqualityContract && EqualityComparer<string>.Default.Equals(this.<Text>k__BackingField, other.<Text>k__BackingField) && EqualityComparer<bool>.Default.Equals(this.<IsClicky>k__BackingField, other.<IsClicky>k__BackingField));
      }

      // Token: 0x060035C5 RID: 13765 RVA: 0x0067CF74 File Offset: 0x0067CF74
      [CompilerGenerated]
      private LineRun(ClickyNoteFace.LineRun original)
      {
        this.Text = original.<Text>k__BackingField;
        this.IsClicky = original.<IsClicky>k__BackingField;
      }

      // Token: 0x060035C6 RID: 13766 RVA: 0x0067CF94 File Offset: 0x0067CF94
      [CompilerGenerated]
      public void Deconstruct(out string Text, out bool IsClicky)
      {
        Text = this.Text;
        IsClicky = this.IsClicky;
      }
    }

    // Token: 0x0200077F RID: 1919
    [Nullable(0)]
    public abstract class NoteFooter : IEquatable<ClickyNoteFace.NoteFooter>
    {
      // Token: 0x170007C7 RID: 1991
      // (get) Token: 0x060035C7 RID: 13767 RVA: 0x0067CFA8 File Offset: 0x0067CFA8
      [CompilerGenerated]
      protected virtual Type EqualityContract
      {
        [CompilerGenerated]
        get
        {
          return typeof(ClickyNoteFace.NoteFooter);
        }
      }

      // Token: 0x060035C8 RID: 13768 RVA: 0x0067CFB4 File Offset: 0x0067CFB4
      [CompilerGenerated]
      public override string ToString()
      {
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.Append("NoteFooter");
        stringBuilder.Append(" { ");
        if (this.PrintMembers(stringBuilder))
        {
          stringBuilder.Append(' ');
        }
        stringBuilder.Append('}');
        return stringBuilder.ToString();
      }

      // Token: 0x060035C9 RID: 13769 RVA: 0x0067D000 File Offset: 0x0067D000
      [CompilerGenerated]
      protected virtual bool PrintMembers(StringBuilder builder)
      {
        return false;
      }

      // Token: 0x060035CA RID: 13770 RVA: 0x0067D004 File Offset: 0x0067D004
      [NullableContext(2)]
      [CompilerGenerated]
      public static bool operator !=(ClickyNoteFace.NoteFooter left, ClickyNoteFace.NoteFooter right)
      {
        return !(left == right);
      }

      // Token: 0x060035CB RID: 13771 RVA: 0x0067D010 File Offset: 0x0067D010
      [NullableContext(2)]
      [CompilerGenerated]
      public static bool operator ==(ClickyNoteFace.NoteFooter left, ClickyNoteFace.NoteFooter right)
      {
        return left == right || (left != null && left.Equals(right));
      }

      // Token: 0x060035CC RID: 13772 RVA: 0x0067D024 File Offset: 0x0067D024
      [CompilerGenerated]
      public override int GetHashCode()
      {
        return EqualityComparer<Type>.Default.GetHashCode(this.EqualityContract);
      }

      // Token: 0x060035CD RID: 13773 RVA: 0x0067D038 File Offset: 0x0067D038
      [NullableContext(2)]
      [CompilerGenerated]
      public override bool Equals(object obj)
      {
        return this.Equals(obj as ClickyNoteFace.NoteFooter);
      }

      // Token: 0x060035CE RID: 13774 RVA: 0x0067D048 File Offset: 0x0067D048
      [NullableContext(2)]
      [CompilerGenerated]
      public virtual bool Equals(ClickyNoteFace.NoteFooter other)
      {
        return this == other || (other != null && this.EqualityContract == other.EqualityContract);
      }

      // Token: 0x060035D0 RID: 13776 RVA: 0x0067D068 File Offset: 0x0067D068
      [CompilerGenerated]
      protected NoteFooter(ClickyNoteFace.NoteFooter original)
      {
      }

      // Token: 0x060035D1 RID: 13777 RVA: 0x0067D070 File Offset: 0x0067D070
      protected NoteFooter()
      {
      }

      // Token: 0x02000AF9 RID: 2809
      [Nullable(0)]
      public sealed class None : ClickyNoteFace.NoteFooter, IEquatable<ClickyNoteFace.NoteFooter.None>
      {
        // Token: 0x170008A0 RID: 2208
        // (get) Token: 0x06004360 RID: 17248 RVA: 0x006CFF78 File Offset: 0x006CFF78
        [CompilerGenerated]
        protected override Type EqualityContract
        {
          [CompilerGenerated]
          get
          {
            return typeof(ClickyNoteFace.NoteFooter.None);
          }
        }

        // Token: 0x06004361 RID: 17249 RVA: 0x006CFF84 File Offset: 0x006CFF84
        [CompilerGenerated]
        public override string ToString()
        {
          StringBuilder stringBuilder = new StringBuilder();
          stringBuilder.Append("None");
          stringBuilder.Append(" { ");
          if (this.PrintMembers(stringBuilder))
          {
            stringBuilder.Append(' ');
          }
          stringBuilder.Append('}');
          return stringBuilder.ToString();
        }

        // Token: 0x06004362 RID: 17250 RVA: 0x006CFFD0 File Offset: 0x006CFFD0
        [CompilerGenerated]
        protected override bool PrintMembers(StringBuilder builder)
        {
          return base.PrintMembers(builder);
        }

        // Token: 0x06004363 RID: 17251 RVA: 0x006CFFDC File Offset: 0x006CFFDC
        [NullableContext(2)]
        [CompilerGenerated]
        public static bool operator !=(ClickyNoteFace.NoteFooter.None left, ClickyNoteFace.NoteFooter.None right)
        {
          return !(left == right);
        }

        // Token: 0x06004364 RID: 17252 RVA: 0x006CFFE8 File Offset: 0x006CFFE8
        [NullableContext(2)]
        [CompilerGenerated]
        public static bool operator ==(ClickyNoteFace.NoteFooter.None left, ClickyNoteFace.NoteFooter.None right)
        {
          return left == right || (left != null && left.Equals(right));
        }

        // Token: 0x06004365 RID: 17253 RVA: 0x006CFFFC File Offset: 0x006CFFFC
        [CompilerGenerated]
        public override int GetHashCode()
        {
          return base.GetHashCode();
        }

        // Token: 0x06004366 RID: 17254 RVA: 0x006D0004 File Offset: 0x006D0004
        [NullableContext(2)]
        [CompilerGenerated]
        public override bool Equals(object obj)
        {
          return this.Equals(obj as ClickyNoteFace.NoteFooter.None);
        }

        // Token: 0x06004367 RID: 17255 RVA: 0x006D0014 File Offset: 0x006D0014
        [NullableContext(2)]
        [CompilerGenerated]
        public sealed override bool Equals(ClickyNoteFace.NoteFooter other)
        {
          return this.Equals(other);
        }

        // Token: 0x06004368 RID: 17256 RVA: 0x006D0020 File Offset: 0x006D0020
        [NullableContext(2)]
        [CompilerGenerated]
        public bool Equals(ClickyNoteFace.NoteFooter.None other)
        {
          return this == other || base.Equals(other);
        }

        // Token: 0x0600436A RID: 17258 RVA: 0x006D0038 File Offset: 0x006D0038
        [CompilerGenerated]
        private None(ClickyNoteFace.NoteFooter.None original)
          : base(original)
        {
        }

        // Token: 0x0600436B RID: 17259 RVA: 0x006D0044 File Offset: 0x006D0044
        public None()
        {
        }
      }

      // Token: 0x02000AFA RID: 2810
      [Nullable(0)]
      public sealed class Actions : ClickyNoteFace.NoteFooter, IEquatable<ClickyNoteFace.NoteFooter.Actions>
      {
        // Token: 0x0600436C RID: 17260 RVA: 0x006D004C File Offset: 0x006D004C
        public Actions(ClickyNoteFace.NoteAction[] Items)
        {
          this.Items = Items;
          base..ctor();
        }

        // Token: 0x170008A1 RID: 2209
        // (get) Token: 0x0600436D RID: 17261 RVA: 0x006D005C File Offset: 0x006D005C
        [CompilerGenerated]
        protected override Type EqualityContract
        {
          [CompilerGenerated]
          get
          {
            return typeof(ClickyNoteFace.NoteFooter.Actions);
          }
        }

        // Token: 0x170008A2 RID: 2210
        // (get) Token: 0x0600436E RID: 17262 RVA: 0x006D0068 File Offset: 0x006D0068
        // (set) Token: 0x0600436F RID: 17263 RVA: 0x006D0070 File Offset: 0x006D0070
        public ClickyNoteFace.NoteAction[] Items { get; set; }

        // Token: 0x06004370 RID: 17264 RVA: 0x006D007C File Offset: 0x006D007C
        [CompilerGenerated]
        public override string ToString()
        {
          StringBuilder stringBuilder = new StringBuilder();
          stringBuilder.Append("Actions");
          stringBuilder.Append(" { ");
          if (this.PrintMembers(stringBuilder))
          {
            stringBuilder.Append(' ');
          }
          stringBuilder.Append('}');
          return stringBuilder.ToString();
        }

        // Token: 0x06004371 RID: 17265 RVA: 0x006D00C8 File Offset: 0x006D00C8
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

        // Token: 0x06004372 RID: 17266 RVA: 0x006D00FC File Offset: 0x006D00FC
        [NullableContext(2)]
        [CompilerGenerated]
        public static bool operator !=(ClickyNoteFace.NoteFooter.Actions left, ClickyNoteFace.NoteFooter.Actions right)
        {
          return !(left == right);
        }

        // Token: 0x06004373 RID: 17267 RVA: 0x006D0108 File Offset: 0x006D0108
        [NullableContext(2)]
        [CompilerGenerated]
        public static bool operator ==(ClickyNoteFace.NoteFooter.Actions left, ClickyNoteFace.NoteFooter.Actions right)
        {
          return left == right || (left != null && left.Equals(right));
        }

        // Token: 0x06004374 RID: 17268 RVA: 0x006D011C File Offset: 0x006D011C
        [CompilerGenerated]
        public override int GetHashCode()
        {
          return base.GetHashCode() * -1521134295 + EqualityComparer<ClickyNoteFace.NoteAction[]>.Default.GetHashCode(this.<Items>k__BackingField);
        }

        // Token: 0x06004375 RID: 17269 RVA: 0x006D013C File Offset: 0x006D013C
        [NullableContext(2)]
        [CompilerGenerated]
        public override bool Equals(object obj)
        {
          return this.Equals(obj as ClickyNoteFace.NoteFooter.Actions);
        }

        // Token: 0x06004376 RID: 17270 RVA: 0x006D014C File Offset: 0x006D014C
        [NullableContext(2)]
        [CompilerGenerated]
        public sealed override bool Equals(ClickyNoteFace.NoteFooter other)
        {
          return this.Equals(other);
        }

        // Token: 0x06004377 RID: 17271 RVA: 0x006D0158 File Offset: 0x006D0158
        [NullableContext(2)]
        [CompilerGenerated]
        public bool Equals(ClickyNoteFace.NoteFooter.Actions other)
        {
          return this == other || (base.Equals(other) && EqualityComparer<ClickyNoteFace.NoteAction[]>.Default.Equals(this.<Items>k__BackingField, other.<Items>k__BackingField));
        }

        // Token: 0x06004379 RID: 17273 RVA: 0x006D018C File Offset: 0x006D018C
        [CompilerGenerated]
        private Actions(ClickyNoteFace.NoteFooter.Actions original)
          : base(original)
        {
          this.Items = original.<Items>k__BackingField;
        }

        // Token: 0x0600437A RID: 17274 RVA: 0x006D01A4 File Offset: 0x006D01A4
        [CompilerGenerated]
        public void Deconstruct(out ClickyNoteFace.NoteAction[] Items)
        {
          Items = this.Items;
        }
      }

      // Token: 0x02000AFB RID: 2811
      [Nullable(0)]
      public sealed class Files : ClickyNoteFace.NoteFooter, IEquatable<ClickyNoteFace.NoteFooter.Files>
      {
        // Token: 0x0600437B RID: 17275 RVA: 0x006D01B0 File Offset: 0x006D01B0
        public Files(string[] Names)
        {
          this.Names = Names;
          base..ctor();
        }

        // Token: 0x170008A3 RID: 2211
        // (get) Token: 0x0600437C RID: 17276 RVA: 0x006D01C0 File Offset: 0x006D01C0
        [CompilerGenerated]
        protected override Type EqualityContract
        {
          [CompilerGenerated]
          get
          {
            return typeof(ClickyNoteFace.NoteFooter.Files);
          }
        }

        // Token: 0x170008A4 RID: 2212
        // (get) Token: 0x0600437D RID: 17277 RVA: 0x006D01CC File Offset: 0x006D01CC
        // (set) Token: 0x0600437E RID: 17278 RVA: 0x006D01D4 File Offset: 0x006D01D4
        public string[] Names { get; set; }

        // Token: 0x0600437F RID: 17279 RVA: 0x006D01E0 File Offset: 0x006D01E0
        [CompilerGenerated]
        public override string ToString()
        {
          StringBuilder stringBuilder = new StringBuilder();
          stringBuilder.Append("Files");
          stringBuilder.Append(" { ");
          if (this.PrintMembers(stringBuilder))
          {
            stringBuilder.Append(' ');
          }
          stringBuilder.Append('}');
          return stringBuilder.ToString();
        }

        // Token: 0x06004380 RID: 17280 RVA: 0x006D022C File Offset: 0x006D022C
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

        // Token: 0x06004381 RID: 17281 RVA: 0x006D0260 File Offset: 0x006D0260
        [NullableContext(2)]
        [CompilerGenerated]
        public static bool operator !=(ClickyNoteFace.NoteFooter.Files left, ClickyNoteFace.NoteFooter.Files right)
        {
          return !(left == right);
        }

        // Token: 0x06004382 RID: 17282 RVA: 0x006D026C File Offset: 0x006D026C
        [NullableContext(2)]
        [CompilerGenerated]
        public static bool operator ==(ClickyNoteFace.NoteFooter.Files left, ClickyNoteFace.NoteFooter.Files right)
        {
          return left == right || (left != null && left.Equals(right));
        }

        // Token: 0x06004383 RID: 17283 RVA: 0x006D0280 File Offset: 0x006D0280
        [CompilerGenerated]
        public override int GetHashCode()
        {
          return base.GetHashCode() * -1521134295 + EqualityComparer<string[]>.Default.GetHashCode(this.<Names>k__BackingField);
        }

        // Token: 0x06004384 RID: 17284 RVA: 0x006D02A0 File Offset: 0x006D02A0
        [NullableContext(2)]
        [CompilerGenerated]
        public override bool Equals(object obj)
        {
          return this.Equals(obj as ClickyNoteFace.NoteFooter.Files);
        }

        // Token: 0x06004385 RID: 17285 RVA: 0x006D02B0 File Offset: 0x006D02B0
        [NullableContext(2)]
        [CompilerGenerated]
        public sealed override bool Equals(ClickyNoteFace.NoteFooter other)
        {
          return this.Equals(other);
        }

        // Token: 0x06004386 RID: 17286 RVA: 0x006D02BC File Offset: 0x006D02BC
        [NullableContext(2)]
        [CompilerGenerated]
        public bool Equals(ClickyNoteFace.NoteFooter.Files other)
        {
          return this == other || (base.Equals(other) && EqualityComparer<string[]>.Default.Equals(this.<Names>k__BackingField, other.<Names>k__BackingField));
        }

        // Token: 0x06004388 RID: 17288 RVA: 0x006D02F0 File Offset: 0x006D02F0
        [CompilerGenerated]
        private Files(ClickyNoteFace.NoteFooter.Files original)
          : base(original)
        {
          this.Names = original.<Names>k__BackingField;
        }

        // Token: 0x06004389 RID: 17289 RVA: 0x006D0308 File Offset: 0x006D0308
        [CompilerGenerated]
        public void Deconstruct(out string[] Names)
        {
          Names = this.Names;
        }
      }

      // Token: 0x02000AFC RID: 2812
      [Nullable(0)]
      public sealed class Progress : ClickyNoteFace.NoteFooter, IEquatable<ClickyNoteFace.NoteFooter.Progress>
      {
        // Token: 0x0600438A RID: 17290 RVA: 0x006D0314 File Offset: 0x006D0314
        public Progress(int Step, int Total, double Fraction)
        {
          this.Step = Step;
          this.Total = Total;
          this.Fraction = Fraction;
          base..ctor();
        }

        // Token: 0x170008A5 RID: 2213
        // (get) Token: 0x0600438B RID: 17291 RVA: 0x006D0334 File Offset: 0x006D0334
        [CompilerGenerated]
        protected override Type EqualityContract
        {
          [CompilerGenerated]
          get
          {
            return typeof(ClickyNoteFace.NoteFooter.Progress);
          }
        }

        // Token: 0x170008A6 RID: 2214
        // (get) Token: 0x0600438C RID: 17292 RVA: 0x006D0340 File Offset: 0x006D0340
        // (set) Token: 0x0600438D RID: 17293 RVA: 0x006D0348 File Offset: 0x006D0348
        public int Step { get; set; }

        // Token: 0x170008A7 RID: 2215
        // (get) Token: 0x0600438E RID: 17294 RVA: 0x006D0354 File Offset: 0x006D0354
        // (set) Token: 0x0600438F RID: 17295 RVA: 0x006D035C File Offset: 0x006D035C
        public int Total { get; set; }

        // Token: 0x170008A8 RID: 2216
        // (get) Token: 0x06004390 RID: 17296 RVA: 0x006D0368 File Offset: 0x006D0368
        // (set) Token: 0x06004391 RID: 17297 RVA: 0x006D0370 File Offset: 0x006D0370
        public double Fraction { get; set; }

        // Token: 0x06004392 RID: 17298 RVA: 0x006D037C File Offset: 0x006D037C
        [CompilerGenerated]
        public override string ToString()
        {
          StringBuilder stringBuilder = new StringBuilder();
          stringBuilder.Append("Progress");
          stringBuilder.Append(" { ");
          if (this.PrintMembers(stringBuilder))
          {
            stringBuilder.Append(' ');
          }
          stringBuilder.Append('}');
          return stringBuilder.ToString();
        }

        // Token: 0x06004393 RID: 17299 RVA: 0x006D03C8 File Offset: 0x006D03C8
        [CompilerGenerated]
        protected override bool PrintMembers(StringBuilder builder)
        {
          if (base.PrintMembers(builder))
          {
            builder.Append(", ");
          }
          builder.Append("Step = ");
          builder.Append(this.Step.ToString());
          builder.Append(", Total = ");
          builder.Append(this.Total.ToString());
          builder.Append(", Fraction = ");
          builder.Append(this.Fraction.ToString());
          return true;
        }

        // Token: 0x06004394 RID: 17300 RVA: 0x006D0460 File Offset: 0x006D0460
        [NullableContext(2)]
        [CompilerGenerated]
        public static bool operator !=(ClickyNoteFace.NoteFooter.Progress left, ClickyNoteFace.NoteFooter.Progress right)
        {
          return !(left == right);
        }

        // Token: 0x06004395 RID: 17301 RVA: 0x006D046C File Offset: 0x006D046C
        [NullableContext(2)]
        [CompilerGenerated]
        public static bool operator ==(ClickyNoteFace.NoteFooter.Progress left, ClickyNoteFace.NoteFooter.Progress right)
        {
          return left == right || (left != null && left.Equals(right));
        }

        // Token: 0x06004396 RID: 17302 RVA: 0x006D0480 File Offset: 0x006D0480
        [CompilerGenerated]
        public override int GetHashCode()
        {
          return ((base.GetHashCode() * -1521134295 + EqualityComparer<int>.Default.GetHashCode(this.<Step>k__BackingField)) * -1521134295 + EqualityComparer<int>.Default.GetHashCode(this.<Total>k__BackingField)) * -1521134295 + EqualityComparer<double>.Default.GetHashCode(this.<Fraction>k__BackingField);
        }

        // Token: 0x06004397 RID: 17303 RVA: 0x006D04D8 File Offset: 0x006D04D8
        [NullableContext(2)]
        [CompilerGenerated]
        public override bool Equals(object obj)
        {
          return this.Equals(obj as ClickyNoteFace.NoteFooter.Progress);
        }

        // Token: 0x06004398 RID: 17304 RVA: 0x006D04E8 File Offset: 0x006D04E8
        [NullableContext(2)]
        [CompilerGenerated]
        public sealed override bool Equals(ClickyNoteFace.NoteFooter other)
        {
          return this.Equals(other);
        }

        // Token: 0x06004399 RID: 17305 RVA: 0x006D04F4 File Offset: 0x006D04F4
        [NullableContext(2)]
        [CompilerGenerated]
        public bool Equals(ClickyNoteFace.NoteFooter.Progress other)
        {
          return this == other || (base.Equals(other) && EqualityComparer<int>.Default.Equals(this.<Step>k__BackingField, other.<Step>k__BackingField) && EqualityComparer<int>.Default.Equals(this.<Total>k__BackingField, other.<Total>k__BackingField) && EqualityComparer<double>.Default.Equals(this.<Fraction>k__BackingField, other.<Fraction>k__BackingField));
        }

        // Token: 0x0600439B RID: 17307 RVA: 0x006D0560 File Offset: 0x006D0560
        [CompilerGenerated]
        private Progress(ClickyNoteFace.NoteFooter.Progress original)
          : base(original)
        {
          this.Step = original.<Step>k__BackingField;
          this.Total = original.<Total>k__BackingField;
          this.Fraction = original.<Fraction>k__BackingField;
        }

        // Token: 0x0600439C RID: 17308 RVA: 0x006D0590 File Offset: 0x006D0590
        [CompilerGenerated]
        public void Deconstruct(out int Step, out int Total, out double Fraction)
        {
          Step = this.Step;
          Total = this.Total;
          Fraction = this.Fraction;
        }
      }

      // Token: 0x02000AFD RID: 2813
      [Nullable(0)]
      public sealed class Text : ClickyNoteFace.NoteFooter, IEquatable<ClickyNoteFace.NoteFooter.Text>
      {
        // Token: 0x0600439D RID: 17309 RVA: 0x006D05AC File Offset: 0x006D05AC
        public Text(string Words)
        {
          this.Words = Words;
          base..ctor();
        }

        // Token: 0x170008A9 RID: 2217
        // (get) Token: 0x0600439E RID: 17310 RVA: 0x006D05BC File Offset: 0x006D05BC
        [CompilerGenerated]
        protected override Type EqualityContract
        {
          [CompilerGenerated]
          get
          {
            return typeof(ClickyNoteFace.NoteFooter.Text);
          }
        }

        // Token: 0x170008AA RID: 2218
        // (get) Token: 0x0600439F RID: 17311 RVA: 0x006D05C8 File Offset: 0x006D05C8
        // (set) Token: 0x060043A0 RID: 17312 RVA: 0x006D05D0 File Offset: 0x006D05D0
        public string Words { get; set; }

        // Token: 0x060043A1 RID: 17313 RVA: 0x006D05DC File Offset: 0x006D05DC
        [CompilerGenerated]
        public override string ToString()
        {
          StringBuilder stringBuilder = new StringBuilder();
          stringBuilder.Append("Text");
          stringBuilder.Append(" { ");
          if (this.PrintMembers(stringBuilder))
          {
            stringBuilder.Append(' ');
          }
          stringBuilder.Append('}');
          return stringBuilder.ToString();
        }

        // Token: 0x060043A2 RID: 17314 RVA: 0x006D0628 File Offset: 0x006D0628
        [CompilerGenerated]
        protected override bool PrintMembers(StringBuilder builder)
        {
          if (base.PrintMembers(builder))
          {
            builder.Append(", ");
          }
          builder.Append("Words = ");
          builder.Append(this.Words);
          return true;
        }

        // Token: 0x060043A3 RID: 17315 RVA: 0x006D065C File Offset: 0x006D065C
        [NullableContext(2)]
        [CompilerGenerated]
        public static bool operator !=(ClickyNoteFace.NoteFooter.Text left, ClickyNoteFace.NoteFooter.Text right)
        {
          return !(left == right);
        }

        // Token: 0x060043A4 RID: 17316 RVA: 0x006D0668 File Offset: 0x006D0668
        [NullableContext(2)]
        [CompilerGenerated]
        public static bool operator ==(ClickyNoteFace.NoteFooter.Text left, ClickyNoteFace.NoteFooter.Text right)
        {
          return left == right || (left != null && left.Equals(right));
        }

        // Token: 0x060043A5 RID: 17317 RVA: 0x006D067C File Offset: 0x006D067C
        [CompilerGenerated]
        public override int GetHashCode()
        {
          return base.GetHashCode() * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Words>k__BackingField);
        }

        // Token: 0x060043A6 RID: 17318 RVA: 0x006D069C File Offset: 0x006D069C
        [NullableContext(2)]
        [CompilerGenerated]
        public override bool Equals(object obj)
        {
          return this.Equals(obj as ClickyNoteFace.NoteFooter.Text);
        }

        // Token: 0x060043A7 RID: 17319 RVA: 0x006D06AC File Offset: 0x006D06AC
        [NullableContext(2)]
        [CompilerGenerated]
        public sealed override bool Equals(ClickyNoteFace.NoteFooter other)
        {
          return this.Equals(other);
        }

        // Token: 0x060043A8 RID: 17320 RVA: 0x006D06B8 File Offset: 0x006D06B8
        [NullableContext(2)]
        [CompilerGenerated]
        public bool Equals(ClickyNoteFace.NoteFooter.Text other)
        {
          return this == other || (base.Equals(other) && EqualityComparer<string>.Default.Equals(this.<Words>k__BackingField, other.<Words>k__BackingField));
        }

        // Token: 0x060043AA RID: 17322 RVA: 0x006D06EC File Offset: 0x006D06EC
        [CompilerGenerated]
        private Text(ClickyNoteFace.NoteFooter.Text original)
          : base(original)
        {
          this.Words = original.<Words>k__BackingField;
        }

        // Token: 0x060043AB RID: 17323 RVA: 0x006D0704 File Offset: 0x006D0704
        [CompilerGenerated]
        public void Deconstruct(out string Words)
        {
          Words = this.Words;
        }
      }
    }

    // Token: 0x02000780 RID: 1920
    [Nullable(0)]
    public sealed class NoteAction : IEquatable<ClickyNoteFace.NoteAction>
    {
      // Token: 0x060035D2 RID: 13778 RVA: 0x0067D078 File Offset: 0x0067D078
      public NoteAction(string Id, string Title, ClickyPaperGel Gel = ClickyPaperGel.Blue)
      {
        this.Id = Id;
        this.Title = Title;
        this.Gel = Gel;
        base..ctor();
      }

      // Token: 0x170007C8 RID: 1992
      // (get) Token: 0x060035D3 RID: 13779 RVA: 0x0067D098 File Offset: 0x0067D098
      [CompilerGenerated]
      private Type EqualityContract
      {
        [CompilerGenerated]
        get
        {
          return typeof(ClickyNoteFace.NoteAction);
        }
      }

      // Token: 0x170007C9 RID: 1993
      // (get) Token: 0x060035D4 RID: 13780 RVA: 0x0067D0A4 File Offset: 0x0067D0A4
      // (set) Token: 0x060035D5 RID: 13781 RVA: 0x0067D0AC File Offset: 0x0067D0AC
      public string Id { get; set; }

      // Token: 0x170007CA RID: 1994
      // (get) Token: 0x060035D6 RID: 13782 RVA: 0x0067D0B8 File Offset: 0x0067D0B8
      // (set) Token: 0x060035D7 RID: 13783 RVA: 0x0067D0C0 File Offset: 0x0067D0C0
      public string Title { get; set; }

      // Token: 0x170007CB RID: 1995
      // (get) Token: 0x060035D8 RID: 13784 RVA: 0x0067D0CC File Offset: 0x0067D0CC
      // (set) Token: 0x060035D9 RID: 13785 RVA: 0x0067D0D4 File Offset: 0x0067D0D4
      public ClickyPaperGel Gel { get; set; }

      // Token: 0x060035DA RID: 13786 RVA: 0x0067D0E0 File Offset: 0x0067D0E0
      [CompilerGenerated]
      public override string ToString()
      {
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.Append("NoteAction");
        stringBuilder.Append(" { ");
        if (this.PrintMembers(stringBuilder))
        {
          stringBuilder.Append(' ');
        }
        stringBuilder.Append('}');
        return stringBuilder.ToString();
      }

      // Token: 0x060035DB RID: 13787 RVA: 0x0067D12C File Offset: 0x0067D12C
      [CompilerGenerated]
      private bool PrintMembers(StringBuilder builder)
      {
        RuntimeHelpers.EnsureSufficientExecutionStack();
        builder.Append("Id = ");
        builder.Append(this.Id);
        builder.Append(", Title = ");
        builder.Append(this.Title);
        builder.Append(", Gel = ");
        builder.Append(this.Gel.ToString());
        return true;
      }

      // Token: 0x060035DC RID: 13788 RVA: 0x0067D198 File Offset: 0x0067D198
      [NullableContext(2)]
      [CompilerGenerated]
      public static bool operator !=(ClickyNoteFace.NoteAction left, ClickyNoteFace.NoteAction right)
      {
        return !(left == right);
      }

      // Token: 0x060035DD RID: 13789 RVA: 0x0067D1A4 File Offset: 0x0067D1A4
      [NullableContext(2)]
      [CompilerGenerated]
      public static bool operator ==(ClickyNoteFace.NoteAction left, ClickyNoteFace.NoteAction right)
      {
        return left == right || (left != null && left.Equals(right));
      }

      // Token: 0x060035DE RID: 13790 RVA: 0x0067D1B8 File Offset: 0x0067D1B8
      [CompilerGenerated]
      public override int GetHashCode()
      {
        return ((EqualityComparer<Type>.Default.GetHashCode(this.EqualityContract) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Id>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Title>k__BackingField)) * -1521134295 + EqualityComparer<ClickyPaperGel>.Default.GetHashCode(this.<Gel>k__BackingField);
      }

      // Token: 0x060035DF RID: 13791 RVA: 0x0067D21C File Offset: 0x0067D21C
      [NullableContext(2)]
      [CompilerGenerated]
      public override bool Equals(object obj)
      {
        return this.Equals(obj as ClickyNoteFace.NoteAction);
      }

      // Token: 0x060035E0 RID: 13792 RVA: 0x0067D22C File Offset: 0x0067D22C
      [NullableContext(2)]
      [CompilerGenerated]
      public bool Equals(ClickyNoteFace.NoteAction other)
      {
        return this == other || (other != null && this.EqualityContract == other.EqualityContract && EqualityComparer<string>.Default.Equals(this.<Id>k__BackingField, other.<Id>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<Title>k__BackingField, other.<Title>k__BackingField) && EqualityComparer<ClickyPaperGel>.Default.Equals(this.<Gel>k__BackingField, other.<Gel>k__BackingField));
      }

      // Token: 0x060035E2 RID: 13794 RVA: 0x0067D2A8 File Offset: 0x0067D2A8
      [CompilerGenerated]
      private NoteAction(ClickyNoteFace.NoteAction original)
      {
        this.Id = original.<Id>k__BackingField;
        this.Title = original.<Title>k__BackingField;
        this.Gel = original.<Gel>k__BackingField;
      }

      // Token: 0x060035E3 RID: 13795 RVA: 0x0067D2D4 File Offset: 0x0067D2D4
      [CompilerGenerated]
      public void Deconstruct(out string Id, out string Title, out ClickyPaperGel Gel)
      {
        Id = this.Id;
        Title = this.Title;
        Gel = this.Gel;
      }
    }
  }
}
