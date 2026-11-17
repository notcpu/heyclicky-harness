using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text.Json.Nodes;

namespace HeyClicky.Core.Tools
{
  // Token: 0x0200009D RID: 157
  [NullableContext(1)]
  [Nullable(0)]
  public static class SessionsSendContext
  {
    // Token: 0x060009F7 RID: 2551 RVA: 0x00372340 File Offset: 0x00372340
    public static string UsersOwnWordsLaunchContext(string usersOwnWords, string brainNote, bool typed)
    {
      string text = "HeyClicky passed the user's message to you exactly as they " + (typed ? "typed" : "said") + " it.";
      if (!SessionsSendContext.BrainNoteAddsToWords(brainNote, usersOwnWords))
      {
        return text;
      }
      return text + " HeyClicky's note on what it refers to and what was agreed with them in the conversation (their words win where the two differ): " + brainNote.Trim();
    }

    // Token: 0x060009F8 RID: 2552 RVA: 0x00372388 File Offset: 0x00372388
    public static bool BrainNoteAddsToWords(string brainNote, string usersOwnWords)
    {
      string text = SessionsSendContext.<BrainNoteAddsToWords>g__Normalized|1_0(brainNote);
      return text.Length > 0 && text != SessionsSendContext.<BrainNoteAddsToWords>g__Normalized|1_0(usersOwnWords);
    }

    // Token: 0x060009F9 RID: 2553 RVA: 0x003723B4 File Offset: 0x003723B4
    [NullableContext(2)]
    public static bool IsHeyClickysOwnInstruction(string from, string message)
    {
      return string.Equals((from != null) ? from.Trim() : null, "heyclicky", StringComparison.OrdinalIgnoreCase) && !string.IsNullOrWhiteSpace(message);
    }

    // Token: 0x060009FA RID: 2554 RVA: 0x003723DC File Offset: 0x003723DC
    public static string HeyClickyInstructionLaunchContext(string usersOwnWords, bool typed)
    {
      return "This message is HeyClicky's own instruction to you (sessions_send), not the user's words. For background only, what the user " + (typed ? "typed" : "said") + " to HeyClicky this turn (said to HeyClicky, not to you, so don't stop your work to answer it): " + usersOwnWords;
    }

    // Token: 0x060009FB RID: 2555 RVA: 0x00372400 File Offset: 0x00372400
    public static IReadOnlyList<string> BuildsOnJobIds([Nullable(2)] JsonObject arguments)
    {
      List<string> list = new List<string>();
      JsonNode jsonNode = ((arguments != null) ? arguments["builds_on"] : null);
      JsonArray jsonArray = jsonNode as JsonArray;
      JsonValue jsonValue;
      if (jsonArray == null)
      {
        jsonValue = jsonNode as JsonValue;
        if (jsonValue == null)
        {
          goto IL_0088;
        }
      }
      else
      {
        using (IEnumerator<JsonNode> enumerator = jsonArray.GetEnumerator())
        {
          while (enumerator.MoveNext())
          {
            JsonNode jsonNode2 = enumerator.Current;
            JsonValue jsonValue2 = jsonNode2 as JsonValue;
            string text;
            if (jsonValue2 != null && jsonValue2.TryGetValue<string>(out text))
            {
              list.Add(text);
            }
          }
          goto IL_0088;
        }
      }
      string text2;
      if (jsonValue.TryGetValue<string>(out text2))
      {
        list.Add(text2);
      }
      IL_0088:
      return (from id in list
        select id.Trim() into id
        where id.Length > 0
        select id).Distinct<string>().Take<string>(3).ToList<string>();
    }

    // Token: 0x060009FC RID: 2556 RVA: 0x00372500 File Offset: 0x00372500
    [CompilerGenerated]
    internal static string <BrainNoteAddsToWords>g__Normalized|1_0(string text)
    {
      IEnumerable<char> enumerable = text.ToLowerInvariant();
      Func<char, bool> func;
      if ((func = SessionsSendContext.<>O.<0>__IsLetterOrDigit) == null)
      {
        func = (SessionsSendContext.<>O.<0>__IsLetterOrDigit = new Func<char, bool>(char.IsLetterOrDigit));
      }
      return new string(enumerable.Where<char>(func).ToArray<char>());
    }

    // Token: 0x0400047D RID: 1149
    public const string AppTurnLaunchContext = "HeyClicky is passing the user's message on to this work (sessions_send).";

    // Token: 0x0200039C RID: 924
    [CompilerGenerated]
    private static class <>O
    {
      // Token: 0x040018DC RID: 6364
      [Nullable(0)]
      public static Func<char, bool> <0>__IsLetterOrDigit;
    }
  }
}
