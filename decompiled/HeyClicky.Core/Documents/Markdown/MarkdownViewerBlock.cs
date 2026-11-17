using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace HeyClicky.Core.Documents.Markdown
{
  // Token: 0x020001BA RID: 442
  [NullableContext(1)]
  [Nullable(0)]
  public abstract class MarkdownViewerBlock : IEquatable<MarkdownViewerBlock>
  {
    // Token: 0x170005AE RID: 1454
    // (get) Token: 0x0600199A RID: 6554 RVA: 0x003D347C File Offset: 0x003D347C
    [CompilerGenerated]
    protected virtual Type EqualityContract
    {
      [CompilerGenerated]
      get
      {
        return typeof(MarkdownViewerBlock);
      }
    }

    // Token: 0x0600199B RID: 6555 RVA: 0x003D3488 File Offset: 0x003D3488
    [CompilerGenerated]
    public override string ToString()
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("MarkdownViewerBlock");
      stringBuilder.Append(" { ");
      if (this.PrintMembers(stringBuilder))
      {
        stringBuilder.Append(' ');
      }
      stringBuilder.Append('}');
      return stringBuilder.ToString();
    }

    // Token: 0x0600199C RID: 6556 RVA: 0x003D34D4 File Offset: 0x003D34D4
    [CompilerGenerated]
    protected virtual bool PrintMembers(StringBuilder builder)
    {
      return false;
    }

    // Token: 0x0600199D RID: 6557 RVA: 0x003D34D8 File Offset: 0x003D34D8
    [NullableContext(2)]
    [CompilerGenerated]
    public static bool operator !=(MarkdownViewerBlock left, MarkdownViewerBlock right)
    {
      return !(left == right);
    }

    // Token: 0x0600199E RID: 6558 RVA: 0x003D34E4 File Offset: 0x003D34E4
    [NullableContext(2)]
    [CompilerGenerated]
    public static bool operator ==(MarkdownViewerBlock left, MarkdownViewerBlock right)
    {
      return left == right || (left != null && left.Equals(right));
    }

    // Token: 0x0600199F RID: 6559 RVA: 0x003D34F8 File Offset: 0x003D34F8
    [CompilerGenerated]
    public override int GetHashCode()
    {
      return EqualityComparer<Type>.Default.GetHashCode(this.EqualityContract);
    }

    // Token: 0x060019A0 RID: 6560 RVA: 0x003D350C File Offset: 0x003D350C
    [NullableContext(2)]
    [CompilerGenerated]
    public override bool Equals(object obj)
    {
      return this.Equals(obj as MarkdownViewerBlock);
    }

    // Token: 0x060019A1 RID: 6561 RVA: 0x003D351C File Offset: 0x003D351C
    [NullableContext(2)]
    [CompilerGenerated]
    public virtual bool Equals(MarkdownViewerBlock other)
    {
      return this == other || (other != null && this.EqualityContract == other.EqualityContract);
    }

    // Token: 0x060019A3 RID: 6563 RVA: 0x003D353C File Offset: 0x003D353C
    [CompilerGenerated]
    protected MarkdownViewerBlock(MarkdownViewerBlock original)
    {
    }

    // Token: 0x060019A4 RID: 6564 RVA: 0x003D3544 File Offset: 0x003D3544
    protected MarkdownViewerBlock()
    {
    }

    // Token: 0x020005AB RID: 1451
    [Nullable(0)]
    public sealed class Heading : MarkdownViewerBlock, IEquatable<MarkdownViewerBlock.Heading>
    {
      // Token: 0x06003625 RID: 13861 RVA: 0x004436C8 File Offset: 0x004436C8
      public Heading(int Level, string InlineMarkdown)
      {
        this.Level = Level;
        this.InlineMarkdown = InlineMarkdown;
        base..ctor();
      }

      // Token: 0x1700098A RID: 2442
      // (get) Token: 0x06003626 RID: 13862 RVA: 0x004436E0 File Offset: 0x004436E0
      [CompilerGenerated]
      protected override Type EqualityContract
      {
        [CompilerGenerated]
        get
        {
          return typeof(MarkdownViewerBlock.Heading);
        }
      }

      // Token: 0x1700098B RID: 2443
      // (get) Token: 0x06003627 RID: 13863 RVA: 0x004436EC File Offset: 0x004436EC
      // (set) Token: 0x06003628 RID: 13864 RVA: 0x004436F4 File Offset: 0x004436F4
      public int Level { get; set; }

      // Token: 0x1700098C RID: 2444
      // (get) Token: 0x06003629 RID: 13865 RVA: 0x00443700 File Offset: 0x00443700
      // (set) Token: 0x0600362A RID: 13866 RVA: 0x00443708 File Offset: 0x00443708
      public string InlineMarkdown { get; set; }

      // Token: 0x0600362B RID: 13867 RVA: 0x00443714 File Offset: 0x00443714
      [CompilerGenerated]
      public override string ToString()
      {
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.Append("Heading");
        stringBuilder.Append(" { ");
        if (this.PrintMembers(stringBuilder))
        {
          stringBuilder.Append(' ');
        }
        stringBuilder.Append('}');
        return stringBuilder.ToString();
      }

      // Token: 0x0600362C RID: 13868 RVA: 0x00443760 File Offset: 0x00443760
      [CompilerGenerated]
      protected override bool PrintMembers(StringBuilder builder)
      {
        if (base.PrintMembers(builder))
        {
          builder.Append(", ");
        }
        builder.Append("Level = ");
        builder.Append(this.Level.ToString());
        builder.Append(", InlineMarkdown = ");
        builder.Append(this.InlineMarkdown);
        return true;
      }

      // Token: 0x0600362D RID: 13869 RVA: 0x004437C4 File Offset: 0x004437C4
      [NullableContext(2)]
      [CompilerGenerated]
      public static bool operator !=(MarkdownViewerBlock.Heading left, MarkdownViewerBlock.Heading right)
      {
        return !(left == right);
      }

      // Token: 0x0600362E RID: 13870 RVA: 0x004437D0 File Offset: 0x004437D0
      [NullableContext(2)]
      [CompilerGenerated]
      public static bool operator ==(MarkdownViewerBlock.Heading left, MarkdownViewerBlock.Heading right)
      {
        return left == right || (left != null && left.Equals(right));
      }

      // Token: 0x0600362F RID: 13871 RVA: 0x004437E4 File Offset: 0x004437E4
      [CompilerGenerated]
      public override int GetHashCode()
      {
        return (base.GetHashCode() * -1521134295 + EqualityComparer<int>.Default.GetHashCode(this.<Level>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<InlineMarkdown>k__BackingField);
      }

      // Token: 0x06003630 RID: 13872 RVA: 0x0044381C File Offset: 0x0044381C
      [NullableContext(2)]
      [CompilerGenerated]
      public override bool Equals(object obj)
      {
        return this.Equals(obj as MarkdownViewerBlock.Heading);
      }

      // Token: 0x06003631 RID: 13873 RVA: 0x0044382C File Offset: 0x0044382C
      [NullableContext(2)]
      [CompilerGenerated]
      public sealed override bool Equals(MarkdownViewerBlock other)
      {
        return this.Equals(other);
      }

      // Token: 0x06003632 RID: 13874 RVA: 0x00443838 File Offset: 0x00443838
      [NullableContext(2)]
      [CompilerGenerated]
      public bool Equals(MarkdownViewerBlock.Heading other)
      {
        return this == other || (base.Equals(other) && EqualityComparer<int>.Default.Equals(this.<Level>k__BackingField, other.<Level>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<InlineMarkdown>k__BackingField, other.<InlineMarkdown>k__BackingField));
      }

      // Token: 0x06003634 RID: 13876 RVA: 0x0044388C File Offset: 0x0044388C
      [CompilerGenerated]
      private Heading(MarkdownViewerBlock.Heading original)
        : base(original)
      {
        this.Level = original.<Level>k__BackingField;
        this.InlineMarkdown = original.<InlineMarkdown>k__BackingField;
      }

      // Token: 0x06003635 RID: 13877 RVA: 0x004438B0 File Offset: 0x004438B0
      [CompilerGenerated]
      public void Deconstruct(out int Level, out string InlineMarkdown)
      {
        Level = this.Level;
        InlineMarkdown = this.InlineMarkdown;
      }
    }

    // Token: 0x020005AC RID: 1452
    [Nullable(0)]
    public sealed class Paragraph : MarkdownViewerBlock, IEquatable<MarkdownViewerBlock.Paragraph>
    {
      // Token: 0x06003636 RID: 13878 RVA: 0x004438C4 File Offset: 0x004438C4
      public Paragraph(string InlineMarkdown)
      {
        this.InlineMarkdown = InlineMarkdown;
        base..ctor();
      }

      // Token: 0x1700098D RID: 2445
      // (get) Token: 0x06003637 RID: 13879 RVA: 0x004438D4 File Offset: 0x004438D4
      [CompilerGenerated]
      protected override Type EqualityContract
      {
        [CompilerGenerated]
        get
        {
          return typeof(MarkdownViewerBlock.Paragraph);
        }
      }

      // Token: 0x1700098E RID: 2446
      // (get) Token: 0x06003638 RID: 13880 RVA: 0x004438E0 File Offset: 0x004438E0
      // (set) Token: 0x06003639 RID: 13881 RVA: 0x004438E8 File Offset: 0x004438E8
      public string InlineMarkdown { get; set; }

      // Token: 0x0600363A RID: 13882 RVA: 0x004438F4 File Offset: 0x004438F4
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

      // Token: 0x0600363B RID: 13883 RVA: 0x00443940 File Offset: 0x00443940
      [CompilerGenerated]
      protected override bool PrintMembers(StringBuilder builder)
      {
        if (base.PrintMembers(builder))
        {
          builder.Append(", ");
        }
        builder.Append("InlineMarkdown = ");
        builder.Append(this.InlineMarkdown);
        return true;
      }

      // Token: 0x0600363C RID: 13884 RVA: 0x00443974 File Offset: 0x00443974
      [NullableContext(2)]
      [CompilerGenerated]
      public static bool operator !=(MarkdownViewerBlock.Paragraph left, MarkdownViewerBlock.Paragraph right)
      {
        return !(left == right);
      }

      // Token: 0x0600363D RID: 13885 RVA: 0x00443980 File Offset: 0x00443980
      [NullableContext(2)]
      [CompilerGenerated]
      public static bool operator ==(MarkdownViewerBlock.Paragraph left, MarkdownViewerBlock.Paragraph right)
      {
        return left == right || (left != null && left.Equals(right));
      }

      // Token: 0x0600363E RID: 13886 RVA: 0x00443994 File Offset: 0x00443994
      [CompilerGenerated]
      public override int GetHashCode()
      {
        return base.GetHashCode() * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<InlineMarkdown>k__BackingField);
      }

      // Token: 0x0600363F RID: 13887 RVA: 0x004439B4 File Offset: 0x004439B4
      [NullableContext(2)]
      [CompilerGenerated]
      public override bool Equals(object obj)
      {
        return this.Equals(obj as MarkdownViewerBlock.Paragraph);
      }

      // Token: 0x06003640 RID: 13888 RVA: 0x004439C4 File Offset: 0x004439C4
      [NullableContext(2)]
      [CompilerGenerated]
      public sealed override bool Equals(MarkdownViewerBlock other)
      {
        return this.Equals(other);
      }

      // Token: 0x06003641 RID: 13889 RVA: 0x004439D0 File Offset: 0x004439D0
      [NullableContext(2)]
      [CompilerGenerated]
      public bool Equals(MarkdownViewerBlock.Paragraph other)
      {
        return this == other || (base.Equals(other) && EqualityComparer<string>.Default.Equals(this.<InlineMarkdown>k__BackingField, other.<InlineMarkdown>k__BackingField));
      }

      // Token: 0x06003643 RID: 13891 RVA: 0x00443A04 File Offset: 0x00443A04
      [CompilerGenerated]
      private Paragraph(MarkdownViewerBlock.Paragraph original)
        : base(original)
      {
        this.InlineMarkdown = original.<InlineMarkdown>k__BackingField;
      }

      // Token: 0x06003644 RID: 13892 RVA: 0x00443A1C File Offset: 0x00443A1C
      [CompilerGenerated]
      public void Deconstruct(out string InlineMarkdown)
      {
        InlineMarkdown = this.InlineMarkdown;
      }
    }

    // Token: 0x020005AD RID: 1453
    [Nullable(0)]
    public sealed class CodeBlock : MarkdownViewerBlock, IEquatable<MarkdownViewerBlock.CodeBlock>
    {
      // Token: 0x06003645 RID: 13893 RVA: 0x00443A28 File Offset: 0x00443A28
      public CodeBlock([Nullable(2)] string Language, string Code)
      {
        this.Language = Language;
        this.Code = Code;
        base..ctor();
      }

      // Token: 0x1700098F RID: 2447
      // (get) Token: 0x06003646 RID: 13894 RVA: 0x00443A40 File Offset: 0x00443A40
      [CompilerGenerated]
      protected override Type EqualityContract
      {
        [CompilerGenerated]
        get
        {
          return typeof(MarkdownViewerBlock.CodeBlock);
        }
      }

      // Token: 0x17000990 RID: 2448
      // (get) Token: 0x06003647 RID: 13895 RVA: 0x00443A4C File Offset: 0x00443A4C
      // (set) Token: 0x06003648 RID: 13896 RVA: 0x00443A54 File Offset: 0x00443A54
      [Nullable(2)]
      public string Language
      {
        [NullableContext(2)]
        get;
        [NullableContext(2)]
        set;
      }

      // Token: 0x17000991 RID: 2449
      // (get) Token: 0x06003649 RID: 13897 RVA: 0x00443A60 File Offset: 0x00443A60
      // (set) Token: 0x0600364A RID: 13898 RVA: 0x00443A68 File Offset: 0x00443A68
      public string Code { get; set; }

      // Token: 0x0600364B RID: 13899 RVA: 0x00443A74 File Offset: 0x00443A74
      [CompilerGenerated]
      public override string ToString()
      {
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.Append("CodeBlock");
        stringBuilder.Append(" { ");
        if (this.PrintMembers(stringBuilder))
        {
          stringBuilder.Append(' ');
        }
        stringBuilder.Append('}');
        return stringBuilder.ToString();
      }

      // Token: 0x0600364C RID: 13900 RVA: 0x00443AC0 File Offset: 0x00443AC0
      [CompilerGenerated]
      protected override bool PrintMembers(StringBuilder builder)
      {
        if (base.PrintMembers(builder))
        {
          builder.Append(", ");
        }
        builder.Append("Language = ");
        builder.Append(this.Language);
        builder.Append(", Code = ");
        builder.Append(this.Code);
        return true;
      }

      // Token: 0x0600364D RID: 13901 RVA: 0x00443B18 File Offset: 0x00443B18
      [NullableContext(2)]
      [CompilerGenerated]
      public static bool operator !=(MarkdownViewerBlock.CodeBlock left, MarkdownViewerBlock.CodeBlock right)
      {
        return !(left == right);
      }

      // Token: 0x0600364E RID: 13902 RVA: 0x00443B24 File Offset: 0x00443B24
      [NullableContext(2)]
      [CompilerGenerated]
      public static bool operator ==(MarkdownViewerBlock.CodeBlock left, MarkdownViewerBlock.CodeBlock right)
      {
        return left == right || (left != null && left.Equals(right));
      }

      // Token: 0x0600364F RID: 13903 RVA: 0x00443B38 File Offset: 0x00443B38
      [CompilerGenerated]
      public override int GetHashCode()
      {
        return (base.GetHashCode() * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Language>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Code>k__BackingField);
      }

      // Token: 0x06003650 RID: 13904 RVA: 0x00443B70 File Offset: 0x00443B70
      [NullableContext(2)]
      [CompilerGenerated]
      public override bool Equals(object obj)
      {
        return this.Equals(obj as MarkdownViewerBlock.CodeBlock);
      }

      // Token: 0x06003651 RID: 13905 RVA: 0x00443B80 File Offset: 0x00443B80
      [NullableContext(2)]
      [CompilerGenerated]
      public sealed override bool Equals(MarkdownViewerBlock other)
      {
        return this.Equals(other);
      }

      // Token: 0x06003652 RID: 13906 RVA: 0x00443B8C File Offset: 0x00443B8C
      [NullableContext(2)]
      [CompilerGenerated]
      public bool Equals(MarkdownViewerBlock.CodeBlock other)
      {
        return this == other || (base.Equals(other) && EqualityComparer<string>.Default.Equals(this.<Language>k__BackingField, other.<Language>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<Code>k__BackingField, other.<Code>k__BackingField));
      }

      // Token: 0x06003654 RID: 13908 RVA: 0x00443BE0 File Offset: 0x00443BE0
      [CompilerGenerated]
      private CodeBlock(MarkdownViewerBlock.CodeBlock original)
        : base(original)
      {
        this.Language = original.<Language>k__BackingField;
        this.Code = original.<Code>k__BackingField;
      }

      // Token: 0x06003655 RID: 13909 RVA: 0x00443C04 File Offset: 0x00443C04
      [CompilerGenerated]
      public void Deconstruct([Nullable(2)] out string Language, out string Code)
      {
        Language = this.Language;
        Code = this.Code;
      }
    }

    // Token: 0x020005AE RID: 1454
    [Nullable(0)]
    public sealed class Blockquote : MarkdownViewerBlock, IEquatable<MarkdownViewerBlock.Blockquote>
    {
      // Token: 0x06003656 RID: 13910 RVA: 0x00443C18 File Offset: 0x00443C18
      public Blockquote(IReadOnlyList<MarkdownViewerBlock> ChildBlocks)
      {
        this.ChildBlocks = ChildBlocks;
        base..ctor();
      }

      // Token: 0x17000992 RID: 2450
      // (get) Token: 0x06003657 RID: 13911 RVA: 0x00443C28 File Offset: 0x00443C28
      [CompilerGenerated]
      protected override Type EqualityContract
      {
        [CompilerGenerated]
        get
        {
          return typeof(MarkdownViewerBlock.Blockquote);
        }
      }

      // Token: 0x17000993 RID: 2451
      // (get) Token: 0x06003658 RID: 13912 RVA: 0x00443C34 File Offset: 0x00443C34
      // (set) Token: 0x06003659 RID: 13913 RVA: 0x00443C3C File Offset: 0x00443C3C
      public IReadOnlyList<MarkdownViewerBlock> ChildBlocks { get; set; }

      // Token: 0x0600365A RID: 13914 RVA: 0x00443C48 File Offset: 0x00443C48
      [CompilerGenerated]
      public override string ToString()
      {
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.Append("Blockquote");
        stringBuilder.Append(" { ");
        if (this.PrintMembers(stringBuilder))
        {
          stringBuilder.Append(' ');
        }
        stringBuilder.Append('}');
        return stringBuilder.ToString();
      }

      // Token: 0x0600365B RID: 13915 RVA: 0x00443C94 File Offset: 0x00443C94
      [CompilerGenerated]
      protected override bool PrintMembers(StringBuilder builder)
      {
        if (base.PrintMembers(builder))
        {
          builder.Append(", ");
        }
        builder.Append("ChildBlocks = ");
        builder.Append(this.ChildBlocks);
        return true;
      }

      // Token: 0x0600365C RID: 13916 RVA: 0x00443CC8 File Offset: 0x00443CC8
      [NullableContext(2)]
      [CompilerGenerated]
      public static bool operator !=(MarkdownViewerBlock.Blockquote left, MarkdownViewerBlock.Blockquote right)
      {
        return !(left == right);
      }

      // Token: 0x0600365D RID: 13917 RVA: 0x00443CD4 File Offset: 0x00443CD4
      [NullableContext(2)]
      [CompilerGenerated]
      public static bool operator ==(MarkdownViewerBlock.Blockquote left, MarkdownViewerBlock.Blockquote right)
      {
        return left == right || (left != null && left.Equals(right));
      }

      // Token: 0x0600365E RID: 13918 RVA: 0x00443CE8 File Offset: 0x00443CE8
      [CompilerGenerated]
      public override int GetHashCode()
      {
        return base.GetHashCode() * -1521134295 + EqualityComparer<IReadOnlyList<MarkdownViewerBlock>>.Default.GetHashCode(this.<ChildBlocks>k__BackingField);
      }

      // Token: 0x0600365F RID: 13919 RVA: 0x00443D08 File Offset: 0x00443D08
      [NullableContext(2)]
      [CompilerGenerated]
      public override bool Equals(object obj)
      {
        return this.Equals(obj as MarkdownViewerBlock.Blockquote);
      }

      // Token: 0x06003660 RID: 13920 RVA: 0x00443D18 File Offset: 0x00443D18
      [NullableContext(2)]
      [CompilerGenerated]
      public sealed override bool Equals(MarkdownViewerBlock other)
      {
        return this.Equals(other);
      }

      // Token: 0x06003661 RID: 13921 RVA: 0x00443D24 File Offset: 0x00443D24
      [NullableContext(2)]
      [CompilerGenerated]
      public bool Equals(MarkdownViewerBlock.Blockquote other)
      {
        return this == other || (base.Equals(other) && EqualityComparer<IReadOnlyList<MarkdownViewerBlock>>.Default.Equals(this.<ChildBlocks>k__BackingField, other.<ChildBlocks>k__BackingField));
      }

      // Token: 0x06003663 RID: 13923 RVA: 0x00443D58 File Offset: 0x00443D58
      [CompilerGenerated]
      private Blockquote(MarkdownViewerBlock.Blockquote original)
        : base(original)
      {
        this.ChildBlocks = original.<ChildBlocks>k__BackingField;
      }

      // Token: 0x06003664 RID: 13924 RVA: 0x00443D70 File Offset: 0x00443D70
      [CompilerGenerated]
      public void Deconstruct(out IReadOnlyList<MarkdownViewerBlock> ChildBlocks)
      {
        ChildBlocks = this.ChildBlocks;
      }
    }

    // Token: 0x020005AF RID: 1455
    [Nullable(0)]
    public sealed class List : MarkdownViewerBlock, IEquatable<MarkdownViewerBlock.List>
    {
      // Token: 0x06003665 RID: 13925 RVA: 0x00443D7C File Offset: 0x00443D7C
      public List(IReadOnlyList<MarkdownViewerListItem> Items)
      {
        this.Items = Items;
        base..ctor();
      }

      // Token: 0x17000994 RID: 2452
      // (get) Token: 0x06003666 RID: 13926 RVA: 0x00443D8C File Offset: 0x00443D8C
      [CompilerGenerated]
      protected override Type EqualityContract
      {
        [CompilerGenerated]
        get
        {
          return typeof(MarkdownViewerBlock.List);
        }
      }

      // Token: 0x17000995 RID: 2453
      // (get) Token: 0x06003667 RID: 13927 RVA: 0x00443D98 File Offset: 0x00443D98
      // (set) Token: 0x06003668 RID: 13928 RVA: 0x00443DA0 File Offset: 0x00443DA0
      public IReadOnlyList<MarkdownViewerListItem> Items { get; set; }

      // Token: 0x06003669 RID: 13929 RVA: 0x00443DAC File Offset: 0x00443DAC
      [CompilerGenerated]
      public override string ToString()
      {
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.Append("List");
        stringBuilder.Append(" { ");
        if (this.PrintMembers(stringBuilder))
        {
          stringBuilder.Append(' ');
        }
        stringBuilder.Append('}');
        return stringBuilder.ToString();
      }

      // Token: 0x0600366A RID: 13930 RVA: 0x00443DF8 File Offset: 0x00443DF8
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

      // Token: 0x0600366B RID: 13931 RVA: 0x00443E2C File Offset: 0x00443E2C
      [NullableContext(2)]
      [CompilerGenerated]
      public static bool operator !=(MarkdownViewerBlock.List left, MarkdownViewerBlock.List right)
      {
        return !(left == right);
      }

      // Token: 0x0600366C RID: 13932 RVA: 0x00443E38 File Offset: 0x00443E38
      [NullableContext(2)]
      [CompilerGenerated]
      public static bool operator ==(MarkdownViewerBlock.List left, MarkdownViewerBlock.List right)
      {
        return left == right || (left != null && left.Equals(right));
      }

      // Token: 0x0600366D RID: 13933 RVA: 0x00443E4C File Offset: 0x00443E4C
      [CompilerGenerated]
      public override int GetHashCode()
      {
        return base.GetHashCode() * -1521134295 + EqualityComparer<IReadOnlyList<MarkdownViewerListItem>>.Default.GetHashCode(this.<Items>k__BackingField);
      }

      // Token: 0x0600366E RID: 13934 RVA: 0x00443E6C File Offset: 0x00443E6C
      [NullableContext(2)]
      [CompilerGenerated]
      public override bool Equals(object obj)
      {
        return this.Equals(obj as MarkdownViewerBlock.List);
      }

      // Token: 0x0600366F RID: 13935 RVA: 0x00443E7C File Offset: 0x00443E7C
      [NullableContext(2)]
      [CompilerGenerated]
      public sealed override bool Equals(MarkdownViewerBlock other)
      {
        return this.Equals(other);
      }

      // Token: 0x06003670 RID: 13936 RVA: 0x00443E88 File Offset: 0x00443E88
      [NullableContext(2)]
      [CompilerGenerated]
      public bool Equals(MarkdownViewerBlock.List other)
      {
        return this == other || (base.Equals(other) && EqualityComparer<IReadOnlyList<MarkdownViewerListItem>>.Default.Equals(this.<Items>k__BackingField, other.<Items>k__BackingField));
      }

      // Token: 0x06003672 RID: 13938 RVA: 0x00443EBC File Offset: 0x00443EBC
      [CompilerGenerated]
      private List(MarkdownViewerBlock.List original)
        : base(original)
      {
        this.Items = original.<Items>k__BackingField;
      }

      // Token: 0x06003673 RID: 13939 RVA: 0x00443ED4 File Offset: 0x00443ED4
      [CompilerGenerated]
      public void Deconstruct(out IReadOnlyList<MarkdownViewerListItem> Items)
      {
        Items = this.Items;
      }
    }

    // Token: 0x020005B0 RID: 1456
    [Nullable(0)]
    public sealed class Table : MarkdownViewerBlock, IEquatable<MarkdownViewerBlock.Table>
    {
      // Token: 0x06003674 RID: 13940 RVA: 0x00443EE0 File Offset: 0x00443EE0
      public Table(IReadOnlyList<string> HeaderCells, IReadOnlyList<IReadOnlyList<string>> Rows)
      {
        this.HeaderCells = HeaderCells;
        this.Rows = Rows;
        base..ctor();
      }

      // Token: 0x17000996 RID: 2454
      // (get) Token: 0x06003675 RID: 13941 RVA: 0x00443EF8 File Offset: 0x00443EF8
      [CompilerGenerated]
      protected override Type EqualityContract
      {
        [CompilerGenerated]
        get
        {
          return typeof(MarkdownViewerBlock.Table);
        }
      }

      // Token: 0x17000997 RID: 2455
      // (get) Token: 0x06003676 RID: 13942 RVA: 0x00443F04 File Offset: 0x00443F04
      // (set) Token: 0x06003677 RID: 13943 RVA: 0x00443F0C File Offset: 0x00443F0C
      public IReadOnlyList<string> HeaderCells { get; set; }

      // Token: 0x17000998 RID: 2456
      // (get) Token: 0x06003678 RID: 13944 RVA: 0x00443F18 File Offset: 0x00443F18
      // (set) Token: 0x06003679 RID: 13945 RVA: 0x00443F20 File Offset: 0x00443F20
      public IReadOnlyList<IReadOnlyList<string>> Rows { get; set; }

      // Token: 0x0600367A RID: 13946 RVA: 0x00443F2C File Offset: 0x00443F2C
      [CompilerGenerated]
      public override string ToString()
      {
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.Append("Table");
        stringBuilder.Append(" { ");
        if (this.PrintMembers(stringBuilder))
        {
          stringBuilder.Append(' ');
        }
        stringBuilder.Append('}');
        return stringBuilder.ToString();
      }

      // Token: 0x0600367B RID: 13947 RVA: 0x00443F78 File Offset: 0x00443F78
      [CompilerGenerated]
      protected override bool PrintMembers(StringBuilder builder)
      {
        if (base.PrintMembers(builder))
        {
          builder.Append(", ");
        }
        builder.Append("HeaderCells = ");
        builder.Append(this.HeaderCells);
        builder.Append(", Rows = ");
        builder.Append(this.Rows);
        return true;
      }

      // Token: 0x0600367C RID: 13948 RVA: 0x00443FD0 File Offset: 0x00443FD0
      [NullableContext(2)]
      [CompilerGenerated]
      public static bool operator !=(MarkdownViewerBlock.Table left, MarkdownViewerBlock.Table right)
      {
        return !(left == right);
      }

      // Token: 0x0600367D RID: 13949 RVA: 0x00443FDC File Offset: 0x00443FDC
      [NullableContext(2)]
      [CompilerGenerated]
      public static bool operator ==(MarkdownViewerBlock.Table left, MarkdownViewerBlock.Table right)
      {
        return left == right || (left != null && left.Equals(right));
      }

      // Token: 0x0600367E RID: 13950 RVA: 0x00443FF0 File Offset: 0x00443FF0
      [CompilerGenerated]
      public override int GetHashCode()
      {
        return (base.GetHashCode() * -1521134295 + EqualityComparer<IReadOnlyList<string>>.Default.GetHashCode(this.<HeaderCells>k__BackingField)) * -1521134295 + EqualityComparer<IReadOnlyList<IReadOnlyList<string>>>.Default.GetHashCode(this.<Rows>k__BackingField);
      }

      // Token: 0x0600367F RID: 13951 RVA: 0x00444028 File Offset: 0x00444028
      [NullableContext(2)]
      [CompilerGenerated]
      public override bool Equals(object obj)
      {
        return this.Equals(obj as MarkdownViewerBlock.Table);
      }

      // Token: 0x06003680 RID: 13952 RVA: 0x00444038 File Offset: 0x00444038
      [NullableContext(2)]
      [CompilerGenerated]
      public sealed override bool Equals(MarkdownViewerBlock other)
      {
        return this.Equals(other);
      }

      // Token: 0x06003681 RID: 13953 RVA: 0x00444044 File Offset: 0x00444044
      [NullableContext(2)]
      [CompilerGenerated]
      public bool Equals(MarkdownViewerBlock.Table other)
      {
        return this == other || (base.Equals(other) && EqualityComparer<IReadOnlyList<string>>.Default.Equals(this.<HeaderCells>k__BackingField, other.<HeaderCells>k__BackingField) && EqualityComparer<IReadOnlyList<IReadOnlyList<string>>>.Default.Equals(this.<Rows>k__BackingField, other.<Rows>k__BackingField));
      }

      // Token: 0x06003683 RID: 13955 RVA: 0x00444098 File Offset: 0x00444098
      [CompilerGenerated]
      private Table(MarkdownViewerBlock.Table original)
        : base(original)
      {
        this.HeaderCells = original.<HeaderCells>k__BackingField;
        this.Rows = original.<Rows>k__BackingField;
      }

      // Token: 0x06003684 RID: 13956 RVA: 0x004440BC File Offset: 0x004440BC
      [CompilerGenerated]
      public void Deconstruct(out IReadOnlyList<string> HeaderCells, out IReadOnlyList<IReadOnlyList<string>> Rows)
      {
        HeaderCells = this.HeaderCells;
        Rows = this.Rows;
      }
    }

    // Token: 0x020005B1 RID: 1457
    [Nullable(0)]
    public sealed class HorizontalRule : MarkdownViewerBlock, IEquatable<MarkdownViewerBlock.HorizontalRule>
    {
      // Token: 0x17000999 RID: 2457
      // (get) Token: 0x06003685 RID: 13957 RVA: 0x004440D0 File Offset: 0x004440D0
      [CompilerGenerated]
      protected override Type EqualityContract
      {
        [CompilerGenerated]
        get
        {
          return typeof(MarkdownViewerBlock.HorizontalRule);
        }
      }

      // Token: 0x06003686 RID: 13958 RVA: 0x004440DC File Offset: 0x004440DC
      [CompilerGenerated]
      public override string ToString()
      {
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.Append("HorizontalRule");
        stringBuilder.Append(" { ");
        if (this.PrintMembers(stringBuilder))
        {
          stringBuilder.Append(' ');
        }
        stringBuilder.Append('}');
        return stringBuilder.ToString();
      }

      // Token: 0x06003687 RID: 13959 RVA: 0x00444128 File Offset: 0x00444128
      [CompilerGenerated]
      protected override bool PrintMembers(StringBuilder builder)
      {
        return base.PrintMembers(builder);
      }

      // Token: 0x06003688 RID: 13960 RVA: 0x00444134 File Offset: 0x00444134
      [NullableContext(2)]
      [CompilerGenerated]
      public static bool operator !=(MarkdownViewerBlock.HorizontalRule left, MarkdownViewerBlock.HorizontalRule right)
      {
        return !(left == right);
      }

      // Token: 0x06003689 RID: 13961 RVA: 0x00444140 File Offset: 0x00444140
      [NullableContext(2)]
      [CompilerGenerated]
      public static bool operator ==(MarkdownViewerBlock.HorizontalRule left, MarkdownViewerBlock.HorizontalRule right)
      {
        return left == right || (left != null && left.Equals(right));
      }

      // Token: 0x0600368A RID: 13962 RVA: 0x00444154 File Offset: 0x00444154
      [CompilerGenerated]
      public override int GetHashCode()
      {
        return base.GetHashCode();
      }

      // Token: 0x0600368B RID: 13963 RVA: 0x0044415C File Offset: 0x0044415C
      [NullableContext(2)]
      [CompilerGenerated]
      public override bool Equals(object obj)
      {
        return this.Equals(obj as MarkdownViewerBlock.HorizontalRule);
      }

      // Token: 0x0600368C RID: 13964 RVA: 0x0044416C File Offset: 0x0044416C
      [NullableContext(2)]
      [CompilerGenerated]
      public sealed override bool Equals(MarkdownViewerBlock other)
      {
        return this.Equals(other);
      }

      // Token: 0x0600368D RID: 13965 RVA: 0x00444178 File Offset: 0x00444178
      [NullableContext(2)]
      [CompilerGenerated]
      public bool Equals(MarkdownViewerBlock.HorizontalRule other)
      {
        return this == other || base.Equals(other);
      }

      // Token: 0x0600368F RID: 13967 RVA: 0x00444190 File Offset: 0x00444190
      [CompilerGenerated]
      private HorizontalRule(MarkdownViewerBlock.HorizontalRule original)
        : base(original)
      {
      }

      // Token: 0x06003690 RID: 13968 RVA: 0x0044419C File Offset: 0x0044419C
      public HorizontalRule()
      {
      }
    }

    // Token: 0x020005B2 RID: 1458
    [Nullable(0)]
    public sealed class Image : MarkdownViewerBlock, IEquatable<MarkdownViewerBlock.Image>
    {
      // Token: 0x06003691 RID: 13969 RVA: 0x004441A4 File Offset: 0x004441A4
      public Image(string AlternateText, string Source)
      {
        this.AlternateText = AlternateText;
        this.Source = Source;
        base..ctor();
      }

      // Token: 0x1700099A RID: 2458
      // (get) Token: 0x06003692 RID: 13970 RVA: 0x004441BC File Offset: 0x004441BC
      [CompilerGenerated]
      protected override Type EqualityContract
      {
        [CompilerGenerated]
        get
        {
          return typeof(MarkdownViewerBlock.Image);
        }
      }

      // Token: 0x1700099B RID: 2459
      // (get) Token: 0x06003693 RID: 13971 RVA: 0x004441C8 File Offset: 0x004441C8
      // (set) Token: 0x06003694 RID: 13972 RVA: 0x004441D0 File Offset: 0x004441D0
      public string AlternateText { get; set; }

      // Token: 0x1700099C RID: 2460
      // (get) Token: 0x06003695 RID: 13973 RVA: 0x004441DC File Offset: 0x004441DC
      // (set) Token: 0x06003696 RID: 13974 RVA: 0x004441E4 File Offset: 0x004441E4
      public string Source { get; set; }

      // Token: 0x06003697 RID: 13975 RVA: 0x004441F0 File Offset: 0x004441F0
      [CompilerGenerated]
      public override string ToString()
      {
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.Append("Image");
        stringBuilder.Append(" { ");
        if (this.PrintMembers(stringBuilder))
        {
          stringBuilder.Append(' ');
        }
        stringBuilder.Append('}');
        return stringBuilder.ToString();
      }

      // Token: 0x06003698 RID: 13976 RVA: 0x0044423C File Offset: 0x0044423C
      [CompilerGenerated]
      protected override bool PrintMembers(StringBuilder builder)
      {
        if (base.PrintMembers(builder))
        {
          builder.Append(", ");
        }
        builder.Append("AlternateText = ");
        builder.Append(this.AlternateText);
        builder.Append(", Source = ");
        builder.Append(this.Source);
        return true;
      }

      // Token: 0x06003699 RID: 13977 RVA: 0x00444294 File Offset: 0x00444294
      [NullableContext(2)]
      [CompilerGenerated]
      public static bool operator !=(MarkdownViewerBlock.Image left, MarkdownViewerBlock.Image right)
      {
        return !(left == right);
      }

      // Token: 0x0600369A RID: 13978 RVA: 0x004442A0 File Offset: 0x004442A0
      [NullableContext(2)]
      [CompilerGenerated]
      public static bool operator ==(MarkdownViewerBlock.Image left, MarkdownViewerBlock.Image right)
      {
        return left == right || (left != null && left.Equals(right));
      }

      // Token: 0x0600369B RID: 13979 RVA: 0x004442B4 File Offset: 0x004442B4
      [CompilerGenerated]
      public override int GetHashCode()
      {
        return (base.GetHashCode() * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<AlternateText>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Source>k__BackingField);
      }

      // Token: 0x0600369C RID: 13980 RVA: 0x004442EC File Offset: 0x004442EC
      [NullableContext(2)]
      [CompilerGenerated]
      public override bool Equals(object obj)
      {
        return this.Equals(obj as MarkdownViewerBlock.Image);
      }

      // Token: 0x0600369D RID: 13981 RVA: 0x004442FC File Offset: 0x004442FC
      [NullableContext(2)]
      [CompilerGenerated]
      public sealed override bool Equals(MarkdownViewerBlock other)
      {
        return this.Equals(other);
      }

      // Token: 0x0600369E RID: 13982 RVA: 0x00444308 File Offset: 0x00444308
      [NullableContext(2)]
      [CompilerGenerated]
      public bool Equals(MarkdownViewerBlock.Image other)
      {
        return this == other || (base.Equals(other) && EqualityComparer<string>.Default.Equals(this.<AlternateText>k__BackingField, other.<AlternateText>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<Source>k__BackingField, other.<Source>k__BackingField));
      }

      // Token: 0x060036A0 RID: 13984 RVA: 0x0044435C File Offset: 0x0044435C
      [CompilerGenerated]
      private Image(MarkdownViewerBlock.Image original)
        : base(original)
      {
        this.AlternateText = original.<AlternateText>k__BackingField;
        this.Source = original.<Source>k__BackingField;
      }

      // Token: 0x060036A1 RID: 13985 RVA: 0x00444380 File Offset: 0x00444380
      [CompilerGenerated]
      public void Deconstruct(out string AlternateText, out string Source)
      {
        AlternateText = this.AlternateText;
        Source = this.Source;
      }
    }
  }
}
