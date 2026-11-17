using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text.Json.Nodes;
using HeyClicky.App.UI;
using HeyClicky.Core.Ink;
using HeyClicky.Core.Telemetry;

namespace HeyClicky.App.Ink
{
  // Token: 0x020001A4 RID: 420
  [NullableContext(1)]
  [Nullable(0)]
  public static class ClickyInkNoteBridge
  {
    // Token: 0x17000405 RID: 1029
    // (get) Token: 0x060016AC RID: 5804 RVA: 0x005BB3E4 File Offset: 0x005BB3E4
    // (set) Token: 0x060016AD RID: 5805 RVA: 0x005BB3EC File Offset: 0x005BB3EC
    [Nullable(2)]
    public static IClickyInkNoteHost Host
    {
      [NullableContext(2)]
      get;
      [NullableContext(2)]
      set;
    }

    // Token: 0x060016AE RID: 5806 RVA: 0x005BB3F4 File Offset: 0x005BB3F4
    public static ClickyInkCardSubject SubjectForClicky(string slug)
    {
      IClickyInkNoteHost host = ClickyInkNoteBridge.Host;
      ClickyInkClickyLive clickyInkClickyLive;
      if ((clickyInkClickyLive = ((host != null) ? host.ClickyLiveStatus(slug) : null)) == null)
      {
        clickyInkClickyLive = new ClickyInkClickyLive(ClickyInkClickyPhase.Idle, null);
      }
      ClickyInkClickyLive clickyInkClickyLive2 = clickyInkClickyLive;
      ClickyInkCardSubject.SubjectPhase subjectPhase;
      switch (clickyInkClickyLive2.Phase)
      {
      case ClickyInkClickyPhase.Working:
        subjectPhase = ClickyInkCardSubject.SubjectPhase.Working;
        break;
      case ClickyInkClickyPhase.NeedsYou:
        subjectPhase = ClickyInkCardSubject.SubjectPhase.NeedsYou;
        break;
      case ClickyInkClickyPhase.Failed:
        subjectPhase = ClickyInkCardSubject.SubjectPhase.Failed;
        break;
      default:
        subjectPhase = ClickyInkCardSubject.SubjectPhase.Done;
        break;
      }
      ClickyInkCardSubject.SubjectPhase subjectPhase2 = subjectPhase;
      ClickyInkCardSubject.SubjectKind subjectKind = ClickyInkCardSubject.SubjectKind.Kept;
      bool flag = clickyInkClickyLive2.Phase == ClickyInkClickyPhase.Working;
      ClickyInkCardSubject.SubjectPhase subjectPhase3 = subjectPhase2;
      IClickyInkNoteHost host2 = ClickyInkNoteBridge.Host;
      return new ClickyInkCardSubject(slug, subjectKind, flag, subjectPhase3, (host2 != null && host2.IsClickyUnread(slug)) ? ClickyThingStatus.Ready : ClickyThingStatus.Done, ClickyNotePaperColors.Stock("clicky:" + slug));
    }

    // Token: 0x060016AF RID: 5807 RVA: 0x005BB490 File Offset: 0x005BB490
    public static ClickyInkCardSubject SubjectForJob(ClickyInkJob job)
    {
      ClickyInkCardSubject.SubjectPhase subjectPhase;
      switch (job.Phase)
      {
      case ClickyInkJobPhase.Queued:
      case ClickyInkJobPhase.Working:
        subjectPhase = ClickyInkCardSubject.SubjectPhase.Working;
        break;
      case ClickyInkJobPhase.NeedsYou:
        subjectPhase = ClickyInkCardSubject.SubjectPhase.NeedsYou;
        break;
      case ClickyInkJobPhase.Failed:
        subjectPhase = ClickyInkCardSubject.SubjectPhase.Failed;
        break;
      default:
        subjectPhase = ClickyInkCardSubject.SubjectPhase.Done;
        break;
      }
      ClickyInkCardSubject.SubjectPhase subjectPhase2 = subjectPhase;
      string id = job.Id;
      ClickyInkJobPhase phase = job.Phase;
      bool flag = phase <= ClickyInkJobPhase.Working;
      return new ClickyInkCardSubject(id, ClickyInkCardSubject.SubjectKind.Job, flag, subjectPhase2, (job.Phase == ClickyInkJobPhase.Done && !job.IsSeen) ? ClickyThingStatus.Ready : ClickyThingStatus.Done, ClickyNotePaperColors.Stock("job:" + job.Id));
    }

    // Token: 0x060016B0 RID: 5808 RVA: 0x005BB51C File Offset: 0x005BB51C
    public static ClickyHomeSpreadPaper Paper(ClickyInkCardSubject subject)
    {
      return ClickyHomeSpreadPaper.Of(subject.Stock);
    }

    // Token: 0x060016B1 RID: 5809 RVA: 0x005BB52C File Offset: 0x005BB52C
    public static string ResolvedTarget(string rawTarget, string noteID)
    {
      string text = rawTarget;
      if (!text.StartsWith('/') && !text.StartsWith('\\') && (text.Length < 2 || text[1] != ':') && !text.StartsWith('~') && !text.Contains("://", StringComparison.Ordinal))
      {
        text = Path.Combine(Path.GetDirectoryName(noteID) ?? "", text);
      }
      return ClickyInkPaths.ExpandingTilde(text);
    }

    // Token: 0x060016B2 RID: 5810 RVA: 0x005BB5A0 File Offset: 0x005BB5A0
    public static ClickyInkActions Actions(string noteID, ClickyInkNoteTarget target, ClickyHomeInkNote ink, ClickyInkCardSubject subject)
    {
      ClickyInkLiveNote note = ink.Note;
      ClickyInkActions clickyInkActions = new ClickyInkActions
      {
        Send = delegate(string words)
        {
          string text = words.Trim();
          if (text.Length == 0)
          {
            return;
          }
          ClickyInkNoteBridge.PerformAction(noteID, target, ClickyInkNoteActions.ActionForSentWords(text, note));
        },
        Open = delegate(string opened)
        {
          ClickyInkNoteBridge.OpenTarget(opened, noteID, target);
        },
        ResolvePath = (string raw) => ClickyInkNoteBridge.ResolvedTarget(raw, noteID),
        Toggle = delegate(string blockID, string itemID)
        {
          string text2 = ClickyInkNoteBridge.ItemText(note, blockID, itemID);
          if (text2 == null)
          {
            return;
          }
          ClickyInkNoteStore.Shared.ToggleCheckbox(noteID, text2);
          ValueTuple<ClickyInkDocument.Kind, bool>? valueTuple = ClickyInkNoteBridge.ItemCheckState(note, blockID, itemID);
          if (valueTuple != null)
          {
            ValueTuple<ClickyInkDocument.Kind, bool> valueOrDefault2 = valueTuple.GetValueOrDefault();
            ClickyInkNoteBridge.TrackNoteObjectAction(valueOrDefault2.Item2 ? "item_unticked" : "item_ticked", valueOrDefault2.Item1.RawValue(), target, false, null);
          }
        },
        Perform = delegate(ClickyInkNoteAction action)
        {
          ClickyInkNoteBridge.PerformAction(noteID, target, action);
        },
        Track = delegate(string action, ClickyInkDocument.Kind kind, JsonObject properties)
        {
          ClickyInkNoteBridge.TrackNoteObjectAction(action, kind.RawValue(), target, false, properties);
        }
      };
      ClickyInkNoteTarget.Clicky clicky = target as ClickyInkNoteTarget.Clicky;
      DateTimeOffset? dateTimeOffset;
      if (clicky == null)
      {
        ClickyInkNoteTarget.Job job = target as ClickyInkNoteTarget.Job;
        if (job == null)
        {
          dateTimeOffset = null;
        }
        else
        {
          IClickyInkNoteHost host = ClickyInkNoteBridge.Host;
          DateTimeOffset? dateTimeOffset2;
          if (host == null)
          {
            dateTimeOffset2 = null;
          }
          else
          {
            ClickyInkJob clickyInkJob = host.Job(job.Id);
            dateTimeOffset2 = ((clickyInkJob != null) ? clickyInkJob.FinishedAt : null);
          }
          dateTimeOffset = dateTimeOffset2;
        }
      }
      else
      {
        IClickyInkNoteHost host2 = ClickyInkNoteBridge.Host;
        dateTimeOffset = ((host2 != null) ? host2.ClickyLiveStatus(clicky.Slug).FinishedAt : null);
      }
      DateTimeOffset? dateTimeOffset3 = dateTimeOffset;
      ClickyInkActions clickyInkActions2 = clickyInkActions;
      IReadOnlyList<ClickyInkSentAction> readOnlyList = ClickyInkActionsStore.Shared.Sent(noteID);
      double? lastVersionAt = note.LastVersionAt;
      DateTimeOffset? dateTimeOffset4;
      if (lastVersionAt != null)
      {
        double valueOrDefault = lastVersionAt.GetValueOrDefault();
        dateTimeOffset4 = new DateTimeOffset?(ink.StartedAt.AddSeconds(valueOrDefault));
      }
      else
      {
        dateTimeOffset4 = null;
      }
      clickyInkActions2.Feedback = ClickyInkActionFeedback.Make(readOnlyList, dateTimeOffset4, subject.IsRunning, dateTimeOffset3, note.EditStarts.Select<double, DateTimeOffset>((double s) => ink.StartedAt.AddSeconds(s)).ToList<DateTimeOffset>(), new DateTimeOffset?(DateTimeOffset.Now));
      ClickyInkNoteTarget.Clicky clicky2 = target as ClickyInkNoteTarget.Clicky;
      if (clicky2 != null)
      {
        IClickyInkNoteHost host3 = ClickyInkNoteBridge.Host;
        if (host3 != null)
        {
          clickyInkActions.Routines = host3.RoutineControls(clicky2.Slug, DateTimeOffset.Now);
          clickyInkActions.SetRoutinePaused = delegate(string routineID, bool isPaused)
          {
            IClickyInkNoteHost host4 = ClickyInkNoteBridge.Host;
            if (host4 == null)
            {
              return;
            }
            host4.SetRoutinePaused(routineID, isPaused, "note");
          };
          clickyInkActions.SetRoutineInterval = delegate(string routineID, int minutes)
          {
            IClickyInkNoteHost host5 = ClickyInkNoteBridge.Host;
            if (host5 == null)
            {
              return;
            }
            host5.SetRoutineInterval(routineID, minutes, "note");
          };
          clickyInkActions.RunRoutineNow = delegate(string routineID)
          {
            IClickyInkNoteHost host6 = ClickyInkNoteBridge.Host;
            return host6 != null && host6.RunRoutineNow(routineID);
          };
        }
      }
      return clickyInkActions;
    }

    // Token: 0x060016B3 RID: 5811 RVA: 0x005BB820 File Offset: 0x005BB820
    [return: Nullable(2)]
    public static string ItemText(ClickyInkLiveNote note, string blockID, string itemID)
    {
      Func<ClickyInkLiveNote.Block, bool> <>9__0;
      Func<ClickyInkLiveNote.Item, bool> <>9__1;
      foreach (ClickyInkLiveNote.Page page in note.Pages)
      {
        IEnumerable<ClickyInkLiveNote.Block> blocks = page.Blocks;
        Func<ClickyInkLiveNote.Block, bool> func;
        if ((func = <>9__0) == null)
        {
          func = (<>9__0 = (ClickyInkLiveNote.Block b) => b.Id == blockID);
        }
        ClickyInkLiveNote.Block block = blocks.FirstOrDefault<ClickyInkLiveNote.Block>(func);
        if (block != null)
        {
          IEnumerable<ClickyInkLiveNote.Item> items = block.Items;
          Func<ClickyInkLiveNote.Item, bool> func2;
          if ((func2 = <>9__1) == null)
          {
            func2 = (<>9__1 = (ClickyInkLiveNote.Item i) => i.Id == itemID);
          }
          ClickyInkLiveNote.Item item = items.FirstOrDefault<ClickyInkLiveNote.Item>(func2);
          return (item != null) ? item.Source.Text : null;
        }
      }
      return null;
    }

    // Token: 0x060016B4 RID: 5812 RVA: 0x005BB8F4 File Offset: 0x005BB8F4
    [return: TupleElementNames(new string[] { "Kind", "WasChecked" })]
    [return: Nullable(0)]
    public static ValueTuple<ClickyInkDocument.Kind, bool>? ItemCheckState(ClickyInkLiveNote note, string blockID, string itemID)
    {
      Func<ClickyInkLiveNote.Block, bool> <>9__0;
      Func<ClickyInkLiveNote.Item, bool> <>9__1;
      foreach (ClickyInkLiveNote.Page page in note.Pages)
      {
        IEnumerable<ClickyInkLiveNote.Block> blocks = page.Blocks;
        Func<ClickyInkLiveNote.Block, bool> func;
        if ((func = <>9__0) == null)
        {
          func = (<>9__0 = (ClickyInkLiveNote.Block b) => b.Id == blockID);
        }
        ClickyInkLiveNote.Block block = blocks.FirstOrDefault<ClickyInkLiveNote.Block>(func);
        if (block != null)
        {
          IEnumerable<ClickyInkLiveNote.Item> items = block.Items;
          Func<ClickyInkLiveNote.Item, bool> func2;
          if ((func2 = <>9__1) == null)
          {
            func2 = (<>9__1 = (ClickyInkLiveNote.Item i) => i.Id == itemID);
          }
          ClickyInkLiveNote.Item item = items.FirstOrDefault<ClickyInkLiveNote.Item>(func2);
          if (item != null)
          {
            return new ValueTuple<ClickyInkDocument.Kind, bool>?(new ValueTuple<ClickyInkDocument.Kind, bool>(block.Source.Kind, item.Source.IsChecked.GetValueOrDefault()));
          }
        }
      }
      return null;
    }

    // Token: 0x060016B5 RID: 5813 RVA: 0x005BB9F4 File Offset: 0x005BB9F4
    public static void TrackNoteObjectAction(string action, string objectKind, ClickyInkNoteTarget target, bool sendsToAgent, [Nullable(2)] JsonObject properties = null)
    {
      properties = ((properties == null) ? new JsonObject(null) : ((JsonObject)properties.DeepClone()));
      properties["action"] = action;
      properties["object"] = objectKind;
      properties["sends_to_agent"] = sendsToAgent;
      ClickyInkNoteTarget.Clicky clicky = target as ClickyInkNoteTarget.Clicky;
      if (clicky != null)
      {
        properties["is_kept_note"] = true;
        ClickyAnalytics.TrackProductEvent("note_object_action", clicky.Slug, null, properties);
        return;
      }
      ClickyInkNoteTarget.Job job = target as ClickyInkNoteTarget.Job;
      if (job == null)
      {
        return;
      }
      properties["is_kept_note"] = false;
      string text = "note_object_action";
      string text2 = null;
      IClickyInkNoteHost host = ClickyInkNoteBridge.Host;
      string text3;
      if (host == null)
      {
        text3 = null;
      }
      else
      {
        ClickyInkJob clickyInkJob = host.Job(job.Id);
        text3 = ((clickyInkJob != null) ? clickyInkJob.ThreadId : null);
      }
      ClickyAnalytics.TrackProductEvent(text, text2, text3, properties);
    }

    // Token: 0x060016B6 RID: 5814 RVA: 0x005BBAD8 File Offset: 0x005BBAD8
    public static void PerformAction(string noteID, ClickyInkNoteTarget target, ClickyInkNoteAction action)
    {
      ClickyInkNoteBridge.<>c__DisplayClass14_0 CS$<>8__locals1 = new ClickyInkNoteBridge.<>c__DisplayClass14_0();
      CS$<>8__locals1.action = action;
      IReadOnlyList<ClickyInkSentAction> readOnlyList = ClickyInkActionsStore.Shared.Sent(noteID);
      if (readOnlyList.Count > 0)
      {
        IReadOnlyList<ClickyInkSentAction> readOnlyList2 = readOnlyList;
        ClickyInkSentAction clickyInkSentAction = readOnlyList2[readOnlyList2.Count - 1];
        if (clickyInkSentAction != null && clickyInkSentAction.ObjectKey == CS$<>8__locals1.action.ObjectKey && clickyInkSentAction.MarginWords == CS$<>8__locals1.action.MarginWords && (DateTimeOffset.Now - clickyInkSentAction.SentAt).TotalSeconds < 3.0)
        {
          return;
        }
      }
      ClickyInkNoteBridge.<>c__DisplayClass14_0 CS$<>8__locals2 = CS$<>8__locals1;
      ClickyInkLiveNote clickyInkLiveNote = ClickyInkNoteStore.Shared.LiveNote(noteID);
      IReadOnlyList<DateTimeOffset> readOnlyList3;
      if (clickyInkLiveNote != null)
      {
        DateTimeOffset? dateTimeOffset = ClickyInkNoteStore.Shared.StartedAt(noteID);
        if (dateTimeOffset != null)
        {
          CS$<>8__locals1.startedAt = dateTimeOffset.GetValueOrDefault();
          readOnlyList3 = clickyInkLiveNote.EditStarts.Select<double, DateTimeOffset>((double s) => CS$<>8__locals1.startedAt.AddSeconds(s)).ToList<DateTimeOffset>();
          goto IL_00E1;
        }
      }
      readOnlyList3 = new List<DateTimeOffset>();
      IL_00E1:
      CS$<>8__locals2.editStartedAt = readOnlyList3;
      ClickyInkNoteAction.ActionKind kind = CS$<>8__locals1.action.Kind;
      bool flag = kind - ClickyInkNoteAction.ActionKind.Answer <= 1;
      if (flag && readOnlyList.Any<ClickyInkSentAction>((ClickyInkSentAction s) => s.ObjectKey == CS$<>8__locals1.action.ObjectKey && s.Kind == CS$<>8__locals1.action.Kind && ClickyInkActionFeedback.IsStillCurrent(s, CS$<>8__locals1.editStartedAt)))
      {
        return;
      }
      bool flag2 = ClickyInkNoteStore.Shared.IsTurnWriting(noteID);
      ClickyInkNotePages.Shared.NoteInkActionSent(noteID);
      ClickyInkActionsStore.Shared.Record(new ClickyInkSentAction(CS$<>8__locals1.action, DateTimeOffset.Now, flag2), noteID);
      Sounds.Play("clicky-text-send", 0.2);
      JsonObject jsonObject = new JsonObject(null);
      jsonObject["action"] = "note_" + CS$<>8__locals1.action.Kind.ToString().ToLowerInvariant();
      jsonObject["was_running"] = flag2;
      JsonObject jsonObject2 = jsonObject;
      ClickyInkNoteTarget.Clicky clicky = target as ClickyInkNoteTarget.Clicky;
      if (clicky == null)
      {
        ClickyInkNoteTarget.Job job = target as ClickyInkNoteTarget.Job;
        if (job != null)
        {
          IClickyInkNoteHost host = ClickyInkNoteBridge.Host;
          ClickyInkJob clickyInkJob = ((host != null) ? host.Job(job.Id) : null);
          if (clickyInkJob != null)
          {
            string threadId = clickyInkJob.ThreadId;
            if (threadId != null)
            {
              ClickyInkNoteBridge.Host.SendToThread(threadId, CS$<>8__locals1.action.Message, CS$<>8__locals1.action.LaunchContext);
              ClickyAnalytics.TrackProductEvent("job_action", null, threadId, jsonObject2);
              goto IL_028F;
            }
          }
          return;
        }
      }
      else
      {
        IClickyInkNoteHost host2 = ClickyInkNoteBridge.Host;
        if (host2 == null || !host2.ClickyExists(clicky.Slug))
        {
          return;
        }
        host2.SendToClicky(clicky.Slug, CS$<>8__locals1.action.Message, CS$<>8__locals1.action.LaunchContext);
        ClickyAnalytics.TrackProductEvent("clicky_dashboard_action", clicky.Slug, null, jsonObject2);
      }
      IL_028F:
      JsonObject jsonObject3 = new JsonObject(null);
      jsonObject3["was_running"] = flag2;
      JsonObject jsonObject4 = jsonObject3;
      string analyticsMethod = CS$<>8__locals1.action.AnalyticsMethod;
      if (analyticsMethod != null)
      {
        jsonObject4["method"] = analyticsMethod;
      }
      foreach (KeyValuePair<string, int> keyValuePair in CS$<>8__locals1.action.AnalyticsCounts)
      {
        string text;
        int num;
        keyValuePair.Deconstruct(out text, out num);
        string text2 = text;
        int num2 = num;
        jsonObject4[text2] = num2;
      }
      ClickyInkNoteBridge.TrackNoteObjectAction((CS$<>8__locals1.action.AnalyticsAction.Length == 0) ? ("note_" + CS$<>8__locals1.action.Kind.ToString().ToLowerInvariant()) : CS$<>8__locals1.action.AnalyticsAction, CS$<>8__locals1.action.AnalyticsObject, target, true, jsonObject4);
      ClickyInkNoteStore.Shared.NoteActionSent();
    }

    // Token: 0x060016B7 RID: 5815 RVA: 0x005BBE88 File Offset: 0x005BBE88
    public static void OpenTarget(string rawTarget, string noteID, ClickyInkNoteTarget owner)
    {
      string text = ClickyInkNoteBridge.ResolvedTarget(rawTarget, noteID);
      bool flag = text.StartsWith("https://", StringComparison.Ordinal) || text.StartsWith("http://", StringComparison.Ordinal);
      string text2 = (flag ? "link_opened" : "file_opened");
      string text3 = (flag ? "link" : "file");
      bool flag2 = false;
      JsonObject jsonObject = new JsonObject(null);
      jsonObject["artifact_kind"] = ClickyAnalytics.ProductArtifactKind(text);
      ClickyInkNoteBridge.TrackNoteObjectAction(text2, text3, owner, flag2, jsonObject);
      ClickyInkNoteTarget.Job job = owner as ClickyInkNoteTarget.Job;
      if (job == null)
      {
        ClickyInkNoteTarget.Clicky clicky = owner as ClickyInkNoteTarget.Clicky;
        if (clicky == null)
        {
          return;
        }
        IClickyInkNoteHost host = ClickyInkNoteBridge.Host;
        string text4 = ((host != null) ? host.BoundThreadId(clicky.Slug) : null);
        IClickyInkNoteHost host2 = ClickyInkNoteBridge.Host;
        if (host2 != null && host2.OpenInViewerIfSupported(text, clicky.Slug, text4))
        {
          return;
        }
        ClickyInkNoteBridge.OpenInItsApp(text);
        return;
      }
      else
      {
        IClickyInkNoteHost host3 = ClickyInkNoteBridge.Host;
        if (host3 == null)
        {
          return;
        }
        host3.OpenJobArtifact(text, job.Id);
        return;
      }
    }

    // Token: 0x060016B8 RID: 5816 RVA: 0x005BBF74 File Offset: 0x005BBF74
    public static void OpenInItsApp(string target)
    {
      try
      {
        string text;
        if (target.Contains("://", StringComparison.Ordinal))
        {
          Uri uri;
          if (!Uri.TryCreate(target, UriKind.Absolute, out uri))
          {
            return;
          }
          text = uri.AbsoluteUri;
        }
        else
        {
          text = ClickyInkPaths.ExpandingTilde(target);
        }
        ShellOpen.Start(text);
      }
      catch (Exception ex)
      {
        Log.Warn("ink", "open " + target + ": " + ex.Message);
      }
    }

    // Token: 0x04000B25 RID: 2853
    private const string DashboardActionEvent = "clicky_dashboard_action";

    // Token: 0x04000B26 RID: 2854
    private const string JobActionEvent = "job_action";
  }
}
