using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

namespace HeyClicky.Core.Proactive
{
  // Token: 0x020000B8 RID: 184
  [NullableContext(1)]
  [Nullable(0)]
  public abstract class RemoteTaskReportBlock : IEquatable<RemoteTaskReportBlock>
  {
    // Token: 0x170002A8 RID: 680
    // (get) Token: 0x06000C26 RID: 3110 RVA: 0x0037C660 File Offset: 0x0037C660
    [CompilerGenerated]
    protected virtual Type EqualityContract
    {
      [CompilerGenerated]
      get
      {
        return typeof(RemoteTaskReportBlock);
      }
    }

    // Token: 0x06000C27 RID: 3111 RVA: 0x0037C66C File Offset: 0x0037C66C
    public static IReadOnlyList<RemoteTaskReportBlock> Parse(string summary)
    {
      RemoteTaskReportBlock.<>c__DisplayClass5_0 CS$<>8__locals1;
      CS$<>8__locals1.blocks = new List<RemoteTaskReportBlock>();
      CS$<>8__locals1.current = null;
      CS$<>8__locals1.paragraph = new List<string>();
      string[] array = summary.Replace("\r", "").Split('\n', StringSplitOptions.None);
      for (int i = 0; i < array.Length; i++)
      {
        string text = array[i].TrimEnd();
        string text2 = text.TrimStart();
        if (text2.Length == 0)
        {
          RemoteTaskReportBlock.<Parse>g__FlushParagraph|5_0(ref CS$<>8__locals1);
          RemoteTaskReportBlock.<Parse>g__FlushBullet|5_1(ref CS$<>8__locals1);
          if (CS$<>8__locals1.blocks.Count > 0)
          {
            List<RemoteTaskReportBlock> blocks = CS$<>8__locals1.blocks;
            if (!(blocks[blocks.Count - 1] is RemoteTaskReportBlock.Spacer))
            {
              CS$<>8__locals1.blocks.Add(new RemoteTaskReportBlock.Spacer());
            }
          }
        }
        else
        {
          bool flag = text2.StartsWith("- ") || text2.StartsWith("* ") || text2.StartsWith("• ");
          bool flag2 = text.Length - text2.Length >= 2;
          if (flag && !flag2)
          {
            RemoteTaskReportBlock.<Parse>g__FlushParagraph|5_0(ref CS$<>8__locals1);
            RemoteTaskReportBlock.<Parse>g__FlushBullet|5_1(ref CS$<>8__locals1);
            string text3 = text2;
            CS$<>8__locals1.current = new RemoteTaskReportBlock.Bullet(text3.Substring(2, text3.Length - 2).Trim(), Array.Empty<string>());
          }
          else if (CS$<>8__locals1.current != null)
          {
            RemoteTaskReportBlock.Bullet bullet = CS$<>8__locals1.current.<Clone>$();
            IEnumerable<string> detailLines = CS$<>8__locals1.current.DetailLines;
            string text4;
            if (!flag)
            {
              text4 = text2;
            }
            else
            {
              string text3 = text2;
              text4 = text3.Substring(2, text3.Length - 2).Trim();
            }
            bullet.DetailLines = detailLines.Append(text4).ToList<string>();
            CS$<>8__locals1.current = bullet;
          }
          else
          {
            CS$<>8__locals1.paragraph.Add(text2);
          }
        }
      }
      RemoteTaskReportBlock.<Parse>g__FlushParagraph|5_0(ref CS$<>8__locals1);
      RemoteTaskReportBlock.<Parse>g__FlushBullet|5_1(ref CS$<>8__locals1);
      while (CS$<>8__locals1.blocks.Count > 0)
      {
        List<RemoteTaskReportBlock> blocks2 = CS$<>8__locals1.blocks;
        if (!(blocks2[blocks2.Count - 1] is RemoteTaskReportBlock.Spacer))
        {
          break;
        }
        CS$<>8__locals1.blocks.RemoveAt(CS$<>8__locals1.blocks.Count - 1);
      }
      return CS$<>8__locals1.blocks;
    }

    // Token: 0x06000C28 RID: 3112 RVA: 0x0037C874 File Offset: 0x0037C874
    [CompilerGenerated]
    public override string ToString()
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("RemoteTaskReportBlock");
      stringBuilder.Append(" { ");
      if (this.PrintMembers(stringBuilder))
      {
        stringBuilder.Append(' ');
      }
      stringBuilder.Append('}');
      return stringBuilder.ToString();
    }

    // Token: 0x06000C29 RID: 3113 RVA: 0x0037C8C0 File Offset: 0x0037C8C0
    [CompilerGenerated]
    protected virtual bool PrintMembers(StringBuilder builder)
    {
      return false;
    }

    // Token: 0x06000C2A RID: 3114 RVA: 0x0037C8C4 File Offset: 0x0037C8C4
    [NullableContext(2)]
    [CompilerGenerated]
    public static bool operator !=(RemoteTaskReportBlock left, RemoteTaskReportBlock right)
    {
      return !(left == right);
    }

    // Token: 0x06000C2B RID: 3115 RVA: 0x0037C8D0 File Offset: 0x0037C8D0
    [NullableContext(2)]
    [CompilerGenerated]
    public static bool operator ==(RemoteTaskReportBlock left, RemoteTaskReportBlock right)
    {
      return left == right || (left != null && left.Equals(right));
    }

    // Token: 0x06000C2C RID: 3116 RVA: 0x0037C8E4 File Offset: 0x0037C8E4
    [CompilerGenerated]
    public override int GetHashCode()
    {
      return EqualityComparer<Type>.Default.GetHashCode(this.EqualityContract);
    }

    // Token: 0x06000C2D RID: 3117 RVA: 0x0037C8F8 File Offset: 0x0037C8F8
    [NullableContext(2)]
    [CompilerGenerated]
    public override bool Equals(object obj)
    {
      return this.Equals(obj as RemoteTaskReportBlock);
    }

    // Token: 0x06000C2E RID: 3118 RVA: 0x0037C908 File Offset: 0x0037C908
    [NullableContext(2)]
    [CompilerGenerated]
    public virtual bool Equals(RemoteTaskReportBlock other)
    {
      return this == other || (other != null && this.EqualityContract == other.EqualityContract);
    }

    // Token: 0x06000C30 RID: 3120 RVA: 0x0037C928 File Offset: 0x0037C928
    [CompilerGenerated]
    protected RemoteTaskReportBlock(RemoteTaskReportBlock original)
    {
    }

    // Token: 0x06000C31 RID: 3121 RVA: 0x0037C930 File Offset: 0x0037C930
    protected RemoteTaskReportBlock()
    {
    }

    // Token: 0x06000C32 RID: 3122 RVA: 0x0037C938 File Offset: 0x0037C938
    [CompilerGenerated]
    internal static void <Parse>g__FlushParagraph|5_0(ref RemoteTaskReportBlock.<>c__DisplayClass5_0 A_0)
    {
      if (A_0.paragraph.Count > 0)
      {
        A_0.blocks.Add(new RemoteTaskReportBlock.Paragraph(string.Join(" ", A_0.paragraph)));
        A_0.paragraph.Clear();
      }
    }

    // Token: 0x06000C33 RID: 3123 RVA: 0x0037C974 File Offset: 0x0037C974
    [CompilerGenerated]
    internal static void <Parse>g__FlushBullet|5_1(ref RemoteTaskReportBlock.<>c__DisplayClass5_0 A_0)
    {
      if (A_0.current != null)
      {
        A_0.blocks.Add(A_0.current);
        A_0.current = null;
      }
    }

    // Token: 0x020003C1 RID: 961
    [Nullable(0)]
    public sealed class Bullet : RemoteTaskReportBlock, IEquatable<RemoteTaskReportBlock.Bullet>
    {
      // Token: 0x06002A31 RID: 10801 RVA: 0x004258B8 File Offset: 0x004258B8
      public Bullet(string Title, IReadOnlyList<string> DetailLines)
      {
        this.Title = Title;
        this.DetailLines = DetailLines;
        base..ctor();
      }

      // Token: 0x1700081C RID: 2076
      // (get) Token: 0x06002A32 RID: 10802 RVA: 0x004258D0 File Offset: 0x004258D0
      [CompilerGenerated]
      protected override Type EqualityContract
      {
        [CompilerGenerated]
        get
        {
          return typeof(RemoteTaskReportBlock.Bullet);
        }
      }

      // Token: 0x1700081D RID: 2077
      // (get) Token: 0x06002A33 RID: 10803 RVA: 0x004258DC File Offset: 0x004258DC
      // (set) Token: 0x06002A34 RID: 10804 RVA: 0x004258E4 File Offset: 0x004258E4
      public string Title { get; set; }

      // Token: 0x1700081E RID: 2078
      // (get) Token: 0x06002A35 RID: 10805 RVA: 0x004258F0 File Offset: 0x004258F0
      // (set) Token: 0x06002A36 RID: 10806 RVA: 0x004258F8 File Offset: 0x004258F8
      public IReadOnlyList<string> DetailLines { get; set; }

      // Token: 0x06002A37 RID: 10807 RVA: 0x00425904 File Offset: 0x00425904
      [CompilerGenerated]
      public override string ToString()
      {
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.Append("Bullet");
        stringBuilder.Append(" { ");
        if (this.PrintMembers(stringBuilder))
        {
          stringBuilder.Append(' ');
        }
        stringBuilder.Append('}');
        return stringBuilder.ToString();
      }

      // Token: 0x06002A38 RID: 10808 RVA: 0x00425950 File Offset: 0x00425950
      [CompilerGenerated]
      protected override bool PrintMembers(StringBuilder builder)
      {
        if (base.PrintMembers(builder))
        {
          builder.Append(", ");
        }
        builder.Append("Title = ");
        builder.Append(this.Title);
        builder.Append(", DetailLines = ");
        builder.Append(this.DetailLines);
        return true;
      }

      // Token: 0x06002A39 RID: 10809 RVA: 0x004259A8 File Offset: 0x004259A8
      [NullableContext(2)]
      [CompilerGenerated]
      public static bool operator !=(RemoteTaskReportBlock.Bullet left, RemoteTaskReportBlock.Bullet right)
      {
        return !(left == right);
      }

      // Token: 0x06002A3A RID: 10810 RVA: 0x004259B4 File Offset: 0x004259B4
      [NullableContext(2)]
      [CompilerGenerated]
      public static bool operator ==(RemoteTaskReportBlock.Bullet left, RemoteTaskReportBlock.Bullet right)
      {
        return left == right || (left != null && left.Equals(right));
      }

      // Token: 0x06002A3B RID: 10811 RVA: 0x004259C8 File Offset: 0x004259C8
      [CompilerGenerated]
      public override int GetHashCode()
      {
        return (base.GetHashCode() * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Title>k__BackingField)) * -1521134295 + EqualityComparer<IReadOnlyList<string>>.Default.GetHashCode(this.<DetailLines>k__BackingField);
      }

      // Token: 0x06002A3C RID: 10812 RVA: 0x00425A00 File Offset: 0x00425A00
      [NullableContext(2)]
      [CompilerGenerated]
      public override bool Equals(object obj)
      {
        return this.Equals(obj as RemoteTaskReportBlock.Bullet);
      }

      // Token: 0x06002A3D RID: 10813 RVA: 0x00425A10 File Offset: 0x00425A10
      [NullableContext(2)]
      [CompilerGenerated]
      public sealed override bool Equals(RemoteTaskReportBlock other)
      {
        return this.Equals(other);
      }

      // Token: 0x06002A3E RID: 10814 RVA: 0x00425A1C File Offset: 0x00425A1C
      [NullableContext(2)]
      [CompilerGenerated]
      public bool Equals(RemoteTaskReportBlock.Bullet other)
      {
        return this == other || (base.Equals(other) && EqualityComparer<string>.Default.Equals(this.<Title>k__BackingField, other.<Title>k__BackingField) && EqualityComparer<IReadOnlyList<string>>.Default.Equals(this.<DetailLines>k__BackingField, other.<DetailLines>k__BackingField));
      }

      // Token: 0x06002A40 RID: 10816 RVA: 0x00425A70 File Offset: 0x00425A70
      [CompilerGenerated]
      private Bullet(RemoteTaskReportBlock.Bullet original)
        : base(original)
      {
        this.Title = original.<Title>k__BackingField;
        this.DetailLines = original.<DetailLines>k__BackingField;
      }

      // Token: 0x06002A41 RID: 10817 RVA: 0x00425A94 File Offset: 0x00425A94
      [CompilerGenerated]
      public void Deconstruct(out string Title, out IReadOnlyList<string> DetailLines)
      {
        Title = this.Title;
        DetailLines = this.DetailLines;
      }
    }

    // Token: 0x020003C2 RID: 962
    [Nullable(0)]
    public sealed class Paragraph : RemoteTaskReportBlock, IEquatable<RemoteTaskReportBlock.Paragraph>
    {
      // Token: 0x06002A42 RID: 10818 RVA: 0x00425AA8 File Offset: 0x00425AA8
      public Paragraph(string Text)
      {
        this.Text = Text;
        base..ctor();
      }

      // Token: 0x1700081F RID: 2079
      // (get) Token: 0x06002A43 RID: 10819 RVA: 0x00425AB8 File Offset: 0x00425AB8
      [CompilerGenerated]
      protected override Type EqualityContract
      {
        [CompilerGenerated]
        get
        {
          return typeof(RemoteTaskReportBlock.Paragraph);
        }
      }

      // Token: 0x17000820 RID: 2080
      // (get) Token: 0x06002A44 RID: 10820 RVA: 0x00425AC4 File Offset: 0x00425AC4
      // (set) Token: 0x06002A45 RID: 10821 RVA: 0x00425ACC File Offset: 0x00425ACC
      public string Text { get; set; }

      // Token: 0x06002A46 RID: 10822 RVA: 0x00425AD8 File Offset: 0x00425AD8
      [CompilerGenerated]
      public override string ToString()
      {
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.Append("Paragraph");
        stringBuilder.Append(" { ");
        if (this.PrintMembers(stringBuilder))
        {
          stringBuilder.Append(' ');
        }
        stringBuilder.Append('}');
        return stringBuilder.ToString();
      }

      // Token: 0x06002A47 RID: 10823 RVA: 0x00425B24 File Offset: 0x00425B24
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

      // Token: 0x06002A48 RID: 10824 RVA: 0x00425B58 File Offset: 0x00425B58
      [NullableContext(2)]
      [CompilerGenerated]
      public static bool operator !=(RemoteTaskReportBlock.Paragraph left, RemoteTaskReportBlock.Paragraph right)
      {
        return !(left == right);
      }

      // Token: 0x06002A49 RID: 10825 RVA: 0x00425B64 File Offset: 0x00425B64
      [NullableContext(2)]
      [CompilerGenerated]
      public static bool operator ==(RemoteTaskReportBlock.Paragraph left, RemoteTaskReportBlock.Paragraph right)
      {
        return left == right || (left != null && left.Equals(right));
      }

      // Token: 0x06002A4A RID: 10826 RVA: 0x00425B78 File Offset: 0x00425B78
      [CompilerGenerated]
      public override int GetHashCode()
      {
        return base.GetHashCode() * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Text>k__BackingField);
      }

      // Token: 0x06002A4B RID: 10827 RVA: 0x00425B98 File Offset: 0x00425B98
      [NullableContext(2)]
      [CompilerGenerated]
      public override bool Equals(object obj)
      {
        return this.Equals(obj as RemoteTaskReportBlock.Paragraph);
      }

      // Token: 0x06002A4C RID: 10828 RVA: 0x00425BA8 File Offset: 0x00425BA8
      [NullableContext(2)]
      [CompilerGenerated]
      public sealed override bool Equals(RemoteTaskReportBlock other)
      {
        return this.Equals(other);
      }

      // Token: 0x06002A4D RID: 10829 RVA: 0x00425BB4 File Offset: 0x00425BB4
      [NullableContext(2)]
      [CompilerGenerated]
      public bool Equals(RemoteTaskReportBlock.Paragraph other)
      {
        return this == other || (base.Equals(other) && EqualityComparer<string>.Default.Equals(this.<Text>k__BackingField, other.<Text>k__BackingField));
      }

      // Token: 0x06002A4F RID: 10831 RVA: 0x00425BE8 File Offset: 0x00425BE8
      [CompilerGenerated]
      private Paragraph(RemoteTaskReportBlock.Paragraph original)
        : base(original)
      {
        this.Text = original.<Text>k__BackingField;
      }

      // Token: 0x06002A50 RID: 10832 RVA: 0x00425C00 File Offset: 0x00425C00
      [CompilerGenerated]
      public void Deconstruct(out string Text)
      {
        Text = this.Text;
      }
    }

    // Token: 0x020003C3 RID: 963
    [Nullable(0)]
    public sealed class Spacer : RemoteTaskReportBlock, IEquatable<RemoteTaskReportBlock.Spacer>
    {
      // Token: 0x17000821 RID: 2081
      // (get) Token: 0x06002A51 RID: 10833 RVA: 0x00425C0C File Offset: 0x00425C0C
      [CompilerGenerated]
      protected override Type EqualityContract
      {
        [CompilerGenerated]
        get
        {
          return typeof(RemoteTaskReportBlock.Spacer);
        }
      }

      // Token: 0x06002A52 RID: 10834 RVA: 0x00425C18 File Offset: 0x00425C18
      [CompilerGenerated]
      public override string ToString()
      {
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.Append("Spacer");
        stringBuilder.Append(" { ");
        if (this.PrintMembers(stringBuilder))
        {
          stringBuilder.Append(' ');
        }
        stringBuilder.Append('}');
        return stringBuilder.ToString();
      }

      // Token: 0x06002A53 RID: 10835 RVA: 0x00425C64 File Offset: 0x00425C64
      [CompilerGenerated]
      protected override bool PrintMembers(StringBuilder builder)
      {
        return base.PrintMembers(builder);
      }

      // Token: 0x06002A54 RID: 10836 RVA: 0x00425C70 File Offset: 0x00425C70
      [NullableContext(2)]
      [CompilerGenerated]
      public static bool operator !=(RemoteTaskReportBlock.Spacer left, RemoteTaskReportBlock.Spacer right)
      {
        return !(left == right);
      }

      // Token: 0x06002A55 RID: 10837 RVA: 0x00425C7C File Offset: 0x00425C7C
      [NullableContext(2)]
      [CompilerGenerated]
      public static bool operator ==(RemoteTaskReportBlock.Spacer left, RemoteTaskReportBlock.Spacer right)
      {
        return left == right || (left != null && left.Equals(right));
      }

      // Token: 0x06002A56 RID: 10838 RVA: 0x00425C90 File Offset: 0x00425C90
      [CompilerGenerated]
      public override int GetHashCode()
      {
        return base.GetHashCode();
      }

      // Token: 0x06002A57 RID: 10839 RVA: 0x00425C98 File Offset: 0x00425C98
      [NullableContext(2)]
      [CompilerGenerated]
      public override bool Equals(object obj)
      {
        return this.Equals(obj as RemoteTaskReportBlock.Spacer);
      }

      // Token: 0x06002A58 RID: 10840 RVA: 0x00425CA8 File Offset: 0x00425CA8
      [NullableContext(2)]
      [CompilerGenerated]
      public sealed override bool Equals(RemoteTaskReportBlock other)
      {
        return this.Equals(other);
      }

      // Token: 0x06002A59 RID: 10841 RVA: 0x00425CB4 File Offset: 0x00425CB4
      [NullableContext(2)]
      [CompilerGenerated]
      public bool Equals(RemoteTaskReportBlock.Spacer other)
      {
        return this == other || base.Equals(other);
      }

      // Token: 0x06002A5B RID: 10843 RVA: 0x00425CCC File Offset: 0x00425CCC
      [CompilerGenerated]
      private Spacer(RemoteTaskReportBlock.Spacer original)
        : base(original)
      {
      }

      // Token: 0x06002A5C RID: 10844 RVA: 0x00425CD8 File Offset: 0x00425CD8
      public Spacer()
      {
      }
    }
  }
}
