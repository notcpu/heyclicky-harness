using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace HeyClicky.Core.Ink
{
  // Token: 0x0200016B RID: 363
  [NullableContext(1)]
  [Nullable(0)]
  public sealed class ClickyInkNoteStore
  {
    // Token: 0x1700050A RID: 1290
    // (get) Token: 0x060015FF RID: 5631 RVA: 0x003BAE10 File Offset: 0x003BAE10
    public static ClickyInkNoteStore Shared { get; } = new ClickyInkNoteStore(null);

    // Token: 0x1700050B RID: 1291
    // (get) Token: 0x06001600 RID: 5632 RVA: 0x003BAE18 File Offset: 0x003BAE18
    // (set) Token: 0x06001601 RID: 5633 RVA: 0x003BAE20 File Offset: 0x003BAE20
    public int Revision { get; private set; }

    // Token: 0x14000034 RID: 52
    // (add) Token: 0x06001602 RID: 5634 RVA: 0x003BAE2C File Offset: 0x003BAE2C
    // (remove) Token: 0x06001603 RID: 5635 RVA: 0x003BAE64 File Offset: 0x003BAE64
    [Nullable(2)]
    [method: NullableContext(2)]
    [field: Nullable(2)]
    public event Action RevisionChanged;

    // Token: 0x1700050C RID: 1292
    // (get) Token: 0x06001604 RID: 5636 RVA: 0x003BAE9C File Offset: 0x003BAE9C
    // (set) Token: 0x06001605 RID: 5637 RVA: 0x003BAEA4 File Offset: 0x003BAEA4
    [Nullable(new byte[] { 2, 1 })]
    public static Action<string> ClearLiveSteps
    {
      [return: Nullable(new byte[] { 2, 1 })]
      get;
      [param: Nullable(new byte[] { 2, 1 })]
      set;
    }

    // Token: 0x06001606 RID: 5638 RVA: 0x003BAEAC File Offset: 0x003BAEAC
    private void Bump()
    {
      this.Revision++;
      Action revisionChanged = this.RevisionChanged;
      if (revisionChanged == null)
      {
        return;
      }
      revisionChanged();
    }

    // Token: 0x1700050D RID: 1293
    // (get) Token: 0x06001607 RID: 5639 RVA: 0x003BAECC File Offset: 0x003BAECC
    public static bool IsEnabled
    {
      get
      {
        return ClickyLiveNotePreferences.IsEnabled;
      }
    }

    // Token: 0x1700050E RID: 1294
    // (get) Token: 0x06001608 RID: 5640 RVA: 0x003BAED4 File Offset: 0x003BAED4
    // (set) Token: 0x06001609 RID: 5641 RVA: 0x003BAEDC File Offset: 0x003BAEDC
    [Nullable(2)]
    public string ScopeKey
    {
      [NullableContext(2)]
      get;
      [NullableContext(2)]
      private set;
    } = "local";

    // Token: 0x0600160A RID: 5642 RVA: 0x003BAEE8 File Offset: 0x003BAEE8
    [NullableContext(2)]
    public ClickyInkNoteStore(string baseDirectory)
    {
      this.baseDirectoryOverride = baseDirectory;
    }

    // Token: 0x0600160B RID: 5643 RVA: 0x003BAF94 File Offset: 0x003BAF94
    public void RemoveAllForSignOut()
    {
      string scopeKey = this.ScopeKey;
      this.versionsByNoteId.Clear();
      this.lastDocumentByNoteId.Clear();
      this.liveNotes.Clear();
      this.settledNotes.Clear();
      this.watched.Clear();
      this.finishTimes.Clear();
      this.turnsAwaitingTheirFirstEdit.Clear();
      this.freshPageNoteIds.Clear();
      this.lastReadStat.Clear();
      this.pendingStat.Clear();
      this.StopWatchTimer();
      this.Bump();
      if (scopeKey != null)
      {
        string directory = ClickyInkNoteStore.ScopeDirectory(this.BaseDirectory, scopeKey);
        if (directory != null)
        {
          this.writeQueue.Async(delegate
          {
            if (Directory.Exists(directory))
            {
              Directory.Delete(directory, true);
            }
          });
        }
      }
    }

    // Token: 0x0600160C RID: 5644 RVA: 0x003BB05C File Offset: 0x003BB05C
    public IReadOnlyList<ClickyInkNoteVersion> Versions(string noteId)
    {
      return this.LoadVersions(noteId);
    }

    // Token: 0x0600160D RID: 5645 RVA: 0x003BB068 File Offset: 0x003BB068
    [return: Nullable(2)]
    public ClickyHomeInkNote InkNote(string noteId)
    {
      if (!ClickyInkNoteStore.IsEnabled)
      {
        return null;
      }
      if (this.freshPageNoteIds.Contains(noteId) && this.LoadVersions(noteId).Count == 0)
      {
        return new ClickyHomeInkNote(new ClickyInkLiveNote(), DateTimeOffset.Now);
      }
      if (!this.liveNotes.ContainsKey(noteId) && !this.watched.ContainsKey(noteId) && !this.keptNoteIds.Contains(noteId))
      {
        ClickyInkNoteVersion clickyInkNoteVersion = this.LoadVersions(noteId).LastOrDefault<ClickyInkNoteVersion>((ClickyInkNoteVersion v) => !v.IsPartial.GetValueOrDefault());
        if (clickyInkNoteVersion != null && (DateTimeOffset.Now - clickyInkNoteVersion.At).TotalSeconds > 3600.0)
        {
          ClickyInkLiveNote clickyInkLiveNote = this.SettledNote(noteId);
          if (clickyInkLiveNote != null)
          {
            return new ClickyHomeInkNote(clickyInkLiveNote, DateTimeOffset.MinValue);
          }
        }
      }
      ClickyInkLiveNote clickyInkLiveNote2 = this.LiveNote(noteId);
      if (clickyInkLiveNote2 != null)
      {
        DateTimeOffset? dateTimeOffset = this.StartedAt(noteId);
        if (dateTimeOffset != null)
        {
          DateTimeOffset valueOrDefault = dateTimeOffset.GetValueOrDefault();
          if (this.IsSettled(noteId, clickyInkLiveNote2, valueOrDefault, DateTimeOffset.Now))
          {
            ClickyInkLiveNote clickyInkLiveNote3 = this.SettledNote(noteId);
            if (clickyInkLiveNote3 != null)
            {
              return new ClickyHomeInkNote(clickyInkLiveNote3, DateTimeOffset.MinValue);
            }
          }
          return new ClickyHomeInkNote(clickyInkLiveNote2, valueOrDefault);
        }
      }
      return null;
    }

    // Token: 0x0600160E RID: 5646 RVA: 0x003BB19C File Offset: 0x003BB19C
    [return: Nullable(2)]
    public ClickyInkLiveNote LiveNote(string noteId)
    {
      ClickyInkLiveNote clickyInkLiveNote;
      if (this.liveNotes.TryGetValue(noteId, out clickyInkLiveNote))
      {
        return clickyInkLiveNote;
      }
      List<ClickyInkNoteVersion> list = this.LoadVersions(noteId);
      if (list.Count == 0)
      {
        return null;
      }
      ClickyInkNoteVersion clickyInkNoteVersion = list[0];
      ClickyInkLiveNote clickyInkLiveNote2 = new ClickyInkLiveNote();
      List<double> list3;
      List<double> list2 = (this.finishTimes.TryGetValue(noteId, out list3) ? list3 : new List<double>()).OrderBy<double, double>((double t) => t).ToList<double>();
      foreach (ClickyInkNoteVersion clickyInkNoteVersion2 in list)
      {
        double totalSeconds = (clickyInkNoteVersion2.At - clickyInkNoteVersion.At).TotalSeconds;
        while (list2.Count > 0 && list2[0] <= totalSeconds)
        {
          clickyInkLiveNote2.Finish(list2[0]);
          list2.RemoveAt(0);
        }
        clickyInkLiveNote2.Apply(ClickyInkParser.Parse(clickyInkNoteVersion2.Text, clickyInkNoteVersion2.IsPartial.GetValueOrDefault()), totalSeconds, clickyInkNoteVersion2.IsEdit, clickyInkNoteVersion2.StartsNewEdit, clickyInkNoteVersion2.IsPartial.GetValueOrDefault());
      }
      foreach (double num in list2)
      {
        clickyInkLiveNote2.Finish(num);
      }
      this.liveNotes[noteId] = clickyInkLiveNote2;
      return clickyInkLiveNote2;
    }

    // Token: 0x0600160F RID: 5647 RVA: 0x003BB340 File Offset: 0x003BB340
    [return: Nullable(2)]
    public ClickyInkLiveNote SettledNote(string noteId)
    {
      List<ClickyInkNoteVersion> list = this.LoadVersions(noteId);
      ClickyInkNoteVersion clickyInkNoteVersion = list.LastOrDefault<ClickyInkNoteVersion>((ClickyInkNoteVersion v) => !v.IsPartial.GetValueOrDefault());
      if (clickyInkNoteVersion == null)
      {
        return null;
      }
      DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(1, 2);
      defaultInterpolatedStringHandler.AppendFormatted<int>(list.Count);
      defaultInterpolatedStringHandler.AppendLiteral("|");
      defaultInterpolatedStringHandler.AppendFormatted(InkText.DoubleDescription(InkFiles.SecondsSince1970(clickyInkNoteVersion.At)));
      string text = defaultInterpolatedStringHandler.ToStringAndClear();
      ValueTuple<string, ClickyInkLiveNote> valueTuple;
      if (this.settledNotes.TryGetValue(noteId, out valueTuple) && valueTuple.Item1 == text)
      {
        return valueTuple.Item2;
      }
      ClickyInkLiveNote clickyInkLiveNote = new ClickyInkLiveNote();
      clickyInkLiveNote.Apply(ClickyInkParser.Parse(clickyInkNoteVersion.Text, false), 0.0, false, null, false);
      this.settledNotes[noteId] = new ValueTuple<string, ClickyInkLiveNote>(text, clickyInkLiveNote);
      return clickyInkLiveNote;
    }

    // Token: 0x06001610 RID: 5648 RVA: 0x003BB42C File Offset: 0x003BB42C
    private bool IsSettled(string noteId, ClickyInkLiveNote live, DateTimeOffset startedAt, DateTimeOffset now)
    {
      return !this.watched.ContainsKey(noteId) && !this.keptNoteIds.Contains(noteId) && (now - startedAt).TotalSeconds > live.FinishedClock + 1.6;
    }

    // Token: 0x06001611 RID: 5649 RVA: 0x003BB47C File Offset: 0x003BB47C
    public DateTimeOffset? LastEditAt(string noteId)
    {
      ClickyInkNoteVersion clickyInkNoteVersion = this.LoadVersions(noteId).LastOrDefault<ClickyInkNoteVersion>((ClickyInkNoteVersion v) => v.IsEdit && !v.IsPartial.GetValueOrDefault());
      if (clickyInkNoteVersion == null)
      {
        return null;
      }
      return new DateTimeOffset?(clickyInkNoteVersion.At);
    }

    // Token: 0x06001612 RID: 5650 RVA: 0x003BB4CC File Offset: 0x003BB4CC
    public DateTimeOffset? StartedAt(string noteId)
    {
      List<ClickyInkNoteVersion> list = this.LoadVersions(noteId);
      if (list.Count <= 0)
      {
        return null;
      }
      return new DateTimeOffset?(list[0].At);
    }

    // Token: 0x06001613 RID: 5651 RVA: 0x003BB508 File Offset: 0x003BB508
    public double Clock(string noteId, DateTimeOffset? now = null)
    {
      DateTimeOffset? dateTimeOffset = this.StartedAt(noteId);
      if (dateTimeOffset != null)
      {
        DateTimeOffset valueOrDefault = dateTimeOffset.GetValueOrDefault();
        return ((now ?? DateTimeOffset.Now) - valueOrDefault).TotalSeconds;
      }
      return 0.0;
    }

    // Token: 0x06001614 RID: 5652 RVA: 0x003BB564 File Offset: 0x003BB564
    public void NoteTurnStarted(string noteId, bool isKept)
    {
      if (isKept)
      {
        this.keptNoteIds.Add(noteId);
      }
      else
      {
        this.keptNoteIds.Remove(noteId);
      }
      if (!ClickyInkNoteStore.IsEnabled || this.watched.ContainsKey(noteId))
      {
        return;
      }
      bool flag = this.LoadVersions(noteId).Count > 0;
      this.watched[noteId] = new ClickyInkNoteStore.Watched(flag);
      if (flag)
      {
        this.turnsAwaitingTheirFirstEdit.Add(noteId);
      }
      this.StartWatchTimer();
    }

    // Token: 0x06001615 RID: 5653 RVA: 0x003BB5E0 File Offset: 0x003BB5E0
    public void NoteTurnEnded(string noteId)
    {
      if (!this.watched.Remove(noteId))
      {
        return;
      }
      this.turnsAwaitingTheirFirstEdit.Remove(noteId);
      Action<string> clearLiveSteps = ClickyInkNoteStore.ClearLiveSteps;
      if (clearLiveSteps != null)
      {
        clearLiveSteps(noteId);
      }
      if (this.watched.Count == 0)
      {
        this.StopWatchTimer();
      }
      ClickyLiveNoteStore.Shared.ReadNow(noteId);
      if (this.StartedAt(noteId) == null)
      {
        return;
      }
      double num = this.Clock(noteId, null);
      List<double> list;
      if (!this.finishTimes.TryGetValue(noteId, out list))
      {
        list = (this.finishTimes[noteId] = new List<double>());
      }
      list.Add(num);
      ClickyInkLiveNote clickyInkLiveNote;
      if (this.liveNotes.TryGetValue(noteId, out clickyInkLiveNote))
      {
        ClickyInkLiveNote clickyInkLiveNote2 = clickyInkLiveNote.Clone();
        clickyInkLiveNote2.Finish(num);
        this.liveNotes[noteId] = clickyInkLiveNote2;
        this.Bump();
      }
      ClickyInkLiveNote clickyInkLiveNote3;
      InkDispatch.After(Math.Max(0.0, (this.liveNotes.TryGetValue(noteId, out clickyInkLiveNote3) ? clickyInkLiveNote3.FinishedClock : num) - num) + 1.6 + 0.2, new Action(this.Bump));
    }

    // Token: 0x06001616 RID: 5654 RVA: 0x003BB70C File Offset: 0x003BB70C
    public void NoteActionSent()
    {
      this.Bump();
    }

    // Token: 0x06001617 RID: 5655 RVA: 0x003BB714 File Offset: 0x003BB714
    public void StartFreshPage(string noteId)
    {
      this.versionsByNoteId[noteId] = new List<ClickyInkNoteVersion>();
      this.lastDocumentByNoteId.Remove(noteId);
      this.liveNotes.Remove(noteId);
      this.settledNotes.Remove(noteId);
      this.finishTimes.Remove(noteId);
      this.lastReadStat.Remove(noteId);
      this.pendingStat.Remove(noteId);
      this.turnsAwaitingTheirFirstEdit.Remove(noteId);
      if (this.watched.ContainsKey(noteId))
      {
        this.watched[noteId] = new ClickyInkNoteStore.Watched(false);
      }
      this.freshPageNoteIds.Add(noteId);
      this.Save(noteId, new List<ClickyInkNoteVersion>());
      this.Bump();
    }

    // Token: 0x06001618 RID: 5656 RVA: 0x003BB7CC File Offset: 0x003BB7CC
    public void Forget(string noteId)
    {
      if (this.LoadVersions(noteId).Count == 0 && !this.liveNotes.ContainsKey(noteId) && !this.settledNotes.ContainsKey(noteId))
      {
        return;
      }
      this.versionsByNoteId[noteId] = new List<ClickyInkNoteVersion>();
      this.lastDocumentByNoteId.Remove(noteId);
      this.liveNotes.Remove(noteId);
      this.settledNotes.Remove(noteId);
      this.finishTimes.Remove(noteId);
      this.lastReadStat.Remove(noteId);
      this.pendingStat.Remove(noteId);
      this.freshPageNoteIds.Remove(noteId);
      this.Save(noteId, new List<ClickyInkNoteVersion>());
      this.Bump();
    }

    // Token: 0x06001619 RID: 5657 RVA: 0x003BB884 File Offset: 0x003BB884
    public bool IsTurnWriting(string noteId)
    {
      return this.watched.ContainsKey(noteId);
    }

    // Token: 0x0600161A RID: 5658 RVA: 0x003BB894 File Offset: 0x003BB894
    public void StreamPatch(string noteId, string kind, string diff)
    {
      ClickyInkNoteVersion clickyInkNoteVersion = this.LoadVersions(noteId).LastOrDefault<ClickyInkNoteVersion>((ClickyInkNoteVersion v) => !v.IsPartial.GetValueOrDefault());
      string text = ClickyInkPatchStream.PartialText(((clickyInkNoteVersion != null) ? clickyInkNoteVersion.Text : null) ?? "", kind, diff);
      if (text == null)
      {
        return;
      }
      ClickyInkNoteStore.Watched watched;
      this.RecordVersion(noteId, text, this.watched.TryGetValue(noteId, out watched) && watched.IsEdit, true, null);
    }

    // Token: 0x0600161B RID: 5659 RVA: 0x003BB91C File Offset: 0x003BB91C
    public void RecordSaved(string noteId, string text, DateTimeOffset modifiedAt)
    {
      ClickyInkNoteStore.Watched watched;
      if (this.watched.TryGetValue(noteId, out watched))
      {
        if (this.RecordVersion(noteId, text, watched.IsEdit, false, null))
        {
          ClickyInkNoteStore.WriteProblems(text, noteId);
        }
        return;
      }
      bool flag = this.LoadVersions(noteId).Count > 0;
      if (this.RecordVersion(noteId, text, flag, false, new DateTimeOffset?(flag ? DateTimeOffset.Now : modifiedAt)))
      {
        ClickyInkNoteStore.WriteProblems(text, noteId);
      }
    }

    // Token: 0x0600161C RID: 5660 RVA: 0x003BB990 File Offset: 0x003BB990
    public bool ReadNoteNow(string noteId, bool isEdit, DateTimeOffset? at = null)
    {
      ValueTuple<DateTimeOffset, long>? valueTuple = InkFiles.Stat(noteId);
      if (valueTuple != null)
      {
        ValueTuple<DateTimeOffset, long> valueOrDefault = valueTuple.GetValueOrDefault();
        if (valueOrDefault.Item2 <= 524288L)
        {
          string text = InkFiles.ReadUtf8(noteId);
          if (text != null)
          {
            this.lastReadStat[noteId] = valueOrDefault;
            this.pendingStat.Remove(noteId);
            return this.RecordVersion(noteId, text, isEdit, false, new DateTimeOffset?(at ?? DateTimeOffset.Now));
          }
        }
      }
      return false;
    }

    // Token: 0x0600161D RID: 5661 RVA: 0x003BBA14 File Offset: 0x003BBA14
    public bool RecordVersion(string noteId, string text, bool isEdit, bool isPartial = false, DateTimeOffset? at = null)
    {
      DateTimeOffset dateTimeOffset = at ?? DateTimeOffset.Now;
      if (this.ScopeKey == null || text.TrimWsNl().Length == 0)
      {
        return false;
      }
      List<ClickyInkNoteVersion> list = this.LoadVersions(noteId).ToList<ClickyInkNoteVersion>();
      if (list.Count > 0)
      {
        List<ClickyInkNoteVersion> list2 = list;
        if (list2[list2.Count - 1].Text == text)
        {
          if (!isPartial)
          {
            List<ClickyInkNoteVersion> list3 = list;
            if (list3[list3.Count - 1].IsPartial.GetValueOrDefault())
            {
              goto IL_0088;
            }
          }
          return false;
        }
      }
      IL_0088:
      if (isPartial && list.Count > 0)
      {
        List<ClickyInkNoteVersion> list4 = list;
        if (list4[list4.Count - 1].IsPartial.GetValueOrDefault())
        {
          DateTimeOffset dateTimeOffset2 = dateTimeOffset;
          List<ClickyInkNoteVersion> list5 = list;
          if ((dateTimeOffset2 - list5[list5.Count - 1].At).TotalSeconds < 0.45)
          {
            return false;
          }
        }
      }
      ClickyInkDocument clickyInkDocument = ClickyInkParser.Parse(text, isPartial);
      bool flag;
      if (!isPartial && list.Count > 0)
      {
        List<ClickyInkNoteVersion> list6 = list;
        flag = list6[list6.Count - 1].IsPartial.GetValueOrDefault();
      }
      else
      {
        flag = false;
      }
      bool flag2 = flag;
      if (list.Count > 0 && !flag2)
      {
        ClickyInkDocument clickyInkDocument2;
        ClickyInkDocument clickyInkDocument3;
        if (!this.lastDocumentByNoteId.TryGetValue(noteId, out clickyInkDocument2))
        {
          List<ClickyInkNoteVersion> list7 = list;
          clickyInkDocument3 = ClickyInkParser.Parse(list7[list7.Count - 1].Text, false);
        }
        else
        {
          clickyInkDocument3 = clickyInkDocument2;
        }
        if (clickyInkDocument3 == clickyInkDocument)
        {
          return false;
        }
      }
      bool flag3 = list.Count != 0 && isEdit;
      bool flag4 = flag3 && this.turnsAwaitingTheirFirstEdit.Remove(noteId);
      bool flag5 = false;
      if (!isPartial && list.Count > 1)
      {
        flag5 = (from v in list.Skip<ClickyInkNoteVersion>(1)
          where v.IsPartial.GetValueOrDefault()
          select v).ToList<ClickyInkNoteVersion>().Any<ClickyInkNoteVersion>((ClickyInkNoteVersion v) => v.StartsNewEdit.GetValueOrDefault());
        List<ClickyInkNoteVersion> list8 = new List<ClickyInkNoteVersion>();
        list8.Add(list[0]);
        list8.AddRange(from v in list.Skip<ClickyInkNoteVersion>(1)
          where !v.IsPartial.GetValueOrDefault()
          select v);
        list = list8;
      }
      DateTimeOffset dateTimeOffset3 = ((list.Count > 0) ? list[0].At : dateTimeOffset);
      DateTimeOffset dateTimeOffset4;
      if (list.Count <= 0)
      {
        dateTimeOffset4 = dateTimeOffset;
      }
      else
      {
        List<ClickyInkNoteVersion> list9 = list;
        dateTimeOffset4 = list9[list9.Count - 1].At;
      }
      DateTimeOffset dateTimeOffset5 = dateTimeOffset4;
      ClickyInkNoteVersion clickyInkNoteVersion = new ClickyInkNoteVersion((dateTimeOffset > dateTimeOffset5) ? dateTimeOffset : dateTimeOffset5, text, flag3, isPartial ? new bool?(true) : null, (flag4 || flag5) ? new bool?(true) : null);
      list.Add(clickyInkNoteVersion);
      if (list.Count > 240)
      {
        List<ClickyInkNoteVersion> list10 = new List<ClickyInkNoteVersion>();
        list10.Add(list[0]);
        list10.AddRange(list.Skip<ClickyInkNoteVersion>(list.Count - 239));
        list = list10;
      }
      this.versionsByNoteId[noteId] = list;
      this.lastDocumentByNoteId[noteId] = clickyInkDocument;
      this.freshPageNoteIds.Remove(noteId);
      ClickyInkLiveNote clickyInkLiveNote;
      if (this.liveNotes.TryGetValue(noteId, out clickyInkLiveNote))
      {
        ClickyInkLiveNote clickyInkLiveNote2 = clickyInkLiveNote.Clone();
        clickyInkLiveNote2.Apply(clickyInkDocument, (clickyInkNoteVersion.At - dateTimeOffset3).TotalSeconds, clickyInkNoteVersion.IsEdit, clickyInkNoteVersion.StartsNewEdit, isPartial);
        this.liveNotes[noteId] = clickyInkLiveNote2;
      }
      if (!isPartial)
      {
        this.Save(noteId, list);
      }
      this.Bump();
      return true;
    }

    // Token: 0x0600161E RID: 5662 RVA: 0x003BBDA4 File Offset: 0x003BBDA4
    private static void WriteProblems(string text, string notePath)
    {
      string problemsText = ClickyInkLint.FileText(ClickyInkLint.Problems(text));
      string path = Path.Combine(Path.GetDirectoryName(notePath) ?? "", "note.problems");
      ClickyInkNoteStore.ProblemsQueue.Async(delegate
      {
        if (problemsText != null)
        {
          InkFiles.WriteUtf8Atomically(path, problemsText);
          return;
        }
        if (File.Exists(path))
        {
          File.Delete(path);
        }
      });
    }

    // Token: 0x0600161F RID: 5663 RVA: 0x003BBE00 File Offset: 0x003BBE00
    public bool ToggleCheckbox(string noteId, string itemText)
    {
      string text = InkFiles.ReadUtf8(noteId);
      if (text != null)
      {
        string text2 = ClickyInkNoteStore.ToggledCheckbox(text, itemText);
        if (text2 != null)
        {
          try
          {
            InkFiles.WriteUtf8Atomically(noteId, text2);
          }
          catch (Exception ex)
          {
            Log.Warn("ink", "couldn't tick the box in " + noteId + ": " + ex.Message);
            return false;
          }
          ValueTuple<DateTimeOffset, long>? valueTuple = InkFiles.Stat(noteId);
          if (valueTuple != null)
          {
            ValueTuple<DateTimeOffset, long> valueOrDefault = valueTuple.GetValueOrDefault();
            this.lastReadStat[noteId] = valueOrDefault;
          }
          else
          {
            this.lastReadStat.Remove(noteId);
          }
          this.pendingStat.Remove(noteId);
          return this.RecordVersion(noteId, text2, true, false, null);
        }
      }
      return false;
    }

    // Token: 0x06001620 RID: 5664 RVA: 0x003BBEC0 File Offset: 0x003BBEC0
    [return: Nullable(2)]
    public static string ToggledCheckbox(string text, string itemText)
    {
      string text2 = ClickyInkNoteStore.<ToggledCheckbox>g__Plain|64_0(itemText);
      string[] array = text.Replace("\r\n", "\n").Split('\n', StringSplitOptions.None);
      bool flag = false;
      for (int i = 0; i < array.Length; i++)
      {
        string text3 = array[i];
        string text4 = text3.TrimWs();
        if (text4.HasPrefix("```"))
        {
          flag = !flag;
        }
        else if (!flag)
        {
          ClickyInkParser.ListStart listStart = ClickyInkParser.ListItemStart(text4);
          if (listStart != null && listStart.Kind == ClickyInkDocument.Kind.Checklist && !(ClickyInkNoteStore.<ToggledCheckbox>g__Plain|64_0(listStart.Text) != text2))
          {
            int num = -1;
            foreach (string text5 in new string[] { "[ ]", "[x]", "[X]" })
            {
              int num2 = text3.Find(text5);
              if (num2 >= 0 && (num < 0 || num2 < num))
              {
                num = num2;
              }
            }
            if (num >= 0)
            {
              bool flag2 = text3.Substring(num, 3) != "[ ]";
              string[] array3 = array;
              int num3 = i;
              string text6 = text3.Substring(0, num);
              string text7 = (flag2 ? "[ ]" : "[x]");
              string text8 = text3;
              int j = num + 3;
              array3[num3] = text6 + text7 + text8.Substring(j, text8.Length - j);
              return string.Join("\n", array);
            }
          }
        }
      }
      return null;
    }

    // Token: 0x06001621 RID: 5665 RVA: 0x003BC028 File Offset: 0x003BC028
    private void StartWatchTimer()
    {
      if (this.watchTimer != null)
      {
        return;
      }
      this.watchTimer = new InkDispatch.RepeatingTimer(0.4, new Action(this.WatchTick));
    }

    // Token: 0x06001622 RID: 5666 RVA: 0x003BC054 File Offset: 0x003BC054
    private void StopWatchTimer()
    {
      InkDispatch.RepeatingTimer repeatingTimer = this.watchTimer;
      if (repeatingTimer != null)
      {
        repeatingTimer.Dispose();
      }
      this.watchTimer = null;
    }

    // Token: 0x06001623 RID: 5667 RVA: 0x003BC070 File Offset: 0x003BC070
    private void WatchTick()
    {
      foreach (string text in this.watched.Keys.ToList<string>())
      {
        ValueTuple<DateTimeOffset, long>? valueTuple = InkFiles.Stat(text);
        if (valueTuple != null)
        {
          ValueTuple<DateTimeOffset, long> valueTuple2;
          if (this.lastReadStat.TryGetValue(text, out valueTuple2))
          {
            ValueTuple<DateTimeOffset, long> valueTuple3 = valueTuple2;
            ValueTuple<DateTimeOffset, long> valueTuple4 = valueTuple.Value;
            if (valueTuple3.Item1 == valueTuple4.Item1 && valueTuple3.Item2 == valueTuple4.Item2)
            {
              goto IL_0074;
            }
          }
          ValueTuple<DateTimeOffset, long> valueTuple5;
          if (this.pendingStat.TryGetValue(text, out valueTuple5))
          {
            ValueTuple<DateTimeOffset, long> valueTuple4 = valueTuple5;
            ValueTuple<DateTimeOffset, long> valueTuple3 = valueTuple.Value;
            if (!(valueTuple4.Item1 != valueTuple3.Item1) && valueTuple4.Item2 == valueTuple3.Item2)
            {
              this.lastReadStat[text] = valueTuple.Value;
              this.pendingStat.Remove(text);
              if (valueTuple.Value.Item2 <= 524288L)
              {
                ClickyLiveNoteStore.Shared.ReadNow(text);
                continue;
              }
              continue;
            }
          }
          this.pendingStat[text] = valueTuple.Value;
          continue;
        }
        IL_0074:
        this.pendingStat.Remove(text);
      }
    }

    // Token: 0x1700050F RID: 1295
    // (get) Token: 0x06001624 RID: 5668 RVA: 0x003BC1D4 File Offset: 0x003BC1D4
    [Nullable(2)]
    private string BaseDirectory
    {
      [NullableContext(2)]
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

    // Token: 0x06001625 RID: 5669 RVA: 0x003BC1F4 File Offset: 0x003BC1F4
    [NullableContext(2)]
    public static string ScopeDirectory(string baseDirectory, [Nullable(1)] string scopeKey)
    {
      if (baseDirectory != null)
      {
        return Path.Combine(baseDirectory, scopeKey);
      }
      return null;
    }

    // Token: 0x06001626 RID: 5670 RVA: 0x003BC204 File Offset: 0x003BC204
    private static string FileNameForNoteId(string noteId)
    {
      return string.Concat(noteId.Characters().Select<string, string>(delegate(string c)
      {
        if (!InkText.IsLetter(c) && !InkText.IsNumber(c) && !(c == "-"))
        {
          return "_";
        }
        return c;
      })).SuffixCharacters(200) + ".json";
    }

    // Token: 0x06001627 RID: 5671 RVA: 0x003BC254 File Offset: 0x003BC254
    [return: Nullable(2)]
    public string VersionsFilePath(string noteId)
    {
      string scopeKey = this.ScopeKey;
      if (scopeKey == null)
      {
        return null;
      }
      string text = ClickyInkNoteStore.ScopeDirectory(this.BaseDirectory, scopeKey);
      if (text == null)
      {
        return null;
      }
      return Path.Combine(text, ClickyInkNoteStore.FileNameForNoteId(noteId));
    }

    // Token: 0x06001628 RID: 5672 RVA: 0x003BC28C File Offset: 0x003BC28C
    public async Task PreloadVersions(IEnumerable<string> noteIds)
    {
      string scope = this.ScopeKey;
      if (scope != null)
      {
        List<ValueTuple<string, string>> files = (from id in noteIds
          where !this.versionsByNoteId.ContainsKey(id)
          select new ValueTuple<string, string>(id, this.VersionsFilePath(id)) into f
          where f.Item2 != null
          select f).ToList<ValueTuple<string, string>>();
        if (files.Count != 0)
        {
          List<ValueTuple<string, List<ClickyInkNoteVersion>>> list = await Task.Run<List<ValueTuple<string, List<ClickyInkNoteVersion>>>>(() => files.Select<ValueTuple<string, string>, ValueTuple<string, List<ClickyInkNoteVersion>>>(([TupleElementNames(new string[] { "Id", "Path" })] ValueTuple<string, string> f) => new ValueTuple<string, List<ClickyInkNoteVersion>>(f.Item1, ClickyInkNoteStore.ReadVersionsFile(f.Item2))).ToList<ValueTuple<string, List<ClickyInkNoteVersion>>>());
          if (!(this.ScopeKey != scope))
          {
            foreach (ValueTuple<string, List<ClickyInkNoteVersion>> valueTuple in list)
            {
              string item = valueTuple.Item1;
              List<ClickyInkNoteVersion> item2 = valueTuple.Item2;
              this.versionsByNoteId.TryAdd(item, item2);
            }
          }
        }
      }
    }

    // Token: 0x06001629 RID: 5673 RVA: 0x003BC2D8 File Offset: 0x003BC2D8
    private List<ClickyInkNoteVersion> LoadVersions(string noteId)
    {
      List<ClickyInkNoteVersion> list;
      if (this.versionsByNoteId.TryGetValue(noteId, out list))
      {
        return list;
      }
      string text = this.VersionsFilePath(noteId);
      List<ClickyInkNoteVersion> list2 = ((text != null) ? ClickyInkNoteStore.ReadVersionsFile(text) : new List<ClickyInkNoteVersion>());
      if (this.ScopeKey != null)
      {
        this.versionsByNoteId[noteId] = list2;
      }
      return list2;
    }

    // Token: 0x0600162A RID: 5674 RVA: 0x003BC328 File Offset: 0x003BC328
    private static List<ClickyInkNoteVersion> ReadVersionsFile(string path)
    {
      List<ClickyInkNoteVersion> list;
      try
      {
        if (!File.Exists(path))
        {
          list = new List<ClickyInkNoteVersion>();
        }
        else
        {
          InkNoteSnapshot inkNoteSnapshot = JsonSerializer.Deserialize<InkNoteSnapshot>(File.ReadAllBytes(path), InkJsonContext.Default.InkNoteSnapshot);
          List<ClickyInkNoteVersion> list2;
          if (inkNoteSnapshot == null)
          {
            list2 = null;
          }
          else
          {
            List<InkNoteVersionFile> versions = inkNoteSnapshot.Versions;
            if (versions == null)
            {
              list2 = null;
            }
            else
            {
              list2 = versions.Select<InkNoteVersionFile, ClickyInkNoteVersion>((InkNoteVersionFile v) => v.ToVersion()).ToList<ClickyInkNoteVersion>();
            }
          }
          list = list2 ?? new List<ClickyInkNoteVersion>();
        }
      }
      catch (Exception)
      {
        list = new List<ClickyInkNoteVersion>();
      }
      return list;
    }

    // Token: 0x0600162B RID: 5675 RVA: 0x003BC3C4 File Offset: 0x003BC3C4
    private void Save(string noteId, List<ClickyInkNoteVersion> versions)
    {
      string path = this.VersionsFilePath(noteId);
      if (path == null)
      {
        return;
      }
      List<ClickyInkNoteVersion> kept = versions.Where<ClickyInkNoteVersion>((ClickyInkNoteVersion v, int index) => index == 0 || !v.IsPartial.GetValueOrDefault()).ToList<ClickyInkNoteVersion>();
      this.writeQueue.Async(delegate
      {
        InkNoteSnapshot inkNoteSnapshot = new InkNoteSnapshot();
        inkNoteSnapshot.Version = 1;
        IEnumerable<ClickyInkNoteVersion> kept2 = kept;
        Func<ClickyInkNoteVersion, InkNoteVersionFile> func;
        if ((func = ClickyInkNoteStore.<>O.<0>__From) == null)
        {
          func = (ClickyInkNoteStore.<>O.<0>__From = new Func<ClickyInkNoteVersion, InkNoteVersionFile>(InkNoteVersionFile.From));
        }
        inkNoteSnapshot.Versions = kept2.Select<ClickyInkNoteVersion, InkNoteVersionFile>(func).ToList<InkNoteVersionFile>();
        byte[] array = JsonSerializer.SerializeToUtf8Bytes<InkNoteSnapshot>(inkNoteSnapshot, InkJsonContext.Default.InkNoteSnapshot);
        try
        {
          InkFiles.WriteBytesAtomically(path, array);
        }
        catch (Exception ex)
        {
          Log.Warn("ink", "couldn't save " + noteId + "'s versions: " + ex.Message);
        }
      });
    }

    // Token: 0x0600162C RID: 5676 RVA: 0x003BC440 File Offset: 0x003BC440
    public void WaitForPendingSaves()
    {
      this.writeQueue.Sync();
      ClickyInkNoteStore.ProblemsQueue.Sync();
    }

    // Token: 0x0600162E RID: 5678 RVA: 0x003BC470 File Offset: 0x003BC470
    [CompilerGenerated]
    internal static string <ToggledCheckbox>g__Plain|64_0(string words)
    {
      StringBuilder stringBuilder = new StringBuilder(words.Length);
      foreach (Rune rune in words.EnumerateRunes())
      {
        int value = rune.Value;
        bool flag = value < 57344 || value > 63743;
        if (flag)
        {
          stringBuilder.Append(rune.ToString());
        }
      }
      return stringBuilder.ToString().TrimWs();
    }

    // Token: 0x04000998 RID: 2456
    public const int MaximumVersions = 240;

    // Token: 0x04000999 RID: 2457
    public const int MaximumNoteBytes = 524288;

    // Token: 0x0400099A RID: 2458
    public const double WatchInterval = 0.4;

    // Token: 0x0400099B RID: 2459
    public const double SettleSeconds = 1.6;

    // Token: 0x0400099C RID: 2460
    public const double SurelySettledAfterSeconds = 3600.0;

    // Token: 0x0400099E RID: 2462
    [Nullable(2)]
    private readonly string baseDirectoryOverride;

    // Token: 0x0400099F RID: 2463
    private readonly Dictionary<string, List<ClickyInkNoteVersion>> versionsByNoteId = new Dictionary<string, List<ClickyInkNoteVersion>>();

    // Token: 0x040009A0 RID: 2464
    private readonly Dictionary<string, ClickyInkDocument> lastDocumentByNoteId = new Dictionary<string, ClickyInkDocument>();

    // Token: 0x040009A1 RID: 2465
    private readonly Dictionary<string, ClickyInkLiveNote> liveNotes = new Dictionary<string, ClickyInkLiveNote>();

    // Token: 0x040009A2 RID: 2466
    [TupleElementNames(new string[] { "Key", "Note" })]
    [Nullable(new byte[] { 1, 1, 0, 1, 1 })]
    private readonly Dictionary<string, ValueTuple<string, ClickyInkLiveNote>> settledNotes = new Dictionary<string, ValueTuple<string, ClickyInkLiveNote>>();

    // Token: 0x040009A3 RID: 2467
    private readonly HashSet<string> keptNoteIds = new HashSet<string>();

    // Token: 0x040009A4 RID: 2468
    private readonly InkDispatch.SerialQueue writeQueue = new InkDispatch.SerialQueue();

    // Token: 0x040009A5 RID: 2469
    private readonly Dictionary<string, ClickyInkNoteStore.Watched> watched = new Dictionary<string, ClickyInkNoteStore.Watched>();

    // Token: 0x040009A6 RID: 2470
    private readonly Dictionary<string, List<double>> finishTimes = new Dictionary<string, List<double>>();

    // Token: 0x040009A7 RID: 2471
    private readonly HashSet<string> turnsAwaitingTheirFirstEdit = new HashSet<string>();

    // Token: 0x040009A8 RID: 2472
    private readonly HashSet<string> freshPageNoteIds = new HashSet<string>();

    // Token: 0x040009A9 RID: 2473
    [TupleElementNames(new string[] { "ModifiedAt", "Size" })]
    [Nullable(new byte[] { 1, 1, 0 })]
    private readonly Dictionary<string, ValueTuple<DateTimeOffset, long>> lastReadStat = new Dictionary<string, ValueTuple<DateTimeOffset, long>>();

    // Token: 0x040009AA RID: 2474
    [TupleElementNames(new string[] { "ModifiedAt", "Size" })]
    [Nullable(new byte[] { 1, 1, 0 })]
    private readonly Dictionary<string, ValueTuple<DateTimeOffset, long>> pendingStat = new Dictionary<string, ValueTuple<DateTimeOffset, long>>();

    // Token: 0x040009AB RID: 2475
    [Nullable(2)]
    private InkDispatch.RepeatingTimer watchTimer;

    // Token: 0x040009AC RID: 2476
    private static readonly InkDispatch.SerialQueue ProblemsQueue = new InkDispatch.SerialQueue();

    // Token: 0x0200052C RID: 1324
    [Nullable(0)]
    private sealed class Watched : IEquatable<ClickyInkNoteStore.Watched>
    {
      // Token: 0x06003309 RID: 13065 RVA: 0x0043CC40 File Offset: 0x0043CC40
      public Watched(bool IsEdit)
      {
        this.IsEdit = IsEdit;
        base..ctor();
      }

      // Token: 0x1700092E RID: 2350
      // (get) Token: 0x0600330A RID: 13066 RVA: 0x0043CC50 File Offset: 0x0043CC50
      [CompilerGenerated]
      private Type EqualityContract
      {
        [CompilerGenerated]
        get
        {
          return typeof(ClickyInkNoteStore.Watched);
        }
      }

      // Token: 0x1700092F RID: 2351
      // (get) Token: 0x0600330B RID: 13067 RVA: 0x0043CC5C File Offset: 0x0043CC5C
      // (set) Token: 0x0600330C RID: 13068 RVA: 0x0043CC64 File Offset: 0x0043CC64
      public bool IsEdit { get; set; }

      // Token: 0x0600330D RID: 13069 RVA: 0x0043CC70 File Offset: 0x0043CC70
      [CompilerGenerated]
      public override string ToString()
      {
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.Append("Watched");
        stringBuilder.Append(" { ");
        if (this.PrintMembers(stringBuilder))
        {
          stringBuilder.Append(' ');
        }
        stringBuilder.Append('}');
        return stringBuilder.ToString();
      }

      // Token: 0x0600330E RID: 13070 RVA: 0x0043CCBC File Offset: 0x0043CCBC
      [CompilerGenerated]
      private bool PrintMembers(StringBuilder builder)
      {
        RuntimeHelpers.EnsureSufficientExecutionStack();
        builder.Append("IsEdit = ");
        builder.Append(this.IsEdit.ToString());
        return true;
      }

      // Token: 0x0600330F RID: 13071 RVA: 0x0043CCF8 File Offset: 0x0043CCF8
      [NullableContext(2)]
      [CompilerGenerated]
      public static bool operator !=(ClickyInkNoteStore.Watched left, ClickyInkNoteStore.Watched right)
      {
        return !(left == right);
      }

      // Token: 0x06003310 RID: 13072 RVA: 0x0043CD04 File Offset: 0x0043CD04
      [NullableContext(2)]
      [CompilerGenerated]
      public static bool operator ==(ClickyInkNoteStore.Watched left, ClickyInkNoteStore.Watched right)
      {
        return left == right || (left != null && left.Equals(right));
      }

      // Token: 0x06003311 RID: 13073 RVA: 0x0043CD18 File Offset: 0x0043CD18
      [CompilerGenerated]
      public override int GetHashCode()
      {
        return EqualityComparer<Type>.Default.GetHashCode(this.EqualityContract) * -1521134295 + EqualityComparer<bool>.Default.GetHashCode(this.<IsEdit>k__BackingField);
      }

      // Token: 0x06003312 RID: 13074 RVA: 0x0043CD44 File Offset: 0x0043CD44
      [NullableContext(2)]
      [CompilerGenerated]
      public override bool Equals(object obj)
      {
        return this.Equals(obj as ClickyInkNoteStore.Watched);
      }

      // Token: 0x06003313 RID: 13075 RVA: 0x0043CD54 File Offset: 0x0043CD54
      [NullableContext(2)]
      [CompilerGenerated]
      public bool Equals(ClickyInkNoteStore.Watched other)
      {
        return this == other || (other != null && this.EqualityContract == other.EqualityContract && EqualityComparer<bool>.Default.Equals(this.<IsEdit>k__BackingField, other.<IsEdit>k__BackingField));
      }

      // Token: 0x06003315 RID: 13077 RVA: 0x0043CD94 File Offset: 0x0043CD94
      [CompilerGenerated]
      private Watched(ClickyInkNoteStore.Watched original)
      {
        this.IsEdit = original.<IsEdit>k__BackingField;
      }

      // Token: 0x06003316 RID: 13078 RVA: 0x0043CDA8 File Offset: 0x0043CDA8
      [CompilerGenerated]
      public void Deconstruct(out bool IsEdit)
      {
        IsEdit = this.IsEdit;
      }
    }

    // Token: 0x0200052D RID: 1325
    [CompilerGenerated]
    private static class <>O
    {
      // Token: 0x04001F96 RID: 8086
      [Nullable(0)]
      public static Func<ClickyInkNoteVersion, InkNoteVersionFile> <0>__From;
    }
  }
}
