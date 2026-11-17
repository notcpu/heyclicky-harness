using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace HeyClicky.App.Portraits
{
  // Token: 0x020000F7 RID: 247
  [NullableContext(1)]
  [Nullable(0)]
  public sealed class RosterClicky : IEquatable<RosterClicky>
  {
    // Token: 0x06000CD6 RID: 3286 RVA: 0x00563B64 File Offset: 0x00563B64
    public RosterClicky(string Slug, string Name, string RoleText, string OneLiner, IReadOnlyList<string> IntroMessages, IReadOnlyList<string> SuggestedAsks, [Nullable(2)] string RevealLine, DateTimeOffset CreatedAt)
    {
      this.Slug = Slug;
      this.Name = Name;
      this.RoleText = RoleText;
      this.OneLiner = OneLiner;
      this.IntroMessages = IntroMessages;
      this.SuggestedAsks = SuggestedAsks;
      this.RevealLine = RevealLine;
      this.CreatedAt = CreatedAt;
      base..ctor();
    }

    // Token: 0x17000243 RID: 579
    // (get) Token: 0x06000CD7 RID: 3287 RVA: 0x00563BB4 File Offset: 0x00563BB4
    [CompilerGenerated]
    private Type EqualityContract
    {
      [CompilerGenerated]
      get
      {
        return typeof(RosterClicky);
      }
    }

    // Token: 0x17000244 RID: 580
    // (get) Token: 0x06000CD8 RID: 3288 RVA: 0x00563BC0 File Offset: 0x00563BC0
    // (set) Token: 0x06000CD9 RID: 3289 RVA: 0x00563BC8 File Offset: 0x00563BC8
    public string Slug { get; set; }

    // Token: 0x17000245 RID: 581
    // (get) Token: 0x06000CDA RID: 3290 RVA: 0x00563BD4 File Offset: 0x00563BD4
    // (set) Token: 0x06000CDB RID: 3291 RVA: 0x00563BDC File Offset: 0x00563BDC
    public string Name { get; set; }

    // Token: 0x17000246 RID: 582
    // (get) Token: 0x06000CDC RID: 3292 RVA: 0x00563BE8 File Offset: 0x00563BE8
    // (set) Token: 0x06000CDD RID: 3293 RVA: 0x00563BF0 File Offset: 0x00563BF0
    public string RoleText { get; set; }

    // Token: 0x17000247 RID: 583
    // (get) Token: 0x06000CDE RID: 3294 RVA: 0x00563BFC File Offset: 0x00563BFC
    // (set) Token: 0x06000CDF RID: 3295 RVA: 0x00563C04 File Offset: 0x00563C04
    public string OneLiner { get; set; }

    // Token: 0x17000248 RID: 584
    // (get) Token: 0x06000CE0 RID: 3296 RVA: 0x00563C10 File Offset: 0x00563C10
    // (set) Token: 0x06000CE1 RID: 3297 RVA: 0x00563C18 File Offset: 0x00563C18
    public IReadOnlyList<string> IntroMessages { get; set; }

    // Token: 0x17000249 RID: 585
    // (get) Token: 0x06000CE2 RID: 3298 RVA: 0x00563C24 File Offset: 0x00563C24
    // (set) Token: 0x06000CE3 RID: 3299 RVA: 0x00563C2C File Offset: 0x00563C2C
    public IReadOnlyList<string> SuggestedAsks { get; set; }

    // Token: 0x1700024A RID: 586
    // (get) Token: 0x06000CE4 RID: 3300 RVA: 0x00563C38 File Offset: 0x00563C38
    // (set) Token: 0x06000CE5 RID: 3301 RVA: 0x00563C40 File Offset: 0x00563C40
    [Nullable(2)]
    public string RevealLine
    {
      [NullableContext(2)]
      get;
      [NullableContext(2)]
      set;
    }

    // Token: 0x1700024B RID: 587
    // (get) Token: 0x06000CE6 RID: 3302 RVA: 0x00563C4C File Offset: 0x00563C4C
    // (set) Token: 0x06000CE7 RID: 3303 RVA: 0x00563C54 File Offset: 0x00563C54
    public DateTimeOffset CreatedAt { get; set; }

    // Token: 0x06000CE8 RID: 3304 RVA: 0x00563C60 File Offset: 0x00563C60
    [CompilerGenerated]
    public override string ToString()
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("RosterClicky");
      stringBuilder.Append(" { ");
      if (this.PrintMembers(stringBuilder))
      {
        stringBuilder.Append(' ');
      }
      stringBuilder.Append('}');
      return stringBuilder.ToString();
    }

    // Token: 0x06000CE9 RID: 3305 RVA: 0x00563CAC File Offset: 0x00563CAC
    [CompilerGenerated]
    private bool PrintMembers(StringBuilder builder)
    {
      RuntimeHelpers.EnsureSufficientExecutionStack();
      builder.Append("Slug = ");
      builder.Append(this.Slug);
      builder.Append(", Name = ");
      builder.Append(this.Name);
      builder.Append(", RoleText = ");
      builder.Append(this.RoleText);
      builder.Append(", OneLiner = ");
      builder.Append(this.OneLiner);
      builder.Append(", IntroMessages = ");
      builder.Append(this.IntroMessages);
      builder.Append(", SuggestedAsks = ");
      builder.Append(this.SuggestedAsks);
      builder.Append(", RevealLine = ");
      builder.Append(this.RevealLine);
      builder.Append(", CreatedAt = ");
      builder.Append(this.CreatedAt.ToString());
      return true;
    }

    // Token: 0x06000CEA RID: 3306 RVA: 0x00563D98 File Offset: 0x00563D98
    [NullableContext(2)]
    [CompilerGenerated]
    public static bool operator !=(RosterClicky left, RosterClicky right)
    {
      return !(left == right);
    }

    // Token: 0x06000CEB RID: 3307 RVA: 0x00563DA4 File Offset: 0x00563DA4
    [NullableContext(2)]
    [CompilerGenerated]
    public static bool operator ==(RosterClicky left, RosterClicky right)
    {
      return left == right || (left != null && left.Equals(right));
    }

    // Token: 0x06000CEC RID: 3308 RVA: 0x00563DB8 File Offset: 0x00563DB8
    [CompilerGenerated]
    public override int GetHashCode()
    {
      return (((((((EqualityComparer<Type>.Default.GetHashCode(this.EqualityContract) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Slug>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Name>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<RoleText>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<OneLiner>k__BackingField)) * -1521134295 + EqualityComparer<IReadOnlyList<string>>.Default.GetHashCode(this.<IntroMessages>k__BackingField)) * -1521134295 + EqualityComparer<IReadOnlyList<string>>.Default.GetHashCode(this.<SuggestedAsks>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<RevealLine>k__BackingField)) * -1521134295 + EqualityComparer<DateTimeOffset>.Default.GetHashCode(this.<CreatedAt>k__BackingField);
    }

    // Token: 0x06000CED RID: 3309 RVA: 0x00563E90 File Offset: 0x00563E90
    [NullableContext(2)]
    [CompilerGenerated]
    public override bool Equals(object obj)
    {
      return this.Equals(obj as RosterClicky);
    }

    // Token: 0x06000CEE RID: 3310 RVA: 0x00563EA0 File Offset: 0x00563EA0
    [NullableContext(2)]
    [CompilerGenerated]
    public bool Equals(RosterClicky other)
    {
      return this == other || (other != null && this.EqualityContract == other.EqualityContract && EqualityComparer<string>.Default.Equals(this.<Slug>k__BackingField, other.<Slug>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<Name>k__BackingField, other.<Name>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<RoleText>k__BackingField, other.<RoleText>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<OneLiner>k__BackingField, other.<OneLiner>k__BackingField) && EqualityComparer<IReadOnlyList<string>>.Default.Equals(this.<IntroMessages>k__BackingField, other.<IntroMessages>k__BackingField) && EqualityComparer<IReadOnlyList<string>>.Default.Equals(this.<SuggestedAsks>k__BackingField, other.<SuggestedAsks>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<RevealLine>k__BackingField, other.<RevealLine>k__BackingField) && EqualityComparer<DateTimeOffset>.Default.Equals(this.<CreatedAt>k__BackingField, other.<CreatedAt>k__BackingField));
    }

    // Token: 0x06000CF0 RID: 3312 RVA: 0x00563FA0 File Offset: 0x00563FA0
    [CompilerGenerated]
    private RosterClicky(RosterClicky original)
    {
      this.Slug = original.<Slug>k__BackingField;
      this.Name = original.<Name>k__BackingField;
      this.RoleText = original.<RoleText>k__BackingField;
      this.OneLiner = original.<OneLiner>k__BackingField;
      this.IntroMessages = original.<IntroMessages>k__BackingField;
      this.SuggestedAsks = original.<SuggestedAsks>k__BackingField;
      this.RevealLine = original.<RevealLine>k__BackingField;
      this.CreatedAt = original.<CreatedAt>k__BackingField;
    }

    // Token: 0x06000CF1 RID: 3313 RVA: 0x00564014 File Offset: 0x00564014
    [CompilerGenerated]
    public void Deconstruct(out string Slug, out string Name, out string RoleText, out string OneLiner, out IReadOnlyList<string> IntroMessages, out IReadOnlyList<string> SuggestedAsks, [Nullable(2)] out string RevealLine, out DateTimeOffset CreatedAt)
    {
      Slug = this.Slug;
      Name = this.Name;
      RoleText = this.RoleText;
      OneLiner = this.OneLiner;
      IntroMessages = this.IntroMessages;
      SuggestedAsks = this.SuggestedAsks;
      RevealLine = this.RevealLine;
      CreatedAt = this.CreatedAt;
    }
  }
}
