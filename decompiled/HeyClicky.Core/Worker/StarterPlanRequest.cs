using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace HeyClicky.Core.Worker
{
  // Token: 0x02000051 RID: 81
  [NullableContext(1)]
  [Nullable(0)]
  public sealed class StarterPlanRequest : IEquatable<StarterPlanRequest>
  {
    // Token: 0x060003A9 RID: 937 RVA: 0x00354208 File Offset: 0x00354208
    public StarterPlanRequest(IReadOnlyList<InterviewLine> Interview, IReadOnlyList<string> ConnectedIntegrations, IReadOnlyList<string> AvailableIntegrations, [Nullable(new byte[] { 2, 1 })] IReadOnlyList<StarterPlanClickyInput> Clickys = null, [Nullable(2)] string FirstAsksForClicky = null, bool? InterviewSaved = null)
    {
      this.Interview = Interview;
      this.ConnectedIntegrations = ConnectedIntegrations;
      this.AvailableIntegrations = AvailableIntegrations;
      this.Clickys = Clickys;
      this.FirstAsksForClicky = FirstAsksForClicky;
      this.InterviewSaved = InterviewSaved;
      this.Jobs = true;
      base..ctor();
    }

    // Token: 0x170000CA RID: 202
    // (get) Token: 0x060003AA RID: 938 RVA: 0x00354244 File Offset: 0x00354244
    [CompilerGenerated]
    private Type EqualityContract
    {
      [CompilerGenerated]
      get
      {
        return typeof(StarterPlanRequest);
      }
    }

    // Token: 0x170000CB RID: 203
    // (get) Token: 0x060003AB RID: 939 RVA: 0x00354250 File Offset: 0x00354250
    // (set) Token: 0x060003AC RID: 940 RVA: 0x00354258 File Offset: 0x00354258
    public IReadOnlyList<InterviewLine> Interview { get; set; }

    // Token: 0x170000CC RID: 204
    // (get) Token: 0x060003AD RID: 941 RVA: 0x00354264 File Offset: 0x00354264
    // (set) Token: 0x060003AE RID: 942 RVA: 0x0035426C File Offset: 0x0035426C
    public IReadOnlyList<string> ConnectedIntegrations { get; set; }

    // Token: 0x170000CD RID: 205
    // (get) Token: 0x060003AF RID: 943 RVA: 0x00354278 File Offset: 0x00354278
    // (set) Token: 0x060003B0 RID: 944 RVA: 0x00354280 File Offset: 0x00354280
    public IReadOnlyList<string> AvailableIntegrations { get; set; }

    // Token: 0x170000CE RID: 206
    // (get) Token: 0x060003B1 RID: 945 RVA: 0x0035428C File Offset: 0x0035428C
    // (set) Token: 0x060003B2 RID: 946 RVA: 0x00354294 File Offset: 0x00354294
    [Nullable(new byte[] { 2, 1 })]
    public IReadOnlyList<StarterPlanClickyInput> Clickys
    {
      [return: Nullable(new byte[] { 2, 1 })]
      get;
      [param: Nullable(new byte[] { 2, 1 })]
      set;
    }

    // Token: 0x170000CF RID: 207
    // (get) Token: 0x060003B3 RID: 947 RVA: 0x003542A0 File Offset: 0x003542A0
    // (set) Token: 0x060003B4 RID: 948 RVA: 0x003542A8 File Offset: 0x003542A8
    [Nullable(2)]
    public string FirstAsksForClicky
    {
      [NullableContext(2)]
      get;
      [NullableContext(2)]
      set;
    }

    // Token: 0x170000D0 RID: 208
    // (get) Token: 0x060003B5 RID: 949 RVA: 0x003542B4 File Offset: 0x003542B4
    // (set) Token: 0x060003B6 RID: 950 RVA: 0x003542BC File Offset: 0x003542BC
    public bool? InterviewSaved { get; set; }

    // Token: 0x170000D1 RID: 209
    // (get) Token: 0x060003B7 RID: 951 RVA: 0x003542C8 File Offset: 0x003542C8
    // (set) Token: 0x060003B8 RID: 952 RVA: 0x003542D0 File Offset: 0x003542D0
    public bool Jobs { get; set; }

    // Token: 0x060003B9 RID: 953 RVA: 0x003542DC File Offset: 0x003542DC
    [CompilerGenerated]
    public override string ToString()
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("StarterPlanRequest");
      stringBuilder.Append(" { ");
      if (this.PrintMembers(stringBuilder))
      {
        stringBuilder.Append(' ');
      }
      stringBuilder.Append('}');
      return stringBuilder.ToString();
    }

    // Token: 0x060003BA RID: 954 RVA: 0x00354328 File Offset: 0x00354328
    [CompilerGenerated]
    private bool PrintMembers(StringBuilder builder)
    {
      RuntimeHelpers.EnsureSufficientExecutionStack();
      builder.Append("Interview = ");
      builder.Append(this.Interview);
      builder.Append(", ConnectedIntegrations = ");
      builder.Append(this.ConnectedIntegrations);
      builder.Append(", AvailableIntegrations = ");
      builder.Append(this.AvailableIntegrations);
      builder.Append(", Clickys = ");
      builder.Append(this.Clickys);
      builder.Append(", FirstAsksForClicky = ");
      builder.Append(this.FirstAsksForClicky);
      builder.Append(", InterviewSaved = ");
      builder.Append(this.InterviewSaved.ToString());
      builder.Append(", Jobs = ");
      builder.Append(this.Jobs.ToString());
      return true;
    }

    // Token: 0x060003BB RID: 955 RVA: 0x00354408 File Offset: 0x00354408
    [NullableContext(2)]
    [CompilerGenerated]
    public static bool operator !=(StarterPlanRequest left, StarterPlanRequest right)
    {
      return !(left == right);
    }

    // Token: 0x060003BC RID: 956 RVA: 0x00354414 File Offset: 0x00354414
    [NullableContext(2)]
    [CompilerGenerated]
    public static bool operator ==(StarterPlanRequest left, StarterPlanRequest right)
    {
      return left == right || (left != null && left.Equals(right));
    }

    // Token: 0x060003BD RID: 957 RVA: 0x00354428 File Offset: 0x00354428
    [CompilerGenerated]
    public override int GetHashCode()
    {
      return ((((((EqualityComparer<Type>.Default.GetHashCode(this.EqualityContract) * -1521134295 + EqualityComparer<IReadOnlyList<InterviewLine>>.Default.GetHashCode(this.<Interview>k__BackingField)) * -1521134295 + EqualityComparer<IReadOnlyList<string>>.Default.GetHashCode(this.<ConnectedIntegrations>k__BackingField)) * -1521134295 + EqualityComparer<IReadOnlyList<string>>.Default.GetHashCode(this.<AvailableIntegrations>k__BackingField)) * -1521134295 + EqualityComparer<IReadOnlyList<StarterPlanClickyInput>>.Default.GetHashCode(this.<Clickys>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<FirstAsksForClicky>k__BackingField)) * -1521134295 + EqualityComparer<bool?>.Default.GetHashCode(this.<InterviewSaved>k__BackingField)) * -1521134295 + EqualityComparer<bool>.Default.GetHashCode(this.<Jobs>k__BackingField);
    }

    // Token: 0x060003BE RID: 958 RVA: 0x003544E8 File Offset: 0x003544E8
    [NullableContext(2)]
    [CompilerGenerated]
    public override bool Equals(object obj)
    {
      return this.Equals(obj as StarterPlanRequest);
    }

    // Token: 0x060003BF RID: 959 RVA: 0x003544F8 File Offset: 0x003544F8
    [NullableContext(2)]
    [CompilerGenerated]
    public bool Equals(StarterPlanRequest other)
    {
      return this == other || (other != null && this.EqualityContract == other.EqualityContract && EqualityComparer<IReadOnlyList<InterviewLine>>.Default.Equals(this.<Interview>k__BackingField, other.<Interview>k__BackingField) && EqualityComparer<IReadOnlyList<string>>.Default.Equals(this.<ConnectedIntegrations>k__BackingField, other.<ConnectedIntegrations>k__BackingField) && EqualityComparer<IReadOnlyList<string>>.Default.Equals(this.<AvailableIntegrations>k__BackingField, other.<AvailableIntegrations>k__BackingField) && EqualityComparer<IReadOnlyList<StarterPlanClickyInput>>.Default.Equals(this.<Clickys>k__BackingField, other.<Clickys>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<FirstAsksForClicky>k__BackingField, other.<FirstAsksForClicky>k__BackingField) && EqualityComparer<bool?>.Default.Equals(this.<InterviewSaved>k__BackingField, other.<InterviewSaved>k__BackingField) && EqualityComparer<bool>.Default.Equals(this.<Jobs>k__BackingField, other.<Jobs>k__BackingField));
    }

    // Token: 0x060003C1 RID: 961 RVA: 0x003545E0 File Offset: 0x003545E0
    [CompilerGenerated]
    private StarterPlanRequest(StarterPlanRequest original)
    {
      this.Interview = original.<Interview>k__BackingField;
      this.ConnectedIntegrations = original.<ConnectedIntegrations>k__BackingField;
      this.AvailableIntegrations = original.<AvailableIntegrations>k__BackingField;
      this.Clickys = original.<Clickys>k__BackingField;
      this.FirstAsksForClicky = original.<FirstAsksForClicky>k__BackingField;
      this.InterviewSaved = original.<InterviewSaved>k__BackingField;
      this.Jobs = original.<Jobs>k__BackingField;
    }

    // Token: 0x060003C2 RID: 962 RVA: 0x00354648 File Offset: 0x00354648
    [CompilerGenerated]
    public void Deconstruct(out IReadOnlyList<InterviewLine> Interview, out IReadOnlyList<string> ConnectedIntegrations, out IReadOnlyList<string> AvailableIntegrations, [Nullable(new byte[] { 2, 1 })] out IReadOnlyList<StarterPlanClickyInput> Clickys, [Nullable(2)] out string FirstAsksForClicky, out bool? InterviewSaved)
    {
      Interview = this.Interview;
      ConnectedIntegrations = this.ConnectedIntegrations;
      AvailableIntegrations = this.AvailableIntegrations;
      Clickys = this.Clickys;
      FirstAsksForClicky = this.FirstAsksForClicky;
      InterviewSaved = this.InterviewSaved;
    }
  }
}
