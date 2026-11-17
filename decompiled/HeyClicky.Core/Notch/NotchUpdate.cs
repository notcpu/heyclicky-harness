using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace HeyClicky.Core.Notch
{
  // Token: 0x0200011B RID: 283
  [NullableContext(1)]
  [Nullable(0)]
  public sealed class NotchUpdate : IEquatable<NotchUpdate>
  {
    // Token: 0x0600128B RID: 4747 RVA: 0x003A21FC File Offset: 0x003A21FC
    public NotchUpdate(string RailKey, NotchUpdateKind Kind)
    {
      this.RailKey = RailKey;
      this.Kind = Kind;
      this.Id = Guid.NewGuid();
      base..ctor();
    }

    // Token: 0x17000450 RID: 1104
    // (get) Token: 0x0600128C RID: 4748 RVA: 0x003A2220 File Offset: 0x003A2220
    [CompilerGenerated]
    private Type EqualityContract
    {
      [CompilerGenerated]
      get
      {
        return typeof(NotchUpdate);
      }
    }

    // Token: 0x17000451 RID: 1105
    // (get) Token: 0x0600128D RID: 4749 RVA: 0x003A222C File Offset: 0x003A222C
    // (set) Token: 0x0600128E RID: 4750 RVA: 0x003A2234 File Offset: 0x003A2234
    public string RailKey { get; set; }

    // Token: 0x17000452 RID: 1106
    // (get) Token: 0x0600128F RID: 4751 RVA: 0x003A2240 File Offset: 0x003A2240
    // (set) Token: 0x06001290 RID: 4752 RVA: 0x003A2248 File Offset: 0x003A2248
    public NotchUpdateKind Kind { get; set; }

    // Token: 0x17000453 RID: 1107
    // (get) Token: 0x06001291 RID: 4753 RVA: 0x003A2254 File Offset: 0x003A2254
    public Guid Id { get; }

    // Token: 0x17000454 RID: 1108
    // (get) Token: 0x06001292 RID: 4754 RVA: 0x003A225C File Offset: 0x003A225C
    public bool ClosesOnItsOwn
    {
      get
      {
        return this.Kind == NotchUpdateKind.Done;
      }
    }

    // Token: 0x06001293 RID: 4755 RVA: 0x003A2268 File Offset: 0x003A2268
    [CompilerGenerated]
    public override string ToString()
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("NotchUpdate");
      stringBuilder.Append(" { ");
      if (this.PrintMembers(stringBuilder))
      {
        stringBuilder.Append(' ');
      }
      stringBuilder.Append('}');
      return stringBuilder.ToString();
    }

    // Token: 0x06001294 RID: 4756 RVA: 0x003A22B4 File Offset: 0x003A22B4
    [CompilerGenerated]
    private bool PrintMembers(StringBuilder builder)
    {
      RuntimeHelpers.EnsureSufficientExecutionStack();
      builder.Append("RailKey = ");
      builder.Append(this.RailKey);
      builder.Append(", Kind = ");
      builder.Append(this.Kind.ToString());
      builder.Append(", Id = ");
      builder.Append(this.Id.ToString());
      builder.Append(", ClosesOnItsOwn = ");
      builder.Append(this.ClosesOnItsOwn.ToString());
      return true;
    }

    // Token: 0x06001295 RID: 4757 RVA: 0x003A2358 File Offset: 0x003A2358
    [NullableContext(2)]
    [CompilerGenerated]
    public static bool operator !=(NotchUpdate left, NotchUpdate right)
    {
      return !(left == right);
    }

    // Token: 0x06001296 RID: 4758 RVA: 0x003A2364 File Offset: 0x003A2364
    [NullableContext(2)]
    [CompilerGenerated]
    public static bool operator ==(NotchUpdate left, NotchUpdate right)
    {
      return left == right || (left != null && left.Equals(right));
    }

    // Token: 0x06001297 RID: 4759 RVA: 0x003A2378 File Offset: 0x003A2378
    [CompilerGenerated]
    public override int GetHashCode()
    {
      return ((EqualityComparer<Type>.Default.GetHashCode(this.EqualityContract) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<RailKey>k__BackingField)) * -1521134295 + EqualityComparer<NotchUpdateKind>.Default.GetHashCode(this.<Kind>k__BackingField)) * -1521134295 + EqualityComparer<Guid>.Default.GetHashCode(this.<Id>k__BackingField);
    }

    // Token: 0x06001298 RID: 4760 RVA: 0x003A23DC File Offset: 0x003A23DC
    [NullableContext(2)]
    [CompilerGenerated]
    public override bool Equals(object obj)
    {
      return this.Equals(obj as NotchUpdate);
    }

    // Token: 0x06001299 RID: 4761 RVA: 0x003A23EC File Offset: 0x003A23EC
    [NullableContext(2)]
    [CompilerGenerated]
    public bool Equals(NotchUpdate other)
    {
      return this == other || (other != null && this.EqualityContract == other.EqualityContract && EqualityComparer<string>.Default.Equals(this.<RailKey>k__BackingField, other.<RailKey>k__BackingField) && EqualityComparer<NotchUpdateKind>.Default.Equals(this.<Kind>k__BackingField, other.<Kind>k__BackingField) && EqualityComparer<Guid>.Default.Equals(this.<Id>k__BackingField, other.<Id>k__BackingField));
    }

    // Token: 0x0600129B RID: 4763 RVA: 0x003A2468 File Offset: 0x003A2468
    [CompilerGenerated]
    private NotchUpdate(NotchUpdate original)
    {
      this.RailKey = original.<RailKey>k__BackingField;
      this.Kind = original.<Kind>k__BackingField;
      this.Id = original.<Id>k__BackingField;
    }

    // Token: 0x0600129C RID: 4764 RVA: 0x003A2494 File Offset: 0x003A2494
    [CompilerGenerated]
    public void Deconstruct(out string RailKey, out NotchUpdateKind Kind)
    {
      RailKey = this.RailKey;
      Kind = this.Kind;
    }
  }
}
