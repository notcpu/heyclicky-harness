using System;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;

namespace HeyClicky
{
  // Token: 0x0200000C RID: 12
  [NullableContext(1)]
  public interface IDictationTextTarget
  {
    // Token: 0x060000A3 RID: 163
    [return: Nullable(new byte[] { 1, 2 })]
    Task<DictationFocus> CaptureFocusAsync(CancellationToken ct = default(CancellationToken));

    // Token: 0x060000A4 RID: 164
    [NullableContext(2)]
    DictationWorkApp ForegroundWorkApp();

    // Token: 0x060000A5 RID: 165
    Task<DictationInsertOutcome> InsertAsync(string text, DictationFocus focus, [Nullable(new byte[] { 2, 1 })] Action<string, int> onPasteVerification = null);

    // Token: 0x060000A6 RID: 166
    Task<bool> BlindPasteAsync(string text, DictationWorkApp app);

    // Token: 0x060000A7 RID: 167
    Task<bool> CopyToClipboardAsync(string text);

    // Token: 0x060000A8 RID: 168
    [return: Nullable(new byte[] { 1, 2 })]
    Task<string> ReadFieldValueAsync(DictationFocus focus);
  }
}
