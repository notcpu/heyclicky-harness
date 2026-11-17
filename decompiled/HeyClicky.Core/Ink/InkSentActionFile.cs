using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text.Json.Serialization;

namespace HeyClicky.Core.Ink
{
  // Token: 0x02000129 RID: 297
  [NullableContext(1)]
  [Nullable(0)]
  public sealed class InkSentActionFile
  {
    // Token: 0x17000485 RID: 1157
    // (get) Token: 0x06001359 RID: 4953 RVA: 0x003A6734 File Offset: 0x003A6734
    // (set) Token: 0x0600135A RID: 4954 RVA: 0x003A673C File Offset: 0x003A673C
    [JsonPropertyName("kind")]
    public string Kind { get; set; } = "ask";

    // Token: 0x17000486 RID: 1158
    // (get) Token: 0x0600135B RID: 4955 RVA: 0x003A6748 File Offset: 0x003A6748
    // (set) Token: 0x0600135C RID: 4956 RVA: 0x003A6750 File Offset: 0x003A6750
    [JsonPropertyName("objectKey")]
    public string ObjectKey { get; set; } = "";

    // Token: 0x17000487 RID: 1159
    // (get) Token: 0x0600135D RID: 4957 RVA: 0x003A675C File Offset: 0x003A675C
    // (set) Token: 0x0600135E RID: 4958 RVA: 0x003A6764 File Offset: 0x003A6764
    [JsonPropertyName("marginWords")]
    public string MarginWords { get; set; } = "";

    // Token: 0x17000488 RID: 1160
    // (get) Token: 0x0600135F RID: 4959 RVA: 0x003A6770 File Offset: 0x003A6770
    // (set) Token: 0x06001360 RID: 4960 RVA: 0x003A6778 File Offset: 0x003A6778
    [JsonPropertyName("sentAt")]
    public double SentAt { get; set; }

    // Token: 0x17000489 RID: 1161
    // (get) Token: 0x06001361 RID: 4961 RVA: 0x003A6784 File Offset: 0x003A6784
    // (set) Token: 0x06001362 RID: 4962 RVA: 0x003A678C File Offset: 0x003A678C
    [JsonPropertyName("wasSentWhileRunning")]
    public bool WasSentWhileRunning { get; set; }

    // Token: 0x1700048A RID: 1162
    // (get) Token: 0x06001363 RID: 4963 RVA: 0x003A6798 File Offset: 0x003A6798
    // (set) Token: 0x06001364 RID: 4964 RVA: 0x003A67A0 File Offset: 0x003A67A0
    [Nullable(new byte[] { 2, 1 })]
    [JsonPropertyName("reviewedItems")]
    public List<string> ReviewedItems
    {
      [return: Nullable(new byte[] { 2, 1 })]
      get;
      [param: Nullable(new byte[] { 2, 1 })]
      set;
    }

    // Token: 0x06001365 RID: 4965 RVA: 0x003A67AC File Offset: 0x003A67AC
    public ClickyInkSentAction ToAction()
    {
      string kind = this.Kind;
      ClickyInkNoteAction.ActionKind actionKind;
      if (!(kind == "answer"))
      {
        if (!(kind == "send"))
        {
          if (!(kind == "review"))
          {
            actionKind = ClickyInkNoteAction.ActionKind.Ask;
          }
          else
          {
            actionKind = ClickyInkNoteAction.ActionKind.Review;
          }
        }
        else
        {
          actionKind = ClickyInkNoteAction.ActionKind.Send;
        }
      }
      else
      {
        actionKind = ClickyInkNoteAction.ActionKind.Answer;
      }
      return new ClickyInkSentAction(actionKind, this.ObjectKey, this.MarginWords, InkFiles.FromSecondsSince1970(this.SentAt), this.WasSentWhileRunning, this.ReviewedItems);
    }

    // Token: 0x06001366 RID: 4966 RVA: 0x003A6820 File Offset: 0x003A6820
    public static InkSentActionFile From(ClickyInkSentAction action)
    {
      InkSentActionFile inkSentActionFile = new InkSentActionFile();
      inkSentActionFile.Kind = action.Kind.ToString().ToLowerInvariant();
      inkSentActionFile.ObjectKey = action.ObjectKey;
      inkSentActionFile.MarginWords = action.MarginWords;
      inkSentActionFile.SentAt = InkFiles.SecondsSince1970(action.SentAt);
      inkSentActionFile.WasSentWhileRunning = action.WasSentWhileRunning;
      IReadOnlyList<string> reviewedItems = action.ReviewedItems;
      inkSentActionFile.ReviewedItems = ((reviewedItems != null) ? reviewedItems.ToList<string>() : null);
      return inkSentActionFile;
    }
  }
}
