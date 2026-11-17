using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using HeyClicky.Core.Tools;

namespace HeyClicky.Core.Drive
{
  // Token: 0x02000195 RID: 405
  [NullableContext(1)]
  [Nullable(0)]
  public static class ClickyDriveLines
  {
    // Token: 0x060017F8 RID: 6136 RVA: 0x003C6C38 File Offset: 0x003C6C38
    public static string DriveFact(string goal, [Nullable(2)] string clickyName)
    {
      string text = ((clickyName == null) ? "" : (" \"" + clickyName + "\", a job working in the background, asked for this one step on their PC; do only this step, then end the take-over so it can carry on. In end_screen_takeover's summary say what you made and where it is (the full path of any file you saved), so the job can pick it up."));
      DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(358, 2);
      defaultInterpolatedStringHandler.AppendLiteral("[Take-over] They said yes: you are driving their screen now, toward: ");
      defaultInterpolatedStringHandler.AppendFormatted(goal);
      defaultInterpolatedStringHandler.AppendLiteral(".");
      defaultInterpolatedStringHandler.AppendFormatted(text);
      defaultInterpolatedStringHandler.AppendLiteral(" Use the computer tool, one short spoken sentence before each group of actions; the first one also asks them to keep their hands off the mouse while you work (pressing the escape key stops you; say it as \"the escape key\", never \"esc\"). When it is done, say so in one line and stop acting.");
      return defaultInterpolatedStringHandler.ToStringAndClear();
    }

    // Token: 0x060017F9 RID: 6137 RVA: 0x003C6CA8 File Offset: 0x003C6CA8
    public static string SafetyStop(string message)
    {
      return "I stopped there: " + ((message.Length > 140) ? message.Substring(0, 140) : message) + ". It's yours.";
    }

    // Token: 0x060017FA RID: 6138 RVA: 0x003C6CD8 File Offset: 0x003C6CD8
    public static string ThenFact([Nullable(2)] string summary, string then)
    {
      string text = (summary ?? "").Trim();
      string text2 = ((text.Length == 0) ? "" : (" What you said at the end: \"" + text + "\"."));
      DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(243, 2);
      defaultInterpolatedStringHandler.AppendLiteral("[App] The screen step is done.");
      defaultInterpolatedStringHandler.AppendFormatted(text2);
      defaultInterpolatedStringHandler.AppendLiteral(" The rest of what they asked: ");
      defaultInterpolatedStringHandler.AppendFormatted(then);
      defaultInterpolatedStringHandler.AppendLiteral(". Start it now as background work (sessions_spawn) with what the drive produced (the file's name and where it was saved, the names, the link), in one short line, without asking again.");
      return defaultInterpolatedStringHandler.ToStringAndClear();
    }

    // Token: 0x060017FB RID: 6139 RVA: 0x003C6D60 File Offset: 0x003C6D60
    public static string ActionsNote(IReadOnlyList<string> actions)
    {
      return "[App] While driving their screen you did: " + string.Join("; ", actions) + ".";
    }

    // Token: 0x060017FC RID: 6140 RVA: 0x003C6D7C File Offset: 0x003C6D7C
    public static string SummaryFirstSentence(string summary)
    {
      string text = summary.TrimStart('.');
      string text2 = ((text.Length == 0) ? summary : (text.Split('.', 2, StringSplitOptions.None)[0] + "."));
      if (text2.Length <= 160)
      {
        return text2;
      }
      return text2.Substring(0, 160);
    }

    // Token: 0x060017FD RID: 6141 RVA: 0x003C6DD0 File Offset: 0x003C6DD0
    public static string Brief(string goal)
    {
      return ClickyDriveLines.Sentence(ClickyNotes.SpeakableSentence(goal, 110));
    }

    // Token: 0x060017FE RID: 6142 RVA: 0x003C6DE0 File Offset: 0x003C6DE0
    public static string AsksToUsePc([Nullable(2)] string clickyName, string request)
    {
      string text = ClickyNotes.SpeakableSentence(request, 110);
      string text3;
      if (text.Length != 0)
      {
        char c = char.ToLowerInvariant(text[0]);
        ReadOnlySpan<char> readOnlySpan = new ReadOnlySpan<char>(ref c);
        string text2 = text;
        text3 = readOnlySpan + text2.Substring(1, text2.Length - 1);
      }
      else
      {
        text3 = text;
      }
      string text4 = text3;
      return (string.IsNullOrWhiteSpace(clickyName) ? "Your job" : clickyName.Trim()) + " needs one step on your PC: " + text4 + " Want me to do it?";
    }

    // Token: 0x060017FF RID: 6143 RVA: 0x003C6E58 File Offset: 0x003C6E58
    public static string Sentence(string goal)
    {
      string text = goal.Trim();
      if (text.Length == 0)
      {
        return text;
      }
      char c = char.ToUpperInvariant(text[0]);
      ReadOnlySpan<char> readOnlySpan = new ReadOnlySpan<char>(ref c);
      string text2 = text;
      string text3 = readOnlySpan + text2.Substring(1, text2.Length - 1);
      string text4 = text3;
      c = text4[text4.Length - 1];
      if (c != '!' && c != '.' && c != '?')
      {
        return text3 + ".";
      }
      return text3;
    }

    // Token: 0x04000A4C RID: 2636
    public const string FixedOfferLine = "Want me to just do it? I'll drive, you watch.";

    // Token: 0x04000A4D RID: 2637
    public const string HandedBack = "Okay, it's all yours.";

    // Token: 0x04000A4E RID: 2638
    public const string CouldNotSee = "I can't see the screen right now.";

    // Token: 0x04000A4F RID: 2639
    public const string LostConnection = "I lost the connection partway, so I stopped there.";

    // Token: 0x04000A50 RID: 2640
    public const string OutOfAgentMessages = "You're out of agent messages for now, so I stopped here.";

    // Token: 0x04000A51 RID: 2641
    public const string Declined = "[App] The user chose to do it themselves (they declined the take-over). Keep guiding them.";

    // Token: 0x04000A52 RID: 2642
    public const string StartingStatus = "Getting started";

    // Token: 0x04000A53 RID: 2643
    public const int ShortRequestCharacters = 110;
  }
}
