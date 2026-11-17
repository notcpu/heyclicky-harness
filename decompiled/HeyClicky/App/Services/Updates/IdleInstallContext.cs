using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace HeyClicky.App.Services.Updates
{
  // Token: 0x020000EB RID: 235
  public readonly struct IdleInstallContext : IEquatable<IdleInstallContext>
  {
    // Token: 0x06000B63 RID: 2915 RVA: 0x0055B45C File Offset: 0x0055B45C
    public IdleInstallContext(bool AgentWorking, bool VoiceActive, bool Driving, bool NotchOpen, bool Recording, bool Onboarding, bool FullScreen, bool Presentation, bool OnCall, bool NotchAnswering, bool ShowingOnScreen)
    {
      this.AgentWorking = AgentWorking;
      this.VoiceActive = VoiceActive;
      this.Driving = Driving;
      this.NotchOpen = NotchOpen;
      this.Recording = Recording;
      this.Onboarding = Onboarding;
      this.FullScreen = FullScreen;
      this.Presentation = Presentation;
      this.OnCall = OnCall;
      this.NotchAnswering = NotchAnswering;
      this.ShowingOnScreen = ShowingOnScreen;
    }

    // Token: 0x170001F2 RID: 498
    // (get) Token: 0x06000B64 RID: 2916 RVA: 0x0055B4C0 File Offset: 0x0055B4C0
    // (set) Token: 0x06000B65 RID: 2917 RVA: 0x0055B4C8 File Offset: 0x0055B4C8
    public bool AgentWorking { get; set; }

    // Token: 0x170001F3 RID: 499
    // (get) Token: 0x06000B66 RID: 2918 RVA: 0x0055B4D4 File Offset: 0x0055B4D4
    // (set) Token: 0x06000B67 RID: 2919 RVA: 0x0055B4DC File Offset: 0x0055B4DC
    public bool VoiceActive { get; set; }

    // Token: 0x170001F4 RID: 500
    // (get) Token: 0x06000B68 RID: 2920 RVA: 0x0055B4E8 File Offset: 0x0055B4E8
    // (set) Token: 0x06000B69 RID: 2921 RVA: 0x0055B4F0 File Offset: 0x0055B4F0
    public bool Driving { get; set; }

    // Token: 0x170001F5 RID: 501
    // (get) Token: 0x06000B6A RID: 2922 RVA: 0x0055B4FC File Offset: 0x0055B4FC
    // (set) Token: 0x06000B6B RID: 2923 RVA: 0x0055B504 File Offset: 0x0055B504
    public bool NotchOpen { get; set; }

    // Token: 0x170001F6 RID: 502
    // (get) Token: 0x06000B6C RID: 2924 RVA: 0x0055B510 File Offset: 0x0055B510
    // (set) Token: 0x06000B6D RID: 2925 RVA: 0x0055B518 File Offset: 0x0055B518
    public bool Recording { get; set; }

    // Token: 0x170001F7 RID: 503
    // (get) Token: 0x06000B6E RID: 2926 RVA: 0x0055B524 File Offset: 0x0055B524
    // (set) Token: 0x06000B6F RID: 2927 RVA: 0x0055B52C File Offset: 0x0055B52C
    public bool Onboarding { get; set; }

    // Token: 0x170001F8 RID: 504
    // (get) Token: 0x06000B70 RID: 2928 RVA: 0x0055B538 File Offset: 0x0055B538
    // (set) Token: 0x06000B71 RID: 2929 RVA: 0x0055B540 File Offset: 0x0055B540
    public bool FullScreen { get; set; }

    // Token: 0x170001F9 RID: 505
    // (get) Token: 0x06000B72 RID: 2930 RVA: 0x0055B54C File Offset: 0x0055B54C
    // (set) Token: 0x06000B73 RID: 2931 RVA: 0x0055B554 File Offset: 0x0055B554
    public bool Presentation { get; set; }

    // Token: 0x170001FA RID: 506
    // (get) Token: 0x06000B74 RID: 2932 RVA: 0x0055B560 File Offset: 0x0055B560
    // (set) Token: 0x06000B75 RID: 2933 RVA: 0x0055B568 File Offset: 0x0055B568
    public bool OnCall { get; set; }

    // Token: 0x170001FB RID: 507
    // (get) Token: 0x06000B76 RID: 2934 RVA: 0x0055B574 File Offset: 0x0055B574
    // (set) Token: 0x06000B77 RID: 2935 RVA: 0x0055B57C File Offset: 0x0055B57C
    public bool NotchAnswering { get; set; }

    // Token: 0x170001FC RID: 508
    // (get) Token: 0x06000B78 RID: 2936 RVA: 0x0055B588 File Offset: 0x0055B588
    // (set) Token: 0x06000B79 RID: 2937 RVA: 0x0055B590 File Offset: 0x0055B590
    public bool ShowingOnScreen { get; set; }

    // Token: 0x06000B7A RID: 2938 RVA: 0x0055B59C File Offset: 0x0055B59C
    [CompilerGenerated]
    public override string ToString()
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("IdleInstallContext");
      stringBuilder.Append(" { ");
      if (this.PrintMembers(stringBuilder))
      {
        stringBuilder.Append(' ');
      }
      stringBuilder.Append('}');
      return stringBuilder.ToString();
    }

    // Token: 0x06000B7B RID: 2939 RVA: 0x0055B5E8 File Offset: 0x0055B5E8
    [CompilerGenerated]
    private bool PrintMembers(StringBuilder builder)
    {
      builder.Append("AgentWorking = ");
      builder.Append(this.AgentWorking.ToString());
      builder.Append(", VoiceActive = ");
      builder.Append(this.VoiceActive.ToString());
      builder.Append(", Driving = ");
      builder.Append(this.Driving.ToString());
      builder.Append(", NotchOpen = ");
      builder.Append(this.NotchOpen.ToString());
      builder.Append(", Recording = ");
      builder.Append(this.Recording.ToString());
      builder.Append(", Onboarding = ");
      builder.Append(this.Onboarding.ToString());
      builder.Append(", FullScreen = ");
      builder.Append(this.FullScreen.ToString());
      builder.Append(", Presentation = ");
      builder.Append(this.Presentation.ToString());
      builder.Append(", OnCall = ");
      builder.Append(this.OnCall.ToString());
      builder.Append(", NotchAnswering = ");
      builder.Append(this.NotchAnswering.ToString());
      builder.Append(", ShowingOnScreen = ");
      builder.Append(this.ShowingOnScreen.ToString());
      return true;
    }

    // Token: 0x06000B7C RID: 2940 RVA: 0x0055B7A4 File Offset: 0x0055B7A4
    [CompilerGenerated]
    public static bool operator !=(IdleInstallContext left, IdleInstallContext right)
    {
      return !(left == right);
    }

    // Token: 0x06000B7D RID: 2941 RVA: 0x0055B7B0 File Offset: 0x0055B7B0
    [CompilerGenerated]
    public static bool operator ==(IdleInstallContext left, IdleInstallContext right)
    {
      return left.Equals(right);
    }

    // Token: 0x06000B7E RID: 2942 RVA: 0x0055B7BC File Offset: 0x0055B7BC
    [CompilerGenerated]
    public override int GetHashCode()
    {
      return (((((((((EqualityComparer<bool>.Default.GetHashCode(this.<AgentWorking>k__BackingField) * -1521134295 + EqualityComparer<bool>.Default.GetHashCode(this.<VoiceActive>k__BackingField)) * -1521134295 + EqualityComparer<bool>.Default.GetHashCode(this.<Driving>k__BackingField)) * -1521134295 + EqualityComparer<bool>.Default.GetHashCode(this.<NotchOpen>k__BackingField)) * -1521134295 + EqualityComparer<bool>.Default.GetHashCode(this.<Recording>k__BackingField)) * -1521134295 + EqualityComparer<bool>.Default.GetHashCode(this.<Onboarding>k__BackingField)) * -1521134295 + EqualityComparer<bool>.Default.GetHashCode(this.<FullScreen>k__BackingField)) * -1521134295 + EqualityComparer<bool>.Default.GetHashCode(this.<Presentation>k__BackingField)) * -1521134295 + EqualityComparer<bool>.Default.GetHashCode(this.<OnCall>k__BackingField)) * -1521134295 + EqualityComparer<bool>.Default.GetHashCode(this.<NotchAnswering>k__BackingField)) * -1521134295 + EqualityComparer<bool>.Default.GetHashCode(this.<ShowingOnScreen>k__BackingField);
    }

    // Token: 0x06000B7F RID: 2943 RVA: 0x0055B8C0 File Offset: 0x0055B8C0
    [CompilerGenerated]
    public override bool Equals(object obj)
    {
      return obj is IdleInstallContext && this.Equals((IdleInstallContext)obj);
    }

    // Token: 0x06000B80 RID: 2944 RVA: 0x0055B8D8 File Offset: 0x0055B8D8
    [CompilerGenerated]
    public bool Equals(IdleInstallContext other)
    {
      return EqualityComparer<bool>.Default.Equals(this.<AgentWorking>k__BackingField, other.<AgentWorking>k__BackingField) && EqualityComparer<bool>.Default.Equals(this.<VoiceActive>k__BackingField, other.<VoiceActive>k__BackingField) && EqualityComparer<bool>.Default.Equals(this.<Driving>k__BackingField, other.<Driving>k__BackingField) && EqualityComparer<bool>.Default.Equals(this.<NotchOpen>k__BackingField, other.<NotchOpen>k__BackingField) && EqualityComparer<bool>.Default.Equals(this.<Recording>k__BackingField, other.<Recording>k__BackingField) && EqualityComparer<bool>.Default.Equals(this.<Onboarding>k__BackingField, other.<Onboarding>k__BackingField) && EqualityComparer<bool>.Default.Equals(this.<FullScreen>k__BackingField, other.<FullScreen>k__BackingField) && EqualityComparer<bool>.Default.Equals(this.<Presentation>k__BackingField, other.<Presentation>k__BackingField) && EqualityComparer<bool>.Default.Equals(this.<OnCall>k__BackingField, other.<OnCall>k__BackingField) && EqualityComparer<bool>.Default.Equals(this.<NotchAnswering>k__BackingField, other.<NotchAnswering>k__BackingField) && EqualityComparer<bool>.Default.Equals(this.<ShowingOnScreen>k__BackingField, other.<ShowingOnScreen>k__BackingField);
    }

    // Token: 0x06000B81 RID: 2945 RVA: 0x0055B9FC File Offset: 0x0055B9FC
    [CompilerGenerated]
    public void Deconstruct(out bool AgentWorking, out bool VoiceActive, out bool Driving, out bool NotchOpen, out bool Recording, out bool Onboarding, out bool FullScreen, out bool Presentation, out bool OnCall, out bool NotchAnswering, out bool ShowingOnScreen)
    {
      AgentWorking = this.AgentWorking;
      VoiceActive = this.VoiceActive;
      Driving = this.Driving;
      NotchOpen = this.NotchOpen;
      Recording = this.Recording;
      Onboarding = this.Onboarding;
      FullScreen = this.FullScreen;
      Presentation = this.Presentation;
      OnCall = this.OnCall;
      NotchAnswering = this.NotchAnswering;
      ShowingOnScreen = this.ShowingOnScreen;
    }
  }
}
