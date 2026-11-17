using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using HeyClicky.Core.Proactive;

namespace HeyClicky.App.Proactive
{
  // Token: 0x020000F3 RID: 243
  [NullableContext(1)]
  [Nullable(0)]
  public sealed class ProactiveJobLaunch : IEquatable<ProactiveJobLaunch>
  {
    // Token: 0x06000CB7 RID: 3255 RVA: 0x005631B8 File Offset: 0x005631B8
    public ProactiveJobLaunch(ProactiveSuggestionCard Card, string Transcript, string LaunchContext)
    {
      this.Card = Card;
      this.Transcript = Transcript;
      this.LaunchContext = LaunchContext;
      base..ctor();
    }

    // Token: 0x1700023E RID: 574
    // (get) Token: 0x06000CB8 RID: 3256 RVA: 0x005631D8 File Offset: 0x005631D8
    [CompilerGenerated]
    private Type EqualityContract
    {
      [CompilerGenerated]
      get
      {
        return typeof(ProactiveJobLaunch);
      }
    }

    // Token: 0x1700023F RID: 575
    // (get) Token: 0x06000CB9 RID: 3257 RVA: 0x005631E4 File Offset: 0x005631E4
    // (set) Token: 0x06000CBA RID: 3258 RVA: 0x005631EC File Offset: 0x005631EC
    public ProactiveSuggestionCard Card { get; set; }

    // Token: 0x17000240 RID: 576
    // (get) Token: 0x06000CBB RID: 3259 RVA: 0x005631F8 File Offset: 0x005631F8
    // (set) Token: 0x06000CBC RID: 3260 RVA: 0x00563200 File Offset: 0x00563200
    public string Transcript { get; set; }

    // Token: 0x17000241 RID: 577
    // (get) Token: 0x06000CBD RID: 3261 RVA: 0x0056320C File Offset: 0x0056320C
    // (set) Token: 0x06000CBE RID: 3262 RVA: 0x00563214 File Offset: 0x00563214
    public string LaunchContext { get; set; }

    // Token: 0x06000CBF RID: 3263 RVA: 0x00563220 File Offset: 0x00563220
    [CompilerGenerated]
    public override string ToString()
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("ProactiveJobLaunch");
      stringBuilder.Append(" { ");
      if (this.PrintMembers(stringBuilder))
      {
        stringBuilder.Append(' ');
      }
      stringBuilder.Append('}');
      return stringBuilder.ToString();
    }

    // Token: 0x06000CC0 RID: 3264 RVA: 0x0056326C File Offset: 0x0056326C
    [CompilerGenerated]
    private bool PrintMembers(StringBuilder builder)
    {
      RuntimeHelpers.EnsureSufficientExecutionStack();
      builder.Append("Card = ");
      builder.Append(this.Card);
      builder.Append(", Transcript = ");
      builder.Append(this.Transcript);
      builder.Append(", LaunchContext = ");
      builder.Append(this.LaunchContext);
      return true;
    }

    // Token: 0x06000CC1 RID: 3265 RVA: 0x005632CC File Offset: 0x005632CC
    [NullableContext(2)]
    [CompilerGenerated]
    public static bool operator !=(ProactiveJobLaunch left, ProactiveJobLaunch right)
    {
      return !(left == right);
    }

    // Token: 0x06000CC2 RID: 3266 RVA: 0x005632D8 File Offset: 0x005632D8
    [NullableContext(2)]
    [CompilerGenerated]
    public static bool operator ==(ProactiveJobLaunch left, ProactiveJobLaunch right)
    {
      return left == right || (left != null && left.Equals(right));
    }

    // Token: 0x06000CC3 RID: 3267 RVA: 0x005632EC File Offset: 0x005632EC
    [CompilerGenerated]
    public override int GetHashCode()
    {
      return ((EqualityComparer<Type>.Default.GetHashCode(this.EqualityContract) * -1521134295 + EqualityComparer<ProactiveSuggestionCard>.Default.GetHashCode(this.<Card>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Transcript>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<LaunchContext>k__BackingField);
    }

    // Token: 0x06000CC4 RID: 3268 RVA: 0x00563350 File Offset: 0x00563350
    [NullableContext(2)]
    [CompilerGenerated]
    public override bool Equals(object obj)
    {
      return this.Equals(obj as ProactiveJobLaunch);
    }

    // Token: 0x06000CC5 RID: 3269 RVA: 0x00563360 File Offset: 0x00563360
    [NullableContext(2)]
    [CompilerGenerated]
    public bool Equals(ProactiveJobLaunch other)
    {
      return this == other || (other != null && this.EqualityContract == other.EqualityContract && EqualityComparer<ProactiveSuggestionCard>.Default.Equals(this.<Card>k__BackingField, other.<Card>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<Transcript>k__BackingField, other.<Transcript>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<LaunchContext>k__BackingField, other.<LaunchContext>k__BackingField));
    }

    // Token: 0x06000CC7 RID: 3271 RVA: 0x005633DC File Offset: 0x005633DC
    [CompilerGenerated]
    private ProactiveJobLaunch(ProactiveJobLaunch original)
    {
      this.Card = original.<Card>k__BackingField;
      this.Transcript = original.<Transcript>k__BackingField;
      this.LaunchContext = original.<LaunchContext>k__BackingField;
    }

    // Token: 0x06000CC8 RID: 3272 RVA: 0x00563408 File Offset: 0x00563408
    [CompilerGenerated]
    public void Deconstruct(out ProactiveSuggestionCard Card, out string Transcript, out string LaunchContext)
    {
      Card = this.Card;
      Transcript = this.Transcript;
      LaunchContext = this.LaunchContext;
    }
  }
}
