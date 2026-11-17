using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace HeyClicky.Agents
{
  // Token: 0x02000014 RID: 20
  [NullableContext(1)]
  [Nullable(0)]
  public sealed class AgentBrowserTurn : IEquatable<AgentBrowserTurn>
  {
    // Token: 0x06000157 RID: 343 RVA: 0x000D9BFC File Offset: 0x000D9BFC
    public AgentBrowserTurn(string Title, string GroupLabel, [Nullable(2)] string LatestCommentary, [Nullable(2)] string ThinkingHeading, Func<bool> IsActive)
    {
      this.Title = Title;
      this.GroupLabel = GroupLabel;
      this.LatestCommentary = LatestCommentary;
      this.ThinkingHeading = ThinkingHeading;
      this.IsActive = IsActive;
      base..ctor();
    }

    // Token: 0x17000075 RID: 117
    // (get) Token: 0x06000158 RID: 344 RVA: 0x000D9C2C File Offset: 0x000D9C2C
    [CompilerGenerated]
    private Type EqualityContract
    {
      [CompilerGenerated]
      get
      {
        return typeof(AgentBrowserTurn);
      }
    }

    // Token: 0x17000076 RID: 118
    // (get) Token: 0x06000159 RID: 345 RVA: 0x000D9C38 File Offset: 0x000D9C38
    // (set) Token: 0x0600015A RID: 346 RVA: 0x000D9C40 File Offset: 0x000D9C40
    public string Title { get; set; }

    // Token: 0x17000077 RID: 119
    // (get) Token: 0x0600015B RID: 347 RVA: 0x000D9C4C File Offset: 0x000D9C4C
    // (set) Token: 0x0600015C RID: 348 RVA: 0x000D9C54 File Offset: 0x000D9C54
    public string GroupLabel { get; set; }

    // Token: 0x17000078 RID: 120
    // (get) Token: 0x0600015D RID: 349 RVA: 0x000D9C60 File Offset: 0x000D9C60
    // (set) Token: 0x0600015E RID: 350 RVA: 0x000D9C68 File Offset: 0x000D9C68
    [Nullable(2)]
    public string LatestCommentary
    {
      [NullableContext(2)]
      get;
      [NullableContext(2)]
      set;
    }

    // Token: 0x17000079 RID: 121
    // (get) Token: 0x0600015F RID: 351 RVA: 0x000D9C74 File Offset: 0x000D9C74
    // (set) Token: 0x06000160 RID: 352 RVA: 0x000D9C7C File Offset: 0x000D9C7C
    [Nullable(2)]
    public string ThinkingHeading
    {
      [NullableContext(2)]
      get;
      [NullableContext(2)]
      set;
    }

    // Token: 0x1700007A RID: 122
    // (get) Token: 0x06000161 RID: 353 RVA: 0x000D9C88 File Offset: 0x000D9C88
    // (set) Token: 0x06000162 RID: 354 RVA: 0x000D9C90 File Offset: 0x000D9C90
    public Func<bool> IsActive { get; set; }

    // Token: 0x06000163 RID: 355 RVA: 0x000D9C9C File Offset: 0x000D9C9C
    [CompilerGenerated]
    public override string ToString()
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("AgentBrowserTurn");
      stringBuilder.Append(" { ");
      if (this.PrintMembers(stringBuilder))
      {
        stringBuilder.Append(' ');
      }
      stringBuilder.Append('}');
      return stringBuilder.ToString();
    }

    // Token: 0x06000164 RID: 356 RVA: 0x000D9CE8 File Offset: 0x000D9CE8
    [CompilerGenerated]
    private bool PrintMembers(StringBuilder builder)
    {
      RuntimeHelpers.EnsureSufficientExecutionStack();
      builder.Append("Title = ");
      builder.Append(this.Title);
      builder.Append(", GroupLabel = ");
      builder.Append(this.GroupLabel);
      builder.Append(", LatestCommentary = ");
      builder.Append(this.LatestCommentary);
      builder.Append(", ThinkingHeading = ");
      builder.Append(this.ThinkingHeading);
      builder.Append(", IsActive = ");
      builder.Append(this.IsActive);
      return true;
    }

    // Token: 0x06000165 RID: 357 RVA: 0x000D9D78 File Offset: 0x000D9D78
    [NullableContext(2)]
    [CompilerGenerated]
    public static bool operator !=(AgentBrowserTurn left, AgentBrowserTurn right)
    {
      return !(left == right);
    }

    // Token: 0x06000166 RID: 358 RVA: 0x000D9D84 File Offset: 0x000D9D84
    [NullableContext(2)]
    [CompilerGenerated]
    public static bool operator ==(AgentBrowserTurn left, AgentBrowserTurn right)
    {
      return left == right || (left != null && left.Equals(right));
    }

    // Token: 0x06000167 RID: 359 RVA: 0x000D9D98 File Offset: 0x000D9D98
    [CompilerGenerated]
    public override int GetHashCode()
    {
      return ((((EqualityComparer<Type>.Default.GetHashCode(this.EqualityContract) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Title>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<GroupLabel>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<LatestCommentary>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<ThinkingHeading>k__BackingField)) * -1521134295 + EqualityComparer<Func<bool>>.Default.GetHashCode(this.<IsActive>k__BackingField);
    }

    // Token: 0x06000168 RID: 360 RVA: 0x000D9E28 File Offset: 0x000D9E28
    [NullableContext(2)]
    [CompilerGenerated]
    public override bool Equals(object obj)
    {
      return this.Equals(obj as AgentBrowserTurn);
    }

    // Token: 0x06000169 RID: 361 RVA: 0x000D9E38 File Offset: 0x000D9E38
    [NullableContext(2)]
    [CompilerGenerated]
    public bool Equals(AgentBrowserTurn other)
    {
      return this == other || (other != null && this.EqualityContract == other.EqualityContract && EqualityComparer<string>.Default.Equals(this.<Title>k__BackingField, other.<Title>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<GroupLabel>k__BackingField, other.<GroupLabel>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<LatestCommentary>k__BackingField, other.<LatestCommentary>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<ThinkingHeading>k__BackingField, other.<ThinkingHeading>k__BackingField) && EqualityComparer<Func<bool>>.Default.Equals(this.<IsActive>k__BackingField, other.<IsActive>k__BackingField));
    }

    // Token: 0x0600016B RID: 363 RVA: 0x000D9EE8 File Offset: 0x000D9EE8
    [CompilerGenerated]
    private AgentBrowserTurn(AgentBrowserTurn original)
    {
      this.Title = original.<Title>k__BackingField;
      this.GroupLabel = original.<GroupLabel>k__BackingField;
      this.LatestCommentary = original.<LatestCommentary>k__BackingField;
      this.ThinkingHeading = original.<ThinkingHeading>k__BackingField;
      this.IsActive = original.<IsActive>k__BackingField;
    }

    // Token: 0x0600016C RID: 364 RVA: 0x000D9F38 File Offset: 0x000D9F38
    [CompilerGenerated]
    public void Deconstruct(out string Title, out string GroupLabel, [Nullable(2)] out string LatestCommentary, [Nullable(2)] out string ThinkingHeading, out Func<bool> IsActive)
    {
      Title = this.Title;
      GroupLabel = this.GroupLabel;
      LatestCommentary = this.LatestCommentary;
      ThinkingHeading = this.ThinkingHeading;
      IsActive = this.IsActive;
    }
  }
}
