using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json;

namespace HeyClicky.Core.Worker
{
  // Token: 0x02000063 RID: 99
  [NullableContext(1)]
  [Nullable(0)]
  public sealed class RealtimeSessionPromptContext : IEquatable<RealtimeSessionPromptContext>
  {
    // Token: 0x06000509 RID: 1289 RVA: 0x003574BC File Offset: 0x003574BC
    public RealtimeSessionPromptContext(IReadOnlyList<string> ConnectedIntegrations, IReadOnlyList<string> NeedsReconnectIntegrations, int PriorMessageCount, string TimeZone, string CurrentLocalDatetime, string VoiceGender, IReadOnlyList<JsonElement> ActiveSkills, [Nullable(2)] string DictationHoldShortcut = null)
    {
      this.ConnectedIntegrations = ConnectedIntegrations;
      this.NeedsReconnectIntegrations = NeedsReconnectIntegrations;
      this.PriorMessageCount = PriorMessageCount;
      this.TimeZone = TimeZone;
      this.CurrentLocalDatetime = CurrentLocalDatetime;
      this.VoiceGender = VoiceGender;
      this.ActiveSkills = ActiveSkills;
      this.DictationHoldShortcut = DictationHoldShortcut;
      base..ctor();
    }

    // Token: 0x17000120 RID: 288
    // (get) Token: 0x0600050A RID: 1290 RVA: 0x0035750C File Offset: 0x0035750C
    [CompilerGenerated]
    private Type EqualityContract
    {
      [CompilerGenerated]
      get
      {
        return typeof(RealtimeSessionPromptContext);
      }
    }

    // Token: 0x17000121 RID: 289
    // (get) Token: 0x0600050B RID: 1291 RVA: 0x00357518 File Offset: 0x00357518
    // (set) Token: 0x0600050C RID: 1292 RVA: 0x00357520 File Offset: 0x00357520
    public IReadOnlyList<string> ConnectedIntegrations { get; set; }

    // Token: 0x17000122 RID: 290
    // (get) Token: 0x0600050D RID: 1293 RVA: 0x0035752C File Offset: 0x0035752C
    // (set) Token: 0x0600050E RID: 1294 RVA: 0x00357534 File Offset: 0x00357534
    public IReadOnlyList<string> NeedsReconnectIntegrations { get; set; }

    // Token: 0x17000123 RID: 291
    // (get) Token: 0x0600050F RID: 1295 RVA: 0x00357540 File Offset: 0x00357540
    // (set) Token: 0x06000510 RID: 1296 RVA: 0x00357548 File Offset: 0x00357548
    public int PriorMessageCount { get; set; }

    // Token: 0x17000124 RID: 292
    // (get) Token: 0x06000511 RID: 1297 RVA: 0x00357554 File Offset: 0x00357554
    // (set) Token: 0x06000512 RID: 1298 RVA: 0x0035755C File Offset: 0x0035755C
    public string TimeZone { get; set; }

    // Token: 0x17000125 RID: 293
    // (get) Token: 0x06000513 RID: 1299 RVA: 0x00357568 File Offset: 0x00357568
    // (set) Token: 0x06000514 RID: 1300 RVA: 0x00357570 File Offset: 0x00357570
    public string CurrentLocalDatetime { get; set; }

    // Token: 0x17000126 RID: 294
    // (get) Token: 0x06000515 RID: 1301 RVA: 0x0035757C File Offset: 0x0035757C
    // (set) Token: 0x06000516 RID: 1302 RVA: 0x00357584 File Offset: 0x00357584
    public string VoiceGender { get; set; }

    // Token: 0x17000127 RID: 295
    // (get) Token: 0x06000517 RID: 1303 RVA: 0x00357590 File Offset: 0x00357590
    // (set) Token: 0x06000518 RID: 1304 RVA: 0x00357598 File Offset: 0x00357598
    public IReadOnlyList<JsonElement> ActiveSkills { get; set; }

    // Token: 0x17000128 RID: 296
    // (get) Token: 0x06000519 RID: 1305 RVA: 0x003575A4 File Offset: 0x003575A4
    // (set) Token: 0x0600051A RID: 1306 RVA: 0x003575AC File Offset: 0x003575AC
    [Nullable(2)]
    public string DictationHoldShortcut
    {
      [NullableContext(2)]
      get;
      [NullableContext(2)]
      set;
    }

    // Token: 0x0600051B RID: 1307 RVA: 0x003575B8 File Offset: 0x003575B8
    [CompilerGenerated]
    public override string ToString()
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("RealtimeSessionPromptContext");
      stringBuilder.Append(" { ");
      if (this.PrintMembers(stringBuilder))
      {
        stringBuilder.Append(' ');
      }
      stringBuilder.Append('}');
      return stringBuilder.ToString();
    }

    // Token: 0x0600051C RID: 1308 RVA: 0x00357604 File Offset: 0x00357604
    [CompilerGenerated]
    private bool PrintMembers(StringBuilder builder)
    {
      RuntimeHelpers.EnsureSufficientExecutionStack();
      builder.Append("ConnectedIntegrations = ");
      builder.Append(this.ConnectedIntegrations);
      builder.Append(", NeedsReconnectIntegrations = ");
      builder.Append(this.NeedsReconnectIntegrations);
      builder.Append(", PriorMessageCount = ");
      builder.Append(this.PriorMessageCount.ToString());
      builder.Append(", TimeZone = ");
      builder.Append(this.TimeZone);
      builder.Append(", CurrentLocalDatetime = ");
      builder.Append(this.CurrentLocalDatetime);
      builder.Append(", VoiceGender = ");
      builder.Append(this.VoiceGender);
      builder.Append(", ActiveSkills = ");
      builder.Append(this.ActiveSkills);
      builder.Append(", DictationHoldShortcut = ");
      builder.Append(this.DictationHoldShortcut);
      return true;
    }

    // Token: 0x0600051D RID: 1309 RVA: 0x003576F0 File Offset: 0x003576F0
    [NullableContext(2)]
    [CompilerGenerated]
    public static bool operator !=(RealtimeSessionPromptContext left, RealtimeSessionPromptContext right)
    {
      return !(left == right);
    }

    // Token: 0x0600051E RID: 1310 RVA: 0x003576FC File Offset: 0x003576FC
    [NullableContext(2)]
    [CompilerGenerated]
    public static bool operator ==(RealtimeSessionPromptContext left, RealtimeSessionPromptContext right)
    {
      return left == right || (left != null && left.Equals(right));
    }

    // Token: 0x0600051F RID: 1311 RVA: 0x00357710 File Offset: 0x00357710
    [CompilerGenerated]
    public override int GetHashCode()
    {
      return (((((((EqualityComparer<Type>.Default.GetHashCode(this.EqualityContract) * -1521134295 + EqualityComparer<IReadOnlyList<string>>.Default.GetHashCode(this.<ConnectedIntegrations>k__BackingField)) * -1521134295 + EqualityComparer<IReadOnlyList<string>>.Default.GetHashCode(this.<NeedsReconnectIntegrations>k__BackingField)) * -1521134295 + EqualityComparer<int>.Default.GetHashCode(this.<PriorMessageCount>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<TimeZone>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<CurrentLocalDatetime>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<VoiceGender>k__BackingField)) * -1521134295 + EqualityComparer<IReadOnlyList<JsonElement>>.Default.GetHashCode(this.<ActiveSkills>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<DictationHoldShortcut>k__BackingField);
    }

    // Token: 0x06000520 RID: 1312 RVA: 0x003577E8 File Offset: 0x003577E8
    [NullableContext(2)]
    [CompilerGenerated]
    public override bool Equals(object obj)
    {
      return this.Equals(obj as RealtimeSessionPromptContext);
    }

    // Token: 0x06000521 RID: 1313 RVA: 0x003577F8 File Offset: 0x003577F8
    [NullableContext(2)]
    [CompilerGenerated]
    public bool Equals(RealtimeSessionPromptContext other)
    {
      return this == other || (other != null && this.EqualityContract == other.EqualityContract && EqualityComparer<IReadOnlyList<string>>.Default.Equals(this.<ConnectedIntegrations>k__BackingField, other.<ConnectedIntegrations>k__BackingField) && EqualityComparer<IReadOnlyList<string>>.Default.Equals(this.<NeedsReconnectIntegrations>k__BackingField, other.<NeedsReconnectIntegrations>k__BackingField) && EqualityComparer<int>.Default.Equals(this.<PriorMessageCount>k__BackingField, other.<PriorMessageCount>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<TimeZone>k__BackingField, other.<TimeZone>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<CurrentLocalDatetime>k__BackingField, other.<CurrentLocalDatetime>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<VoiceGender>k__BackingField, other.<VoiceGender>k__BackingField) && EqualityComparer<IReadOnlyList<JsonElement>>.Default.Equals(this.<ActiveSkills>k__BackingField, other.<ActiveSkills>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<DictationHoldShortcut>k__BackingField, other.<DictationHoldShortcut>k__BackingField));
    }

    // Token: 0x06000523 RID: 1315 RVA: 0x003578F8 File Offset: 0x003578F8
    [CompilerGenerated]
    private RealtimeSessionPromptContext(RealtimeSessionPromptContext original)
    {
      this.ConnectedIntegrations = original.<ConnectedIntegrations>k__BackingField;
      this.NeedsReconnectIntegrations = original.<NeedsReconnectIntegrations>k__BackingField;
      this.PriorMessageCount = original.<PriorMessageCount>k__BackingField;
      this.TimeZone = original.<TimeZone>k__BackingField;
      this.CurrentLocalDatetime = original.<CurrentLocalDatetime>k__BackingField;
      this.VoiceGender = original.<VoiceGender>k__BackingField;
      this.ActiveSkills = original.<ActiveSkills>k__BackingField;
      this.DictationHoldShortcut = original.<DictationHoldShortcut>k__BackingField;
    }

    // Token: 0x06000524 RID: 1316 RVA: 0x0035796C File Offset: 0x0035796C
    [CompilerGenerated]
    public void Deconstruct(out IReadOnlyList<string> ConnectedIntegrations, out IReadOnlyList<string> NeedsReconnectIntegrations, out int PriorMessageCount, out string TimeZone, out string CurrentLocalDatetime, out string VoiceGender, out IReadOnlyList<JsonElement> ActiveSkills, [Nullable(2)] out string DictationHoldShortcut)
    {
      ConnectedIntegrations = this.ConnectedIntegrations;
      NeedsReconnectIntegrations = this.NeedsReconnectIntegrations;
      PriorMessageCount = this.PriorMessageCount;
      TimeZone = this.TimeZone;
      CurrentLocalDatetime = this.CurrentLocalDatetime;
      VoiceGender = this.VoiceGender;
      ActiveSkills = this.ActiveSkills;
      DictationHoldShortcut = this.DictationHoldShortcut;
    }
  }
}
