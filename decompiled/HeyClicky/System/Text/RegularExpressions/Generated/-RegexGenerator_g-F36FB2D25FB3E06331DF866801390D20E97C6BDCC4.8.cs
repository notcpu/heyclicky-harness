using System;
using System.CodeDom.Compiler;
using System.Collections;
using System.Runtime.CompilerServices;

namespace System.Text.RegularExpressions.Generated
{
  // Token: 0x02000070 RID: 112
  [GeneratedCode("System.Text.RegularExpressions.Generator", "9.0.15.2218")]
  [SkipLocalsInit]
  internal sealed class <RegexGenerator_g>F36FB2D25FB3E06331DF866801390D20E97C6BDCC4C519A6278D40F829CA940A3__InlineRegex_7 : Regex
  {
    // Token: 0x060002AF RID: 687 RVA: 0x0051B1F8 File Offset: 0x0051B1F8
    private <RegexGenerator_g>F36FB2D25FB3E06331DF866801390D20E97C6BDCC4C519A6278D40F829CA940A3__InlineRegex_7()
    {
      this.pattern = "\\*\\*(?<bold>[^*]+)\\*\\*|__(?<bold>[^_]+)__|\\*(?<ital>[^*\\s][^*]*)\\*|`(?<code>[^`]+)`|\\[(?<ltext>[^\\]]+)\\]\\((?<lurl>[^)\\s]+)\\)";
      this.roptions = RegexOptions.None;
      Regex.ValidateMatchTimeout(<RegexGenerator_g>F36FB2D25FB3E06331DF866801390D20E97C6BDCC4C519A6278D40F829CA940A3__Utilities.s_defaultTimeout);
      this.internalMatchTimeout = <RegexGenerator_g>F36FB2D25FB3E06331DF866801390D20E97C6BDCC4C519A6278D40F829CA940A3__Utilities.s_defaultTimeout;
      this.factory = new <RegexGenerator_g>F36FB2D25FB3E06331DF866801390D20E97C6BDCC4C519A6278D40F829CA940A3__InlineRegex_7.RunnerFactory();
      base.CapNames = new Hashtable
      {
        { "0", 0 },
        { "bold", 1 },
        { "code", 3 },
        { "ital", 2 },
        { "ltext", 4 },
        { "lurl", 5 }
      };
      this.capslist = new string[] { "0", "bold", "ital", "code", "ltext", "lurl" };
      this.capsize = 6;
    }

    // Token: 0x04000068 RID: 104
    [Nullable(1)]
    internal static readonly <RegexGenerator_g>F36FB2D25FB3E06331DF866801390D20E97C6BDCC4C519A6278D40F829CA940A3__InlineRegex_7 Instance = new <RegexGenerator_g>F36FB2D25FB3E06331DF866801390D20E97C6BDCC4C519A6278D40F829CA940A3__InlineRegex_7();

    // Token: 0x0200037C RID: 892
    private sealed class RunnerFactory : RegexRunnerFactory
    {
      // Token: 0x060026B7 RID: 9911 RVA: 0x00640B1C File Offset: 0x00640B1C
      [NullableContext(1)]
      protected override RegexRunner CreateInstance()
      {
        return new <RegexGenerator_g>F36FB2D25FB3E06331DF866801390D20E97C6BDCC4C519A6278D40F829CA940A3__InlineRegex_7.RunnerFactory.Runner();
      }

      // Token: 0x02000AB0 RID: 2736
      private sealed class Runner : RegexRunner
      {
        // Token: 0x060041A9 RID: 16809 RVA: 0x006C8FF4 File Offset: 0x006C8FF4
        protected override void Scan(ReadOnlySpan<char> inputSpan)
        {
          while (this.TryFindNextPossibleStartingPosition(inputSpan) && !this.TryMatchAtCurrentPosition(inputSpan) && this.runtextpos != inputSpan.Length)
          {
            this.runtextpos++;
            if (<RegexGenerator_g>F36FB2D25FB3E06331DF866801390D20E97C6BDCC4C519A6278D40F829CA940A3__Utilities.s_hasTimeout)
            {
              base.CheckTimeout();
            }
          }
        }

        // Token: 0x060041AA RID: 16810 RVA: 0x006C9034 File Offset: 0x006C9034
        private bool TryFindNextPossibleStartingPosition(ReadOnlySpan<char> inputSpan)
        {
          int runtextpos = this.runtextpos;
          if (runtextpos <= inputSpan.Length - 3)
          {
            int num = inputSpan.Slice(runtextpos).IndexOfAny<char>(<RegexGenerator_g>F36FB2D25FB3E06331DF866801390D20E97C6BDCC4C519A6278D40F829CA940A3__Utilities.s_ascii_400000000008801000000);
            if (num >= 0)
            {
              this.runtextpos = runtextpos + num;
              return true;
            }
          }
          this.runtextpos = inputSpan.Length;
          return false;
        }

        // Token: 0x060041AB RID: 16811 RVA: 0x006C9084 File Offset: 0x006C9084
        private unsafe bool TryMatchAtCurrentPosition(ReadOnlySpan<char> inputSpan)
        {
          int num = this.runtextpos;
          int num2 = num;
          ReadOnlySpan<char> readOnlySpan = inputSpan.Slice(num);
          if (readOnlySpan.IsEmpty)
          {
            this.<TryMatchAtCurrentPosition>g__UncaptureUntil|2_0(0);
            return false;
          }
          char c = (char)(*readOnlySpan[0]);
          if (c <= '[')
          {
            if (c != '*')
            {
              if (c == '[')
              {
                num++;
                readOnlySpan = inputSpan.Slice(num);
                int num3 = num;
                int num4 = readOnlySpan.IndexOf(']');
                if (num4 < 0)
                {
                  num4 = readOnlySpan.Length;
                }
                if (num4 == 0)
                {
                  this.<TryMatchAtCurrentPosition>g__UncaptureUntil|2_0(0);
                  return false;
                }
                readOnlySpan = readOnlySpan.Slice(num4);
                num += num4;
                base.Capture(4, num3, num);
                if (!readOnlySpan.StartsWith<char>("]("))
                {
                  this.<TryMatchAtCurrentPosition>g__UncaptureUntil|2_0(0);
                  return false;
                }
                num += 2;
                readOnlySpan = inputSpan.Slice(num);
                int num5 = num;
                int num6 = 0;
                char c2;
                while (num6 < readOnlySpan.Length && (((c2 = (char)(*readOnlySpan[num6])) < '\u0080') ? (((int)"쇿\uffff﷾\uffff\uffff\uffff\uffff\uffff"[(int)(c2 >> 4)] & (1 << (int)(c2 & '\u000f'))) != 0) : RegexRunner.CharInClass(c2, "\u0001\u0002\u0001)*d")))
                {
                  num6++;
                }
                if (num6 == 0)
                {
                  this.<TryMatchAtCurrentPosition>g__UncaptureUntil|2_0(0);
                  return false;
                }
                readOnlySpan = readOnlySpan.Slice(num6);
                num += num6;
                base.Capture(5, num5, num);
                if (readOnlySpan.IsEmpty || *readOnlySpan[0] != 41)
                {
                  this.<TryMatchAtCurrentPosition>g__UncaptureUntil|2_0(0);
                  return false;
                }
                num++;
                readOnlySpan = inputSpan.Slice(num);
                goto IL_0443;
              }
            }
            else
            {
              int num7 = num;
              int num8 = base.Crawlpos();
              if (readOnlySpan.Length >= 2 && *readOnlySpan[1] == 42)
              {
                num += 2;
                readOnlySpan = inputSpan.Slice(num);
                int num9 = num;
                int num10 = readOnlySpan.IndexOf('*');
                if (num10 < 0)
                {
                  num10 = readOnlySpan.Length;
                }
                if (num10 != 0)
                {
                  readOnlySpan = readOnlySpan.Slice(num10);
                  num += num10;
                  base.Capture(1, num9, num);
                  if (readOnlySpan.StartsWith<char>("**"))
                  {
                    num += 2;
                    readOnlySpan = inputSpan.Slice(num);
                    goto IL_0443;
                  }
                }
              }
              num = num7;
              readOnlySpan = inputSpan.Slice(num);
              this.<TryMatchAtCurrentPosition>g__UncaptureUntil|2_0(num8);
              num++;
              readOnlySpan = inputSpan.Slice(num);
              int num11 = num;
              char c2;
              if (readOnlySpan.IsEmpty || (((c2 = (char)(*readOnlySpan[0])) < '\u0080') ? (((int)"쇿\uffffﯾ\uffff\uffff\uffff\uffff\uffff"[(int)(c2 >> 4)] & (1 << (int)(c2 & '\u000f'))) == 0) : (!RegexRunner.CharInClass(c2, "\u0001\u0002\u0001*+d"))))
              {
                this.<TryMatchAtCurrentPosition>g__UncaptureUntil|2_0(0);
                return false;
              }
              int num12 = readOnlySpan.Slice(1).IndexOf('*');
              if (num12 < 0)
              {
                num12 = readOnlySpan.Length - 1;
              }
              readOnlySpan = readOnlySpan.Slice(num12);
              num += num12;
              num++;
              readOnlySpan = inputSpan.Slice(num);
              base.Capture(2, num11, num);
              if (readOnlySpan.IsEmpty || *readOnlySpan[0] != 42)
              {
                this.<TryMatchAtCurrentPosition>g__UncaptureUntil|2_0(0);
                return false;
              }
              num++;
              readOnlySpan = inputSpan.Slice(num);
              goto IL_0443;
            }
          }
          else if (c != '_')
          {
            if (c == '`')
            {
              num++;
              readOnlySpan = inputSpan.Slice(num);
              int num13 = num;
              int num14 = readOnlySpan.IndexOf('`');
              if (num14 < 0)
              {
                num14 = readOnlySpan.Length;
              }
              if (num14 == 0)
              {
                this.<TryMatchAtCurrentPosition>g__UncaptureUntil|2_0(0);
                return false;
              }
              readOnlySpan = readOnlySpan.Slice(num14);
              num += num14;
              base.Capture(3, num13, num);
              if (readOnlySpan.IsEmpty || *readOnlySpan[0] != 96)
              {
                this.<TryMatchAtCurrentPosition>g__UncaptureUntil|2_0(0);
                return false;
              }
              num++;
              readOnlySpan = inputSpan.Slice(num);
              goto IL_0443;
            }
          }
          else
          {
            if (readOnlySpan.Length < 2 || *readOnlySpan[1] != 95)
            {
              this.<TryMatchAtCurrentPosition>g__UncaptureUntil|2_0(0);
              return false;
            }
            num += 2;
            readOnlySpan = inputSpan.Slice(num);
            int num15 = num;
            int num16 = readOnlySpan.IndexOf('_');
            if (num16 < 0)
            {
              num16 = readOnlySpan.Length;
            }
            if (num16 == 0)
            {
              this.<TryMatchAtCurrentPosition>g__UncaptureUntil|2_0(0);
              return false;
            }
            readOnlySpan = readOnlySpan.Slice(num16);
            num += num16;
            base.Capture(1, num15, num);
            if (!readOnlySpan.StartsWith<char>("__"))
            {
              this.<TryMatchAtCurrentPosition>g__UncaptureUntil|2_0(0);
              return false;
            }
            num += 2;
            readOnlySpan = inputSpan.Slice(num);
            goto IL_0443;
          }
          this.<TryMatchAtCurrentPosition>g__UncaptureUntil|2_0(0);
          return false;
          IL_0443:
          this.runtextpos = num;
          base.Capture(0, num2, num);
          return true;
        }

        // Token: 0x060041AD RID: 16813 RVA: 0x006C94F0 File Offset: 0x006C94F0
        [CompilerGenerated]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void <TryMatchAtCurrentPosition>g__UncaptureUntil|2_0(int capturePosition)
        {
          while (base.Crawlpos() > capturePosition)
          {
            base.Uncapture();
          }
        }
      }
    }
  }
}
