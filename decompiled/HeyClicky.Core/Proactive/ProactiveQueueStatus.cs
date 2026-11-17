using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

namespace HeyClicky.Core.Proactive
{
  // Token: 0x020000D4 RID: 212
  [NullableContext(1)]
  [Nullable(0)]
  public readonly struct ProactiveQueueStatus : IEquatable<ProactiveQueueStatus>
  {
    // Token: 0x06000DA6 RID: 3494 RVA: 0x00382DCC File Offset: 0x00382DCC
    public ProactiveQueueStatus(ProactiveQueueStatusKind Kind, bool EarlierIsPaused = false)
    {
      this.Kind = Kind;
      this.EarlierIsPaused = EarlierIsPaused;
    }

    // Token: 0x17000307 RID: 775
    // (get) Token: 0x06000DA7 RID: 3495 RVA: 0x00382DDC File Offset: 0x00382DDC
    // (set) Token: 0x06000DA8 RID: 3496 RVA: 0x00382DE4 File Offset: 0x00382DE4
    public ProactiveQueueStatusKind Kind { get; set; }

    // Token: 0x17000308 RID: 776
    // (get) Token: 0x06000DA9 RID: 3497 RVA: 0x00382DF0 File Offset: 0x00382DF0
    // (set) Token: 0x06000DAA RID: 3498 RVA: 0x00382DF8 File Offset: 0x00382DF8
    public bool EarlierIsPaused { get; set; }

    // Token: 0x06000DAB RID: 3499 RVA: 0x00382E04 File Offset: 0x00382E04
    public static ProactiveQueueStatus Resolve(QueuedProactiveTask entry, IReadOnlyList<QueuedProactiveTask> queue, bool isClickyBusy)
    {
      if (entry.IsLaunching)
      {
        return new ProactiveQueueStatus(ProactiveQueueStatusKind.Starting, false);
      }
      if (entry.IsPaused)
      {
        return new ProactiveQueueStatus(ProactiveQueueStatusKind.Paused, false);
      }
      QueuedProactiveTask queuedProactiveTask = queue.TakeWhile<QueuedProactiveTask>((QueuedProactiveTask e) => e.Id != entry.Id).FirstOrDefault<QueuedProactiveTask>((QueuedProactiveTask e) => e.ClickySlug == entry.ClickySlug);
      if (queuedProactiveTask != null)
      {
        return new ProactiveQueueStatus(ProactiveQueueStatusKind.WaitingForEarlierTask, queuedProactiveTask.IsPaused);
      }
      if (!isClickyBusy)
      {
        return new ProactiveQueueStatus(ProactiveQueueStatusKind.Ready, false);
      }
      return new ProactiveQueueStatus(ProactiveQueueStatusKind.WaitingForClicky, false);
    }

    // Token: 0x17000309 RID: 777
    // (get) Token: 0x06000DAC RID: 3500 RVA: 0x00382E90 File Offset: 0x00382E90
    public string Title
    {
      get
      {
        string text;
        switch (this.Kind)
        {
        case ProactiveQueueStatusKind.Ready:
          text = "Ready to start";
          break;
        case ProactiveQueueStatusKind.Starting:
          text = "Starting";
          break;
        case ProactiveQueueStatusKind.Paused:
          text = "Ready to resume";
          break;
        case ProactiveQueueStatusKind.WaitingForClicky:
          text = "Up next";
          break;
        default:
          text = "Queued";
          break;
        }
        return text;
      }
    }

    // Token: 0x06000DAD RID: 3501 RVA: 0x00382EE4 File Offset: 0x00382EE4
    public string Detail(string clickyName)
    {
      string text;
      switch (this.Kind)
      {
      case ProactiveQueueStatusKind.Ready:
        text = "Approved — starting shortly";
        break;
      case ProactiveQueueStatusKind.Starting:
        text = "Approved — getting started";
        break;
      case ProactiveQueueStatusKind.Paused:
        text = "Approved — saved for later";
        break;
      case ProactiveQueueStatusKind.WaitingForClicky:
        text = "Starts after " + clickyName + "’s current task";
        break;
      default:
        text = (this.EarlierIsPaused ? "Resume the earlier approved task first" : "Starts after the earlier approved task");
        break;
      }
      return text;
    }

    // Token: 0x1700030A RID: 778
    // (get) Token: 0x06000DAE RID: 3502 RVA: 0x00382F54 File Offset: 0x00382F54
    public string Acknowledgment
    {
      get
      {
        ProactiveQueueStatusKind kind = this.Kind;
        string text;
        if (kind > ProactiveQueueStatusKind.Starting)
        {
          if (kind != ProactiveQueueStatusKind.Paused)
          {
            text = "Queued up.";
          }
          else
          {
            text = "Saved for later.";
          }
        }
        else
        {
          text = "On it.";
        }
        return text;
      }
    }

    // Token: 0x06000DAF RID: 3503 RVA: 0x00382F8C File Offset: 0x00382F8C
    [NullableContext(0)]
    [CompilerGenerated]
    public override string ToString()
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("ProactiveQueueStatus");
      stringBuilder.Append(" { ");
      if (this.PrintMembers(stringBuilder))
      {
        stringBuilder.Append(' ');
      }
      stringBuilder.Append('}');
      return stringBuilder.ToString();
    }

    // Token: 0x06000DB0 RID: 3504 RVA: 0x00382FD8 File Offset: 0x00382FD8
    [NullableContext(0)]
    [CompilerGenerated]
    private bool PrintMembers(StringBuilder builder)
    {
      builder.Append("Kind = ");
      builder.Append(this.Kind.ToString());
      builder.Append(", EarlierIsPaused = ");
      builder.Append(this.EarlierIsPaused.ToString());
      builder.Append(", Title = ");
      builder.Append(this.Title);
      builder.Append(", Acknowledgment = ");
      builder.Append(this.Acknowledgment);
      return true;
    }

    // Token: 0x06000DB1 RID: 3505 RVA: 0x00383068 File Offset: 0x00383068
    [CompilerGenerated]
    public static bool operator !=(ProactiveQueueStatus left, ProactiveQueueStatus right)
    {
      return !(left == right);
    }

    // Token: 0x06000DB2 RID: 3506 RVA: 0x00383074 File Offset: 0x00383074
    [CompilerGenerated]
    public static bool operator ==(ProactiveQueueStatus left, ProactiveQueueStatus right)
    {
      return left.Equals(right);
    }

    // Token: 0x06000DB3 RID: 3507 RVA: 0x00383080 File Offset: 0x00383080
    [CompilerGenerated]
    public override int GetHashCode()
    {
      return EqualityComparer<ProactiveQueueStatusKind>.Default.GetHashCode(this.<Kind>k__BackingField) * -1521134295 + EqualityComparer<bool>.Default.GetHashCode(this.<EarlierIsPaused>k__BackingField);
    }

    // Token: 0x06000DB4 RID: 3508 RVA: 0x003830AC File Offset: 0x003830AC
    [NullableContext(0)]
    [CompilerGenerated]
    public override bool Equals(object obj)
    {
      return obj is ProactiveQueueStatus && this.Equals((ProactiveQueueStatus)obj);
    }

    // Token: 0x06000DB5 RID: 3509 RVA: 0x003830C4 File Offset: 0x003830C4
    [CompilerGenerated]
    public bool Equals(ProactiveQueueStatus other)
    {
      return EqualityComparer<ProactiveQueueStatusKind>.Default.Equals(this.<Kind>k__BackingField, other.<Kind>k__BackingField) && EqualityComparer<bool>.Default.Equals(this.<EarlierIsPaused>k__BackingField, other.<EarlierIsPaused>k__BackingField);
    }

    // Token: 0x06000DB6 RID: 3510 RVA: 0x003830F8 File Offset: 0x003830F8
    [CompilerGenerated]
    public void Deconstruct(out ProactiveQueueStatusKind Kind, out bool EarlierIsPaused)
    {
      Kind = this.Kind;
      EarlierIsPaused = this.EarlierIsPaused;
    }
  }
}
