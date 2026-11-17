using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text.Json;

namespace HeyClicky.Core.Ink
{
  // Token: 0x02000127 RID: 295
  [NullableContext(1)]
  [Nullable(0)]
  public sealed class ClickyInkActionsStore
  {
    // Token: 0x17000481 RID: 1153
    // (get) Token: 0x06001349 RID: 4937 RVA: 0x003A6344 File Offset: 0x003A6344
    public static ClickyInkActionsStore Shared { get; } = new ClickyInkActionsStore(null, () => ClickyInkNoteStore.Shared.ScopeKey);

    // Token: 0x0600134A RID: 4938 RVA: 0x003A634C File Offset: 0x003A634C
    [NullableContext(2)]
    public ClickyInkActionsStore(string baseDirectory, [Nullable(new byte[] { 1, 2 })] Func<string> scopeKey)
    {
      this.baseDirectoryOverride = baseDirectory;
      this.currentScopeKey = scopeKey;
    }

    // Token: 0x0600134B RID: 4939 RVA: 0x003A6378 File Offset: 0x003A6378
    public IReadOnlyList<ClickyInkSentAction> Sent(string thingId)
    {
      if (!this.LoadScopeIfNeeded())
      {
        return Array.Empty<ClickyInkSentAction>();
      }
      List<ClickyInkSentAction> list;
      if (!this.actionsByThingId.TryGetValue(thingId, out list))
      {
        return new List<ClickyInkSentAction>();
      }
      return list;
    }

    // Token: 0x0600134C RID: 4940 RVA: 0x003A63AC File Offset: 0x003A63AC
    public void Record(ClickyInkSentAction action, string thingId)
    {
      if (!this.LoadScopeIfNeeded())
      {
        return;
      }
      List<ClickyInkSentAction> list2;
      List<ClickyInkSentAction> list = (this.actionsByThingId.TryGetValue(thingId, out list2) ? list2.ToList<ClickyInkSentAction>() : new List<ClickyInkSentAction>());
      list.Add(action);
      this.actionsByThingId[thingId] = list.Skip<ClickyInkSentAction>(Math.Max(0, list.Count - 24)).ToList<ClickyInkSentAction>();
      if (this.actionsByThingId.Count > 400)
      {
        foreach (string text in (from kv in this.actionsByThingId.OrderBy<KeyValuePair<string, List<ClickyInkSentAction>>, DateTimeOffset>(delegate(KeyValuePair<string, List<ClickyInkSentAction>> kv)
          {
            if (kv.Value.Count <= 0)
            {
              return DateTimeOffset.MinValue;
            }
            List<ClickyInkSentAction> value = kv.Value;
            return value[value.Count - 1].SentAt;
          }).Take<KeyValuePair<string, List<ClickyInkSentAction>>>(this.actionsByThingId.Count - 400)
          select kv.Key).ToList<string>())
        {
          this.actionsByThingId.Remove(text);
        }
      }
      this.Save();
    }

    // Token: 0x0600134D RID: 4941 RVA: 0x003A64D8 File Offset: 0x003A64D8
    public void RemoveAllForSignOut()
    {
      this.loadedScopeKey = null;
      this.actionsByThingId = new Dictionary<string, List<ClickyInkSentAction>>();
    }

    // Token: 0x0600134E RID: 4942 RVA: 0x003A64EC File Offset: 0x003A64EC
    public void WaitForPendingSaves()
    {
      this.writeQueue.Sync();
    }

    // Token: 0x17000482 RID: 1154
    // (get) Token: 0x0600134F RID: 4943 RVA: 0x003A64FC File Offset: 0x003A64FC
    private string BaseDirectory
    {
      get
      {
        string text;
        if ((text = this.baseDirectoryOverride) == null)
        {
          text = AppPaths.Combine(new string[] { "Ink" });
        }
        return text;
      }
    }

    // Token: 0x06001350 RID: 4944 RVA: 0x003A651C File Offset: 0x003A651C
    private string FilePath(string scopeKey)
    {
      return Path.Combine(this.BaseDirectory, scopeKey, "note-actions.json");
    }

    // Token: 0x06001351 RID: 4945 RVA: 0x003A6530 File Offset: 0x003A6530
    private bool LoadScopeIfNeeded()
    {
      string text = this.currentScopeKey();
      if (text == null)
      {
        this.loadedScopeKey = null;
        this.actionsByThingId = new Dictionary<string, List<ClickyInkSentAction>>();
        return false;
      }
      if (text == this.loadedScopeKey)
      {
        return true;
      }
      this.loadedScopeKey = text;
      this.actionsByThingId = new Dictionary<string, List<ClickyInkSentAction>>();
      try
      {
        string text2 = this.FilePath(text);
        if (File.Exists(text2))
        {
          InkActionsSnapshot inkActionsSnapshot = JsonSerializer.Deserialize<InkActionsSnapshot>(File.ReadAllBytes(text2), InkJsonContext.Default.InkActionsSnapshot);
          Dictionary<string, List<InkSentActionFile>> dictionary = ((inkActionsSnapshot != null) ? inkActionsSnapshot.ActionsByThingId : null);
          if (dictionary != null)
          {
            this.actionsByThingId = dictionary.ToDictionary<KeyValuePair<string, List<InkSentActionFile>>, string, List<ClickyInkSentAction>>((KeyValuePair<string, List<InkSentActionFile>> kv) => kv.Key, (KeyValuePair<string, List<InkSentActionFile>> kv) => kv.Value.Select<InkSentActionFile, ClickyInkSentAction>((InkSentActionFile a) => a.ToAction()).ToList<ClickyInkSentAction>());
          }
        }
      }
      catch (Exception)
      {
        this.actionsByThingId = new Dictionary<string, List<ClickyInkSentAction>>();
      }
      return true;
    }

    // Token: 0x06001352 RID: 4946 RVA: 0x003A6628 File Offset: 0x003A6628
    private void Save()
    {
      string text = this.loadedScopeKey;
      if (text == null)
      {
        return;
      }
      string path = this.FilePath(text);
      InkActionsSnapshot inkActionsSnapshot = new InkActionsSnapshot();
      inkActionsSnapshot.Version = 1;
      inkActionsSnapshot.ActionsByThingId = this.actionsByThingId.ToDictionary<KeyValuePair<string, List<ClickyInkSentAction>>, string, List<InkSentActionFile>>((KeyValuePair<string, List<ClickyInkSentAction>> kv) => kv.Key, delegate(KeyValuePair<string, List<ClickyInkSentAction>> kv)
      {
        IEnumerable<ClickyInkSentAction> value = kv.Value;
        Func<ClickyInkSentAction, InkSentActionFile> func;
        if ((func = ClickyInkActionsStore.<>O.<0>__From) == null)
        {
          func = (ClickyInkActionsStore.<>O.<0>__From = new Func<ClickyInkSentAction, InkSentActionFile>(InkSentActionFile.From));
        }
        return value.Select<ClickyInkSentAction, InkSentActionFile>(func).ToList<InkSentActionFile>();
      });
      InkActionsSnapshot inkActionsSnapshot2 = inkActionsSnapshot;
      byte[] bytes = JsonSerializer.SerializeToUtf8Bytes<InkActionsSnapshot>(inkActionsSnapshot2, InkJsonContext.Default.InkActionsSnapshot);
      this.writeQueue.Async(delegate
      {
        try
        {
          InkFiles.WriteBytesAtomically(path, bytes);
        }
        catch (Exception ex)
        {
          Log.Warn("ink", "couldn't save the notes' actions: " + ex.Message);
        }
      });
    }

    // Token: 0x0400089A RID: 2202
    public const string FileName = "note-actions.json";

    // Token: 0x0400089B RID: 2203
    public const int MaximumActionsPerThing = 24;

    // Token: 0x0400089C RID: 2204
    public const int MaximumThings = 400;

    // Token: 0x0400089D RID: 2205
    [Nullable(2)]
    private readonly string baseDirectoryOverride;

    // Token: 0x0400089E RID: 2206
    [Nullable(new byte[] { 1, 2 })]
    private readonly Func<string> currentScopeKey;

    // Token: 0x0400089F RID: 2207
    [Nullable(2)]
    private string loadedScopeKey;

    // Token: 0x040008A0 RID: 2208
    private Dictionary<string, List<ClickyInkSentAction>> actionsByThingId = new Dictionary<string, List<ClickyInkSentAction>>();

    // Token: 0x040008A1 RID: 2209
    private readonly InkDispatch.SerialQueue writeQueue = new InkDispatch.SerialQueue();

    // Token: 0x0200049D RID: 1181
    [CompilerGenerated]
    private static class <>O
    {
      // Token: 0x04001D70 RID: 7536
      [Nullable(0)]
      public static Func<ClickyInkSentAction, InkSentActionFile> <0>__From;
    }
  }
}
