using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace HeyClicky.Core.Browser
{
  // Token: 0x020001E8 RID: 488
  [NullableContext(1)]
  [Nullable(0)]
  public sealed class BrowserTurnContext : IEquatable<BrowserTurnContext>
  {
    // Token: 0x06001C89 RID: 7305 RVA: 0x003E3FE0 File Offset: 0x003E3FE0
    public BrowserTurnContext(string Title, string GroupTitle, [Nullable(2)] string ProgressLine, Func<bool> IsActive)
    {
      this.Title = Title;
      this.GroupTitle = GroupTitle;
      this.ProgressLine = ProgressLine;
      this.IsActive = IsActive;
      base..ctor();
    }

    // Token: 0x17000652 RID: 1618
    // (get) Token: 0x06001C8A RID: 7306 RVA: 0x003E4008 File Offset: 0x003E4008
    [CompilerGenerated]
    private Type EqualityContract
    {
      [CompilerGenerated]
      get
      {
        return typeof(BrowserTurnContext);
      }
    }

    // Token: 0x17000653 RID: 1619
    // (get) Token: 0x06001C8B RID: 7307 RVA: 0x003E4014 File Offset: 0x003E4014
    // (set) Token: 0x06001C8C RID: 7308 RVA: 0x003E401C File Offset: 0x003E401C
    public string Title { get; set; }

    // Token: 0x17000654 RID: 1620
    // (get) Token: 0x06001C8D RID: 7309 RVA: 0x003E4028 File Offset: 0x003E4028
    // (set) Token: 0x06001C8E RID: 7310 RVA: 0x003E4030 File Offset: 0x003E4030
    public string GroupTitle { get; set; }

    // Token: 0x17000655 RID: 1621
    // (get) Token: 0x06001C8F RID: 7311 RVA: 0x003E403C File Offset: 0x003E403C
    // (set) Token: 0x06001C90 RID: 7312 RVA: 0x003E4044 File Offset: 0x003E4044
    [Nullable(2)]
    public string ProgressLine
    {
      [NullableContext(2)]
      get;
      [NullableContext(2)]
      set;
    }

    // Token: 0x17000656 RID: 1622
    // (get) Token: 0x06001C91 RID: 7313 RVA: 0x003E4050 File Offset: 0x003E4050
    // (set) Token: 0x06001C92 RID: 7314 RVA: 0x003E4058 File Offset: 0x003E4058
    public Func<bool> IsActive { get; set; }

    // Token: 0x06001C93 RID: 7315 RVA: 0x003E4064 File Offset: 0x003E4064
    [CompilerGenerated]
    public override string ToString()
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("BrowserTurnContext");
      stringBuilder.Append(" { ");
      if (this.PrintMembers(stringBuilder))
      {
        stringBuilder.Append(' ');
      }
      stringBuilder.Append('}');
      return stringBuilder.ToString();
    }

    // Token: 0x06001C94 RID: 7316 RVA: 0x003E40B0 File Offset: 0x003E40B0
    [CompilerGenerated]
    private bool PrintMembers(StringBuilder builder)
    {
      RuntimeHelpers.EnsureSufficientExecutionStack();
      builder.Append("Title = ");
      builder.Append(this.Title);
      builder.Append(", GroupTitle = ");
      builder.Append(this.GroupTitle);
      builder.Append(", ProgressLine = ");
      builder.Append(this.ProgressLine);
      builder.Append(", IsActive = ");
      builder.Append(this.IsActive);
      return true;
    }

    // Token: 0x06001C95 RID: 7317 RVA: 0x003E4128 File Offset: 0x003E4128
    [NullableContext(2)]
    [CompilerGenerated]
    public static bool operator !=(BrowserTurnContext left, BrowserTurnContext right)
    {
      return !(left == right);
    }

    // Token: 0x06001C96 RID: 7318 RVA: 0x003E4134 File Offset: 0x003E4134
    [NullableContext(2)]
    [CompilerGenerated]
    public static bool operator ==(BrowserTurnContext left, BrowserTurnContext right)
    {
      return left == right || (left != null && left.Equals(right));
    }

    // Token: 0x06001C97 RID: 7319 RVA: 0x003E4148 File Offset: 0x003E4148
    [CompilerGenerated]
    public override int GetHashCode()
    {
      return (((EqualityComparer<Type>.Default.GetHashCode(this.EqualityContract) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Title>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<GroupTitle>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<ProgressLine>k__BackingField)) * -1521134295 + EqualityComparer<Func<bool>>.Default.GetHashCode(this.<IsActive>k__BackingField);
    }

    // Token: 0x06001C98 RID: 7320 RVA: 0x003E41C4 File Offset: 0x003E41C4
    [NullableContext(2)]
    [CompilerGenerated]
    public override bool Equals(object obj)
    {
      return this.Equals(obj as BrowserTurnContext);
    }

    // Token: 0x06001C99 RID: 7321 RVA: 0x003E41D4 File Offset: 0x003E41D4
    [NullableContext(2)]
    [CompilerGenerated]
    public bool Equals(BrowserTurnContext other)
    {
      return this == other || (other != null && this.EqualityContract == other.EqualityContract && EqualityComparer<string>.Default.Equals(this.<Title>k__BackingField, other.<Title>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<GroupTitle>k__BackingField, other.<GroupTitle>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<ProgressLine>k__BackingField, other.<ProgressLine>k__BackingField) && EqualityComparer<Func<bool>>.Default.Equals(this.<IsActive>k__BackingField, other.<IsActive>k__BackingField));
    }

    // Token: 0x06001C9B RID: 7323 RVA: 0x003E4268 File Offset: 0x003E4268
    [CompilerGenerated]
    private BrowserTurnContext(BrowserTurnContext original)
    {
      this.Title = original.<Title>k__BackingField;
      this.GroupTitle = original.<GroupTitle>k__BackingField;
      this.ProgressLine = original.<ProgressLine>k__BackingField;
      this.IsActive = original.<IsActive>k__BackingField;
    }

    // Token: 0x06001C9C RID: 7324 RVA: 0x003E42A0 File Offset: 0x003E42A0
    [CompilerGenerated]
    public void Deconstruct(out string Title, out string GroupTitle, [Nullable(2)] out string ProgressLine, out Func<bool> IsActive)
    {
      Title = this.Title;
      GroupTitle = this.GroupTitle;
      ProgressLine = this.ProgressLine;
      IsActive = this.IsActive;
    }
  }
}
