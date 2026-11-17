using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace HeyClicky
{
  // Token: 0x02000003 RID: 3
  [NullableContext(1)]
  [Nullable(0)]
  public sealed class ClickyEndpoints : IEquatable<ClickyEndpoints>
  {
    // Token: 0x06000007 RID: 7 RVA: 0x0001C2AC File Offset: 0x0001C2AC
    public ClickyEndpoints(Uri WorkerBaseUrl, Uri SupabaseUrl, string SupabaseAnonKey)
    {
      this.WorkerBaseUrl = WorkerBaseUrl;
      this.SupabaseUrl = SupabaseUrl;
      this.SupabaseAnonKey = SupabaseAnonKey;
      base..ctor();
    }

    // Token: 0x17000004 RID: 4
    // (get) Token: 0x06000008 RID: 8 RVA: 0x0001C2CC File Offset: 0x0001C2CC
    [CompilerGenerated]
    private Type EqualityContract
    {
      [CompilerGenerated]
      get
      {
        return typeof(ClickyEndpoints);
      }
    }

    // Token: 0x17000005 RID: 5
    // (get) Token: 0x06000009 RID: 9 RVA: 0x0001C2D8 File Offset: 0x0001C2D8
    // (set) Token: 0x0600000A RID: 10 RVA: 0x0001C2E0 File Offset: 0x0001C2E0
    public Uri WorkerBaseUrl { get; set; }

    // Token: 0x17000006 RID: 6
    // (get) Token: 0x0600000B RID: 11 RVA: 0x0001C2EC File Offset: 0x0001C2EC
    // (set) Token: 0x0600000C RID: 12 RVA: 0x0001C2F4 File Offset: 0x0001C2F4
    public Uri SupabaseUrl { get; set; }

    // Token: 0x17000007 RID: 7
    // (get) Token: 0x0600000D RID: 13 RVA: 0x0001C300 File Offset: 0x0001C300
    // (set) Token: 0x0600000E RID: 14 RVA: 0x0001C308 File Offset: 0x0001C308
    public string SupabaseAnonKey { get; set; }

    // Token: 0x0600000F RID: 15 RVA: 0x0001C314 File Offset: 0x0001C314
    [CompilerGenerated]
    public override string ToString()
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("ClickyEndpoints");
      stringBuilder.Append(" { ");
      if (this.PrintMembers(stringBuilder))
      {
        stringBuilder.Append(' ');
      }
      stringBuilder.Append('}');
      return stringBuilder.ToString();
    }

    // Token: 0x06000010 RID: 16 RVA: 0x0001C360 File Offset: 0x0001C360
    [CompilerGenerated]
    private bool PrintMembers(StringBuilder builder)
    {
      RuntimeHelpers.EnsureSufficientExecutionStack();
      builder.Append("WorkerBaseUrl = ");
      builder.Append(this.WorkerBaseUrl);
      builder.Append(", SupabaseUrl = ");
      builder.Append(this.SupabaseUrl);
      builder.Append(", SupabaseAnonKey = ");
      builder.Append(this.SupabaseAnonKey);
      return true;
    }

    // Token: 0x06000011 RID: 17 RVA: 0x0001C3C0 File Offset: 0x0001C3C0
    [NullableContext(2)]
    [CompilerGenerated]
    public static bool operator !=(ClickyEndpoints left, ClickyEndpoints right)
    {
      return !(left == right);
    }

    // Token: 0x06000012 RID: 18 RVA: 0x0001C3CC File Offset: 0x0001C3CC
    [NullableContext(2)]
    [CompilerGenerated]
    public static bool operator ==(ClickyEndpoints left, ClickyEndpoints right)
    {
      return left == right || (left != null && left.Equals(right));
    }

    // Token: 0x06000013 RID: 19 RVA: 0x0001C3E0 File Offset: 0x0001C3E0
    [CompilerGenerated]
    public override int GetHashCode()
    {
      return ((EqualityComparer<Type>.Default.GetHashCode(this.EqualityContract) * -1521134295 + EqualityComparer<Uri>.Default.GetHashCode(this.<WorkerBaseUrl>k__BackingField)) * -1521134295 + EqualityComparer<Uri>.Default.GetHashCode(this.<SupabaseUrl>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<SupabaseAnonKey>k__BackingField);
    }

    // Token: 0x06000014 RID: 20 RVA: 0x0001C444 File Offset: 0x0001C444
    [NullableContext(2)]
    [CompilerGenerated]
    public override bool Equals(object obj)
    {
      return this.Equals(obj as ClickyEndpoints);
    }

    // Token: 0x06000015 RID: 21 RVA: 0x0001C454 File Offset: 0x0001C454
    [NullableContext(2)]
    [CompilerGenerated]
    public bool Equals(ClickyEndpoints other)
    {
      return this == other || (other != null && this.EqualityContract == other.EqualityContract && EqualityComparer<Uri>.Default.Equals(this.<WorkerBaseUrl>k__BackingField, other.<WorkerBaseUrl>k__BackingField) && EqualityComparer<Uri>.Default.Equals(this.<SupabaseUrl>k__BackingField, other.<SupabaseUrl>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<SupabaseAnonKey>k__BackingField, other.<SupabaseAnonKey>k__BackingField));
    }

    // Token: 0x06000017 RID: 23 RVA: 0x0001C4D0 File Offset: 0x0001C4D0
    [CompilerGenerated]
    private ClickyEndpoints(ClickyEndpoints original)
    {
      this.WorkerBaseUrl = original.<WorkerBaseUrl>k__BackingField;
      this.SupabaseUrl = original.<SupabaseUrl>k__BackingField;
      this.SupabaseAnonKey = original.<SupabaseAnonKey>k__BackingField;
    }

    // Token: 0x06000018 RID: 24 RVA: 0x0001C4FC File Offset: 0x0001C4FC
    [CompilerGenerated]
    public void Deconstruct(out Uri WorkerBaseUrl, out Uri SupabaseUrl, out string SupabaseAnonKey)
    {
      WorkerBaseUrl = this.WorkerBaseUrl;
      SupabaseUrl = this.SupabaseUrl;
      SupabaseAnonKey = this.SupabaseAnonKey;
    }
  }
}
