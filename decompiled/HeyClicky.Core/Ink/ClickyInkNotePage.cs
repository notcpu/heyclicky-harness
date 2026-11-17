using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace HeyClicky.Core.Ink
{
  // Token: 0x02000167 RID: 359
  [NullableContext(1)]
  [Nullable(0)]
  public sealed class ClickyInkNotePage : IEquatable<ClickyInkNotePage>
  {
    // Token: 0x060015BB RID: 5563 RVA: 0x003BA36C File Offset: 0x003BA36C
    public ClickyInkNotePage(int Number, string UserMessage, bool IsRoutineRun = false)
    {
      this.Number = Number;
      this.UserMessage = UserMessage;
      this.IsRoutineRun = IsRoutineRun;
      base..ctor();
    }

    // Token: 0x170004FB RID: 1275
    // (get) Token: 0x060015BC RID: 5564 RVA: 0x003BA38C File Offset: 0x003BA38C
    [CompilerGenerated]
    private Type EqualityContract
    {
      [CompilerGenerated]
      get
      {
        return typeof(ClickyInkNotePage);
      }
    }

    // Token: 0x170004FC RID: 1276
    // (get) Token: 0x060015BD RID: 5565 RVA: 0x003BA398 File Offset: 0x003BA398
    // (set) Token: 0x060015BE RID: 5566 RVA: 0x003BA3A0 File Offset: 0x003BA3A0
    public int Number { get; set; }

    // Token: 0x170004FD RID: 1277
    // (get) Token: 0x060015BF RID: 5567 RVA: 0x003BA3AC File Offset: 0x003BA3AC
    // (set) Token: 0x060015C0 RID: 5568 RVA: 0x003BA3B4 File Offset: 0x003BA3B4
    public string UserMessage { get; set; }

    // Token: 0x170004FE RID: 1278
    // (get) Token: 0x060015C1 RID: 5569 RVA: 0x003BA3C0 File Offset: 0x003BA3C0
    // (set) Token: 0x060015C2 RID: 5570 RVA: 0x003BA3C8 File Offset: 0x003BA3C8
    public bool IsRoutineRun { get; set; }

    // Token: 0x060015C3 RID: 5571 RVA: 0x003BA3D4 File Offset: 0x003BA3D4
    [CompilerGenerated]
    public override string ToString()
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("ClickyInkNotePage");
      stringBuilder.Append(" { ");
      if (this.PrintMembers(stringBuilder))
      {
        stringBuilder.Append(' ');
      }
      stringBuilder.Append('}');
      return stringBuilder.ToString();
    }

    // Token: 0x060015C4 RID: 5572 RVA: 0x003BA420 File Offset: 0x003BA420
    [CompilerGenerated]
    private bool PrintMembers(StringBuilder builder)
    {
      RuntimeHelpers.EnsureSufficientExecutionStack();
      builder.Append("Number = ");
      builder.Append(this.Number.ToString());
      builder.Append(", UserMessage = ");
      builder.Append(this.UserMessage);
      builder.Append(", IsRoutineRun = ");
      builder.Append(this.IsRoutineRun.ToString());
      return true;
    }

    // Token: 0x060015C5 RID: 5573 RVA: 0x003BA49C File Offset: 0x003BA49C
    [NullableContext(2)]
    [CompilerGenerated]
    public static bool operator !=(ClickyInkNotePage left, ClickyInkNotePage right)
    {
      return !(left == right);
    }

    // Token: 0x060015C6 RID: 5574 RVA: 0x003BA4A8 File Offset: 0x003BA4A8
    [NullableContext(2)]
    [CompilerGenerated]
    public static bool operator ==(ClickyInkNotePage left, ClickyInkNotePage right)
    {
      return left == right || (left != null && left.Equals(right));
    }

    // Token: 0x060015C7 RID: 5575 RVA: 0x003BA4BC File Offset: 0x003BA4BC
    [CompilerGenerated]
    public override int GetHashCode()
    {
      return ((EqualityComparer<Type>.Default.GetHashCode(this.EqualityContract) * -1521134295 + EqualityComparer<int>.Default.GetHashCode(this.<Number>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<UserMessage>k__BackingField)) * -1521134295 + EqualityComparer<bool>.Default.GetHashCode(this.<IsRoutineRun>k__BackingField);
    }

    // Token: 0x060015C8 RID: 5576 RVA: 0x003BA520 File Offset: 0x003BA520
    [NullableContext(2)]
    [CompilerGenerated]
    public override bool Equals(object obj)
    {
      return this.Equals(obj as ClickyInkNotePage);
    }

    // Token: 0x060015C9 RID: 5577 RVA: 0x003BA530 File Offset: 0x003BA530
    [NullableContext(2)]
    [CompilerGenerated]
    public bool Equals(ClickyInkNotePage other)
    {
      return this == other || (other != null && this.EqualityContract == other.EqualityContract && EqualityComparer<int>.Default.Equals(this.<Number>k__BackingField, other.<Number>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<UserMessage>k__BackingField, other.<UserMessage>k__BackingField) && EqualityComparer<bool>.Default.Equals(this.<IsRoutineRun>k__BackingField, other.<IsRoutineRun>k__BackingField));
    }

    // Token: 0x060015CB RID: 5579 RVA: 0x003BA5AC File Offset: 0x003BA5AC
    [CompilerGenerated]
    private ClickyInkNotePage(ClickyInkNotePage original)
    {
      this.Number = original.<Number>k__BackingField;
      this.UserMessage = original.<UserMessage>k__BackingField;
      this.IsRoutineRun = original.<IsRoutineRun>k__BackingField;
    }

    // Token: 0x060015CC RID: 5580 RVA: 0x003BA5D8 File Offset: 0x003BA5D8
    [CompilerGenerated]
    public void Deconstruct(out int Number, out string UserMessage, out bool IsRoutineRun)
    {
      Number = this.Number;
      UserMessage = this.UserMessage;
      IsRoutineRun = this.IsRoutineRun;
    }
  }
}
