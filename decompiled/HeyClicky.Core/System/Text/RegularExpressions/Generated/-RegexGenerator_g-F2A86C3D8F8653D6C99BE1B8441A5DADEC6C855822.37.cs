using System;
using System.CodeDom.Compiler;
using System.Runtime.CompilerServices;

namespace System.Text.RegularExpressions.Generated
{
  // Token: 0x02000026 RID: 38
  [GeneratedCode("System.Text.RegularExpressions.Generator", "9.0.15.2218")]
  [SkipLocalsInit]
  internal sealed class <RegexGenerator_g>F2A86C3D8F8653D6C99BE1B8441A5DADEC6C855822CB294D49492A00C67E55137__NumberRegex_36 : Regex
  {
    // Token: 0x06000049 RID: 73 RVA: 0x0033206C File Offset: 0x0033206C
    private <RegexGenerator_g>F2A86C3D8F8653D6C99BE1B8441A5DADEC6C855822CB294D49492A00C67E55137__NumberRegex_36()
    {
      this.pattern = "^([+-]?)([^\\d+\\-.]{0,4}?)([+-]?)(\\d[\\d,]*(?:\\.\\d+)?|\\.\\d+)(.*)$";
      this.roptions = RegexOptions.None;
      Regex.ValidateMatchTimeout(<RegexGenerator_g>F2A86C3D8F8653D6C99BE1B8441A5DADEC6C855822CB294D49492A00C67E55137__Utilities.s_defaultTimeout);
      this.internalMatchTimeout = <RegexGenerator_g>F2A86C3D8F8653D6C99BE1B8441A5DADEC6C855822CB294D49492A00C67E55137__Utilities.s_defaultTimeout;
      this.factory = new <RegexGenerator_g>F2A86C3D8F8653D6C99BE1B8441A5DADEC6C855822CB294D49492A00C67E55137__NumberRegex_36.RunnerFactory();
      this.capsize = 6;
    }

    // Token: 0x04000025 RID: 37
    [Nullable(1)]
    internal static readonly <RegexGenerator_g>F2A86C3D8F8653D6C99BE1B8441A5DADEC6C855822CB294D49492A00C67E55137__NumberRegex_36 Instance = new <RegexGenerator_g>F2A86C3D8F8653D6C99BE1B8441A5DADEC6C855822CB294D49492A00C67E55137__NumberRegex_36();

    // Token: 0x02000259 RID: 601
    private sealed class RunnerFactory : RegexRunnerFactory
    {
      // Token: 0x0600213C RID: 8508 RVA: 0x003F6EE0 File Offset: 0x003F6EE0
      [NullableContext(1)]
      protected override RegexRunner CreateInstance()
      {
        return new <RegexGenerator_g>F2A86C3D8F8653D6C99BE1B8441A5DADEC6C855822CB294D49492A00C67E55137__NumberRegex_36.RunnerFactory.Runner();
      }

      // Token: 0x020006C3 RID: 1731
      private sealed class Runner : RegexRunner
      {
        // Token: 0x06003AAF RID: 15023 RVA: 0x0045EA4C File Offset: 0x0045EA4C
        protected override void Scan(ReadOnlySpan<char> inputSpan)
        {
          if (this.TryFindNextPossibleStartingPosition(inputSpan) && !this.TryMatchAtCurrentPosition(inputSpan))
          {
            this.runtextpos = inputSpan.Length;
          }
        }

        // Token: 0x06003AB0 RID: 15024 RVA: 0x0045EA70 File Offset: 0x0045EA70
        private bool TryFindNextPossibleStartingPosition(ReadOnlySpan<char> inputSpan)
        {
          int runtextpos = this.runtextpos;
          if (runtextpos < inputSpan.Length && runtextpos == 0)
          {
            return true;
          }
          this.runtextpos = inputSpan.Length;
          return false;
        }

        // Token: 0x06003AB1 RID: 15025 RVA: 0x0045EAA4 File Offset: 0x0045EAA4
        private unsafe bool TryMatchAtCurrentPosition(ReadOnlySpan<char> inputSpan)
        {
          int num = this.runtextpos;
          int num2 = num;
          int num3 = 0;
          int num4 = 0;
          int num5 = 0;
          int num6 = 0;
          int num7 = 0;
          int num8 = 0;
          int num9 = 0;
          int num10 = 0;
          int num11 = 0;
          int num12 = 0;
          ReadOnlySpan<char> readOnlySpan = inputSpan.Slice(num);
          if (num != 0)
          {
            this.<TryMatchAtCurrentPosition>g__UncaptureUntil|2_0(0);
            return false;
          }
          int num13 = num;
          int num14 = num;
          char c;
          if (!readOnlySpan.IsEmpty && (((c = (char)(*readOnlySpan[0])) == '+') | (c == '-')))
          {
            readOnlySpan = readOnlySpan.Slice(1);
            num++;
          }
          int num15 = num;
          for (;;)
          {
            int num16 = base.Crawlpos();
            base.Capture(1, num13, num);
            int num17 = num;
            int num18 = 0;
            int num19 = num;
            for (;;)
            {
              int num20 = base.Crawlpos();
              base.Capture(2, num17, num);
              int num21 = num;
              if (!readOnlySpan.IsEmpty && (((c = (char)(*readOnlySpan[0])) == '+') | (c == '-')))
              {
                readOnlySpan = readOnlySpan.Slice(1);
                num++;
              }
              base.Capture(3, num21, num);
              int num22 = num;
              int num23 = num;
              int num24 = base.Crawlpos();
              if (!readOnlySpan.IsEmpty && char.IsDigit((char)(*readOnlySpan[0])))
              {
                num++;
                readOnlySpan = inputSpan.Slice(num);
                num5 = num;
                int num25 = 0;
                while (num25 < readOnlySpan.Length && (((c = (char)(*readOnlySpan[num25])) < '\u0080') ? (((int)"\0\0ကϿ\0\0\0\0"[(int)(c >> 4)] & (1 << (int)(c & '\u000f'))) != 0) : RegexRunner.CharInClass(c, "\0\u0002\u0001,-\t")))
                {
                  num25++;
                }
                readOnlySpan = readOnlySpan.Slice(num25);
                num += num25;
                num6 = num;
                goto IL_02AB;
              }
              goto IL_040C;
              int num31;
              do
              {
                IL_04EA:
                base.Capture(4, num22, num);
                int num26 = num;
                int num27 = num;
                int num28 = readOnlySpan.IndexOf('\n');
                if (num28 < 0)
                {
                  num28 = readOnlySpan.Length;
                }
                readOnlySpan = readOnlySpan.Slice(num28);
                num += num28;
                int num29 = num;
                for (;;)
                {
                  int num30 = base.Crawlpos();
                  base.Capture(5, num26, num);
                  if (num >= inputSpan.Length - 1 && (num >= inputSpan.Length || *inputSpan[num] == 10))
                  {
                    goto IL_058A;
                  }
                  this.<TryMatchAtCurrentPosition>g__UncaptureUntil|2_0(num30);
                  if (<RegexGenerator_g>F2A86C3D8F8653D6C99BE1B8441A5DADEC6C855822CB294D49492A00C67E55137__Utilities.s_hasTimeout)
                  {
                    base.CheckTimeout();
                  }
                  if (num27 >= num29)
                  {
                    break;
                  }
                  num29 = (num = num29 - 1);
                  readOnlySpan = inputSpan.Slice(num);
                }
                if (<RegexGenerator_g>F2A86C3D8F8653D6C99BE1B8441A5DADEC6C855822CB294D49492A00C67E55137__Utilities.s_hasTimeout)
                {
                  base.CheckTimeout();
                }
                if (num31 == 0)
                {
                  goto IL_03EC;
                }
              }
              while (num31 != 1);
              this.<TryMatchAtCurrentPosition>g__UncaptureUntil|2_0(num4);
              if (<RegexGenerator_g>F2A86C3D8F8653D6C99BE1B8441A5DADEC6C855822CB294D49492A00C67E55137__Utilities.s_hasTimeout)
              {
                base.CheckTimeout();
              }
              if (num9 < num10)
              {
                num10 = (num = num10 - 1);
                readOnlySpan = inputSpan.Slice(num);
                goto IL_04C7;
              }
              goto IL_00FE;
              IL_03EC:
              if (<RegexGenerator_g>F2A86C3D8F8653D6C99BE1B8441A5DADEC6C855822CB294D49492A00C67E55137__Utilities.s_hasTimeout)
              {
                base.CheckTimeout();
              }
              if (num11 == 0)
              {
                goto IL_027B;
              }
              this.<TryMatchAtCurrentPosition>g__UncaptureUntil|2_0(this.runstack[--num12]);
              <RegexGenerator_g>F2A86C3D8F8653D6C99BE1B8441A5DADEC6C855822CB294D49492A00C67E55137__Utilities.StackPop(this.runstack, ref num12, out num8, out num7);
              if (<RegexGenerator_g>F2A86C3D8F8653D6C99BE1B8441A5DADEC6C855822CB294D49492A00C67E55137__Utilities.s_hasTimeout)
              {
                base.CheckTimeout();
              }
              if (num7 < num8)
              {
                num8 = (num = num8 - 1);
                readOnlySpan = inputSpan.Slice(num);
                goto IL_0390;
              }
              goto IL_03B0;
              IL_00FE:
              this.<TryMatchAtCurrentPosition>g__UncaptureUntil|2_0(num20);
              if (num18 >= 4)
              {
                break;
              }
              num18++;
              if (<RegexGenerator_g>F2A86C3D8F8653D6C99BE1B8441A5DADEC6C855822CB294D49492A00C67E55137__Utilities.s_hasTimeout)
              {
                base.CheckTimeout();
              }
              num = num19;
              readOnlySpan = inputSpan.Slice(num);
              if (!readOnlySpan.IsEmpty && !(((c = (char)(*readOnlySpan[0])) < '\u0080') ? (((int)"\uffff\uffff響ﰀ\uffff\uffff\uffff\uffff"[(int)(c >> 4)] & (1 << (int)(c & '\u000f'))) == 0) : (!RegexRunner.CharInClass(c, "\u0001\u0004\u0001+,-/\t"))))
              {
                num++;
                readOnlySpan = inputSpan.Slice(num);
                num19 = num;
                continue;
              }
              break;
              IL_040C:
              num = num23;
              readOnlySpan = inputSpan.Slice(num);
              this.<TryMatchAtCurrentPosition>g__UncaptureUntil|2_0(num24);
              if (readOnlySpan.IsEmpty || *readOnlySpan[0] != 46)
              {
                goto IL_00FE;
              }
              num++;
              readOnlySpan = inputSpan.Slice(num);
              num9 = num;
              int num32 = 0;
              while (num32 < readOnlySpan.Length && char.IsDigit((char)(*readOnlySpan[num32])))
              {
                num32++;
              }
              if (num32 == 0)
              {
                goto IL_00FE;
              }
              readOnlySpan = readOnlySpan.Slice(num32);
              num += num32;
              num10 = num;
              num9++;
              IL_04C7:
              num4 = base.Crawlpos();
              num31 = 1;
              goto IL_04EA;
              IL_0405:
              num31 = 0;
              goto IL_04EA;
              IL_03B0:
              if (--num11 >= 0)
              {
                num = this.runstack[--num12];
                this.<TryMatchAtCurrentPosition>g__UncaptureUntil|2_0(this.runstack[--num12]);
                readOnlySpan = inputSpan.Slice(num);
                goto IL_0405;
              }
              goto IL_027B;
              IL_02B6:
              <RegexGenerator_g>F2A86C3D8F8653D6C99BE1B8441A5DADEC6C855822CB294D49492A00C67E55137__Utilities.StackPush(ref this.runstack, ref num12, base.Crawlpos(), num);
              num11++;
              if (readOnlySpan.IsEmpty || *readOnlySpan[0] != 46)
              {
                goto IL_03B0;
              }
              num++;
              readOnlySpan = inputSpan.Slice(num);
              num7 = num;
              int num33 = 0;
              while (num33 < readOnlySpan.Length && char.IsDigit((char)(*readOnlySpan[num33])))
              {
                num33++;
              }
              if (num33 != 0)
              {
                readOnlySpan = readOnlySpan.Slice(num33);
                num += num33;
                num8 = num;
                num7++;
                goto IL_0390;
              }
              goto IL_03B0;
              IL_02AB:
              num3 = base.Crawlpos();
              num11 = 0;
              goto IL_02B6;
              IL_0390:
              <RegexGenerator_g>F2A86C3D8F8653D6C99BE1B8441A5DADEC6C855822CB294D49492A00C67E55137__Utilities.StackPush(ref this.runstack, ref num12, num7, num8, base.Crawlpos());
              if (num11 == 0)
              {
                goto IL_02B6;
              }
              goto IL_0405;
              IL_027B:
              this.<TryMatchAtCurrentPosition>g__UncaptureUntil|2_0(num3);
              if (<RegexGenerator_g>F2A86C3D8F8653D6C99BE1B8441A5DADEC6C855822CB294D49492A00C67E55137__Utilities.s_hasTimeout)
              {
                base.CheckTimeout();
              }
              if (num5 < num6)
              {
                num6 = (num = num6 - 1);
                readOnlySpan = inputSpan.Slice(num);
                goto IL_02AB;
              }
              goto IL_040C;
            }
            this.<TryMatchAtCurrentPosition>g__UncaptureUntil|2_0(num16);
            if (<RegexGenerator_g>F2A86C3D8F8653D6C99BE1B8441A5DADEC6C855822CB294D49492A00C67E55137__Utilities.s_hasTimeout)
            {
              base.CheckTimeout();
            }
            if (num14 >= num15)
            {
              break;
            }
            num15 = (num = num15 - 1);
            readOnlySpan = inputSpan.Slice(num);
          }
          this.<TryMatchAtCurrentPosition>g__UncaptureUntil|2_0(0);
          return false;
          IL_058A:
          this.runtextpos = num;
          base.Capture(0, num2, num);
          return true;
        }

        // Token: 0x06003AB3 RID: 15027 RVA: 0x0045F054 File Offset: 0x0045F054
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
