using System;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;

namespace HeyClicky.App.Dock
{
  // Token: 0x0200030A RID: 778
  [NullableContext(1)]
  [Nullable(0)]
  public static class ArcCover
  {
    // Token: 0x060021D1 RID: 8657 RVA: 0x0061DE98 File Offset: 0x0061DE98
    public static string Title(ArcCardState state)
    {
      switch (state)
      {
      case ArcCardState.Failed:
        return "Didn't finish";
      case ArcCardState.New:
        return "Ready to view";
      case ArcCardState.Idle:
        return "No work yet";
      }
      return ArcCardStates.Title(state);
    }

    // Token: 0x060021D2 RID: 8658 RVA: 0x0061DEE4 File Offset: 0x0061DEE4
    [NullableContext(2)]
    public static string Symbol(ArcCardState state)
    {
      switch (state)
      {
      case ArcCardState.NeedsYou:
        return "hand.raised.fill";
      case ArcCardState.Failed:
        return "exclamationmark.triangle.fill";
      case ArcCardState.Stopped:
        return "stop.circle.fill";
      case ArcCardState.New:
      case ArcCardState.Suggested:
        return "sparkles";
      case ArcCardState.Done:
        return "checkmark.circle.fill";
      case ArcCardState.Pinning:
        return "pin.fill";
      }
      return null;
    }

    // Token: 0x060021D3 RID: 8659 RVA: 0x0061DF50 File Offset: 0x0061DF50
    public static bool IsQuiet(ArcCardState state)
    {
      return state - ArcCardState.Done <= 1;
    }

    // Token: 0x060021D4 RID: 8660 RVA: 0x0061DF6C File Offset: 0x0061DF6C
    [return: TupleElementNames(new string[] { "State", "Title" })]
    [return: Nullable(new byte[] { 1, 0 })]
    public static ValueTuple<double, double>[] LevelsFor(ArcCardState state)
    {
      if (state != ArcCardState.Suggested)
      {
        return ArcCover.Levels;
      }
      return ArcCover.SuggestedLevels;
    }

    // Token: 0x060021D5 RID: 8661 RVA: 0x0061DF80 File Offset: 0x0061DF80
    public static string CardPitch(string pitch, int maximumLength = 200)
    {
      string text = pitch.Trim();
      if (ArcCover.CharacterCount(text) <= maximumLength)
      {
        return text;
      }
      string text2 = "";
      foreach (string text3 in from s in SentenceBreaks.Sentences(text)
        select s.Trim())
      {
        if (text3.Length != 0)
        {
          string text4 = ((text2.Length == 0) ? text3 : (text2 + " " + text3));
          if (text2.Length > 0 && ArcCover.CharacterCount(text4) > maximumLength)
          {
            break;
          }
          text2 = text4;
        }
      }
      if (text2.Length != 0)
      {
        return text2;
      }
      return text;
    }

    // Token: 0x060021D6 RID: 8662 RVA: 0x0061E048 File Offset: 0x0061E048
    private static int CharacterCount(string text)
    {
      return new StringInfo(text).LengthInTextElements;
    }

    // Token: 0x04001050 RID: 4176
    public const string FinalizingTitle = "Finalizing";

    // Token: 0x04001051 RID: 4177
    public const string WrappingUpLine = "Finalizing…";

    // Token: 0x04001052 RID: 4178
    [TupleElementNames(new string[] { "State", "Title" })]
    [Nullable(new byte[] { 1, 0 })]
    public static readonly ValueTuple<double, double>[] Levels = new ValueTuple<double, double>[]
    {
      new ValueTuple<double, double>(50.0, 38.0),
      new ValueTuple<double, double>(48.0, 35.0),
      new ValueTuple<double, double>(46.0, 32.0),
      new ValueTuple<double, double>(44.0, 29.0),
      new ValueTuple<double, double>(40.0, 27.0),
      new ValueTuple<double, double>(38.0, 25.0),
      new ValueTuple<double, double>(36.0, 23.0),
      new ValueTuple<double, double>(34.0, 21.0)
    };

    // Token: 0x04001053 RID: 4179
    [TupleElementNames(new string[] { "State", "Title" })]
    [Nullable(new byte[] { 1, 0 })]
    public static readonly ValueTuple<double, double>[] SuggestedLevels = new ValueTuple<double, double>[]
    {
      new ValueTuple<double, double>(30.0, 30.0),
      new ValueTuple<double, double>(28.0, 28.0),
      new ValueTuple<double, double>(26.0, 26.0),
      new ValueTuple<double, double>(24.0, 24.0),
      new ValueTuple<double, double>(22.0, 22.0),
      new ValueTuple<double, double>(20.0, 20.0)
    };

    // Token: 0x04001054 RID: 4180
    [TupleElementNames(new string[] { "Title", "Body", "Spacing" })]
    [Nullable(new byte[] { 1, 0 })]
    public static readonly ValueTuple<double, double, double>[] Suggested = new ValueTuple<double, double, double>[]
    {
      new ValueTuple<double, double, double>(30.0, 20.0, 14.0),
      new ValueTuple<double, double, double>(28.0, 19.0, 12.0),
      new ValueTuple<double, double, double>(26.0, 18.0, 11.0),
      new ValueTuple<double, double, double>(24.0, 17.0, 10.0),
      new ValueTuple<double, double, double>(22.0, 16.0, 9.0),
      new ValueTuple<double, double, double>(20.0, 15.0, 8.0),
      new ValueTuple<double, double, double>(19.0, 14.0, 7.0)
    };

    // Token: 0x04001055 RID: 4181
    public const int CardPitchMaximumLines = 5;
  }
}
