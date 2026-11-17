using System;
using System.Runtime.CompilerServices;

namespace HeyClicky.Core.Proactive
{
  // Token: 0x020000BF RID: 191
  [NullableContext(1)]
  [Nullable(0)]
  public static class MorningGreetingPolicy
  {
    // Token: 0x06000CA2 RID: 3234 RVA: 0x0037F668 File Offset: 0x0037F668
    public static bool IsWithinWindow(DateTime local)
    {
      return local.Hour >= 7 && local.Hour < 12;
    }

    // Token: 0x06000CA3 RID: 3235 RVA: 0x0037F684 File Offset: 0x0037F684
    public static string DayKey(DateTime local)
    {
      DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(2, 3);
      defaultInterpolatedStringHandler.AppendFormatted<int>(local.Year);
      defaultInterpolatedStringHandler.AppendLiteral("-");
      defaultInterpolatedStringHandler.AppendFormatted<int>(local.Month);
      defaultInterpolatedStringHandler.AppendLiteral("-");
      defaultInterpolatedStringHandler.AppendFormatted<int>(local.Day);
      return defaultInterpolatedStringHandler.ToStringAndClear();
    }

    // Token: 0x06000CA4 RID: 3236 RVA: 0x0037F6E4 File Offset: 0x0037F6E4
    public static string Pick(Random random)
    {
      return MorningGreetingPolicy.Kaomoji[random.Next(MorningGreetingPolicy.Kaomoji.Length)] + " " + MorningGreetingPolicy.Messages[random.Next(MorningGreetingPolicy.Messages.Length)];
    }

    // Token: 0x06000CA5 RID: 3237 RVA: 0x0037F718 File Offset: 0x0037F718
    public static TimeSpan UntilNextCheck(DateTime nowLocal, bool greetedToday)
    {
      if (!greetedToday && MorningGreetingPolicy.IsWithinWindow(nowLocal))
      {
        return MorningGreetingPolicy.PollInterval;
      }
      DateTime dateTime = nowLocal.Date.AddHours(7.0);
      TimeSpan timeSpan = ((nowLocal < dateTime) ? dateTime : dateTime.AddDays(1.0)) - nowLocal;
      if (!(timeSpan < MorningGreetingPolicy.PollInterval))
      {
        return timeSpan;
      }
      return MorningGreetingPolicy.PollInterval;
    }

    // Token: 0x04000561 RID: 1377
    public const int WindowStartHour = 7;

    // Token: 0x04000562 RID: 1378
    public const int WindowEndHour = 12;

    // Token: 0x04000563 RID: 1379
    public static readonly TimeSpan PollInterval = TimeSpan.FromSeconds(15L);

    // Token: 0x04000564 RID: 1380
    public static readonly TimeSpan ArmDelay = TimeSpan.FromSeconds(10L);

    // Token: 0x04000565 RID: 1381
    public static readonly TimeSpan ActiveRecency = TimeSpan.FromSeconds(30L);

    // Token: 0x04000566 RID: 1382
    public static readonly TimeSpan PostStreamHold = TimeSpan.FromSeconds(5L);

    // Token: 0x04000567 RID: 1383
    public static readonly string[] Kaomoji = new string[]
    {
      "＼(^_^ )", "( ^_^)／", "(^o^)", "(^\u203f^)", "(・∀・)", "(\uffe3▽\uffe3)", "( \u02d8▽\u02d8)", "(=^･^=)", "(^_^)v", "＼(^o^)／",
      "( \u00b4▽`)", "(⌒\u203f⌒)", "(\uff3e▽\uff3e)", "(・ω・)ノ", "(^-^)ノ", "( \u02d9▿\u02d9 )", "☀(^_^)☀"
    };

    // Token: 0x04000568 RID: 1384
    public static readonly string[] Messages = new string[]
    {
      "good morning beautiful. today's gonna be a good one.", "morning! drink a glass of water before anything else.", "good morning. hope you slept like a rock.", "yo good morning. glad you're here.", "morning! no rush today, we ease into it.", "morning. one thing at a time today. we got this.", "good morning. i'm around if you need me.", "morning! go grab some coffee, i'll be here.", "morning. fresh day, clean slate.", "morning! breakfast first, greatness second.",
      "yo, you made it. the day officially started.", "morning! the sun's up and so are we.", "hey, there you are. missed you.", "good morning! today's to-do list doesn't stand a chance.", "yo, morning. happy to see you.", "morning! whatever today brings, we got it.", "good morning. it's a nice day to make something cool.", "morning! small wins today. they add up."
    };
  }
}
