using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using HeyClicky.App.UI;
using HeyClicky.Core.Ink;
using Microsoft.UI.Text;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media;
using Windows.Foundation;
using Windows.UI;
using Windows.UI.Text;

namespace HeyClicky.App.Ink
{
  // Token: 0x02000218 RID: 536
  [NullableContext(1)]
  [Nullable(0)]
  public static class ClickyInkDashboardView
  {
    // Token: 0x1700047D RID: 1149
    // (get) Token: 0x0600195A RID: 6490 RVA: 0x005D16BC File Offset: 0x005D16BC
    public static double CardWidth
    {
      get
      {
        return 864.0;
      }
    }

    // Token: 0x0600195B RID: 6491 RVA: 0x005D16C8 File Offset: 0x005D16C8
    [NullableContext(2)]
    [return: Nullable(1)]
    public static FrameworkElement Make([Nullable(1)] ClickyInkLiveNote note, double clock, ClickyHomeSpreadPaper? paper = null, ClickyInkActions actions = null, double? width = null, int? runNumber = null, string dateLine = null, ClickyInkDashboardView.Job job = null, ClickyInkTiles.Look tilesLook = ClickyInkTiles.Look.Ruled)
    {
      FrameworkElement frameworkElement = new ClickyInkDashboardView.Builder(note, clock, paper ?? ClickyHomeSpreadPaper.Note, actions ?? new ClickyInkActions(), width ?? ClickyInkDashboardView.CardWidth, runNumber, dateLine, job, tilesLook).Body();
      ClickyInkFormKit.WithoutPenRenderer(frameworkElement);
      return frameworkElement;
    }

    // Token: 0x0600195C RID: 6492 RVA: 0x005D172C File Offset: 0x005D172C
    public static List<ClickyInkGroup> SpreadingLongList(List<ClickyInkGroup> groups)
    {
      return ClickyInkDashboardSplit.SpreadingLongList(groups);
    }

    // Token: 0x04000C57 RID: 3159
    public const double Margin = 36.0;

    // Token: 0x04000C58 RID: 3160
    public const double StruckWordsFoldAfter = 2.5;

    // Token: 0x020007BD RID: 1981
    [NullableContext(2)]
    [Nullable(0)]
    public sealed class Job : IEquatable<ClickyInkDashboardView.Job>
    {
      // Token: 0x060036D4 RID: 14036 RVA: 0x0067ED7C File Offset: 0x0067ED7C
      public Job([Nullable(1)] string CapsLine, ClickyThingStatus Status, string StampWord = null, string LiveLine = null)
      {
        this.CapsLine = CapsLine;
        this.Status = Status;
        this.StampWord = StampWord;
        this.LiveLine = LiveLine;
        base..ctor();
      }

      // Token: 0x170007E1 RID: 2017
      // (get) Token: 0x060036D5 RID: 14037 RVA: 0x0067EDA4 File Offset: 0x0067EDA4
      [Nullable(1)]
      [CompilerGenerated]
      private Type EqualityContract
      {
        [NullableContext(1)]
        [CompilerGenerated]
        get
        {
          return typeof(ClickyInkDashboardView.Job);
        }
      }

      // Token: 0x170007E2 RID: 2018
      // (get) Token: 0x060036D6 RID: 14038 RVA: 0x0067EDB0 File Offset: 0x0067EDB0
      // (set) Token: 0x060036D7 RID: 14039 RVA: 0x0067EDB8 File Offset: 0x0067EDB8
      [Nullable(1)]
      public string CapsLine
      {
        [NullableContext(1)]
        get;
        [NullableContext(1)]
        set;
      }

      // Token: 0x170007E3 RID: 2019
      // (get) Token: 0x060036D8 RID: 14040 RVA: 0x0067EDC4 File Offset: 0x0067EDC4
      // (set) Token: 0x060036D9 RID: 14041 RVA: 0x0067EDCC File Offset: 0x0067EDCC
      public ClickyThingStatus Status { get; set; }

      // Token: 0x170007E4 RID: 2020
      // (get) Token: 0x060036DA RID: 14042 RVA: 0x0067EDD8 File Offset: 0x0067EDD8
      // (set) Token: 0x060036DB RID: 14043 RVA: 0x0067EDE0 File Offset: 0x0067EDE0
      public string StampWord { get; set; }

      // Token: 0x170007E5 RID: 2021
      // (get) Token: 0x060036DC RID: 14044 RVA: 0x0067EDEC File Offset: 0x0067EDEC
      // (set) Token: 0x060036DD RID: 14045 RVA: 0x0067EDF4 File Offset: 0x0067EDF4
      public string LiveLine { get; set; }

      // Token: 0x060036DE RID: 14046 RVA: 0x0067EE00 File Offset: 0x0067EE00
      [NullableContext(1)]
      [CompilerGenerated]
      public override string ToString()
      {
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.Append("Job");
        stringBuilder.Append(" { ");
        if (this.PrintMembers(stringBuilder))
        {
          stringBuilder.Append(' ');
        }
        stringBuilder.Append('}');
        return stringBuilder.ToString();
      }

      // Token: 0x060036DF RID: 14047 RVA: 0x0067EE4C File Offset: 0x0067EE4C
      [NullableContext(1)]
      [CompilerGenerated]
      private bool PrintMembers(StringBuilder builder)
      {
        RuntimeHelpers.EnsureSufficientExecutionStack();
        builder.Append("CapsLine = ");
        builder.Append(this.CapsLine);
        builder.Append(", Status = ");
        builder.Append(this.Status.ToString());
        builder.Append(", StampWord = ");
        builder.Append(this.StampWord);
        builder.Append(", LiveLine = ");
        builder.Append(this.LiveLine);
        return true;
      }

      // Token: 0x060036E0 RID: 14048 RVA: 0x0067EED4 File Offset: 0x0067EED4
      [CompilerGenerated]
      public static bool operator !=(ClickyInkDashboardView.Job left, ClickyInkDashboardView.Job right)
      {
        return !(left == right);
      }

      // Token: 0x060036E1 RID: 14049 RVA: 0x0067EEE0 File Offset: 0x0067EEE0
      [CompilerGenerated]
      public static bool operator ==(ClickyInkDashboardView.Job left, ClickyInkDashboardView.Job right)
      {
        return left == right || (left != null && left.Equals(right));
      }

      // Token: 0x060036E2 RID: 14050 RVA: 0x0067EEF4 File Offset: 0x0067EEF4
      [CompilerGenerated]
      public override int GetHashCode()
      {
        return (((EqualityComparer<Type>.Default.GetHashCode(this.EqualityContract) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<CapsLine>k__BackingField)) * -1521134295 + EqualityComparer<ClickyThingStatus>.Default.GetHashCode(this.<Status>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<StampWord>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<LiveLine>k__BackingField);
      }

      // Token: 0x060036E3 RID: 14051 RVA: 0x0067EF70 File Offset: 0x0067EF70
      [CompilerGenerated]
      public override bool Equals(object obj)
      {
        return this.Equals(obj as ClickyInkDashboardView.Job);
      }

      // Token: 0x060036E4 RID: 14052 RVA: 0x0067EF80 File Offset: 0x0067EF80
      [CompilerGenerated]
      public bool Equals(ClickyInkDashboardView.Job other)
      {
        return this == other || (other != null && this.EqualityContract == other.EqualityContract && EqualityComparer<string>.Default.Equals(this.<CapsLine>k__BackingField, other.<CapsLine>k__BackingField) && EqualityComparer<ClickyThingStatus>.Default.Equals(this.<Status>k__BackingField, other.<Status>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<StampWord>k__BackingField, other.<StampWord>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<LiveLine>k__BackingField, other.<LiveLine>k__BackingField));
      }

      // Token: 0x060036E6 RID: 14054 RVA: 0x0067F014 File Offset: 0x0067F014
      [CompilerGenerated]
      private Job([Nullable(1)] ClickyInkDashboardView.Job original)
      {
        this.CapsLine = original.<CapsLine>k__BackingField;
        this.Status = original.<Status>k__BackingField;
        this.StampWord = original.<StampWord>k__BackingField;
        this.LiveLine = original.<LiveLine>k__BackingField;
      }

      // Token: 0x060036E7 RID: 14055 RVA: 0x0067F04C File Offset: 0x0067F04C
      [CompilerGenerated]
      public void Deconstruct([Nullable(1)] out string CapsLine, out ClickyThingStatus Status, out string StampWord, out string LiveLine)
      {
        CapsLine = this.CapsLine;
        Status = this.Status;
        StampWord = this.StampWord;
        LiveLine = this.LiveLine;
      }
    }

    // Token: 0x020007BE RID: 1982
    [Nullable(0)]
    private sealed class Builder
    {
      // Token: 0x060036E8 RID: 14056 RVA: 0x0067F070 File Offset: 0x0067F070
      public Builder(ClickyInkLiveNote rawNote, double clock, ClickyHomeSpreadPaper paper, ClickyInkActions actions, double width, int? runNumber, [Nullable(2)] string dateLine, [Nullable(2)] ClickyInkDashboardView.Job job, ClickyInkTiles.Look tilesLook)
      {
      }

      // Token: 0x170007E6 RID: 2022
      // (get) Token: 0x060036E9 RID: 14057 RVA: 0x0067F0D8 File Offset: 0x0067F0D8
      private ClickyInkStyle Style
      {
        get
        {
          ClickyInkStyle clickyInkStyle;
          if ((clickyInkStyle = this.style) == null)
          {
            clickyInkStyle = (this.style = new ClickyInkStyle(this.<paper>P.Ink, this.<paper>P.Quiet, new Color?(this.<paper>P.Deep), this.note.LatestEdit(this.<clock>P)));
          }
          return clickyInkStyle;
        }
      }

      // Token: 0x170007E7 RID: 2023
      // (get) Token: 0x060036EA RID: 14058 RVA: 0x0067F134 File Offset: 0x0067F134
      private double InnerWidth
      {
        get
        {
          return this.<width>P - 72.0;
        }
      }

      // Token: 0x170007E8 RID: 2024
      // (get) Token: 0x060036EB RID: 14059 RVA: 0x0067F148 File Offset: 0x0067F148
      private bool IsTwoColumns
      {
        get
        {
          return this.<width>P >= 760.0;
        }
      }

      // Token: 0x060036EC RID: 14060 RVA: 0x0067F160 File Offset: 0x0067F160
      public FrameworkElement Body()
      {
        ClickyInkDashboardBoard clickyInkDashboardBoard = new ClickyInkDashboardBoard(this.note, this.<clock>P, (this.<job>P == null) ? "This run" : "In short");
        List<ClickyInkDashboardSplit.Placed> list = (this.IsTwoColumns ? ClickyInkDashboardRows.For(this.note, this.<paper>P).Placing(clickyInkDashboardBoard.Sections) : new List<ClickyInkDashboardSplit.Placed>());
        StackPanel stackPanel = new StackPanel
        {
          Width = this.InnerWidth,
          HorizontalAlignment = HorizontalAlignment.Left
        };
        stackPanel.Children.Add(this.Masthead(clickyInkDashboardBoard).Padding(0.0, 0.0, 0.0, 12.0));
        FrameworkElement frameworkElement = this.Headline(clickyInkDashboardBoard);
        if (frameworkElement != null)
        {
          stackPanel.Children.Add(frameworkElement);
        }
        if (clickyInkDashboardBoard.Strip.Count > 0)
        {
          stackPanel.Children.Add(ClickyInkDashboardFormNeeds.Tiles(clickyInkDashboardBoard.Strip, this.<clock>P, this.Style, this.InnerWidth, this.<tilesLook>P).Padding(0.0, 14.0, 0.0, 8.0));
        }
        if (this.IsTwoColumns)
        {
          for (int i = 0; i < list.Count; i++)
          {
            ClickyInkDashboardSplit.Placed placed = list[i];
            ClickyInkDashboardSplit.Placed.Columns columns = placed as ClickyInkDashboardSplit.Placed.Columns;
            if (columns == null)
            {
              ClickyInkDashboardSplit.Placed.Wide wide = placed as ClickyInkDashboardSplit.Placed.Wide;
              if (wide != null)
              {
                stackPanel.Children.Add(this.WideSection(wide.Section).Padding(0.0, 16.0, 0.0, 0.0));
              }
            }
            else if (columns.Left.Count + columns.Right.Count == 1 && !columns.HasMove)
            {
              stackPanel.Children.Add(this.WideSection(columns.Left.Concat<ClickyInkDashboardBoard.Section>(columns.Right).First<ClickyInkDashboardBoard.Section>()).Padding(0.0, 16.0, 0.0, 0.0));
            }
            else
            {
              ClickyInkDashboardSplit.Placed.Columns columns2 = columns;
              ClickyInkDashboardBoard.Section section = (columns2.HasMove ? clickyInkDashboardBoard.Move : null);
              ClickyInkDashboardBoard.Section section2;
              if (section != null)
              {
                (section2 = section.<Clone>$()).Groups = section.Groups.Take<ClickyInkGroup>(columns2.MoveOnRight).ToList<ClickyInkGroup>();
              }
              else
              {
                section2 = null;
              }
              ClickyInkDashboardBoard.Section section3 = section2;
              ClickyInkDashboardBoard.Section section4 = ((section != null && section.Groups.Count > columns2.MoveOnRight) ? new ClickyInkDashboardBoard.Section("move-more", ClickyInkLiveNote.Words.Plain("Your move"), section.Groups.Skip<ClickyInkGroup>(columns2.MoveOnRight).ToList<ClickyInkGroup>()) : null);
              ClickyInkHStack clickyInkHStack = new ClickyInkHStack(36.0, InkVerticalAlignment.Top, new UIElement[]
              {
                this.Column(columns2.Left, null, section4),
                this.Column(columns2.Right, section3, null)
              });
              FrameworkElement frameworkElement2 = clickyInkHStack;
              if (columns2.Left.Count > 0 && (columns2.Right.Count > 0 || columns2.HasMove))
              {
                ClickyShapeView clickyShapeView = ClickyShapeView.Stroke(new ClickyInkRuledLine(new Point(0.5, 0.02), new Point(0.5, 0.98), 9 + i), PaperStroke.Round(this.<paper>P.Ink.Opacity(0.14), 1.0));
                clickyShapeView.Width = 1.0;
                clickyShapeView.HorizontalAlignment = HorizontalAlignment.Center;
                frameworkElement2 = new ClickyInkOverlay(clickyInkHStack, new UIElement[] { clickyShapeView });
              }
              frameworkElement2.HorizontalAlignment = HorizontalAlignment.Left;
              stackPanel.Children.Add(frameworkElement2.Padding(0.0, 16.0, 0.0, 0.0));
            }
          }
        }
        else
        {
          stackPanel.Children.Add(this.Column(clickyInkDashboardBoard.Sections, clickyInkDashboardBoard.Move, null).Padding(0.0, 14.0, 0.0, 0.0));
        }
        if (clickyInkDashboardBoard.Sources.Count > 0)
        {
          stackPanel.Children.Add(this.Footer(clickyInkDashboardBoard).Padding(0.0, 14.0, 0.0, 0.0));
        }
        stackPanel.Margin = new Thickness(36.0, 30.0, 36.0, 28.0);
        return new Grid
        {
          HorizontalAlignment = HorizontalAlignment.Left,
          VerticalAlignment = VerticalAlignment.Top,
          Children = 
          {
            this.Sheet(),
            stackPanel,
            ClickyInkDashboardView.Builder.Tape(-38.0, -16.0, HorizontalAlignment.Left),
            ClickyInkDashboardView.Builder.Tape(38.0, 16.0, HorizontalAlignment.Right)
          }
        };
      }

      // Token: 0x060036ED RID: 14061 RVA: 0x0067F6A8 File Offset: 0x0067F6A8
      private FrameworkElement Masthead(ClickyInkDashboardBoard board)
      {
        ClickyInkLiveNote.Block run = board.Run;
        ClickyInkRunLine clickyInkRunLine = ((run != null) ? new ClickyInkRunLine(run.Source) : null);
        string text = ((clickyInkRunLine != null) ? clickyInkRunLine.Number : null) ?? ((this.<runNumber>P != null) ? this.<runNumber>P.GetValueOrDefault().ToString(CultureInfo.InvariantCulture) : null);
        StackPanel stackPanel = new StackPanel
        {
          Spacing = 2.0
        };
        ClickyInkLiveNote.Words title = this.note.Title;
        if (title != null)
        {
          stackPanel.Children.Add(ClickyInkWordsView.Make(title, this.<clock>P, this.Style, ClickyPaperFonts.Hand(34.0), true, null, null).With(delegate(InkTextView t)
          {
            t.LineLimit = new int?(1);
            t.MinimumScale = 0.6;
          }));
        }
        string text4;
        if (this.<job>P == null)
        {
          string text2 = "  ·  ";
          string[] array = new string[2];
          array[0] = "KEPT BY CLICKY";
          int num = 1;
          string text3 = ((clickyInkRunLine != null) ? clickyInkRunLine.Next : null);
          array[num] = ((text3 != null) ? ("NEXT RUN " + text3).ToUpperInvariant() : null);
          text4 = string.Join(text2, array.Where<string>((string s) => s != null));
        }
        else
        {
          text4 = this.<job>P.CapsLine.ToUpperInvariant();
        }
        string text5 = text4;
        stackPanel.Children.Add(ClickyInkFormKit.Text(text5, ClickyPaperFonts.Typed(9.5, new FontWeight?(FontWeights.Bold)), this.Style.Quiet, 1.3, 0.7, new int?(1)));
        ClickyInkDashboardView.Builder.<>c__DisplayClass18_0 CS$<>8__locals1;
        CS$<>8__locals1.right = new StackPanel
        {
          Spacing = 3.0,
          HorizontalAlignment = HorizontalAlignment.Right
        };
        if (this.<job>P != null)
        {
          ClickyInkDashboardView.Builder.<Masthead>g__AddRight|18_2(new ClickyPaperStamp(this.<job>P.Status, this.<job>P.StampWord, 12.5, -5.0), ref CS$<>8__locals1);
          string liveLine = this.<job>P.LiveLine;
          if (liveLine != null)
          {
            ClickyInkDashboardView.Builder.<Masthead>g__AddRight|18_2(ClickyInkFormKit.Text(liveLine, ClickyPaperFonts.Hand(15.0), ClickyPaperInk.ClickyBlue, 0.0, 1.0, new int?(1)).With(delegate(InkTextView t)
            {
              t.TextAlignment = TextAlignment.Right;
            }).MaxWidthOf(300.0), ref CS$<>8__locals1);
          }
        }
        else
        {
          ClickyInkLiveNote.Block run2 = board.Run;
          if (run2 != null)
          {
            ClickyInkDashboardView.Builder.<Masthead>g__AddRight|18_2(ClickyInkDashboardFormNeeds.RunStampMark(text, run2.Source, this.<clock>P, run2.LandsAt, 12.5), ref CS$<>8__locals1);
          }
          else if (text != null)
          {
            ClickyInkDashboardView.Builder.<Masthead>g__AddRight|18_2(new ClickyPaperStamp(ClickyThingStatus.Watching, "RUN " + text, 12.5, -5.0), ref CS$<>8__locals1);
          }
        }
        string text6 = ((clickyInkRunLine != null) ? clickyInkRunLine.RanAt : null) ?? this.<dateLine>P;
        if (text6 != null)
        {
          ClickyInkDashboardView.Builder.<Masthead>g__AddRight|18_2(ClickyInkFormKit.Text(text6, ClickyPaperFonts.Typed(11.5, new FontWeight?(FontWeights.SemiBold)), this.Style.Ink.Opacity(0.7), 0.0, 1.0, new int?(1)), ref CS$<>8__locals1);
        }
        string text7 = ((clickyInkRunLine != null) ? clickyInkRunLine.Extra : null);
        if (text7 != null)
        {
          ClickyInkDashboardView.Builder.<Masthead>g__AddRight|18_2(ClickyInkFormKit.Text(text7, ClickyPaperFonts.Hand(15.0), this.Style.Quiet, 0.0, 1.0, new int?(1)), ref CS$<>8__locals1);
        }
        FrameworkElement frameworkElement = this.Changes(board.Changes, 15.0, 5.0);
        if (frameworkElement != null)
        {
          ClickyInkDashboardView.Builder.<Masthead>g__AddRight|18_2(frameworkElement.Padding(0.0, 2.0, 0.0, 0.0), ref CS$<>8__locals1);
        }
        return new ClickyInkHStack(16.0, InkVerticalAlignment.Top, new UIElement[]
        {
          ClickyInkHStack.Flexible<StackPanel>(stackPanel),
          new ClickyInkSpacer(),
          CS$<>8__locals1.right
        });
      }

      // Token: 0x060036EE RID: 14062 RVA: 0x0067FAF4 File Offset: 0x0067FAF4
      [return: Nullable(2)]
      private FrameworkElement Changes(ClickyInkDashboardBoard.ChangeCounts changes, double size, double padding)
      {
        return ClickyInkFormKit.Changes(changes, this.Style, size, padding);
      }

      // Token: 0x060036EF RID: 14063 RVA: 0x0067FB04 File Offset: 0x0067FB04
      [return: Nullable(2)]
      private FrameworkElement Headline(ClickyInkDashboardBoard board)
      {
        ClickyInkGroup quiet = board.Quiet;
        if (quiet != null)
        {
          StackPanel stackPanel = new StackPanel
          {
            Spacing = 8.0,
            Margin = new Thickness(0.0, 4.0, 0.0, 4.0)
          };
          stackPanel.Children.Add(ClickyInkDashboardFormNeeds.Quiet(quiet.Blocks[0], this.<clock>P, this.Style, this.InnerWidth, true));
          ClickyInkGroup answer = board.Answer;
          if (answer != null)
          {
            stackPanel.Children.Add(this.LastNews(answer.Blocks[0]));
          }
          return ClickyInkGrow.Around(stackPanel, this.<clock>P, quiet.LandsAt, quiet.VanishesAt);
        }
        ClickyInkGroup answer2 = board.Answer;
        if (answer2 != null)
        {
          ClickyInkLiveNote.Block block = answer2.Blocks[0];
          double? num = ClickyInkChange.ChangedAt(block.Source);
          double num2;
          if (num != null)
          {
            double valueOrDefault = num.GetValueOrDefault();
            num2 = block.Words.FinishOfWriting(valueOrDefault);
          }
          else
          {
            num2 = block.Words.FinishedAt;
          }
          double num3 = num2;
          ClickyInkHStack clickyInkHStack = new ClickyInkHStack(14.0, InkVerticalAlignment.Center, Array.Empty<UIElement>());
          clickyInkHStack.Children.Add(ClickyInkDashboardFormNeeds.LampView(board.Overall, 20.0, 4, ClickyInkClock.Progress(this.<clock>P, block.LandsAt, block.LandsAt + 0.5)));
          bool flag = block.IsStruck(this.<clock>P);
          InkTextView inkTextView = ClickyInkWordsView.Make(block.Words, this.<clock>P, this.Style, ClickyPaperFonts.Hand((double)((block.Words.Current.CharacterCount() > 70) ? 22 : 26)), true, flag ? new Color?(this.Style.Quiet) : null, null);
          if (flag)
          {
            inkTextView.Strikethrough = new Color?(this.Style.Fresh);
          }
          clickyInkHStack.Children.Add(inkTextView);
          clickyInkHStack.Children.Add(new ClickyInkSpacer());
          string text = block.Source["big"];
          if (text != null && text.Length > 0)
          {
            FrameworkElement frameworkElement = ClickyInkDashboardFormNeeds.TickingNumber(block.Source, "big", this.<clock>P, this.Style, (double)((text.CharacterCount() > 6) ? 30 : 38), Math.Max(0.0, num3 - num.GetValueOrDefault(num3)), text, ClickyInkChange.Previous(block.Source, "big") ?? block.Source["_was"], false);
            frameworkElement.Margin = new Thickness(8.0, 2.0, 8.0, 2.0);
            ClickyShapeView clickyShapeView = ClickyShapeView.Stroke(new ClickyHandRing(text.CharacterCount(), 0.16).Trim(0.0, ClickyInkClock.Progress(this.<clock>P, num3, num3 + 0.5)), PaperStroke.Round(this.Style.Fresh.Opacity(0.8), 2.2));
            clickyShapeView.Margin = new Thickness(-4.0);
            StackPanel stackPanel2 = new StackPanel
            {
              HorizontalAlignment = HorizontalAlignment.Right
            };
            ClickyInkOverlay clickyInkOverlay = new ClickyInkOverlay(frameworkElement, new UIElement[] { clickyShapeView })
            {
              HorizontalAlignment = HorizontalAlignment.Right
            };
            stackPanel2.Children.Add(clickyInkOverlay);
            string text2 = block.Source["note"];
            if (text2 != null && text2.Length > 0)
            {
              InkTextView inkTextView2 = ClickyInkFormKit.Text((num != null && this.<clock>P < num3) ? (ClickyInkChange.Previous(block.Source, "note") ?? text2) : text2, ClickyPaperFonts.Hand(15.0), this.Style.Fresh, 0.0, 1.0, new int?(1));
              inkTextView2.HorizontalAlignment = HorizontalAlignment.Right;
              stackPanel2.Children.Add(inkTextView2);
            }
            clickyInkHStack.Children.Add(stackPanel2);
          }
          clickyInkHStack.Margin = new Thickness(0.0, 4.0, 0.0, 4.0);
          return ClickyInkGrow.Around(clickyInkHStack, this.<clock>P, answer2.LandsAt, answer2.VanishesAt);
        }
        return null;
      }

      // Token: 0x060036F0 RID: 14064 RVA: 0x0067FFAC File Offset: 0x0067FFAC
      private FrameworkElement LastNews(ClickyInkLiveNote.Block block)
      {
        ClickyInkHStack clickyInkHStack = new ClickyInkHStack(8.0, InkVerticalAlignment.FirstBaseline, Array.Empty<UIElement>());
        clickyInkHStack.Children.Add(ClickyInkFormKit.Pill("last news", ClickyPaperFonts.Hand(15.0), this.Style.Quiet, this.Style.Quiet.Opacity(0.5), 5.0, 1.0, -1.5));
        clickyInkHStack.Children.Add(ClickyInkWordsView.Make(block.Words, this.<clock>P, this.Style, ClickyPaperFonts.Hand(18.0), false, new Color?(this.Style.Ink.Opacity(0.75)), null).With(delegate(InkTextView t)
        {
          t.LineLimit = new int?(2);
        }));
        string text = block.Source["big"];
        if (text != null && text.Length > 0)
        {
          clickyInkHStack.Children.Add(ClickyInkFormKit.Text(text, ClickyPaperFonts.BigNumber(15.0), this.Style.Ink.Opacity(0.7), 0.0, 1.0, new int?(1)));
        }
        clickyInkHStack.Children.Add(new ClickyInkSpacer());
        clickyInkHStack.Margin = new Thickness(4.0, 0.0, 0.0, 0.0);
        return clickyInkHStack;
      }

      // Token: 0x060036F1 RID: 14065 RVA: 0x0068015C File Offset: 0x0068015C
      private FrameworkElement Column(IEnumerable<ClickyInkDashboardBoard.Section> sections, [Nullable(2)] ClickyInkDashboardBoard.Section move, [Nullable(2)] ClickyInkDashboardBoard.Section moreMove = null)
      {
        StackPanel stackPanel = new StackPanel
        {
          Spacing = 18.0,
          Width = (this.IsTwoColumns ? 378.0 : this.InnerWidth),
          HorizontalAlignment = HorizontalAlignment.Left
        };
        if (move != null)
        {
          stackPanel.Children.Add(this.Section(move, true, true));
        }
        foreach (ClickyInkDashboardBoard.Section section in sections)
        {
          stackPanel.Children.Add(this.Section(section, false, true));
        }
        if (moreMove != null)
        {
          stackPanel.Children.Add(this.Section(moreMove, true, false));
        }
        return stackPanel;
      }

      // Token: 0x060036F2 RID: 14066 RVA: 0x0068021C File Offset: 0x0068021C
      private FrameworkElement Header(ClickyInkDashboardBoard.Section section, bool isMove)
      {
        ClickyInkHStack clickyInkHStack = new ClickyInkHStack(8.0, InkVerticalAlignment.FirstBaseline, Array.Empty<UIElement>());
        if (isMove)
        {
          clickyInkHStack.Children.Add(ClickyInkFormKit.Text("your move", ClickyPaperFonts.Hand(21.0), PaperColor.Hex("#B05F00"), 0.0, 1.0, new int?(1)));
        }
        else
        {
          clickyInkHStack.Children.Add(ClickyInkWordsView.Make(section.Name, this.<clock>P, this.Style, ClickyPaperFonts.Hand(21.0), true, null, null).With(delegate(InkTextView t)
          {
            t.LineLimit = new int?(1);
          }));
        }
        int num = section.NewCount(this.<clock>P);
        if (num > 0 && !section.IsNamedNew)
        {
          UIElementCollection children = clickyInkHStack.Children;
          DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(4, 1);
          defaultInterpolatedStringHandler.AppendFormatted<int>(num);
          defaultInterpolatedStringHandler.AppendLiteral(" new");
          children.Add(ClickyInkTag.Make(defaultInterpolatedStringHandler.ToStringAndClear(), ClickyPaperInk.DoneGreen));
        }
        clickyInkHStack.Children.Add(ClickyInkFormKit.Squiggle(section.Id.CharacterCount(), this.Style.Ink.Opacity(0.16)));
        clickyInkHStack.Margin = new Thickness(0.0, 0.0, 0.0, 8.0);
        return clickyInkHStack;
      }

      // Token: 0x060036F3 RID: 14067 RVA: 0x006803A8 File Offset: 0x006803A8
      private FrameworkElement Section(ClickyInkDashboardBoard.Section section, bool isMove, bool showsHeader = true)
      {
        StackPanel stackPanel = new StackPanel();
        if (showsHeader)
        {
          stackPanel.Children.Add(this.Header(section, isMove));
        }
        foreach (ClickyInkGroup clickyInkGroup in section.Groups)
        {
          stackPanel.Children.Add(this.GroupRow(clickyInkGroup));
        }
        stackPanel.Opacity = section.Groups.Count != 0;
        return stackPanel;
      }

      // Token: 0x060036F4 RID: 14068 RVA: 0x00680438 File Offset: 0x00680438
      private FrameworkElement GroupRow(ClickyInkGroup group)
      {
        FrameworkElement frameworkElement = ClickyInkGroupView.Make(group, this.<clock>P, this.Style, this.<paper>P, this.<actions>P);
        frameworkElement.Margin = new Thickness(frameworkElement.Margin.Left, frameworkElement.Margin.Top, frameworkElement.Margin.Right, frameworkElement.Margin.Bottom + 12.0);
        return ClickyInkGrow.Around(frameworkElement, this.<clock>P, group.LandsAt, group.VanishesAt);
      }

      // Token: 0x060036F5 RID: 14069 RVA: 0x006804CC File Offset: 0x006804CC
      private FrameworkElement WideSection(ClickyInkDashboardBoard.Section section)
      {
        List<ClickyInkGroup> list = ClickyInkDashboardView.SpreadingLongList(section.Groups.Where<ClickyInkGroup>((ClickyInkGroup g) => g.Kind != ClickyInkDocument.Kind.Table).ToList<ClickyInkGroup>());
        StackPanel stackPanel = new StackPanel
        {
          Width = this.InnerWidth,
          HorizontalAlignment = HorizontalAlignment.Left
        };
        stackPanel.Children.Add(this.Header(section, false));
        foreach (ClickyInkGroup clickyInkGroup in section.Groups.Where<ClickyInkGroup>((ClickyInkGroup g) => g.Kind == ClickyInkDocument.Kind.Table))
        {
          FrameworkElement frameworkElement = ClickyInkTable.Create(clickyInkGroup.Blocks[0], this.<clock>P, this.Style, this.InnerWidth);
          frameworkElement.Margin = new Thickness(frameworkElement.Margin.Left, frameworkElement.Margin.Top, frameworkElement.Margin.Right, frameworkElement.Margin.Bottom + 12.0);
          stackPanel.Children.Add(ClickyInkGrow.Around(frameworkElement, this.<clock>P, clickyInkGroup.LandsAt, clickyInkGroup.VanishesAt));
        }
        if (list.Count > 0)
        {
          UIElementCollection children = stackPanel.Children;
          double num = 36.0;
          InkVerticalAlignment inkVerticalAlignment = InkVerticalAlignment.Top;
          UIElement[] array = new UIElement[2];
          array[0] = this.Groups(list.Where<ClickyInkGroup>((ClickyInkGroup _, int i) => i % 2 == 0));
          array[1] = this.Groups(list.Where<ClickyInkGroup>((ClickyInkGroup _, int i) => i % 2 == 1));
          children.Add(new ClickyInkHStack(num, inkVerticalAlignment, array)
          {
            HorizontalAlignment = HorizontalAlignment.Left
          });
        }
        return stackPanel;
      }

      // Token: 0x060036F6 RID: 14070 RVA: 0x006806CC File Offset: 0x006806CC
      private FrameworkElement Groups(IEnumerable<ClickyInkGroup> groups)
      {
        StackPanel stackPanel = new StackPanel
        {
          Width = 378.0,
          HorizontalAlignment = HorizontalAlignment.Left
        };
        foreach (ClickyInkGroup clickyInkGroup in groups)
        {
          stackPanel.Children.Add(this.GroupRow(clickyInkGroup));
        }
        return stackPanel;
      }

      // Token: 0x060036F7 RID: 14071 RVA: 0x0068073C File Offset: 0x0068073C
      private FrameworkElement Footer(ClickyInkDashboardBoard board)
      {
        StackPanel stackPanel = new StackPanel
        {
          Spacing = 6.0
        };
        stackPanel.Children.Add(new Border
        {
          Height = 1.0,
          Background = Ui.Brush(this.Style.Ink.Opacity(0.12))
        });
        ClickyNotebookFlow clickyNotebookFlow = new ClickyNotebookFlow
        {
          Spacing = 14.0,
          LineSpacing = 4.0
        };
        clickyNotebookFlow.Children.Add(ClickyInkFormKit.Text("sources", ClickyPaperFonts.Hand(15.0), this.Style.Quiet, 0.0, 1.0, new int?(1)));
        foreach (ClickyInkLiveNote.Block block in board.Sources)
        {
          if (this.<clock>P >= block.LandsAt)
          {
            clickyNotebookFlow.Children.Add(ClickyInkFormKit.SourceLink(block, this.<clock>P, this.Style, this.<actions>P, 10.5, 11.5));
          }
        }
        stackPanel.Children.Add(clickyNotebookFlow);
        return stackPanel;
      }

      // Token: 0x060036F8 RID: 14072 RVA: 0x0068089C File Offset: 0x0068089C
      private FrameworkElement Sheet()
      {
        PaperSurface paperSurface = new PaperSurface(ClickyShape.RoundedRectangle(6.0), this.<paper>P.Fill, new double?(0.45)).WithShadow(PaperColor.Black.Opacity(0.28), 12.0, 0.0, 5.0);
        paperSurface.Washes.Add(ClickyInkFormKit.Dots(18.0, 0.75));
        return paperSurface;
      }

      // Token: 0x060036F9 RID: 14073 RVA: 0x0068092C File Offset: 0x0068092C
      private static FrameworkElement Tape(double angle, double x, HorizontalAlignment alignment)
      {
        PaperSurface paperSurface = new PaperSurface(ClickyShape.Rectangle, PaperFill.Of(PaperColor.Hex("#F4EBD2").Opacity(0.78)), null).WithOutline(new PaperStroke(PaperColor.Black.Opacity(0.04), 0.5, PenLineCap.Flat, PenLineJoin.Miter, null), false).WithShadow(PaperColor.Black.Opacity(0.08), 1.0, 0.0, 1.0);
        paperSurface.Width = 70.0;
        paperSurface.Height = 20.0;
        paperSurface.HorizontalAlignment = alignment;
        paperSurface.VerticalAlignment = VerticalAlignment.Top;
        paperSurface.Transformed(x, 2.0, angle, 1.0, null);
        return paperSurface;
      }

      // Token: 0x060036FA RID: 14074 RVA: 0x00680A18 File Offset: 0x00680A18
      [CompilerGenerated]
      internal static void <Masthead>g__AddRight|18_2(FrameworkElement e, ref ClickyInkDashboardView.Builder.<>c__DisplayClass18_0 A_1)
      {
        e.HorizontalAlignment = HorizontalAlignment.Right;
        A_1.right.Children.Add(e);
      }

      // Token: 0x0400215E RID: 8542
      [CompilerGenerated]
      private double <clock>P = clock;

      // Token: 0x0400215F RID: 8543
      [CompilerGenerated]
      private ClickyHomeSpreadPaper <paper>P = paper;

      // Token: 0x04002160 RID: 8544
      [CompilerGenerated]
      private ClickyInkActions <actions>P = actions;

      // Token: 0x04002161 RID: 8545
      [CompilerGenerated]
      private double <width>P = width;

      // Token: 0x04002162 RID: 8546
      [CompilerGenerated]
      private int? <runNumber>P = runNumber;

      // Token: 0x04002163 RID: 8547
      [Nullable(2)]
      [CompilerGenerated]
      private string <dateLine>P = dateLine;

      // Token: 0x04002164 RID: 8548
      [Nullable(2)]
      [CompilerGenerated]
      private ClickyInkDashboardView.Job <job>P = job;

      // Token: 0x04002165 RID: 8549
      [CompilerGenerated]
      private ClickyInkTiles.Look <tilesLook>P = tilesLook;

      // Token: 0x04002166 RID: 8550
      private readonly ClickyInkLiveNote note = rawNote.FoldingStruckWords(2.5);

      // Token: 0x04002167 RID: 8551
      [Nullable(2)]
      private ClickyInkStyle style;
    }
  }
}
