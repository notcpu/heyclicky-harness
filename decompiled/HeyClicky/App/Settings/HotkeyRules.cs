using System;
using System.Runtime.CompilerServices;

namespace HeyClicky.App.Settings
{
  // Token: 0x020000DB RID: 219
  [NullableContext(1)]
  [Nullable(0)]
  public static class HotkeyRules
  {
    // Token: 0x06000A63 RID: 2659 RVA: 0x005582B8 File Offset: 0x005582B8
    public static string Title(ShortcutRecorder.Slot slot)
    {
      string text;
      switch (slot)
      {
      case ShortcutRecorder.Slot.PushToTalk:
        text = "Push to talk";
        break;
      case ShortcutRecorder.Slot.TextMode:
        text = "Text mode";
        break;
      case ShortcutRecorder.Slot.Dictation:
        text = "Dictate";
        break;
      default:
        text = "Hands-free dictation";
        break;
      }
      return text;
    }

    // Token: 0x06000A64 RID: 2660 RVA: 0x005582F8 File Offset: 0x005582F8
    public static HotkeyBinding Get(HotkeyBindings b, ShortcutRecorder.Slot slot)
    {
      HotkeyBinding hotkeyBinding;
      switch (slot)
      {
      case ShortcutRecorder.Slot.PushToTalk:
        hotkeyBinding = b.PushToTalk;
        break;
      case ShortcutRecorder.Slot.TextMode:
        hotkeyBinding = b.TextMode;
        break;
      case ShortcutRecorder.Slot.Dictation:
        hotkeyBinding = b.Dictation;
        break;
      default:
        hotkeyBinding = b.HandsFreeDictation;
        break;
      }
      return hotkeyBinding;
    }

    // Token: 0x06000A65 RID: 2661 RVA: 0x0055833C File Offset: 0x0055833C
    public static HotkeyBindings With(HotkeyBindings b, ShortcutRecorder.Slot slot, HotkeyBinding binding)
    {
      HotkeyBindings hotkeyBindings2;
      switch (slot)
      {
      case ShortcutRecorder.Slot.PushToTalk:
      {
        HotkeyBindings hotkeyBindings = b.<Clone>$();
        HotkeyBinding hotkeyBinding = binding.<Clone>$();
        hotkeyBinding.Trigger = HotkeyTrigger.Hold;
        hotkeyBindings.PushToTalk = hotkeyBinding;
        hotkeyBindings2 = hotkeyBindings;
        break;
      }
      case ShortcutRecorder.Slot.TextMode:
      {
        HotkeyBindings hotkeyBindings3 = b.<Clone>$();
        hotkeyBindings3.TextMode = binding;
        hotkeyBindings2 = hotkeyBindings3;
        break;
      }
      case ShortcutRecorder.Slot.Dictation:
      {
        HotkeyBindings hotkeyBindings4 = b.<Clone>$();
        HotkeyBinding hotkeyBinding2 = binding.<Clone>$();
        hotkeyBinding2.Trigger = HotkeyTrigger.Hold;
        hotkeyBindings4.Dictation = hotkeyBinding2;
        hotkeyBindings2 = hotkeyBindings4;
        break;
      }
      default:
      {
        HotkeyBindings hotkeyBindings5 = b.<Clone>$();
        hotkeyBindings5.HandsFreeDictation = binding;
        hotkeyBindings2 = hotkeyBindings5;
        break;
      }
      }
      return hotkeyBindings2;
    }

    // Token: 0x06000A66 RID: 2662 RVA: 0x005583B4 File Offset: 0x005583B4
    public static ShortcutRecorder.Slot? Conflict(HotkeyBindings b, ShortcutRecorder.Slot slot, HotkeyBinding binding)
    {
      foreach (ShortcutRecorder.Slot slot2 in Enum.GetValues<ShortcutRecorder.Slot>())
      {
        if (slot2 != slot)
        {
          HotkeyBinding hotkeyBinding = HotkeyRules.Get(b, slot2);
          if (hotkeyBinding.Modifiers == binding.Modifiers && (slot != ShortcutRecorder.Slot.HandsFreeDictation || slot2 != ShortcutRecorder.Slot.Dictation || binding.Trigger != HotkeyTrigger.DoubleTap) && (slot != ShortcutRecorder.Slot.Dictation || slot2 != ShortcutRecorder.Slot.HandsFreeDictation || hotkeyBinding.Trigger != HotkeyTrigger.DoubleTap))
          {
            return new ShortcutRecorder.Slot?(slot2);
          }
        }
      }
      return null;
    }
  }
}
