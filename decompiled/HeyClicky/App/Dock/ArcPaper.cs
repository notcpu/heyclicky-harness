using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using HeyClicky.App.Design;
using Windows.UI;

namespace HeyClicky.App.Dock
{
  // Token: 0x02000318 RID: 792
  public readonly struct ArcPaper : IEquatable<ArcPaper>
  {
    // Token: 0x06002256 RID: 8790 RVA: 0x0062268C File Offset: 0x0062268C
    public ArcPaper(PaperStock Stock)
    {
      this.Stock = Stock;
    }

    // Token: 0x1700065C RID: 1628
    // (get) Token: 0x06002257 RID: 8791 RVA: 0x00622698 File Offset: 0x00622698
    // (set) Token: 0x06002258 RID: 8792 RVA: 0x006226A0 File Offset: 0x006226A0
    public PaperStock Stock { get; set; }

    // Token: 0x1700065D RID: 1629
    // (get) Token: 0x06002259 RID: 8793 RVA: 0x006226AC File Offset: 0x006226AC
    public Color Fill
    {
      get
      {
        Color color;
        switch (this.Stock)
        {
        case PaperStock.Sky:
          color = DS.Hex(15462909U, 1.0);
          break;
        case PaperStock.Butter:
          color = DS.Hex(16578019U, 1.0);
          break;
        case PaperStock.Mint:
          color = DS.Hex(15333100U, 1.0);
          break;
        case PaperStock.Periwinkle:
          color = DS.Hex(15789821U, 1.0);
          break;
        case PaperStock.Peach:
          color = DS.Hex(16641765U, 1.0);
          break;
        case PaperStock.Blush:
          color = DS.Hex(16574961U, 1.0);
          break;
        case PaperStock.Kraft:
          color = DS.Hex(16051938U, 1.0);
          break;
        default:
          color = DS.Hex(16513004U, 1.0);
          break;
        }
        return color;
      }
    }

    // Token: 0x1700065E RID: 1630
    // (get) Token: 0x0600225A RID: 8794 RVA: 0x0062279C File Offset: 0x0062279C
    public Color TitleWash
    {
      get
      {
        Color color;
        switch (this.Stock)
        {
        case PaperStock.Sky:
          color = DS.Hex(14149119U, 1.0);
          break;
        case PaperStock.Butter:
          color = DS.Hex(16707271U, 1.0);
          break;
        case PaperStock.Mint:
          color = DS.Hex(13889756U, 1.0);
          break;
        case PaperStock.Periwinkle:
          color = DS.Hex(14933759U, 1.0);
          break;
        case PaperStock.Peach:
          color = DS.Hex(16768457U, 1.0);
          break;
        case PaperStock.Blush:
          color = DS.Hex(16762849U, 1.0);
          break;
        case PaperStock.Kraft:
          color = DS.Hex(15655887U, 1.0);
          break;
        default:
          color = DS.Hex(15985100U, 1.0);
          break;
        }
        return color;
      }
    }

    // Token: 0x1700065F RID: 1631
    // (get) Token: 0x0600225B RID: 8795 RVA: 0x0062288C File Offset: 0x0062288C
    public Color Ink
    {
      get
      {
        Color color;
        switch (this.Stock)
        {
        case PaperStock.Sky:
          color = DS.Hex(924191U, 1.0);
          break;
        case PaperStock.Butter:
          color = DS.Hex(1840646U, 1.0);
          break;
        case PaperStock.Mint:
          color = DS.Hex(992279U, 1.0);
          break;
        case PaperStock.Periwinkle:
          color = DS.Hex(1317432U, 1.0);
          break;
        case PaperStock.Peach:
          color = DS.Hex(2955531U, 1.0);
          break;
        case PaperStock.Blush:
          color = DS.Hex(2888728U, 1.0);
          break;
        case PaperStock.Kraft:
          color = DS.Hex(2826516U, 1.0);
          break;
        default:
          color = DS.Hex(2827808U, 1.0);
          break;
        }
        return color;
      }
    }

    // Token: 0x17000660 RID: 1632
    // (get) Token: 0x0600225C RID: 8796 RVA: 0x0062297C File Offset: 0x0062297C
    public Color Deep
    {
      get
      {
        Color color;
        switch (this.Stock)
        {
        case PaperStock.Sky:
          color = DS.Hex(1784460U, 1.0);
          break;
        case PaperStock.Butter:
          color = DS.Hex(5913088U, 1.0);
          break;
        case PaperStock.Mint:
          color = DS.Hex(1923635U, 1.0);
          break;
        case PaperStock.Periwinkle:
          color = DS.Hex(2832784U, 1.0);
          break;
        case PaperStock.Peach:
          color = DS.Hex(9059087U, 1.0);
          break;
        case PaperStock.Blush:
          color = DS.Hex(8003381U, 1.0);
          break;
        case PaperStock.Kraft:
          color = DS.Hex(5913878U, 1.0);
          break;
        default:
          color = DS.Hex(2972643U, 1.0);
          break;
        }
        return color;
      }
    }

    // Token: 0x0600225D RID: 8797 RVA: 0x00622A6C File Offset: 0x00622A6C
    [CompilerGenerated]
    public override string ToString()
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("ArcPaper");
      stringBuilder.Append(" { ");
      if (this.PrintMembers(stringBuilder))
      {
        stringBuilder.Append(' ');
      }
      stringBuilder.Append('}');
      return stringBuilder.ToString();
    }

    // Token: 0x0600225E RID: 8798 RVA: 0x00622AB8 File Offset: 0x00622AB8
    [CompilerGenerated]
    private bool PrintMembers(StringBuilder builder)
    {
      builder.Append("Stock = ");
      builder.Append(this.Stock.ToString());
      builder.Append(", Fill = ");
      builder.Append(this.Fill.ToString());
      builder.Append(", TitleWash = ");
      builder.Append(this.TitleWash.ToString());
      builder.Append(", Ink = ");
      builder.Append(this.Ink.ToString());
      builder.Append(", Deep = ");
      builder.Append(this.Deep.ToString());
      return true;
    }

    // Token: 0x0600225F RID: 8799 RVA: 0x00622B8C File Offset: 0x00622B8C
    [CompilerGenerated]
    public static bool operator !=(ArcPaper left, ArcPaper right)
    {
      return !(left == right);
    }

    // Token: 0x06002260 RID: 8800 RVA: 0x00622B98 File Offset: 0x00622B98
    [CompilerGenerated]
    public static bool operator ==(ArcPaper left, ArcPaper right)
    {
      return left.Equals(right);
    }

    // Token: 0x06002261 RID: 8801 RVA: 0x00622BA4 File Offset: 0x00622BA4
    [CompilerGenerated]
    public override int GetHashCode()
    {
      return EqualityComparer<PaperStock>.Default.GetHashCode(this.<Stock>k__BackingField);
    }

    // Token: 0x06002262 RID: 8802 RVA: 0x00622BB8 File Offset: 0x00622BB8
    [CompilerGenerated]
    public override bool Equals(object obj)
    {
      return obj is ArcPaper && this.Equals((ArcPaper)obj);
    }

    // Token: 0x06002263 RID: 8803 RVA: 0x00622BD0 File Offset: 0x00622BD0
    [CompilerGenerated]
    public bool Equals(ArcPaper other)
    {
      return EqualityComparer<PaperStock>.Default.Equals(this.<Stock>k__BackingField, other.<Stock>k__BackingField);
    }

    // Token: 0x06002264 RID: 8804 RVA: 0x00622BE8 File Offset: 0x00622BE8
    [CompilerGenerated]
    public void Deconstruct(out PaperStock Stock)
    {
      Stock = this.Stock;
    }
  }
}
