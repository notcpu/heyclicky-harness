using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Runtime.CompilerServices;

namespace HeyClicky.App.Portraits
{
  // Token: 0x020000FD RID: 253
  [NullableContext(1)]
  [Nullable(0)]
  public static class PortraitArt
  {
    // Token: 0x06000D4E RID: 3406 RVA: 0x00565D2C File Offset: 0x00565D2C
    public static IReadOnlyList<HairPiece> Back(Hairstyle style, bool faceOnly = true)
    {
      PortraitPack portraitPack = style.Pack();
      if (portraitPack.IsCloud())
      {
        return Array.Empty<HairPiece>();
      }
      if (portraitPack == PortraitPack.StorybookSpirits)
      {
        return PortraitArt.StorybookBack(style, faceOnly);
      }
      if (portraitPack == PortraitPack.ArcadeAllStars)
      {
        return PortraitArt.ArcadeBack(style);
      }
      if (portraitPack == PortraitPack.KiCrew)
      {
        return Array.Empty<HairPiece>();
      }
      switch (style)
      {
      case Hairstyle.Bob:
      {
        HairPiece[] array = new HairPiece[1];
        array[0] = PortraitArt.P(delegate(PPath p)
        {
          p.M(39.0, 144.0).C(38.0, 79.0, 80.0, 35.0, 127.0, 35.0).C(155.0, 33.0, 169.0, 43.0, 182.0, 58.0)
            .C(225.0, 54.0, 263.0, 76.0, 269.0, 121.0)
            .C(274.0, 154.0, 288.0, 168.0, 288.0, 186.0)
            .C(288.0, 230.0, 250.0, 248.0, 220.0, 244.0)
            .C(174.0, 251.0, 114.0, 260.0, 72.0, 255.0)
            .C(50.0, 255.0, 35.0, 241.0, 37.0, 224.0)
            .C(17.0, 212.0, 10.0, 199.0, 20.0, 179.0)
            .C(33.0, 165.0, 36.0, 163.0, 39.0, 144.0)
            .Z();
        });
        return array;
      }
      case Hairstyle.Topknot:
      {
        HairPiece[] array2 = new HairPiece[3];
        array2[0] = PortraitArt.E(134.0, 5.0, 88.0, 86.0);
        array2[1] = PortraitArt.P(delegate(PPath p)
        {
          p.M(43.0, 144.0).C(40.0, 96.0, 85.0, 66.0, 151.0, 66.0).C(227.0, 63.0, 282.0, 98.0, 282.0, 145.0)
            .C(282.0, 165.0, 275.0, 186.0, 264.0, 199.0)
            .C(245.0, 161.0, 222.0, 134.0, 164.0, 132.0)
            .C(108.0, 132.0, 79.0, 162.0, 60.0, 199.0)
            .C(48.0, 185.0, 43.0, 165.0, 43.0, 144.0)
            .Z();
        });
        array2[2] = new HairPiece(PPath.Oval(154.0, 71.0, 53.0, 13.0), new PieceFill?(PieceFill.Accent), true);
        return array2;
      }
      case Hairstyle.Pixie:
      {
        HairPiece[] array3 = new HairPiece[1];
        array3[0] = PortraitArt.P(delegate(PPath p)
        {
          p.M(45.0, 209.0).C(32.0, 182.0, 31.0, 154.0, 45.0, 126.0).C(67.0, 70.0, 116.0, 43.0, 182.0, 52.0)
            .C(234.0, 49.0, 280.0, 90.0, 278.0, 151.0)
            .C(275.0, 178.0, 269.0, 199.0, 259.0, 214.0)
            .C(186.0, 231.0, 109.0, 231.0, 45.0, 209.0)
            .Z();
        });
        return array3;
      }
      case Hairstyle.TwinBuns:
        return new HairPiece[]
        {
          PortraitArt.E(10.0, 46.0, 77.0, 80.0),
          PortraitArt.E(233.0, 46.0, 77.0, 80.0),
          PortraitArt.E(10.0, 183.0, 77.0, 76.0),
          PortraitArt.E(233.0, 182.0, 77.0, 76.0),
          PortraitArt.E(43.0, 58.0, 236.0, 190.0),
          new HairPiece(PPath.Oval(67.0, 90.0, 15.0, 30.0), new PieceFill?(PieceFill.Accent), true),
          new HairPiece(PPath.Oval(238.0, 90.0, 15.0, 30.0), new PieceFill?(PieceFill.Accent), true)
        };
      case Hairstyle.Waves:
      {
        if (faceOnly)
        {
          HairPiece[] array4 = new HairPiece[1];
          array4[0] = PortraitArt.P(delegate(PPath p)
          {
            p.M(41.0, 144.0).C(32.0, 56.0, 95.0, 25.0, 163.0, 35.0).C(235.0, 25.0, 283.0, 75.0, 277.0, 143.0)
              .C(271.0, 177.0, 293.0, 189.0, 289.0, 212.0)
              .C(287.0, 228.0, 277.0, 229.0, 284.0, 244.0)
              .C(270.0, 255.0, 254.0, 243.0, 250.0, 227.0)
              .C(246.0, 207.0, 257.0, 189.0, 257.0, 166.0)
              .L(70.0, 166.0)
              .C(67.0, 190.0, 83.0, 208.0, 69.0, 225.0)
              .C(62.0, 240.0, 49.0, 245.0, 34.0, 237.0)
              .C(43.0, 222.0, 24.0, 211.0, 31.0, 193.0)
              .C(35.0, 174.0, 44.0, 166.0, 41.0, 144.0)
              .Z();
          });
          return array4;
        }
        HairPiece[] array5 = new HairPiece[1];
        array5[0] = PortraitArt.P(delegate(PPath p)
        {
          p.M(41.0, 144.0).C(32.0, 56.0, 95.0, 25.0, 163.0, 35.0).C(235.0, 25.0, 283.0, 75.0, 277.0, 143.0)
            .C(270.0, 195.0, 309.0, 217.0, 287.0, 250.0)
            .C(263.0, 277.0, 300.0, 295.0, 271.0, 315.0)
            .C(244.0, 340.0, 226.0, 314.0, 209.0, 330.0)
            .L(112.0, 325.0)
            .C(89.0, 337.0, 76.0, 311.0, 51.0, 325.0)
            .C(22.0, 317.0, 47.0, 288.0, 28.0, 270.0)
            .C(9.0, 246.0, 46.0, 224.0, 30.0, 204.0)
            .C(17.0, 182.0, 44.0, 169.0, 41.0, 144.0)
            .Z();
        });
        return array5;
      }
      case Hairstyle.Curls:
        return new HairPiece[]
        {
          new HairPiece(PortraitArt.Scallop(160.0, 132.0, 133.0, 110.0, 11, 6.0), new PieceFill?(PieceFill.Hair), true)
        };
      case Hairstyle.Ponytail:
      {
        HairPiece[] array6 = new HairPiece[3];
        array6[0] = PortraitArt.P(delegate(PPath p)
        {
          p.M(241.0, 89.0).C(286.0, 56.0, 316.0, 98.0, 293.0, 143.0).C(276.0, 178.0, 299.0, 219.0, 307.0, 237.0)
            .C(279.0, 249.0, 270.0, 273.0, 244.0, 248.0)
            .C(223.0, 225.0, 236.0, 188.0, 245.0, 160.0)
            .C(257.0, 132.0, 216.0, 122.0, 241.0, 89.0)
            .Z();
        });
        array6[1] = PortraitArt.E(45.0, 53.0, 233.0, 188.0);
        array6[2] = new HairPiece(PPath.Oval(243.0, 86.0, 25.0, 21.0), new PieceFill?(PieceFill.Accent), true);
        return array6;
      }
      case Hairstyle.Undercut:
      {
        HairPiece[] array7 = new HairPiece[1];
        array7[0] = PortraitArt.P(delegate(PPath p)
        {
          p.M(43.0, 200.0).L(41.0, 120.0).C(42.0, 66.0, 81.0, 39.0, 137.0, 47.0)
            .L(211.0, 29.0)
            .L(201.0, 43.0)
            .L(244.0, 34.0)
            .L(233.0, 54.0)
            .C(271.0, 58.0, 285.0, 94.0, 274.0, 147.0)
            .L(267.0, 207.0)
            .Z();
        });
        return array7;
      }
      case Hairstyle.Braids:
      {
        List<HairPiece> list = new List<HairPiece> { PortraitArt.E(42.0, 55.0, 237.0, 192.0) };
        double[] array8 = new double[] { 48.0, 246.0 };
        for (int i = 0; i < array8.Length; i++)
        {
          double x = array8[i];
          for (int j = 0; j < 5; j++)
          {
            double num = (double)((j % 2 == 0) ? (-3) : 3);
            list.Add(PortraitArt.E(x + num, (double)(180 + j * 24), 36.0, 39.0));
          }
          list.Add(new HairPiece(PPath.Oval(x + 1.0, 292.0, 27.0, 14.0), new PieceFill?(PieceFill.Accent), true));
          list.Add(PortraitArt.P(delegate(PPath p)
          {
            p.M(x + 3.0, 306.0).L(x + 27.0, 306.0).L(x + 28.0, 332.0)
              .Q(x + 13.0, 339.0, x + 2.0, 329.0)
              .Z();
          }));
        }
        return list;
      }
      case Hairstyle.Puff:
        return new HairPiece[]
        {
          new HairPiece(PortraitArt.Scallop(160.0, 128.0, 139.0, 112.0, 16, 5.0), new PieceFill?(PieceFill.Hair), true)
        };
      default:
        return Array.Empty<HairPiece>();
      }
    }

    // Token: 0x06000D4F RID: 3407 RVA: 0x005662C4 File Offset: 0x005662C4
    public static IReadOnlyList<HairPiece> Front(Hairstyle style)
    {
      PortraitPack portraitPack = style.Pack();
      if (portraitPack.IsCloud() || portraitPack == PortraitPack.KiCrew)
      {
        return Array.Empty<HairPiece>();
      }
      if (portraitPack == PortraitPack.StorybookSpirits)
      {
        return PortraitArt.StorybookFront(style);
      }
      if (portraitPack == PortraitPack.ArcadeAllStars)
      {
        return PortraitArt.ArcadeFront(style);
      }
      switch (style)
      {
      case Hairstyle.Bob:
        return PortraitArt.Parted(182.0, 58.0, 148.0, 147.0, 35.0);
      case Hairstyle.Topknot:
      {
        List<HairPiece> list = PortraitArt.Parted(161.0, 82.0, 133.0, 135.0, 66.0).ToList<HairPiece>();
        list.Add(new HairPiece(new PPath().M(174.0, 24.0).C(204.0, 22.0, 210.0, 61.0, 183.0, 61.0).C(161.0, 62.0, 160.0, 41.0, 178.0, 39.0)
          .C(191.0, 38.0, 193.0, 51.0, 184.0, 52.0), null, true));
        return list;
      }
      case Hairstyle.Pixie:
      {
        HairPiece[] array = new HairPiece[1];
        array[0] = PortraitArt.P(delegate(PPath p)
        {
          p.M(28.0, 133.0).C(56.0, 76.0, 109.0, 42.0, 181.0, 50.0).C(186.0, 37.0, 179.0, 23.0, 177.0, 19.0)
            .C(194.0, 24.0, 203.0, 36.0, 206.0, 47.0)
            .C(215.0, 42.0, 229.0, 42.0, 240.0, 45.0)
            .C(230.0, 50.0, 225.0, 55.0, 223.0, 61.0)
            .C(263.0, 63.0, 283.0, 99.0, 278.0, 151.0)
            .C(271.0, 132.0, 257.0, 113.0, 238.0, 102.0)
            .C(214.0, 111.0, 192.0, 125.0, 177.0, 142.0)
            .C(177.0, 126.0, 174.0, 117.0, 174.0, 112.0)
            .C(137.0, 129.0, 97.0, 164.0, 85.0, 213.0)
            .Q(81.0, 232.0, 79.0, 249.0)
            .C(61.0, 231.0, 52.0, 203.0, 53.0, 180.0)
            .C(43.0, 186.0, 36.0, 192.0, 31.0, 203.0)
            .C(34.0, 177.0, 41.0, 151.0, 50.0, 137.0)
            .C(39.0, 143.0, 33.0, 146.0, 21.0, 150.0)
            .Q(22.0, 142.0, 28.0, 133.0)
            .Z();
        });
        return array;
      }
      case Hairstyle.TwinBuns:
        return PortraitArt.Parted(160.0, 72.0, 130.0, 132.0, 54.0);
      case Hairstyle.Waves:
      {
        HairPiece[] array2 = new HairPiece[1];
        array2[0] = PortraitArt.P(delegate(PPath p)
        {
          p.M(42.0, 153.0).C(31.0, 70.0, 95.0, 25.0, 163.0, 35.0).C(230.0, 30.0, 276.0, 67.0, 278.0, 147.0)
            .C(248.0, 144.0, 215.0, 112.0, 194.0, 77.0)
            .C(171.0, 122.0, 124.0, 110.0, 108.0, 142.0)
            .C(93.0, 173.0, 73.0, 171.0, 73.0, 210.0)
            .C(40.0, 190.0, 59.0, 173.0, 42.0, 153.0)
            .Z();
        });
        return array2;
      }
      case Hairstyle.Curls:
        return new HairPiece[]
        {
          new HairPiece(new PPath().M(39.0, 131.0).C(39.0, 68.0, 80.0, 23.0, 154.0, 24.0).C(222.0, 23.0, 278.0, 61.0, 280.0, 130.0)
            .L(244.0, 110.0)
            .Q(225.0, 132.0, 202.0, 118.0)
            .Q(182.0, 139.0, 163.0, 119.0)
            .Q(143.0, 136.0, 125.0, 120.0)
            .Q(104.0, 137.0, 87.0, 113.0)
            .Q(70.0, 129.0, 53.0, 111.0)
            .L(39.0, 131.0)
            .Z(), new PieceFill?(PieceFill.Hair), false),
          new HairPiece(PPath.Oval(39.0, 100.0, 33.0, 71.0), new PieceFill?(PieceFill.Hair), false),
          new HairPiece(PPath.Oval(249.0, 100.0, 32.0, 68.0), new PieceFill?(PieceFill.Hair), false),
          new HairPiece(new PPath().M(53.0, 111.0).Q(70.0, 129.0, 87.0, 113.0).Q(104.0, 137.0, 125.0, 120.0)
            .Q(143.0, 136.0, 163.0, 119.0)
            .Q(182.0, 139.0, 202.0, 118.0)
            .Q(225.0, 132.0, 244.0, 110.0), null, true)
        };
      case Hairstyle.Ponytail:
      {
        HairPiece[] array3 = new HairPiece[1];
        array3[0] = PortraitArt.P(delegate(PPath p)
        {
          p.M(46.0, 149.0).C(40.0, 88.0, 80.0, 48.0, 150.0, 51.0).C(224.0, 45.0, 274.0, 73.0, 279.0, 145.0)
            .C(252.0, 139.0, 226.0, 99.0, 220.0, 82.0)
            .C(197.0, 128.0, 153.0, 128.0, 125.0, 148.0)
            .L(138.0, 115.0)
            .C(113.0, 138.0, 86.0, 145.0, 46.0, 149.0)
            .Z();
        });
        return array3;
      }
      case Hairstyle.Undercut:
      {
        HairPiece[] array4 = new HairPiece[2];
        array4[0] = PortraitArt.P(delegate(PPath p)
        {
          p.M(53.0, 122.0).C(50.0, 70.0, 85.0, 45.0, 137.0, 47.0).L(211.0, 29.0)
            .L(201.0, 43.0)
            .L(244.0, 34.0)
            .L(233.0, 54.0)
            .C(276.0, 58.0, 286.0, 96.0, 274.0, 147.0)
            .L(251.0, 115.0)
            .L(257.0, 158.0)
            .C(214.0, 146.0, 174.0, 112.0, 157.0, 78.0)
            .C(132.0, 110.0, 92.0, 113.0, 53.0, 122.0)
            .Z();
        });
        array4[1] = new HairPiece(PPath.Oval(44.0, 128.0, 14.0, 40.0), new PieceFill?(PieceFill.Outfit), false);
        return array4;
      }
      case Hairstyle.Braids:
        return PortraitArt.Parted(162.0, 68.0, 125.0, 125.0, 54.0);
      case Hairstyle.Puff:
        return new HairPiece[]
        {
          new HairPiece(new PPath().M(54.0, 113.0).C(53.0, 55.0, 94.0, 35.0, 159.0, 35.0).C(221.0, 30.0, 267.0, 63.0, 267.0, 113.0)
            .Q(249.0, 125.0, 237.0, 105.0)
            .Q(214.0, 125.0, 198.0, 101.0)
            .Q(177.0, 120.0, 161.0, 100.0)
            .Q(140.0, 121.0, 123.0, 102.0)
            .Q(99.0, 126.0, 84.0, 108.0)
            .Q(68.0, 127.0, 54.0, 113.0)
            .Z(), new PieceFill?(PieceFill.Hair), false),
          new HairPiece(new PPath().M(267.0, 113.0).Q(249.0, 125.0, 237.0, 105.0).Q(214.0, 125.0, 198.0, 101.0)
            .Q(177.0, 120.0, 161.0, 100.0)
            .Q(140.0, 121.0, 123.0, 102.0)
            .Q(99.0, 126.0, 84.0, 108.0)
            .Q(68.0, 127.0, 54.0, 113.0), null, true)
        };
      default:
        return Array.Empty<HairPiece>();
      }
    }

    // Token: 0x06000D50 RID: 3408 RVA: 0x00566BB8 File Offset: 0x00566BB8
    private static IReadOnlyList<HairPiece> Parted(double x, double y, double left, double right, double top = 35.0)
    {
      return new HairPiece[]
      {
        PortraitArt.P(delegate(PPath p)
        {
          p.M(43.0, 144.0).C(39.0, 83.0, 82.0, top, x - 48.0, top).C(x - 27.0, top, x - 12.0, top + (y - top) * 0.3, x, y)
            .C(x - 20.0, y + 35.0, 94.0, left - 3.0, 53.0, left)
            .L(43.0, 144.0)
            .Z();
        }),
        PortraitArt.P(delegate(PPath p)
        {
          p.M(x, y).C(228.0, top + 12.0, 266.0, 82.0, 273.0, right).C(241.0, right - 10.0, x + 20.0, y + 34.0, x, y)
            .Z();
        })
      };
    }

    // Token: 0x06000D51 RID: 3409 RVA: 0x00566C20 File Offset: 0x00566C20
    private static PPath Scallop(double cx, double cy, double rx, double ry, int bumps, double depth)
    {
      int num = bumps * 8;
      Vector2[] array = new Vector2[num];
      for (int i = 0; i < num; i++)
      {
        double num2 = (double)i * 3.141592653589793 * 2.0 / (double)num;
        double num3 = depth * Math.Cos(num2 * (double)bumps);
        array[i] = new Vector2((float)(cx + (rx + num3) * Math.Cos(num2)), (float)(cy + (ry + num3) * Math.Sin(num2)));
      }
      PPath ppath = new PPath().M((double)array[0].X, (double)array[0].Y);
      for (int j = 0; j < num; j++)
      {
        Vector2 vector = array[(j + num - 1) % num];
        Vector2 vector2 = array[j];
        Vector2 vector3 = array[(j + 1) % num];
        Vector2 vector4 = array[(j + 2) % num];
        ppath.C((double)(vector2.X + (vector3.X - vector.X) / 6f), (double)(vector2.Y + (vector3.Y - vector.Y) / 6f), (double)(vector3.X - (vector4.X - vector2.X) / 6f), (double)(vector3.Y - (vector4.Y - vector2.Y) / 6f), (double)vector3.X, (double)vector3.Y);
      }
      return ppath.Z();
    }

    // Token: 0x06000D52 RID: 3410 RVA: 0x00566DA0 File Offset: 0x00566DA0
    private static IReadOnlyList<HairPiece> ArcadeBack(Hairstyle style)
    {
      HairPiece[] array2;
      switch (style)
      {
      case Hairstyle.HeroCap:
      {
        HairPiece[] array = new HairPiece[2];
        array[0] = PortraitArt.O(39.0, 72.0, 242.0, 166.0, PieceFill.Hair);
        array[1] = PortraitArt.P(delegate(PPath p)
        {
          p.M(35.0, 118.0).C(36.0, 6.0, 271.0, 4.0, 275.0, 116.0).Z();
        });
        array2 = array;
        break;
      }
      case Hairstyle.FoxHood:
        array2 = new HairPiece[]
        {
          PortraitArt.Poly(new double[]
          {
            43.0, 133.0, 24.0, 9.0, 111.0, 65.0, 217.0, 65.0, 294.0, 10.0,
            277.0, 148.0, 281.0, 220.0, 246.0, 252.0, 70.0, 252.0, 38.0, 222.0
          }),
          PortraitArt.PolyF(PieceFill.Accent, new double[] { 40.0, 35.0, 82.0, 77.0, 49.0, 97.0 }),
          PortraitArt.PolyF(PieceFill.Accent, new double[] { 280.0, 36.0, 270.0, 99.0, 238.0, 78.0 })
        };
        break;
      case Hairstyle.KnightHelm:
        array2 = new HairPiece[]
        {
          PortraitArt.O(37.0, 47.0, 248.0, 213.0, PieceFill.Hair),
          PortraitArt.PolyF(PieceFill.Accent, new double[] { 131.0, 55.0, 130.0, 24.0, 173.0, 8.0, 223.0, 24.0, 202.0, 52.0 }),
          PortraitArt.R(21.0, 121.0, 34.0, 80.0, 12.0, PieceFill.Hair),
          PortraitArt.R(269.0, 121.0, 34.0, 80.0, 12.0, PieceFill.Hair)
        };
        break;
      case Hairstyle.Crown:
      {
        HairPiece[] array3 = new HairPiece[1];
        array3[0] = PortraitArt.P(delegate(PPath p)
        {
          p.M(44.0, 143.0).C(21.0, 18.0, 292.0, 18.0, 279.0, 140.0).Q(265.0, 203.0, 277.0, 253.0)
            .L(243.0, 235.0)
            .L(72.0, 235.0)
            .Q(40.0, 271.0, 41.0, 255.0)
            .Z();
        });
        array2 = array3;
        break;
      }
      case Hairstyle.Robot:
        array2 = new HairPiece[]
        {
          PortraitArt.R(34.0, 45.0, 254.0, 205.0, 48.0, PieceFill.Hair),
          PortraitArt.R(13.0, 125.0, 31.0, 68.0, 10.0, PieceFill.Hair),
          PortraitArt.R(278.0, 125.0, 31.0, 68.0, 10.0, PieceFill.Hair),
          PortraitArt.R(155.0, 12.0, 14.0, 41.0, 6.0, PieceFill.Accent),
          PortraitArt.O(149.0, 5.0, 26.0, 22.0, PieceFill.Accent)
        };
        break;
      case Hairstyle.LeafCap:
      {
        HairPiece[] array4 = new HairPiece[2];
        array4[0] = PortraitArt.O(43.0, 73.0, 233.0, 169.0, PieceFill.Hair);
        array4[1] = PortraitArt.P(delegate(PPath p)
        {
          p.M(36.0, 113.0).C(51.0, 32.0, 132.0, 29.0, 231.0, 41.0).Q(272.0, 37.0, 301.0, 18.0)
            .Q(305.0, 74.0, 264.0, 113.0)
            .Z();
        });
        array2 = array4;
        break;
      }
      case Hairstyle.Pilot:
        array2 = new HairPiece[]
        {
          PortraitArt.O(34.0, 35.0, 254.0, 230.0, PieceFill.Hair),
          PortraitArt.O(12.0, 116.0, 54.0, 87.0, PieceFill.Hair),
          PortraitArt.O(259.0, 116.0, 49.0, 87.0, PieceFill.Hair),
          PortraitArt.R(154.0, 37.0, 18.0, 63.0, 8.0, PieceFill.Accent)
        };
        break;
      case Hairstyle.BearHood:
        array2 = new HairPiece[]
        {
          PortraitArt.O(12.0, 40.0, 89.0, 93.0, PieceFill.Hair),
          PortraitArt.O(220.0, 40.0, 89.0, 93.0, PieceFill.Hair),
          PortraitArt.O(34.0, 64.0, 251.0, 196.0, PieceFill.Hair),
          PortraitArt.O(32.0, 60.0, 49.0, 51.0, PieceFill.Accent),
          PortraitArt.O(240.0, 60.0, 49.0, 51.0, PieceFill.Accent)
        };
        break;
      case Hairstyle.Jester:
      {
        HairPiece[] array5 = new HairPiece[5];
        array5[0] = PortraitArt.O(41.0, 70.0, 237.0, 171.0, PieceFill.Hair);
        array5[1] = PortraitArt.P(delegate(PPath p)
        {
          p.M(36.0, 112.0).C(45.0, 61.0, 42.0, 28.0, 17.0, 46.0).C(71.0, 3.0, 124.0, 22.0, 133.0, 58.0)
            .Q(146.0, 5.0, 186.0, 14.0)
            .L(215.0, 58.0)
            .Q(284.0, 10.0, 303.0, 46.0)
            .Q(272.0, 44.0, 280.0, 117.0)
            .Z();
        });
        array5[2] = PortraitArt.O(5.0, 42.0, 25.0, 26.0, PieceFill.Accent);
        array5[3] = PortraitArt.O(175.0, 7.0, 24.0, 25.0, PieceFill.Accent);
        array5[4] = PortraitArt.O(289.0, 42.0, 25.0, 26.0, PieceFill.Accent);
        array2 = array5;
        break;
      }
      case Hairstyle.Pirate:
        array2 = new HairPiece[]
        {
          PortraitArt.O(40.0, 61.0, 240.0, 188.0, PieceFill.Hair),
          PortraitArt.Poly(new double[]
          {
            12.0, 88.0, 47.0, 23.0, 92.0, 44.0, 162.0, 13.0, 227.0, 44.0,
            274.0, 22.0, 308.0, 88.0, 274.0, 125.0, 43.0, 125.0
          })
        };
        break;
      default:
        array2 = Array.Empty<HairPiece>();
        break;
      }
      return array2;
    }

    // Token: 0x06000D53 RID: 3411 RVA: 0x005673F4 File Offset: 0x005673F4
    private static IReadOnlyList<HairPiece> ArcadeFront(Hairstyle style)
    {
      HairPiece[] array2;
      switch (style)
      {
      case Hairstyle.HeroCap:
      {
        HairPiece[] array = new HairPiece[3];
        array[0] = PortraitArt.P(delegate(PPath p)
        {
          p.M(34.0, 113.0).Q(151.0, 90.0, 276.0, 113.0).Q(321.0, 131.0, 292.0, 136.0)
            .Q(160.0, 116.0, 38.0, 132.0)
            .Z();
        });
        array[1] = PortraitArt.O(135.0, 55.0, 53.0, 43.0, PieceFill.Accent);
        array[2] = PortraitArt.PolyF(PieceFill.Outfit, new double[] { 153.0, 66.0, 168.0, 66.0, 175.0, 78.0, 162.0, 90.0, 149.0, 78.0 });
        array2 = array;
        break;
      }
      case Hairstyle.FoxHood:
      case Hairstyle.BearHood:
      {
        HairPiece[] array3 = new HairPiece[2];
        array3[0] = PortraitArt.P(delegate(PPath p)
        {
          p.M(37.0, 159.0).C(17.0, 40.0, 300.0, 39.0, 283.0, 159.0).L(255.0, 140.0)
            .Q(251.0, 96.0, 216.0, 117.0)
            .Q(187.0, 146.0, 162.0, 135.0)
            .Q(136.0, 147.0, 109.0, 117.0)
            .Q(74.0, 96.0, 65.0, 140.0)
            .Z();
        });
        array3[1] = PortraitArt.O(147.0, 100.0, 30.0, 20.0, PieceFill.Accent);
        array2 = array3;
        break;
      }
      case Hairstyle.KnightHelm:
      {
        HairPiece[] array4 = new HairPiece[4];
        array4[0] = PortraitArt.P(delegate(PPath p)
        {
          p.M(38.0, 142.0).Q(43.0, 40.0, 163.0, 51.0).Q(282.0, 42.0, 284.0, 142.0)
            .L(260.0, 156.0)
            .L(256.0, 117.0)
            .Q(162.0, 99.0, 67.0, 117.0)
            .L(62.0, 156.0)
            .Z();
        });
        array4[1] = PortraitArt.R(153.0, 52.0, 20.0, 51.0, 5.0, PieceFill.Accent);
        array4[2] = PortraitArt.Line(new double[] { 34.0, 148.0, 42.0, 178.0 });
        array4[3] = PortraitArt.Line(new double[] { 288.0, 148.0, 280.0, 178.0 });
        array2 = array4;
        break;
      }
      case Hairstyle.Crown:
        array2 = new HairPiece[]
        {
          PortraitArt.Sweep(177.0, 55.0, 151.0),
          PortraitArt.PolyF(PieceFill.Accent, new double[]
          {
            108.0, 76.0, 99.0, 32.0, 132.0, 51.0, 160.0, 17.0, 190.0, 51.0,
            222.0, 32.0, 214.0, 78.0
          }),
          PortraitArt.O(151.0, 47.0, 20.0, 22.0, PieceFill.Outfit)
        };
        break;
      case Hairstyle.Robot:
        array2 = new HairPiece[]
        {
          PortraitArt.R(47.0, 53.0, 228.0, 65.0, 25.0, PieceFill.Hair),
          PortraitArt.R(48.0, 220.0, 225.0, 29.0, 12.0, PieceFill.Hair),
          PortraitArt.R(73.0, 73.0, 34.0, 16.0, 6.0, PieceFill.Accent),
          PortraitArt.R(215.0, 73.0, 34.0, 16.0, 6.0, PieceFill.Accent),
          PortraitArt.Line(new double[] { 129.0, 234.0, 195.0, 234.0 }),
          PortraitArt.O(25.0, 142.0, 10.0, 12.0, PieceFill.Accent),
          PortraitArt.O(285.0, 142.0, 10.0, 12.0, PieceFill.Accent)
        };
        break;
      case Hairstyle.LeafCap:
      {
        HairPiece[] array5 = new HairPiece[3];
        array5[0] = PortraitArt.P(delegate(PPath p)
        {
          p.M(34.0, 119.0).Q(157.0, 74.0, 273.0, 118.0).L(266.0, 139.0)
            .Q(161.0, 111.0, 43.0, 141.0)
            .Z();
        });
        array5[1] = PortraitArt.PolyF(PieceFill.Accent, new double[] { 219.0, 87.0, 262.0, 52.0, 257.0, 94.0, 222.0, 111.0 });
        array5[2] = PortraitArt.Line(new double[] { 227.0, 102.0, 252.0, 69.0 });
        array2 = array5;
        break;
      }
      case Hairstyle.Pilot:
      {
        HairPiece[] array6 = new HairPiece[4];
        array6[0] = PortraitArt.P(delegate(PPath p)
        {
          p.M(37.0, 147.0).C(29.0, 20.0, 293.0, 21.0, 284.0, 147.0).L(261.0, 151.0)
            .Q(164.0, 62.0, 65.0, 151.0)
            .Z();
        });
        array6[1] = PortraitArt.R(154.0, 45.0, 18.0, 53.0, 8.0, PieceFill.Accent);
        array6[2] = PortraitArt.O(26.0, 137.0, 27.0, 44.0, PieceFill.Accent);
        array6[3] = PortraitArt.O(273.0, 137.0, 23.0, 44.0, PieceFill.Accent);
        array2 = array6;
        break;
      }
      case Hairstyle.Jester:
        array2 = new HairPiece[] { PortraitArt.PolyF(PieceFill.Accent, new double[]
        {
          37.0, 118.0, 72.0, 88.0, 118.0, 81.0, 165.0, 102.0, 210.0, 79.0,
          256.0, 91.0, 281.0, 118.0, 257.0, 140.0, 236.0, 122.0, 210.0, 145.0,
          185.0, 123.0, 159.0, 145.0, 132.0, 123.0, 103.0, 144.0, 79.0, 121.0,
          58.0, 143.0
        }) };
        break;
      case Hairstyle.Pirate:
      {
        HairPiece[] array7 = new HairPiece[3];
        array7[0] = PortraitArt.P(delegate(PPath p)
        {
          p.M(19.0, 96.0).Q(158.0, 68.0, 301.0, 96.0).L(278.0, 130.0)
            .Q(159.0, 106.0, 43.0, 129.0)
            .Z();
        });
        array7[1] = PortraitArt.O(139.0, 49.0, 43.0, 40.0, PieceFill.Accent);
        array7[2] = PortraitArt.PolyF(PieceFill.Outfit, new double[] { 157.0, 56.0, 166.0, 56.0, 174.0, 69.0, 161.0, 82.0, 148.0, 69.0 });
        array2 = array7;
        break;
      }
      default:
        array2 = Array.Empty<HairPiece>();
        break;
      }
      return array2;
    }

    // Token: 0x06000D54 RID: 3412 RVA: 0x00567948 File Offset: 0x00567948
    private static HairPiece Sweep(double part, double top, double tip)
    {
      return PortraitArt.P(delegate(PPath p)
      {
        p.M(41.0, 147.0).C(29.0, 59.0, 89.0, top - 7.0, part, top).C(261.0, top - 9.0, 282.0, 94.0, 280.0, 145.0)
          .Q(235.0, 137.0, part, top + 41.0)
          .Q(108.0, 156.0, 74.0, tip)
          .L(60.0, tip - 35.0)
          .Z();
      });
    }

    // Token: 0x06000D55 RID: 3413 RVA: 0x00567974 File Offset: 0x00567974
    public static PPath StorybookEyePath()
    {
      return new PPath().M(19.0, 0.0).C(36.0, -1.0, 44.0, 11.0, 44.0, 27.0).C(44.0, 43.0, 37.0, 54.0, 23.0, 54.0)
        .C(8.0, 54.0, 0.0, 45.0, 0.0, 31.0)
        .C(0.0, 13.0, 6.0, 1.0, 19.0, 0.0)
        .Z();
    }

    // Token: 0x06000D56 RID: 3414 RVA: 0x00567A90 File Offset: 0x00567A90
    private static IReadOnlyList<HairPiece> StorybookBack(Hairstyle style, bool faceOnly)
    {
      double hem = (double)(faceOnly ? 242 : 274);
      HairPiece[] array2;
      switch (style)
      {
      case Hairstyle.SeedHood:
      {
        HairPiece[] array = new HairPiece[2];
        array[0] = PortraitArt.O(32.0, 54.0, 256.0, 206.0, PieceFill.Hair);
        array[1] = PortraitArt.P(delegate(PPath p)
        {
          p.M(148.0, 65.0).C(99.0, 67.0, 92.0, 36.0, 111.0, 16.0).Q(154.0, 11.0, 162.0, 51.0)
            .Q(183.0, 8.0, 221.0, 18.0)
            .Q(233.0, 63.0, 166.0, 69.0)
            .Z();
        });
        array2 = array;
        break;
      }
      case Hairstyle.RibbonBob:
        array2 = new HairPiece[]
        {
          PortraitArt.O(35.0, 47.0, 250.0, 207.0, PieceFill.Hair),
          PortraitArt.O(43.0, 159.0, 49.0, 91.0, PieceFill.Hair),
          PortraitArt.O(232.0, 157.0, 47.0, 93.0, PieceFill.Hair)
        };
        break;
      case Hairstyle.SleepyEars:
        array2 = new HairPiece[]
        {
          PortraitArt.O(43.0, 16.0, 65.0, 130.0, PieceFill.Hair),
          PortraitArt.O(225.0, 36.0, 66.0, 109.0, PieceFill.Hair),
          PortraitArt.O(31.0, 72.0, 258.0, 188.0, PieceFill.Hair),
          PortraitArt.O(61.0, 40.0, 29.0, 62.0, PieceFill.Accent),
          PortraitArt.O(241.0, 55.0, 29.0, 52.0, PieceFill.Accent)
        };
        break;
      case Hairstyle.MeadowHat:
        array2 = new HairPiece[]
        {
          PortraitArt.O(42.0, 67.0, 237.0, 187.0, PieceFill.Hair),
          PortraitArt.O(82.0, 23.0, 157.0, 99.0, PieceFill.Hair),
          PortraitArt.O(14.0, 79.0, 291.0, 51.0, PieceFill.Hair)
        };
        break;
      case Hairstyle.CatKerchief:
      {
        HairPiece[] array3 = new HairPiece[3];
        array3[0] = PortraitArt.P(delegate(PPath p)
        {
          p.M(34.0, 153.0).Q(27.0, 58.0, 39.0, 24.0).Q(81.0, 32.0, 107.0, 67.0)
            .Q(165.0, 45.0, 222.0, 68.0)
            .Q(250.0, 32.0, 280.0, 29.0)
            .Q(295.0, 96.0, 284.0, 177.0)
            .Q(163.0, 307.0, 35.0, 191.0)
            .Z();
        });
        array3[1] = PortraitArt.O(51.0, 47.0, 27.0, 47.0, PieceFill.HairHighlight);
        array3[2] = PortraitArt.O(250.0, 49.0, 23.0, 45.0, PieceFill.HairHighlight);
        array2 = array3;
        break;
      }
      case Hairstyle.WindPart:
      {
        HairPiece[] array4 = new HairPiece[2];
        array4[0] = PortraitArt.O(35.0, 44.0, 247.0, 209.0, PieceFill.Hair);
        array4[1] = PortraitArt.P(delegate(PPath p)
        {
          p.M(248.0, 142.0).Q(269.0, 207.0, 294.0, 215.0).Q(265.0, 250.0, 231.0, 240.0)
            .Z();
        });
        array2 = array4;
        break;
      }
      case Hairstyle.CandleTuft:
      {
        HairPiece[] array5 = new HairPiece[1];
        array5[0] = PortraitArt.P(delegate(PPath p)
        {
          p.M(32.0, 177.0).C(23.0, 105.0, 97.0, 113.0, 108.0, 58.0).Q(121.0, 62.0, 117.0, 90.0)
            .C(161.0, 68.0, 142.0, 32.0, 183.0, 12.0)
            .C(178.0, 50.0, 229.0, 49.0, 234.0, 89.0)
            .Q(247.0, 81.0, 261.0, 64.0)
            .C(255.0, 116.0, 310.0, 141.0, 282.0, 194.0)
            .Q(153.0, 321.0, 32.0, 177.0)
            .Z();
        });
        array2 = array5;
        break;
      }
      case Hairstyle.BerryBraids:
        array2 = new HairPiece[]
        {
          PortraitArt.O(39.0, 50.0, 241.0, 192.0, PieceFill.Hair),
          PortraitArt.O(42.0, 164.0, 52.0, hem - 164.0, PieceFill.Hair),
          PortraitArt.O(232.0, 164.0, 48.0, hem - 164.0, PieceFill.Hair),
          PortraitArt.O(48.0, hem - 24.0, 36.0, 17.0, PieceFill.Accent),
          PortraitArt.O(239.0, hem - 24.0, 33.0, 17.0, PieceFill.Accent)
        };
        break;
      case Hairstyle.AcornCap:
        array2 = new HairPiece[]
        {
          PortraitArt.O(38.0, 64.0, 246.0, 189.0, PieceFill.Hair),
          PortraitArt.R(151.0, 10.0, 20.0, 42.0, 9.0, PieceFill.Hair),
          PortraitArt.O(32.0, 36.0, 255.0, 101.0, PieceFill.Hair)
        };
        break;
      case Hairstyle.MoonMantle:
        array2 = new HairPiece[] { PortraitArt.P(delegate(PPath p)
        {
          p.M(25.0, hem).C(40.0, 85.0, 86.0, 17.0, 170.0, 19.0).C(250.0, 39.0, 275.0, 152.0, 297.0, hem)
            .Q(156.0, hem - 22.0, 25.0, hem)
            .Z();
        }) };
        break;
      default:
        array2 = Array.Empty<HairPiece>();
        break;
      }
      return array2;
    }

    // Token: 0x06000D57 RID: 3415 RVA: 0x00568048 File Offset: 0x00568048
    private static IReadOnlyList<HairPiece> StorybookFront(Hairstyle style)
    {
      HairPiece[] array;
      switch (style)
      {
      case Hairstyle.SeedHood:
        array = new HairPiece[]
        {
          PortraitArt.Fringe(),
          PortraitArt.Leaf(137.0, 63.0, PieceFill.Accent)
        };
        break;
      case Hairstyle.RibbonBob:
        array = new HairPiece[]
        {
          PortraitArt.Fringe(),
          new HairPiece(new PPath().M(215.0, 65.0).C(175.0, 83.0, 164.0, 51.0, 177.0, 38.0).Q(209.0, 29.0, 219.0, 56.0)
            .C(243.0, 25.0, 280.0, 22.0, 268.0, 48.0)
            .Q(270.0, 85.0, 222.0, 72.0)
            .Z(), new PieceFill?(PieceFill.Accent), true),
          PortraitArt.O(211.0, 53.0, 21.0, 23.0, PieceFill.Outfit)
        };
        break;
      case Hairstyle.SleepyEars:
        array = new HairPiece[]
        {
          PortraitArt.Fringe(),
          PortraitArt.O(124.0, 78.0, 20.0, 13.0, PieceFill.HairHighlight),
          PortraitArt.O(158.0, 71.0, 22.0, 14.0, PieceFill.HairHighlight),
          PortraitArt.O(194.0, 80.0, 20.0, 13.0, PieceFill.HairHighlight)
        };
        break;
      case Hairstyle.MeadowHat:
        array = new HairPiece[]
        {
          PortraitArt.Fringe(),
          PortraitArt.R(70.0, 67.0, 181.0, 22.0, 10.0, PieceFill.Accent),
          PortraitArt.O(14.0, 84.0, 291.0, 36.0, PieceFill.Hair),
          PortraitArt.Leaf(225.0, 53.0, PieceFill.Outfit)
        };
        break;
      case Hairstyle.CatKerchief:
        array = new HairPiece[]
        {
          PortraitArt.Fringe(),
          new HairPiece(new PPath().M(34.0, 99.0).Q(152.0, 28.0, 282.0, 99.0).L(277.0, 119.0)
            .Q(158.0, 67.0, 40.0, 119.0)
            .Z(), new PieceFill?(PieceFill.Accent), true),
          PortraitArt.O(254.0, 90.0, 24.0, 23.0, PieceFill.Outfit)
        };
        break;
      case Hairstyle.WindPart:
        array = new HairPiece[]
        {
          PortraitArt.Fringe(),
          new HairPiece(new PPath().M(63.0, 104.0).Q(105.0, 40.0, 184.0, 62.0).Q(122.0, 67.0, 63.0, 104.0)
            .Z(), new PieceFill?(PieceFill.HairHighlight), false)
        };
        break;
      case Hairstyle.CandleTuft:
        array = new HairPiece[]
        {
          PortraitArt.Fringe(),
          PortraitArt.Leaf(153.0, 48.0, PieceFill.Accent)
        };
        break;
      case Hairstyle.BerryBraids:
        array = new HairPiece[]
        {
          PortraitArt.Fringe(),
          PortraitArt.O(48.0, 119.0, 17.0, 18.0, PieceFill.Accent),
          PortraitArt.O(61.0, 108.0, 17.0, 18.0, PieceFill.Accent),
          PortraitArt.Leaf(57.0, 81.0, PieceFill.Outfit)
        };
        break;
      case Hairstyle.AcornCap:
        array = new HairPiece[]
        {
          PortraitArt.Fringe(),
          PortraitArt.R(29.0, 87.0, 260.0, 34.0, 16.0, PieceFill.HairShadow),
          PortraitArt.Line(new double[] { 65.0, 57.0, 83.0, 80.0 }),
          PortraitArt.Line(new double[] { 102.0, 43.0, 122.0, 77.0 }),
          PortraitArt.Line(new double[] { 145.0, 39.0, 163.0, 76.0 }),
          PortraitArt.Line(new double[] { 187.0, 43.0, 205.0, 78.0 }),
          PortraitArt.Line(new double[] { 226.0, 57.0, 242.0, 81.0 })
        };
        break;
      case Hairstyle.MoonMantle:
        array = new HairPiece[]
        {
          PortraitArt.Fringe(),
          PortraitArt.O(153.0, 51.0, 30.0, 31.0, PieceFill.Accent),
          PortraitArt.O(166.0, 46.0, 25.0, 27.0, PieceFill.Hair)
        };
        break;
      default:
        array = Array.Empty<HairPiece>();
        break;
      }
      return array;
    }

    // Token: 0x06000D58 RID: 3416 RVA: 0x00568680 File Offset: 0x00568680
    private static HairPiece Fringe()
    {
      return PortraitArt.P(delegate(PPath p)
      {
        p.M(38.0, 145.0).C(15.0, 31.0, 289.0, 32.0, 280.0, 140.0).L(257.0, 143.0)
          .Q(245.0, 112.0, 204.0, 104.0)
          .Q(192.0, 126.0, 172.0, 128.0)
          .L(174.0, 105.0)
          .Q(153.0, 127.0, 121.0, 132.0)
          .L(125.0, 109.0)
          .Q(81.0, 122.0, 64.0, 147.0)
          .Z();
      });
    }

    // Token: 0x06000D59 RID: 3417 RVA: 0x005686A8 File Offset: 0x005686A8
    private static HairPiece Leaf(double x, double y, PieceFill fill)
    {
      return new HairPiece(new PPath().M(x, y + 26.0).Q(x - 8.0, y, x + 30.0, y).Q(x + 37.0, y + 31.0, x, y + 26.0)
        .Z(), new PieceFill?(fill), true);
    }

    // Token: 0x06000D5A RID: 3418 RVA: 0x00568720 File Offset: 0x00568720
    public static PPath CloudClubSilhouette(Hairstyle style)
    {
      PPath ppath = new PPath();
      switch (style)
      {
      case Hairstyle.CloudCumulus:
        ppath.M(54.0, 119.0).C(44.0, 80.0, 82.0, 59.0, 116.0, 85.0).C(124.0, 36.0, 192.0, 41.0, 203.0, 88.0)
          .C(236.0, 60.0, 279.0, 88.0, 267.0, 123.0)
          .C(310.0, 128.0, 310.0, 186.0, 276.0, 200.0)
          .C(278.0, 236.0, 246.0, 253.0, 217.0, 239.0)
          .C(194.0, 265.0, 151.0, 266.0, 132.0, 244.0)
          .C(98.0, 263.0, 63.0, 246.0, 63.0, 222.0)
          .C(22.0, 226.0, 13.0, 186.0, 34.0, 163.0)
          .C(17.0, 145.0, 28.0, 122.0, 54.0, 119.0);
        break;
      case Hairstyle.CloudCotton:
        ppath.M(58.0, 113.0).C(40.0, 79.0, 83.0, 56.0, 108.0, 77.0).C(111.0, 42.0, 157.0, 36.0, 178.0, 65.0)
          .C(211.0, 43.0, 252.0, 63.0, 250.0, 98.0)
          .C(285.0, 90.0, 301.0, 123.0, 282.0, 149.0)
          .C(310.0, 179.0, 295.0, 218.0, 264.0, 218.0)
          .C(262.0, 252.0, 223.0, 268.0, 198.0, 249.0)
          .C(169.0, 278.0, 122.0, 265.0, 112.0, 244.0)
          .C(76.0, 259.0, 49.0, 234.0, 55.0, 212.0)
          .C(19.0, 212.0, 17.0, 176.0, 37.0, 159.0)
          .C(19.0, 140.0, 33.0, 113.0, 58.0, 113.0);
        break;
      case Hairstyle.CloudRipple:
        ppath.M(45.0, 130.0).C(31.0, 99.0, 63.0, 77.0, 90.0, 97.0).C(92.0, 64.0, 126.0, 57.0, 147.0, 83.0)
          .C(168.0, 46.0, 218.0, 57.0, 223.0, 94.0)
          .C(253.0, 75.0, 284.0, 95.0, 277.0, 124.0)
          .C(309.0, 130.0, 310.0, 166.0, 287.0, 183.0)
          .C(305.0, 218.0, 268.0, 245.0, 242.0, 229.0)
          .C(229.0, 261.0, 187.0, 259.0, 172.0, 239.0)
          .C(150.0, 266.0, 111.0, 260.0, 99.0, 237.0)
          .C(67.0, 255.0, 40.0, 232.0, 45.0, 208.0)
          .C(9.0, 197.0, 12.0, 148.0, 45.0, 130.0);
        break;
      case Hairstyle.CloudBillow:
        ppath.M(47.0, 135.0).C(29.0, 96.0, 73.0, 68.0, 103.0, 89.0).C(92.0, 49.0, 146.0, 26.0, 174.0, 61.0)
          .C(205.0, 38.0, 243.0, 63.0, 242.0, 103.0)
          .C(285.0, 94.0, 307.0, 134.0, 281.0, 164.0)
          .C(311.0, 193.0, 287.0, 239.0, 251.0, 232.0)
          .C(233.0, 263.0, 196.0, 266.0, 171.0, 248.0)
          .C(141.0, 273.0, 96.0, 255.0, 91.0, 234.0)
          .C(55.0, 246.0, 27.0, 218.0, 40.0, 188.0)
          .C(12.0, 180.0, 17.0, 140.0, 47.0, 135.0);
        break;
      case Hairstyle.CloudBloom:
        ppath.M(50.0, 120.0).C(35.0, 92.0, 60.0, 65.0, 91.0, 78.0).C(95.0, 43.0, 137.0, 32.0, 161.0, 62.0)
          .C(184.0, 31.0, 226.0, 43.0, 232.0, 78.0)
          .C(261.0, 65.0, 288.0, 91.0, 274.0, 120.0)
          .C(310.0, 129.0, 306.0, 168.0, 284.0, 183.0)
          .C(300.0, 214.0, 271.0, 243.0, 243.0, 229.0)
          .C(235.0, 263.0, 189.0, 268.0, 163.0, 242.0)
          .C(137.0, 268.0, 94.0, 263.0, 85.0, 232.0)
          .C(53.0, 246.0, 23.0, 220.0, 37.0, 188.0)
          .C(10.0, 166.0, 18.0, 126.0, 50.0, 120.0);
        break;
      case Hairstyle.CloudTuft:
        ppath.M(53.0, 128.0).C(35.0, 98.0, 65.0, 70.0, 94.0, 88.0).C(95.0, 51.0, 128.0, 34.0, 161.0, 55.0)
          .C(177.0, 66.0, 194.0, 60.0, 203.0, 50.0)
          .C(213.0, 38.0, 225.0, 47.0, 222.0, 62.0)
          .C(219.0, 81.0, 205.0, 89.0, 211.0, 99.0)
          .C(245.0, 76.0, 280.0, 96.0, 270.0, 128.0)
          .C(308.0, 139.0, 303.0, 184.0, 276.0, 196.0)
          .C(287.0, 231.0, 249.0, 254.0, 223.0, 239.0)
          .C(197.0, 265.0, 151.0, 260.0, 134.0, 239.0)
          .C(104.0, 257.0, 68.0, 248.0, 60.0, 220.0)
          .C(22.0, 224.0, 12.0, 184.0, 35.0, 163.0)
          .C(23.0, 144.0, 33.0, 128.0, 53.0, 128.0);
        break;
      case Hairstyle.CloudDrift:
        ppath.M(43.0, 138.0).C(25.0, 108.0, 60.0, 83.0, 87.0, 103.0).C(87.0, 69.0, 133.0, 53.0, 157.0, 83.0)
          .C(180.0, 62.0, 214.0, 76.0, 222.0, 102.0)
          .C(244.0, 85.0, 272.0, 95.0, 278.0, 123.0)
          .C(309.0, 125.0, 310.0, 164.0, 284.0, 183.0)
          .C(304.0, 211.0, 277.0, 240.0, 246.0, 227.0)
          .C(221.0, 253.0, 191.0, 247.0, 174.0, 236.0)
          .C(138.0, 260.0, 104.0, 246.0, 91.0, 231.0)
          .C(59.0, 248.0, 27.0, 225.0, 39.0, 200.0)
          .C(8.0, 186.0, 15.0, 149.0, 43.0, 138.0);
        break;
      case Hairstyle.CloudPillow:
        ppath.M(60.0, 111.0).C(45.0, 65.0, 97.0, 46.0, 126.0, 75.0).C(149.0, 46.0, 194.0, 51.0, 211.0, 80.0)
          .C(248.0, 50.0, 288.0, 82.0, 271.0, 118.0)
          .C(309.0, 142.0, 301.0, 183.0, 278.0, 198.0)
          .C(290.0, 235.0, 250.0, 260.0, 218.0, 238.0)
          .C(189.0, 270.0, 148.0, 263.0, 130.0, 241.0)
          .C(93.0, 268.0, 51.0, 244.0, 57.0, 216.0)
          .C(18.0, 212.0, 16.0, 164.0, 41.0, 149.0)
          .C(30.0, 132.0, 39.0, 113.0, 60.0, 111.0);
        break;
      case Hairstyle.CloudNimbus:
        ppath.M(50.0, 124.0).C(35.0, 101.0, 56.0, 77.0, 82.0, 86.0).C(77.0, 53.0, 115.0, 40.0, 140.0, 65.0)
          .C(161.0, 32.0, 206.0, 43.0, 212.0, 77.0)
          .C(242.0, 57.0, 279.0, 84.0, 266.0, 117.0)
          .C(300.0, 119.0, 310.0, 154.0, 284.0, 180.0)
          .C(307.0, 209.0, 275.0, 243.0, 247.0, 230.0)
          .C(237.0, 261.0, 197.0, 265.0, 177.0, 241.0)
          .C(158.0, 270.0, 113.0, 260.0, 104.0, 240.0)
          .C(72.0, 257.0, 45.0, 236.0, 49.0, 211.0)
          .C(17.0, 216.0, 6.0, 181.0, 31.0, 161.0)
          .C(16.0, 142.0, 31.0, 124.0, 50.0, 124.0);
        break;
      case Hairstyle.CloudWisp:
        ppath.M(50.0, 126.0).C(34.0, 98.0, 68.0, 76.0, 94.0, 96.0).C(96.0, 60.0, 139.0, 49.0, 162.0, 83.0)
          .C(184.0, 59.0, 214.0, 77.0, 221.0, 99.0)
          .C(246.0, 101.0, 266.0, 90.0, 277.0, 77.0)
          .C(287.0, 65.0, 300.0, 76.0, 294.0, 90.0)
          .C(288.0, 106.0, 280.0, 114.0, 280.0, 126.0)
          .C(310.0, 143.0, 303.0, 180.0, 278.0, 195.0)
          .C(289.0, 228.0, 251.0, 246.0, 226.0, 233.0)
          .C(201.0, 259.0, 157.0, 255.0, 139.0, 237.0)
          .C(106.0, 258.0, 65.0, 240.0, 60.0, 219.0)
          .C(21.0, 217.0, 13.0, 183.0, 35.0, 160.0)
          .C(22.0, 142.0, 32.0, 128.0, 50.0, 126.0);
        break;
      default:
        return PPath.Oval(24.0, 60.0, 272.0, 204.0);
      }
      return ppath.Z();
    }

    // Token: 0x06000D5B RID: 3419 RVA: 0x0056A044 File Offset: 0x0056A044
    public static PPath CaretEyePath(PortraitExpression e, int index)
    {
      double num;
      switch (e)
      {
      case PortraitExpression.Listening:
        num = 17.0;
        goto IL_00B2;
      case PortraitExpression.Thinking:
        num = 9.0;
        goto IL_00B2;
      case PortraitExpression.Surprised:
        num = 20.0;
        goto IL_00B2;
      case PortraitExpression.Skeptical:
        num = ((index == 0) ? 4.0 : 16.0);
        goto IL_00B2;
      case PortraitExpression.Shy:
        num = 12.0;
        goto IL_00B2;
      case PortraitExpression.Excited:
      case PortraitExpression.Loving:
        num = 18.0;
        goto IL_00B2;
      case PortraitExpression.Determined:
        num = 8.0;
        goto IL_00B2;
      case PortraitExpression.Sad:
        num = -14.0;
        goto IL_00B2;
      }
      num = 16.0;
      IL_00B2:
      double num2 = num;
      return PortraitArt.Caret(36.0, 43.0, num2);
    }

    // Token: 0x06000D5C RID: 3420 RVA: 0x0056A120 File Offset: 0x0056A120
    public static PPath CloudClosedEyePath(PortraitExpression e, double centerX)
    {
      return PortraitArt.Caret(centerX, 164.0, (e == PortraitExpression.Laughing) ? 16.0 : ((e == PortraitExpression.Sleepy) ? (-16.0) : 0.0));
    }

    // Token: 0x06000D5D RID: 3421 RVA: 0x0056A15C File Offset: 0x0056A15C
    private static PPath Caret(double cx, double cy, double rise)
    {
      return new PPath().M(cx - 24.0, cy + rise / 2.0).L(cx, cy - rise / 2.0).L(cx + 24.0, cy + rise / 2.0);
    }

    // Token: 0x06000D5E RID: 3422 RVA: 0x0056A1BC File Offset: 0x0056A1BC
    [NullableContext(0)]
    [return: Nullable(1)]
    public static PPath CloudShape(bool wide, bool mirrored, [TupleElementNames(new string[] { "X", "Y", "W", "H" })] ValueTuple<double, double, double, double> rect)
    {
      PPath ppath = (wide ? PortraitArt.WideTraced : PortraitArt.CompactTraced);
      ValueTuple<double, double, double, double> valueTuple = ppath.Bounds();
      if (valueTuple.Item3 <= 0.0 || valueTuple.Item4 <= 0.0)
      {
        return new PPath();
      }
      double num = Math.Min(rect.Item3 / valueTuple.Item3, rect.Item4 / valueTuple.Item4);
      Matrix3x2 matrix3x = Matrix3x2.CreateTranslation((float)(-(float)(valueTuple.Item1 + valueTuple.Item3 / 2.0)), (float)(-(float)(valueTuple.Item2 + valueTuple.Item4 / 2.0))) * Matrix3x2.CreateScale((float)(mirrored ? (-(float)num) : num), (float)num) * Matrix3x2.CreateTranslation((float)(rect.Item1 + rect.Item3 / 2.0), (float)(rect.Item2 + rect.Item4 / 2.0));
      return ppath.Transformed(matrix3x);
    }

    // Token: 0x06000D5F RID: 3423 RVA: 0x0056A2B8 File Offset: 0x0056A2B8
    public static CloudFaceTraits ClassicTraits(PortraitAppearance a)
    {
      return new CloudFaceTraits(a.Hairstyle.RawValue(), new double?(a.HairColor.ClassicCloudHue()));
    }

    // Token: 0x06000D60 RID: 3424 RVA: 0x0056A2DC File Offset: 0x0056A2DC
    public static PPath ClassicSilhouette(PortraitAppearance a)
    {
      CloudFaceTraits cloudFaceTraits = PortraitArt.ClassicTraits(a);
      return PortraitArt.CloudShape(cloudFaceTraits.IsWide, cloudFaceTraits.IsMirrored, PortraitArt.ClassicBodyRect);
    }

    // Token: 0x06000D61 RID: 3425 RVA: 0x0056A308 File Offset: 0x0056A308
    [NullableContext(0)]
    [return: TupleElementNames(new string[] { "X", "Y" })]
    public static ValueTuple<double, double> ClassicEyeCenter(int index, [Nullable(1)] PortraitAppearance a)
    {
      double num = 16.0 * PortraitArt.ClassicTraits(a).RestingGazeDirection * 1.6;
      return new ValueTuple<double, double>(160.0 + ((index == 0) ? (-45.6) : 45.6) + num, PortraitArt.ClassicBodyRect.Item2 + PortraitArt.ClassicBodyRect.Item4 / 2.0 + 6.4);
    }

    // Token: 0x06000D62 RID: 3426 RVA: 0x0056A388 File Offset: 0x0056A388
    [NullableContext(0)]
    [return: Nullable(1)]
    public static PPath ClassicClosedEyePath(PortraitExpression e, [TupleElementNames(new string[] { "X", "Y" })] ValueTuple<double, double> c)
    {
      int num;
      if (e == PortraitExpression.Laughing)
      {
        num = 16;
      }
      else
      {
        bool flag = e == PortraitExpression.Sleepy || e == PortraitExpression.Sad;
        num = (flag ? (-16) : 0);
      }
      double num2 = (double)num;
      return new PPath().M(c.Item1 - 24.0, c.Item2 + num2 / 2.0).L(c.Item1, c.Item2 - num2 / 2.0).L(c.Item1 + 24.0, c.Item2 + num2 / 2.0);
    }

    // Token: 0x06000D63 RID: 3427 RVA: 0x0056A42C File Offset: 0x0056A42C
    public static PPath MouthPath(PortraitExpression e)
    {
      ValueTuple<double, double, double> valueTuple;
      switch (e)
      {
      case PortraitExpression.Thinking:
      case PortraitExpression.Skeptical:
        valueTuple = new ValueTuple<double, double, double>(18.0, 0.0, -1.0);
        goto IL_0163;
      case PortraitExpression.Happy:
      case PortraitExpression.Loving:
        valueTuple = new ValueTuple<double, double, double>(22.0, 5.0, 2.0);
        goto IL_0163;
      case PortraitExpression.Sleepy:
        valueTuple = new ValueTuple<double, double, double>(13.0, 1.0, 0.0);
        goto IL_0163;
      case PortraitExpression.Surprised:
        valueTuple = new ValueTuple<double, double, double>(17.0, 13.0, -13.0);
        goto IL_0163;
      case PortraitExpression.Shy:
        valueTuple = new ValueTuple<double, double, double>(14.0, 5.0, 3.0);
        goto IL_0163;
      case PortraitExpression.Excited:
      case PortraitExpression.Laughing:
        valueTuple = new ValueTuple<double, double, double>(32.0, 19.0, -3.0);
        goto IL_0163;
      case PortraitExpression.Sad:
        valueTuple = new ValueTuple<double, double, double>(18.0, -5.0, -3.0);
        goto IL_0163;
      }
      valueTuple = new ValueTuple<double, double, double>(18.0, 4.0, 2.0);
      IL_0163:
      ValueTuple<double, double, double> valueTuple2 = valueTuple;
      double item = valueTuple2.Item1;
      double item2 = valueTuple2.Item2;
      double item3 = valueTuple2.Item3;
      double num = 162.0 - item / 2.0;
      double num2 = 162.0 + item / 2.0;
      return new PPath().M(num, 217.0).C(num + 2.0, 217.0 + item2, num2 - 2.0, 217.0 + item2, num2, 217.0).C(num2 - 2.0, 217.0 + item3, num + 2.0, 217.0 + item3, num, 217.0)
        .Z();
    }

    // Token: 0x06000D64 RID: 3428 RVA: 0x0056A674 File Offset: 0x0056A674
    public static PPath FacePath(double foreheadTop)
    {
      return new PPath().M(53.0, 145.0).C(53.0, 110.0, 89.0, foreheadTop, 162.0, foreheadTop).C(235.0, foreheadTop, 273.0, 110.0, 273.0, 145.0)
        .C(273.0, 216.0, 222.0, 249.0, 164.0, 249.0)
        .C(99.0, 249.0, 53.0, 216.0, 53.0, 145.0)
        .Z();
    }

    // Token: 0x06000D65 RID: 3429 RVA: 0x0056A778 File Offset: 0x0056A778
    public static PPath JawPath()
    {
      return new PPath().M(53.0, 145.0).C(53.0, 216.0, 99.0, 249.0, 164.0, 249.0).C(222.0, 249.0, 273.0, 216.0, 273.0, 145.0);
    }

    // Token: 0x06000D66 RID: 3430 RVA: 0x0056A818 File Offset: 0x0056A818
    public static PPath HeartPath(double sx, double sy, double tx = 0.0, double ty = 0.0)
    {
      return new PPath().M(39.0, 73.0).C(25.0, 62.0, 11.0, 52.0, 11.0, 38.0).C(11.0, 20.0, 31.0, 20.0, 39.0, 32.0)
        .C(49.0, 20.0, 67.0, 20.0, 67.0, 38.0)
        .C(67.0, 52.0, 52.0, 62.0, 39.0, 73.0)
        .Z()
        .Transformed(Matrix3x2.CreateScale((float)sx, (float)sy) * Matrix3x2.CreateTranslation((float)tx, (float)ty));
    }

    // Token: 0x06000D67 RID: 3431 RVA: 0x0056A950 File Offset: 0x0056A950
    public static PPath LedgeHand(double w, double h)
    {
      PortraitArt.<>c__DisplayClass33_0 CS$<>8__locals1;
      CS$<>8__locals1.w = w;
      CS$<>8__locals1.h = h;
      return new PPath().M(PortraitArt.<LedgeHand>g__X|33_0(2.0, ref CS$<>8__locals1), PortraitArt.<LedgeHand>g__Y|33_1(13.0, ref CS$<>8__locals1)).C(PortraitArt.<LedgeHand>g__X|33_0(2.0, ref CS$<>8__locals1), PortraitArt.<LedgeHand>g__Y|33_1(7.0, ref CS$<>8__locals1), PortraitArt.<LedgeHand>g__X|33_0(6.0, ref CS$<>8__locals1), PortraitArt.<LedgeHand>g__Y|33_1(4.0, ref CS$<>8__locals1), PortraitArt.<LedgeHand>g__X|33_0(12.0, ref CS$<>8__locals1), PortraitArt.<LedgeHand>g__Y|33_1(4.0, ref CS$<>8__locals1)).C(PortraitArt.<LedgeHand>g__X|33_0(14.0, ref CS$<>8__locals1), PortraitArt.<LedgeHand>g__Y|33_1(4.0, ref CS$<>8__locals1), PortraitArt.<LedgeHand>g__X|33_0(15.0, ref CS$<>8__locals1), PortraitArt.<LedgeHand>g__Y|33_1(5.5, ref CS$<>8__locals1), PortraitArt.<LedgeHand>g__X|33_0(17.0, ref CS$<>8__locals1), PortraitArt.<LedgeHand>g__Y|33_1(5.5, ref CS$<>8__locals1))
        .C(PortraitArt.<LedgeHand>g__X|33_0(19.0, ref CS$<>8__locals1), PortraitArt.<LedgeHand>g__Y|33_1(5.5, ref CS$<>8__locals1), PortraitArt.<LedgeHand>g__X|33_0(20.0, ref CS$<>8__locals1), PortraitArt.<LedgeHand>g__Y|33_1(4.0, ref CS$<>8__locals1), PortraitArt.<LedgeHand>g__X|33_0(23.0, ref CS$<>8__locals1), PortraitArt.<LedgeHand>g__Y|33_1(4.0, ref CS$<>8__locals1))
        .C(PortraitArt.<LedgeHand>g__X|33_0(29.0, ref CS$<>8__locals1), PortraitArt.<LedgeHand>g__Y|33_1(4.0, ref CS$<>8__locals1), PortraitArt.<LedgeHand>g__X|33_0(32.0, ref CS$<>8__locals1), PortraitArt.<LedgeHand>g__Y|33_1(8.0, ref CS$<>8__locals1), PortraitArt.<LedgeHand>g__X|33_0(32.0, ref CS$<>8__locals1), PortraitArt.<LedgeHand>g__Y|33_1(14.0, ref CS$<>8__locals1))
        .C(PortraitArt.<LedgeHand>g__X|33_0(32.0, ref CS$<>8__locals1), PortraitArt.<LedgeHand>g__Y|33_1(20.0, ref CS$<>8__locals1), PortraitArt.<LedgeHand>g__X|33_0(27.0, ref CS$<>8__locals1), PortraitArt.<LedgeHand>g__Y|33_1(23.0, ref CS$<>8__locals1), PortraitArt.<LedgeHand>g__X|33_0(17.0, ref CS$<>8__locals1), PortraitArt.<LedgeHand>g__Y|33_1(23.0, ref CS$<>8__locals1))
        .C(PortraitArt.<LedgeHand>g__X|33_0(7.0, ref CS$<>8__locals1), PortraitArt.<LedgeHand>g__Y|33_1(23.0, ref CS$<>8__locals1), PortraitArt.<LedgeHand>g__X|33_0(2.0, ref CS$<>8__locals1), PortraitArt.<LedgeHand>g__Y|33_1(20.0, ref CS$<>8__locals1), PortraitArt.<LedgeHand>g__X|33_0(2.0, ref CS$<>8__locals1), PortraitArt.<LedgeHand>g__Y|33_1(13.0, ref CS$<>8__locals1))
        .Z();
    }

    // Token: 0x06000D68 RID: 3432 RVA: 0x0056ABFC File Offset: 0x0056ABFC
    private static HairPiece P(Action<PPath> draw)
    {
      PPath ppath = new PPath();
      draw(ppath);
      return new HairPiece(ppath, new PieceFill?(PieceFill.Hair), true);
    }

    // Token: 0x06000D69 RID: 3433 RVA: 0x0056AC24 File Offset: 0x0056AC24
    private static HairPiece E(double x, double y, double w, double h)
    {
      return new HairPiece(PPath.Oval(x, y, w, h), new PieceFill?(PieceFill.Hair), true);
    }

    // Token: 0x06000D6A RID: 3434 RVA: 0x0056AC3C File Offset: 0x0056AC3C
    private static HairPiece O(double x, double y, double w, double h, PieceFill fill = PieceFill.Hair)
    {
      return new HairPiece(PPath.Oval(x, y, w, h), new PieceFill?(fill), true);
    }

    // Token: 0x06000D6B RID: 3435 RVA: 0x0056AC54 File Offset: 0x0056AC54
    private static HairPiece R(double x, double y, double w, double h, double r, PieceFill fill = PieceFill.Hair)
    {
      return new HairPiece(PPath.Rounded(x, y, w, h, r), new PieceFill?(fill), true);
    }

    // Token: 0x06000D6C RID: 3436 RVA: 0x0056AC70 File Offset: 0x0056AC70
    private static HairPiece Poly(params double[] c)
    {
      return PortraitArt.PolyF(PieceFill.Hair, c);
    }

    // Token: 0x06000D6D RID: 3437 RVA: 0x0056AC7C File Offset: 0x0056AC7C
    private static HairPiece PolyF(PieceFill fill, params double[] c)
    {
      PPath ppath = new PPath().M(c[0], c[1]);
      for (int i = 2; i < c.Length; i += 2)
      {
        ppath.L(c[i], c[i + 1]);
      }
      return new HairPiece(ppath.Z(), new PieceFill?(fill), true);
    }

    // Token: 0x06000D6E RID: 3438 RVA: 0x0056ACCC File Offset: 0x0056ACCC
    private static HairPiece Line(params double[] c)
    {
      PPath ppath = new PPath().M(c[0], c[1]);
      for (int i = 2; i < c.Length; i += 2)
      {
        ppath.L(c[i], c[i + 1]);
      }
      return new HairPiece(ppath, null, true);
    }

    // Token: 0x06000D70 RID: 3440 RVA: 0x0056AD74 File Offset: 0x0056AD74
    [CompilerGenerated]
    internal static double <LedgeHand>g__X|33_0(double x, ref PortraitArt.<>c__DisplayClass33_0 A_1)
    {
      return A_1.w * x / 34.0;
    }

    // Token: 0x06000D71 RID: 3441 RVA: 0x0056AD88 File Offset: 0x0056AD88
    [CompilerGenerated]
    internal static double <LedgeHand>g__Y|33_1(double y, ref PortraitArt.<>c__DisplayClass33_0 A_1)
    {
      return A_1.h * y / 26.0;
    }

    // Token: 0x040004CF RID: 1231
    public const double CaretStrokeWidth = 22.400000000000002;

    // Token: 0x040004D0 RID: 1232
    private const double CaretWidth = 48.0;

    // Token: 0x040004D1 RID: 1233
    private const double CaretRise = 16.0;

    // Token: 0x040004D2 RID: 1234
    [TupleElementNames(new string[] { "X", "Y", "W", "H" })]
    [Nullable(0)]
    public static readonly ValueTuple<double, double, double, double> ClassicBodyRect = new ValueTuple<double, double, double, double>(0.0, 45.6, 320.0, 236.8);

    // Token: 0x040004D3 RID: 1235
    private const string WideSvg = "M19.6865 9.83594C20.3452 9.83594 20.9381 10.1139 21.3574 10.5576C21.8768 10.2445 22.4861 10.0654 23.1367 10.0654C23.9457 10.0655 24.6891 10.3442 25.2773 10.8105C26.026 10.2684 26.9327 9.95117 27.9092 9.95117C29.2042 9.95127 30.3756 10.5095 31.2197 11.4102C31.4299 11.3589 31.649 11.3291 31.875 11.3291C33.0851 11.3291 34.1101 12.1096 34.4824 13.1934C35.6073 13.3436 36.4756 14.3047 36.4756 15.4707C36.4755 16.1923 36.1421 16.8353 35.6221 17.2568C36.8521 18.3914 37.6229 20.016 37.623 21.8213C37.623 24.8523 35.4514 27.3744 32.5791 27.9199C30.8959 29.5883 28.5806 30.6201 26.0234 30.6201C24.3464 30.6201 22.7743 30.1742 21.415 29.3984C21.1092 29.675 20.7056 29.8457 20.2607 29.8457C19.8105 29.8457 19.4019 29.6712 19.0947 29.3887C18.591 29.6766 18.0085 29.8427 17.3867 29.8428C15.7829 29.8428 14.4343 28.7484 14.0479 27.2656C11.4247 25.947 9.62321 23.2337 9.62305 20.0977C9.62305 18.0525 10.3894 16.1863 11.6494 14.7695C11.6415 14.6592 11.6367 14.5479 11.6367 14.4355C11.6367 11.8953 13.6961 9.83609 16.2363 9.83594C16.9716 9.83594 17.6668 10.0086 18.2832 10.3154C18.6716 10.0154 19.1578 9.83599 19.6865 9.83594Z";

    // Token: 0x040004D4 RID: 1236
    private const string CompactSvg = "M22.2256 28.0938C21.5862 28.0938 21.0107 27.8241 20.6035 27.3936C20.0988 27.698 19.5064 27.8721 18.874 27.8721C18.0884 27.872 17.3662 27.6013 16.7949 27.1484C16.0678 27.6748 15.1876 27.9824 14.2393 27.9824C12.9805 27.9824 11.8425 27.4393 11.0225 26.5635C10.8182 26.6133 10.6053 26.6426 10.3857 26.6426C9.2111 26.6426 8.21581 25.8856 7.85352 24.834C6.76095 24.6881 5.91805 23.7545 5.91797 22.6221C5.91797 22.4279 5.94563 22.2402 5.99219 22.0605C4.76526 20.8306 3.99906 19.0976 3.99902 17.1758C3.99902 13.5625 6.70149 10.6126 10.0996 10.4307C11.7254 8.86944 13.9313 7.90723 16.3633 7.90723C17.9208 7.90729 19.3864 8.30097 20.666 8.99414C20.9455 8.78527 21.2913 8.65918 21.667 8.65918C22.1035 8.6593 22.4997 8.82798 22.7979 9.10156C23.2876 8.82135 23.8543 8.65918 24.459 8.65918C26.0168 8.65934 27.3269 9.72277 27.7021 11.1631C30.2501 12.4439 31.999 15.0797 31.999 18.126C31.999 20.8569 30.5935 23.2584 28.4668 24.6494C27.8521 25.9656 26.5173 26.8778 24.9688 26.8779C24.7215 26.8779 24.4795 26.854 24.2451 26.8096C23.8881 27.568 23.1193 28.0936 22.2256 28.0938Z";

    // Token: 0x040004D5 RID: 1237
    private static readonly PPath WideTraced = PPath.ParseSvg("M19.6865 9.83594C20.3452 9.83594 20.9381 10.1139 21.3574 10.5576C21.8768 10.2445 22.4861 10.0654 23.1367 10.0654C23.9457 10.0655 24.6891 10.3442 25.2773 10.8105C26.026 10.2684 26.9327 9.95117 27.9092 9.95117C29.2042 9.95127 30.3756 10.5095 31.2197 11.4102C31.4299 11.3589 31.649 11.3291 31.875 11.3291C33.0851 11.3291 34.1101 12.1096 34.4824 13.1934C35.6073 13.3436 36.4756 14.3047 36.4756 15.4707C36.4755 16.1923 36.1421 16.8353 35.6221 17.2568C36.8521 18.3914 37.6229 20.016 37.623 21.8213C37.623 24.8523 35.4514 27.3744 32.5791 27.9199C30.8959 29.5883 28.5806 30.6201 26.0234 30.6201C24.3464 30.6201 22.7743 30.1742 21.415 29.3984C21.1092 29.675 20.7056 29.8457 20.2607 29.8457C19.8105 29.8457 19.4019 29.6712 19.0947 29.3887C18.591 29.6766 18.0085 29.8427 17.3867 29.8428C15.7829 29.8428 14.4343 28.7484 14.0479 27.2656C11.4247 25.947 9.62321 23.2337 9.62305 20.0977C9.62305 18.0525 10.3894 16.1863 11.6494 14.7695C11.6415 14.6592 11.6367 14.5479 11.6367 14.4355C11.6367 11.8953 13.6961 9.83609 16.2363 9.83594C16.9716 9.83594 17.6668 10.0086 18.2832 10.3154C18.6716 10.0154 19.1578 9.83599 19.6865 9.83594Z");

    // Token: 0x040004D6 RID: 1238
    private static readonly PPath CompactTraced = PPath.ParseSvg("M22.2256 28.0938C21.5862 28.0938 21.0107 27.8241 20.6035 27.3936C20.0988 27.698 19.5064 27.8721 18.874 27.8721C18.0884 27.872 17.3662 27.6013 16.7949 27.1484C16.0678 27.6748 15.1876 27.9824 14.2393 27.9824C12.9805 27.9824 11.8425 27.4393 11.0225 26.5635C10.8182 26.6133 10.6053 26.6426 10.3857 26.6426C9.2111 26.6426 8.21581 25.8856 7.85352 24.834C6.76095 24.6881 5.91805 23.7545 5.91797 22.6221C5.91797 22.4279 5.94563 22.2402 5.99219 22.0605C4.76526 20.8306 3.99906 19.0976 3.99902 17.1758C3.99902 13.5625 6.70149 10.6126 10.0996 10.4307C11.7254 8.86944 13.9313 7.90723 16.3633 7.90723C17.9208 7.90729 19.3864 8.30097 20.666 8.99414C20.9455 8.78527 21.2913 8.65918 21.667 8.65918C22.1035 8.6593 22.4997 8.82798 22.7979 9.10156C23.2876 8.82135 23.8543 8.65918 24.459 8.65918C26.0168 8.65934 27.3269 9.72277 27.7021 11.1631C30.2501 12.4439 31.999 15.0797 31.999 18.126C31.999 20.8569 30.5935 23.2584 28.4668 24.6494C27.8521 25.9656 26.5173 26.8778 24.9688 26.8779C24.7215 26.8779 24.4795 26.854 24.2451 26.8096C23.8881 27.568 23.1193 28.0936 22.2256 28.0938Z");
  }
}
