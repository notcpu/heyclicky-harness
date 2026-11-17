using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace HeyClicky.Core.Proactive
{
  // Token: 0x020000E1 RID: 225
  [NullableContext(1)]
  [Nullable(0)]
  public sealed class UpcomingCalendarFetch : IEquatable<UpcomingCalendarFetch>
  {
    // Token: 0x06000EA3 RID: 3747 RVA: 0x0038BC24 File Offset: 0x0038BC24
    public UpcomingCalendarFetch(bool IsCalendarConnected, IReadOnlyList<UpcomingCalendarEvent> Events)
    {
      this.IsCalendarConnected = IsCalendarConnected;
      this.Events = Events;
      base..ctor();
    }

    // Token: 0x17000353 RID: 851
    // (get) Token: 0x06000EA4 RID: 3748 RVA: 0x0038BC3C File Offset: 0x0038BC3C
    [CompilerGenerated]
    private Type EqualityContract
    {
      [CompilerGenerated]
      get
      {
        return typeof(UpcomingCalendarFetch);
      }
    }

    // Token: 0x17000354 RID: 852
    // (get) Token: 0x06000EA5 RID: 3749 RVA: 0x0038BC48 File Offset: 0x0038BC48
    // (set) Token: 0x06000EA6 RID: 3750 RVA: 0x0038BC50 File Offset: 0x0038BC50
    public bool IsCalendarConnected { get; set; }

    // Token: 0x17000355 RID: 853
    // (get) Token: 0x06000EA7 RID: 3751 RVA: 0x0038BC5C File Offset: 0x0038BC5C
    // (set) Token: 0x06000EA8 RID: 3752 RVA: 0x0038BC64 File Offset: 0x0038BC64
    public IReadOnlyList<UpcomingCalendarEvent> Events { get; set; }

    // Token: 0x06000EA9 RID: 3753 RVA: 0x0038BC70 File Offset: 0x0038BC70
    [CompilerGenerated]
    public override string ToString()
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("UpcomingCalendarFetch");
      stringBuilder.Append(" { ");
      if (this.PrintMembers(stringBuilder))
      {
        stringBuilder.Append(' ');
      }
      stringBuilder.Append('}');
      return stringBuilder.ToString();
    }

    // Token: 0x06000EAA RID: 3754 RVA: 0x0038BCBC File Offset: 0x0038BCBC
    [CompilerGenerated]
    private bool PrintMembers(StringBuilder builder)
    {
      RuntimeHelpers.EnsureSufficientExecutionStack();
      builder.Append("IsCalendarConnected = ");
      builder.Append(this.IsCalendarConnected.ToString());
      builder.Append(", Events = ");
      builder.Append(this.Events);
      return true;
    }

    // Token: 0x06000EAB RID: 3755 RVA: 0x0038BD10 File Offset: 0x0038BD10
    [NullableContext(2)]
    [CompilerGenerated]
    public static bool operator !=(UpcomingCalendarFetch left, UpcomingCalendarFetch right)
    {
      return !(left == right);
    }

    // Token: 0x06000EAC RID: 3756 RVA: 0x0038BD1C File Offset: 0x0038BD1C
    [NullableContext(2)]
    [CompilerGenerated]
    public static bool operator ==(UpcomingCalendarFetch left, UpcomingCalendarFetch right)
    {
      return left == right || (left != null && left.Equals(right));
    }

    // Token: 0x06000EAD RID: 3757 RVA: 0x0038BD30 File Offset: 0x0038BD30
    [CompilerGenerated]
    public override int GetHashCode()
    {
      return (EqualityComparer<Type>.Default.GetHashCode(this.EqualityContract) * -1521134295 + EqualityComparer<bool>.Default.GetHashCode(this.<IsCalendarConnected>k__BackingField)) * -1521134295 + EqualityComparer<IReadOnlyList<UpcomingCalendarEvent>>.Default.GetHashCode(this.<Events>k__BackingField);
    }

    // Token: 0x06000EAE RID: 3758 RVA: 0x0038BD70 File Offset: 0x0038BD70
    [NullableContext(2)]
    [CompilerGenerated]
    public override bool Equals(object obj)
    {
      return this.Equals(obj as UpcomingCalendarFetch);
    }

    // Token: 0x06000EAF RID: 3759 RVA: 0x0038BD80 File Offset: 0x0038BD80
    [NullableContext(2)]
    [CompilerGenerated]
    public bool Equals(UpcomingCalendarFetch other)
    {
      return this == other || (other != null && this.EqualityContract == other.EqualityContract && EqualityComparer<bool>.Default.Equals(this.<IsCalendarConnected>k__BackingField, other.<IsCalendarConnected>k__BackingField) && EqualityComparer<IReadOnlyList<UpcomingCalendarEvent>>.Default.Equals(this.<Events>k__BackingField, other.<Events>k__BackingField));
    }

    // Token: 0x06000EB1 RID: 3761 RVA: 0x0038BDE4 File Offset: 0x0038BDE4
    [CompilerGenerated]
    private UpcomingCalendarFetch(UpcomingCalendarFetch original)
    {
      this.IsCalendarConnected = original.<IsCalendarConnected>k__BackingField;
      this.Events = original.<Events>k__BackingField;
    }

    // Token: 0x06000EB2 RID: 3762 RVA: 0x0038BE04 File Offset: 0x0038BE04
    [CompilerGenerated]
    public void Deconstruct(out bool IsCalendarConnected, out IReadOnlyList<UpcomingCalendarEvent> Events)
    {
      IsCalendarConnected = this.IsCalendarConnected;
      Events = this.Events;
    }
  }
}
