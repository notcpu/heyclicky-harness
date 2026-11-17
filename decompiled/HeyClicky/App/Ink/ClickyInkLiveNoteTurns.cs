using System;
using System.IO;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using HeyClicky.Agents;
using HeyClicky.Agents.Clickys;
using HeyClicky.Agents.Jobs;
using HeyClicky.Core.Ink;
using Microsoft.UI.Dispatching;

namespace HeyClicky.App.Ink
{
  // Token: 0x020001A5 RID: 421
  [NullableContext(1)]
  [Nullable(0)]
  public sealed class ClickyInkLiveNoteTurns : IClickyLiveNoteTurns
  {
    // Token: 0x060016B9 RID: 5817 RVA: 0x005BBFE8 File Offset: 0x005BBFE8
    public ClickyInkLiveNoteTurns(AgentService agents, DispatcherQueue ui)
    {
    }

    // Token: 0x060016BA RID: 5818 RVA: 0x005BC000 File Offset: 0x005BC000
    [return: Nullable(2)]
    public string NotePath(string clickySlug, string workspacePath)
    {
      if (!ClickyLiveNotePreferences.IsEnabled)
      {
        return null;
      }
      ClickyInfo clickyInfo = this.<agents>P.FindClicky(clickySlug);
      string text = ((clickyInfo != null) ? clickyInfo.ThreadId : null);
      if (text != null)
      {
        ClickyJob clickyJob = this.<agents>P.Jobs.JobForThread(text);
        if (clickyJob != null)
        {
          return ClickyLiveNotePreferences.NotePath(clickyJob.FolderPath);
        }
      }
      if (workspacePath.Length != 0)
      {
        return ClickyLiveNotePreferences.NotePath(workspacePath);
      }
      return null;
    }

    // Token: 0x060016BB RID: 5819 RVA: 0x005BC064 File Offset: 0x005BC064
    public string PromptSection(string notePath, [Nullable(2)] string userMessage, [Nullable(2)] string routineRun, ClickyLiveNoteWriter writer)
    {
      return this.OnUi<string>(delegate
      {
        bool flag = ((userMessage != null) ? ClickyInkNotePages.Shared.UserMessageSent(userMessage, notePath) : (routineRun != null && ClickyInkNotePages.Shared.RoutineRunStarted(routineRun, notePath)));
        ClickyInkNoteStore.Shared.NoteTurnStarted(notePath, true);
        string text = Path.GetDirectoryName(notePath) ?? "";
        bool flag3;
        bool flag2 = (flag3 = flag) && userMessage == null;
        ClickyLiveNoteWriter writer2 = writer;
        ClickyInkBrief.NoteWriter noteWriter;
        if (writer2 != ClickyLiveNoteWriter.FileTools)
        {
          if (writer2 == ClickyLiveNoteWriter.NoteTools)
          {
            noteWriter = ClickyInkBrief.NoteWriter.NoteTools;
          }
          else
          {
            noteWriter = ClickyInkBrief.NoteWriter.ApplyPatch;
          }
        }
        else
        {
          noteWriter = ClickyInkBrief.NoteWriter.FileTools;
        }
        return ClickyInkBrief.AgentSection(text, true, flag3, flag2, noteWriter);
      }, "");
    }

    // Token: 0x060016BC RID: 5820 RVA: 0x005BC0AC File Offset: 0x005BC0AC
    public void FileActivity(string notePath, string path, [Nullable(2)] string streamedKind, [Nullable(2)] string streamedDiff)
    {
      if (!string.Equals(Path.GetFileName(path), "note.md", StringComparison.OrdinalIgnoreCase))
      {
        return;
      }
      string fullPath;
      try
      {
        fullPath = Path.GetFullPath(Path.IsPathRooted(path) ? path : Path.Combine(Path.GetDirectoryName(notePath) ?? "", path));
      }
      catch
      {
        return;
      }
      if (!string.Equals(fullPath, notePath, StringComparison.OrdinalIgnoreCase))
      {
        return;
      }
      this.<ui>P.TryEnqueue(delegate
      {
        if (streamedKind != null && streamedDiff != null)
        {
          ClickyLiveNoteStore.Shared.StreamPatch(notePath, streamedKind, streamedDiff);
          return;
        }
        ClickyLiveNoteStore.Shared.ReadNow(notePath);
      });
    }

    // Token: 0x060016BD RID: 5821 RVA: 0x005BC154 File Offset: 0x005BC154
    public void TurnEnded(string notePath)
    {
      this.<ui>P.TryEnqueue(delegate
      {
        ClickyLiveNoteStore.Shared.ReadNow(notePath);
        ClickyInkNoteStore.Shared.NoteTurnEnded(notePath);
      });
    }

    // Token: 0x060016BE RID: 5822 RVA: 0x005BC188 File Offset: 0x005BC188
    public void Forget(string notePath)
    {
      this.<ui>P.TryEnqueue(delegate
      {
        ClickyInkNoteStore.Shared.Forget(notePath);
      });
    }

    // Token: 0x060016BF RID: 5823 RVA: 0x005BC1BC File Offset: 0x005BC1BC
    public static ClickyInkJob InkJob(AgentService agents, ClickyJob job)
    {
      string id = job.Id;
      ClickyInkJobPhase clickyInkJobPhase;
      switch (agents.RailPhase(job))
      {
      case ClickyJobRailPhase.Queued:
        clickyInkJobPhase = ClickyInkJobPhase.Queued;
        break;
      case ClickyJobRailPhase.Working:
        clickyInkJobPhase = ClickyInkJobPhase.Working;
        break;
      case ClickyJobRailPhase.NeedsYou:
        clickyInkJobPhase = ClickyInkJobPhase.NeedsYou;
        break;
      case ClickyJobRailPhase.Done:
        clickyInkJobPhase = ClickyInkJobPhase.Done;
        break;
      case ClickyJobRailPhase.Failed:
        clickyInkJobPhase = ClickyInkJobPhase.Failed;
        break;
      default:
        clickyInkJobPhase = ClickyInkJobPhase.Stopped;
        break;
      }
      return new ClickyInkJob(id, clickyInkJobPhase, job.IsSeen, job.FinishedAt, job.ThreadId);
    }

    // Token: 0x060016C0 RID: 5824 RVA: 0x005BC224 File Offset: 0x005BC224
    private T OnUi<[Nullable(2)] T>(Func<T> work, T fallback)
    {
      if (this.<ui>P.HasThreadAccess)
      {
        return work();
      }
      TaskCompletionSource<T> done = new TaskCompletionSource<T>(TaskCreationOptions.RunContinuationsAsynchronously);
      if (!this.<ui>P.TryEnqueue(delegate
      {
        try
        {
          done.SetResult(work());
        }
        catch (Exception ex)
        {
          Log.Warn("ink", "live note: " + ex.Message);
          done.SetResult(fallback);
        }
      }))
      {
        return fallback;
      }
      if (!done.Task.Wait(TimeSpan.FromSeconds(5L)))
      {
        return fallback;
      }
      return done.Task.Result;
    }

    // Token: 0x04000B27 RID: 2855
    [CompilerGenerated]
    private AgentService <agents>P = agents;

    // Token: 0x04000B28 RID: 2856
    [CompilerGenerated]
    private DispatcherQueue <ui>P = ui;
  }
}
