using System;
using System.Globalization;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text.Json.Nodes;
using System.Threading.Tasks;
using HeyClicky.Core.Configuration;

namespace HeyClicky.Core.Dictation
{
  // Token: 0x020001CE RID: 462
  [NullableContext(1)]
  [Nullable(0)]
  public static class DictationTranscriptArchive
  {
    // Token: 0x170005E4 RID: 1508
    // (get) Token: 0x06001AAA RID: 6826 RVA: 0x003D8828 File Offset: 0x003D8828
    public static string Directory
    {
      get
      {
        return Path.Combine(AppBundleConfiguration.AppDataDirectory, "DictationTranscripts");
      }
    }

    // Token: 0x06001AAB RID: 6827 RVA: 0x003D883C File Offset: 0x003D883C
    public static void DeleteAll()
    {
      object gate = DictationTranscriptArchive.Gate;
      lock (gate)
      {
        try
        {
          if (global::System.IO.Directory.Exists(DictationTranscriptArchive.Directory))
          {
            global::System.IO.Directory.Delete(DictationTranscriptArchive.Directory, true);
          }
        }
        catch (Exception ex)
        {
          Log.Warn("Dictation", "transcript archive delete: " + ex.Message);
        }
      }
    }

    // Token: 0x06001AAC RID: 6828 RVA: 0x003D88B8 File Offset: 0x003D88B8
    public static void AppendFinishedTranscript(string transcript, [Nullable(2)] string targetAppName)
    {
      string text = transcript.Trim();
      if (text.Length == 0)
      {
        return;
      }
      DateTimeOffset at = DateTimeOffset.Now;
      Task.Run(delegate
      {
        try
        {
          object gate = DictationTranscriptArchive.Gate;
          lock (gate)
          {
            global::System.IO.Directory.CreateDirectory(DictationTranscriptArchive.Directory);
            JsonObject jsonObject = new JsonObject(null);
            jsonObject["at"] = at.ToString("O");
            jsonObject["text"] = text;
            JsonObject jsonObject2 = jsonObject;
            if (!string.IsNullOrEmpty(targetAppName))
            {
              jsonObject2["app"] = targetAppName;
            }
            string directory = DictationTranscriptArchive.Directory;
            DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(6, 1);
            defaultInterpolatedStringHandler.AppendFormatted<DateTimeOffset>(at, "yyyy-MM-dd");
            defaultInterpolatedStringHandler.AppendLiteral(".jsonl");
            File.AppendAllText(Path.Combine(directory, defaultInterpolatedStringHandler.ToStringAndClear()), jsonObject2.ToJsonString(null) + "\n");
            foreach (string text2 in global::System.IO.Directory.EnumerateFiles(DictationTranscriptArchive.Directory, "*.jsonl"))
            {
              DateTime dateTime;
              if (DateTime.TryParseExact(Path.GetFileNameWithoutExtension(text2), "yyyy-MM-dd", null, DateTimeStyles.None, out dateTime) && dateTime < at.Date.AddDays(-14.0))
              {
                File.Delete(text2);
              }
            }
          }
        }
        catch (Exception ex)
        {
          Log.Warn("Dictation", "transcript archive: " + ex.Message);
        }
      });
    }

    // Token: 0x04000B74 RID: 2932
    private const int RetentionDays = 14;

    // Token: 0x04000B75 RID: 2933
    private static readonly object Gate = new object();
  }
}
