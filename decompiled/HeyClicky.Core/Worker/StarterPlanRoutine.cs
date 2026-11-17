using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace HeyClicky.Core.Worker
{
  // Token: 0x0200004F RID: 79
  [NullableContext(1)]
  [Nullable(0)]
  public sealed class StarterPlanRoutine : IEquatable<StarterPlanRoutine>
  {
    // Token: 0x06000381 RID: 897 RVA: 0x00353C28 File Offset: 0x00353C28
    public StarterPlanRoutine(string Title, int EveryMinutes)
    {
      this.Title = Title;
      this.EveryMinutes = EveryMinutes;
      base..ctor();
    }

    // Token: 0x170000C0 RID: 192
    // (get) Token: 0x06000382 RID: 898 RVA: 0x00353C40 File Offset: 0x00353C40
    [CompilerGenerated]
    private Type EqualityContract
    {
      [CompilerGenerated]
      get
      {
        return typeof(StarterPlanRoutine);
      }
    }

    // Token: 0x170000C1 RID: 193
    // (get) Token: 0x06000383 RID: 899 RVA: 0x00353C4C File Offset: 0x00353C4C
    // (set) Token: 0x06000384 RID: 900 RVA: 0x00353C54 File Offset: 0x00353C54
    public string Title { get; set; }

    // Token: 0x170000C2 RID: 194
    // (get) Token: 0x06000385 RID: 901 RVA: 0x00353C60 File Offset: 0x00353C60
    // (set) Token: 0x06000386 RID: 902 RVA: 0x00353C68 File Offset: 0x00353C68
    public int EveryMinutes { get; set; }

    // Token: 0x06000387 RID: 903 RVA: 0x00353C74 File Offset: 0x00353C74
    [CompilerGenerated]
    public override string ToString()
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("StarterPlanRoutine");
      stringBuilder.Append(" { ");
      if (this.PrintMembers(stringBuilder))
      {
        stringBuilder.Append(' ');
      }
      stringBuilder.Append('}');
      return stringBuilder.ToString();
    }

    // Token: 0x06000388 RID: 904 RVA: 0x00353CC0 File Offset: 0x00353CC0
    [CompilerGenerated]
    private bool PrintMembers(StringBuilder builder)
    {
      RuntimeHelpers.EnsureSufficientExecutionStack();
      builder.Append("Title = ");
      builder.Append(this.Title);
      builder.Append(", EveryMinutes = ");
      builder.Append(this.EveryMinutes.ToString());
      return true;
    }

    // Token: 0x06000389 RID: 905 RVA: 0x00353D14 File Offset: 0x00353D14
    [NullableContext(2)]
    [CompilerGenerated]
    public static bool operator !=(StarterPlanRoutine left, StarterPlanRoutine right)
    {
      return !(left == right);
    }

    // Token: 0x0600038A RID: 906 RVA: 0x00353D20 File Offset: 0x00353D20
    [NullableContext(2)]
    [CompilerGenerated]
    public static bool operator ==(StarterPlanRoutine left, StarterPlanRoutine right)
    {
      return left == right || (left != null && left.Equals(right));
    }

    // Token: 0x0600038B RID: 907 RVA: 0x00353D34 File Offset: 0x00353D34
    [CompilerGenerated]
    public override int GetHashCode()
    {
      return (EqualityComparer<Type>.Default.GetHashCode(this.EqualityContract) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Title>k__BackingField)) * -1521134295 + EqualityComparer<int>.Default.GetHashCode(this.<EveryMinutes>k__BackingField);
    }

    // Token: 0x0600038C RID: 908 RVA: 0x00353D74 File Offset: 0x00353D74
    [NullableContext(2)]
    [CompilerGenerated]
    public override bool Equals(object obj)
    {
      return this.Equals(obj as StarterPlanRoutine);
    }

    // Token: 0x0600038D RID: 909 RVA: 0x00353D84 File Offset: 0x00353D84
    [NullableContext(2)]
    [CompilerGenerated]
    public bool Equals(StarterPlanRoutine other)
    {
      return this == other || (other != null && this.EqualityContract == other.EqualityContract && EqualityComparer<string>.Default.Equals(this.<Title>k__BackingField, other.<Title>k__BackingField) && EqualityComparer<int>.Default.Equals(this.<EveryMinutes>k__BackingField, other.<EveryMinutes>k__BackingField));
    }

    // Token: 0x0600038F RID: 911 RVA: 0x00353DE8 File Offset: 0x00353DE8
    [CompilerGenerated]
    private StarterPlanRoutine(StarterPlanRoutine original)
    {
      this.Title = original.<Title>k__BackingField;
      this.EveryMinutes = original.<EveryMinutes>k__BackingField;
    }

    // Token: 0x06000390 RID: 912 RVA: 0x00353E08 File Offset: 0x00353E08
    [CompilerGenerated]
    public void Deconstruct(out string Title, out int EveryMinutes)
    {
      Title = this.Title;
      EveryMinutes = this.EveryMinutes;
    }
  }
}
