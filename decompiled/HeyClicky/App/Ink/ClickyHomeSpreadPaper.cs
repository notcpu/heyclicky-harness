using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using Windows.UI;

namespace HeyClicky.App.Ink
{
  // Token: 0x020001B7 RID: 439
  public readonly struct ClickyHomeSpreadPaper : IEquatable<ClickyHomeSpreadPaper>
  {
    // Token: 0x06001745 RID: 5957 RVA: 0x005BFCD0 File Offset: 0x005BFCD0
    public ClickyHomeSpreadPaper(ClickyPaperStock? Stock)
    {
      this.Stock = Stock;
    }

    // Token: 0x17000422 RID: 1058
    // (get) Token: 0x06001746 RID: 5958 RVA: 0x005BFCDC File Offset: 0x005BFCDC
    // (set) Token: 0x06001747 RID: 5959 RVA: 0x005BFCE4 File Offset: 0x005BFCE4
    public ClickyPaperStock? Stock { get; set; }

    // Token: 0x06001748 RID: 5960 RVA: 0x005BFCF0 File Offset: 0x005BFCF0
    public static ClickyHomeSpreadPaper Of(ClickyPaperStock stock)
    {
      return new ClickyHomeSpreadPaper(new ClickyPaperStock?(stock));
    }

    // Token: 0x17000423 RID: 1059
    // (get) Token: 0x06001749 RID: 5961 RVA: 0x005BFD00 File Offset: 0x005BFD00
    public Color Ink
    {
      get
      {
        ClickyPaperStock? stock = this.Stock;
        if (stock != null)
        {
          ClickyPaperStock valueOrDefault = stock.GetValueOrDefault();
          return valueOrDefault.Ink();
        }
        return PaperColor.Hex("#2B2620");
      }
    }

    // Token: 0x17000424 RID: 1060
    // (get) Token: 0x0600174A RID: 5962 RVA: 0x005BFD38 File Offset: 0x005BFD38
    public Color Quiet
    {
      get
      {
        ClickyPaperStock? stock = this.Stock;
        if (stock != null)
        {
          ClickyPaperStock valueOrDefault = stock.GetValueOrDefault();
          return valueOrDefault.Ink().Opacity(0.58);
        }
        return PaperColor.Hex("#8A8F9E");
      }
    }

    // Token: 0x17000425 RID: 1061
    // (get) Token: 0x0600174B RID: 5963 RVA: 0x005BFD80 File Offset: 0x005BFD80
    public Color Deep
    {
      get
      {
        ClickyPaperStock? stock = this.Stock;
        if (stock != null)
        {
          ClickyPaperStock valueOrDefault = stock.GetValueOrDefault();
          return valueOrDefault.Deep();
        }
        return ClickyPaperInk.ClickyBlue;
      }
    }

    // Token: 0x17000426 RID: 1062
    // (get) Token: 0x0600174C RID: 5964 RVA: 0x005BFDB4 File Offset: 0x005BFDB4
    public Color TitleWash
    {
      get
      {
        return this.Stock.GetValueOrDefault(ClickyPaperStock.Cream).Wash();
      }
    }

    // Token: 0x17000427 RID: 1063
    // (get) Token: 0x0600174D RID: 5965 RVA: 0x005BFDD8 File Offset: 0x005BFDD8
    public Color Bottom
    {
      get
      {
        ClickyPaperStock? stock = this.Stock;
        if (stock != null)
        {
          ClickyPaperStock valueOrDefault = stock.GetValueOrDefault();
          return valueOrDefault.Paper();
        }
        return PaperColor.Hex("#F7F2E4");
      }
    }

    // Token: 0x17000428 RID: 1064
    // (get) Token: 0x0600174E RID: 5966 RVA: 0x005BFE10 File Offset: 0x005BFE10
    public Color Pencil
    {
      get
      {
        ClickyPaperStock? stock = this.Stock;
        if (stock != null)
        {
          ClickyPaperStock valueOrDefault = stock.GetValueOrDefault();
          return valueOrDefault.Ink().Opacity(0.66);
        }
        return PaperColor.Hex("#6A6F7A");
      }
    }

    // Token: 0x17000429 RID: 1065
    // (get) Token: 0x0600174F RID: 5967 RVA: 0x005BFE58 File Offset: 0x005BFE58
    [Nullable(1)]
    public PaperFill Fill
    {
      [NullableContext(1)]
      get
      {
        ClickyPaperStock? stock = this.Stock;
        if (stock != null)
        {
          ClickyPaperStock valueOrDefault = stock.GetValueOrDefault();
          return PaperFill.Of(valueOrDefault.Paper());
        }
        return PaperFill.Vertical(new ValueTuple<double, Color>[]
        {
          new ValueTuple<double, Color>(0.0, PaperColor.Hex("#FFFCF3")),
          new ValueTuple<double, Color>(0.4, PaperColor.Hex("#FBF7EC")),
          new ValueTuple<double, Color>(1.0, PaperColor.Hex("#F7F2E4"))
        });
      }
    }

    // Token: 0x06001750 RID: 5968 RVA: 0x005BFEF4 File Offset: 0x005BFEF4
    [CompilerGenerated]
    public override string ToString()
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("ClickyHomeSpreadPaper");
      stringBuilder.Append(" { ");
      if (this.PrintMembers(stringBuilder))
      {
        stringBuilder.Append(' ');
      }
      stringBuilder.Append('}');
      return stringBuilder.ToString();
    }

    // Token: 0x06001751 RID: 5969 RVA: 0x005BFF40 File Offset: 0x005BFF40
    [CompilerGenerated]
    private bool PrintMembers(StringBuilder builder)
    {
      builder.Append("Stock = ");
      builder.Append(this.Stock.ToString());
      builder.Append(", Ink = ");
      builder.Append(this.Ink.ToString());
      builder.Append(", Quiet = ");
      builder.Append(this.Quiet.ToString());
      builder.Append(", Deep = ");
      builder.Append(this.Deep.ToString());
      builder.Append(", TitleWash = ");
      builder.Append(this.TitleWash.ToString());
      builder.Append(", Bottom = ");
      builder.Append(this.Bottom.ToString());
      builder.Append(", Pencil = ");
      builder.Append(this.Pencil.ToString());
      builder.Append(", Fill = ");
      builder.Append(this.Fill);
      return true;
    }

    // Token: 0x06001752 RID: 5970 RVA: 0x005C0078 File Offset: 0x005C0078
    [CompilerGenerated]
    public static bool operator !=(ClickyHomeSpreadPaper left, ClickyHomeSpreadPaper right)
    {
      return !(left == right);
    }

    // Token: 0x06001753 RID: 5971 RVA: 0x005C0084 File Offset: 0x005C0084
    [CompilerGenerated]
    public static bool operator ==(ClickyHomeSpreadPaper left, ClickyHomeSpreadPaper right)
    {
      return left.Equals(right);
    }

    // Token: 0x06001754 RID: 5972 RVA: 0x005C0090 File Offset: 0x005C0090
    [CompilerGenerated]
    public override int GetHashCode()
    {
      return EqualityComparer<ClickyPaperStock?>.Default.GetHashCode(this.<Stock>k__BackingField);
    }

    // Token: 0x06001755 RID: 5973 RVA: 0x005C00A4 File Offset: 0x005C00A4
    [CompilerGenerated]
    public override bool Equals(object obj)
    {
      return obj is ClickyHomeSpreadPaper && this.Equals((ClickyHomeSpreadPaper)obj);
    }

    // Token: 0x06001756 RID: 5974 RVA: 0x005C00BC File Offset: 0x005C00BC
    [CompilerGenerated]
    public bool Equals(ClickyHomeSpreadPaper other)
    {
      return EqualityComparer<ClickyPaperStock?>.Default.Equals(this.<Stock>k__BackingField, other.<Stock>k__BackingField);
    }

    // Token: 0x06001757 RID: 5975 RVA: 0x005C00D4 File Offset: 0x005C00D4
    [CompilerGenerated]
    public void Deconstruct(out ClickyPaperStock? Stock)
    {
      Stock = this.Stock;
    }

    // Token: 0x04000B5A RID: 2906
    public static readonly ClickyHomeSpreadPaper Note = new ClickyHomeSpreadPaper(null);
  }
}
