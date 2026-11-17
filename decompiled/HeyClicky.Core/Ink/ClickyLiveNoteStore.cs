using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

namespace HeyClicky.Core.Ink
{
  // Token: 0x02000187 RID: 391
  [NullableContext(1)]
  [Nullable(0)]
  public sealed class ClickyLiveNoteStore
  {
    // Token: 0x17000551 RID: 1361
    // (get) Token: 0x0600175C RID: 5980 RVA: 0x003C340C File Offset: 0x003C340C
    public static ClickyLiveNoteStore Shared { get; } = new ClickyLiveNoteStore();

    // Token: 0x17000552 RID: 1362
    // (get) Token: 0x0600175D RID: 5981 RVA: 0x003C3414 File Offset: 0x003C3414
    public IReadOnlyDictionary<string, ClickyLiveNoteStore.Note> Notes
    {
      get
      {
        return this.notes;
      }
    }

    // Token: 0x14000035 RID: 53
    // (add) Token: 0x0600175E RID: 5982 RVA: 0x003C341C File Offset: 0x003C341C
    // (remove) Token: 0x0600175F RID: 5983 RVA: 0x003C3454 File Offset: 0x003C3454
    [Nullable(2)]
    [method: NullableContext(2)]
    [field: Nullable(2)]
    public event Action Changed;

    // Token: 0x06001760 RID: 5984 RVA: 0x003C348C File Offset: 0x003C348C
    private ClickyLiveNoteStore()
    {
    }

    // Token: 0x06001761 RID: 5985 RVA: 0x003C34CC File Offset: 0x003C34CC
    private void SetNote(string path, ClickyLiveNoteStore.Note note)
    {
      ClickyLiveNoteStore.Note note2;
      if (this.notes.TryGetValue(path, out note2) && note2 == note)
      {
        return;
      }
      this.notes[path] = note;
      Action changed = this.Changed;
      if (changed == null)
      {
        return;
      }
      changed();
    }

    // Token: 0x06001762 RID: 5986 RVA: 0x003C3510 File Offset: 0x003C3510
    public void BeginShowing(string path)
    {
      int num;
      this.shownCounts[path] = (this.shownCounts.TryGetValue(path, out num) ? num : 0) + 1;
      this.ReadNow(path);
      this.StartPollingIfNeeded();
    }

    // Token: 0x06001763 RID: 5987 RVA: 0x003C354C File Offset: 0x003C354C
    public void EndShowing(string path)
    {
      int num;
      if (!this.shownCounts.TryGetValue(path, out num))
      {
        return;
      }
      if (num <= 1)
      {
        this.shownCounts.Remove(path);
      }
      else
      {
        this.shownCounts[path] = num - 1;
      }
      if (this.shownCounts.Count == 0)
      {
        InkDispatch.RepeatingTimer repeatingTimer = this.pollTimer;
        if (repeatingTimer != null)
        {
          repeatingTimer.Dispose();
        }
        this.pollTimer = null;
      }
    }

    // Token: 0x06001764 RID: 5988 RVA: 0x003C35B4 File Offset: 0x003C35B4
    public void ReadNow(string path)
    {
      ValueTuple<DateTimeOffset, long>? valueTuple = InkFiles.Stat(path);
      string text = ((valueTuple == null) ? null : InkFiles.ReadUtf8(path));
      if (valueTuple != null)
      {
        ValueTuple<DateTimeOffset, long> valueOrDefault = valueTuple.GetValueOrDefault();
        if (text != null)
        {
          if (valueOrDefault.Item2 > 524288L)
          {
            return;
          }
          this.savedStats[path] = valueOrDefault;
          this.savedTexts[path] = text;
          this.pendingStreamedTexts.Remove(path);
          this.SetNote(path, new ClickyLiveNoteStore.Note(text, false, new DateTimeOffset?(valueOrDefault.Item1)));
          ClickyInkNoteStore.Shared.RecordSaved(path, text, valueOrDefault.Item1);
          return;
        }
      }
      if (this.notes.ContainsKey(path) && !File.Exists(path))
      {
        this.notes.Remove(path);
        this.savedTexts.Remove(path);
        this.savedStats.Remove(path);
        Action changed = this.Changed;
        if (changed == null)
        {
          return;
        }
        changed();
      }
    }

    // Token: 0x06001765 RID: 5989 RVA: 0x003C36A0 File Offset: 0x003C36A0
    public void StreamPatch(string path, string kind, string diff)
    {
      if (!this.savedTexts.ContainsKey(path))
      {
        this.ReadNow(path);
      }
      ClickyInkNoteStore.Shared.StreamPatch(path, kind, diff);
      string text2;
      string text = ClickyInkPatchStream.PartialText(this.savedTexts.TryGetValue(path, out text2) ? text2 : "", kind, diff);
      if (text == null)
      {
        return;
      }
      this.pendingStreamedTexts[path] = text;
      if (this.streamPublishScheduled)
      {
        return;
      }
      this.streamPublishScheduled = true;
      InkDispatch.After(0.12, new Action(this.PublishStreamedTexts));
    }

    // Token: 0x06001766 RID: 5990 RVA: 0x003C372C File Offset: 0x003C372C
    private void PublishStreamedTexts()
    {
      this.streamPublishScheduled = false;
      foreach (KeyValuePair<string, string> keyValuePair in this.pendingStreamedTexts.ToList<KeyValuePair<string, string>>())
      {
        string text;
        string text2;
        keyValuePair.Deconstruct(out text, out text2);
        string text3 = text;
        string text4 = text2;
        ClickyLiveNoteStore.Note note;
        this.SetNote(text3, new ClickyLiveNoteStore.Note(text4, true, this.notes.TryGetValue(text3, out note) ? note.SavedAt : null));
      }
      this.pendingStreamedTexts.Clear();
    }

    // Token: 0x06001767 RID: 5991 RVA: 0x003C37D4 File Offset: 0x003C37D4
    private void StartPollingIfNeeded()
    {
      if (this.pollTimer != null || this.shownCounts.Count == 0)
      {
        return;
      }
      this.pollTimer = new InkDispatch.RepeatingTimer(0.5, new Action(this.PollShownNotes));
    }

    // Token: 0x06001768 RID: 5992 RVA: 0x003C380C File Offset: 0x003C380C
    private void PollShownNotes()
    {
      foreach (string text in this.shownCounts.Keys.ToList<string>())
      {
        ValueTuple<DateTimeOffset, long>? valueTuple = InkFiles.Stat(text);
        ValueTuple<DateTimeOffset, long> valueTuple3;
        ValueTuple<DateTimeOffset, long>? valueTuple2 = (this.savedStats.TryGetValue(text, out valueTuple3) ? new ValueTuple<DateTimeOffset, long>?(valueTuple3) : null);
        DateTimeOffset? dateTimeOffset = ((valueTuple != null) ? new DateTimeOffset?(valueTuple.GetValueOrDefault().Item1) : null);
        if (!(dateTimeOffset != ((valueTuple2 != null) ? new DateTimeOffset?(valueTuple2.GetValueOrDefault().Item1) : null)))
        {
          long? num = ((valueTuple != null) ? new long?(valueTuple.GetValueOrDefault().Item2) : null);
          long? num2 = ((valueTuple2 != null) ? new long?(valueTuple2.GetValueOrDefault().Item2) : null);
          if ((num.GetValueOrDefault() == num2.GetValueOrDefault()) & (num != null == (num2 != null)))
          {
            continue;
          }
        }
        this.ReadNow(text);
      }
    }

    // Token: 0x06001769 RID: 5993 RVA: 0x003C39A0 File Offset: 0x003C39A0
    public void RemoveAllForSignOut()
    {
      this.notes.Clear();
      this.savedTexts.Clear();
      this.savedStats.Clear();
      this.pendingStreamedTexts.Clear();
      Action changed = this.Changed;
      if (changed != null)
      {
        changed();
      }
      ClickyInkNoteStore.Shared.RemoveAllForSignOut();
      ClickyInkActionsStore.Shared.RemoveAllForSignOut();
      ClickyInkNotePages.Shared.RemoveAllForSignOut();
    }

    // Token: 0x04000A17 RID: 2583
    private readonly Dictionary<string, ClickyLiveNoteStore.Note> notes = new Dictionary<string, ClickyLiveNoteStore.Note>();

    // Token: 0x04000A19 RID: 2585
    private readonly Dictionary<string, string> savedTexts = new Dictionary<string, string>();

    // Token: 0x04000A1A RID: 2586
    [TupleElementNames(new string[] { "ModifiedAt", "Size" })]
    [Nullable(new byte[] { 1, 1, 0 })]
    private readonly Dictionary<string, ValueTuple<DateTimeOffset, long>> savedStats = new Dictionary<string, ValueTuple<DateTimeOffset, long>>();

    // Token: 0x04000A1B RID: 2587
    private readonly Dictionary<string, int> shownCounts = new Dictionary<string, int>();

    // Token: 0x04000A1C RID: 2588
    [Nullable(2)]
    private InkDispatch.RepeatingTimer pollTimer;

    // Token: 0x04000A1D RID: 2589
    private readonly Dictionary<string, string> pendingStreamedTexts = new Dictionary<string, string>();

    // Token: 0x04000A1E RID: 2590
    private bool streamPublishScheduled;

    // Token: 0x04000A1F RID: 2591
    private const double PollInterval = 0.5;

    // Token: 0x04000A20 RID: 2592
    private const double StreamPublishInterval = 0.12;

    // Token: 0x02000565 RID: 1381
    [Nullable(0)]
    public sealed class Note : IEquatable<ClickyLiveNoteStore.Note>
    {
      // Token: 0x060034B1 RID: 13489 RVA: 0x0044060C File Offset: 0x0044060C
      public Note(string Text, bool IsBeingWritten, DateTimeOffset? SavedAt)
      {
        this.Text = Text;
        this.IsBeingWritten = IsBeingWritten;
        this.SavedAt = SavedAt;
        base..ctor();
      }

      // Token: 0x17000958 RID: 2392
      // (get) Token: 0x060034B2 RID: 13490 RVA: 0x0044062C File Offset: 0x0044062C
      [CompilerGenerated]
      private Type EqualityContract
      {
        [CompilerGenerated]
        get
        {
          return typeof(ClickyLiveNoteStore.Note);
        }
      }

      // Token: 0x17000959 RID: 2393
      // (get) Token: 0x060034B3 RID: 13491 RVA: 0x00440638 File Offset: 0x00440638
      // (set) Token: 0x060034B4 RID: 13492 RVA: 0x00440640 File Offset: 0x00440640
      public string Text { get; set; }

      // Token: 0x1700095A RID: 2394
      // (get) Token: 0x060034B5 RID: 13493 RVA: 0x0044064C File Offset: 0x0044064C
      // (set) Token: 0x060034B6 RID: 13494 RVA: 0x00440654 File Offset: 0x00440654
      public bool IsBeingWritten { get; set; }

      // Token: 0x1700095B RID: 2395
      // (get) Token: 0x060034B7 RID: 13495 RVA: 0x00440660 File Offset: 0x00440660
      // (set) Token: 0x060034B8 RID: 13496 RVA: 0x00440668 File Offset: 0x00440668
      public DateTimeOffset? SavedAt { get; set; }

      // Token: 0x060034B9 RID: 13497 RVA: 0x00440674 File Offset: 0x00440674
      [CompilerGenerated]
      public override string ToString()
      {
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.Append("Note");
        stringBuilder.Append(" { ");
        if (this.PrintMembers(stringBuilder))
        {
          stringBuilder.Append(' ');
        }
        stringBuilder.Append('}');
        return stringBuilder.ToString();
      }

      // Token: 0x060034BA RID: 13498 RVA: 0x004406C0 File Offset: 0x004406C0
      [CompilerGenerated]
      private bool PrintMembers(StringBuilder builder)
      {
        RuntimeHelpers.EnsureSufficientExecutionStack();
        builder.Append("Text = ");
        builder.Append(this.Text);
        builder.Append(", IsBeingWritten = ");
        builder.Append(this.IsBeingWritten.ToString());
        builder.Append(", SavedAt = ");
        builder.Append(this.SavedAt.ToString());
        return true;
      }

      // Token: 0x060034BB RID: 13499 RVA: 0x0044073C File Offset: 0x0044073C
      [NullableContext(2)]
      [CompilerGenerated]
      public static bool operator !=(ClickyLiveNoteStore.Note left, ClickyLiveNoteStore.Note right)
      {
        return !(left == right);
      }

      // Token: 0x060034BC RID: 13500 RVA: 0x00440748 File Offset: 0x00440748
      [NullableContext(2)]
      [CompilerGenerated]
      public static bool operator ==(ClickyLiveNoteStore.Note left, ClickyLiveNoteStore.Note right)
      {
        return left == right || (left != null && left.Equals(right));
      }

      // Token: 0x060034BD RID: 13501 RVA: 0x0044075C File Offset: 0x0044075C
      [CompilerGenerated]
      public override int GetHashCode()
      {
        return ((EqualityComparer<Type>.Default.GetHashCode(this.EqualityContract) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Text>k__BackingField)) * -1521134295 + EqualityComparer<bool>.Default.GetHashCode(this.<IsBeingWritten>k__BackingField)) * -1521134295 + EqualityComparer<DateTimeOffset?>.Default.GetHashCode(this.<SavedAt>k__BackingField);
      }

      // Token: 0x060034BE RID: 13502 RVA: 0x004407C0 File Offset: 0x004407C0
      [NullableContext(2)]
      [CompilerGenerated]
      public override bool Equals(object obj)
      {
        return this.Equals(obj as ClickyLiveNoteStore.Note);
      }

      // Token: 0x060034BF RID: 13503 RVA: 0x004407D0 File Offset: 0x004407D0
      [NullableContext(2)]
      [CompilerGenerated]
      public bool Equals(ClickyLiveNoteStore.Note other)
      {
        return this == other || (other != null && this.EqualityContract == other.EqualityContract && EqualityComparer<string>.Default.Equals(this.<Text>k__BackingField, other.<Text>k__BackingField) && EqualityComparer<bool>.Default.Equals(this.<IsBeingWritten>k__BackingField, other.<IsBeingWritten>k__BackingField) && EqualityComparer<DateTimeOffset?>.Default.Equals(this.<SavedAt>k__BackingField, other.<SavedAt>k__BackingField));
      }

      // Token: 0x060034C1 RID: 13505 RVA: 0x0044084C File Offset: 0x0044084C
      [CompilerGenerated]
      private Note(ClickyLiveNoteStore.Note original)
      {
        this.Text = original.<Text>k__BackingField;
        this.IsBeingWritten = original.<IsBeingWritten>k__BackingField;
        this.SavedAt = original.<SavedAt>k__BackingField;
      }

      // Token: 0x060034C2 RID: 13506 RVA: 0x00440878 File Offset: 0x00440878
      [CompilerGenerated]
      public void Deconstruct(out string Text, out bool IsBeingWritten, out DateTimeOffset? SavedAt)
      {
        Text = this.Text;
        IsBeingWritten = this.IsBeingWritten;
        SavedAt = this.SavedAt;
      }
    }
  }
}
