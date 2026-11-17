using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using Windows.Foundation;

namespace HeyClicky.Core.Ink
{
  // Token: 0x0200017F RID: 383
  [NullableContext(1)]
  [Nullable(0)]
  public sealed class ClickyInkSketchModel : IEquatable<ClickyInkSketchModel>
  {
    // Token: 0x060016E5 RID: 5861 RVA: 0x003C0DD0 File Offset: 0x003C0DD0
    public ClickyInkSketchModel(IReadOnlyList<ClickyInkSketchModel.Piece> Pieces)
    {
      this.Pieces = Pieces;
      base..ctor();
    }

    // Token: 0x17000534 RID: 1332
    // (get) Token: 0x060016E6 RID: 5862 RVA: 0x003C0DE0 File Offset: 0x003C0DE0
    [CompilerGenerated]
    private Type EqualityContract
    {
      [CompilerGenerated]
      get
      {
        return typeof(ClickyInkSketchModel);
      }
    }

    // Token: 0x17000535 RID: 1333
    // (get) Token: 0x060016E7 RID: 5863 RVA: 0x003C0DEC File Offset: 0x003C0DEC
    // (set) Token: 0x060016E8 RID: 5864 RVA: 0x003C0DF4 File Offset: 0x003C0DF4
    public IReadOnlyList<ClickyInkSketchModel.Piece> Pieces { get; set; }

    // Token: 0x060016E9 RID: 5865 RVA: 0x003C0E00 File Offset: 0x003C0E00
    public static ClickyInkSketchModel Cached(string text)
    {
      Dictionary<string, ClickyInkSketchModel> cache = ClickyInkSketchModel.Cache;
      ClickyInkSketchModel clickyInkSketchModel2;
      lock (cache)
      {
        ClickyInkSketchModel clickyInkSketchModel;
        if (ClickyInkSketchModel.Cache.TryGetValue(text, out clickyInkSketchModel))
        {
          clickyInkSketchModel2 = clickyInkSketchModel;
        }
        else
        {
          ClickyInkSketchModel clickyInkSketchModel3 = ClickyInkSketchModel.Parse(text);
          if (ClickyInkSketchModel.Cache.Count > 60)
          {
            ClickyInkSketchModel.Cache.Clear();
          }
          ClickyInkSketchModel.Cache[text] = clickyInkSketchModel3;
          clickyInkSketchModel2 = clickyInkSketchModel3;
        }
      }
      return clickyInkSketchModel2;
    }

    // Token: 0x060016EA RID: 5866 RVA: 0x003C0E80 File Offset: 0x003C0E80
    public static ClickyInkSketchModel Parse(string text)
    {
      List<ClickyInkSketchModel.Piece> list = new List<ClickyInkSketchModel.Piece>();
      string[] array = text.Split(new char[] { '\n', '\r', '\v', '\f', '\u0085', '\u2028', '\u2029' }, StringSplitOptions.RemoveEmptyEntries);
      for (int i = 0; i < array.Length; i++)
      {
        List<string> list2 = ClickyInkSketchModel.Tokenize(array[i]);
        if (list2.Count != 0)
        {
          ClickyInkSketchModel.Piece piece = ClickyInkSketchModel.MakePiece(list2[0].Lowered(), list2.Skip<string>(1).ToList<string>());
          if (piece != null)
          {
            list.Add(piece);
            if (list.Count >= 40)
            {
              break;
            }
          }
        }
      }
      return new ClickyInkSketchModel(list);
    }

    // Token: 0x060016EB RID: 5867 RVA: 0x003C0F08 File Offset: 0x003C0F08
    private static List<string> Tokenize(string line)
    {
      List<string> list = new List<string>();
      string text = "";
      bool flag = false;
      foreach (string text2 in line.Characters())
      {
        bool flag2 = text2 == "\"" || text2 == "“" || text2 == "”";
        if (flag2)
        {
          if (flag)
          {
            list.Add("\"" + text);
            text = "";
          }
          else if (text.Length > 0)
          {
            list.Add(text);
            text = "";
          }
          flag = !flag;
        }
        else if (text2 == " " && !flag)
        {
          if (text.Length > 0)
          {
            list.Add(text);
            text = "";
          }
        }
        else
        {
          text += text2;
        }
      }
      if (text.Length > 0)
      {
        list.Add(flag ? ("\"" + text) : text);
      }
      return list;
    }

    // Token: 0x060016EC RID: 5868 RVA: 0x003C100C File Offset: 0x003C100C
    [return: Nullable(2)]
    private static ClickyInkSketchModel.Piece MakePiece(string command, List<string> tokens)
    {
      ClickyInkSketchModel.<>c__DisplayClass17_0 CS$<>8__locals1;
      CS$<>8__locals1.numbers = new List<double>();
      List<Point> list = new List<Point>();
      List<string> list2 = new List<string>();
      ClickyInkSketchModel.Piece piece = new ClickyInkSketchModel.Piece();
      foreach (string text in tokens)
      {
        if (text.HasPrefix("\""))
        {
          string text2 = text;
          string text3 = text2.Substring(1, text2.Length - 1).TrimWs();
          if (text3.Length > 0)
          {
            piece.Label = text3.PrefixCharacters(60);
          }
        }
        else
        {
          string text4 = text.Lowered();
          if (ClickyInkSketchModel.ColorNames.Contains(text4))
          {
            piece.Color = text4;
          }
          else
          {
            if (text4 != null)
            {
              switch (text4.Length)
              {
              case 3:
                if (!(text4 == "big"))
                {
                  goto IL_02D2;
                }
                goto IL_02BA;
              case 4:
              {
                char c = text4[3];
                if (c <= 'h')
                {
                  if (c != 'd')
                  {
                    if (c != 'h')
                    {
                      goto IL_02D2;
                    }
                    if (!(text4 == "dash"))
                    {
                      goto IL_02D2;
                    }
                    goto IL_02A2;
                  }
                  else
                  {
                    if (text4 == "bend")
                    {
                      goto IL_02AE;
                    }
                    if (!(text4 == "bold"))
                    {
                      goto IL_02D2;
                    }
                    goto IL_02BA;
                  }
                }
                else if (c != 'l')
                {
                  if (c != 't')
                  {
                    if (c != 'y')
                    {
                      goto IL_02D2;
                    }
                    if (!(text4 == "tiny"))
                    {
                      goto IL_02D2;
                    }
                    goto IL_02C6;
                  }
                  else
                  {
                    if (!(text4 == "bent"))
                    {
                      goto IL_02D2;
                    }
                    goto IL_02AE;
                  }
                }
                else if (!(text4 == "fill"))
                {
                  goto IL_02D2;
                }
                break;
              }
              case 5:
              {
                char c = text4[0];
                if (c != 'c')
                {
                  if (c != 'l')
                  {
                    if (c != 's')
                    {
                      goto IL_02D2;
                    }
                    if (!(text4 == "small"))
                    {
                      goto IL_02D2;
                    }
                    goto IL_02C6;
                  }
                  else
                  {
                    if (!(text4 == "large"))
                    {
                      goto IL_02D2;
                    }
                    goto IL_02BA;
                  }
                }
                else
                {
                  if (!(text4 == "curve"))
                  {
                    goto IL_02D2;
                  }
                  goto IL_02AE;
                }
                break;
              }
              case 6:
              {
                char c = text4[1];
                if (c <= 'i')
                {
                  if (c != 'a')
                  {
                    if (c != 'i')
                    {
                      goto IL_02D2;
                    }
                    if (!(text4 == "filled") && !(text4 == "fillin"))
                    {
                      goto IL_02D2;
                    }
                  }
                  else
                  {
                    if (!(text4 == "dashed"))
                    {
                      goto IL_02D2;
                    }
                    goto IL_02A2;
                  }
                }
                else if (c != 'o')
                {
                  if (c != 'u')
                  {
                    goto IL_02D2;
                  }
                  if (!(text4 == "curved"))
                  {
                    goto IL_02D2;
                  }
                  goto IL_02AE;
                }
                else
                {
                  if (!(text4 == "dotted"))
                  {
                    goto IL_02D2;
                  }
                  goto IL_02A2;
                }
                break;
              }
              default:
                goto IL_02D2;
              }
              piece.IsFilled = true;
              continue;
              IL_02A2:
              piece.IsDashed = true;
              continue;
              IL_02AE:
              piece.IsCurved = true;
              continue;
              IL_02BA:
              piece.Size = ClickyInkSketchModel.Size.Big;
              continue;
              IL_02C6:
              piece.Size = ClickyInkSketchModel.Size.Small;
              continue;
            }
            IL_02D2:
            double? num;
            if (text4.Contains(','))
            {
              string[] array = text4.Split(',', StringSplitOptions.RemoveEmptyEntries);
              if (array.Length == 2)
              {
                num = InkText.ParseDouble(array[0]);
                if (num != null)
                {
                  double valueOrDefault = num.GetValueOrDefault();
                  num = InkText.ParseDouble(array[1]);
                  if (num != null)
                  {
                    double valueOrDefault2 = num.GetValueOrDefault();
                    list.Add(new Point(valueOrDefault, valueOrDefault2));
                    continue;
                  }
                }
              }
            }
            num = InkText.ParseDouble(text4);
            if (num != null)
            {
              double valueOrDefault3 = num.GetValueOrDefault();
              CS$<>8__locals1.numbers.Add(valueOrDefault3);
            }
            else
            {
              list2.Add(text);
            }
          }
        }
      }
      if (command != null)
      {
        double? num;
        switch (command.Length)
        {
        case 3:
        {
          char c = command[0];
          if (c != 'b')
          {
            if (c != 'd')
            {
              if (c != 'n')
              {
                goto IL_0B51;
              }
              if (!(command == "num"))
              {
                goto IL_0B51;
              }
              goto IL_0AE1;
            }
            else
            {
              if (!(command == "dot"))
              {
                goto IL_0B51;
              }
              goto IL_0A8F;
            }
          }
          else if (!(command == "box"))
          {
            goto IL_0B51;
          }
          break;
        }
        case 4:
        {
          char c = command[2];
          switch (c)
          {
          case 'a':
            if (!(command == "oval"))
            {
              goto IL_0B51;
            }
            goto IL_075C;
          case 'b':
          case 'd':
            goto IL_0B51;
          case 'c':
            if (!(command == "rect"))
            {
              goto IL_0B51;
            }
            break;
          case 'e':
            if (!(command == "step"))
            {
              goto IL_0B51;
            }
            goto IL_0AE1;
          default:
            switch (c)
            {
            case 'l':
              if (!(command == "poly"))
              {
                goto IL_0B51;
              }
              goto IL_0906;
            case 'm':
            case 'p':
            case 'q':
            case 's':
              goto IL_0B51;
            case 'n':
              if (command == "ring")
              {
                goto IL_075C;
              }
              if (!(command == "line"))
              {
                goto IL_0B51;
              }
              num = ClickyInkSketchModel.<MakePiece>g__N|17_0(0, ref CS$<>8__locals1);
              if (num != null)
              {
                double valueOrDefault4 = num.GetValueOrDefault();
                num = ClickyInkSketchModel.<MakePiece>g__N|17_0(1, ref CS$<>8__locals1);
                if (num != null)
                {
                  double valueOrDefault5 = num.GetValueOrDefault();
                  num = ClickyInkSketchModel.<MakePiece>g__N|17_0(2, ref CS$<>8__locals1);
                  if (num != null)
                  {
                    double valueOrDefault6 = num.GetValueOrDefault();
                    num = ClickyInkSketchModel.<MakePiece>g__N|17_0(3, ref CS$<>8__locals1);
                    if (num != null)
                    {
                      double valueOrDefault7 = num.GetValueOrDefault();
                      piece.Shape = new ClickyInkSketchModel.Shape.Line(new Point(valueOrDefault4, valueOrDefault5), new Point(valueOrDefault6, valueOrDefault7));
                      return piece;
                    }
                  }
                }
              }
              return null;
            case 'o':
              if (!(command == "icon"))
              {
                goto IL_0B51;
              }
              goto IL_09F7;
            case 'r':
              if (!(command == "card"))
              {
                goto IL_0B51;
              }
              break;
            case 't':
              if (!(command == "path"))
              {
                goto IL_0B51;
              }
              goto IL_0906;
            default:
              if (c != 'x')
              {
                goto IL_0B51;
              }
              if (!(command == "text"))
              {
                goto IL_0B51;
              }
              goto IL_099C;
            }
            break;
          }
          break;
        }
        case 5:
        {
          char c = command[0];
          if (c <= 'l')
          {
            switch (c)
            {
            case 'a':
              if (!(command == "arrow"))
              {
                goto IL_0B51;
              }
              num = ClickyInkSketchModel.<MakePiece>g__N|17_0(0, ref CS$<>8__locals1);
              if (num != null)
              {
                double valueOrDefault8 = num.GetValueOrDefault();
                num = ClickyInkSketchModel.<MakePiece>g__N|17_0(1, ref CS$<>8__locals1);
                if (num != null)
                {
                  double valueOrDefault9 = num.GetValueOrDefault();
                  num = ClickyInkSketchModel.<MakePiece>g__N|17_0(2, ref CS$<>8__locals1);
                  if (num != null)
                  {
                    double valueOrDefault10 = num.GetValueOrDefault();
                    num = ClickyInkSketchModel.<MakePiece>g__N|17_0(3, ref CS$<>8__locals1);
                    if (num != null)
                    {
                      double valueOrDefault11 = num.GetValueOrDefault();
                      piece.Shape = new ClickyInkSketchModel.Shape.Arrow(new Point(valueOrDefault8, valueOrDefault9), new Point(valueOrDefault10, valueOrDefault11));
                      return piece;
                    }
                  }
                }
              }
              return null;
            case 'b':
              if (!(command == "badge"))
              {
                goto IL_0B51;
              }
              goto IL_0AE1;
            case 'c':
              if (!(command == "curve"))
              {
                goto IL_0B51;
              }
              goto IL_0906;
            default:
              if (c != 'l')
              {
                goto IL_0B51;
              }
              if (!(command == "label"))
              {
                goto IL_0B51;
              }
              goto IL_099C;
            }
          }
          else if (c != 'p')
          {
            if (c != 'w')
            {
              goto IL_0B51;
            }
            if (!(command == "write"))
            {
              goto IL_0B51;
            }
            goto IL_099C;
          }
          else
          {
            if (!(command == "point"))
            {
              goto IL_0B51;
            }
            goto IL_0A8F;
          }
          break;
        }
        case 6:
        {
          char c = command[0];
          if (c != 'c')
          {
            if (c != 'n')
            {
              if (c != 's')
              {
                goto IL_0B51;
              }
              if (!(command == "symbol"))
              {
                goto IL_0B51;
              }
              goto IL_09F7;
            }
            else
            {
              if (!(command == "number"))
              {
                goto IL_0B51;
              }
              goto IL_0AE1;
            }
          }
          else
          {
            if (!(command == "circle"))
            {
              goto IL_0B51;
            }
            goto IL_075C;
          }
          break;
        }
        case 7:
          goto IL_0B51;
        case 8:
          if (!(command == "polyline"))
          {
            goto IL_0B51;
          }
          goto IL_0906;
        case 9:
          if (!(command == "rectangle"))
          {
            goto IL_0B51;
          }
          break;
        default:
          goto IL_0B51;
        }
        num = ClickyInkSketchModel.<MakePiece>g__N|17_0(0, ref CS$<>8__locals1);
        if (num != null)
        {
          double valueOrDefault12 = num.GetValueOrDefault();
          num = ClickyInkSketchModel.<MakePiece>g__N|17_0(1, ref CS$<>8__locals1);
          if (num != null)
          {
            double valueOrDefault13 = num.GetValueOrDefault();
            num = ClickyInkSketchModel.<MakePiece>g__N|17_0(2, ref CS$<>8__locals1);
            if (num != null)
            {
              double valueOrDefault14 = num.GetValueOrDefault();
              num = ClickyInkSketchModel.<MakePiece>g__N|17_0(3, ref CS$<>8__locals1);
              if (num != null)
              {
                double valueOrDefault15 = num.GetValueOrDefault();
                if (valueOrDefault14 > 0.0 && valueOrDefault15 > 0.0)
                {
                  piece.Shape = new ClickyInkSketchModel.Shape.Box(new Rect(valueOrDefault12, valueOrDefault13, valueOrDefault14, valueOrDefault15));
                  return piece;
                }
              }
            }
          }
        }
        return null;
        IL_075C:
        num = ClickyInkSketchModel.<MakePiece>g__N|17_0(0, ref CS$<>8__locals1);
        if (num != null)
        {
          double valueOrDefault16 = num.GetValueOrDefault();
          num = ClickyInkSketchModel.<MakePiece>g__N|17_0(1, ref CS$<>8__locals1);
          if (num != null)
          {
            double valueOrDefault17 = num.GetValueOrDefault();
            num = ClickyInkSketchModel.<MakePiece>g__N|17_0(2, ref CS$<>8__locals1);
            if (num != null)
            {
              double valueOrDefault18 = num.GetValueOrDefault();
              if (valueOrDefault18 > 0.0)
              {
                piece.Shape = new ClickyInkSketchModel.Shape.Circle(new Point(valueOrDefault16, valueOrDefault17), valueOrDefault18);
                return piece;
              }
            }
          }
        }
        return null;
        IL_0906:
        List<Point> list3 = list.ToList<Point>();
        if (list3.Count == 0)
        {
          for (int i = 0; i < CS$<>8__locals1.numbers.Count - 1; i += 2)
          {
            list3.Add(new Point(CS$<>8__locals1.numbers[i], CS$<>8__locals1.numbers[i + 1]));
          }
        }
        if (list3.Count < 2)
        {
          return null;
        }
        piece.Shape = new ClickyInkSketchModel.Shape.Path(list3.Take<Point>(40).ToList<Point>());
        if (command == "curve")
        {
          piece.IsCurved = true;
          return piece;
        }
        return piece;
        IL_099C:
        num = ClickyInkSketchModel.<MakePiece>g__N|17_0(0, ref CS$<>8__locals1);
        if (num != null)
        {
          double valueOrDefault19 = num.GetValueOrDefault();
          num = ClickyInkSketchModel.<MakePiece>g__N|17_0(1, ref CS$<>8__locals1);
          if (num != null)
          {
            double valueOrDefault20 = num.GetValueOrDefault();
            if (piece.Label != null)
            {
              piece.Shape = new ClickyInkSketchModel.Shape.Text(new Point(valueOrDefault19, valueOrDefault20));
              return piece;
            }
          }
        }
        return null;
        IL_09F7:
        num = ClickyInkSketchModel.<MakePiece>g__N|17_0(0, ref CS$<>8__locals1);
        if (num != null)
        {
          double valueOrDefault21 = num.GetValueOrDefault();
          num = ClickyInkSketchModel.<MakePiece>g__N|17_0(1, ref CS$<>8__locals1);
          if (num != null)
          {
            double valueOrDefault22 = num.GetValueOrDefault();
            if (list2.Count != 0)
            {
              piece.Shape = new ClickyInkSketchModel.Shape.Icon(new Point(valueOrDefault21, valueOrDefault22), list2[0], Math.Max(10.0, Math.Min(64.0, ClickyInkSketchModel.<MakePiece>g__N|17_0(2, ref CS$<>8__locals1).GetValueOrDefault(24.0))));
              return piece;
            }
          }
        }
        return null;
        IL_0A8F:
        num = ClickyInkSketchModel.<MakePiece>g__N|17_0(0, ref CS$<>8__locals1);
        if (num != null)
        {
          double valueOrDefault23 = num.GetValueOrDefault();
          num = ClickyInkSketchModel.<MakePiece>g__N|17_0(1, ref CS$<>8__locals1);
          if (num != null)
          {
            double valueOrDefault24 = num.GetValueOrDefault();
            piece.Shape = new ClickyInkSketchModel.Shape.Dot(new Point(valueOrDefault23, valueOrDefault24));
            return piece;
          }
        }
        return null;
        IL_0AE1:
        num = ClickyInkSketchModel.<MakePiece>g__N|17_0(0, ref CS$<>8__locals1);
        if (num != null)
        {
          double valueOrDefault25 = num.GetValueOrDefault();
          num = ClickyInkSketchModel.<MakePiece>g__N|17_0(1, ref CS$<>8__locals1);
          if (num != null)
          {
            double valueOrDefault26 = num.GetValueOrDefault();
            piece.Shape = new ClickyInkSketchModel.Shape.Badge(new Point(valueOrDefault25, valueOrDefault26));
            if (piece.Label == null && list2.Count > 0)
            {
              piece.Label = list2[0];
              return piece;
            }
            return piece;
          }
        }
        return null;
      }
      IL_0B51:
      return null;
    }

    // Token: 0x060016ED RID: 5869 RVA: 0x003C1B8C File Offset: 0x003C1B8C
    public static double Step(int count)
    {
      return Math.Min(0.28, 3.2 / (double)Math.Max(count, 1));
    }

    // Token: 0x060016EE RID: 5870 RVA: 0x003C1BB0 File Offset: 0x003C1BB0
    public static double DrawingSeconds(string text)
    {
      int count = ClickyInkSketchModel.Parse(text).Pieces.Count;
      return 0.15 + (double)Math.Max(count - 1, 0) * ClickyInkSketchModel.Step(count) + 0.45 + 0.3;
    }

    // Token: 0x060016EF RID: 5871 RVA: 0x003C1C00 File Offset: 0x003C1C00
    [NullableContext(2)]
    public bool Equals(ClickyInkSketchModel other)
    {
      return other != null && this.Pieces.SequenceEqual<ClickyInkSketchModel.Piece>(other.Pieces);
    }

    // Token: 0x060016F0 RID: 5872 RVA: 0x003C1C18 File Offset: 0x003C1C18
    public override int GetHashCode()
    {
      return this.Pieces.Count;
    }

    // Token: 0x060016F1 RID: 5873 RVA: 0x003C1C28 File Offset: 0x003C1C28
    [CompilerGenerated]
    public override string ToString()
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("ClickyInkSketchModel");
      stringBuilder.Append(" { ");
      if (this.PrintMembers(stringBuilder))
      {
        stringBuilder.Append(' ');
      }
      stringBuilder.Append('}');
      return stringBuilder.ToString();
    }

    // Token: 0x060016F2 RID: 5874 RVA: 0x003C1C74 File Offset: 0x003C1C74
    [CompilerGenerated]
    private bool PrintMembers(StringBuilder builder)
    {
      RuntimeHelpers.EnsureSufficientExecutionStack();
      builder.Append("Pieces = ");
      builder.Append(this.Pieces);
      return true;
    }

    // Token: 0x060016F3 RID: 5875 RVA: 0x003C1C98 File Offset: 0x003C1C98
    [NullableContext(2)]
    [CompilerGenerated]
    public static bool operator !=(ClickyInkSketchModel left, ClickyInkSketchModel right)
    {
      return !(left == right);
    }

    // Token: 0x060016F4 RID: 5876 RVA: 0x003C1CA4 File Offset: 0x003C1CA4
    [NullableContext(2)]
    [CompilerGenerated]
    public static bool operator ==(ClickyInkSketchModel left, ClickyInkSketchModel right)
    {
      return left == right || (left != null && left.Equals(right));
    }

    // Token: 0x060016F5 RID: 5877 RVA: 0x003C1CB8 File Offset: 0x003C1CB8
    [NullableContext(2)]
    [CompilerGenerated]
    public override bool Equals(object obj)
    {
      return this.Equals(obj as ClickyInkSketchModel);
    }

    // Token: 0x060016F7 RID: 5879 RVA: 0x003C1CD0 File Offset: 0x003C1CD0
    [CompilerGenerated]
    private ClickyInkSketchModel(ClickyInkSketchModel original)
    {
      this.Pieces = original.<Pieces>k__BackingField;
    }

    // Token: 0x060016F8 RID: 5880 RVA: 0x003C1CE4 File Offset: 0x003C1CE4
    [CompilerGenerated]
    public void Deconstruct(out IReadOnlyList<ClickyInkSketchModel.Piece> Pieces)
    {
      Pieces = this.Pieces;
    }

    // Token: 0x060016FA RID: 5882 RVA: 0x003C1D98 File Offset: 0x003C1D98
    [CompilerGenerated]
    internal static double? <MakePiece>g__N|17_0(int index, ref ClickyInkSketchModel.<>c__DisplayClass17_0 A_1)
    {
      if (index >= A_1.numbers.Count)
      {
        return null;
      }
      return new double?(A_1.numbers[index]);
    }

    // Token: 0x040009F4 RID: 2548
    public const double CanvasWidth = 380.0;

    // Token: 0x040009F5 RID: 2549
    public const double DefaultHeight = 160.0;

    // Token: 0x040009F6 RID: 2550
    public static readonly IReadOnlySet<string> ColorNames = new HashSet<string>
    {
      "ink", "black", "blue", "red", "green", "orange", "purple", "pink", "yellow", "gray",
      "grey"
    };

    // Token: 0x040009F7 RID: 2551
    private static readonly Dictionary<string, ClickyInkSketchModel> Cache = new Dictionary<string, ClickyInkSketchModel>();

    // Token: 0x040009F8 RID: 2552
    public const double LeadSeconds = 0.15;

    // Token: 0x040009F9 RID: 2553
    public const double PieceSeconds = 0.45;

    // Token: 0x0200055C RID: 1372
    [Nullable(0)]
    public abstract class Shape : IEquatable<ClickyInkSketchModel.Shape>
    {
      // Token: 0x1700094E RID: 2382
      // (get) Token: 0x06003476 RID: 13430 RVA: 0x0043F4C8 File Offset: 0x0043F4C8
      [CompilerGenerated]
      protected virtual Type EqualityContract
      {
        [CompilerGenerated]
        get
        {
          return typeof(ClickyInkSketchModel.Shape);
        }
      }

      // Token: 0x06003477 RID: 13431 RVA: 0x0043F4D4 File Offset: 0x0043F4D4
      [CompilerGenerated]
      public override string ToString()
      {
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.Append("Shape");
        stringBuilder.Append(" { ");
        if (this.PrintMembers(stringBuilder))
        {
          stringBuilder.Append(' ');
        }
        stringBuilder.Append('}');
        return stringBuilder.ToString();
      }

      // Token: 0x06003478 RID: 13432 RVA: 0x0043F520 File Offset: 0x0043F520
      [CompilerGenerated]
      protected virtual bool PrintMembers(StringBuilder builder)
      {
        return false;
      }

      // Token: 0x06003479 RID: 13433 RVA: 0x0043F524 File Offset: 0x0043F524
      [NullableContext(2)]
      [CompilerGenerated]
      public static bool operator !=(ClickyInkSketchModel.Shape left, ClickyInkSketchModel.Shape right)
      {
        return !(left == right);
      }

      // Token: 0x0600347A RID: 13434 RVA: 0x0043F530 File Offset: 0x0043F530
      [NullableContext(2)]
      [CompilerGenerated]
      public static bool operator ==(ClickyInkSketchModel.Shape left, ClickyInkSketchModel.Shape right)
      {
        return left == right || (left != null && left.Equals(right));
      }

      // Token: 0x0600347B RID: 13435 RVA: 0x0043F544 File Offset: 0x0043F544
      [CompilerGenerated]
      public override int GetHashCode()
      {
        return EqualityComparer<Type>.Default.GetHashCode(this.EqualityContract);
      }

      // Token: 0x0600347C RID: 13436 RVA: 0x0043F558 File Offset: 0x0043F558
      [NullableContext(2)]
      [CompilerGenerated]
      public override bool Equals(object obj)
      {
        return this.Equals(obj as ClickyInkSketchModel.Shape);
      }

      // Token: 0x0600347D RID: 13437 RVA: 0x0043F568 File Offset: 0x0043F568
      [NullableContext(2)]
      [CompilerGenerated]
      public virtual bool Equals(ClickyInkSketchModel.Shape other)
      {
        return this == other || (other != null && this.EqualityContract == other.EqualityContract);
      }

      // Token: 0x0600347F RID: 13439 RVA: 0x0043F588 File Offset: 0x0043F588
      [CompilerGenerated]
      protected Shape(ClickyInkSketchModel.Shape original)
      {
      }

      // Token: 0x06003480 RID: 13440 RVA: 0x0043F590 File Offset: 0x0043F590
      protected Shape()
      {
      }

      // Token: 0x02000703 RID: 1795
      [Nullable(0)]
      public sealed class Box : ClickyInkSketchModel.Shape, IEquatable<ClickyInkSketchModel.Shape.Box>
      {
        // Token: 0x06003C2B RID: 15403 RVA: 0x004669E0 File Offset: 0x004669E0
        public Box(Rect Rect)
        {
          this.Rect = Rect;
          base..ctor();
        }

        // Token: 0x17000A15 RID: 2581
        // (get) Token: 0x06003C2C RID: 15404 RVA: 0x004669F0 File Offset: 0x004669F0
        [CompilerGenerated]
        protected override Type EqualityContract
        {
          [CompilerGenerated]
          get
          {
            return typeof(ClickyInkSketchModel.Shape.Box);
          }
        }

        // Token: 0x17000A16 RID: 2582
        // (get) Token: 0x06003C2D RID: 15405 RVA: 0x004669FC File Offset: 0x004669FC
        // (set) Token: 0x06003C2E RID: 15406 RVA: 0x00466A04 File Offset: 0x00466A04
        public Rect Rect { get; set; }

        // Token: 0x06003C2F RID: 15407 RVA: 0x00466A10 File Offset: 0x00466A10
        [CompilerGenerated]
        public override string ToString()
        {
          StringBuilder stringBuilder = new StringBuilder();
          stringBuilder.Append("Box");
          stringBuilder.Append(" { ");
          if (this.PrintMembers(stringBuilder))
          {
            stringBuilder.Append(' ');
          }
          stringBuilder.Append('}');
          return stringBuilder.ToString();
        }

        // Token: 0x06003C30 RID: 15408 RVA: 0x00466A5C File Offset: 0x00466A5C
        [CompilerGenerated]
        protected override bool PrintMembers(StringBuilder builder)
        {
          if (base.PrintMembers(builder))
          {
            builder.Append(", ");
          }
          builder.Append("Rect = ");
          builder.Append(this.Rect.ToString());
          return true;
        }

        // Token: 0x06003C31 RID: 15409 RVA: 0x00466AA8 File Offset: 0x00466AA8
        [NullableContext(2)]
        [CompilerGenerated]
        public static bool operator !=(ClickyInkSketchModel.Shape.Box left, ClickyInkSketchModel.Shape.Box right)
        {
          return !(left == right);
        }

        // Token: 0x06003C32 RID: 15410 RVA: 0x00466AB4 File Offset: 0x00466AB4
        [NullableContext(2)]
        [CompilerGenerated]
        public static bool operator ==(ClickyInkSketchModel.Shape.Box left, ClickyInkSketchModel.Shape.Box right)
        {
          return left == right || (left != null && left.Equals(right));
        }

        // Token: 0x06003C33 RID: 15411 RVA: 0x00466AC8 File Offset: 0x00466AC8
        [CompilerGenerated]
        public override int GetHashCode()
        {
          return base.GetHashCode() * -1521134295 + EqualityComparer<Rect>.Default.GetHashCode(this.<Rect>k__BackingField);
        }

        // Token: 0x06003C34 RID: 15412 RVA: 0x00466AE8 File Offset: 0x00466AE8
        [NullableContext(2)]
        [CompilerGenerated]
        public override bool Equals(object obj)
        {
          return this.Equals(obj as ClickyInkSketchModel.Shape.Box);
        }

        // Token: 0x06003C35 RID: 15413 RVA: 0x00466AF8 File Offset: 0x00466AF8
        [NullableContext(2)]
        [CompilerGenerated]
        public sealed override bool Equals(ClickyInkSketchModel.Shape other)
        {
          return this.Equals(other);
        }

        // Token: 0x06003C36 RID: 15414 RVA: 0x00466B04 File Offset: 0x00466B04
        [NullableContext(2)]
        [CompilerGenerated]
        public bool Equals(ClickyInkSketchModel.Shape.Box other)
        {
          return this == other || (base.Equals(other) && EqualityComparer<Rect>.Default.Equals(this.<Rect>k__BackingField, other.<Rect>k__BackingField));
        }

        // Token: 0x06003C38 RID: 15416 RVA: 0x00466B38 File Offset: 0x00466B38
        [CompilerGenerated]
        private Box(ClickyInkSketchModel.Shape.Box original)
          : base(original)
        {
          this.Rect = original.<Rect>k__BackingField;
        }

        // Token: 0x06003C39 RID: 15417 RVA: 0x00466B50 File Offset: 0x00466B50
        [CompilerGenerated]
        public void Deconstruct(out Rect Rect)
        {
          Rect = this.Rect;
        }
      }

      // Token: 0x02000704 RID: 1796
      [Nullable(0)]
      public sealed class Circle : ClickyInkSketchModel.Shape, IEquatable<ClickyInkSketchModel.Shape.Circle>
      {
        // Token: 0x06003C3A RID: 15418 RVA: 0x00466B60 File Offset: 0x00466B60
        public Circle(Point Center, double Radius)
        {
          this.Center = Center;
          this.Radius = Radius;
          base..ctor();
        }

        // Token: 0x17000A17 RID: 2583
        // (get) Token: 0x06003C3B RID: 15419 RVA: 0x00466B78 File Offset: 0x00466B78
        [CompilerGenerated]
        protected override Type EqualityContract
        {
          [CompilerGenerated]
          get
          {
            return typeof(ClickyInkSketchModel.Shape.Circle);
          }
        }

        // Token: 0x17000A18 RID: 2584
        // (get) Token: 0x06003C3C RID: 15420 RVA: 0x00466B84 File Offset: 0x00466B84
        // (set) Token: 0x06003C3D RID: 15421 RVA: 0x00466B8C File Offset: 0x00466B8C
        public Point Center { get; set; }

        // Token: 0x17000A19 RID: 2585
        // (get) Token: 0x06003C3E RID: 15422 RVA: 0x00466B98 File Offset: 0x00466B98
        // (set) Token: 0x06003C3F RID: 15423 RVA: 0x00466BA0 File Offset: 0x00466BA0
        public double Radius { get; set; }

        // Token: 0x06003C40 RID: 15424 RVA: 0x00466BAC File Offset: 0x00466BAC
        [CompilerGenerated]
        public override string ToString()
        {
          StringBuilder stringBuilder = new StringBuilder();
          stringBuilder.Append("Circle");
          stringBuilder.Append(" { ");
          if (this.PrintMembers(stringBuilder))
          {
            stringBuilder.Append(' ');
          }
          stringBuilder.Append('}');
          return stringBuilder.ToString();
        }

        // Token: 0x06003C41 RID: 15425 RVA: 0x00466BF8 File Offset: 0x00466BF8
        [CompilerGenerated]
        protected override bool PrintMembers(StringBuilder builder)
        {
          if (base.PrintMembers(builder))
          {
            builder.Append(", ");
          }
          builder.Append("Center = ");
          builder.Append(this.Center.ToString());
          builder.Append(", Radius = ");
          builder.Append(this.Radius.ToString());
          return true;
        }

        // Token: 0x06003C42 RID: 15426 RVA: 0x00466C6C File Offset: 0x00466C6C
        [NullableContext(2)]
        [CompilerGenerated]
        public static bool operator !=(ClickyInkSketchModel.Shape.Circle left, ClickyInkSketchModel.Shape.Circle right)
        {
          return !(left == right);
        }

        // Token: 0x06003C43 RID: 15427 RVA: 0x00466C78 File Offset: 0x00466C78
        [NullableContext(2)]
        [CompilerGenerated]
        public static bool operator ==(ClickyInkSketchModel.Shape.Circle left, ClickyInkSketchModel.Shape.Circle right)
        {
          return left == right || (left != null && left.Equals(right));
        }

        // Token: 0x06003C44 RID: 15428 RVA: 0x00466C8C File Offset: 0x00466C8C
        [CompilerGenerated]
        public override int GetHashCode()
        {
          return (base.GetHashCode() * -1521134295 + EqualityComparer<Point>.Default.GetHashCode(this.<Center>k__BackingField)) * -1521134295 + EqualityComparer<double>.Default.GetHashCode(this.<Radius>k__BackingField);
        }

        // Token: 0x06003C45 RID: 15429 RVA: 0x00466CC4 File Offset: 0x00466CC4
        [NullableContext(2)]
        [CompilerGenerated]
        public override bool Equals(object obj)
        {
          return this.Equals(obj as ClickyInkSketchModel.Shape.Circle);
        }

        // Token: 0x06003C46 RID: 15430 RVA: 0x00466CD4 File Offset: 0x00466CD4
        [NullableContext(2)]
        [CompilerGenerated]
        public sealed override bool Equals(ClickyInkSketchModel.Shape other)
        {
          return this.Equals(other);
        }

        // Token: 0x06003C47 RID: 15431 RVA: 0x00466CE0 File Offset: 0x00466CE0
        [NullableContext(2)]
        [CompilerGenerated]
        public bool Equals(ClickyInkSketchModel.Shape.Circle other)
        {
          return this == other || (base.Equals(other) && EqualityComparer<Point>.Default.Equals(this.<Center>k__BackingField, other.<Center>k__BackingField) && EqualityComparer<double>.Default.Equals(this.<Radius>k__BackingField, other.<Radius>k__BackingField));
        }

        // Token: 0x06003C49 RID: 15433 RVA: 0x00466D34 File Offset: 0x00466D34
        [CompilerGenerated]
        private Circle(ClickyInkSketchModel.Shape.Circle original)
          : base(original)
        {
          this.Center = original.<Center>k__BackingField;
          this.Radius = original.<Radius>k__BackingField;
        }

        // Token: 0x06003C4A RID: 15434 RVA: 0x00466D58 File Offset: 0x00466D58
        [CompilerGenerated]
        public void Deconstruct(out Point Center, out double Radius)
        {
          Center = this.Center;
          Radius = this.Radius;
        }
      }

      // Token: 0x02000705 RID: 1797
      [Nullable(0)]
      public sealed class Line : ClickyInkSketchModel.Shape, IEquatable<ClickyInkSketchModel.Shape.Line>
      {
        // Token: 0x06003C4B RID: 15435 RVA: 0x00466D70 File Offset: 0x00466D70
        public Line(Point From, Point To)
        {
          this.From = From;
          this.To = To;
          base..ctor();
        }

        // Token: 0x17000A1A RID: 2586
        // (get) Token: 0x06003C4C RID: 15436 RVA: 0x00466D88 File Offset: 0x00466D88
        [CompilerGenerated]
        protected override Type EqualityContract
        {
          [CompilerGenerated]
          get
          {
            return typeof(ClickyInkSketchModel.Shape.Line);
          }
        }

        // Token: 0x17000A1B RID: 2587
        // (get) Token: 0x06003C4D RID: 15437 RVA: 0x00466D94 File Offset: 0x00466D94
        // (set) Token: 0x06003C4E RID: 15438 RVA: 0x00466D9C File Offset: 0x00466D9C
        public Point From { get; set; }

        // Token: 0x17000A1C RID: 2588
        // (get) Token: 0x06003C4F RID: 15439 RVA: 0x00466DA8 File Offset: 0x00466DA8
        // (set) Token: 0x06003C50 RID: 15440 RVA: 0x00466DB0 File Offset: 0x00466DB0
        public Point To { get; set; }

        // Token: 0x06003C51 RID: 15441 RVA: 0x00466DBC File Offset: 0x00466DBC
        [CompilerGenerated]
        public override string ToString()
        {
          StringBuilder stringBuilder = new StringBuilder();
          stringBuilder.Append("Line");
          stringBuilder.Append(" { ");
          if (this.PrintMembers(stringBuilder))
          {
            stringBuilder.Append(' ');
          }
          stringBuilder.Append('}');
          return stringBuilder.ToString();
        }

        // Token: 0x06003C52 RID: 15442 RVA: 0x00466E08 File Offset: 0x00466E08
        [CompilerGenerated]
        protected override bool PrintMembers(StringBuilder builder)
        {
          if (base.PrintMembers(builder))
          {
            builder.Append(", ");
          }
          builder.Append("From = ");
          builder.Append(this.From.ToString());
          builder.Append(", To = ");
          builder.Append(this.To.ToString());
          return true;
        }

        // Token: 0x06003C53 RID: 15443 RVA: 0x00466E7C File Offset: 0x00466E7C
        [NullableContext(2)]
        [CompilerGenerated]
        public static bool operator !=(ClickyInkSketchModel.Shape.Line left, ClickyInkSketchModel.Shape.Line right)
        {
          return !(left == right);
        }

        // Token: 0x06003C54 RID: 15444 RVA: 0x00466E88 File Offset: 0x00466E88
        [NullableContext(2)]
        [CompilerGenerated]
        public static bool operator ==(ClickyInkSketchModel.Shape.Line left, ClickyInkSketchModel.Shape.Line right)
        {
          return left == right || (left != null && left.Equals(right));
        }

        // Token: 0x06003C55 RID: 15445 RVA: 0x00466E9C File Offset: 0x00466E9C
        [CompilerGenerated]
        public override int GetHashCode()
        {
          return (base.GetHashCode() * -1521134295 + EqualityComparer<Point>.Default.GetHashCode(this.<From>k__BackingField)) * -1521134295 + EqualityComparer<Point>.Default.GetHashCode(this.<To>k__BackingField);
        }

        // Token: 0x06003C56 RID: 15446 RVA: 0x00466ED4 File Offset: 0x00466ED4
        [NullableContext(2)]
        [CompilerGenerated]
        public override bool Equals(object obj)
        {
          return this.Equals(obj as ClickyInkSketchModel.Shape.Line);
        }

        // Token: 0x06003C57 RID: 15447 RVA: 0x00466EE4 File Offset: 0x00466EE4
        [NullableContext(2)]
        [CompilerGenerated]
        public sealed override bool Equals(ClickyInkSketchModel.Shape other)
        {
          return this.Equals(other);
        }

        // Token: 0x06003C58 RID: 15448 RVA: 0x00466EF0 File Offset: 0x00466EF0
        [NullableContext(2)]
        [CompilerGenerated]
        public bool Equals(ClickyInkSketchModel.Shape.Line other)
        {
          return this == other || (base.Equals(other) && EqualityComparer<Point>.Default.Equals(this.<From>k__BackingField, other.<From>k__BackingField) && EqualityComparer<Point>.Default.Equals(this.<To>k__BackingField, other.<To>k__BackingField));
        }

        // Token: 0x06003C5A RID: 15450 RVA: 0x00466F44 File Offset: 0x00466F44
        [CompilerGenerated]
        private Line(ClickyInkSketchModel.Shape.Line original)
          : base(original)
        {
          this.From = original.<From>k__BackingField;
          this.To = original.<To>k__BackingField;
        }

        // Token: 0x06003C5B RID: 15451 RVA: 0x00466F68 File Offset: 0x00466F68
        [CompilerGenerated]
        public void Deconstruct(out Point From, out Point To)
        {
          From = this.From;
          To = this.To;
        }
      }

      // Token: 0x02000706 RID: 1798
      [Nullable(0)]
      public sealed class Arrow : ClickyInkSketchModel.Shape, IEquatable<ClickyInkSketchModel.Shape.Arrow>
      {
        // Token: 0x06003C5C RID: 15452 RVA: 0x00466F84 File Offset: 0x00466F84
        public Arrow(Point From, Point To)
        {
          this.From = From;
          this.To = To;
          base..ctor();
        }

        // Token: 0x17000A1D RID: 2589
        // (get) Token: 0x06003C5D RID: 15453 RVA: 0x00466F9C File Offset: 0x00466F9C
        [CompilerGenerated]
        protected override Type EqualityContract
        {
          [CompilerGenerated]
          get
          {
            return typeof(ClickyInkSketchModel.Shape.Arrow);
          }
        }

        // Token: 0x17000A1E RID: 2590
        // (get) Token: 0x06003C5E RID: 15454 RVA: 0x00466FA8 File Offset: 0x00466FA8
        // (set) Token: 0x06003C5F RID: 15455 RVA: 0x00466FB0 File Offset: 0x00466FB0
        public Point From { get; set; }

        // Token: 0x17000A1F RID: 2591
        // (get) Token: 0x06003C60 RID: 15456 RVA: 0x00466FBC File Offset: 0x00466FBC
        // (set) Token: 0x06003C61 RID: 15457 RVA: 0x00466FC4 File Offset: 0x00466FC4
        public Point To { get; set; }

        // Token: 0x06003C62 RID: 15458 RVA: 0x00466FD0 File Offset: 0x00466FD0
        [CompilerGenerated]
        public override string ToString()
        {
          StringBuilder stringBuilder = new StringBuilder();
          stringBuilder.Append("Arrow");
          stringBuilder.Append(" { ");
          if (this.PrintMembers(stringBuilder))
          {
            stringBuilder.Append(' ');
          }
          stringBuilder.Append('}');
          return stringBuilder.ToString();
        }

        // Token: 0x06003C63 RID: 15459 RVA: 0x0046701C File Offset: 0x0046701C
        [CompilerGenerated]
        protected override bool PrintMembers(StringBuilder builder)
        {
          if (base.PrintMembers(builder))
          {
            builder.Append(", ");
          }
          builder.Append("From = ");
          builder.Append(this.From.ToString());
          builder.Append(", To = ");
          builder.Append(this.To.ToString());
          return true;
        }

        // Token: 0x06003C64 RID: 15460 RVA: 0x00467090 File Offset: 0x00467090
        [NullableContext(2)]
        [CompilerGenerated]
        public static bool operator !=(ClickyInkSketchModel.Shape.Arrow left, ClickyInkSketchModel.Shape.Arrow right)
        {
          return !(left == right);
        }

        // Token: 0x06003C65 RID: 15461 RVA: 0x0046709C File Offset: 0x0046709C
        [NullableContext(2)]
        [CompilerGenerated]
        public static bool operator ==(ClickyInkSketchModel.Shape.Arrow left, ClickyInkSketchModel.Shape.Arrow right)
        {
          return left == right || (left != null && left.Equals(right));
        }

        // Token: 0x06003C66 RID: 15462 RVA: 0x004670B0 File Offset: 0x004670B0
        [CompilerGenerated]
        public override int GetHashCode()
        {
          return (base.GetHashCode() * -1521134295 + EqualityComparer<Point>.Default.GetHashCode(this.<From>k__BackingField)) * -1521134295 + EqualityComparer<Point>.Default.GetHashCode(this.<To>k__BackingField);
        }

        // Token: 0x06003C67 RID: 15463 RVA: 0x004670E8 File Offset: 0x004670E8
        [NullableContext(2)]
        [CompilerGenerated]
        public override bool Equals(object obj)
        {
          return this.Equals(obj as ClickyInkSketchModel.Shape.Arrow);
        }

        // Token: 0x06003C68 RID: 15464 RVA: 0x004670F8 File Offset: 0x004670F8
        [NullableContext(2)]
        [CompilerGenerated]
        public sealed override bool Equals(ClickyInkSketchModel.Shape other)
        {
          return this.Equals(other);
        }

        // Token: 0x06003C69 RID: 15465 RVA: 0x00467104 File Offset: 0x00467104
        [NullableContext(2)]
        [CompilerGenerated]
        public bool Equals(ClickyInkSketchModel.Shape.Arrow other)
        {
          return this == other || (base.Equals(other) && EqualityComparer<Point>.Default.Equals(this.<From>k__BackingField, other.<From>k__BackingField) && EqualityComparer<Point>.Default.Equals(this.<To>k__BackingField, other.<To>k__BackingField));
        }

        // Token: 0x06003C6B RID: 15467 RVA: 0x00467158 File Offset: 0x00467158
        [CompilerGenerated]
        private Arrow(ClickyInkSketchModel.Shape.Arrow original)
          : base(original)
        {
          this.From = original.<From>k__BackingField;
          this.To = original.<To>k__BackingField;
        }

        // Token: 0x06003C6C RID: 15468 RVA: 0x0046717C File Offset: 0x0046717C
        [CompilerGenerated]
        public void Deconstruct(out Point From, out Point To)
        {
          From = this.From;
          To = this.To;
        }
      }

      // Token: 0x02000707 RID: 1799
      [Nullable(0)]
      public sealed class Path : ClickyInkSketchModel.Shape, IEquatable<ClickyInkSketchModel.Shape.Path>
      {
        // Token: 0x06003C6D RID: 15469 RVA: 0x00467198 File Offset: 0x00467198
        public Path(IReadOnlyList<Point> Points)
        {
          this.Points = Points;
          base..ctor();
        }

        // Token: 0x17000A20 RID: 2592
        // (get) Token: 0x06003C6E RID: 15470 RVA: 0x004671A8 File Offset: 0x004671A8
        [CompilerGenerated]
        protected override Type EqualityContract
        {
          [CompilerGenerated]
          get
          {
            return typeof(ClickyInkSketchModel.Shape.Path);
          }
        }

        // Token: 0x17000A21 RID: 2593
        // (get) Token: 0x06003C6F RID: 15471 RVA: 0x004671B4 File Offset: 0x004671B4
        // (set) Token: 0x06003C70 RID: 15472 RVA: 0x004671BC File Offset: 0x004671BC
        public IReadOnlyList<Point> Points { get; set; }

        // Token: 0x06003C71 RID: 15473 RVA: 0x004671C8 File Offset: 0x004671C8
        [NullableContext(2)]
        public bool Equals(ClickyInkSketchModel.Shape.Path other)
        {
          return other != null && this.Points.SequenceEqual<Point>(other.Points);
        }

        // Token: 0x06003C72 RID: 15474 RVA: 0x004671E0 File Offset: 0x004671E0
        public override int GetHashCode()
        {
          return this.Points.Count;
        }

        // Token: 0x06003C73 RID: 15475 RVA: 0x004671F0 File Offset: 0x004671F0
        [CompilerGenerated]
        public override string ToString()
        {
          StringBuilder stringBuilder = new StringBuilder();
          stringBuilder.Append("Path");
          stringBuilder.Append(" { ");
          if (this.PrintMembers(stringBuilder))
          {
            stringBuilder.Append(' ');
          }
          stringBuilder.Append('}');
          return stringBuilder.ToString();
        }

        // Token: 0x06003C74 RID: 15476 RVA: 0x0046723C File Offset: 0x0046723C
        [CompilerGenerated]
        protected override bool PrintMembers(StringBuilder builder)
        {
          if (base.PrintMembers(builder))
          {
            builder.Append(", ");
          }
          builder.Append("Points = ");
          builder.Append(this.Points);
          return true;
        }

        // Token: 0x06003C75 RID: 15477 RVA: 0x00467270 File Offset: 0x00467270
        [NullableContext(2)]
        [CompilerGenerated]
        public static bool operator !=(ClickyInkSketchModel.Shape.Path left, ClickyInkSketchModel.Shape.Path right)
        {
          return !(left == right);
        }

        // Token: 0x06003C76 RID: 15478 RVA: 0x0046727C File Offset: 0x0046727C
        [NullableContext(2)]
        [CompilerGenerated]
        public static bool operator ==(ClickyInkSketchModel.Shape.Path left, ClickyInkSketchModel.Shape.Path right)
        {
          return left == right || (left != null && left.Equals(right));
        }

        // Token: 0x06003C77 RID: 15479 RVA: 0x00467290 File Offset: 0x00467290
        [NullableContext(2)]
        [CompilerGenerated]
        public override bool Equals(object obj)
        {
          return this.Equals(obj as ClickyInkSketchModel.Shape.Path);
        }

        // Token: 0x06003C78 RID: 15480 RVA: 0x004672A0 File Offset: 0x004672A0
        [NullableContext(2)]
        [CompilerGenerated]
        public sealed override bool Equals(ClickyInkSketchModel.Shape other)
        {
          return this.Equals(other);
        }

        // Token: 0x06003C7A RID: 15482 RVA: 0x004672B4 File Offset: 0x004672B4
        [CompilerGenerated]
        private Path(ClickyInkSketchModel.Shape.Path original)
          : base(original)
        {
          this.Points = original.<Points>k__BackingField;
        }

        // Token: 0x06003C7B RID: 15483 RVA: 0x004672CC File Offset: 0x004672CC
        [CompilerGenerated]
        public void Deconstruct(out IReadOnlyList<Point> Points)
        {
          Points = this.Points;
        }
      }

      // Token: 0x02000708 RID: 1800
      [Nullable(0)]
      public sealed class Text : ClickyInkSketchModel.Shape, IEquatable<ClickyInkSketchModel.Shape.Text>
      {
        // Token: 0x06003C7C RID: 15484 RVA: 0x004672D8 File Offset: 0x004672D8
        public Text(Point At)
        {
          this.At = At;
          base..ctor();
        }

        // Token: 0x17000A22 RID: 2594
        // (get) Token: 0x06003C7D RID: 15485 RVA: 0x004672E8 File Offset: 0x004672E8
        [CompilerGenerated]
        protected override Type EqualityContract
        {
          [CompilerGenerated]
          get
          {
            return typeof(ClickyInkSketchModel.Shape.Text);
          }
        }

        // Token: 0x17000A23 RID: 2595
        // (get) Token: 0x06003C7E RID: 15486 RVA: 0x004672F4 File Offset: 0x004672F4
        // (set) Token: 0x06003C7F RID: 15487 RVA: 0x004672FC File Offset: 0x004672FC
        public Point At { get; set; }

        // Token: 0x06003C80 RID: 15488 RVA: 0x00467308 File Offset: 0x00467308
        [CompilerGenerated]
        public override string ToString()
        {
          StringBuilder stringBuilder = new StringBuilder();
          stringBuilder.Append("Text");
          stringBuilder.Append(" { ");
          if (this.PrintMembers(stringBuilder))
          {
            stringBuilder.Append(' ');
          }
          stringBuilder.Append('}');
          return stringBuilder.ToString();
        }

        // Token: 0x06003C81 RID: 15489 RVA: 0x00467354 File Offset: 0x00467354
        [CompilerGenerated]
        protected override bool PrintMembers(StringBuilder builder)
        {
          if (base.PrintMembers(builder))
          {
            builder.Append(", ");
          }
          builder.Append("At = ");
          builder.Append(this.At.ToString());
          return true;
        }

        // Token: 0x06003C82 RID: 15490 RVA: 0x004673A0 File Offset: 0x004673A0
        [NullableContext(2)]
        [CompilerGenerated]
        public static bool operator !=(ClickyInkSketchModel.Shape.Text left, ClickyInkSketchModel.Shape.Text right)
        {
          return !(left == right);
        }

        // Token: 0x06003C83 RID: 15491 RVA: 0x004673AC File Offset: 0x004673AC
        [NullableContext(2)]
        [CompilerGenerated]
        public static bool operator ==(ClickyInkSketchModel.Shape.Text left, ClickyInkSketchModel.Shape.Text right)
        {
          return left == right || (left != null && left.Equals(right));
        }

        // Token: 0x06003C84 RID: 15492 RVA: 0x004673C0 File Offset: 0x004673C0
        [CompilerGenerated]
        public override int GetHashCode()
        {
          return base.GetHashCode() * -1521134295 + EqualityComparer<Point>.Default.GetHashCode(this.<At>k__BackingField);
        }

        // Token: 0x06003C85 RID: 15493 RVA: 0x004673E0 File Offset: 0x004673E0
        [NullableContext(2)]
        [CompilerGenerated]
        public override bool Equals(object obj)
        {
          return this.Equals(obj as ClickyInkSketchModel.Shape.Text);
        }

        // Token: 0x06003C86 RID: 15494 RVA: 0x004673F0 File Offset: 0x004673F0
        [NullableContext(2)]
        [CompilerGenerated]
        public sealed override bool Equals(ClickyInkSketchModel.Shape other)
        {
          return this.Equals(other);
        }

        // Token: 0x06003C87 RID: 15495 RVA: 0x004673FC File Offset: 0x004673FC
        [NullableContext(2)]
        [CompilerGenerated]
        public bool Equals(ClickyInkSketchModel.Shape.Text other)
        {
          return this == other || (base.Equals(other) && EqualityComparer<Point>.Default.Equals(this.<At>k__BackingField, other.<At>k__BackingField));
        }

        // Token: 0x06003C89 RID: 15497 RVA: 0x00467430 File Offset: 0x00467430
        [CompilerGenerated]
        private Text(ClickyInkSketchModel.Shape.Text original)
          : base(original)
        {
          this.At = original.<At>k__BackingField;
        }

        // Token: 0x06003C8A RID: 15498 RVA: 0x00467448 File Offset: 0x00467448
        [CompilerGenerated]
        public void Deconstruct(out Point At)
        {
          At = this.At;
        }
      }

      // Token: 0x02000709 RID: 1801
      [Nullable(0)]
      public sealed class Icon : ClickyInkSketchModel.Shape, IEquatable<ClickyInkSketchModel.Shape.Icon>
      {
        // Token: 0x06003C8B RID: 15499 RVA: 0x00467458 File Offset: 0x00467458
        public Icon(Point At, string Name, double Size)
        {
          this.At = At;
          this.Name = Name;
          this.Size = Size;
          base..ctor();
        }

        // Token: 0x17000A24 RID: 2596
        // (get) Token: 0x06003C8C RID: 15500 RVA: 0x00467478 File Offset: 0x00467478
        [CompilerGenerated]
        protected override Type EqualityContract
        {
          [CompilerGenerated]
          get
          {
            return typeof(ClickyInkSketchModel.Shape.Icon);
          }
        }

        // Token: 0x17000A25 RID: 2597
        // (get) Token: 0x06003C8D RID: 15501 RVA: 0x00467484 File Offset: 0x00467484
        // (set) Token: 0x06003C8E RID: 15502 RVA: 0x0046748C File Offset: 0x0046748C
        public Point At { get; set; }

        // Token: 0x17000A26 RID: 2598
        // (get) Token: 0x06003C8F RID: 15503 RVA: 0x00467498 File Offset: 0x00467498
        // (set) Token: 0x06003C90 RID: 15504 RVA: 0x004674A0 File Offset: 0x004674A0
        public string Name { get; set; }

        // Token: 0x17000A27 RID: 2599
        // (get) Token: 0x06003C91 RID: 15505 RVA: 0x004674AC File Offset: 0x004674AC
        // (set) Token: 0x06003C92 RID: 15506 RVA: 0x004674B4 File Offset: 0x004674B4
        public double Size { get; set; }

        // Token: 0x06003C93 RID: 15507 RVA: 0x004674C0 File Offset: 0x004674C0
        [CompilerGenerated]
        public override string ToString()
        {
          StringBuilder stringBuilder = new StringBuilder();
          stringBuilder.Append("Icon");
          stringBuilder.Append(" { ");
          if (this.PrintMembers(stringBuilder))
          {
            stringBuilder.Append(' ');
          }
          stringBuilder.Append('}');
          return stringBuilder.ToString();
        }

        // Token: 0x06003C94 RID: 15508 RVA: 0x0046750C File Offset: 0x0046750C
        [CompilerGenerated]
        protected override bool PrintMembers(StringBuilder builder)
        {
          if (base.PrintMembers(builder))
          {
            builder.Append(", ");
          }
          builder.Append("At = ");
          builder.Append(this.At.ToString());
          builder.Append(", Name = ");
          builder.Append(this.Name);
          builder.Append(", Size = ");
          builder.Append(this.Size.ToString());
          return true;
        }

        // Token: 0x06003C95 RID: 15509 RVA: 0x00467598 File Offset: 0x00467598
        [NullableContext(2)]
        [CompilerGenerated]
        public static bool operator !=(ClickyInkSketchModel.Shape.Icon left, ClickyInkSketchModel.Shape.Icon right)
        {
          return !(left == right);
        }

        // Token: 0x06003C96 RID: 15510 RVA: 0x004675A4 File Offset: 0x004675A4
        [NullableContext(2)]
        [CompilerGenerated]
        public static bool operator ==(ClickyInkSketchModel.Shape.Icon left, ClickyInkSketchModel.Shape.Icon right)
        {
          return left == right || (left != null && left.Equals(right));
        }

        // Token: 0x06003C97 RID: 15511 RVA: 0x004675B8 File Offset: 0x004675B8
        [CompilerGenerated]
        public override int GetHashCode()
        {
          return ((base.GetHashCode() * -1521134295 + EqualityComparer<Point>.Default.GetHashCode(this.<At>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Name>k__BackingField)) * -1521134295 + EqualityComparer<double>.Default.GetHashCode(this.<Size>k__BackingField);
        }

        // Token: 0x06003C98 RID: 15512 RVA: 0x00467610 File Offset: 0x00467610
        [NullableContext(2)]
        [CompilerGenerated]
        public override bool Equals(object obj)
        {
          return this.Equals(obj as ClickyInkSketchModel.Shape.Icon);
        }

        // Token: 0x06003C99 RID: 15513 RVA: 0x00467620 File Offset: 0x00467620
        [NullableContext(2)]
        [CompilerGenerated]
        public sealed override bool Equals(ClickyInkSketchModel.Shape other)
        {
          return this.Equals(other);
        }

        // Token: 0x06003C9A RID: 15514 RVA: 0x0046762C File Offset: 0x0046762C
        [NullableContext(2)]
        [CompilerGenerated]
        public bool Equals(ClickyInkSketchModel.Shape.Icon other)
        {
          return this == other || (base.Equals(other) && EqualityComparer<Point>.Default.Equals(this.<At>k__BackingField, other.<At>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<Name>k__BackingField, other.<Name>k__BackingField) && EqualityComparer<double>.Default.Equals(this.<Size>k__BackingField, other.<Size>k__BackingField));
        }

        // Token: 0x06003C9C RID: 15516 RVA: 0x00467698 File Offset: 0x00467698
        [CompilerGenerated]
        private Icon(ClickyInkSketchModel.Shape.Icon original)
          : base(original)
        {
          this.At = original.<At>k__BackingField;
          this.Name = original.<Name>k__BackingField;
          this.Size = original.<Size>k__BackingField;
        }

        // Token: 0x06003C9D RID: 15517 RVA: 0x004676C8 File Offset: 0x004676C8
        [CompilerGenerated]
        public void Deconstruct(out Point At, out string Name, out double Size)
        {
          At = this.At;
          Name = this.Name;
          Size = this.Size;
        }
      }

      // Token: 0x0200070A RID: 1802
      [Nullable(0)]
      public sealed class Dot : ClickyInkSketchModel.Shape, IEquatable<ClickyInkSketchModel.Shape.Dot>
      {
        // Token: 0x06003C9E RID: 15518 RVA: 0x004676E8 File Offset: 0x004676E8
        public Dot(Point At)
        {
          this.At = At;
          base..ctor();
        }

        // Token: 0x17000A28 RID: 2600
        // (get) Token: 0x06003C9F RID: 15519 RVA: 0x004676F8 File Offset: 0x004676F8
        [CompilerGenerated]
        protected override Type EqualityContract
        {
          [CompilerGenerated]
          get
          {
            return typeof(ClickyInkSketchModel.Shape.Dot);
          }
        }

        // Token: 0x17000A29 RID: 2601
        // (get) Token: 0x06003CA0 RID: 15520 RVA: 0x00467704 File Offset: 0x00467704
        // (set) Token: 0x06003CA1 RID: 15521 RVA: 0x0046770C File Offset: 0x0046770C
        public Point At { get; set; }

        // Token: 0x06003CA2 RID: 15522 RVA: 0x00467718 File Offset: 0x00467718
        [CompilerGenerated]
        public override string ToString()
        {
          StringBuilder stringBuilder = new StringBuilder();
          stringBuilder.Append("Dot");
          stringBuilder.Append(" { ");
          if (this.PrintMembers(stringBuilder))
          {
            stringBuilder.Append(' ');
          }
          stringBuilder.Append('}');
          return stringBuilder.ToString();
        }

        // Token: 0x06003CA3 RID: 15523 RVA: 0x00467764 File Offset: 0x00467764
        [CompilerGenerated]
        protected override bool PrintMembers(StringBuilder builder)
        {
          if (base.PrintMembers(builder))
          {
            builder.Append(", ");
          }
          builder.Append("At = ");
          builder.Append(this.At.ToString());
          return true;
        }

        // Token: 0x06003CA4 RID: 15524 RVA: 0x004677B0 File Offset: 0x004677B0
        [NullableContext(2)]
        [CompilerGenerated]
        public static bool operator !=(ClickyInkSketchModel.Shape.Dot left, ClickyInkSketchModel.Shape.Dot right)
        {
          return !(left == right);
        }

        // Token: 0x06003CA5 RID: 15525 RVA: 0x004677BC File Offset: 0x004677BC
        [NullableContext(2)]
        [CompilerGenerated]
        public static bool operator ==(ClickyInkSketchModel.Shape.Dot left, ClickyInkSketchModel.Shape.Dot right)
        {
          return left == right || (left != null && left.Equals(right));
        }

        // Token: 0x06003CA6 RID: 15526 RVA: 0x004677D0 File Offset: 0x004677D0
        [CompilerGenerated]
        public override int GetHashCode()
        {
          return base.GetHashCode() * -1521134295 + EqualityComparer<Point>.Default.GetHashCode(this.<At>k__BackingField);
        }

        // Token: 0x06003CA7 RID: 15527 RVA: 0x004677F0 File Offset: 0x004677F0
        [NullableContext(2)]
        [CompilerGenerated]
        public override bool Equals(object obj)
        {
          return this.Equals(obj as ClickyInkSketchModel.Shape.Dot);
        }

        // Token: 0x06003CA8 RID: 15528 RVA: 0x00467800 File Offset: 0x00467800
        [NullableContext(2)]
        [CompilerGenerated]
        public sealed override bool Equals(ClickyInkSketchModel.Shape other)
        {
          return this.Equals(other);
        }

        // Token: 0x06003CA9 RID: 15529 RVA: 0x0046780C File Offset: 0x0046780C
        [NullableContext(2)]
        [CompilerGenerated]
        public bool Equals(ClickyInkSketchModel.Shape.Dot other)
        {
          return this == other || (base.Equals(other) && EqualityComparer<Point>.Default.Equals(this.<At>k__BackingField, other.<At>k__BackingField));
        }

        // Token: 0x06003CAB RID: 15531 RVA: 0x00467840 File Offset: 0x00467840
        [CompilerGenerated]
        private Dot(ClickyInkSketchModel.Shape.Dot original)
          : base(original)
        {
          this.At = original.<At>k__BackingField;
        }

        // Token: 0x06003CAC RID: 15532 RVA: 0x00467858 File Offset: 0x00467858
        [CompilerGenerated]
        public void Deconstruct(out Point At)
        {
          At = this.At;
        }
      }

      // Token: 0x0200070B RID: 1803
      [Nullable(0)]
      public sealed class Badge : ClickyInkSketchModel.Shape, IEquatable<ClickyInkSketchModel.Shape.Badge>
      {
        // Token: 0x06003CAD RID: 15533 RVA: 0x00467868 File Offset: 0x00467868
        public Badge(Point At)
        {
          this.At = At;
          base..ctor();
        }

        // Token: 0x17000A2A RID: 2602
        // (get) Token: 0x06003CAE RID: 15534 RVA: 0x00467878 File Offset: 0x00467878
        [CompilerGenerated]
        protected override Type EqualityContract
        {
          [CompilerGenerated]
          get
          {
            return typeof(ClickyInkSketchModel.Shape.Badge);
          }
        }

        // Token: 0x17000A2B RID: 2603
        // (get) Token: 0x06003CAF RID: 15535 RVA: 0x00467884 File Offset: 0x00467884
        // (set) Token: 0x06003CB0 RID: 15536 RVA: 0x0046788C File Offset: 0x0046788C
        public Point At { get; set; }

        // Token: 0x06003CB1 RID: 15537 RVA: 0x00467898 File Offset: 0x00467898
        [CompilerGenerated]
        public override string ToString()
        {
          StringBuilder stringBuilder = new StringBuilder();
          stringBuilder.Append("Badge");
          stringBuilder.Append(" { ");
          if (this.PrintMembers(stringBuilder))
          {
            stringBuilder.Append(' ');
          }
          stringBuilder.Append('}');
          return stringBuilder.ToString();
        }

        // Token: 0x06003CB2 RID: 15538 RVA: 0x004678E4 File Offset: 0x004678E4
        [CompilerGenerated]
        protected override bool PrintMembers(StringBuilder builder)
        {
          if (base.PrintMembers(builder))
          {
            builder.Append(", ");
          }
          builder.Append("At = ");
          builder.Append(this.At.ToString());
          return true;
        }

        // Token: 0x06003CB3 RID: 15539 RVA: 0x00467930 File Offset: 0x00467930
        [NullableContext(2)]
        [CompilerGenerated]
        public static bool operator !=(ClickyInkSketchModel.Shape.Badge left, ClickyInkSketchModel.Shape.Badge right)
        {
          return !(left == right);
        }

        // Token: 0x06003CB4 RID: 15540 RVA: 0x0046793C File Offset: 0x0046793C
        [NullableContext(2)]
        [CompilerGenerated]
        public static bool operator ==(ClickyInkSketchModel.Shape.Badge left, ClickyInkSketchModel.Shape.Badge right)
        {
          return left == right || (left != null && left.Equals(right));
        }

        // Token: 0x06003CB5 RID: 15541 RVA: 0x00467950 File Offset: 0x00467950
        [CompilerGenerated]
        public override int GetHashCode()
        {
          return base.GetHashCode() * -1521134295 + EqualityComparer<Point>.Default.GetHashCode(this.<At>k__BackingField);
        }

        // Token: 0x06003CB6 RID: 15542 RVA: 0x00467970 File Offset: 0x00467970
        [NullableContext(2)]
        [CompilerGenerated]
        public override bool Equals(object obj)
        {
          return this.Equals(obj as ClickyInkSketchModel.Shape.Badge);
        }

        // Token: 0x06003CB7 RID: 15543 RVA: 0x00467980 File Offset: 0x00467980
        [NullableContext(2)]
        [CompilerGenerated]
        public sealed override bool Equals(ClickyInkSketchModel.Shape other)
        {
          return this.Equals(other);
        }

        // Token: 0x06003CB8 RID: 15544 RVA: 0x0046798C File Offset: 0x0046798C
        [NullableContext(2)]
        [CompilerGenerated]
        public bool Equals(ClickyInkSketchModel.Shape.Badge other)
        {
          return this == other || (base.Equals(other) && EqualityComparer<Point>.Default.Equals(this.<At>k__BackingField, other.<At>k__BackingField));
        }

        // Token: 0x06003CBA RID: 15546 RVA: 0x004679C0 File Offset: 0x004679C0
        [CompilerGenerated]
        private Badge(ClickyInkSketchModel.Shape.Badge original)
          : base(original)
        {
          this.At = original.<At>k__BackingField;
        }

        // Token: 0x06003CBB RID: 15547 RVA: 0x004679D8 File Offset: 0x004679D8
        [CompilerGenerated]
        public void Deconstruct(out Point At)
        {
          At = this.At;
        }
      }
    }

    // Token: 0x0200055D RID: 1373
    [NullableContext(0)]
    public enum Size
    {
      // Token: 0x0400204D RID: 8269
      Small,
      // Token: 0x0400204E RID: 8270
      Normal,
      // Token: 0x0400204F RID: 8271
      Big
    }

    // Token: 0x0200055E RID: 1374
    [NullableContext(2)]
    [Nullable(0)]
    public sealed class Piece : IEquatable<ClickyInkSketchModel.Piece>
    {
      // Token: 0x1700094F RID: 2383
      // (get) Token: 0x06003481 RID: 13441 RVA: 0x0043F598 File Offset: 0x0043F598
      [Nullable(1)]
      [CompilerGenerated]
      private Type EqualityContract
      {
        [NullableContext(1)]
        [CompilerGenerated]
        get
        {
          return typeof(ClickyInkSketchModel.Piece);
        }
      }

      // Token: 0x17000950 RID: 2384
      // (get) Token: 0x06003482 RID: 13442 RVA: 0x0043F5A4 File Offset: 0x0043F5A4
      // (set) Token: 0x06003483 RID: 13443 RVA: 0x0043F5AC File Offset: 0x0043F5AC
      [Nullable(1)]
      public ClickyInkSketchModel.Shape Shape
      {
        [NullableContext(1)]
        get;
        [NullableContext(1)]
        set;
      }

      // Token: 0x17000951 RID: 2385
      // (get) Token: 0x06003484 RID: 13444 RVA: 0x0043F5B8 File Offset: 0x0043F5B8
      // (set) Token: 0x06003485 RID: 13445 RVA: 0x0043F5C0 File Offset: 0x0043F5C0
      public string Label { get; set; }

      // Token: 0x17000952 RID: 2386
      // (get) Token: 0x06003486 RID: 13446 RVA: 0x0043F5CC File Offset: 0x0043F5CC
      // (set) Token: 0x06003487 RID: 13447 RVA: 0x0043F5D4 File Offset: 0x0043F5D4
      public string Color { get; set; }

      // Token: 0x17000953 RID: 2387
      // (get) Token: 0x06003488 RID: 13448 RVA: 0x0043F5E0 File Offset: 0x0043F5E0
      // (set) Token: 0x06003489 RID: 13449 RVA: 0x0043F5E8 File Offset: 0x0043F5E8
      public bool IsFilled { get; set; }

      // Token: 0x17000954 RID: 2388
      // (get) Token: 0x0600348A RID: 13450 RVA: 0x0043F5F4 File Offset: 0x0043F5F4
      // (set) Token: 0x0600348B RID: 13451 RVA: 0x0043F5FC File Offset: 0x0043F5FC
      public bool IsDashed { get; set; }

      // Token: 0x17000955 RID: 2389
      // (get) Token: 0x0600348C RID: 13452 RVA: 0x0043F608 File Offset: 0x0043F608
      // (set) Token: 0x0600348D RID: 13453 RVA: 0x0043F610 File Offset: 0x0043F610
      public bool IsCurved { get; set; }

      // Token: 0x17000956 RID: 2390
      // (get) Token: 0x0600348E RID: 13454 RVA: 0x0043F61C File Offset: 0x0043F61C
      // (set) Token: 0x0600348F RID: 13455 RVA: 0x0043F624 File Offset: 0x0043F624
      public ClickyInkSketchModel.Size Size { get; set; }

      // Token: 0x06003490 RID: 13456 RVA: 0x0043F630 File Offset: 0x0043F630
      [NullableContext(1)]
      [CompilerGenerated]
      public override string ToString()
      {
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.Append("Piece");
        stringBuilder.Append(" { ");
        if (this.PrintMembers(stringBuilder))
        {
          stringBuilder.Append(' ');
        }
        stringBuilder.Append('}');
        return stringBuilder.ToString();
      }

      // Token: 0x06003491 RID: 13457 RVA: 0x0043F67C File Offset: 0x0043F67C
      [NullableContext(1)]
      [CompilerGenerated]
      private bool PrintMembers(StringBuilder builder)
      {
        RuntimeHelpers.EnsureSufficientExecutionStack();
        builder.Append("Shape = ");
        builder.Append(this.Shape);
        builder.Append(", Label = ");
        builder.Append(this.Label);
        builder.Append(", Color = ");
        builder.Append(this.Color);
        builder.Append(", IsFilled = ");
        builder.Append(this.IsFilled.ToString());
        builder.Append(", IsDashed = ");
        builder.Append(this.IsDashed.ToString());
        builder.Append(", IsCurved = ");
        builder.Append(this.IsCurved.ToString());
        builder.Append(", Size = ");
        builder.Append(this.Size.ToString());
        return true;
      }

      // Token: 0x06003492 RID: 13458 RVA: 0x0043F778 File Offset: 0x0043F778
      [CompilerGenerated]
      public static bool operator !=(ClickyInkSketchModel.Piece left, ClickyInkSketchModel.Piece right)
      {
        return !(left == right);
      }

      // Token: 0x06003493 RID: 13459 RVA: 0x0043F784 File Offset: 0x0043F784
      [CompilerGenerated]
      public static bool operator ==(ClickyInkSketchModel.Piece left, ClickyInkSketchModel.Piece right)
      {
        return left == right || (left != null && left.Equals(right));
      }

      // Token: 0x06003494 RID: 13460 RVA: 0x0043F798 File Offset: 0x0043F798
      [CompilerGenerated]
      public override int GetHashCode()
      {
        return ((((((EqualityComparer<Type>.Default.GetHashCode(this.EqualityContract) * -1521134295 + EqualityComparer<ClickyInkSketchModel.Shape>.Default.GetHashCode(this.<Shape>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Label>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Color>k__BackingField)) * -1521134295 + EqualityComparer<bool>.Default.GetHashCode(this.<IsFilled>k__BackingField)) * -1521134295 + EqualityComparer<bool>.Default.GetHashCode(this.<IsDashed>k__BackingField)) * -1521134295 + EqualityComparer<bool>.Default.GetHashCode(this.<IsCurved>k__BackingField)) * -1521134295 + EqualityComparer<ClickyInkSketchModel.Size>.Default.GetHashCode(this.<Size>k__BackingField);
      }

      // Token: 0x06003495 RID: 13461 RVA: 0x0043F858 File Offset: 0x0043F858
      [CompilerGenerated]
      public override bool Equals(object obj)
      {
        return this.Equals(obj as ClickyInkSketchModel.Piece);
      }

      // Token: 0x06003496 RID: 13462 RVA: 0x0043F868 File Offset: 0x0043F868
      [CompilerGenerated]
      public bool Equals(ClickyInkSketchModel.Piece other)
      {
        return this == other || (other != null && this.EqualityContract == other.EqualityContract && EqualityComparer<ClickyInkSketchModel.Shape>.Default.Equals(this.<Shape>k__BackingField, other.<Shape>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<Label>k__BackingField, other.<Label>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<Color>k__BackingField, other.<Color>k__BackingField) && EqualityComparer<bool>.Default.Equals(this.<IsFilled>k__BackingField, other.<IsFilled>k__BackingField) && EqualityComparer<bool>.Default.Equals(this.<IsDashed>k__BackingField, other.<IsDashed>k__BackingField) && EqualityComparer<bool>.Default.Equals(this.<IsCurved>k__BackingField, other.<IsCurved>k__BackingField) && EqualityComparer<ClickyInkSketchModel.Size>.Default.Equals(this.<Size>k__BackingField, other.<Size>k__BackingField));
      }

      // Token: 0x06003498 RID: 13464 RVA: 0x0043F950 File Offset: 0x0043F950
      [CompilerGenerated]
      private Piece([Nullable(1)] ClickyInkSketchModel.Piece original)
      {
        this.Shape = original.<Shape>k__BackingField;
        this.Label = original.<Label>k__BackingField;
        this.Color = original.<Color>k__BackingField;
        this.IsFilled = original.<IsFilled>k__BackingField;
        this.IsDashed = original.<IsDashed>k__BackingField;
        this.IsCurved = original.<IsCurved>k__BackingField;
        this.Size = original.<Size>k__BackingField;
      }

      // Token: 0x06003499 RID: 13465 RVA: 0x0043F9B8 File Offset: 0x0043F9B8
      public Piece()
      {
        this.Shape = new ClickyInkSketchModel.Shape.Dot(new Point(0f, 0f));
        this.Size = ClickyInkSketchModel.Size.Normal;
        base..ctor();
      }
    }
  }
}
