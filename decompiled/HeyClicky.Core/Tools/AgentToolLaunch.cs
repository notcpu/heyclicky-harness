using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace HeyClicky.Core.Tools
{
  // Token: 0x02000097 RID: 151
  [NullableContext(2)]
  [Nullable(0)]
  public sealed class AgentToolLaunch : IEquatable<AgentToolLaunch>
  {
    // Token: 0x06000964 RID: 2404 RVA: 0x0036DBD4 File Offset: 0x0036DBD4
    public AgentToolLaunch([Nullable(1)] string Prompt, string ClickySlug = null, string NewClickyName = null, [Nullable(1)] string NewClickyRole = "Helper", string NewClickyOneLiner = null, string LaunchContext = null, [Nullable(1)] string LaunchSource = "voice", [Nullable(new byte[] { 2, 1 })] IReadOnlyList<byte[]> ImagesJpeg = null, [Nullable(new byte[] { 2, 1 })] IReadOnlyList<string> AttachedFilePaths = null, [Nullable(1)] string UsageContext = "companion", bool AsJob = false, string ChromeChoice = null, [Nullable(new byte[] { 2, 1 })] IReadOnlyList<string> BuildsOnJobIds = null)
    {
      this.Prompt = Prompt;
      this.ClickySlug = ClickySlug;
      this.NewClickyName = NewClickyName;
      this.NewClickyRole = NewClickyRole;
      this.NewClickyOneLiner = NewClickyOneLiner;
      this.LaunchContext = LaunchContext;
      this.LaunchSource = LaunchSource;
      this.ImagesJpeg = ImagesJpeg;
      this.AttachedFilePaths = AttachedFilePaths;
      this.UsageContext = UsageContext;
      this.AsJob = AsJob;
      this.ChromeChoice = ChromeChoice;
      this.BuildsOnJobIds = BuildsOnJobIds;
      base..ctor();
    }

    // Token: 0x1700021E RID: 542
    // (get) Token: 0x06000965 RID: 2405 RVA: 0x0036DC4C File Offset: 0x0036DC4C
    [Nullable(1)]
    [CompilerGenerated]
    private Type EqualityContract
    {
      [NullableContext(1)]
      [CompilerGenerated]
      get
      {
        return typeof(AgentToolLaunch);
      }
    }

    // Token: 0x1700021F RID: 543
    // (get) Token: 0x06000966 RID: 2406 RVA: 0x0036DC58 File Offset: 0x0036DC58
    // (set) Token: 0x06000967 RID: 2407 RVA: 0x0036DC60 File Offset: 0x0036DC60
    [Nullable(1)]
    public string Prompt
    {
      [NullableContext(1)]
      get;
      [NullableContext(1)]
      set;
    }

    // Token: 0x17000220 RID: 544
    // (get) Token: 0x06000968 RID: 2408 RVA: 0x0036DC6C File Offset: 0x0036DC6C
    // (set) Token: 0x06000969 RID: 2409 RVA: 0x0036DC74 File Offset: 0x0036DC74
    public string ClickySlug { get; set; }

    // Token: 0x17000221 RID: 545
    // (get) Token: 0x0600096A RID: 2410 RVA: 0x0036DC80 File Offset: 0x0036DC80
    // (set) Token: 0x0600096B RID: 2411 RVA: 0x0036DC88 File Offset: 0x0036DC88
    public string NewClickyName { get; set; }

    // Token: 0x17000222 RID: 546
    // (get) Token: 0x0600096C RID: 2412 RVA: 0x0036DC94 File Offset: 0x0036DC94
    // (set) Token: 0x0600096D RID: 2413 RVA: 0x0036DC9C File Offset: 0x0036DC9C
    [Nullable(1)]
    public string NewClickyRole
    {
      [NullableContext(1)]
      get;
      [NullableContext(1)]
      set;
    }

    // Token: 0x17000223 RID: 547
    // (get) Token: 0x0600096E RID: 2414 RVA: 0x0036DCA8 File Offset: 0x0036DCA8
    // (set) Token: 0x0600096F RID: 2415 RVA: 0x0036DCB0 File Offset: 0x0036DCB0
    public string NewClickyOneLiner { get; set; }

    // Token: 0x17000224 RID: 548
    // (get) Token: 0x06000970 RID: 2416 RVA: 0x0036DCBC File Offset: 0x0036DCBC
    // (set) Token: 0x06000971 RID: 2417 RVA: 0x0036DCC4 File Offset: 0x0036DCC4
    public string LaunchContext { get; set; }

    // Token: 0x17000225 RID: 549
    // (get) Token: 0x06000972 RID: 2418 RVA: 0x0036DCD0 File Offset: 0x0036DCD0
    // (set) Token: 0x06000973 RID: 2419 RVA: 0x0036DCD8 File Offset: 0x0036DCD8
    [Nullable(1)]
    public string LaunchSource
    {
      [NullableContext(1)]
      get;
      [NullableContext(1)]
      set;
    }

    // Token: 0x17000226 RID: 550
    // (get) Token: 0x06000974 RID: 2420 RVA: 0x0036DCE4 File Offset: 0x0036DCE4
    // (set) Token: 0x06000975 RID: 2421 RVA: 0x0036DCEC File Offset: 0x0036DCEC
    [Nullable(new byte[] { 2, 1 })]
    public IReadOnlyList<byte[]> ImagesJpeg
    {
      [return: Nullable(new byte[] { 2, 1 })]
      get;
      [param: Nullable(new byte[] { 2, 1 })]
      set;
    }

    // Token: 0x17000227 RID: 551
    // (get) Token: 0x06000976 RID: 2422 RVA: 0x0036DCF8 File Offset: 0x0036DCF8
    // (set) Token: 0x06000977 RID: 2423 RVA: 0x0036DD00 File Offset: 0x0036DD00
    [Nullable(new byte[] { 2, 1 })]
    public IReadOnlyList<string> AttachedFilePaths
    {
      [return: Nullable(new byte[] { 2, 1 })]
      get;
      [param: Nullable(new byte[] { 2, 1 })]
      set;
    }

    // Token: 0x17000228 RID: 552
    // (get) Token: 0x06000978 RID: 2424 RVA: 0x0036DD0C File Offset: 0x0036DD0C
    // (set) Token: 0x06000979 RID: 2425 RVA: 0x0036DD14 File Offset: 0x0036DD14
    [Nullable(1)]
    public string UsageContext
    {
      [NullableContext(1)]
      get;
      [NullableContext(1)]
      set;
    }

    // Token: 0x17000229 RID: 553
    // (get) Token: 0x0600097A RID: 2426 RVA: 0x0036DD20 File Offset: 0x0036DD20
    // (set) Token: 0x0600097B RID: 2427 RVA: 0x0036DD28 File Offset: 0x0036DD28
    public bool AsJob { get; set; }

    // Token: 0x1700022A RID: 554
    // (get) Token: 0x0600097C RID: 2428 RVA: 0x0036DD34 File Offset: 0x0036DD34
    // (set) Token: 0x0600097D RID: 2429 RVA: 0x0036DD3C File Offset: 0x0036DD3C
    public string ChromeChoice { get; set; }

    // Token: 0x1700022B RID: 555
    // (get) Token: 0x0600097E RID: 2430 RVA: 0x0036DD48 File Offset: 0x0036DD48
    // (set) Token: 0x0600097F RID: 2431 RVA: 0x0036DD50 File Offset: 0x0036DD50
    [Nullable(new byte[] { 2, 1 })]
    public IReadOnlyList<string> BuildsOnJobIds
    {
      [return: Nullable(new byte[] { 2, 1 })]
      get;
      [param: Nullable(new byte[] { 2, 1 })]
      set;
    }

    // Token: 0x1700022C RID: 556
    // (get) Token: 0x06000980 RID: 2432 RVA: 0x0036DD5C File Offset: 0x0036DD5C
    public bool IsGeneratedTaskBrief
    {
      get
      {
        return !this.RelaysUsersOwnRequest;
      }
    }

    // Token: 0x1700022D RID: 557
    // (get) Token: 0x06000981 RID: 2433 RVA: 0x0036DD68 File Offset: 0x0036DD68
    public bool RelaysUsersOwnRequest
    {
      get
      {
        string launchSource = this.LaunchSource;
        bool flag = launchSource == "text" || launchSource == "voice";
        if (flag)
        {
          string launchContext = this.LaunchContext;
          if (launchContext != null)
          {
            return launchContext.StartsWith("HeyClicky passed the user's message to you exactly as they", StringComparison.Ordinal) || (AgentHandoffBlock.IsPresent(launchContext) && launchContext.Contains("Original user request (typed/STT text):", StringComparison.Ordinal) && !launchContext.Contains("(not available at tool dispatch time)", StringComparison.Ordinal));
          }
        }
        return false;
      }
    }

    // Token: 0x06000982 RID: 2434 RVA: 0x0036DDE4 File Offset: 0x0036DDE4
    [NullableContext(1)]
    [CompilerGenerated]
    public override string ToString()
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("AgentToolLaunch");
      stringBuilder.Append(" { ");
      if (this.PrintMembers(stringBuilder))
      {
        stringBuilder.Append(' ');
      }
      stringBuilder.Append('}');
      return stringBuilder.ToString();
    }

    // Token: 0x06000983 RID: 2435 RVA: 0x0036DE30 File Offset: 0x0036DE30
    [NullableContext(1)]
    [CompilerGenerated]
    private bool PrintMembers(StringBuilder builder)
    {
      RuntimeHelpers.EnsureSufficientExecutionStack();
      builder.Append("Prompt = ");
      builder.Append(this.Prompt);
      builder.Append(", ClickySlug = ");
      builder.Append(this.ClickySlug);
      builder.Append(", NewClickyName = ");
      builder.Append(this.NewClickyName);
      builder.Append(", NewClickyRole = ");
      builder.Append(this.NewClickyRole);
      builder.Append(", NewClickyOneLiner = ");
      builder.Append(this.NewClickyOneLiner);
      builder.Append(", LaunchContext = ");
      builder.Append(this.LaunchContext);
      builder.Append(", LaunchSource = ");
      builder.Append(this.LaunchSource);
      builder.Append(", ImagesJpeg = ");
      builder.Append(this.ImagesJpeg);
      builder.Append(", AttachedFilePaths = ");
      builder.Append(this.AttachedFilePaths);
      builder.Append(", UsageContext = ");
      builder.Append(this.UsageContext);
      builder.Append(", AsJob = ");
      builder.Append(this.AsJob.ToString());
      builder.Append(", ChromeChoice = ");
      builder.Append(this.ChromeChoice);
      builder.Append(", BuildsOnJobIds = ");
      builder.Append(this.BuildsOnJobIds);
      builder.Append(", IsGeneratedTaskBrief = ");
      builder.Append(this.IsGeneratedTaskBrief.ToString());
      builder.Append(", RelaysUsersOwnRequest = ");
      builder.Append(this.RelaysUsersOwnRequest.ToString());
      return true;
    }

    // Token: 0x06000984 RID: 2436 RVA: 0x0036DFE4 File Offset: 0x0036DFE4
    [CompilerGenerated]
    public static bool operator !=(AgentToolLaunch left, AgentToolLaunch right)
    {
      return !(left == right);
    }

    // Token: 0x06000985 RID: 2437 RVA: 0x0036DFF0 File Offset: 0x0036DFF0
    [CompilerGenerated]
    public static bool operator ==(AgentToolLaunch left, AgentToolLaunch right)
    {
      return left == right || (left != null && left.Equals(right));
    }

    // Token: 0x06000986 RID: 2438 RVA: 0x0036E004 File Offset: 0x0036E004
    [CompilerGenerated]
    public override int GetHashCode()
    {
      return ((((((((((((EqualityComparer<Type>.Default.GetHashCode(this.EqualityContract) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Prompt>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<ClickySlug>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<NewClickyName>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<NewClickyRole>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<NewClickyOneLiner>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<LaunchContext>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<LaunchSource>k__BackingField)) * -1521134295 + EqualityComparer<IReadOnlyList<byte[]>>.Default.GetHashCode(this.<ImagesJpeg>k__BackingField)) * -1521134295 + EqualityComparer<IReadOnlyList<string>>.Default.GetHashCode(this.<AttachedFilePaths>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<UsageContext>k__BackingField)) * -1521134295 + EqualityComparer<bool>.Default.GetHashCode(this.<AsJob>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<ChromeChoice>k__BackingField)) * -1521134295 + EqualityComparer<IReadOnlyList<string>>.Default.GetHashCode(this.<BuildsOnJobIds>k__BackingField);
    }

    // Token: 0x06000987 RID: 2439 RVA: 0x0036E14C File Offset: 0x0036E14C
    [CompilerGenerated]
    public override bool Equals(object obj)
    {
      return this.Equals(obj as AgentToolLaunch);
    }

    // Token: 0x06000988 RID: 2440 RVA: 0x0036E15C File Offset: 0x0036E15C
    [CompilerGenerated]
    public bool Equals(AgentToolLaunch other)
    {
      return this == other || (other != null && this.EqualityContract == other.EqualityContract && EqualityComparer<string>.Default.Equals(this.<Prompt>k__BackingField, other.<Prompt>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<ClickySlug>k__BackingField, other.<ClickySlug>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<NewClickyName>k__BackingField, other.<NewClickyName>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<NewClickyRole>k__BackingField, other.<NewClickyRole>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<NewClickyOneLiner>k__BackingField, other.<NewClickyOneLiner>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<LaunchContext>k__BackingField, other.<LaunchContext>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<LaunchSource>k__BackingField, other.<LaunchSource>k__BackingField) && EqualityComparer<IReadOnlyList<byte[]>>.Default.Equals(this.<ImagesJpeg>k__BackingField, other.<ImagesJpeg>k__BackingField) && EqualityComparer<IReadOnlyList<string>>.Default.Equals(this.<AttachedFilePaths>k__BackingField, other.<AttachedFilePaths>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<UsageContext>k__BackingField, other.<UsageContext>k__BackingField) && EqualityComparer<bool>.Default.Equals(this.<AsJob>k__BackingField, other.<AsJob>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<ChromeChoice>k__BackingField, other.<ChromeChoice>k__BackingField) && EqualityComparer<IReadOnlyList<string>>.Default.Equals(this.<BuildsOnJobIds>k__BackingField, other.<BuildsOnJobIds>k__BackingField));
    }

    // Token: 0x0600098A RID: 2442 RVA: 0x0036E2E4 File Offset: 0x0036E2E4
    [CompilerGenerated]
    private AgentToolLaunch([Nullable(1)] AgentToolLaunch original)
    {
      this.Prompt = original.<Prompt>k__BackingField;
      this.ClickySlug = original.<ClickySlug>k__BackingField;
      this.NewClickyName = original.<NewClickyName>k__BackingField;
      this.NewClickyRole = original.<NewClickyRole>k__BackingField;
      this.NewClickyOneLiner = original.<NewClickyOneLiner>k__BackingField;
      this.LaunchContext = original.<LaunchContext>k__BackingField;
      this.LaunchSource = original.<LaunchSource>k__BackingField;
      this.ImagesJpeg = original.<ImagesJpeg>k__BackingField;
      this.AttachedFilePaths = original.<AttachedFilePaths>k__BackingField;
      this.UsageContext = original.<UsageContext>k__BackingField;
      this.AsJob = original.<AsJob>k__BackingField;
      this.ChromeChoice = original.<ChromeChoice>k__BackingField;
      this.BuildsOnJobIds = original.<BuildsOnJobIds>k__BackingField;
    }

    // Token: 0x0600098B RID: 2443 RVA: 0x0036E394 File Offset: 0x0036E394
    [CompilerGenerated]
    public void Deconstruct([Nullable(1)] out string Prompt, out string ClickySlug, out string NewClickyName, [Nullable(1)] out string NewClickyRole, out string NewClickyOneLiner, out string LaunchContext, [Nullable(1)] out string LaunchSource, [Nullable(new byte[] { 2, 1 })] out IReadOnlyList<byte[]> ImagesJpeg, [Nullable(new byte[] { 2, 1 })] out IReadOnlyList<string> AttachedFilePaths, [Nullable(1)] out string UsageContext, out bool AsJob, out string ChromeChoice, [Nullable(new byte[] { 2, 1 })] out IReadOnlyList<string> BuildsOnJobIds)
    {
      Prompt = this.Prompt;
      ClickySlug = this.ClickySlug;
      NewClickyName = this.NewClickyName;
      NewClickyRole = this.NewClickyRole;
      NewClickyOneLiner = this.NewClickyOneLiner;
      LaunchContext = this.LaunchContext;
      LaunchSource = this.LaunchSource;
      ImagesJpeg = this.ImagesJpeg;
      AttachedFilePaths = this.AttachedFilePaths;
      UsageContext = this.UsageContext;
      AsJob = this.AsJob;
      ChromeChoice = this.ChromeChoice;
      BuildsOnJobIds = this.BuildsOnJobIds;
    }

    // Token: 0x0400045B RID: 1115
    [Nullable(1)]
    public const string UsersOwnWordsContextPrefix = "HeyClicky passed the user's message to you exactly as they";
  }
}
