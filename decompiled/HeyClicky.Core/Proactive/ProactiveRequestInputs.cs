using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace HeyClicky.Core.Proactive
{
  // Token: 0x020000C2 RID: 194
  [NullableContext(1)]
  [Nullable(0)]
  public sealed class ProactiveRequestInputs : IEquatable<ProactiveRequestInputs>
  {
    // Token: 0x06000CC0 RID: 3264 RVA: 0x0037FD44 File Offset: 0x0037FD44
    public ProactiveRequestInputs(IReadOnlyList<string> ConnectedIntegrationNames, IReadOnlyList<ProactiveHistoryEntry> RecentSuggestions, ProactiveCheckReason CheckReason, IReadOnlyList<ProactiveClickyDescriptor> Clickys, [TupleElementNames(new string[] { "Name", "Description" })] [Nullable(new byte[] { 1, 0, 1, 1 })] IReadOnlyList<ValueTuple<string, string>> CustomConnectors)
    {
      this.ConnectedIntegrationNames = ConnectedIntegrationNames;
      this.RecentSuggestions = RecentSuggestions;
      this.CheckReason = CheckReason;
      this.Clickys = Clickys;
      this.CustomConnectors = CustomConnectors;
      this.ExistingRoutines = Array.Empty<ValueTuple<string, int>>();
      base..ctor();
    }

    // Token: 0x170002C7 RID: 711
    // (get) Token: 0x06000CC1 RID: 3265 RVA: 0x0037FD7C File Offset: 0x0037FD7C
    [CompilerGenerated]
    private Type EqualityContract
    {
      [CompilerGenerated]
      get
      {
        return typeof(ProactiveRequestInputs);
      }
    }

    // Token: 0x170002C8 RID: 712
    // (get) Token: 0x06000CC2 RID: 3266 RVA: 0x0037FD88 File Offset: 0x0037FD88
    // (set) Token: 0x06000CC3 RID: 3267 RVA: 0x0037FD90 File Offset: 0x0037FD90
    public IReadOnlyList<string> ConnectedIntegrationNames { get; set; }

    // Token: 0x170002C9 RID: 713
    // (get) Token: 0x06000CC4 RID: 3268 RVA: 0x0037FD9C File Offset: 0x0037FD9C
    // (set) Token: 0x06000CC5 RID: 3269 RVA: 0x0037FDA4 File Offset: 0x0037FDA4
    public IReadOnlyList<ProactiveHistoryEntry> RecentSuggestions { get; set; }

    // Token: 0x170002CA RID: 714
    // (get) Token: 0x06000CC6 RID: 3270 RVA: 0x0037FDB0 File Offset: 0x0037FDB0
    // (set) Token: 0x06000CC7 RID: 3271 RVA: 0x0037FDB8 File Offset: 0x0037FDB8
    public ProactiveCheckReason CheckReason { get; set; }

    // Token: 0x170002CB RID: 715
    // (get) Token: 0x06000CC8 RID: 3272 RVA: 0x0037FDC4 File Offset: 0x0037FDC4
    // (set) Token: 0x06000CC9 RID: 3273 RVA: 0x0037FDCC File Offset: 0x0037FDCC
    public IReadOnlyList<ProactiveClickyDescriptor> Clickys { get; set; }

    // Token: 0x170002CC RID: 716
    // (get) Token: 0x06000CCA RID: 3274 RVA: 0x0037FDD8 File Offset: 0x0037FDD8
    // (set) Token: 0x06000CCB RID: 3275 RVA: 0x0037FDE0 File Offset: 0x0037FDE0
    [TupleElementNames(new string[] { "Name", "Description" })]
    [Nullable(new byte[] { 1, 0, 1, 1 })]
    public IReadOnlyList<ValueTuple<string, string>> CustomConnectors
    {
      [return: TupleElementNames(new string[] { "Name", "Description" })]
      [return: Nullable(new byte[] { 1, 0, 1, 1 })]
      get;
      [param: TupleElementNames(new string[] { "Name", "Description" })]
      [param: Nullable(new byte[] { 1, 0, 1, 1 })]
      set;
    }

    // Token: 0x170002CD RID: 717
    // (get) Token: 0x06000CCC RID: 3276 RVA: 0x0037FDEC File Offset: 0x0037FDEC
    // (set) Token: 0x06000CCD RID: 3277 RVA: 0x0037FDF4 File Offset: 0x0037FDF4
    public bool ResearchOnlyForGoodMorning { get; set; }

    // Token: 0x170002CE RID: 718
    // (get) Token: 0x06000CCE RID: 3278 RVA: 0x0037FE00 File Offset: 0x0037FE00
    // (set) Token: 0x06000CCF RID: 3279 RVA: 0x0037FE08 File Offset: 0x0037FE08
    public bool ReadsForMorningPaper { get; set; }

    // Token: 0x170002CF RID: 719
    // (get) Token: 0x06000CD0 RID: 3280 RVA: 0x0037FE14 File Offset: 0x0037FE14
    // (set) Token: 0x06000CD1 RID: 3281 RVA: 0x0037FE1C File Offset: 0x0037FE1C
    [TupleElementNames(new string[] { "Title", "EveryMinutes" })]
    [Nullable(new byte[] { 1, 0, 1 })]
    public IReadOnlyList<ValueTuple<string, int>> ExistingRoutines
    {
      [return: TupleElementNames(new string[] { "Title", "EveryMinutes" })]
      [return: Nullable(new byte[] { 1, 0, 1 })]
      get;
      [param: TupleElementNames(new string[] { "Title", "EveryMinutes" })]
      [param: Nullable(new byte[] { 1, 0, 1 })]
      set;
    }

    // Token: 0x06000CD2 RID: 3282 RVA: 0x0037FE28 File Offset: 0x0037FE28
    [CompilerGenerated]
    public override string ToString()
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("ProactiveRequestInputs");
      stringBuilder.Append(" { ");
      if (this.PrintMembers(stringBuilder))
      {
        stringBuilder.Append(' ');
      }
      stringBuilder.Append('}');
      return stringBuilder.ToString();
    }

    // Token: 0x06000CD3 RID: 3283 RVA: 0x0037FE74 File Offset: 0x0037FE74
    [CompilerGenerated]
    private bool PrintMembers(StringBuilder builder)
    {
      RuntimeHelpers.EnsureSufficientExecutionStack();
      builder.Append("ConnectedIntegrationNames = ");
      builder.Append(this.ConnectedIntegrationNames);
      builder.Append(", RecentSuggestions = ");
      builder.Append(this.RecentSuggestions);
      builder.Append(", CheckReason = ");
      builder.Append(this.CheckReason.ToString());
      builder.Append(", Clickys = ");
      builder.Append(this.Clickys);
      builder.Append(", CustomConnectors = ");
      builder.Append(this.CustomConnectors);
      builder.Append(", ResearchOnlyForGoodMorning = ");
      builder.Append(this.ResearchOnlyForGoodMorning.ToString());
      builder.Append(", ReadsForMorningPaper = ");
      builder.Append(this.ReadsForMorningPaper.ToString());
      builder.Append(", ExistingRoutines = ");
      builder.Append(this.ExistingRoutines);
      return true;
    }

    // Token: 0x06000CD4 RID: 3284 RVA: 0x0037FF7C File Offset: 0x0037FF7C
    [NullableContext(2)]
    [CompilerGenerated]
    public static bool operator !=(ProactiveRequestInputs left, ProactiveRequestInputs right)
    {
      return !(left == right);
    }

    // Token: 0x06000CD5 RID: 3285 RVA: 0x0037FF88 File Offset: 0x0037FF88
    [NullableContext(2)]
    [CompilerGenerated]
    public static bool operator ==(ProactiveRequestInputs left, ProactiveRequestInputs right)
    {
      return left == right || (left != null && left.Equals(right));
    }

    // Token: 0x06000CD6 RID: 3286 RVA: 0x0037FF9C File Offset: 0x0037FF9C
    [CompilerGenerated]
    public override int GetHashCode()
    {
      return (((((((EqualityComparer<Type>.Default.GetHashCode(this.EqualityContract) * -1521134295 + EqualityComparer<IReadOnlyList<string>>.Default.GetHashCode(this.<ConnectedIntegrationNames>k__BackingField)) * -1521134295 + EqualityComparer<IReadOnlyList<ProactiveHistoryEntry>>.Default.GetHashCode(this.<RecentSuggestions>k__BackingField)) * -1521134295 + EqualityComparer<ProactiveCheckReason>.Default.GetHashCode(this.<CheckReason>k__BackingField)) * -1521134295 + EqualityComparer<IReadOnlyList<ProactiveClickyDescriptor>>.Default.GetHashCode(this.<Clickys>k__BackingField)) * -1521134295 + EqualityComparer<IReadOnlyList<ValueTuple<string, string>>>.Default.GetHashCode(this.<CustomConnectors>k__BackingField)) * -1521134295 + EqualityComparer<bool>.Default.GetHashCode(this.<ResearchOnlyForGoodMorning>k__BackingField)) * -1521134295 + EqualityComparer<bool>.Default.GetHashCode(this.<ReadsForMorningPaper>k__BackingField)) * -1521134295 + EqualityComparer<IReadOnlyList<ValueTuple<string, int>>>.Default.GetHashCode(this.<ExistingRoutines>k__BackingField);
    }

    // Token: 0x06000CD7 RID: 3287 RVA: 0x00380074 File Offset: 0x00380074
    [NullableContext(2)]
    [CompilerGenerated]
    public override bool Equals(object obj)
    {
      return this.Equals(obj as ProactiveRequestInputs);
    }

    // Token: 0x06000CD8 RID: 3288 RVA: 0x00380084 File Offset: 0x00380084
    [NullableContext(2)]
    [CompilerGenerated]
    public bool Equals(ProactiveRequestInputs other)
    {
      return this == other || (other != null && this.EqualityContract == other.EqualityContract && EqualityComparer<IReadOnlyList<string>>.Default.Equals(this.<ConnectedIntegrationNames>k__BackingField, other.<ConnectedIntegrationNames>k__BackingField) && EqualityComparer<IReadOnlyList<ProactiveHistoryEntry>>.Default.Equals(this.<RecentSuggestions>k__BackingField, other.<RecentSuggestions>k__BackingField) && EqualityComparer<ProactiveCheckReason>.Default.Equals(this.<CheckReason>k__BackingField, other.<CheckReason>k__BackingField) && EqualityComparer<IReadOnlyList<ProactiveClickyDescriptor>>.Default.Equals(this.<Clickys>k__BackingField, other.<Clickys>k__BackingField) && EqualityComparer<IReadOnlyList<ValueTuple<string, string>>>.Default.Equals(this.<CustomConnectors>k__BackingField, other.<CustomConnectors>k__BackingField) && EqualityComparer<bool>.Default.Equals(this.<ResearchOnlyForGoodMorning>k__BackingField, other.<ResearchOnlyForGoodMorning>k__BackingField) && EqualityComparer<bool>.Default.Equals(this.<ReadsForMorningPaper>k__BackingField, other.<ReadsForMorningPaper>k__BackingField) && EqualityComparer<IReadOnlyList<ValueTuple<string, int>>>.Default.Equals(this.<ExistingRoutines>k__BackingField, other.<ExistingRoutines>k__BackingField));
    }

    // Token: 0x06000CDA RID: 3290 RVA: 0x00380184 File Offset: 0x00380184
    [CompilerGenerated]
    private ProactiveRequestInputs(ProactiveRequestInputs original)
    {
      this.ConnectedIntegrationNames = original.<ConnectedIntegrationNames>k__BackingField;
      this.RecentSuggestions = original.<RecentSuggestions>k__BackingField;
      this.CheckReason = original.<CheckReason>k__BackingField;
      this.Clickys = original.<Clickys>k__BackingField;
      this.CustomConnectors = original.<CustomConnectors>k__BackingField;
      this.ResearchOnlyForGoodMorning = original.<ResearchOnlyForGoodMorning>k__BackingField;
      this.ReadsForMorningPaper = original.<ReadsForMorningPaper>k__BackingField;
      this.ExistingRoutines = original.<ExistingRoutines>k__BackingField;
    }

    // Token: 0x06000CDB RID: 3291 RVA: 0x003801F8 File Offset: 0x003801F8
    [CompilerGenerated]
    public void Deconstruct(out IReadOnlyList<string> ConnectedIntegrationNames, out IReadOnlyList<ProactiveHistoryEntry> RecentSuggestions, out ProactiveCheckReason CheckReason, out IReadOnlyList<ProactiveClickyDescriptor> Clickys, [TupleElementNames(new string[] { "Name", "Description" })] [Nullable(new byte[] { 1, 0, 1, 1 })] out IReadOnlyList<ValueTuple<string, string>> CustomConnectors)
    {
      ConnectedIntegrationNames = this.ConnectedIntegrationNames;
      RecentSuggestions = this.RecentSuggestions;
      CheckReason = this.CheckReason;
      Clickys = this.Clickys;
      CustomConnectors = this.CustomConnectors;
    }
  }
}
