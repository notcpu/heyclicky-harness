using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;

namespace HeyClicky.Core.Ink
{
  // Token: 0x02000168 RID: 360
  [NullableContext(1)]
  [Nullable(0)]
  public sealed class ClickyInkNotePages
  {
    // Token: 0x170004FF RID: 1279
    // (get) Token: 0x060015CD RID: 5581 RVA: 0x003BA5F4 File Offset: 0x003BA5F4
    public static ClickyInkNotePages Shared { get; } = new ClickyInkNotePages();

    // Token: 0x17000500 RID: 1280
    // (get) Token: 0x060015CE RID: 5582 RVA: 0x003BA5FC File Offset: 0x003BA5FC
    public IReadOnlyDictionary<string, ClickyInkNotePage> CurrentPages
    {
      get
      {
        return this.currentPages;
      }
    }

    // Token: 0x14000033 RID: 51
    // (add) Token: 0x060015CF RID: 5583 RVA: 0x003BA604 File Offset: 0x003BA604
    // (remove) Token: 0x060015D0 RID: 5584 RVA: 0x003BA63C File Offset: 0x003BA63C
    [Nullable(2)]
    [method: NullableContext(2)]
    [field: Nullable(2)]
    public event Action Changed;

    // Token: 0x060015D1 RID: 5585 RVA: 0x003BA674 File Offset: 0x003BA674
    private ClickyInkNotePages()
    {
    }

    // Token: 0x060015D2 RID: 5586 RVA: 0x003BA694 File Offset: 0x003BA694
    [return: Nullable(2)]
    public ClickyInkNotePage CurrentPage(string notePath)
    {
      ClickyInkNotePage clickyInkNotePage;
      if (!this.currentPages.TryGetValue(notePath, out clickyInkNotePage))
      {
        return null;
      }
      return clickyInkNotePage;
    }

    // Token: 0x060015D3 RID: 5587 RVA: 0x003BA6B4 File Offset: 0x003BA6B4
    public void NoteInkActionSent(string notePath)
    {
      this.lastInkActionAt[notePath] = DateTimeOffset.Now;
    }

    // Token: 0x060015D4 RID: 5588 RVA: 0x003BA6C8 File Offset: 0x003BA6C8
    public bool UserMessageSent(string message, string notePath)
    {
      DateTimeOffset dateTimeOffset;
      if (this.lastInkActionAt.TryGetValue(notePath, out dateTimeOffset) && (DateTimeOffset.Now - dateTimeOffset).TotalSeconds < 10.0)
      {
        this.lastInkActionAt.Remove(notePath);
        return false;
      }
      return this.TurnPage(message, false, notePath);
    }

    // Token: 0x060015D5 RID: 5589 RVA: 0x003BA71C File Offset: 0x003BA71C
    public bool RoutineRunStarted(string runLine, string notePath)
    {
      return this.TurnPage(runLine, true, notePath);
    }

    // Token: 0x060015D6 RID: 5590 RVA: 0x003BA728 File Offset: 0x003BA728
    private bool TurnPage(string message, bool isRoutineRun, string notePath)
    {
      string text = message.TrimWsNl();
      if (text.Length == 0)
      {
        return false;
      }
      bool flag = (InkFiles.ReadUtf8(notePath) ?? "").TrimWsNl().Length > 0;
      ClickyInkNotePage clickyInkNotePage = this.CurrentPage(notePath);
      int num = ((clickyInkNotePage != null) ? clickyInkNotePage.Number : 0) + 1;
      this.currentPages[notePath] = new ClickyInkNotePage(num, text, isRoutineRun);
      Action changed = this.Changed;
      if (changed != null)
      {
        changed();
      }
      string text2 = Path.Combine(Path.GetDirectoryName(notePath) ?? "", "note-pages");
      try
      {
        if (Directory.Exists(text2))
        {
          Directory.Delete(text2, true);
        }
      }
      catch (Exception)
      {
      }
      if (!flag)
      {
        return false;
      }
      try
      {
        File.Delete(notePath);
      }
      catch (Exception ex)
      {
        Log.Warn("ink pages", "couldn't clear " + notePath + ": " + ex.Message);
        return false;
      }
      ClickyInkNoteStore.Shared.StartFreshPage(notePath);
      ClickyLiveNoteStore.Shared.ReadNow(notePath);
      return true;
    }

    // Token: 0x060015D7 RID: 5591 RVA: 0x003BA838 File Offset: 0x003BA838
    public void RemoveAllForSignOut()
    {
      this.currentPages.Clear();
      this.lastInkActionAt.Clear();
      Action changed = this.Changed;
      if (changed == null)
      {
        return;
      }
      changed();
    }

    // Token: 0x04000988 RID: 2440
    private const string RetiredFolderName = "note-pages";

    // Token: 0x04000989 RID: 2441
    private const double InkActionGraceSeconds = 10.0;

    // Token: 0x0400098A RID: 2442
    private readonly Dictionary<string, ClickyInkNotePage> currentPages = new Dictionary<string, ClickyInkNotePage>();

    // Token: 0x0400098B RID: 2443
    private readonly Dictionary<string, DateTimeOffset> lastInkActionAt = new Dictionary<string, DateTimeOffset>();
  }
}
