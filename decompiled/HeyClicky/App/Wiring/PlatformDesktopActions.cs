using System;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using HeyClicky.Core.Platform;
using HeyClicky.Platform.Win32;

namespace HeyClicky.App.Wiring
{
  // Token: 0x0200009D RID: 157
  [NullableContext(1)]
  [Nullable(0)]
  public sealed class PlatformDesktopActions : IDesktopActions
  {
    // Token: 0x0600065D RID: 1629 RVA: 0x005305A0 File Offset: 0x005305A0
    public bool OpenUrl(string url)
    {
      return this.basic.OpenUrl(url);
    }

    // Token: 0x0600065E RID: 1630 RVA: 0x005305B0 File Offset: 0x005305B0
    public bool CopyToClipboard(string text)
    {
      return this.basic.CopyToClipboard(text);
    }

    // Token: 0x0600065F RID: 1631 RVA: 0x005305C0 File Offset: 0x005305C0
    public async Task<string> TypeIntoFocusedFieldAsync(string text)
    {
      string status = (await this.InsertIntoFocusedFieldAsync(text).ConfigureAwait(false)).Status;
      string text2;
      if (!(status == "inserted"))
      {
        if (!(status == "copied_to_clipboard"))
        {
          text2 = "refused";
        }
        else
        {
          text2 = "copied_to_clipboard";
        }
      }
      else
      {
        text2 = "typed";
      }
      return text2;
    }

    // Token: 0x06000660 RID: 1632 RVA: 0x0053060C File Offset: 0x0053060C
    public async Task<TextInsertOutcome> InsertIntoFocusedFieldAsync(string text)
    {
      UiaPageText.FocusTarget focusTarget = await UiaPageText.FocusedTargetAsync(default(CancellationToken)).ConfigureAwait(false);
      UiaPageText.FocusTarget target = focusTarget;
      TextInsertOutcome textInsertOutcome;
      if (target == null || !target.HasFocus)
      {
        textInsertOutcome = new TextInsertOutcome("refused", "no_focused_text_field", null);
      }
      else if (target.ProcessId == Environment.ProcessId)
      {
        textInsertOutcome = new TextInsertOutcome("refused", "own_window", null);
      }
      else if (target.IsPassword)
      {
        textInsertOutcome = new TextInsertOutcome("refused", "secure_field", null);
      }
      else if (target.IsAddressBar)
      {
        textInsertOutcome = new TextInsertOutcome("refused", "address_bar", null);
      }
      else
      {
        ForegroundApp foregroundApp = ForegroundAppInfo.CaptureUserApp();
        string appName = ((foregroundApp != null) ? foregroundApp.AppName : null) ?? target.AppName;
        DictationFocus dictationFocus = await this.fieldTarget.CaptureFocusAsync(default(CancellationToken)).ConfigureAwait(false);
        DefaultInterpolatedStringHandler defaultInterpolatedStringHandler;
        if (dictationFocus != null && !dictationFocus.IsSecure)
        {
          PlatformDesktopActions.<>c__DisplayClass5_0 CS$<>8__locals1 = new PlatformDesktopActions.<>c__DisplayClass5_0();
          CS$<>8__locals1.verdict = new TaskCompletionSource<string>(TaskCreationOptions.RunContinuationsAsynchronously);
          DictationInsertOutcome dictationInsertOutcome = await this.fieldTarget.InsertAsync(text, dictationFocus, false, delegate(string result, int _)
          {
            CS$<>8__locals1.verdict.TrySetResult(result);
          }).ConfigureAwait(false);
          if (dictationInsertOutcome == DictationInsertOutcome.Inserted)
          {
            ConfiguredTaskAwaitable<Task>.ConfiguredTaskAwaiter configuredTaskAwaiter = Task.WhenAny(CS$<>8__locals1.verdict.Task, Task.Delay(TimeSpan.FromSeconds(3L))).ConfigureAwait(false).GetAwaiter();
            if (!configuredTaskAwaiter.IsCompleted)
            {
              await configuredTaskAwaiter;
              ConfiguredTaskAwaitable<Task>.ConfiguredTaskAwaiter configuredTaskAwaiter2;
              configuredTaskAwaiter = configuredTaskAwaiter2;
              configuredTaskAwaiter2 = default(ConfiguredTaskAwaitable<Task>.ConfiguredTaskAwaiter);
            }
            string text2 = ((configuredTaskAwaiter.GetResult() == CS$<>8__locals1.verdict.Task) ? CS$<>8__locals1.verdict.Task.Result : "no_answer");
            string text3 = "Desktop";
            defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(33, 3);
            defaultInterpolatedStringHandler.AppendLiteral("insert into ");
            defaultInterpolatedStringHandler.AppendFormatted(target.AppName ?? "?");
            defaultInterpolatedStringHandler.AppendLiteral(" (");
            defaultInterpolatedStringHandler.AppendFormatted(target.ControlType);
            defaultInterpolatedStringHandler.AppendLiteral("): pasted, landing ");
            defaultInterpolatedStringHandler.AppendFormatted(text2);
            Log.Info(text3, defaultInterpolatedStringHandler.ToStringAndClear());
            return (text2 == "not_observed") ? new TextInsertOutcome("unconfirmed", "not_observed", appName) : new TextInsertOutcome("inserted", null, appName);
          }
          if (dictationInsertOutcome == DictationInsertOutcome.ClipboardWriteFailed)
          {
            return new TextInsertOutcome("failed", "clipboard_unavailable", null);
          }
          string text4 = "Desktop";
          defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(49, 1);
          defaultInterpolatedStringHandler.AppendLiteral("insert via the dictation path: ");
          defaultInterpolatedStringHandler.AppendFormatted<DictationInsertOutcome>(dictationInsertOutcome);
          defaultInterpolatedStringHandler.AppendLiteral("; pasting directly");
          Log.Info(text4, defaultInterpolatedStringHandler.ToStringAndClear());
          CS$<>8__locals1 = null;
        }
        bool flag = await TextInsertion.PasteTextAsync(text, null, true).ConfigureAwait(false);
        string text5 = "Desktop";
        defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(17, 3);
        defaultInterpolatedStringHandler.AppendLiteral("insert into ");
        defaultInterpolatedStringHandler.AppendFormatted(target.AppName ?? "?");
        defaultInterpolatedStringHandler.AppendLiteral(" (");
        defaultInterpolatedStringHandler.AppendFormatted(target.ControlType);
        defaultInterpolatedStringHandler.AppendLiteral("): ");
        defaultInterpolatedStringHandler.AppendFormatted(flag ? "pasted (unverified)" : "clipboard write failed");
        Log.Info(text5, defaultInterpolatedStringHandler.ToStringAndClear());
        textInsertOutcome = (flag ? new TextInsertOutcome("inserted", null, appName) : new TextInsertOutcome("failed", "clipboard_unavailable", null));
      }
      return textInsertOutcome;
    }

    // Token: 0x06000661 RID: 1633 RVA: 0x00530658 File Offset: 0x00530658
    [return: TupleElementNames(new string[] { "DisplayName", "ProcessName" })]
    [return: Nullable(new byte[] { 0, 2, 2 })]
    public ValueTuple<string, string> FrontApplication()
    {
      ForegroundApp foregroundApp = ForegroundAppInfo.CaptureUserApp();
      if (foregroundApp != null)
      {
        return new ValueTuple<string, string>(foregroundApp.AppName, foregroundApp.ProcessName);
      }
      return new ValueTuple<string, string>(null, null);
    }

    // Token: 0x0400018C RID: 396
    private readonly WindowsDesktopActions basic = new WindowsDesktopActions();

    // Token: 0x0400018D RID: 397
    private readonly DictationTextTarget fieldTarget = new DictationTextTarget();
  }
}
