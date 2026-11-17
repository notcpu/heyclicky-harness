using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace HeyClicky.Core.Worker
{
  // Token: 0x02000066 RID: 102
  [NullableContext(1)]
  [Nullable(0)]
  public sealed class EarsTranscribeRequest : IEquatable<EarsTranscribeRequest>
  {
    // Token: 0x06000551 RID: 1361 RVA: 0x003580A0 File Offset: 0x003580A0
    public EarsTranscribeRequest(string AudioWavBase64, string Language)
    {
      this.AudioWavBase64 = AudioWavBase64;
      this.Language = Language;
      base..ctor();
    }

    // Token: 0x17000135 RID: 309
    // (get) Token: 0x06000552 RID: 1362 RVA: 0x003580B8 File Offset: 0x003580B8
    [CompilerGenerated]
    private Type EqualityContract
    {
      [CompilerGenerated]
      get
      {
        return typeof(EarsTranscribeRequest);
      }
    }

    // Token: 0x17000136 RID: 310
    // (get) Token: 0x06000553 RID: 1363 RVA: 0x003580C4 File Offset: 0x003580C4
    // (set) Token: 0x06000554 RID: 1364 RVA: 0x003580CC File Offset: 0x003580CC
    public string AudioWavBase64 { get; set; }

    // Token: 0x17000137 RID: 311
    // (get) Token: 0x06000555 RID: 1365 RVA: 0x003580D8 File Offset: 0x003580D8
    // (set) Token: 0x06000556 RID: 1366 RVA: 0x003580E0 File Offset: 0x003580E0
    public string Language { get; set; }

    // Token: 0x06000557 RID: 1367 RVA: 0x003580EC File Offset: 0x003580EC
    [CompilerGenerated]
    public override string ToString()
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("EarsTranscribeRequest");
      stringBuilder.Append(" { ");
      if (this.PrintMembers(stringBuilder))
      {
        stringBuilder.Append(' ');
      }
      stringBuilder.Append('}');
      return stringBuilder.ToString();
    }

    // Token: 0x06000558 RID: 1368 RVA: 0x00358138 File Offset: 0x00358138
    [CompilerGenerated]
    private bool PrintMembers(StringBuilder builder)
    {
      RuntimeHelpers.EnsureSufficientExecutionStack();
      builder.Append("AudioWavBase64 = ");
      builder.Append(this.AudioWavBase64);
      builder.Append(", Language = ");
      builder.Append(this.Language);
      return true;
    }

    // Token: 0x06000559 RID: 1369 RVA: 0x00358174 File Offset: 0x00358174
    [NullableContext(2)]
    [CompilerGenerated]
    public static bool operator !=(EarsTranscribeRequest left, EarsTranscribeRequest right)
    {
      return !(left == right);
    }

    // Token: 0x0600055A RID: 1370 RVA: 0x00358180 File Offset: 0x00358180
    [NullableContext(2)]
    [CompilerGenerated]
    public static bool operator ==(EarsTranscribeRequest left, EarsTranscribeRequest right)
    {
      return left == right || (left != null && left.Equals(right));
    }

    // Token: 0x0600055B RID: 1371 RVA: 0x00358194 File Offset: 0x00358194
    [CompilerGenerated]
    public override int GetHashCode()
    {
      return (EqualityComparer<Type>.Default.GetHashCode(this.EqualityContract) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<AudioWavBase64>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Language>k__BackingField);
    }

    // Token: 0x0600055C RID: 1372 RVA: 0x003581D4 File Offset: 0x003581D4
    [NullableContext(2)]
    [CompilerGenerated]
    public override bool Equals(object obj)
    {
      return this.Equals(obj as EarsTranscribeRequest);
    }

    // Token: 0x0600055D RID: 1373 RVA: 0x003581E4 File Offset: 0x003581E4
    [NullableContext(2)]
    [CompilerGenerated]
    public bool Equals(EarsTranscribeRequest other)
    {
      return this == other || (other != null && this.EqualityContract == other.EqualityContract && EqualityComparer<string>.Default.Equals(this.<AudioWavBase64>k__BackingField, other.<AudioWavBase64>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<Language>k__BackingField, other.<Language>k__BackingField));
    }

    // Token: 0x0600055F RID: 1375 RVA: 0x00358248 File Offset: 0x00358248
    [CompilerGenerated]
    private EarsTranscribeRequest(EarsTranscribeRequest original)
    {
      this.AudioWavBase64 = original.<AudioWavBase64>k__BackingField;
      this.Language = original.<Language>k__BackingField;
    }

    // Token: 0x06000560 RID: 1376 RVA: 0x00358268 File Offset: 0x00358268
    [CompilerGenerated]
    public void Deconstruct(out string AudioWavBase64, out string Language)
    {
      AudioWavBase64 = this.AudioWavBase64;
      Language = this.Language;
    }
  }
}
