using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace HeyClicky.Agents.Jobs
{
  // Token: 0x02000034 RID: 52
  [NullableContext(1)]
  [Nullable(0)]
  public sealed class ClickyAppLiveStatus : IEquatable<ClickyAppLiveStatus>
  {
    // Token: 0x0600052C RID: 1324 RVA: 0x000EFCC0 File Offset: 0x000EFCC0
    public ClickyAppLiveStatus(ClickyAppLiveStatus.Phases Phase, string Line, IReadOnlyList<string> ProgressLines, DateTimeOffset? StartedAt, DateTimeOffset? FinishedAt)
    {
      this.Phase = Phase;
      this.Line = Line;
      this.ProgressLines = ProgressLines;
      this.StartedAt = StartedAt;
      this.FinishedAt = FinishedAt;
      base..ctor();
    }

    // Token: 0x17000157 RID: 343
    // (get) Token: 0x0600052D RID: 1325 RVA: 0x000EFCF0 File Offset: 0x000EFCF0
    [CompilerGenerated]
    private Type EqualityContract
    {
      [CompilerGenerated]
      get
      {
        return typeof(ClickyAppLiveStatus);
      }
    }

    // Token: 0x17000158 RID: 344
    // (get) Token: 0x0600052E RID: 1326 RVA: 0x000EFCFC File Offset: 0x000EFCFC
    // (set) Token: 0x0600052F RID: 1327 RVA: 0x000EFD04 File Offset: 0x000EFD04
    public ClickyAppLiveStatus.Phases Phase { get; set; }

    // Token: 0x17000159 RID: 345
    // (get) Token: 0x06000530 RID: 1328 RVA: 0x000EFD10 File Offset: 0x000EFD10
    // (set) Token: 0x06000531 RID: 1329 RVA: 0x000EFD18 File Offset: 0x000EFD18
    public string Line { get; set; }

    // Token: 0x1700015A RID: 346
    // (get) Token: 0x06000532 RID: 1330 RVA: 0x000EFD24 File Offset: 0x000EFD24
    // (set) Token: 0x06000533 RID: 1331 RVA: 0x000EFD2C File Offset: 0x000EFD2C
    public IReadOnlyList<string> ProgressLines { get; set; }

    // Token: 0x1700015B RID: 347
    // (get) Token: 0x06000534 RID: 1332 RVA: 0x000EFD38 File Offset: 0x000EFD38
    // (set) Token: 0x06000535 RID: 1333 RVA: 0x000EFD40 File Offset: 0x000EFD40
    public DateTimeOffset? StartedAt { get; set; }

    // Token: 0x1700015C RID: 348
    // (get) Token: 0x06000536 RID: 1334 RVA: 0x000EFD4C File Offset: 0x000EFD4C
    // (set) Token: 0x06000537 RID: 1335 RVA: 0x000EFD54 File Offset: 0x000EFD54
    public DateTimeOffset? FinishedAt { get; set; }

    // Token: 0x06000538 RID: 1336 RVA: 0x000EFD60 File Offset: 0x000EFD60
    [CompilerGenerated]
    public override string ToString()
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("ClickyAppLiveStatus");
      stringBuilder.Append(" { ");
      if (this.PrintMembers(stringBuilder))
      {
        stringBuilder.Append(' ');
      }
      stringBuilder.Append('}');
      return stringBuilder.ToString();
    }

    // Token: 0x06000539 RID: 1337 RVA: 0x000EFDAC File Offset: 0x000EFDAC
    [CompilerGenerated]
    private bool PrintMembers(StringBuilder builder)
    {
      RuntimeHelpers.EnsureSufficientExecutionStack();
      builder.Append("Phase = ");
      builder.Append(this.Phase.ToString());
      builder.Append(", Line = ");
      builder.Append(this.Line);
      builder.Append(", ProgressLines = ");
      builder.Append(this.ProgressLines);
      builder.Append(", StartedAt = ");
      builder.Append(this.StartedAt.ToString());
      builder.Append(", FinishedAt = ");
      builder.Append(this.FinishedAt.ToString());
      return true;
    }

    // Token: 0x0600053A RID: 1338 RVA: 0x000EFE68 File Offset: 0x000EFE68
    [NullableContext(2)]
    [CompilerGenerated]
    public static bool operator !=(ClickyAppLiveStatus left, ClickyAppLiveStatus right)
    {
      return !(left == right);
    }

    // Token: 0x0600053B RID: 1339 RVA: 0x000EFE74 File Offset: 0x000EFE74
    [NullableContext(2)]
    [CompilerGenerated]
    public static bool operator ==(ClickyAppLiveStatus left, ClickyAppLiveStatus right)
    {
      return left == right || (left != null && left.Equals(right));
    }

    // Token: 0x0600053C RID: 1340 RVA: 0x000EFE88 File Offset: 0x000EFE88
    [CompilerGenerated]
    public override int GetHashCode()
    {
      return ((((EqualityComparer<Type>.Default.GetHashCode(this.EqualityContract) * -1521134295 + EqualityComparer<ClickyAppLiveStatus.Phases>.Default.GetHashCode(this.<Phase>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Line>k__BackingField)) * -1521134295 + EqualityComparer<IReadOnlyList<string>>.Default.GetHashCode(this.<ProgressLines>k__BackingField)) * -1521134295 + EqualityComparer<DateTimeOffset?>.Default.GetHashCode(this.<StartedAt>k__BackingField)) * -1521134295 + EqualityComparer<DateTimeOffset?>.Default.GetHashCode(this.<FinishedAt>k__BackingField);
    }

    // Token: 0x0600053D RID: 1341 RVA: 0x000EFF18 File Offset: 0x000EFF18
    [NullableContext(2)]
    [CompilerGenerated]
    public override bool Equals(object obj)
    {
      return this.Equals(obj as ClickyAppLiveStatus);
    }

    // Token: 0x0600053E RID: 1342 RVA: 0x000EFF28 File Offset: 0x000EFF28
    [NullableContext(2)]
    [CompilerGenerated]
    public bool Equals(ClickyAppLiveStatus other)
    {
      return this == other || (other != null && this.EqualityContract == other.EqualityContract && EqualityComparer<ClickyAppLiveStatus.Phases>.Default.Equals(this.<Phase>k__BackingField, other.<Phase>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<Line>k__BackingField, other.<Line>k__BackingField) && EqualityComparer<IReadOnlyList<string>>.Default.Equals(this.<ProgressLines>k__BackingField, other.<ProgressLines>k__BackingField) && EqualityComparer<DateTimeOffset?>.Default.Equals(this.<StartedAt>k__BackingField, other.<StartedAt>k__BackingField) && EqualityComparer<DateTimeOffset?>.Default.Equals(this.<FinishedAt>k__BackingField, other.<FinishedAt>k__BackingField));
    }

    // Token: 0x06000540 RID: 1344 RVA: 0x000EFFD8 File Offset: 0x000EFFD8
    [CompilerGenerated]
    private ClickyAppLiveStatus(ClickyAppLiveStatus original)
    {
      this.Phase = original.<Phase>k__BackingField;
      this.Line = original.<Line>k__BackingField;
      this.ProgressLines = original.<ProgressLines>k__BackingField;
      this.StartedAt = original.<StartedAt>k__BackingField;
      this.FinishedAt = original.<FinishedAt>k__BackingField;
    }

    // Token: 0x06000541 RID: 1345 RVA: 0x000F0028 File Offset: 0x000F0028
    [CompilerGenerated]
    public void Deconstruct(out ClickyAppLiveStatus.Phases Phase, out string Line, out IReadOnlyList<string> ProgressLines, out DateTimeOffset? StartedAt, out DateTimeOffset? FinishedAt)
    {
      Phase = this.Phase;
      Line = this.Line;
      ProgressLines = this.ProgressLines;
      StartedAt = this.StartedAt;
      FinishedAt = this.FinishedAt;
    }

    // Token: 0x040001E9 RID: 489
    public static readonly ClickyAppLiveStatus Idle = new ClickyAppLiveStatus(ClickyAppLiveStatus.Phases.Idle, "", Array.Empty<string>(), null, null);

    // Token: 0x02000148 RID: 328
    [NullableContext(0)]
    public enum Phases
    {
      // Token: 0x040006D9 RID: 1753
      Idle,
      // Token: 0x040006DA RID: 1754
      Working,
      // Token: 0x040006DB RID: 1755
      NeedsYou,
      // Token: 0x040006DC RID: 1756
      Finished,
      // Token: 0x040006DD RID: 1757
      Failed
    }
  }
}
