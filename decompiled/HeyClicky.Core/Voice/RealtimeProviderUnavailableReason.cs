using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace HeyClicky.Core.Voice
{
  // Token: 0x02000081 RID: 129
  [NullableContext(1)]
  [Nullable(0)]
  public abstract class RealtimeProviderUnavailableReason : IEquatable<RealtimeProviderUnavailableReason>
  {
    // Token: 0x170001C0 RID: 448
    // (get) Token: 0x060007E3 RID: 2019 RVA: 0x00367844 File Offset: 0x00367844
    [CompilerGenerated]
    protected virtual Type EqualityContract
    {
      [CompilerGenerated]
      get
      {
        return typeof(RealtimeProviderUnavailableReason);
      }
    }

    // Token: 0x170001C1 RID: 449
    // (get) Token: 0x060007E4 RID: 2020 RVA: 0x00367850 File Offset: 0x00367850
    public string UserFacingMessage
    {
      get
      {
        RealtimeProviderUnavailableReason.RegionUnsupported regionUnsupported = this as RealtimeProviderUnavailableReason.RegionUnsupported;
        string text;
        if (regionUnsupported == null)
        {
          RealtimeProviderUnavailableReason.NetworkBlocksProvider networkBlocksProvider = this as RealtimeProviderUnavailableReason.NetworkBlocksProvider;
          if (networkBlocksProvider == null)
          {
            text = "";
          }
          else
          {
            text = "I can't reach " + networkBlocksProvider.ProviderName + " from this network. A firewall, proxy or DNS filter seems to be blocking it, so I can't answer until that changes.";
          }
        }
        else
        {
          DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(85, 3);
          defaultInterpolatedStringHandler.AppendLiteral("HeyClicky's voice runs on ");
          defaultInterpolatedStringHandler.AppendFormatted(regionUnsupported.ProviderName);
          defaultInterpolatedStringHandler.AppendLiteral(", and ");
          defaultInterpolatedStringHandler.AppendFormatted(regionUnsupported.ProviderName);
          defaultInterpolatedStringHandler.AppendLiteral(" isn't available in ");
          defaultInterpolatedStringHandler.AppendFormatted(regionUnsupported.LocationName ?? "your region");
          defaultInterpolatedStringHandler.AppendLiteral(". That's why I can't answer here.");
          text = defaultInterpolatedStringHandler.ToStringAndClear();
        }
        return text;
      }
    }

    // Token: 0x170001C2 RID: 450
    // (get) Token: 0x060007E5 RID: 2021 RVA: 0x0036790C File Offset: 0x0036790C
    public string AnalyticsKey
    {
      get
      {
        if (!(this is RealtimeProviderUnavailableReason.RegionUnsupported))
        {
          return "provider_blocked_by_network";
        }
        return "provider_region_unsupported";
      }
    }

    // Token: 0x060007E6 RID: 2022 RVA: 0x00367924 File Offset: 0x00367924
    [CompilerGenerated]
    public override string ToString()
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("RealtimeProviderUnavailableReason");
      stringBuilder.Append(" { ");
      if (this.PrintMembers(stringBuilder))
      {
        stringBuilder.Append(' ');
      }
      stringBuilder.Append('}');
      return stringBuilder.ToString();
    }

    // Token: 0x060007E7 RID: 2023 RVA: 0x00367970 File Offset: 0x00367970
    [CompilerGenerated]
    protected virtual bool PrintMembers(StringBuilder builder)
    {
      RuntimeHelpers.EnsureSufficientExecutionStack();
      builder.Append("UserFacingMessage = ");
      builder.Append(this.UserFacingMessage);
      builder.Append(", AnalyticsKey = ");
      builder.Append(this.AnalyticsKey);
      return true;
    }

    // Token: 0x060007E8 RID: 2024 RVA: 0x003679AC File Offset: 0x003679AC
    [NullableContext(2)]
    [CompilerGenerated]
    public static bool operator !=(RealtimeProviderUnavailableReason left, RealtimeProviderUnavailableReason right)
    {
      return !(left == right);
    }

    // Token: 0x060007E9 RID: 2025 RVA: 0x003679B8 File Offset: 0x003679B8
    [NullableContext(2)]
    [CompilerGenerated]
    public static bool operator ==(RealtimeProviderUnavailableReason left, RealtimeProviderUnavailableReason right)
    {
      return left == right || (left != null && left.Equals(right));
    }

    // Token: 0x060007EA RID: 2026 RVA: 0x003679CC File Offset: 0x003679CC
    [CompilerGenerated]
    public override int GetHashCode()
    {
      return EqualityComparer<Type>.Default.GetHashCode(this.EqualityContract);
    }

    // Token: 0x060007EB RID: 2027 RVA: 0x003679E0 File Offset: 0x003679E0
    [NullableContext(2)]
    [CompilerGenerated]
    public override bool Equals(object obj)
    {
      return this.Equals(obj as RealtimeProviderUnavailableReason);
    }

    // Token: 0x060007EC RID: 2028 RVA: 0x003679F0 File Offset: 0x003679F0
    [NullableContext(2)]
    [CompilerGenerated]
    public virtual bool Equals(RealtimeProviderUnavailableReason other)
    {
      return this == other || (other != null && this.EqualityContract == other.EqualityContract);
    }

    // Token: 0x060007EE RID: 2030 RVA: 0x00367A10 File Offset: 0x00367A10
    [CompilerGenerated]
    protected RealtimeProviderUnavailableReason(RealtimeProviderUnavailableReason original)
    {
    }

    // Token: 0x060007EF RID: 2031 RVA: 0x00367A18 File Offset: 0x00367A18
    protected RealtimeProviderUnavailableReason()
    {
    }

    // Token: 0x02000354 RID: 852
    [Nullable(0)]
    public sealed class RegionUnsupported : RealtimeProviderUnavailableReason, IEquatable<RealtimeProviderUnavailableReason.RegionUnsupported>
    {
      // Token: 0x06002865 RID: 10341 RVA: 0x0041B464 File Offset: 0x0041B464
      public RegionUnsupported(string ProviderName, [Nullable(2)] string LocationName)
      {
        this.ProviderName = ProviderName;
        this.LocationName = LocationName;
        base..ctor();
      }

      // Token: 0x170007E9 RID: 2025
      // (get) Token: 0x06002866 RID: 10342 RVA: 0x0041B47C File Offset: 0x0041B47C
      [CompilerGenerated]
      protected override Type EqualityContract
      {
        [CompilerGenerated]
        get
        {
          return typeof(RealtimeProviderUnavailableReason.RegionUnsupported);
        }
      }

      // Token: 0x170007EA RID: 2026
      // (get) Token: 0x06002867 RID: 10343 RVA: 0x0041B488 File Offset: 0x0041B488
      // (set) Token: 0x06002868 RID: 10344 RVA: 0x0041B490 File Offset: 0x0041B490
      public string ProviderName { get; set; }

      // Token: 0x170007EB RID: 2027
      // (get) Token: 0x06002869 RID: 10345 RVA: 0x0041B49C File Offset: 0x0041B49C
      // (set) Token: 0x0600286A RID: 10346 RVA: 0x0041B4A4 File Offset: 0x0041B4A4
      [Nullable(2)]
      public string LocationName
      {
        [NullableContext(2)]
        get;
        [NullableContext(2)]
        set;
      }

      // Token: 0x0600286B RID: 10347 RVA: 0x0041B4B0 File Offset: 0x0041B4B0
      [CompilerGenerated]
      public override string ToString()
      {
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.Append("RegionUnsupported");
        stringBuilder.Append(" { ");
        if (this.PrintMembers(stringBuilder))
        {
          stringBuilder.Append(' ');
        }
        stringBuilder.Append('}');
        return stringBuilder.ToString();
      }

      // Token: 0x0600286C RID: 10348 RVA: 0x0041B4FC File Offset: 0x0041B4FC
      [CompilerGenerated]
      protected override bool PrintMembers(StringBuilder builder)
      {
        if (base.PrintMembers(builder))
        {
          builder.Append(", ");
        }
        builder.Append("ProviderName = ");
        builder.Append(this.ProviderName);
        builder.Append(", LocationName = ");
        builder.Append(this.LocationName);
        return true;
      }

      // Token: 0x0600286D RID: 10349 RVA: 0x0041B554 File Offset: 0x0041B554
      [NullableContext(2)]
      [CompilerGenerated]
      public static bool operator !=(RealtimeProviderUnavailableReason.RegionUnsupported left, RealtimeProviderUnavailableReason.RegionUnsupported right)
      {
        return !(left == right);
      }

      // Token: 0x0600286E RID: 10350 RVA: 0x0041B560 File Offset: 0x0041B560
      [NullableContext(2)]
      [CompilerGenerated]
      public static bool operator ==(RealtimeProviderUnavailableReason.RegionUnsupported left, RealtimeProviderUnavailableReason.RegionUnsupported right)
      {
        return left == right || (left != null && left.Equals(right));
      }

      // Token: 0x0600286F RID: 10351 RVA: 0x0041B574 File Offset: 0x0041B574
      [CompilerGenerated]
      public override int GetHashCode()
      {
        return (base.GetHashCode() * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<ProviderName>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<LocationName>k__BackingField);
      }

      // Token: 0x06002870 RID: 10352 RVA: 0x0041B5AC File Offset: 0x0041B5AC
      [NullableContext(2)]
      [CompilerGenerated]
      public override bool Equals(object obj)
      {
        return this.Equals(obj as RealtimeProviderUnavailableReason.RegionUnsupported);
      }

      // Token: 0x06002871 RID: 10353 RVA: 0x0041B5BC File Offset: 0x0041B5BC
      [NullableContext(2)]
      [CompilerGenerated]
      public sealed override bool Equals(RealtimeProviderUnavailableReason other)
      {
        return this.Equals(other);
      }

      // Token: 0x06002872 RID: 10354 RVA: 0x0041B5C8 File Offset: 0x0041B5C8
      [NullableContext(2)]
      [CompilerGenerated]
      public bool Equals(RealtimeProviderUnavailableReason.RegionUnsupported other)
      {
        return this == other || (base.Equals(other) && EqualityComparer<string>.Default.Equals(this.<ProviderName>k__BackingField, other.<ProviderName>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<LocationName>k__BackingField, other.<LocationName>k__BackingField));
      }

      // Token: 0x06002874 RID: 10356 RVA: 0x0041B61C File Offset: 0x0041B61C
      [CompilerGenerated]
      private RegionUnsupported(RealtimeProviderUnavailableReason.RegionUnsupported original)
        : base(original)
      {
        this.ProviderName = original.<ProviderName>k__BackingField;
        this.LocationName = original.<LocationName>k__BackingField;
      }

      // Token: 0x06002875 RID: 10357 RVA: 0x0041B640 File Offset: 0x0041B640
      [CompilerGenerated]
      public void Deconstruct(out string ProviderName, [Nullable(2)] out string LocationName)
      {
        ProviderName = this.ProviderName;
        LocationName = this.LocationName;
      }
    }

    // Token: 0x02000355 RID: 853
    [Nullable(0)]
    public sealed class NetworkBlocksProvider : RealtimeProviderUnavailableReason, IEquatable<RealtimeProviderUnavailableReason.NetworkBlocksProvider>
    {
      // Token: 0x06002876 RID: 10358 RVA: 0x0041B654 File Offset: 0x0041B654
      public NetworkBlocksProvider(string ProviderName)
      {
        this.ProviderName = ProviderName;
        base..ctor();
      }

      // Token: 0x170007EC RID: 2028
      // (get) Token: 0x06002877 RID: 10359 RVA: 0x0041B664 File Offset: 0x0041B664
      [CompilerGenerated]
      protected override Type EqualityContract
      {
        [CompilerGenerated]
        get
        {
          return typeof(RealtimeProviderUnavailableReason.NetworkBlocksProvider);
        }
      }

      // Token: 0x170007ED RID: 2029
      // (get) Token: 0x06002878 RID: 10360 RVA: 0x0041B670 File Offset: 0x0041B670
      // (set) Token: 0x06002879 RID: 10361 RVA: 0x0041B678 File Offset: 0x0041B678
      public string ProviderName { get; set; }

      // Token: 0x0600287A RID: 10362 RVA: 0x0041B684 File Offset: 0x0041B684
      [CompilerGenerated]
      public override string ToString()
      {
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.Append("NetworkBlocksProvider");
        stringBuilder.Append(" { ");
        if (this.PrintMembers(stringBuilder))
        {
          stringBuilder.Append(' ');
        }
        stringBuilder.Append('}');
        return stringBuilder.ToString();
      }

      // Token: 0x0600287B RID: 10363 RVA: 0x0041B6D0 File Offset: 0x0041B6D0
      [CompilerGenerated]
      protected override bool PrintMembers(StringBuilder builder)
      {
        if (base.PrintMembers(builder))
        {
          builder.Append(", ");
        }
        builder.Append("ProviderName = ");
        builder.Append(this.ProviderName);
        return true;
      }

      // Token: 0x0600287C RID: 10364 RVA: 0x0041B704 File Offset: 0x0041B704
      [NullableContext(2)]
      [CompilerGenerated]
      public static bool operator !=(RealtimeProviderUnavailableReason.NetworkBlocksProvider left, RealtimeProviderUnavailableReason.NetworkBlocksProvider right)
      {
        return !(left == right);
      }

      // Token: 0x0600287D RID: 10365 RVA: 0x0041B710 File Offset: 0x0041B710
      [NullableContext(2)]
      [CompilerGenerated]
      public static bool operator ==(RealtimeProviderUnavailableReason.NetworkBlocksProvider left, RealtimeProviderUnavailableReason.NetworkBlocksProvider right)
      {
        return left == right || (left != null && left.Equals(right));
      }

      // Token: 0x0600287E RID: 10366 RVA: 0x0041B724 File Offset: 0x0041B724
      [CompilerGenerated]
      public override int GetHashCode()
      {
        return base.GetHashCode() * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<ProviderName>k__BackingField);
      }

      // Token: 0x0600287F RID: 10367 RVA: 0x0041B744 File Offset: 0x0041B744
      [NullableContext(2)]
      [CompilerGenerated]
      public override bool Equals(object obj)
      {
        return this.Equals(obj as RealtimeProviderUnavailableReason.NetworkBlocksProvider);
      }

      // Token: 0x06002880 RID: 10368 RVA: 0x0041B754 File Offset: 0x0041B754
      [NullableContext(2)]
      [CompilerGenerated]
      public sealed override bool Equals(RealtimeProviderUnavailableReason other)
      {
        return this.Equals(other);
      }

      // Token: 0x06002881 RID: 10369 RVA: 0x0041B760 File Offset: 0x0041B760
      [NullableContext(2)]
      [CompilerGenerated]
      public bool Equals(RealtimeProviderUnavailableReason.NetworkBlocksProvider other)
      {
        return this == other || (base.Equals(other) && EqualityComparer<string>.Default.Equals(this.<ProviderName>k__BackingField, other.<ProviderName>k__BackingField));
      }

      // Token: 0x06002883 RID: 10371 RVA: 0x0041B794 File Offset: 0x0041B794
      [CompilerGenerated]
      private NetworkBlocksProvider(RealtimeProviderUnavailableReason.NetworkBlocksProvider original)
        : base(original)
      {
        this.ProviderName = original.<ProviderName>k__BackingField;
      }

      // Token: 0x06002884 RID: 10372 RVA: 0x0041B7AC File Offset: 0x0041B7AC
      [CompilerGenerated]
      public void Deconstruct(out string ProviderName)
      {
        ProviderName = this.ProviderName;
      }
    }
  }
}
