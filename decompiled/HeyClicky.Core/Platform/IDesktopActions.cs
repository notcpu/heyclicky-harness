using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace HeyClicky.Core.Platform
{
  // Token: 0x020000E4 RID: 228
  [NullableContext(1)]
  public interface IDesktopActions
  {
    // Token: 0x06000EBD RID: 3773
    bool OpenUrl(string url);

    // Token: 0x06000EBE RID: 3774
    bool CopyToClipboard(string text);

    // Token: 0x06000EBF RID: 3775
    Task<string> TypeIntoFocusedFieldAsync(string text);

    // Token: 0x06000EC0 RID: 3776
    [return: TupleElementNames(new string[] { "DisplayName", "ProcessName" })]
    [return: Nullable(new byte[] { 0, 2, 2 })]
    ValueTuple<string, string> FrontApplication();

    // Token: 0x06000EC1 RID: 3777 RVA: 0x0038C2AC File Offset: 0x0038C2AC
    async Task<TextInsertOutcome> InsertIntoFocusedFieldAsync(string text)
    {
      string text2 = await this.TypeIntoFocusedFieldAsync(text).ConfigureAwait(false);
      return (text2 == "typed") ? new TextInsertOutcome("inserted", null, null) : ((text2 == "copied_to_clipboard") ? new TextInsertOutcome("copied_to_clipboard", null, null) : new TextInsertOutcome("refused", "unsafe_target", null));
    }
  }
}
