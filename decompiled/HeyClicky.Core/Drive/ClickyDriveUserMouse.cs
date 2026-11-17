using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace HeyClicky.Core.Drive
{
  // Token: 0x02000197 RID: 407
  public sealed class ClickyDriveUserMouse
  {
    // Token: 0x0600180D RID: 6157 RVA: 0x003C7378 File Offset: 0x003C7378
    public ClickyDriveUserMouse.Verdict NoteMove(double x, double y, DateTimeOffset now, bool clickyHasActed)
    {
      DateTimeOffset? dateTimeOffset = this.lastMoveAt;
      bool flag;
      if (dateTimeOffset != null)
      {
        DateTimeOffset valueOrDefault = dateTimeOffset.GetValueOrDefault();
        flag = now - valueOrDefault <= ClickyDriveUserMouse.RunGap;
      }
      else
      {
        flag = false;
      }
      if (flag)
      {
        ValueTuple<double, double>? valueTuple = this.lastPoint;
        if (valueTuple != null)
        {
          ValueTuple<double, double> valueOrDefault2 = valueTuple.GetValueOrDefault();
          dateTimeOffset = this.runStartedAt;
          if (dateTimeOffset != null)
          {
            this.runDistance += Math.Sqrt(Math.Pow(x - valueOrDefault2.Item1, 2.0) + Math.Pow(y - valueOrDefault2.Item2, 2.0));
            goto IL_00BC;
          }
        }
      }
      this.runStartedAt = new DateTimeOffset?(now);
      this.runDistance = 0.0;
      this.nudgedThisRun = false;
      IL_00BC:
      this.lastMoveAt = new DateTimeOffset?(now);
      this.lastPoint = new ValueTuple<double, double>?(new ValueTuple<double, double>(x, y));
      if (clickyHasActed)
      {
        dateTimeOffset = this.runStartedAt;
        if (dateTimeOffset != null)
        {
          DateTimeOffset valueOrDefault3 = dateTimeOffset.GetValueOrDefault();
          if (now - valueOrDefault3 >= ClickyDriveUserMouse.SustainedTime && this.runDistance >= 150.0)
          {
            return new ClickyDriveUserMouse.Verdict(true, false);
          }
        }
      }
      bool flag2 = !this.nudgedThisRun && this.runDistance >= 24.0;
      if (flag2)
      {
        this.nudgedThisRun = true;
      }
      return new ClickyDriveUserMouse.Verdict(false, flag2);
    }

    // Token: 0x0600180E RID: 6158 RVA: 0x003C74DC File Offset: 0x003C74DC
    public bool IsResting(DateTimeOffset now)
    {
      DateTimeOffset? dateTimeOffset = this.lastMoveAt;
      if (dateTimeOffset != null)
      {
        DateTimeOffset valueOrDefault = dateTimeOffset.GetValueOrDefault();
        return now - valueOrDefault >= ClickyDriveUserMouse.RestTime;
      }
      return true;
    }

    // Token: 0x0600180F RID: 6159 RVA: 0x003C7514 File Offset: 0x003C7514
    public void ForgetRun()
    {
      this.lastPoint = null;
      this.runStartedAt = null;
      this.runDistance = 0.0;
      this.nudgedThisRun = false;
    }

    // Token: 0x06001810 RID: 6160 RVA: 0x003C7544 File Offset: 0x003C7544
    public void Reset()
    {
      this.ForgetRun();
      this.lastMoveAt = null;
    }

    // Token: 0x04000A54 RID: 2644
    public static readonly TimeSpan RestTime = TimeSpan.FromSeconds(1L);

    // Token: 0x04000A55 RID: 2645
    public static readonly TimeSpan RunGap = TimeSpan.FromSeconds(0.5);

    // Token: 0x04000A56 RID: 2646
    public static readonly TimeSpan SustainedTime = TimeSpan.FromSeconds(3L);

    // Token: 0x04000A57 RID: 2647
    public static readonly TimeSpan WaitCap = TimeSpan.FromSeconds(8L);

    // Token: 0x04000A58 RID: 2648
    public static readonly TimeSpan HintLinger = TimeSpan.FromSeconds(1.2);

    // Token: 0x04000A59 RID: 2649
    public const double SustainedDistance = 150.0;

    // Token: 0x04000A5A RID: 2650
    public const double HintDistance = 24.0;

    // Token: 0x04000A5B RID: 2651
    private DateTimeOffset? lastMoveAt;

    // Token: 0x04000A5C RID: 2652
    [TupleElementNames(new string[] { "X", "Y" })]
    private ValueTuple<double, double>? lastPoint;

    // Token: 0x04000A5D RID: 2653
    private DateTimeOffset? runStartedAt;

    // Token: 0x04000A5E RID: 2654
    private double runDistance;

    // Token: 0x04000A5F RID: 2655
    private bool nudgedThisRun;

    // Token: 0x02000580 RID: 1408
    public readonly struct Verdict : IEquatable<ClickyDriveUserMouse.Verdict>
    {
      // Token: 0x0600355F RID: 13663 RVA: 0x00441800 File Offset: 0x00441800
      public Verdict(bool TakeOver, bool Nudge)
      {
        this.TakeOver = TakeOver;
        this.Nudge = Nudge;
      }

      // Token: 0x17000967 RID: 2407
      // (get) Token: 0x06003560 RID: 13664 RVA: 0x00441810 File Offset: 0x00441810
      // (set) Token: 0x06003561 RID: 13665 RVA: 0x00441818 File Offset: 0x00441818
      public bool TakeOver { get; set; }

      // Token: 0x17000968 RID: 2408
      // (get) Token: 0x06003562 RID: 13666 RVA: 0x00441824 File Offset: 0x00441824
      // (set) Token: 0x06003563 RID: 13667 RVA: 0x0044182C File Offset: 0x0044182C
      public bool Nudge { get; set; }

      // Token: 0x06003564 RID: 13668 RVA: 0x00441838 File Offset: 0x00441838
      [CompilerGenerated]
      public override string ToString()
      {
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.Append("Verdict");
        stringBuilder.Append(" { ");
        if (this.PrintMembers(stringBuilder))
        {
          stringBuilder.Append(' ');
        }
        stringBuilder.Append('}');
        return stringBuilder.ToString();
      }

      // Token: 0x06003565 RID: 13669 RVA: 0x00441884 File Offset: 0x00441884
      [CompilerGenerated]
      private bool PrintMembers(StringBuilder builder)
      {
        builder.Append("TakeOver = ");
        builder.Append(this.TakeOver.ToString());
        builder.Append(", Nudge = ");
        builder.Append(this.Nudge.ToString());
        return true;
      }

      // Token: 0x06003566 RID: 13670 RVA: 0x004418E0 File Offset: 0x004418E0
      [CompilerGenerated]
      public static bool operator !=(ClickyDriveUserMouse.Verdict left, ClickyDriveUserMouse.Verdict right)
      {
        return !(left == right);
      }

      // Token: 0x06003567 RID: 13671 RVA: 0x004418EC File Offset: 0x004418EC
      [CompilerGenerated]
      public static bool operator ==(ClickyDriveUserMouse.Verdict left, ClickyDriveUserMouse.Verdict right)
      {
        return left.Equals(right);
      }

      // Token: 0x06003568 RID: 13672 RVA: 0x004418F8 File Offset: 0x004418F8
      [CompilerGenerated]
      public override int GetHashCode()
      {
        return EqualityComparer<bool>.Default.GetHashCode(this.<TakeOver>k__BackingField) * -1521134295 + EqualityComparer<bool>.Default.GetHashCode(this.<Nudge>k__BackingField);
      }

      // Token: 0x06003569 RID: 13673 RVA: 0x00441924 File Offset: 0x00441924
      [CompilerGenerated]
      public override bool Equals(object obj)
      {
        return obj is ClickyDriveUserMouse.Verdict && this.Equals((ClickyDriveUserMouse.Verdict)obj);
      }

      // Token: 0x0600356A RID: 13674 RVA: 0x0044193C File Offset: 0x0044193C
      [CompilerGenerated]
      public bool Equals(ClickyDriveUserMouse.Verdict other)
      {
        return EqualityComparer<bool>.Default.Equals(this.<TakeOver>k__BackingField, other.<TakeOver>k__BackingField) && EqualityComparer<bool>.Default.Equals(this.<Nudge>k__BackingField, other.<Nudge>k__BackingField);
      }

      // Token: 0x0600356B RID: 13675 RVA: 0x00441970 File Offset: 0x00441970
      [CompilerGenerated]
      public void Deconstruct(out bool TakeOver, out bool Nudge)
      {
        TakeOver = this.TakeOver;
        Nudge = this.Nudge;
      }
    }
  }
}
