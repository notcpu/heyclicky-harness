using System;
using System.CodeDom.Compiler;
using System.Runtime.CompilerServices;

namespace System.Text.RegularExpressions.Generated
{
  // Token: 0x0200001C RID: 28
  [GeneratedCode("System.Text.RegularExpressions.Generator", "9.0.15.2218")]
  [SkipLocalsInit]
  internal sealed class <RegexGenerator_g>F2A86C3D8F8653D6C99BE1B8441A5DADEC6C855822CB294D49492A00C67E55137__FontEntry_26 : Regex
  {
    // Token: 0x06000035 RID: 53 RVA: 0x00331CF4 File Offset: 0x00331CF4
    private <RegexGenerator_g>F2A86C3D8F8653D6C99BE1B8441A5DADEC6C855822CB294D49492A00C67E55137__FontEntry_26()
    {
      this.pattern = "/([^\\s/<>\\[\\]()]+)\\s+(\\d+)\\s+\\d+\\s+R";
      this.roptions = RegexOptions.None;
      Regex.ValidateMatchTimeout(<RegexGenerator_g>F2A86C3D8F8653D6C99BE1B8441A5DADEC6C855822CB294D49492A00C67E55137__Utilities.s_defaultTimeout);
      this.internalMatchTimeout = <RegexGenerator_g>F2A86C3D8F8653D6C99BE1B8441A5DADEC6C855822CB294D49492A00C67E55137__Utilities.s_defaultTimeout;
      this.factory = new <RegexGenerator_g>F2A86C3D8F8653D6C99BE1B8441A5DADEC6C855822CB294D49492A00C67E55137__FontEntry_26.RunnerFactory();
      this.capsize = 3;
    }

    // Token: 0x0400001B RID: 27
    [Nullable(1)]
    internal static readonly <RegexGenerator_g>F2A86C3D8F8653D6C99BE1B8441A5DADEC6C855822CB294D49492A00C67E55137__FontEntry_26 Instance = new <RegexGenerator_g>F2A86C3D8F8653D6C99BE1B8441A5DADEC6C855822CB294D49492A00C67E55137__FontEntry_26();

    // Token: 0x0200024F RID: 591
    private sealed class RunnerFactory : RegexRunnerFactory
    {
      // Token: 0x06002128 RID: 8488 RVA: 0x003F6E40 File Offset: 0x003F6E40
      [NullableContext(1)]
      protected override RegexRunner CreateInstance()
      {
        return new <RegexGenerator_g>F2A86C3D8F8653D6C99BE1B8441A5DADEC6C855822CB294D49492A00C67E55137__FontEntry_26.RunnerFactory.Runner();
      }

      // Token: 0x020006B9 RID: 1721
      private sealed class Runner : RegexRunner
      {
        // Token: 0x06003A80 RID: 14976 RVA: 0x0045D65C File Offset: 0x0045D65C
        protected override void Scan(ReadOnlySpan<char> inputSpan)
        {
          while (this.TryFindNextPossibleStartingPosition(inputSpan) && !this.TryMatchAtCurrentPosition(inputSpan) && this.runtextpos != inputSpan.Length)
          {
            this.runtextpos++;
            if (<RegexGenerator_g>F2A86C3D8F8653D6C99BE1B8441A5DADEC6C855822CB294D49492A00C67E55137__Utilities.s_hasTimeout)
            {
              base.CheckTimeout();
            }
          }
        }

        // Token: 0x06003A81 RID: 14977 RVA: 0x0045D69C File Offset: 0x0045D69C
        private unsafe bool TryFindNextPossibleStartingPosition(ReadOnlySpan<char> inputSpan)
        {
          int runtextpos = this.runtextpos;
          if (runtextpos <= inputSpan.Length - 8)
          {
            ReadOnlySpan<char> readOnlySpan = inputSpan.Slice(runtextpos);
            for (int i = 0; i < readOnlySpan.Length - 7; i++)
            {
              int num = readOnlySpan.Slice(i).IndexOf('/');
              if (num < 0)
              {
                break;
              }
              i += num;
              if (i + 1 >= readOnlySpan.Length)
              {
                break;
              }
              char c;
              if (((c = (char)(*readOnlySpan[i + 1])) < '\u0080') ? (((int)"쇿\uffff糾꿿\uffff\ud7ff\uffff\uffff"[(int)(c >> 4)] & (1 << (int)(c & '\u000f'))) != 0) : RegexRunner.CharInClass(c, "\u0001\f\u0001(*/0<=>?[\\]^d"))
              {
                this.runtextpos = runtextpos + i;
                return true;
              }
            }
          }
          this.runtextpos = inputSpan.Length;
          return false;
        }

        // Token: 0x06003A82 RID: 14978 RVA: 0x0045D75C File Offset: 0x0045D75C
        private unsafe bool TryMatchAtCurrentPosition(ReadOnlySpan<char> inputSpan)
        {
          int num = this.runtextpos;
          int num2 = num;
          ReadOnlySpan<char> readOnlySpan = inputSpan.Slice(num);
          if (readOnlySpan.IsEmpty || *readOnlySpan[0] != 47)
          {
            this.<TryMatchAtCurrentPosition>g__UncaptureUntil|2_0(0);
            return false;
          }
          num++;
          readOnlySpan = inputSpan.Slice(num);
          int num3 = num;
          int num4 = num;
          int num5 = 0;
          char c;
          while (num5 < readOnlySpan.Length && (((c = (char)(*readOnlySpan[num5])) < '\u0080') ? (((int)"쇿\uffff糾꿿\uffff\ud7ff\uffff\uffff"[(int)(c >> 4)] & (1 << (int)(c & '\u000f'))) != 0) : RegexRunner.CharInClass(c, "\u0001\f\u0001(*/0<=>?[\\]^d")))
          {
            num5++;
          }
          if (num5 == 0)
          {
            this.<TryMatchAtCurrentPosition>g__UncaptureUntil|2_0(0);
            return false;
          }
          readOnlySpan = readOnlySpan.Slice(num5);
          num += num5;
          int num6 = num;
          num4++;
          for (;;)
          {
            int num7 = base.Crawlpos();
            base.Capture(1, num3, num);
            int num8 = 0;
            while (num8 < readOnlySpan.Length && char.IsWhiteSpace((char)(*readOnlySpan[num8])))
            {
              num8++;
            }
            if (num8 != 0)
            {
              readOnlySpan = readOnlySpan.Slice(num8);
              num += num8;
              int num9 = num;
              int num10 = 0;
              while (num10 < readOnlySpan.Length && char.IsDigit((char)(*readOnlySpan[num10])))
              {
                num10++;
              }
              if (num10 != 0)
              {
                readOnlySpan = readOnlySpan.Slice(num10);
                num += num10;
                base.Capture(2, num9, num);
                int num11 = 0;
                while (num11 < readOnlySpan.Length && char.IsWhiteSpace((char)(*readOnlySpan[num11])))
                {
                  num11++;
                }
                if (num11 != 0)
                {
                  readOnlySpan = readOnlySpan.Slice(num11);
                  num += num11;
                  int num12 = 0;
                  while (num12 < readOnlySpan.Length && char.IsDigit((char)(*readOnlySpan[num12])))
                  {
                    num12++;
                  }
                  if (num12 != 0)
                  {
                    readOnlySpan = readOnlySpan.Slice(num12);
                    num += num12;
                    int num13 = 0;
                    while (num13 < readOnlySpan.Length && char.IsWhiteSpace((char)(*readOnlySpan[num13])))
                    {
                      num13++;
                    }
                    if (num13 != 0)
                    {
                      readOnlySpan = readOnlySpan.Slice(num13);
                      num += num13;
                      if (!readOnlySpan.IsEmpty && *readOnlySpan[0] == 82)
                      {
                        goto Block_18;
                      }
                    }
                  }
                }
              }
            }
            this.<TryMatchAtCurrentPosition>g__UncaptureUntil|2_0(num7);
            if (<RegexGenerator_g>F2A86C3D8F8653D6C99BE1B8441A5DADEC6C855822CB294D49492A00C67E55137__Utilities.s_hasTimeout)
            {
              base.CheckTimeout();
            }
            if (num4 >= num6)
            {
              break;
            }
            num6 = (num = num6 - 1);
            readOnlySpan = inputSpan.Slice(num);
          }
          this.<TryMatchAtCurrentPosition>g__UncaptureUntil|2_0(0);
          return false;
          Block_18:
          num++;
          this.runtextpos = num;
          base.Capture(0, num2, num);
          return true;
        }

        // Token: 0x06003A84 RID: 14980 RVA: 0x0045D9F8 File Offset: 0x0045D9F8
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
