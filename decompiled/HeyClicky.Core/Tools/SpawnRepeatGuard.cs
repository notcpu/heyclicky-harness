using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Threading;

namespace HeyClicky.Core.Tools
{
  // Token: 0x020000A2 RID: 162
  [NullableContext(1)]
  [Nullable(0)]
  public static class SpawnRepeatGuard
  {
    // Token: 0x06000A19 RID: 2585 RVA: 0x00373488 File Offset: 0x00373488
    [NullableContext(2)]
    public static string RosterSlug(string requestedSlug, [Nullable(1)] Func<string, bool> isRosterSlug)
    {
      string text = ((requestedSlug != null) ? requestedSlug.Trim() : null);
      if (string.IsNullOrEmpty(text))
      {
        return null;
      }
      if (isRosterSlug(text))
      {
        return text;
      }
      string text2 = new string(text.ToLowerInvariant().Where<char>(delegate(char c)
      {
        if (c >= 'a')
        {
          if (c > 'z')
          {
            goto IL_0021;
          }
        }
        else if (c >= '0')
        {
          if (c > '9')
          {
            goto IL_0021;
          }
        }
        else if (c != '-')
        {
          goto IL_0021;
        }
        return true;
        IL_0021:
        return false;
      }).ToArray<char>()).Trim('-');
      if (text2.Length == 0 || !isRosterSlug(text2))
      {
        return null;
      }
      string text3 = "Tools";
      DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(33, 2);
      defaultInterpolatedStringHandler.AppendLiteral("[spawn] read clicky_slug \"");
      defaultInterpolatedStringHandler.AppendFormatted(text);
      defaultInterpolatedStringHandler.AppendLiteral("\" as \"");
      defaultInterpolatedStringHandler.AppendFormatted(text2);
      defaultInterpolatedStringHandler.AppendLiteral("\"");
      Log.Info(text3, defaultInterpolatedStringHandler.ToStringAndClear());
      return text2;
    }

    // Token: 0x06000A1A RID: 2586 RVA: 0x00373558 File Offset: 0x00373558
    [return: Nullable(2)]
    public static string TryClaim(string task)
    {
      DateTimeOffset now = DateTimeOffset.Now;
      object gate = SpawnRepeatGuard.Gate;
      bool flag = false;
      string text;
      try
      {
        Monitor.Enter(gate, ref flag);
        SpawnRepeatGuard.Claims.RemoveAll((SpawnRepeatGuard.Claim c) => now - c.At > SpawnRepeatGuard.Window);
        HashSet<string> words = SpawnRepeatGuard.SignificantWords(task);
        if (words.Count == 0)
        {
          text = null;
        }
        else
        {
          SpawnRepeatGuard.Claim claim = SpawnRepeatGuard.Claims.FirstOrDefault<SpawnRepeatGuard.Claim>((SpawnRepeatGuard.Claim c) => SpawnRepeatGuard.Overlap(c.Words, words) >= 0.6);
          if (claim != null)
          {
            string text2 = "Tools";
            DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(46, 1);
            defaultInterpolatedStringHandler.AppendLiteral("[spawn] refused a repeat of work started ");
            defaultInterpolatedStringHandler.AppendFormatted<int>((int)(now - claim.At).TotalSeconds);
            defaultInterpolatedStringHandler.AppendLiteral("s ago");
            Log.Info(text2, defaultInterpolatedStringHandler.ToStringAndClear());
            string startedName = claim.StartedName;
            string text3 = ((startedName != null) ? (" (" + startedName + ")") : "");
            JsonObject jsonObject = new JsonObject(null);
            jsonObject["status"] = "already_started";
            jsonObject["message"] = "This same work was already started a moment ago" + text3 + ". Nothing new was started. Don't start it again; tell the user it's underway.";
            text = ToolResults.Json(jsonObject);
          }
          else
          {
            SpawnRepeatGuard.Claims.Add(new SpawnRepeatGuard.Claim(words, now));
            text = null;
          }
        }
      }
      finally
      {
        if (flag)
        {
          Monitor.Exit(gate);
        }
      }
      return text;
    }

    // Token: 0x06000A1B RID: 2587 RVA: 0x003736F8 File Offset: 0x003736F8
    public static void Settle(string task, string resultJson)
    {
      HashSet<string> words = SpawnRepeatGuard.SignificantWords(task);
      object gate = SpawnRepeatGuard.Gate;
      lock (gate)
      {
        int num = SpawnRepeatGuard.Claims.FindLastIndex((SpawnRepeatGuard.Claim c) => c.Words.SetEquals(words));
        if (num >= 0)
        {
          JsonObject jsonObject = null;
          try
          {
            jsonObject = JsonNode.Parse(resultJson, null, default(JsonDocumentOptions)) as JsonObject;
          }
          catch (JsonException)
          {
          }
          string text;
          if (jsonObject == null)
          {
            text = null;
          }
          else
          {
            JsonNode jsonNode = jsonObject["status"];
            text = ((jsonNode != null) ? jsonNode.ToString() : null);
          }
          string text2 = text ?? "";
          bool flag2 = text2 == "error" || text2 == "unavailable" || text2 == "blocked" || text2 == "paywall" || text2 == "already_started";
          if (flag2)
          {
            SpawnRepeatGuard.Claims.RemoveAt(num);
          }
          else
          {
            SpawnRepeatGuard.Claim claim = SpawnRepeatGuard.Claims[num];
            object obj;
            if (jsonObject == null)
            {
              obj = null;
            }
            else
            {
              JsonNode jsonNode2 = jsonObject["clicky_name"];
              obj = ((jsonNode2 != null) ? jsonNode2.ToString() : null);
            }
            object obj2;
            if ((obj2 = obj) == null)
            {
              if (jsonObject == null)
              {
                obj2 = null;
              }
              else
              {
                JsonNode jsonNode3 = jsonObject["job_title"];
                obj2 = ((jsonNode3 != null) ? jsonNode3.ToString() : null);
              }
            }
            claim.StartedName = obj2;
          }
        }
      }
    }

    // Token: 0x06000A1C RID: 2588 RVA: 0x0037388C File Offset: 0x0037388C
    private static HashSet<string> SignificantWords(string text)
    {
      SpawnRepeatGuard.<>c__DisplayClass8_0 CS$<>8__locals1;
      CS$<>8__locals1.words = new HashSet<string>();
      CS$<>8__locals1.current = new StringBuilder();
      foreach (char c in text.ToLowerInvariant())
      {
        if (char.IsLetterOrDigit(c))
        {
          CS$<>8__locals1.current.Append(c);
        }
        else
        {
          SpawnRepeatGuard.<SignificantWords>g__Flush|8_0(ref CS$<>8__locals1);
        }
      }
      SpawnRepeatGuard.<SignificantWords>g__Flush|8_0(ref CS$<>8__locals1);
      return CS$<>8__locals1.words;
    }

    // Token: 0x06000A1D RID: 2589 RVA: 0x003738FC File Offset: 0x003738FC
    private static double Overlap(HashSet<string> first, HashSet<string> second)
    {
      int num = first.Union<string>(second).Count<string>();
      if (num != 0)
      {
        return (double)first.Intersect<string>(second).Count<string>() / (double)num;
      }
      return 0.0;
    }

    // Token: 0x06000A1F RID: 2591 RVA: 0x00373958 File Offset: 0x00373958
    [CompilerGenerated]
    internal static void <SignificantWords>g__Flush|8_0(ref SpawnRepeatGuard.<>c__DisplayClass8_0 A_0)
    {
      if (A_0.current.Length >= 3)
      {
        A_0.words.Add(A_0.current.ToString());
      }
      A_0.current.Clear();
    }

    // Token: 0x04000495 RID: 1173
    private static readonly object Gate = new object();

    // Token: 0x04000496 RID: 1174
    private static readonly List<SpawnRepeatGuard.Claim> Claims = new List<SpawnRepeatGuard.Claim>();

    // Token: 0x04000497 RID: 1175
    private static readonly TimeSpan Window = TimeSpan.FromSeconds(30L);

    // Token: 0x04000498 RID: 1176
    private const double SameBriefOverlap = 0.6;

    // Token: 0x020003AA RID: 938
    [Nullable(0)]
    private sealed class Claim
    {
      // Token: 0x060029BC RID: 10684 RVA: 0x00423C7C File Offset: 0x00423C7C
      public Claim(HashSet<string> words, DateTimeOffset at)
      {
      }

      // Token: 0x17000806 RID: 2054
      // (get) Token: 0x060029BD RID: 10685 RVA: 0x00423C94 File Offset: 0x00423C94
      public HashSet<string> Words { get; } = words;

      // Token: 0x17000807 RID: 2055
      // (get) Token: 0x060029BE RID: 10686 RVA: 0x00423C9C File Offset: 0x00423C9C
      public DateTimeOffset At { get; } = at;

      // Token: 0x17000808 RID: 2056
      // (get) Token: 0x060029BF RID: 10687 RVA: 0x00423CA4 File Offset: 0x00423CA4
      // (set) Token: 0x060029C0 RID: 10688 RVA: 0x00423CAC File Offset: 0x00423CAC
      [Nullable(2)]
      public string StartedName
      {
        [NullableContext(2)]
        get;
        [NullableContext(2)]
        set;
      }
    }
  }
}
