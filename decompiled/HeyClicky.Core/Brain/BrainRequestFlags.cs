using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json;

namespace HeyClicky.Core.Brain
{
  // Token: 0x020001F9 RID: 505
  [NullableContext(1)]
  [Nullable(0)]
  public sealed class BrainRequestFlags : IEquatable<BrainRequestFlags>
  {
    // Token: 0x06001D54 RID: 7508 RVA: 0x003E87EC File Offset: 0x003E87EC
    public BrainRequestFlags(bool JobsWorld, bool SavesDrawings, bool Drive)
    {
      this.JobsWorld = JobsWorld;
      this.SavesDrawings = SavesDrawings;
      this.Drive = Drive;
      base..ctor();
    }

    // Token: 0x17000673 RID: 1651
    // (get) Token: 0x06001D55 RID: 7509 RVA: 0x003E880C File Offset: 0x003E880C
    [CompilerGenerated]
    private Type EqualityContract
    {
      [CompilerGenerated]
      get
      {
        return typeof(BrainRequestFlags);
      }
    }

    // Token: 0x17000674 RID: 1652
    // (get) Token: 0x06001D56 RID: 7510 RVA: 0x003E8818 File Offset: 0x003E8818
    // (set) Token: 0x06001D57 RID: 7511 RVA: 0x003E8820 File Offset: 0x003E8820
    public bool JobsWorld { get; set; }

    // Token: 0x17000675 RID: 1653
    // (get) Token: 0x06001D58 RID: 7512 RVA: 0x003E882C File Offset: 0x003E882C
    // (set) Token: 0x06001D59 RID: 7513 RVA: 0x003E8834 File Offset: 0x003E8834
    public bool SavesDrawings { get; set; }

    // Token: 0x17000676 RID: 1654
    // (get) Token: 0x06001D5A RID: 7514 RVA: 0x003E8840 File Offset: 0x003E8840
    // (set) Token: 0x06001D5B RID: 7515 RVA: 0x003E8848 File Offset: 0x003E8848
    public bool Drive { get; set; }

    // Token: 0x17000677 RID: 1655
    // (get) Token: 0x06001D5C RID: 7516 RVA: 0x003E8854 File Offset: 0x003E8854
    // (set) Token: 0x06001D5D RID: 7517 RVA: 0x003E885C File Offset: 0x003E885C
    public bool FiguresFloat { get; set; }

    // Token: 0x06001D5E RID: 7518 RVA: 0x003E8868 File Offset: 0x003E8868
    public void Write(Utf8JsonWriter writer)
    {
      if (this.FiguresFloat)
      {
        writer.WriteBoolean("figures_float", true);
      }
      if (this.JobsWorld)
      {
        writer.WriteBoolean("library_items", true);
        writer.WriteBoolean("routines_tool", true);
        writer.WriteBoolean("find_jobs_tool", true);
        writer.WriteBoolean("job_handoffs", true);
      }
      if (this.SavesDrawings)
      {
        writer.WriteBoolean("saves_drawings", true);
      }
      if (this.Drive)
      {
        writer.WriteBoolean("drive", true);
      }
    }

    // Token: 0x06001D5F RID: 7519 RVA: 0x003E88EC File Offset: 0x003E88EC
    [CompilerGenerated]
    public override string ToString()
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("BrainRequestFlags");
      stringBuilder.Append(" { ");
      if (this.PrintMembers(stringBuilder))
      {
        stringBuilder.Append(' ');
      }
      stringBuilder.Append('}');
      return stringBuilder.ToString();
    }

    // Token: 0x06001D60 RID: 7520 RVA: 0x003E8938 File Offset: 0x003E8938
    [CompilerGenerated]
    private bool PrintMembers(StringBuilder builder)
    {
      RuntimeHelpers.EnsureSufficientExecutionStack();
      builder.Append("JobsWorld = ");
      builder.Append(this.JobsWorld.ToString());
      builder.Append(", SavesDrawings = ");
      builder.Append(this.SavesDrawings.ToString());
      builder.Append(", Drive = ");
      builder.Append(this.Drive.ToString());
      builder.Append(", FiguresFloat = ");
      builder.Append(this.FiguresFloat.ToString());
      return true;
    }

    // Token: 0x06001D61 RID: 7521 RVA: 0x003E89E8 File Offset: 0x003E89E8
    [NullableContext(2)]
    [CompilerGenerated]
    public static bool operator !=(BrainRequestFlags left, BrainRequestFlags right)
    {
      return !(left == right);
    }

    // Token: 0x06001D62 RID: 7522 RVA: 0x003E89F4 File Offset: 0x003E89F4
    [NullableContext(2)]
    [CompilerGenerated]
    public static bool operator ==(BrainRequestFlags left, BrainRequestFlags right)
    {
      return left == right || (left != null && left.Equals(right));
    }

    // Token: 0x06001D63 RID: 7523 RVA: 0x003E8A08 File Offset: 0x003E8A08
    [CompilerGenerated]
    public override int GetHashCode()
    {
      return (((EqualityComparer<Type>.Default.GetHashCode(this.EqualityContract) * -1521134295 + EqualityComparer<bool>.Default.GetHashCode(this.<JobsWorld>k__BackingField)) * -1521134295 + EqualityComparer<bool>.Default.GetHashCode(this.<SavesDrawings>k__BackingField)) * -1521134295 + EqualityComparer<bool>.Default.GetHashCode(this.<Drive>k__BackingField)) * -1521134295 + EqualityComparer<bool>.Default.GetHashCode(this.<FiguresFloat>k__BackingField);
    }

    // Token: 0x06001D64 RID: 7524 RVA: 0x003E8A84 File Offset: 0x003E8A84
    [NullableContext(2)]
    [CompilerGenerated]
    public override bool Equals(object obj)
    {
      return this.Equals(obj as BrainRequestFlags);
    }

    // Token: 0x06001D65 RID: 7525 RVA: 0x003E8A94 File Offset: 0x003E8A94
    [NullableContext(2)]
    [CompilerGenerated]
    public bool Equals(BrainRequestFlags other)
    {
      return this == other || (other != null && this.EqualityContract == other.EqualityContract && EqualityComparer<bool>.Default.Equals(this.<JobsWorld>k__BackingField, other.<JobsWorld>k__BackingField) && EqualityComparer<bool>.Default.Equals(this.<SavesDrawings>k__BackingField, other.<SavesDrawings>k__BackingField) && EqualityComparer<bool>.Default.Equals(this.<Drive>k__BackingField, other.<Drive>k__BackingField) && EqualityComparer<bool>.Default.Equals(this.<FiguresFloat>k__BackingField, other.<FiguresFloat>k__BackingField));
    }

    // Token: 0x06001D67 RID: 7527 RVA: 0x003E8B28 File Offset: 0x003E8B28
    [CompilerGenerated]
    private BrainRequestFlags(BrainRequestFlags original)
    {
      this.JobsWorld = original.<JobsWorld>k__BackingField;
      this.SavesDrawings = original.<SavesDrawings>k__BackingField;
      this.Drive = original.<Drive>k__BackingField;
      this.FiguresFloat = original.<FiguresFloat>k__BackingField;
    }

    // Token: 0x06001D68 RID: 7528 RVA: 0x003E8B60 File Offset: 0x003E8B60
    [CompilerGenerated]
    public void Deconstruct(out bool JobsWorld, out bool SavesDrawings, out bool Drive)
    {
      JobsWorld = this.JobsWorld;
      SavesDrawings = this.SavesDrawings;
      Drive = this.Drive;
    }

    // Token: 0x04000CA2 RID: 3234
    public static readonly BrainRequestFlags None = new BrainRequestFlags(false, false, false);
  }
}
