using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace HeyClicky.App.Notch.Peek
{
  // Token: 0x0200018A RID: 394
  [NullableContext(1)]
  [Nullable(0)]
  public sealed class NotchUpdateReadyOutput : IEquatable<NotchUpdateReadyOutput>
  {
    // Token: 0x0600156C RID: 5484 RVA: 0x005B4F5C File Offset: 0x005B4F5C
    public NotchUpdateReadyOutput(string ButtonTitle, [Nullable(2)] string Glyph, bool OpensFullReplyOnly, bool IsLink, [Nullable(2)] string LinkTitle, Action Open)
    {
      this.ButtonTitle = ButtonTitle;
      this.Glyph = Glyph;
      this.OpensFullReplyOnly = OpensFullReplyOnly;
      this.IsLink = IsLink;
      this.LinkTitle = LinkTitle;
      this.Open = Open;
      base..ctor();
    }

    // Token: 0x170003DB RID: 987
    // (get) Token: 0x0600156D RID: 5485 RVA: 0x005B4F94 File Offset: 0x005B4F94
    [CompilerGenerated]
    private Type EqualityContract
    {
      [CompilerGenerated]
      get
      {
        return typeof(NotchUpdateReadyOutput);
      }
    }

    // Token: 0x170003DC RID: 988
    // (get) Token: 0x0600156E RID: 5486 RVA: 0x005B4FA0 File Offset: 0x005B4FA0
    // (set) Token: 0x0600156F RID: 5487 RVA: 0x005B4FA8 File Offset: 0x005B4FA8
    public string ButtonTitle { get; set; }

    // Token: 0x170003DD RID: 989
    // (get) Token: 0x06001570 RID: 5488 RVA: 0x005B4FB4 File Offset: 0x005B4FB4
    // (set) Token: 0x06001571 RID: 5489 RVA: 0x005B4FBC File Offset: 0x005B4FBC
    [Nullable(2)]
    public string Glyph
    {
      [NullableContext(2)]
      get;
      [NullableContext(2)]
      set;
    }

    // Token: 0x170003DE RID: 990
    // (get) Token: 0x06001572 RID: 5490 RVA: 0x005B4FC8 File Offset: 0x005B4FC8
    // (set) Token: 0x06001573 RID: 5491 RVA: 0x005B4FD0 File Offset: 0x005B4FD0
    public bool OpensFullReplyOnly { get; set; }

    // Token: 0x170003DF RID: 991
    // (get) Token: 0x06001574 RID: 5492 RVA: 0x005B4FDC File Offset: 0x005B4FDC
    // (set) Token: 0x06001575 RID: 5493 RVA: 0x005B4FE4 File Offset: 0x005B4FE4
    public bool IsLink { get; set; }

    // Token: 0x170003E0 RID: 992
    // (get) Token: 0x06001576 RID: 5494 RVA: 0x005B4FF0 File Offset: 0x005B4FF0
    // (set) Token: 0x06001577 RID: 5495 RVA: 0x005B4FF8 File Offset: 0x005B4FF8
    [Nullable(2)]
    public string LinkTitle
    {
      [NullableContext(2)]
      get;
      [NullableContext(2)]
      set;
    }

    // Token: 0x170003E1 RID: 993
    // (get) Token: 0x06001578 RID: 5496 RVA: 0x005B5004 File Offset: 0x005B5004
    // (set) Token: 0x06001579 RID: 5497 RVA: 0x005B500C File Offset: 0x005B500C
    public Action Open { get; set; }

    // Token: 0x0600157A RID: 5498 RVA: 0x005B5018 File Offset: 0x005B5018
    [CompilerGenerated]
    public override string ToString()
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("NotchUpdateReadyOutput");
      stringBuilder.Append(" { ");
      if (this.PrintMembers(stringBuilder))
      {
        stringBuilder.Append(' ');
      }
      stringBuilder.Append('}');
      return stringBuilder.ToString();
    }

    // Token: 0x0600157B RID: 5499 RVA: 0x005B5064 File Offset: 0x005B5064
    [CompilerGenerated]
    private bool PrintMembers(StringBuilder builder)
    {
      RuntimeHelpers.EnsureSufficientExecutionStack();
      builder.Append("ButtonTitle = ");
      builder.Append(this.ButtonTitle);
      builder.Append(", Glyph = ");
      builder.Append(this.Glyph);
      builder.Append(", OpensFullReplyOnly = ");
      builder.Append(this.OpensFullReplyOnly.ToString());
      builder.Append(", IsLink = ");
      builder.Append(this.IsLink.ToString());
      builder.Append(", LinkTitle = ");
      builder.Append(this.LinkTitle);
      builder.Append(", Open = ");
      builder.Append(this.Open);
      return true;
    }

    // Token: 0x0600157C RID: 5500 RVA: 0x005B512C File Offset: 0x005B512C
    [NullableContext(2)]
    [CompilerGenerated]
    public static bool operator !=(NotchUpdateReadyOutput left, NotchUpdateReadyOutput right)
    {
      return !(left == right);
    }

    // Token: 0x0600157D RID: 5501 RVA: 0x005B5138 File Offset: 0x005B5138
    [NullableContext(2)]
    [CompilerGenerated]
    public static bool operator ==(NotchUpdateReadyOutput left, NotchUpdateReadyOutput right)
    {
      return left == right || (left != null && left.Equals(right));
    }

    // Token: 0x0600157E RID: 5502 RVA: 0x005B514C File Offset: 0x005B514C
    [CompilerGenerated]
    public override int GetHashCode()
    {
      return (((((EqualityComparer<Type>.Default.GetHashCode(this.EqualityContract) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<ButtonTitle>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Glyph>k__BackingField)) * -1521134295 + EqualityComparer<bool>.Default.GetHashCode(this.<OpensFullReplyOnly>k__BackingField)) * -1521134295 + EqualityComparer<bool>.Default.GetHashCode(this.<IsLink>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<LinkTitle>k__BackingField)) * -1521134295 + EqualityComparer<Action>.Default.GetHashCode(this.<Open>k__BackingField);
    }

    // Token: 0x0600157F RID: 5503 RVA: 0x005B51F4 File Offset: 0x005B51F4
    [NullableContext(2)]
    [CompilerGenerated]
    public override bool Equals(object obj)
    {
      return this.Equals(obj as NotchUpdateReadyOutput);
    }

    // Token: 0x06001580 RID: 5504 RVA: 0x005B5204 File Offset: 0x005B5204
    [NullableContext(2)]
    [CompilerGenerated]
    public bool Equals(NotchUpdateReadyOutput other)
    {
      return this == other || (other != null && this.EqualityContract == other.EqualityContract && EqualityComparer<string>.Default.Equals(this.<ButtonTitle>k__BackingField, other.<ButtonTitle>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<Glyph>k__BackingField, other.<Glyph>k__BackingField) && EqualityComparer<bool>.Default.Equals(this.<OpensFullReplyOnly>k__BackingField, other.<OpensFullReplyOnly>k__BackingField) && EqualityComparer<bool>.Default.Equals(this.<IsLink>k__BackingField, other.<IsLink>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<LinkTitle>k__BackingField, other.<LinkTitle>k__BackingField) && EqualityComparer<Action>.Default.Equals(this.<Open>k__BackingField, other.<Open>k__BackingField));
    }

    // Token: 0x06001582 RID: 5506 RVA: 0x005B52D0 File Offset: 0x005B52D0
    [CompilerGenerated]
    private NotchUpdateReadyOutput(NotchUpdateReadyOutput original)
    {
      this.ButtonTitle = original.<ButtonTitle>k__BackingField;
      this.Glyph = original.<Glyph>k__BackingField;
      this.OpensFullReplyOnly = original.<OpensFullReplyOnly>k__BackingField;
      this.IsLink = original.<IsLink>k__BackingField;
      this.LinkTitle = original.<LinkTitle>k__BackingField;
      this.Open = original.<Open>k__BackingField;
    }

    // Token: 0x06001583 RID: 5507 RVA: 0x005B532C File Offset: 0x005B532C
    [CompilerGenerated]
    public void Deconstruct(out string ButtonTitle, [Nullable(2)] out string Glyph, out bool OpensFullReplyOnly, out bool IsLink, [Nullable(2)] out string LinkTitle, out Action Open)
    {
      ButtonTitle = this.ButtonTitle;
      Glyph = this.Glyph;
      OpensFullReplyOnly = this.OpensFullReplyOnly;
      IsLink = this.IsLink;
      LinkTitle = this.LinkTitle;
      Open = this.Open;
    }
  }
}
