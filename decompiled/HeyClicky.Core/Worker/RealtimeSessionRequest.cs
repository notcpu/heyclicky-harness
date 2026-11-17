using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace HeyClicky.Core.Worker
{
  // Token: 0x02000064 RID: 100
  [NullableContext(1)]
  [Nullable(0)]
  public sealed class RealtimeSessionRequest : IEquatable<RealtimeSessionRequest>
  {
    // Token: 0x06000525 RID: 1317 RVA: 0x003579C0 File Offset: 0x003579C0
    public RealtimeSessionRequest(string Voice, double Speed, RealtimeSessionPromptContext PromptContext, IReadOnlyList<string> ClientCapabilities, [Nullable(2)] string MouthProvider = null, [Nullable(2)] string Language = null, [Nullable(new byte[] { 2, 1 })] IReadOnlyList<string> Languages = null)
    {
      this.Voice = Voice;
      this.Speed = Speed;
      this.PromptContext = PromptContext;
      this.ClientCapabilities = ClientCapabilities;
      this.MouthProvider = MouthProvider;
      this.Language = Language;
      this.Languages = Languages;
      base..ctor();
    }

    // Token: 0x17000129 RID: 297
    // (get) Token: 0x06000526 RID: 1318 RVA: 0x00357A00 File Offset: 0x00357A00
    [CompilerGenerated]
    private Type EqualityContract
    {
      [CompilerGenerated]
      get
      {
        return typeof(RealtimeSessionRequest);
      }
    }

    // Token: 0x1700012A RID: 298
    // (get) Token: 0x06000527 RID: 1319 RVA: 0x00357A0C File Offset: 0x00357A0C
    // (set) Token: 0x06000528 RID: 1320 RVA: 0x00357A14 File Offset: 0x00357A14
    public string Voice { get; set; }

    // Token: 0x1700012B RID: 299
    // (get) Token: 0x06000529 RID: 1321 RVA: 0x00357A20 File Offset: 0x00357A20
    // (set) Token: 0x0600052A RID: 1322 RVA: 0x00357A28 File Offset: 0x00357A28
    public double Speed { get; set; }

    // Token: 0x1700012C RID: 300
    // (get) Token: 0x0600052B RID: 1323 RVA: 0x00357A34 File Offset: 0x00357A34
    // (set) Token: 0x0600052C RID: 1324 RVA: 0x00357A3C File Offset: 0x00357A3C
    public RealtimeSessionPromptContext PromptContext { get; set; }

    // Token: 0x1700012D RID: 301
    // (get) Token: 0x0600052D RID: 1325 RVA: 0x00357A48 File Offset: 0x00357A48
    // (set) Token: 0x0600052E RID: 1326 RVA: 0x00357A50 File Offset: 0x00357A50
    public IReadOnlyList<string> ClientCapabilities { get; set; }

    // Token: 0x1700012E RID: 302
    // (get) Token: 0x0600052F RID: 1327 RVA: 0x00357A5C File Offset: 0x00357A5C
    // (set) Token: 0x06000530 RID: 1328 RVA: 0x00357A64 File Offset: 0x00357A64
    [Nullable(2)]
    public string MouthProvider
    {
      [NullableContext(2)]
      get;
      [NullableContext(2)]
      set;
    }

    // Token: 0x1700012F RID: 303
    // (get) Token: 0x06000531 RID: 1329 RVA: 0x00357A70 File Offset: 0x00357A70
    // (set) Token: 0x06000532 RID: 1330 RVA: 0x00357A78 File Offset: 0x00357A78
    [Nullable(2)]
    public string Language
    {
      [NullableContext(2)]
      get;
      [NullableContext(2)]
      set;
    }

    // Token: 0x17000130 RID: 304
    // (get) Token: 0x06000533 RID: 1331 RVA: 0x00357A84 File Offset: 0x00357A84
    // (set) Token: 0x06000534 RID: 1332 RVA: 0x00357A8C File Offset: 0x00357A8C
    [Nullable(new byte[] { 2, 1 })]
    public IReadOnlyList<string> Languages
    {
      [return: Nullable(new byte[] { 2, 1 })]
      get;
      [param: Nullable(new byte[] { 2, 1 })]
      set;
    }

    // Token: 0x06000535 RID: 1333 RVA: 0x00357A98 File Offset: 0x00357A98
    [CompilerGenerated]
    public override string ToString()
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("RealtimeSessionRequest");
      stringBuilder.Append(" { ");
      if (this.PrintMembers(stringBuilder))
      {
        stringBuilder.Append(' ');
      }
      stringBuilder.Append('}');
      return stringBuilder.ToString();
    }

    // Token: 0x06000536 RID: 1334 RVA: 0x00357AE4 File Offset: 0x00357AE4
    [CompilerGenerated]
    private bool PrintMembers(StringBuilder builder)
    {
      RuntimeHelpers.EnsureSufficientExecutionStack();
      builder.Append("Voice = ");
      builder.Append(this.Voice);
      builder.Append(", Speed = ");
      builder.Append(this.Speed.ToString());
      builder.Append(", PromptContext = ");
      builder.Append(this.PromptContext);
      builder.Append(", ClientCapabilities = ");
      builder.Append(this.ClientCapabilities);
      builder.Append(", MouthProvider = ");
      builder.Append(this.MouthProvider);
      builder.Append(", Language = ");
      builder.Append(this.Language);
      builder.Append(", Languages = ");
      builder.Append(this.Languages);
      return true;
    }

    // Token: 0x06000537 RID: 1335 RVA: 0x00357BB4 File Offset: 0x00357BB4
    [NullableContext(2)]
    [CompilerGenerated]
    public static bool operator !=(RealtimeSessionRequest left, RealtimeSessionRequest right)
    {
      return !(left == right);
    }

    // Token: 0x06000538 RID: 1336 RVA: 0x00357BC0 File Offset: 0x00357BC0
    [NullableContext(2)]
    [CompilerGenerated]
    public static bool operator ==(RealtimeSessionRequest left, RealtimeSessionRequest right)
    {
      return left == right || (left != null && left.Equals(right));
    }

    // Token: 0x06000539 RID: 1337 RVA: 0x00357BD4 File Offset: 0x00357BD4
    [CompilerGenerated]
    public override int GetHashCode()
    {
      return ((((((EqualityComparer<Type>.Default.GetHashCode(this.EqualityContract) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Voice>k__BackingField)) * -1521134295 + EqualityComparer<double>.Default.GetHashCode(this.<Speed>k__BackingField)) * -1521134295 + EqualityComparer<RealtimeSessionPromptContext>.Default.GetHashCode(this.<PromptContext>k__BackingField)) * -1521134295 + EqualityComparer<IReadOnlyList<string>>.Default.GetHashCode(this.<ClientCapabilities>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<MouthProvider>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Language>k__BackingField)) * -1521134295 + EqualityComparer<IReadOnlyList<string>>.Default.GetHashCode(this.<Languages>k__BackingField);
    }

    // Token: 0x0600053A RID: 1338 RVA: 0x00357C94 File Offset: 0x00357C94
    [NullableContext(2)]
    [CompilerGenerated]
    public override bool Equals(object obj)
    {
      return this.Equals(obj as RealtimeSessionRequest);
    }

    // Token: 0x0600053B RID: 1339 RVA: 0x00357CA4 File Offset: 0x00357CA4
    [NullableContext(2)]
    [CompilerGenerated]
    public bool Equals(RealtimeSessionRequest other)
    {
      return this == other || (other != null && this.EqualityContract == other.EqualityContract && EqualityComparer<string>.Default.Equals(this.<Voice>k__BackingField, other.<Voice>k__BackingField) && EqualityComparer<double>.Default.Equals(this.<Speed>k__BackingField, other.<Speed>k__BackingField) && EqualityComparer<RealtimeSessionPromptContext>.Default.Equals(this.<PromptContext>k__BackingField, other.<PromptContext>k__BackingField) && EqualityComparer<IReadOnlyList<string>>.Default.Equals(this.<ClientCapabilities>k__BackingField, other.<ClientCapabilities>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<MouthProvider>k__BackingField, other.<MouthProvider>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<Language>k__BackingField, other.<Language>k__BackingField) && EqualityComparer<IReadOnlyList<string>>.Default.Equals(this.<Languages>k__BackingField, other.<Languages>k__BackingField));
    }

    // Token: 0x0600053D RID: 1341 RVA: 0x00357D8C File Offset: 0x00357D8C
    [CompilerGenerated]
    private RealtimeSessionRequest(RealtimeSessionRequest original)
    {
      this.Voice = original.<Voice>k__BackingField;
      this.Speed = original.<Speed>k__BackingField;
      this.PromptContext = original.<PromptContext>k__BackingField;
      this.ClientCapabilities = original.<ClientCapabilities>k__BackingField;
      this.MouthProvider = original.<MouthProvider>k__BackingField;
      this.Language = original.<Language>k__BackingField;
      this.Languages = original.<Languages>k__BackingField;
    }

    // Token: 0x0600053E RID: 1342 RVA: 0x00357DF4 File Offset: 0x00357DF4
    [CompilerGenerated]
    public void Deconstruct(out string Voice, out double Speed, out RealtimeSessionPromptContext PromptContext, out IReadOnlyList<string> ClientCapabilities, [Nullable(2)] out string MouthProvider, [Nullable(2)] out string Language, [Nullable(new byte[] { 2, 1 })] out IReadOnlyList<string> Languages)
    {
      Voice = this.Voice;
      Speed = this.Speed;
      PromptContext = this.PromptContext;
      ClientCapabilities = this.ClientCapabilities;
      MouthProvider = this.MouthProvider;
      Language = this.Language;
      Languages = this.Languages;
    }
  }
}
