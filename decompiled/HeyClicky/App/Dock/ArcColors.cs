using System;
using System.Runtime.CompilerServices;
using HeyClicky.App.Design;
using Windows.UI;

namespace HeyClicky.App.Dock
{
  // Token: 0x02000316 RID: 790
  public static class ArcColors
  {
    // Token: 0x06002251 RID: 8785 RVA: 0x00622544 File Offset: 0x00622544
    public static Color Of([TupleElementNames(new string[] { "Rgb", "Opacity" })] ValueTuple<uint, double> c)
    {
      return DS.Hex(c.Item1, c.Item2);
    }

    // Token: 0x06002252 RID: 8786 RVA: 0x00622558 File Offset: 0x00622558
    public static Color State(ArcCardState state)
    {
      return ArcColors.Of(ArcCardStates.Color(state));
    }

    // Token: 0x06002253 RID: 8787 RVA: 0x00622568 File Offset: 0x00622568
    public static Color BlendWithWhite(Color c, double fraction)
    {
      return Color.FromArgb(c.A, (byte)Math.Round((double)c.R + (double)(byte.MaxValue - c.R) * fraction), (byte)Math.Round((double)c.G + (double)(byte.MaxValue - c.G) * fraction), (byte)Math.Round((double)c.B + (double)(byte.MaxValue - c.B) * fraction));
    }

    // Token: 0x06002254 RID: 8788 RVA: 0x006225E0 File Offset: 0x006225E0
    public static Color Opacity(this Color c, double alpha)
    {
      return c.WithAlpha(alpha);
    }

    // Token: 0x04001083 RID: 4227
    public static readonly Color Ink = DS.Hex(1842724U, 1.0);

    // Token: 0x04001084 RID: 4228
    public static readonly Color Coral = DS.Hex(14242877U, 1.0);

    // Token: 0x04001085 RID: 4229
    public static readonly Color Working = DS.Hex(1755979U, 1.0);

    // Token: 0x04001086 RID: 4230
    public static readonly Color Blue = DS.Hex(3112182U, 1.0);

    // Token: 0x04001087 RID: 4231
    public static readonly Color TitleGreyEdge = DS.Hex(14079447U, 1.0);

    // Token: 0x04001088 RID: 4232
    public static readonly Color TitleGreyMiddle = DS.Hex(15790320U, 1.0);
  }
}
