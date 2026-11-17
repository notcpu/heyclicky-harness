using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Text;

namespace HeyClicky.Agents.Harness
{
  // Token: 0x02000060 RID: 96
  [NullableContext(1)]
  [Nullable(0)]
  [RequiredMember]
  public sealed class AgentHarnessTurnContext : IEquatable<AgentHarnessTurnContext>
  {
    // Token: 0x170001F2 RID: 498
    // (get) Token: 0x060007C7 RID: 1991 RVA: 0x000FE940 File Offset: 0x000FE940
    [CompilerGenerated]
    private Type EqualityContract
    {
      [CompilerGenerated]
      get
      {
        return typeof(AgentHarnessTurnContext);
      }
    }

    // Token: 0x170001F3 RID: 499
    // (get) Token: 0x060007C8 RID: 1992 RVA: 0x000FE94C File Offset: 0x000FE94C
    // (set) Token: 0x060007C9 RID: 1993 RVA: 0x000FE954 File Offset: 0x000FE954
    [RequiredMember]
    public string Instructions { get; set; }

    // Token: 0x170001F4 RID: 500
    // (get) Token: 0x060007CA RID: 1994 RVA: 0x000FE960 File Offset: 0x000FE960
    // (set) Token: 0x060007CB RID: 1995 RVA: 0x000FE968 File Offset: 0x000FE968
    public IReadOnlyList<AgentHarnessMcpServer> McpServers { get; set; }

    // Token: 0x170001F5 RID: 501
    // (get) Token: 0x060007CC RID: 1996 RVA: 0x000FE974 File Offset: 0x000FE974
    // (set) Token: 0x060007CD RID: 1997 RVA: 0x000FE97C File Offset: 0x000FE97C
    public IReadOnlyList<string> SkillDirectories { get; set; }

    // Token: 0x170001F6 RID: 502
    // (get) Token: 0x060007CE RID: 1998 RVA: 0x000FE988 File Offset: 0x000FE988
    // (set) Token: 0x060007CF RID: 1999 RVA: 0x000FE990 File Offset: 0x000FE990
    public IReadOnlyList<string> UserSkillDirectories { get; set; }

    // Token: 0x170001F7 RID: 503
    // (get) Token: 0x060007D0 RID: 2000 RVA: 0x000FE99C File Offset: 0x000FE99C
    // (set) Token: 0x060007D1 RID: 2001 RVA: 0x000FE9A4 File Offset: 0x000FE9A4
    [RequiredMember]
    public string WorkerBaseUrl { get; set; }

    // Token: 0x170001F8 RID: 504
    // (get) Token: 0x060007D2 RID: 2002 RVA: 0x000FE9B0 File Offset: 0x000FE9B0
    // (set) Token: 0x060007D3 RID: 2003 RVA: 0x000FE9B8 File Offset: 0x000FE9B8
    [RequiredMember]
    public string SessionToken { get; set; }

    // Token: 0x170001F9 RID: 505
    // (get) Token: 0x060007D4 RID: 2004 RVA: 0x000FE9C4 File Offset: 0x000FE9C4
    // (set) Token: 0x060007D5 RID: 2005 RVA: 0x000FE9CC File Offset: 0x000FE9CC
    [Nullable(2)]
    public string NotePath
    {
      [NullableContext(2)]
      get;
      [NullableContext(2)]
      set;
    }

    // Token: 0x170001FA RID: 506
    // (get) Token: 0x060007D6 RID: 2006 RVA: 0x000FE9D8 File Offset: 0x000FE9D8
    // (set) Token: 0x060007D7 RID: 2007 RVA: 0x000FE9E0 File Offset: 0x000FE9E0
    public bool IsKeptThing { get; set; }

    // Token: 0x170001FB RID: 507
    // (get) Token: 0x060007D8 RID: 2008 RVA: 0x000FE9EC File Offset: 0x000FE9EC
    // (set) Token: 0x060007D9 RID: 2009 RVA: 0x000FE9F4 File Offset: 0x000FE9F4
    public bool WritesNoteWithTools { get; set; }

    // Token: 0x170001FC RID: 508
    // (get) Token: 0x060007DA RID: 2010 RVA: 0x000FEA00 File Offset: 0x000FEA00
    // (set) Token: 0x060007DB RID: 2011 RVA: 0x000FEA08 File Offset: 0x000FEA08
    public bool UsesPlanTool { get; set; }

    // Token: 0x170001FD RID: 509
    // (get) Token: 0x060007DC RID: 2012 RVA: 0x000FEA14 File Offset: 0x000FEA14
    // (set) Token: 0x060007DD RID: 2013 RVA: 0x000FEA1C File Offset: 0x000FEA1C
    public AgentHarnessModel Model { get; set; }

    // Token: 0x060007DE RID: 2014 RVA: 0x000FEA28 File Offset: 0x000FEA28
    [CompilerGenerated]
    public override string ToString()
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("AgentHarnessTurnContext");
      stringBuilder.Append(" { ");
      if (this.PrintMembers(stringBuilder))
      {
        stringBuilder.Append(' ');
      }
      stringBuilder.Append('}');
      return stringBuilder.ToString();
    }

    // Token: 0x060007DF RID: 2015 RVA: 0x000FEA74 File Offset: 0x000FEA74
    [CompilerGenerated]
    private bool PrintMembers(StringBuilder builder)
    {
      RuntimeHelpers.EnsureSufficientExecutionStack();
      builder.Append("Instructions = ");
      builder.Append(this.Instructions);
      builder.Append(", McpServers = ");
      builder.Append(this.McpServers);
      builder.Append(", SkillDirectories = ");
      builder.Append(this.SkillDirectories);
      builder.Append(", UserSkillDirectories = ");
      builder.Append(this.UserSkillDirectories);
      builder.Append(", WorkerBaseUrl = ");
      builder.Append(this.WorkerBaseUrl);
      builder.Append(", SessionToken = ");
      builder.Append(this.SessionToken);
      builder.Append(", NotePath = ");
      builder.Append(this.NotePath);
      builder.Append(", IsKeptThing = ");
      builder.Append(this.IsKeptThing.ToString());
      builder.Append(", WritesNoteWithTools = ");
      builder.Append(this.WritesNoteWithTools.ToString());
      builder.Append(", UsesPlanTool = ");
      builder.Append(this.UsesPlanTool.ToString());
      builder.Append(", Model = ");
      builder.Append(this.Model.ToString());
      return true;
    }

    // Token: 0x060007E0 RID: 2016 RVA: 0x000FEBD4 File Offset: 0x000FEBD4
    [NullableContext(2)]
    [CompilerGenerated]
    public static bool operator !=(AgentHarnessTurnContext left, AgentHarnessTurnContext right)
    {
      return !(left == right);
    }

    // Token: 0x060007E1 RID: 2017 RVA: 0x000FEBE0 File Offset: 0x000FEBE0
    [NullableContext(2)]
    [CompilerGenerated]
    public static bool operator ==(AgentHarnessTurnContext left, AgentHarnessTurnContext right)
    {
      return left == right || (left != null && left.Equals(right));
    }

    // Token: 0x060007E2 RID: 2018 RVA: 0x000FEBF4 File Offset: 0x000FEBF4
    [CompilerGenerated]
    public override int GetHashCode()
    {
      return ((((((((((EqualityComparer<Type>.Default.GetHashCode(this.EqualityContract) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Instructions>k__BackingField)) * -1521134295 + EqualityComparer<IReadOnlyList<AgentHarnessMcpServer>>.Default.GetHashCode(this.<McpServers>k__BackingField)) * -1521134295 + EqualityComparer<IReadOnlyList<string>>.Default.GetHashCode(this.<SkillDirectories>k__BackingField)) * -1521134295 + EqualityComparer<IReadOnlyList<string>>.Default.GetHashCode(this.<UserSkillDirectories>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<WorkerBaseUrl>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<SessionToken>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<NotePath>k__BackingField)) * -1521134295 + EqualityComparer<bool>.Default.GetHashCode(this.<IsKeptThing>k__BackingField)) * -1521134295 + EqualityComparer<bool>.Default.GetHashCode(this.<WritesNoteWithTools>k__BackingField)) * -1521134295 + EqualityComparer<bool>.Default.GetHashCode(this.<UsesPlanTool>k__BackingField)) * -1521134295 + EqualityComparer<AgentHarnessModel>.Default.GetHashCode(this.<Model>k__BackingField);
    }

    // Token: 0x060007E3 RID: 2019 RVA: 0x000FED10 File Offset: 0x000FED10
    [NullableContext(2)]
    [CompilerGenerated]
    public override bool Equals(object obj)
    {
      return this.Equals(obj as AgentHarnessTurnContext);
    }

    // Token: 0x060007E4 RID: 2020 RVA: 0x000FED20 File Offset: 0x000FED20
    [NullableContext(2)]
    [CompilerGenerated]
    public bool Equals(AgentHarnessTurnContext other)
    {
      return this == other || (other != null && this.EqualityContract == other.EqualityContract && EqualityComparer<string>.Default.Equals(this.<Instructions>k__BackingField, other.<Instructions>k__BackingField) && EqualityComparer<IReadOnlyList<AgentHarnessMcpServer>>.Default.Equals(this.<McpServers>k__BackingField, other.<McpServers>k__BackingField) && EqualityComparer<IReadOnlyList<string>>.Default.Equals(this.<SkillDirectories>k__BackingField, other.<SkillDirectories>k__BackingField) && EqualityComparer<IReadOnlyList<string>>.Default.Equals(this.<UserSkillDirectories>k__BackingField, other.<UserSkillDirectories>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<WorkerBaseUrl>k__BackingField, other.<WorkerBaseUrl>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<SessionToken>k__BackingField, other.<SessionToken>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<NotePath>k__BackingField, other.<NotePath>k__BackingField) && EqualityComparer<bool>.Default.Equals(this.<IsKeptThing>k__BackingField, other.<IsKeptThing>k__BackingField) && EqualityComparer<bool>.Default.Equals(this.<WritesNoteWithTools>k__BackingField, other.<WritesNoteWithTools>k__BackingField) && EqualityComparer<bool>.Default.Equals(this.<UsesPlanTool>k__BackingField, other.<UsesPlanTool>k__BackingField) && EqualityComparer<AgentHarnessModel>.Default.Equals(this.<Model>k__BackingField, other.<Model>k__BackingField));
    }

    // Token: 0x060007E6 RID: 2022 RVA: 0x000FEE74 File Offset: 0x000FEE74
    [CompilerGenerated]
    [SetsRequiredMembers]
    private AgentHarnessTurnContext(AgentHarnessTurnContext original)
    {
      this.Instructions = original.<Instructions>k__BackingField;
      this.McpServers = original.<McpServers>k__BackingField;
      this.SkillDirectories = original.<SkillDirectories>k__BackingField;
      this.UserSkillDirectories = original.<UserSkillDirectories>k__BackingField;
      this.WorkerBaseUrl = original.<WorkerBaseUrl>k__BackingField;
      this.SessionToken = original.<SessionToken>k__BackingField;
      this.NotePath = original.<NotePath>k__BackingField;
      this.IsKeptThing = original.<IsKeptThing>k__BackingField;
      this.WritesNoteWithTools = original.<WritesNoteWithTools>k__BackingField;
      this.UsesPlanTool = original.<UsesPlanTool>k__BackingField;
      this.Model = original.<Model>k__BackingField;
    }

    // Token: 0x060007E7 RID: 2023 RVA: 0x000FEF0C File Offset: 0x000FEF0C
    [Obsolete("Constructors of types with required members are not supported in this version of your compiler.", true)]
    [CompilerFeatureRequired("RequiredMembers")]
    public AgentHarnessTurnContext()
    {
      this.McpServers = Array.Empty<AgentHarnessMcpServer>();
      this.SkillDirectories = Array.Empty<string>();
      this.UserSkillDirectories = Array.Empty<string>();
      base..ctor();
    }
  }
}
