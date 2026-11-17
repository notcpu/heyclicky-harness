using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using HeyClicky.App.Shell;

namespace HeyClicky.App.DevFakes
{
  // Token: 0x0200033C RID: 828
  public sealed class DevShortcutMonitor : IShortcutMonitor, IDisposable
  {
    // Token: 0x0600233A RID: 9018
    [NullableContext(1)]
    [DllImport("user32.dll")]
    private static extern IntPtr SetWindowsHookExW(int id, DevShortcutMonitor.HookProc proc, IntPtr mod, uint thread);

    // Token: 0x0600233B RID: 9019
    [DllImport("user32.dll")]
    private static extern bool UnhookWindowsHookEx(IntPtr hook);

    // Token: 0x0600233C RID: 9020
    [DllImport("user32.dll")]
    private static extern IntPtr CallNextHookEx(IntPtr hook, int code, IntPtr w, IntPtr l);

    // Token: 0x0600233D RID: 9021
    [NullableContext(2)]
    [DllImport("kernel32.dll")]
    private static extern IntPtr GetModuleHandleW(string name);

    // Token: 0x1400007D RID: 125
    // (add) Token: 0x0600233E RID: 9022 RVA: 0x0062DCA8 File Offset: 0x0062DCA8
    // (remove) Token: 0x0600233F RID: 9023 RVA: 0x0062DCE0 File Offset: 0x0062DCE0
    [Nullable(2)]
    [method: NullableContext(2)]
    [field: Nullable(2)]
    public event EventHandler<ShortcutAction> ShortcutTriggered;

    // Token: 0x17000677 RID: 1655
    // (get) Token: 0x06002340 RID: 9024 RVA: 0x0062DD18 File Offset: 0x0062DD18
    public bool IsPushToTalkHeld
    {
      get
      {
        return this.ptt;
      }
    }

    // Token: 0x06002341 RID: 9025 RVA: 0x0062DD20 File Offset: 0x0062DD20
    public DevShortcutMonitor()
    {
      this.proc = new DevShortcutMonitor.HookProc(this.Hook);
    }

    // Token: 0x06002342 RID: 9026 RVA: 0x0062DD3C File Offset: 0x0062DD3C
    public void Start()
    {
      if (this.hook != 0)
      {
        return;
      }
      if (OverlayWindow.Offscreen || OverlayWindow.OnscreenBesideReal)
      {
        Log.Info("hotkeys", "offscreen run: dev keyboard hook skipped");
        return;
      }
      this.hook = DevShortcutMonitor.SetWindowsHookExW(13, this.proc, DevShortcutMonitor.GetModuleHandleW(null), 0U);
      Log.Info("hotkeys", (this.hook != 0) ? "dev keyboard hook installed" : "dev keyboard hook failed");
    }

    // Token: 0x06002343 RID: 9027 RVA: 0x0062DDA8 File Offset: 0x0062DDA8
    public void Stop()
    {
      if (this.hook != 0)
      {
        DevShortcutMonitor.UnhookWindowsHookEx(this.hook);
      }
      this.hook = (IntPtr)0;
    }

    // Token: 0x06002344 RID: 9028 RVA: 0x0062DDC8 File Offset: 0x0062DDC8
    private IntPtr Hook(int code, IntPtr wParam, IntPtr lParam)
    {
      if (code >= 0)
      {
        DevShortcutMonitor.KBDLLHOOKSTRUCT kbdllhookstruct = Marshal.PtrToStructure<DevShortcutMonitor.KBDLLHOOKSTRUCT>(lParam);
        bool flag = wParam == (IntPtr)256 || wParam == (IntPtr)260;
        bool flag2 = wParam == (IntPtr)257 || wParam == (IntPtr)261;
        bool flag3 = kbdllhookstruct.vkCode == 162U && (kbdllhookstruct.scanCode & 512U) > 0U;
        uint vkCode = kbdllhookstruct.vkCode;
        if (vkCode != 27U)
        {
          switch (vkCode)
          {
          case 162U:
            if (!flag3)
            {
              this.OnCtrl(ref this.lctrl, flag, flag2);
              goto IL_0101;
            }
            break;
          case 163U:
            this.OnCtrl(ref this.rctrl, flag, flag2);
            goto IL_0101;
          case 164U:
            if (flag)
            {
              this.lalt = true;
            }
            if (flag2)
            {
              this.lalt = false;
            }
            this.otherKeySinceCtrl = true;
            goto IL_0101;
          case 165U:
            if (flag)
            {
              this.ralt = true;
            }
            if (flag2)
            {
              this.ralt = false;
            }
            this.otherKeySinceCtrl = true;
            goto IL_0101;
          }
          if (flag)
          {
            this.otherKeySinceCtrl = true;
          }
        }
        else
        {
          if (flag)
          {
            this.Raise(ShortcutAction.Escape);
          }
          this.otherKeySinceCtrl = true;
        }
        IL_0101:
        bool flag4 = (this.lalt && (this.lctrl || this.rctrl)) || (this.rctrl && this.ralt);
        if (flag4 && !this.ptt)
        {
          this.ptt = true;
          this.Raise(ShortcutAction.PushToTalkDown);
        }
        else if (!flag4 && this.ptt)
        {
          this.ptt = false;
          this.Raise(ShortcutAction.PushToTalkUp);
        }
      }
      return DevShortcutMonitor.CallNextHookEx(this.hook, code, wParam, lParam);
    }

    // Token: 0x06002345 RID: 9029 RVA: 0x0062DF48 File Offset: 0x0062DF48
    private void OnCtrl(ref bool state, bool down, bool up)
    {
      if (down && !state)
      {
        state = true;
        this.otherKeySinceCtrl = false;
        return;
      }
      if (up & state)
      {
        state = false;
        if (this.otherKeySinceCtrl || this.ptt)
        {
          this.lastCtrlTap = 0L;
          return;
        }
        long tickCount = Environment.TickCount64;
        if (this.lastCtrlTap != 0L && tickCount - this.lastCtrlTap <= 350L)
        {
          this.lastCtrlTap = 0L;
          this.Raise(ShortcutAction.TextComposerToggle);
          return;
        }
        this.lastCtrlTap = tickCount;
      }
    }

    // Token: 0x06002346 RID: 9030 RVA: 0x0062DFC0 File Offset: 0x0062DFC0
    private void Raise(ShortcutAction a)
    {
      try
      {
        EventHandler<ShortcutAction> shortcutTriggered = this.ShortcutTriggered;
        if (shortcutTriggered != null)
        {
          shortcutTriggered(this, a);
        }
      }
      catch (Exception ex)
      {
        string text = "hotkeys";
        DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(12, 1);
        defaultInterpolatedStringHandler.AppendLiteral("handler for ");
        defaultInterpolatedStringHandler.AppendFormatted<ShortcutAction>(a);
        Log.Error(text, defaultInterpolatedStringHandler.ToStringAndClear(), ex);
      }
    }

    // Token: 0x06002347 RID: 9031 RVA: 0x0062E028 File Offset: 0x0062E028
    public void Dispose()
    {
      this.Stop();
    }

    // Token: 0x0400113A RID: 4410
    private const int WH_KEYBOARD_LL = 13;

    // Token: 0x0400113B RID: 4411
    private const int WM_KEYDOWN = 256;

    // Token: 0x0400113C RID: 4412
    private const int WM_KEYUP = 257;

    // Token: 0x0400113D RID: 4413
    private const int WM_SYSKEYDOWN = 260;

    // Token: 0x0400113E RID: 4414
    private const int WM_SYSKEYUP = 261;

    // Token: 0x0400113F RID: 4415
    private const uint VK_LCONTROL = 162U;

    // Token: 0x04001140 RID: 4416
    private const uint VK_RCONTROL = 163U;

    // Token: 0x04001141 RID: 4417
    private const uint VK_LMENU = 164U;

    // Token: 0x04001142 RID: 4418
    private const uint VK_RMENU = 165U;

    // Token: 0x04001143 RID: 4419
    private const uint VK_ESCAPE = 27U;

    // Token: 0x04001144 RID: 4420
    [Nullable(1)]
    private readonly DevShortcutMonitor.HookProc proc;

    // Token: 0x04001145 RID: 4421
    private IntPtr hook;

    // Token: 0x04001146 RID: 4422
    private bool lctrl;

    // Token: 0x04001147 RID: 4423
    private bool rctrl;

    // Token: 0x04001148 RID: 4424
    private bool lalt;

    // Token: 0x04001149 RID: 4425
    private bool ralt;

    // Token: 0x0400114A RID: 4426
    private bool ptt;

    // Token: 0x0400114B RID: 4427
    private bool otherKeySinceCtrl;

    // Token: 0x0400114C RID: 4428
    private long lastCtrlTap;

    // Token: 0x0400114D RID: 4429
    private const int DoubleTapMs = 350;

    // Token: 0x0200098F RID: 2447
    // (Invoke) Token: 0x06003DA5 RID: 15781
    private delegate IntPtr HookProc(int code, IntPtr wParam, IntPtr lParam);

    // Token: 0x02000990 RID: 2448
    private struct KBDLLHOOKSTRUCT
    {
      // Token: 0x040027F2 RID: 10226
      public uint vkCode;

      // Token: 0x040027F3 RID: 10227
      public uint scanCode;

      // Token: 0x040027F4 RID: 10228
      public uint flags;

      // Token: 0x040027F5 RID: 10229
      public uint time;

      // Token: 0x040027F6 RID: 10230
      public UIntPtr extra;
    }
  }
}
