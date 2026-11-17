using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace HeyClicky.Core.Worker
{
  // Token: 0x0200005E RID: 94
  [NullableContext(1)]
  [Nullable(0)]
  public sealed class PortalResponse : IEquatable<PortalResponse>
  {
    // Token: 0x060004B3 RID: 1203 RVA: 0x003569A4 File Offset: 0x003569A4
    public PortalResponse(string Url)
    {
      this.Url = Url;
      base..ctor();
    }

    // Token: 0x1700010E RID: 270
    // (get) Token: 0x060004B4 RID: 1204 RVA: 0x003569B4 File Offset: 0x003569B4
    [CompilerGenerated]
    private Type EqualityContract
    {
      [CompilerGenerated]
      get
      {
        return typeof(PortalResponse);
      }
    }

    // Token: 0x1700010F RID: 271
    // (get) Token: 0x060004B5 RID: 1205 RVA: 0x003569C0 File Offset: 0x003569C0
    // (set) Token: 0x060004B6 RID: 1206 RVA: 0x003569C8 File Offset: 0x003569C8
    public string Url { get; set; }

    // Token: 0x060004B7 RID: 1207 RVA: 0x003569D4 File Offset: 0x003569D4
    [CompilerGenerated]
    public override string ToString()
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("PortalResponse");
      stringBuilder.Append(" { ");
      if (this.PrintMembers(stringBuilder))
      {
        stringBuilder.Append(' ');
      }
      stringBuilder.Append('}');
      return stringBuilder.ToString();
    }

    // Token: 0x060004B8 RID: 1208 RVA: 0x00356A20 File Offset: 0x00356A20
    [CompilerGenerated]
    private bool PrintMembers(StringBuilder builder)
    {
      RuntimeHelpers.EnsureSufficientExecutionStack();
      builder.Append("Url = ");
      builder.Append(this.Url);
      return true;
    }

    // Token: 0x060004B9 RID: 1209 RVA: 0x00356A44 File Offset: 0x00356A44
    [NullableContext(2)]
    [CompilerGenerated]
    public static bool operator !=(PortalResponse left, PortalResponse right)
    {
      return !(left == right);
    }

    // Token: 0x060004BA RID: 1210 RVA: 0x00356A50 File Offset: 0x00356A50
    [NullableContext(2)]
    [CompilerGenerated]
    public static bool operator ==(PortalResponse left, PortalResponse right)
    {
      return left == right || (left != null && left.Equals(right));
    }

    // Token: 0x060004BB RID: 1211 RVA: 0x00356A64 File Offset: 0x00356A64
    [CompilerGenerated]
    public override int GetHashCode()
    {
      return EqualityComparer<Type>.Default.GetHashCode(this.EqualityContract) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Url>k__BackingField);
    }

    // Token: 0x060004BC RID: 1212 RVA: 0x00356A90 File Offset: 0x00356A90
    [NullableContext(2)]
    [CompilerGenerated]
    public override bool Equals(object obj)
    {
      return this.Equals(obj as PortalResponse);
    }

    // Token: 0x060004BD RID: 1213 RVA: 0x00356AA0 File Offset: 0x00356AA0
    [NullableContext(2)]
    [CompilerGenerated]
    public bool Equals(PortalResponse other)
    {
      return this == other || (other != null && this.EqualityContract == other.EqualityContract && EqualityComparer<string>.Default.Equals(this.<Url>k__BackingField, other.<Url>k__BackingField));
    }

    // Token: 0x060004BF RID: 1215 RVA: 0x00356AE0 File Offset: 0x00356AE0
    [CompilerGenerated]
    private PortalResponse(PortalResponse original)
    {
      this.Url = original.<Url>k__BackingField;
    }

    // Token: 0x060004C0 RID: 1216 RVA: 0x00356AF4 File Offset: 0x00356AF4
    [CompilerGenerated]
    public void Deconstruct(out string Url)
    {
      Url = this.Url;
    }
  }
}
