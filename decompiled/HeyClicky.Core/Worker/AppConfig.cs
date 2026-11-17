using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace HeyClicky.Core.Worker
{
  // Token: 0x02000042 RID: 66
  [NullableContext(1)]
  [Nullable(0)]
  public sealed class AppConfig : IEquatable<AppConfig>
  {
    // Token: 0x0600028F RID: 655 RVA: 0x00351AF4 File Offset: 0x00351AF4
    public AppConfig(AppConfigLinks Links)
    {
      this.Links = Links;
      base..ctor();
    }

    // Token: 0x17000088 RID: 136
    // (get) Token: 0x06000290 RID: 656 RVA: 0x00351B04 File Offset: 0x00351B04
    [CompilerGenerated]
    private Type EqualityContract
    {
      [CompilerGenerated]
      get
      {
        return typeof(AppConfig);
      }
    }

    // Token: 0x17000089 RID: 137
    // (get) Token: 0x06000291 RID: 657 RVA: 0x00351B10 File Offset: 0x00351B10
    // (set) Token: 0x06000292 RID: 658 RVA: 0x00351B18 File Offset: 0x00351B18
    public AppConfigLinks Links { get; set; }

    // Token: 0x06000293 RID: 659 RVA: 0x00351B24 File Offset: 0x00351B24
    [CompilerGenerated]
    public override string ToString()
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("AppConfig");
      stringBuilder.Append(" { ");
      if (this.PrintMembers(stringBuilder))
      {
        stringBuilder.Append(' ');
      }
      stringBuilder.Append('}');
      return stringBuilder.ToString();
    }

    // Token: 0x06000294 RID: 660 RVA: 0x00351B70 File Offset: 0x00351B70
    [CompilerGenerated]
    private bool PrintMembers(StringBuilder builder)
    {
      RuntimeHelpers.EnsureSufficientExecutionStack();
      builder.Append("Links = ");
      builder.Append(this.Links);
      return true;
    }

    // Token: 0x06000295 RID: 661 RVA: 0x00351B94 File Offset: 0x00351B94
    [NullableContext(2)]
    [CompilerGenerated]
    public static bool operator !=(AppConfig left, AppConfig right)
    {
      return !(left == right);
    }

    // Token: 0x06000296 RID: 662 RVA: 0x00351BA0 File Offset: 0x00351BA0
    [NullableContext(2)]
    [CompilerGenerated]
    public static bool operator ==(AppConfig left, AppConfig right)
    {
      return left == right || (left != null && left.Equals(right));
    }

    // Token: 0x06000297 RID: 663 RVA: 0x00351BB4 File Offset: 0x00351BB4
    [CompilerGenerated]
    public override int GetHashCode()
    {
      return EqualityComparer<Type>.Default.GetHashCode(this.EqualityContract) * -1521134295 + EqualityComparer<AppConfigLinks>.Default.GetHashCode(this.<Links>k__BackingField);
    }

    // Token: 0x06000298 RID: 664 RVA: 0x00351BE0 File Offset: 0x00351BE0
    [NullableContext(2)]
    [CompilerGenerated]
    public override bool Equals(object obj)
    {
      return this.Equals(obj as AppConfig);
    }

    // Token: 0x06000299 RID: 665 RVA: 0x00351BF0 File Offset: 0x00351BF0
    [NullableContext(2)]
    [CompilerGenerated]
    public bool Equals(AppConfig other)
    {
      return this == other || (other != null && this.EqualityContract == other.EqualityContract && EqualityComparer<AppConfigLinks>.Default.Equals(this.<Links>k__BackingField, other.<Links>k__BackingField));
    }

    // Token: 0x0600029B RID: 667 RVA: 0x00351C30 File Offset: 0x00351C30
    [CompilerGenerated]
    private AppConfig(AppConfig original)
    {
      this.Links = original.<Links>k__BackingField;
    }

    // Token: 0x0600029C RID: 668 RVA: 0x00351C44 File Offset: 0x00351C44
    [CompilerGenerated]
    public void Deconstruct(out AppConfigLinks Links)
    {
      Links = this.Links;
    }
  }
}
