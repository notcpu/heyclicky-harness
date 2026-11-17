using System;
using System.Runtime.CompilerServices;
using HeyClicky.Platform.Input;

namespace HeyClicky.App.Dock
{
  // Token: 0x02000302 RID: 770
  public sealed class DeckScrollGestures
  {
    // Token: 0x17000635 RID: 1589
    // (get) Token: 0x0600219E RID: 8606 RVA: 0x0061D39C File Offset: 0x0061D39C
    // (set) Token: 0x0600219F RID: 8607 RVA: 0x0061D3A4 File Offset: 0x0061D3A4
    public DeckScrollGestures.Kind Last { get; private set; }

    // Token: 0x17000636 RID: 1590
    // (get) Token: 0x060021A0 RID: 8608 RVA: 0x0061D3B0 File Offset: 0x0061D3B0
    public long Touch
    {
      get
      {
        return this.touch;
      }
    }

    // Token: 0x060021A1 RID: 8609 RVA: 0x0061D3B8 File Offset: 0x0061D3B8
    public int Feed(double time, double signedDelta, TouchpadSnapshot pad)
    {
      double num = time - this.lastAt;
      this.lastAt = time;
      DeckScrollGestures.Kind last = this.Last;
      DeckScrollGestures.Kind kind;
      if (pad.Present && pad.Touch > 0L && time >= pad.DownAt - 0.03 && (pad.IsTouching || time <= pad.LiftedAt + 0.03))
      {
        kind = DeckScrollGestures.Kind.Swipe;
      }
      else
      {
        bool flag = pad.Present;
        if (flag)
        {
          bool flag2 = last - DeckScrollGestures.Kind.Swipe <= 1;
          flag = flag2;
        }
        kind = ((flag && num <= 0.22) ? DeckScrollGestures.Kind.Coast : ((!pad.Present && signedDelta % 120.0 != 0.0) ? DeckScrollGestures.Kind.Guess : DeckScrollGestures.Kind.Wheel));
      }
      this.Last = kind;
      if (this.Last != last)
      {
        this.accumulated = 0.0;
      }
      switch (this.Last)
      {
      case DeckScrollGestures.Kind.Swipe:
        if (pad.Touch != this.touch)
        {
          this.touch = pad.Touch;
          this.accumulated = 0.0;
          this.swipeTurned = false;
        }
        if (this.swipeTurned)
        {
          return 0;
        }
        this.accumulated += signedDelta;
        if (Math.Abs(this.accumulated) < 40.0)
        {
          return 0;
        }
        this.swipeTurned = true;
        return this.Step();
      case DeckScrollGestures.Kind.Wheel:
        if (num > 0.22)
        {
          this.accumulated = 0.0;
        }
        this.accumulated += signedDelta;
        if (Math.Abs(this.accumulated) >= 120.0)
        {
          return this.Step();
        }
        return 0;
      case DeckScrollGestures.Kind.Guess:
        return this.guess.Feed(time, signedDelta);
      }
      return 0;
    }

    // Token: 0x060021A2 RID: 8610 RVA: 0x0061D58C File Offset: 0x0061D58C
    private int Step()
    {
      int num = ((this.accumulated > 0.0) ? (-1) : 1);
      this.accumulated = 0.0;
      return num;
    }

    // Token: 0x0400102B RID: 4139
    public const double QuietGap = 0.22;

    // Token: 0x0400102C RID: 4140
    public const double SwipeTurnDistance = 40.0;

    // Token: 0x0400102D RID: 4141
    public const double NotchDistance = 120.0;

    // Token: 0x0400102E RID: 4142
    private const double Slack = 0.03;

    // Token: 0x0400102F RID: 4143
    [Nullable(1)]
    private readonly DeckScrollGestures.GuessedSwipes guess = new DeckScrollGestures.GuessedSwipes();

    // Token: 0x04001030 RID: 4144
    private double lastAt = double.NegativeInfinity;

    // Token: 0x04001031 RID: 4145
    private double accumulated;

    // Token: 0x04001032 RID: 4146
    private long touch = -1L;

    // Token: 0x04001033 RID: 4147
    private bool swipeTurned;

    // Token: 0x02000950 RID: 2384
    public enum Kind
    {
      // Token: 0x04002715 RID: 10005
      None,
      // Token: 0x04002716 RID: 10006
      Swipe,
      // Token: 0x04002717 RID: 10007
      Coast,
      // Token: 0x04002718 RID: 10008
      Wheel,
      // Token: 0x04002719 RID: 10009
      Guess
    }

    // Token: 0x02000951 RID: 2385
    private sealed class GuessedSwipes
    {
      // Token: 0x06003CFB RID: 15611 RVA: 0x0069903C File Offset: 0x0069903C
      public int Feed(double time, double signedDelta)
      {
        double num = time - this.lastAt;
        this.lastAt = time;
        double num2 = Math.Abs(signedDelta) / Math.Clamp(num, 0.004, 0.05);
        this.speed = ((num > 0.22) ? num2 : (this.speed + (num2 - this.speed) * 0.4));
        if (num > 0.22 || (this.turned && (num >= 0.06 || (signedDelta != 0.0 && Math.Sign(signedDelta) != this.turnSign) || (this.trough < 0.35 * this.peak && this.speed > 3.0 * this.trough && this.speed > 1500.0))))
        {
          this.accumulated = 0.0;
          this.turned = false;
        }
        if (this.turned)
        {
          if (this.speed > this.peak)
          {
            this.peak = (this.trough = this.speed);
          }
          else
          {
            this.trough = Math.Min(this.trough, this.speed);
          }
          return 0;
        }
        this.accumulated += signedDelta;
        if (Math.Abs(this.accumulated) < 78.0)
        {
          return 0;
        }
        this.turnSign = Math.Sign(this.accumulated);
        this.turned = true;
        this.accumulated = 0.0;
        this.peak = (this.trough = this.speed);
        if (this.turnSign <= 0)
        {
          return 1;
        }
        return -1;
      }

      // Token: 0x0400271A RID: 10010
      private const double LiftGap = 0.06;

      // Token: 0x0400271B RID: 10011
      private const double TurnDistance = 78.0;

      // Token: 0x0400271C RID: 10012
      private const double SwipeSpeed = 1500.0;

      // Token: 0x0400271D RID: 10013
      private double lastAt = double.NegativeInfinity;

      // Token: 0x0400271E RID: 10014
      private double accumulated;

      // Token: 0x0400271F RID: 10015
      private double speed;

      // Token: 0x04002720 RID: 10016
      private double peak;

      // Token: 0x04002721 RID: 10017
      private double trough;

      // Token: 0x04002722 RID: 10018
      private bool turned;

      // Token: 0x04002723 RID: 10019
      private int turnSign;
    }
  }
}
