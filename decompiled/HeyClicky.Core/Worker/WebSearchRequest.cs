using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace HeyClicky.Core.Worker
{
  // Token: 0x02000059 RID: 89
  [NullableContext(1)]
  [Nullable(0)]
  public sealed class WebSearchRequest : IEquatable<WebSearchRequest>
  {
    // Token: 0x06000457 RID: 1111 RVA: 0x00355D24 File Offset: 0x00355D24
    public WebSearchRequest(string Query, int? Count = null, [Nullable(2)] string Freshness = null)
    {
      this.Query = Query;
      this.Count = Count;
      this.Freshness = Freshness;
      base..ctor();
    }

    // Token: 0x170000F9 RID: 249
    // (get) Token: 0x06000458 RID: 1112 RVA: 0x00355D44 File Offset: 0x00355D44
    [CompilerGenerated]
    private Type EqualityContract
    {
      [CompilerGenerated]
      get
      {
        return typeof(WebSearchRequest);
      }
    }

    // Token: 0x170000FA RID: 250
    // (get) Token: 0x06000459 RID: 1113 RVA: 0x00355D50 File Offset: 0x00355D50
    // (set) Token: 0x0600045A RID: 1114 RVA: 0x00355D58 File Offset: 0x00355D58
    public string Query { get; set; }

    // Token: 0x170000FB RID: 251
    // (get) Token: 0x0600045B RID: 1115 RVA: 0x00355D64 File Offset: 0x00355D64
    // (set) Token: 0x0600045C RID: 1116 RVA: 0x00355D6C File Offset: 0x00355D6C
    public int? Count { get; set; }

    // Token: 0x170000FC RID: 252
    // (get) Token: 0x0600045D RID: 1117 RVA: 0x00355D78 File Offset: 0x00355D78
    // (set) Token: 0x0600045E RID: 1118 RVA: 0x00355D80 File Offset: 0x00355D80
    [Nullable(2)]
    public string Freshness
    {
      [NullableContext(2)]
      get;
      [NullableContext(2)]
      set;
    }

    // Token: 0x0600045F RID: 1119 RVA: 0x00355D8C File Offset: 0x00355D8C
    [CompilerGenerated]
    public override string ToString()
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("WebSearchRequest");
      stringBuilder.Append(" { ");
      if (this.PrintMembers(stringBuilder))
      {
        stringBuilder.Append(' ');
      }
      stringBuilder.Append('}');
      return stringBuilder.ToString();
    }

    // Token: 0x06000460 RID: 1120 RVA: 0x00355DD8 File Offset: 0x00355DD8
    [CompilerGenerated]
    private bool PrintMembers(StringBuilder builder)
    {
      RuntimeHelpers.EnsureSufficientExecutionStack();
      builder.Append("Query = ");
      builder.Append(this.Query);
      builder.Append(", Count = ");
      builder.Append(this.Count.ToString());
      builder.Append(", Freshness = ");
      builder.Append(this.Freshness);
      return true;
    }

    // Token: 0x06000461 RID: 1121 RVA: 0x00355E44 File Offset: 0x00355E44
    [NullableContext(2)]
    [CompilerGenerated]
    public static bool operator !=(WebSearchRequest left, WebSearchRequest right)
    {
      return !(left == right);
    }

    // Token: 0x06000462 RID: 1122 RVA: 0x00355E50 File Offset: 0x00355E50
    [NullableContext(2)]
    [CompilerGenerated]
    public static bool operator ==(WebSearchRequest left, WebSearchRequest right)
    {
      return left == right || (left != null && left.Equals(right));
    }

    // Token: 0x06000463 RID: 1123 RVA: 0x00355E64 File Offset: 0x00355E64
    [CompilerGenerated]
    public override int GetHashCode()
    {
      return ((EqualityComparer<Type>.Default.GetHashCode(this.EqualityContract) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Query>k__BackingField)) * -1521134295 + EqualityComparer<int?>.Default.GetHashCode(this.<Count>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Freshness>k__BackingField);
    }

    // Token: 0x06000464 RID: 1124 RVA: 0x00355EC8 File Offset: 0x00355EC8
    [NullableContext(2)]
    [CompilerGenerated]
    public override bool Equals(object obj)
    {
      return this.Equals(obj as WebSearchRequest);
    }

    // Token: 0x06000465 RID: 1125 RVA: 0x00355ED8 File Offset: 0x00355ED8
    [NullableContext(2)]
    [CompilerGenerated]
    public bool Equals(WebSearchRequest other)
    {
      return this == other || (other != null && this.EqualityContract == other.EqualityContract && EqualityComparer<string>.Default.Equals(this.<Query>k__BackingField, other.<Query>k__BackingField) && EqualityComparer<int?>.Default.Equals(this.<Count>k__BackingField, other.<Count>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<Freshness>k__BackingField, other.<Freshness>k__BackingField));
    }

    // Token: 0x06000467 RID: 1127 RVA: 0x00355F54 File Offset: 0x00355F54
    [CompilerGenerated]
    private WebSearchRequest(WebSearchRequest original)
    {
      this.Query = original.<Query>k__BackingField;
      this.Count = original.<Count>k__BackingField;
      this.Freshness = original.<Freshness>k__BackingField;
    }

    // Token: 0x06000468 RID: 1128 RVA: 0x00355F80 File Offset: 0x00355F80
    [CompilerGenerated]
    public void Deconstruct(out string Query, out int? Count, [Nullable(2)] out string Freshness)
    {
      Query = this.Query;
      Count = this.Count;
      Freshness = this.Freshness;
    }
  }
}
