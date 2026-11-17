using System;
using System.Runtime.CompilerServices;
using Microsoft.UI.Dispatching;

namespace HeyClicky.App.Settings
{
  // Token: 0x020000DA RID: 218
  [NullableContext(2)]
  [Nullable(0)]
  public sealed class ShortcutRecorder
  {
    // Token: 0x17000199 RID: 409
    // (get) Token: 0x06000A4B RID: 2635 RVA: 0x00557AC4 File Offset: 0x00557AC4
    // (set) Token: 0x06000A4C RID: 2636 RVA: 0x00557ACC File Offset: 0x00557ACC
    public ShortcutRecorder.Slot? Mode { get; private set; }

    // Token: 0x1700019A RID: 410
    // (get) Token: 0x06000A4D RID: 2637 RVA: 0x00557AD8 File Offset: 0x00557AD8
    // (set) Token: 0x06000A4E RID: 2638 RVA: 0x00557AE0 File Offset: 0x00557AE0
    public HotkeyBinding Captured { get; private set; }

    // Token: 0x1700019B RID: 411
    // (get) Token: 0x06000A4F RID: 2639 RVA: 0x00557AEC File Offset: 0x00557AEC
    // (set) Token: 0x06000A50 RID: 2640 RVA: 0x00557AF4 File Offset: 0x00557AF4
    public bool IsLocked { get; private set; }

    // Token: 0x1700019C RID: 412
    // (get) Token: 0x06000A51 RID: 2641 RVA: 0x00557B00 File Offset: 0x00557B00
    // (set) Token: 0x06000A52 RID: 2642 RVA: 0x00557B08 File Offset: 0x00557B08
    public string Hint { get; private set; }

    // Token: 0x1700019D RID: 413
    // (get) Token: 0x06000A53 RID: 2643 RVA: 0x00557B14 File Offset: 0x00557B14
    // (set) Token: 0x06000A54 RID: 2644 RVA: 0x00557B1C File Offset: 0x00557B1C
    [Nullable(new byte[] { 2, 1, 2 })]
    public Func<ShortcutRecorder.Slot, HotkeyBinding, string> Validate
    {
      [return: Nullable(new byte[] { 2, 1, 2 })]
      get;
      [param: Nullable(new byte[] { 2, 1, 2 })]
      set;
    }

    // Token: 0x14000025 RID: 37
    // (add) Token: 0x06000A55 RID: 2645 RVA: 0x00557B28 File Offset: 0x00557B28
    // (remove) Token: 0x06000A56 RID: 2646 RVA: 0x00557B60 File Offset: 0x00557B60
    public event Action Changed;

    // Token: 0x06000A57 RID: 2647 RVA: 0x00557B98 File Offset: 0x00557B98
    [NullableContext(1)]
    public ShortcutRecorder(DispatcherQueue dq)
    {
      this.dq = dq;
      this.timeout = dq.CreateTimer();
      this.timeout.Interval = TimeSpan.FromSeconds(7.0);
      this.timeout.IsRepeating = false;
      this.timeout.Tick += delegate(DispatcherQueueTimer _, object _)
      {
        if (this.Captured == null)
        {
          this.Cancel();
        }
      };
      this.loneTapSettle = dq.CreateTimer();
      this.loneTapSettle.Interval = TimeSpan.FromSeconds(0.5);
      this.loneTapSettle.IsRepeating = false;
      this.loneTapSettle.Tick += delegate(DispatcherQueueTimer _, object _)
      {
        HotkeyModifiers? hotkeyModifiers = this.pendingFirstTap;
        if (hotkeyModifiers != null)
        {
          HotkeyModifiers valueOrDefault = hotkeyModifiers.GetValueOrDefault();
          if (!this.IsLocked)
          {
            DateTime? dateTime = this.tapPressStart;
            if (dateTime == null)
            {
              this.Commit(new HotkeyBinding(valueOrDefault, HotkeyTrigger.Hold));
            }
          }
        }
      };
    }

    // Token: 0x06000A58 RID: 2648 RVA: 0x00557C44 File Offset: 0x00557C44
    private static bool IsHoldSlot(ShortcutRecorder.Slot slot)
    {
      return slot == ShortcutRecorder.Slot.PushToTalk || slot == ShortcutRecorder.Slot.Dictation;
    }

    // Token: 0x06000A59 RID: 2649 RVA: 0x00557C60 File Offset: 0x00557C60
    public void Begin(ShortcutRecorder.Slot slot)
    {
      this.ResetBuffers();
      this.Hint = null;
      this.Mode = new ShortcutRecorder.Slot?(slot);
      this.timeout.Stop();
      this.timeout.Start();
      string text = "settings";
      DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(10, 1);
      defaultInterpolatedStringHandler.AppendLiteral("recording ");
      defaultInterpolatedStringHandler.AppendFormatted<ShortcutRecorder.Slot>(slot);
      Log.Info(text, defaultInterpolatedStringHandler.ToStringAndClear());
      Action changed = this.Changed;
      if (changed == null)
      {
        return;
      }
      changed();
    }

    // Token: 0x06000A5A RID: 2650 RVA: 0x00557CDC File Offset: 0x00557CDC
    public void Cancel()
    {
      if (this.Mode == null)
      {
        return;
      }
      this.timeout.Stop();
      this.loneTapSettle.Stop();
      this.ResetBuffers();
      this.Hint = null;
      this.Mode = null;
      Action changed = this.Changed;
      if (changed == null)
      {
        return;
      }
      changed();
    }

    // Token: 0x06000A5B RID: 2651 RVA: 0x00557D3C File Offset: 0x00557D3C
    private void ResetBuffers()
    {
      this.Captured = null;
      this.IsLocked = false;
      this.holdUnion = HotkeyModifiers.None;
      this.holdSawOtherKey = false;
      this.tapUnion = HotkeyModifiers.None;
      this.tapPressStart = null;
      this.pendingFirstTap = null;
      this.loneTapSettle.Stop();
    }

    // Token: 0x06000A5C RID: 2652 RVA: 0x00557D90 File Offset: 0x00557D90
    public void IngestKey()
    {
      ShortcutRecorder.Slot? mode = this.Mode;
      if (mode != null)
      {
        ShortcutRecorder.Slot valueOrDefault = mode.GetValueOrDefault();
        if (!this.IsLocked)
        {
          if (!ShortcutRecorder.IsHoldSlot(valueOrDefault))
          {
            this.pendingFirstTap = null;
            this.loneTapSettle.Stop();
            return;
          }
          if (this.holdUnion != HotkeyModifiers.None)
          {
            this.holdSawOtherKey = true;
          }
          this.Hint = "Shortcuts use only Ctrl, Alt, Shift and Win. Hold two of them together, then let go.";
          this.timeout.Stop();
          this.timeout.Start();
          Action changed = this.Changed;
          if (changed == null)
          {
            return;
          }
          changed();
          return;
        }
      }
    }

    // Token: 0x06000A5D RID: 2653 RVA: 0x00557E20 File Offset: 0x00557E20
    public void IngestModifiers(HotkeyModifiers held)
    {
      ShortcutRecorder.Slot? mode = this.Mode;
      if (mode != null)
      {
        ShortcutRecorder.Slot valueOrDefault = mode.GetValueOrDefault();
        if (!this.IsLocked)
        {
          if (ShortcutRecorder.IsHoldSlot(valueOrDefault))
          {
            if (held == HotkeyModifiers.None)
            {
              if (this.holdUnion == HotkeyModifiers.None)
              {
                return;
              }
              string text = ShortcutCaptureRules.HoldCaptureProblem(this.holdUnion, this.holdSawOtherKey);
              if (text != null)
              {
                this.Reject(text);
                return;
              }
              this.Commit(new HotkeyBinding(this.holdUnion, HotkeyTrigger.Hold));
              return;
            }
            else
            {
              this.holdUnion |= held;
              this.Captured = new HotkeyBinding(this.holdUnion, HotkeyTrigger.Hold);
              this.timeout.Stop();
              this.timeout.Start();
              Action changed = this.Changed;
              if (changed == null)
              {
                return;
              }
              changed();
              return;
            }
          }
          else
          {
            if (held == HotkeyModifiers.None)
            {
              this.ReleaseToEmpty();
              return;
            }
            DateTime dateTime = this.tapPressStart.GetValueOrDefault();
            if (this.tapPressStart == null)
            {
              dateTime = DateTime.UtcNow;
              this.tapPressStart = new DateTime?(dateTime);
            }
            this.tapUnion |= held;
            HotkeyModifiers? hotkeyModifiers = this.pendingFirstTap;
            if (hotkeyModifiers != null)
            {
              HotkeyModifiers valueOrDefault2 = hotkeyModifiers.GetValueOrDefault();
              if ((valueOrDefault2 | this.tapUnion) != valueOrDefault2)
              {
                this.pendingFirstTap = null;
                this.loneTapSettle.Stop();
              }
            }
            this.timeout.Stop();
            this.timeout.Start();
            this.Captured = new HotkeyBinding(this.tapUnion, HotkeyTrigger.Hold);
            Action changed2 = this.Changed;
            if (changed2 == null)
            {
              return;
            }
            changed2();
            return;
          }
        }
      }
    }

    // Token: 0x06000A5E RID: 2654 RVA: 0x00557F98 File Offset: 0x00557F98
    private void ReleaseToEmpty()
    {
      if (this.tapUnion == HotkeyModifiers.None)
      {
        this.tapPressStart = null;
        return;
      }
      HotkeyModifiers hotkeyModifiers = this.tapUnion;
      DateTime dateTime = this.tapPressStart ?? DateTime.UtcNow;
      this.tapUnion = HotkeyModifiers.None;
      this.tapPressStart = null;
      if ((DateTime.UtcNow - dateTime).TotalSeconds > 0.4)
      {
        this.pendingFirstTap = null;
        this.loneTapSettle.Stop();
        this.Commit(new HotkeyBinding(hotkeyModifiers, HotkeyTrigger.Hold));
        return;
      }
      HotkeyModifiers? hotkeyModifiers2 = this.pendingFirstTap;
      HotkeyModifiers hotkeyModifiers3 = hotkeyModifiers;
      if (((hotkeyModifiers2.GetValueOrDefault() == hotkeyModifiers3) & (hotkeyModifiers2 != null)) && (dateTime - this.pendingFirstTapRelease).TotalSeconds <= 0.5)
      {
        this.Commit(new HotkeyBinding(hotkeyModifiers, HotkeyTrigger.DoubleTap));
        return;
      }
      this.pendingFirstTap = new HotkeyModifiers?(hotkeyModifiers);
      this.pendingFirstTapRelease = DateTime.UtcNow;
      this.Captured = new HotkeyBinding(hotkeyModifiers, HotkeyTrigger.DoubleTap);
      this.timeout.Stop();
      this.timeout.Start();
      this.loneTapSettle.Stop();
      this.loneTapSettle.Start();
      Action changed = this.Changed;
      if (changed == null)
      {
        return;
      }
      changed();
    }

    // Token: 0x06000A5F RID: 2655 RVA: 0x005580EC File Offset: 0x005580EC
    [NullableContext(1)]
    private void Reject(string problem)
    {
      string text = "settings";
      DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(27, 2);
      defaultInterpolatedStringHandler.AppendLiteral("recording ");
      defaultInterpolatedStringHandler.AppendFormatted<ShortcutRecorder.Slot?>(this.Mode);
      defaultInterpolatedStringHandler.AppendLiteral(": not captured (");
      defaultInterpolatedStringHandler.AppendFormatted(problem);
      defaultInterpolatedStringHandler.AppendLiteral(")");
      Log.Info(text, defaultInterpolatedStringHandler.ToStringAndClear());
      this.ResetBuffers();
      this.Hint = problem;
      this.timeout.Stop();
      this.timeout.Start();
      Action changed = this.Changed;
      if (changed == null)
      {
        return;
      }
      changed();
    }

    // Token: 0x06000A60 RID: 2656 RVA: 0x00558180 File Offset: 0x00558180
    [NullableContext(1)]
    private void Commit(HotkeyBinding binding)
    {
      ShortcutRecorder.Slot? mode = this.Mode;
      if (mode != null)
      {
        ShortcutRecorder.Slot valueOrDefault = mode.GetValueOrDefault();
        Func<ShortcutRecorder.Slot, HotkeyBinding, string> validate = this.Validate;
        string text = ((validate != null) ? validate(valueOrDefault, binding) : null);
        if (text != null)
        {
          this.Reject(text);
          return;
        }
      }
      this.Hint = null;
      this.timeout.Stop();
      this.loneTapSettle.Stop();
      this.pendingFirstTap = null;
      this.Captured = binding;
      this.IsLocked = true;
      string text2 = "settings";
      DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(11, 2);
      defaultInterpolatedStringHandler.AppendLiteral("recorded ");
      defaultInterpolatedStringHandler.AppendFormatted<ShortcutRecorder.Slot?>(this.Mode);
      defaultInterpolatedStringHandler.AppendLiteral(": ");
      defaultInterpolatedStringHandler.AppendFormatted(binding.DisplayText);
      Log.Info(text2, defaultInterpolatedStringHandler.ToStringAndClear());
      Action changed = this.Changed;
      if (changed == null)
      {
        return;
      }
      changed();
    }

    // Token: 0x040003C0 RID: 960
    private const double TapMaxHoldSeconds = 0.4;

    // Token: 0x040003C1 RID: 961
    private const double DoubleTapMaxIntervalSeconds = 0.5;

    // Token: 0x040003C2 RID: 962
    private const double TimeoutSeconds = 7.0;

    // Token: 0x040003C3 RID: 963
    [Nullable(1)]
    private readonly DispatcherQueue dq;

    // Token: 0x040003C4 RID: 964
    [Nullable(1)]
    private readonly DispatcherQueueTimer timeout;

    // Token: 0x040003C5 RID: 965
    [Nullable(1)]
    private readonly DispatcherQueueTimer loneTapSettle;

    // Token: 0x040003CC RID: 972
    private HotkeyModifiers holdUnion;

    // Token: 0x040003CD RID: 973
    private bool holdSawOtherKey;

    // Token: 0x040003CE RID: 974
    private HotkeyModifiers tapUnion;

    // Token: 0x040003CF RID: 975
    private DateTime? tapPressStart;

    // Token: 0x040003D0 RID: 976
    private HotkeyModifiers? pendingFirstTap;

    // Token: 0x040003D1 RID: 977
    private DateTime pendingFirstTapRelease;

    // Token: 0x02000546 RID: 1350
    [NullableContext(0)]
    public enum Slot
    {
      // Token: 0x0400191B RID: 6427
      PushToTalk,
      // Token: 0x0400191C RID: 6428
      TextMode,
      // Token: 0x0400191D RID: 6429
      Dictation,
      // Token: 0x0400191E RID: 6430
      HandsFreeDictation
    }
  }
}
