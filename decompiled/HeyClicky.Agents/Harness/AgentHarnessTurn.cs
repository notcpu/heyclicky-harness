using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Text;

namespace HeyClicky.Agents.Harness
{
  // Token: 0x0200005F RID: 95
  [NullableContext(1)]
  [Nullable(0)]
  [RequiredMember]
  public sealed class AgentHarnessTurn : IEquatable<AgentHarnessTurn>
  {
    // Token: 0x170001E7 RID: 487
    // (get) Token: 0x060007A8 RID: 1960 RVA: 0x000FE3E8 File Offset: 0x000FE3E8
    [CompilerGenerated]
    private Type EqualityContract
    {
      [CompilerGenerated]
      get
      {
        return typeof(AgentHarnessTurn);
      }
    }

    // Token: 0x170001E8 RID: 488
    // (get) Token: 0x060007A9 RID: 1961 RVA: 0x000FE3F4 File Offset: 0x000FE3F4
    // (set) Token: 0x060007AA RID: 1962 RVA: 0x000FE3FC File Offset: 0x000FE3FC
    [RequiredMember]
    public string HarnessThreadId { get; set; }

    // Token: 0x170001E9 RID: 489
    // (get) Token: 0x060007AB RID: 1963 RVA: 0x000FE408 File Offset: 0x000FE408
    // (set) Token: 0x060007AC RID: 1964 RVA: 0x000FE410 File Offset: 0x000FE410
    [RequiredMember]
    public string AppThreadId { get; set; }

    // Token: 0x170001EA RID: 490
    // (get) Token: 0x060007AD RID: 1965 RVA: 0x000FE41C File Offset: 0x000FE41C
    // (set) Token: 0x060007AE RID: 1966 RVA: 0x000FE424 File Offset: 0x000FE424
    public bool IsFirstTurn { get; set; }

    // Token: 0x170001EB RID: 491
    // (get) Token: 0x060007AF RID: 1967 RVA: 0x000FE430 File Offset: 0x000FE430
    // (set) Token: 0x060007B0 RID: 1968 RVA: 0x000FE438 File Offset: 0x000FE438
    [RequiredMember]
    public string Text { get; set; }

    // Token: 0x170001EC RID: 492
    // (get) Token: 0x060007B1 RID: 1969 RVA: 0x000FE444 File Offset: 0x000FE444
    // (set) Token: 0x060007B2 RID: 1970 RVA: 0x000FE44C File Offset: 0x000FE44C
    public IReadOnlyList<string> ImagePaths { get; set; }

    // Token: 0x170001ED RID: 493
    // (get) Token: 0x060007B3 RID: 1971 RVA: 0x000FE458 File Offset: 0x000FE458
    // (set) Token: 0x060007B4 RID: 1972 RVA: 0x000FE460 File Offset: 0x000FE460
    [RequiredMember]
    public string Workspace { get; set; }

    // Token: 0x170001EE RID: 494
    // (get) Token: 0x060007B5 RID: 1973 RVA: 0x000FE46C File Offset: 0x000FE46C
    // (set) Token: 0x060007B6 RID: 1974 RVA: 0x000FE474 File Offset: 0x000FE474
    public AgentHarnessModel Model { get; set; }

    // Token: 0x170001EF RID: 495
    // (get) Token: 0x060007B7 RID: 1975 RVA: 0x000FE480 File Offset: 0x000FE480
    // (set) Token: 0x060007B8 RID: 1976 RVA: 0x000FE488 File Offset: 0x000FE488
    [RequiredMember]
    public AgentHarnessTurnContext Context { get; set; }

    // Token: 0x170001F0 RID: 496
    // (get) Token: 0x060007B9 RID: 1977 RVA: 0x000FE494 File Offset: 0x000FE494
    // (set) Token: 0x060007BA RID: 1978 RVA: 0x000FE49C File Offset: 0x000FE49C
    [Nullable(new byte[] { 2, 1, 1 })]
    public IReadOnlyDictionary<string, string> LeaseMetadata
    {
      [return: Nullable(new byte[] { 2, 1, 1 })]
      get;
      [param: Nullable(new byte[] { 2, 1, 1 })]
      set;
    }

    // Token: 0x170001F1 RID: 497
    // (get) Token: 0x060007BB RID: 1979 RVA: 0x000FE4A8 File Offset: 0x000FE4A8
    // (set) Token: 0x060007BC RID: 1980 RVA: 0x000FE4B0 File Offset: 0x000FE4B0
    [Nullable(2)]
    public string TurnId
    {
      [NullableContext(2)]
      get;
      [NullableContext(2)]
      set;
    }

    // Token: 0x060007BD RID: 1981 RVA: 0x000FE4BC File Offset: 0x000FE4BC
    [CompilerGenerated]
    public override string ToString()
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("AgentHarnessTurn");
      stringBuilder.Append(" { ");
      if (this.PrintMembers(stringBuilder))
      {
        stringBuilder.Append(' ');
      }
      stringBuilder.Append('}');
      return stringBuilder.ToString();
    }

    // Token: 0x060007BE RID: 1982 RVA: 0x000FE508 File Offset: 0x000FE508
    [CompilerGenerated]
    private bool PrintMembers(StringBuilder builder)
    {
      RuntimeHelpers.EnsureSufficientExecutionStack();
      builder.Append("HarnessThreadId = ");
      builder.Append(this.HarnessThreadId);
      builder.Append(", AppThreadId = ");
      builder.Append(this.AppThreadId);
      builder.Append(", IsFirstTurn = ");
      builder.Append(this.IsFirstTurn.ToString());
      builder.Append(", Text = ");
      builder.Append(this.Text);
      builder.Append(", ImagePaths = ");
      builder.Append(this.ImagePaths);
      builder.Append(", Workspace = ");
      builder.Append(this.Workspace);
      builder.Append(", Model = ");
      builder.Append(this.Model.ToString());
      builder.Append(", Context = ");
      builder.Append(this.Context);
      builder.Append(", LeaseMetadata = ");
      builder.Append(this.LeaseMetadata);
      builder.Append(", TurnId = ");
      builder.Append(this.TurnId);
      return true;
    }

    // Token: 0x060007BF RID: 1983 RVA: 0x000FE634 File Offset: 0x000FE634
    [NullableContext(2)]
    [CompilerGenerated]
    public static bool operator !=(AgentHarnessTurn left, AgentHarnessTurn right)
    {
      return !(left == right);
    }

    // Token: 0x060007C0 RID: 1984 RVA: 0x000FE640 File Offset: 0x000FE640
    [NullableContext(2)]
    [CompilerGenerated]
    public static bool operator ==(AgentHarnessTurn left, AgentHarnessTurn right)
    {
      return left == right || (left != null && left.Equals(right));
    }

    // Token: 0x060007C1 RID: 1985 RVA: 0x000FE654 File Offset: 0x000FE654
    [CompilerGenerated]
    public override int GetHashCode()
    {
      return (((((((((EqualityComparer<Type>.Default.GetHashCode(this.EqualityContract) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<HarnessThreadId>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<AppThreadId>k__BackingField)) * -1521134295 + EqualityComparer<bool>.Default.GetHashCode(this.<IsFirstTurn>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Text>k__BackingField)) * -1521134295 + EqualityComparer<IReadOnlyList<string>>.Default.GetHashCode(this.<ImagePaths>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Workspace>k__BackingField)) * -1521134295 + EqualityComparer<AgentHarnessModel>.Default.GetHashCode(this.<Model>k__BackingField)) * -1521134295 + EqualityComparer<AgentHarnessTurnContext>.Default.GetHashCode(this.<Context>k__BackingField)) * -1521134295 + EqualityComparer<IReadOnlyDictionary<string, string>>.Default.GetHashCode(this.<LeaseMetadata>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<TurnId>k__BackingField);
    }

    // Token: 0x060007C2 RID: 1986 RVA: 0x000FE758 File Offset: 0x000FE758
    [NullableContext(2)]
    [CompilerGenerated]
    public override bool Equals(object obj)
    {
      return this.Equals(obj as AgentHarnessTurn);
    }

    // Token: 0x060007C3 RID: 1987 RVA: 0x000FE768 File Offset: 0x000FE768
    [NullableContext(2)]
    [CompilerGenerated]
    public bool Equals(AgentHarnessTurn other)
    {
      return this == other || (other != null && this.EqualityContract == other.EqualityContract && EqualityComparer<string>.Default.Equals(this.<HarnessThreadId>k__BackingField, other.<HarnessThreadId>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<AppThreadId>k__BackingField, other.<AppThreadId>k__BackingField) && EqualityComparer<bool>.Default.Equals(this.<IsFirstTurn>k__BackingField, other.<IsFirstTurn>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<Text>k__BackingField, other.<Text>k__BackingField) && EqualityComparer<IReadOnlyList<string>>.Default.Equals(this.<ImagePaths>k__BackingField, other.<ImagePaths>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<Workspace>k__BackingField, other.<Workspace>k__BackingField) && EqualityComparer<AgentHarnessModel>.Default.Equals(this.<Model>k__BackingField, other.<Model>k__BackingField) && EqualityComparer<AgentHarnessTurnContext>.Default.Equals(this.<Context>k__BackingField, other.<Context>k__BackingField) && EqualityComparer<IReadOnlyDictionary<string, string>>.Default.Equals(this.<LeaseMetadata>k__BackingField, other.<LeaseMetadata>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<TurnId>k__BackingField, other.<TurnId>k__BackingField));
    }

    // Token: 0x060007C5 RID: 1989 RVA: 0x000FE8A0 File Offset: 0x000FE8A0
    [CompilerGenerated]
    [SetsRequiredMembers]
    private AgentHarnessTurn(AgentHarnessTurn original)
    {
      this.HarnessThreadId = original.<HarnessThreadId>k__BackingField;
      this.AppThreadId = original.<AppThreadId>k__BackingField;
      this.IsFirstTurn = original.<IsFirstTurn>k__BackingField;
      this.Text = original.<Text>k__BackingField;
      this.ImagePaths = original.<ImagePaths>k__BackingField;
      this.Workspace = original.<Workspace>k__BackingField;
      this.Model = original.<Model>k__BackingField;
      this.Context = original.<Context>k__BackingField;
      this.LeaseMetadata = original.<LeaseMetadata>k__BackingField;
      this.TurnId = original.<TurnId>k__BackingField;
    }

    // Token: 0x060007C6 RID: 1990 RVA: 0x000FE92C File Offset: 0x000FE92C
    [Obsolete("Constructors of types with required members are not supported in this version of your compiler.", true)]
    [CompilerFeatureRequired("RequiredMembers")]
    public AgentHarnessTurn()
    {
      this.ImagePaths = Array.Empty<string>();
      base..ctor();
    }
  }
}
