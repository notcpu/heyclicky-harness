using System;
using System.Runtime.CompilerServices;
using HeyClicky.Agents;
using HeyClicky.Agents.Clickys;
using HeyClicky.Agents.Jobs;
using HeyClicky.Core.Ink;

namespace HeyClicky.App.Dock
{
  // Token: 0x020002E7 RID: 743
  [NullableContext(1)]
  [Nullable(0)]
  public static class ArcLiveNotes
  {
    // Token: 0x170005E2 RID: 1506
    // (get) Token: 0x06001FF2 RID: 8178 RVA: 0x00611100 File Offset: 0x00611100
    public static bool IsEnabled
    {
      get
      {
        return ClickyAppsPreferences.IsEnabled && ClickyLiveNotePreferences.IsEnabled;
      }
    }

    // Token: 0x06001FF3 RID: 8179 RVA: 0x00611110 File Offset: 0x00611110
    public static string NotePath(string folder)
    {
      return ClickyLiveNotePreferences.NotePath(folder);
    }

    // Token: 0x06001FF4 RID: 8180 RVA: 0x00611118 File Offset: 0x00611118
    [return: Nullable(2)]
    public static string ClickyNotePath(AgentService agents, string slug)
    {
      ClickyInfo clickyInfo = agents.FindClicky(slug);
      string text = ((clickyInfo != null) ? clickyInfo.ThreadId : null);
      if (text != null)
      {
        ClickyJob clickyJob = agents.Jobs.JobForThread(text);
        if (clickyJob != null)
        {
          return ArcLiveNotes.NotePath(clickyJob.FolderPath);
        }
      }
      string text2 = agents.WorkspacePath(slug);
      if (text2 == null || text2.Length <= 0)
      {
        return null;
      }
      return ArcLiveNotes.NotePath(text2);
    }
  }
}
