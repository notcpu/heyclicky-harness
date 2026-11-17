using System;
using System.Runtime.CompilerServices;
using HeyClicky.Agents.Jobs;

namespace HeyClicky.App.Dock
{
  // Token: 0x02000309 RID: 777
  public static class ArcCardStates
  {
    // Token: 0x060021CA RID: 8650 RVA: 0x0061DC90 File Offset: 0x0061DC90
    public static ArcCardState FromJobPhase(ClickyJobRailPhase phase, bool isSeen)
    {
      switch (phase)
      {
      case ClickyJobRailPhase.Queued:
        return ArcCardState.Queued;
      case ClickyJobRailPhase.Working:
        return ArcCardState.Working;
      case ClickyJobRailPhase.NeedsYou:
        return ArcCardState.NeedsYou;
      case ClickyJobRailPhase.Failed:
        return ArcCardState.Failed;
      case ClickyJobRailPhase.Stopped:
        return ArcCardState.Stopped;
      }
      return isSeen ? ArcCardState.Done : ArcCardState.New;
    }

    // Token: 0x060021CB RID: 8651 RVA: 0x0061DCDC File Offset: 0x0061DCDC
    public static ArcCardState FromLatest(ArcLatestKind? kind, bool isUnread)
    {
      if (kind != null)
      {
        switch (kind.GetValueOrDefault())
        {
        case ArcLatestKind.NeedsYou:
          return ArcCardState.NeedsYou;
        case ArcLatestKind.Working:
          return ArcCardState.Working;
        case ArcLatestKind.Finished:
          return isUnread ? ArcCardState.New : ArcCardState.Done;
        case ArcLatestKind.Failed:
          return ArcCardState.Failed;
        }
      }
      return ArcCardState.Idle;
    }

    // Token: 0x060021CC RID: 8652 RVA: 0x0061DD2C File Offset: 0x0061DD2C
    [NullableContext(1)]
    public static string Title(ArcCardState state)
    {
      string text;
      switch (state)
      {
      case ArcCardState.Queued:
        text = "Queued";
        break;
      case ArcCardState.Working:
        text = "Working";
        break;
      case ArcCardState.NeedsYou:
        text = "Needs you";
        break;
      case ArcCardState.Failed:
        text = "Failed";
        break;
      case ArcCardState.Stopped:
        text = "Interrupted";
        break;
      case ArcCardState.New:
        text = "New";
        break;
      case ArcCardState.Done:
        text = "Done";
        break;
      case ArcCardState.Idle:
        text = "Idle";
        break;
      case ArcCardState.Pinning:
        text = "Pinning";
        break;
      default:
        text = "Ready to start";
        break;
      }
      return text;
    }

    // Token: 0x060021CD RID: 8653 RVA: 0x0061DDB4 File Offset: 0x0061DDB4
    [return: TupleElementNames(new string[] { "Rgb", "Opacity" })]
    public static ValueTuple<uint, double> Color(ArcCardState state)
    {
      switch (state)
      {
      case ArcCardState.Queued:
      case ArcCardState.Working:
        return new ValueTuple<uint, double>(1755979U, 1.0);
      case ArcCardState.NeedsYou:
      case ArcCardState.Failed:
        return new ValueTuple<uint, double>(14242877U, 1.0);
      case ArcCardState.New:
      case ArcCardState.Pinning:
      case ArcCardState.Suggested:
        return new ValueTuple<uint, double>(3112182U, 1.0);
      }
      return new ValueTuple<uint, double>(1842724U, 0.5);
    }

    // Token: 0x060021CE RID: 8654 RVA: 0x0061DE48 File Offset: 0x0061DE48
    public static bool IsBusy(ArcCardState state)
    {
      return state <= ArcCardState.Working || state == ArcCardState.Pinning;
    }

    // Token: 0x060021CF RID: 8655 RVA: 0x0061DE64 File Offset: 0x0061DE64
    public static bool IsWorking(ArcCardState state)
    {
      return state <= ArcCardState.Working;
    }

    // Token: 0x060021D0 RID: 8656 RVA: 0x0061DE7C File Offset: 0x0061DE7C
    public static bool IsDone(ArcCardState state)
    {
      return state - ArcCardState.New <= 1;
    }

    // Token: 0x0400104C RID: 4172
    public const uint InkRgb = 1842724U;

    // Token: 0x0400104D RID: 4173
    public const uint CoralRgb = 14242877U;

    // Token: 0x0400104E RID: 4174
    public const uint WorkingRgb = 1755979U;

    // Token: 0x0400104F RID: 4175
    public const uint BlueRgb = 3112182U;
  }
}
