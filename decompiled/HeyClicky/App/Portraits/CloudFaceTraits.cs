using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace HeyClicky.App.Portraits
{
  // Token: 0x0200010A RID: 266
  [NullableContext(1)]
  [Nullable(0)]
  public sealed class CloudFaceTraits
  {
    // Token: 0x17000282 RID: 642
    // (get) Token: 0x06000DEF RID: 3567 RVA: 0x0056E778 File Offset: 0x0056E778
    public double BaseHue { get; }

    // Token: 0x17000283 RID: 643
    // (get) Token: 0x06000DF0 RID: 3568 RVA: 0x0056E780 File Offset: 0x0056E780
    public IReadOnlyList<CloudFaceTraits.Blob> ColorBlobs { get; }

    // Token: 0x17000284 RID: 644
    // (get) Token: 0x06000DF1 RID: 3569 RVA: 0x0056E788 File Offset: 0x0056E788
    public bool IsWide { get; }

    // Token: 0x17000285 RID: 645
    // (get) Token: 0x06000DF2 RID: 3570 RVA: 0x0056E790 File Offset: 0x0056E790
    public bool IsMirrored { get; }

    // Token: 0x17000286 RID: 646
    // (get) Token: 0x06000DF3 RID: 3571 RVA: 0x0056E798 File Offset: 0x0056E798
    public double RestingGazeDirection { get; }

    // Token: 0x06000DF4 RID: 3572 RVA: 0x0056E7A0 File Offset: 0x0056E7A0
    public CloudFaceTraits(string seedString, double? baseHueOverride = null)
    {
      ulong num = 5381UL;
      foreach (Rune rune in seedString.EnumerateRunes())
      {
        num = num * 33UL + (ulong)((long)rune.Value);
      }
      CloudFaceTraits.<>c__DisplayClass16_0 CS$<>8__locals1;
      CS$<>8__locals1.state = num;
      double num2 = CloudFaceTraits.<.ctor>g__Next|16_0(ref CS$<>8__locals1);
      this.BaseHue = baseHueOverride.GetValueOrDefault(num2);
      List<CloudFaceTraits.Blob> list = new List<CloudFaceTraits.Blob>();
      int num3 = 2 + ((CloudFaceTraits.<.ctor>g__Next|16_0(ref CS$<>8__locals1) > 0.4) ? 1 : 0);
      for (int i = 0; i < num3; i++)
      {
        CloudFaceTraits.<.ctor>g__Next|16_0(ref CS$<>8__locals1);
        list.Add(new CloudFaceTraits.Blob(0.22 + CloudFaceTraits.<.ctor>g__Next|16_0(ref CS$<>8__locals1) * 0.56, 0.25 + CloudFaceTraits.<.ctor>g__Next|16_0(ref CS$<>8__locals1) * 0.5, 0.5 + CloudFaceTraits.<.ctor>g__Next|16_0(ref CS$<>8__locals1) * 0.35));
      }
      this.ColorBlobs = list;
      this.IsWide = CloudFaceTraits.<.ctor>g__Next|16_0(ref CS$<>8__locals1) > 0.5;
      this.IsMirrored = CloudFaceTraits.<.ctor>g__Next|16_0(ref CS$<>8__locals1) > 0.5;
      this.RestingGazeDirection = (CloudFaceTraits.<.ctor>g__Next|16_0(ref CS$<>8__locals1) - 0.5) * 2.0;
    }

    // Token: 0x06000DF5 RID: 3573 RVA: 0x0056E918 File Offset: 0x0056E918
    [CompilerGenerated]
    internal static double <.ctor>g__Next|16_0(ref CloudFaceTraits.<>c__DisplayClass16_0 A_0)
    {
      A_0.state += 11400714819323198485UL;
      ulong state = A_0.state;
      ulong num = (state ^ (state >> 30)) * 13787848793156543929UL;
      ulong num2 = (num ^ (num >> 27)) * 10723151780598845931UL;
      return (num2 ^ (num2 >> 31)) % 10000UL / 10000.0;
    }

    // Token: 0x020005E1 RID: 1505
    [NullableContext(0)]
    public readonly struct Blob : IEquatable<CloudFaceTraits.Blob>
    {
      // Token: 0x06002FE9 RID: 12265 RVA: 0x006661B0 File Offset: 0x006661B0
      public Blob(double CenterX, double CenterY, double Diameter)
      {
        this.CenterX = CenterX;
        this.CenterY = CenterY;
        this.Diameter = Diameter;
      }

      // Token: 0x17000754 RID: 1876
      // (get) Token: 0x06002FEA RID: 12266 RVA: 0x006661C8 File Offset: 0x006661C8
      // (set) Token: 0x06002FEB RID: 12267 RVA: 0x006661D0 File Offset: 0x006661D0
      public double CenterX { get; set; }

      // Token: 0x17000755 RID: 1877
      // (get) Token: 0x06002FEC RID: 12268 RVA: 0x006661DC File Offset: 0x006661DC
      // (set) Token: 0x06002FED RID: 12269 RVA: 0x006661E4 File Offset: 0x006661E4
      public double CenterY { get; set; }

      // Token: 0x17000756 RID: 1878
      // (get) Token: 0x06002FEE RID: 12270 RVA: 0x006661F0 File Offset: 0x006661F0
      // (set) Token: 0x06002FEF RID: 12271 RVA: 0x006661F8 File Offset: 0x006661F8
      public double Diameter { get; set; }

      // Token: 0x06002FF0 RID: 12272 RVA: 0x00666204 File Offset: 0x00666204
      [CompilerGenerated]
      public override string ToString()
      {
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.Append("Blob");
        stringBuilder.Append(" { ");
        if (this.PrintMembers(stringBuilder))
        {
          stringBuilder.Append(' ');
        }
        stringBuilder.Append('}');
        return stringBuilder.ToString();
      }

      // Token: 0x06002FF1 RID: 12273 RVA: 0x00666250 File Offset: 0x00666250
      [CompilerGenerated]
      private bool PrintMembers(StringBuilder builder)
      {
        builder.Append("CenterX = ");
        builder.Append(this.CenterX.ToString());
        builder.Append(", CenterY = ");
        builder.Append(this.CenterY.ToString());
        builder.Append(", Diameter = ");
        builder.Append(this.Diameter.ToString());
        return true;
      }

      // Token: 0x06002FF2 RID: 12274 RVA: 0x006662D4 File Offset: 0x006662D4
      [CompilerGenerated]
      public static bool operator !=(CloudFaceTraits.Blob left, CloudFaceTraits.Blob right)
      {
        return !(left == right);
      }

      // Token: 0x06002FF3 RID: 12275 RVA: 0x006662E0 File Offset: 0x006662E0
      [CompilerGenerated]
      public static bool operator ==(CloudFaceTraits.Blob left, CloudFaceTraits.Blob right)
      {
        return left.Equals(right);
      }

      // Token: 0x06002FF4 RID: 12276 RVA: 0x006662EC File Offset: 0x006662EC
      [CompilerGenerated]
      public override int GetHashCode()
      {
        return (EqualityComparer<double>.Default.GetHashCode(this.<CenterX>k__BackingField) * -1521134295 + EqualityComparer<double>.Default.GetHashCode(this.<CenterY>k__BackingField)) * -1521134295 + EqualityComparer<double>.Default.GetHashCode(this.<Diameter>k__BackingField);
      }

      // Token: 0x06002FF5 RID: 12277 RVA: 0x0066632C File Offset: 0x0066632C
      [CompilerGenerated]
      public override bool Equals(object obj)
      {
        return obj is CloudFaceTraits.Blob && this.Equals((CloudFaceTraits.Blob)obj);
      }

      // Token: 0x06002FF6 RID: 12278 RVA: 0x00666344 File Offset: 0x00666344
      [CompilerGenerated]
      public bool Equals(CloudFaceTraits.Blob other)
      {
        return EqualityComparer<double>.Default.Equals(this.<CenterX>k__BackingField, other.<CenterX>k__BackingField) && EqualityComparer<double>.Default.Equals(this.<CenterY>k__BackingField, other.<CenterY>k__BackingField) && EqualityComparer<double>.Default.Equals(this.<Diameter>k__BackingField, other.<Diameter>k__BackingField);
      }

      // Token: 0x06002FF7 RID: 12279 RVA: 0x0066639C File Offset: 0x0066639C
      [CompilerGenerated]
      public void Deconstruct(out double CenterX, out double CenterY, out double Diameter)
      {
        CenterX = this.CenterX;
        CenterY = this.CenterY;
        Diameter = this.Diameter;
      }
    }
  }
}
