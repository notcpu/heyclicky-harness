using System;
using System.Runtime.CompilerServices;

namespace HeyClicky.App.Settings
{
  // Token: 0x020000D9 RID: 217
  [NullableContext(1)]
  [Nullable(0)]
  public static class ShortcutCaptureRules
  {
    // Token: 0x06000A48 RID: 2632 RVA: 0x005579EC File Offset: 0x005579EC
    [NullableContext(2)]
    public static string HoldCaptureProblem(HotkeyModifiers held, bool pressedOtherKey)
    {
      if (pressedOtherKey)
      {
        return "Shortcuts use only Ctrl, Alt, Shift and Win. Hold two of them together, then let go.";
      }
      if (held == HotkeyModifiers.None || !ShortcutCaptureRules.IsSingleKey(held))
      {
        return null;
      }
      string text = new HotkeyBinding(held, HotkeyTrigger.Hold).KeyNames[0];
      return "Only " + text + " came through. Hold two of Ctrl, Alt, Shift and Win together, then let go. If you did, another app may be using that combo.";
    }

    // Token: 0x06000A49 RID: 2633 RVA: 0x00557A34 File Offset: 0x00557A34
    public static string ConflictLine(HotkeyBinding captured, string otherTitle, HotkeyBinding other)
    {
      DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(67, 4);
      defaultInterpolatedStringHandler.AppendFormatted(string.Join(" + ", captured.KeyNames));
      defaultInterpolatedStringHandler.AppendLiteral(" is already used by ");
      defaultInterpolatedStringHandler.AppendFormatted(otherTitle);
      defaultInterpolatedStringHandler.AppendLiteral(" (");
      defaultInterpolatedStringHandler.AppendFormatted(other.DisplayText);
      defaultInterpolatedStringHandler.AppendLiteral("). Press a different combo, or change ");
      defaultInterpolatedStringHandler.AppendFormatted(otherTitle);
      defaultInterpolatedStringHandler.AppendLiteral(" first.");
      return defaultInterpolatedStringHandler.ToStringAndClear();
    }

    // Token: 0x06000A4A RID: 2634 RVA: 0x00557AB8 File Offset: 0x00557AB8
    private static bool IsSingleKey(HotkeyModifiers modifiers)
    {
      return (modifiers & (modifiers - 1)) == HotkeyModifiers.None;
    }

    // Token: 0x040003BF RID: 959
    public const string ModifiersOnly = "Shortcuts use only Ctrl, Alt, Shift and Win. Hold two of them together, then let go.";
  }
}
