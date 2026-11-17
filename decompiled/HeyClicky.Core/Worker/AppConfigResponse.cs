using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json.Serialization;

namespace HeyClicky.Core.Worker
{
  // Token: 0x02000043 RID: 67
  [NullableContext(1)]
  [Nullable(0)]
  public sealed class AppConfigResponse : IEquatable<AppConfigResponse>
  {
    // Token: 0x0600029D RID: 669 RVA: 0x00351C50 File Offset: 0x00351C50
    public AppConfigResponse(AppConfig AppConfig)
    {
      this.AppConfig = AppConfig;
      base..ctor();
    }

    // Token: 0x1700008A RID: 138
    // (get) Token: 0x0600029E RID: 670 RVA: 0x00351C60 File Offset: 0x00351C60
    [CompilerGenerated]
    private Type EqualityContract
    {
      [CompilerGenerated]
      get
      {
        return typeof(AppConfigResponse);
      }
    }

    // Token: 0x1700008B RID: 139
    // (get) Token: 0x0600029F RID: 671 RVA: 0x00351C6C File Offset: 0x00351C6C
    // (set) Token: 0x060002A0 RID: 672 RVA: 0x00351C74 File Offset: 0x00351C74
    [JsonPropertyName("appConfig")]
    public AppConfig AppConfig { get; set; }

    // Token: 0x060002A1 RID: 673 RVA: 0x00351C80 File Offset: 0x00351C80
    [CompilerGenerated]
    public override string ToString()
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("AppConfigResponse");
      stringBuilder.Append(" { ");
      if (this.PrintMembers(stringBuilder))
      {
        stringBuilder.Append(' ');
      }
      stringBuilder.Append('}');
      return stringBuilder.ToString();
    }

    // Token: 0x060002A2 RID: 674 RVA: 0x00351CCC File Offset: 0x00351CCC
    [CompilerGenerated]
    private bool PrintMembers(StringBuilder builder)
    {
      RuntimeHelpers.EnsureSufficientExecutionStack();
      builder.Append("AppConfig = ");
      builder.Append(this.AppConfig);
      return true;
    }

    // Token: 0x060002A3 RID: 675 RVA: 0x00351CF0 File Offset: 0x00351CF0
    [NullableContext(2)]
    [CompilerGenerated]
    public static bool operator !=(AppConfigResponse left, AppConfigResponse right)
    {
      return !(left == right);
    }

    // Token: 0x060002A4 RID: 676 RVA: 0x00351CFC File Offset: 0x00351CFC
    [NullableContext(2)]
    [CompilerGenerated]
    public static bool operator ==(AppConfigResponse left, AppConfigResponse right)
    {
      return left == right || (left != null && left.Equals(right));
    }

    // Token: 0x060002A5 RID: 677 RVA: 0x00351D10 File Offset: 0x00351D10
    [CompilerGenerated]
    public override int GetHashCode()
    {
      return EqualityComparer<Type>.Default.GetHashCode(this.EqualityContract) * -1521134295 + EqualityComparer<AppConfig>.Default.GetHashCode(this.<AppConfig>k__BackingField);
    }

    // Token: 0x060002A6 RID: 678 RVA: 0x00351D3C File Offset: 0x00351D3C
    [NullableContext(2)]
    [CompilerGenerated]
    public override bool Equals(object obj)
    {
      return this.Equals(obj as AppConfigResponse);
    }

    // Token: 0x060002A7 RID: 679 RVA: 0x00351D4C File Offset: 0x00351D4C
    [NullableContext(2)]
    [CompilerGenerated]
    public bool Equals(AppConfigResponse other)
    {
      return this == other || (other != null && this.EqualityContract == other.EqualityContract && EqualityComparer<AppConfig>.Default.Equals(this.<AppConfig>k__BackingField, other.<AppConfig>k__BackingField));
    }

    // Token: 0x060002A9 RID: 681 RVA: 0x00351D8C File Offset: 0x00351D8C
    [CompilerGenerated]
    private AppConfigResponse(AppConfigResponse original)
    {
      this.AppConfig = original.<AppConfig>k__BackingField;
    }

    // Token: 0x060002AA RID: 682 RVA: 0x00351DA0 File Offset: 0x00351DA0
    [CompilerGenerated]
    public void Deconstruct(out AppConfig AppConfig)
    {
      AppConfig = this.AppConfig;
    }
  }
}
