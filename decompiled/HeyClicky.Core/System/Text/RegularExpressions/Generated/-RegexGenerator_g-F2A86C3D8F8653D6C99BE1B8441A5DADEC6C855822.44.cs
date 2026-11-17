using System;
using System.CodeDom.Compiler;
using System.Runtime.CompilerServices;

namespace System.Text.RegularExpressions.Generated
{
  // Token: 0x0200002D RID: 45
  [GeneratedCode("System.Text.RegularExpressions.Generator", "9.0.15.2218")]
  [SkipLocalsInit]
  internal sealed class <RegexGenerator_g>F2A86C3D8F8653D6C99BE1B8441A5DADEC6C855822CB294D49492A00C67E55137__TimedLineRegex_43 : Regex
  {
    // Token: 0x06000057 RID: 87 RVA: 0x003322D4 File Offset: 0x003322D4
    private <RegexGenerator_g>F2A86C3D8F8653D6C99BE1B8441A5DADEC6C855822CB294D49492A00C67E55137__TimedLineRegex_43()
    {
      this.pattern = "^((?:\\d{1,2}(?::\\d{2})?\\s*(?:am|pm|AM|PM)?)(?:\\s*[–-]\\s*\\d{1,2}(?::\\d{2})?\\s*(?:am|pm|AM|PM)?)?|(?:Morning|Afternoon|Evening|Night|Lunch|Dinner|Breakfast)(?=\\s*[:·—-]))\\s*[:·—-]?\\s+";
      this.roptions = RegexOptions.None;
      Regex.ValidateMatchTimeout(<RegexGenerator_g>F2A86C3D8F8653D6C99BE1B8441A5DADEC6C855822CB294D49492A00C67E55137__Utilities.s_defaultTimeout);
      this.internalMatchTimeout = <RegexGenerator_g>F2A86C3D8F8653D6C99BE1B8441A5DADEC6C855822CB294D49492A00C67E55137__Utilities.s_defaultTimeout;
      this.factory = new <RegexGenerator_g>F2A86C3D8F8653D6C99BE1B8441A5DADEC6C855822CB294D49492A00C67E55137__TimedLineRegex_43.RunnerFactory();
      this.capsize = 2;
    }

    // Token: 0x0400002C RID: 44
    [Nullable(1)]
    internal static readonly <RegexGenerator_g>F2A86C3D8F8653D6C99BE1B8441A5DADEC6C855822CB294D49492A00C67E55137__TimedLineRegex_43 Instance = new <RegexGenerator_g>F2A86C3D8F8653D6C99BE1B8441A5DADEC6C855822CB294D49492A00C67E55137__TimedLineRegex_43();

    // Token: 0x02000260 RID: 608
    private sealed class RunnerFactory : RegexRunnerFactory
    {
      // Token: 0x0600214A RID: 8522 RVA: 0x003F6F50 File Offset: 0x003F6F50
      [NullableContext(1)]
      protected override RegexRunner CreateInstance()
      {
        return new <RegexGenerator_g>F2A86C3D8F8653D6C99BE1B8441A5DADEC6C855822CB294D49492A00C67E55137__TimedLineRegex_43.RunnerFactory.Runner();
      }

      // Token: 0x020006CA RID: 1738
      private sealed class Runner : RegexRunner
      {
        // Token: 0x06003AD2 RID: 15058 RVA: 0x00460318 File Offset: 0x00460318
        protected override void Scan(ReadOnlySpan<char> inputSpan)
        {
          if (this.TryFindNextPossibleStartingPosition(inputSpan) && !this.TryMatchAtCurrentPosition(inputSpan))
          {
            this.runtextpos = inputSpan.Length;
          }
        }

        // Token: 0x06003AD3 RID: 15059 RVA: 0x0046033C File Offset: 0x0046033C
        private bool TryFindNextPossibleStartingPosition(ReadOnlySpan<char> inputSpan)
        {
          int runtextpos = this.runtextpos;
          if (runtextpos <= inputSpan.Length - 2 && runtextpos == 0)
          {
            return true;
          }
          this.runtextpos = inputSpan.Length;
          return false;
        }

        // Token: 0x06003AD4 RID: 15060 RVA: 0x00460370 File Offset: 0x00460370
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
          int num13 = 0;
          int num14 = 0;
          int num15 = 0;
          ReadOnlySpan<char> readOnlySpan = inputSpan.Slice(num);
          if (num != 0)
          {
            this.<TryMatchAtCurrentPosition>g__UncaptureUntil|2_0(0);
            return false;
          }
          int num16 = num;
          int num17 = num;
          int num18 = base.Crawlpos();
          int num19 = num;
          int num20 = 0;
          while (num20 < 2 && num20 < readOnlySpan.Length && char.IsDigit((char)(*readOnlySpan[num20])))
          {
            num20++;
          }
          if (num20 != 0)
          {
            readOnlySpan = readOnlySpan.Slice(num20);
            num += num20;
            num5 = num;
            num19++;
            goto IL_00F2;
          }
          goto IL_080D;
          char c;
          int num25;
          int num26;
          for (;;)
          {
            IL_0AB5:
            base.Capture(1, num16, num);
            int num21 = num;
            int num22 = 0;
            while (num22 < readOnlySpan.Length && char.IsWhiteSpace((char)(*readOnlySpan[num22])))
            {
              num22++;
            }
            readOnlySpan = readOnlySpan.Slice(num22);
            num += num22;
            int num23 = num;
            for (;;)
            {
              int num24 = base.Crawlpos();
              if (!readOnlySpan.IsEmpty && (((c = (char)(*readOnlySpan[0])) < '\u0080') ? (((int)"\0\0\u2000Ѐ\0\0\0\0"[(int)(c >> 4)] & (1 << (int)(c & '\u000f'))) != 0) : RegexRunner.CharInClass(c, "\0\b\0-.:;·\u00b8—―")))
              {
                readOnlySpan = readOnlySpan.Slice(1);
                num++;
              }
              num25 = 0;
              while (num25 < readOnlySpan.Length && char.IsWhiteSpace((char)(*readOnlySpan[num25])))
              {
                num25++;
              }
              if (num25 != 0)
              {
                goto Block_96;
              }
              this.<TryMatchAtCurrentPosition>g__UncaptureUntil|2_0(num24);
              if (<RegexGenerator_g>F2A86C3D8F8653D6C99BE1B8441A5DADEC6C855822CB294D49492A00C67E55137__Utilities.s_hasTimeout)
              {
                base.CheckTimeout();
              }
              if (num21 >= num23)
              {
                break;
              }
              num23 = (num = num23 - 1);
              readOnlySpan = inputSpan.Slice(num);
            }
            if (<RegexGenerator_g>F2A86C3D8F8653D6C99BE1B8441A5DADEC6C855822CB294D49492A00C67E55137__Utilities.s_hasTimeout)
            {
              base.CheckTimeout();
            }
            if (num26 == 0)
            {
              break;
            }
            if (num26 == 1)
            {
              goto Block_88;
            }
          }
          if (<RegexGenerator_g>F2A86C3D8F8653D6C99BE1B8441A5DADEC6C855822CB294D49492A00C67E55137__Utilities.s_hasTimeout)
          {
            base.CheckTimeout();
          }
          if (num14 == 0)
          {
            goto IL_031B;
          }
          int num27 = this.runstack[--num15];
          if (<RegexGenerator_g>F2A86C3D8F8653D6C99BE1B8441A5DADEC6C855822CB294D49492A00C67E55137__Utilities.s_hasTimeout)
          {
            base.CheckTimeout();
            goto IL_0741;
          }
          goto IL_0741;
          Block_88:
          this.<TryMatchAtCurrentPosition>g__UncaptureUntil|2_0(0);
          return false;
          Block_96:
          readOnlySpan = readOnlySpan.Slice(num25);
          num += num25;
          this.runtextpos = num;
          base.Capture(0, num2, num);
          return true;
          IL_00F2:
          num3 = base.Crawlpos();
          num12 = 0;
          for (;;)
          {
            <RegexGenerator_g>F2A86C3D8F8653D6C99BE1B8441A5DADEC6C855822CB294D49492A00C67E55137__Utilities.StackPush(ref this.runstack, ref num15, base.Crawlpos(), num);
            num12++;
            if (readOnlySpan.Length < 3 || *readOnlySpan[0] != 58 || !char.IsDigit((char)(*readOnlySpan[1])) || !char.IsDigit((char)(*readOnlySpan[2])))
            {
              break;
            }
            num += 3;
            readOnlySpan = inputSpan.Slice(num);
            if (num12 != 0)
            {
              goto IL_019C;
            }
          }
          IL_0162:
          if (--num12 >= 0)
          {
            num = this.runstack[--num15];
            this.<TryMatchAtCurrentPosition>g__UncaptureUntil|2_0(this.runstack[--num15]);
            readOnlySpan = inputSpan.Slice(num);
          }
          else
          {
            this.<TryMatchAtCurrentPosition>g__UncaptureUntil|2_0(num3);
            if (<RegexGenerator_g>F2A86C3D8F8653D6C99BE1B8441A5DADEC6C855822CB294D49492A00C67E55137__Utilities.s_hasTimeout)
            {
              base.CheckTimeout();
            }
            if (num19 < num5)
            {
              num5 = (num = num5 - 1);
              readOnlySpan = inputSpan.Slice(num);
              goto IL_00F2;
            }
            goto IL_080D;
          }
          IL_019C:
          num6 = num;
          int num28 = 0;
          while (num28 < readOnlySpan.Length && char.IsWhiteSpace((char)(*readOnlySpan[num28])))
          {
            num28++;
          }
          readOnlySpan = readOnlySpan.Slice(num28);
          num += num28;
          num7 = num;
          IL_020B:
          num4 = base.Crawlpos();
          num13 = 0;
          char c2;
          for (;;)
          {
            <RegexGenerator_g>F2A86C3D8F8653D6C99BE1B8441A5DADEC6C855822CB294D49492A00C67E55137__Utilities.StackPush(ref this.runstack, ref num15, base.Crawlpos(), num);
            num13++;
            if (readOnlySpan.IsEmpty)
            {
              break;
            }
            c2 = (char)(*readOnlySpan[0]);
            if (c2 <= 'P')
            {
              if (c2 != 'A')
              {
                if (c2 != 'P')
                {
                  break;
                }
                if (readOnlySpan.Length < 2 || *readOnlySpan[1] != 77)
                {
                  break;
                }
                num += 2;
                readOnlySpan = inputSpan.Slice(num);
              }
              else
              {
                if (readOnlySpan.Length < 2 || *readOnlySpan[1] != 77)
                {
                  break;
                }
                num += 2;
                readOnlySpan = inputSpan.Slice(num);
              }
            }
            else if (c2 != 'a')
            {
              if (c2 != 'p')
              {
                break;
              }
              if (readOnlySpan.Length < 2 || *readOnlySpan[1] != 109)
              {
                break;
              }
              num += 2;
              readOnlySpan = inputSpan.Slice(num);
            }
            else
            {
              if (readOnlySpan.Length < 2 || *readOnlySpan[1] != 109)
              {
                break;
              }
              num += 2;
              readOnlySpan = inputSpan.Slice(num);
            }
            if (num13 != 0)
            {
              goto IL_0355;
            }
          }
          IL_031B:
          if (--num13 >= 0)
          {
            num = this.runstack[--num15];
            this.<TryMatchAtCurrentPosition>g__UncaptureUntil|2_0(this.runstack[--num15]);
            readOnlySpan = inputSpan.Slice(num);
          }
          else
          {
            this.<TryMatchAtCurrentPosition>g__UncaptureUntil|2_0(num4);
            if (<RegexGenerator_g>F2A86C3D8F8653D6C99BE1B8441A5DADEC6C855822CB294D49492A00C67E55137__Utilities.s_hasTimeout)
            {
              base.CheckTimeout();
            }
            if (num6 < num7)
            {
              num7 = (num = num7 - 1);
              readOnlySpan = inputSpan.Slice(num);
              goto IL_020B;
            }
            goto IL_0162;
          }
          IL_0355:
          num14 = 0;
          IL_0358:
          <RegexGenerator_g>F2A86C3D8F8653D6C99BE1B8441A5DADEC6C855822CB294D49492A00C67E55137__Utilities.StackPush(ref this.runstack, ref num15, base.Crawlpos(), num);
          num14++;
          int num29 = 0;
          while (num29 < readOnlySpan.Length && char.IsWhiteSpace((char)(*readOnlySpan[num29])))
          {
            num29++;
          }
          readOnlySpan = readOnlySpan.Slice(num29);
          num += num29;
          if (readOnlySpan.IsEmpty || (((c = (char)(*readOnlySpan[0])) != '-') & (c != '–')))
          {
            goto IL_07B4;
          }
          int num30 = 1;
          while (num30 < readOnlySpan.Length && char.IsWhiteSpace((char)(*readOnlySpan[num30])))
          {
            num30++;
          }
          readOnlySpan = readOnlySpan.Slice(num30);
          num += num30;
          num8 = num;
          int num31 = 0;
          while (num31 < 2 && num31 < readOnlySpan.Length && char.IsDigit((char)(*readOnlySpan[num31])))
          {
            num31++;
          }
          if (num31 == 0)
          {
            goto IL_07B4;
          }
          readOnlySpan = readOnlySpan.Slice(num31);
          num += num31;
          num9 = num;
          num8++;
          IL_04AD:
          <RegexGenerator_g>F2A86C3D8F8653D6C99BE1B8441A5DADEC6C855822CB294D49492A00C67E55137__Utilities.StackPush(ref this.runstack, ref num15, num8, num9, base.Crawlpos());
          int num32 = 0;
          for (;;)
          {
            <RegexGenerator_g>F2A86C3D8F8653D6C99BE1B8441A5DADEC6C855822CB294D49492A00C67E55137__Utilities.StackPush(ref this.runstack, ref num15, base.Crawlpos(), num);
            num32++;
            if (readOnlySpan.Length < 3 || *readOnlySpan[0] != 58 || !char.IsDigit((char)(*readOnlySpan[1])) || !char.IsDigit((char)(*readOnlySpan[2])))
            {
              break;
            }
            num += 3;
            readOnlySpan = inputSpan.Slice(num);
            if (num32 != 0)
            {
              goto IL_0566;
            }
          }
          IL_052C:
          if (--num32 >= 0)
          {
            num = this.runstack[--num15];
            this.<TryMatchAtCurrentPosition>g__UncaptureUntil|2_0(this.runstack[--num15]);
            readOnlySpan = inputSpan.Slice(num);
          }
          else
          {
            this.<TryMatchAtCurrentPosition>g__UncaptureUntil|2_0(this.runstack[--num15]);
            <RegexGenerator_g>F2A86C3D8F8653D6C99BE1B8441A5DADEC6C855822CB294D49492A00C67E55137__Utilities.StackPop(this.runstack, ref num15, out num9, out num8);
            if (<RegexGenerator_g>F2A86C3D8F8653D6C99BE1B8441A5DADEC6C855822CB294D49492A00C67E55137__Utilities.s_hasTimeout)
            {
              base.CheckTimeout();
            }
            if (num8 < num9)
            {
              num9 = (num = num9 - 1);
              readOnlySpan = inputSpan.Slice(num);
              goto IL_04AD;
            }
            goto IL_07B4;
          }
          IL_0566:
          <RegexGenerator_g>F2A86C3D8F8653D6C99BE1B8441A5DADEC6C855822CB294D49492A00C67E55137__Utilities.StackPush(ref this.runstack, ref num15, num32);
          num10 = num;
          int num33 = 0;
          while (num33 < readOnlySpan.Length && char.IsWhiteSpace((char)(*readOnlySpan[num33])))
          {
            num33++;
          }
          readOnlySpan = readOnlySpan.Slice(num33);
          num += num33;
          num11 = num;
          IL_0622:
          <RegexGenerator_g>F2A86C3D8F8653D6C99BE1B8441A5DADEC6C855822CB294D49492A00C67E55137__Utilities.StackPush(ref this.runstack, ref num15, num10, num11, base.Crawlpos());
          num27 = 0;
          for (;;)
          {
            <RegexGenerator_g>F2A86C3D8F8653D6C99BE1B8441A5DADEC6C855822CB294D49492A00C67E55137__Utilities.StackPush(ref this.runstack, ref num15, base.Crawlpos(), num);
            num27++;
            if (readOnlySpan.IsEmpty)
            {
              break;
            }
            c2 = (char)(*readOnlySpan[0]);
            if (c2 <= 'P')
            {
              if (c2 != 'A')
              {
                if (c2 != 'P')
                {
                  break;
                }
                if (readOnlySpan.Length < 2 || *readOnlySpan[1] != 77)
                {
                  break;
                }
                num += 2;
                readOnlySpan = inputSpan.Slice(num);
              }
              else
              {
                if (readOnlySpan.Length < 2 || *readOnlySpan[1] != 77)
                {
                  break;
                }
                num += 2;
                readOnlySpan = inputSpan.Slice(num);
              }
            }
            else if (c2 != 'a')
            {
              if (c2 != 'p')
              {
                break;
              }
              if (readOnlySpan.Length < 2 || *readOnlySpan[1] != 109)
              {
                break;
              }
              num += 2;
              readOnlySpan = inputSpan.Slice(num);
            }
            else
            {
              if (readOnlySpan.Length < 2 || *readOnlySpan[1] != 109)
              {
                break;
              }
              num += 2;
              readOnlySpan = inputSpan.Slice(num);
            }
            if (num27 != 0)
            {
              goto IL_077B;
            }
          }
          IL_0741:
          if (--num27 >= 0)
          {
            num = this.runstack[--num15];
            this.<TryMatchAtCurrentPosition>g__UncaptureUntil|2_0(this.runstack[--num15]);
            readOnlySpan = inputSpan.Slice(num);
          }
          else
          {
            this.<TryMatchAtCurrentPosition>g__UncaptureUntil|2_0(this.runstack[--num15]);
            <RegexGenerator_g>F2A86C3D8F8653D6C99BE1B8441A5DADEC6C855822CB294D49492A00C67E55137__Utilities.StackPop(this.runstack, ref num15, out num11, out num10);
            if (<RegexGenerator_g>F2A86C3D8F8653D6C99BE1B8441A5DADEC6C855822CB294D49492A00C67E55137__Utilities.s_hasTimeout)
            {
              base.CheckTimeout();
            }
            if (num10 < num11)
            {
              num11 = (num = num11 - 1);
              readOnlySpan = inputSpan.Slice(num);
              goto IL_0622;
            }
            num32 = this.runstack[--num15];
            if (<RegexGenerator_g>F2A86C3D8F8653D6C99BE1B8441A5DADEC6C855822CB294D49492A00C67E55137__Utilities.s_hasTimeout)
            {
              base.CheckTimeout();
              goto IL_052C;
            }
            goto IL_052C;
          }
          IL_077B:
          <RegexGenerator_g>F2A86C3D8F8653D6C99BE1B8441A5DADEC6C855822CB294D49492A00C67E55137__Utilities.StackPush(ref this.runstack, ref num15, num27);
          if (num14 == 0)
          {
            goto IL_0358;
          }
          goto IL_0806;
          IL_07B4:
          if (--num14 < 0)
          {
            goto IL_031B;
          }
          num = this.runstack[--num15];
          this.<TryMatchAtCurrentPosition>g__UncaptureUntil|2_0(this.runstack[--num15]);
          readOnlySpan = inputSpan.Slice(num);
          IL_0806:
          num26 = 0;
          goto IL_0AB5;
          IL_080D:
          num = num17;
          readOnlySpan = inputSpan.Slice(num);
          this.<TryMatchAtCurrentPosition>g__UncaptureUntil|2_0(num18);
          if (readOnlySpan.IsEmpty)
          {
            this.<TryMatchAtCurrentPosition>g__UncaptureUntil|2_0(0);
            return false;
          }
          c2 = (char)(*readOnlySpan[0]);
          switch (c2)
          {
          case 'A':
            if (!readOnlySpan.Slice(1).StartsWith<char>("fternoon"))
            {
              this.<TryMatchAtCurrentPosition>g__UncaptureUntil|2_0(0);
              return false;
            }
            num += 9;
            readOnlySpan = inputSpan.Slice(num);
            goto IL_09ED;
          case 'B':
            if (!readOnlySpan.Slice(1).StartsWith<char>("reakfast"))
            {
              this.<TryMatchAtCurrentPosition>g__UncaptureUntil|2_0(0);
              return false;
            }
            num += 9;
            readOnlySpan = inputSpan.Slice(num);
            goto IL_09ED;
          case 'C':
            break;
          case 'D':
            if (!readOnlySpan.Slice(1).StartsWith<char>("inner"))
            {
              this.<TryMatchAtCurrentPosition>g__UncaptureUntil|2_0(0);
              return false;
            }
            num += 6;
            readOnlySpan = inputSpan.Slice(num);
            goto IL_09ED;
          case 'E':
            if (!readOnlySpan.Slice(1).StartsWith<char>("vening"))
            {
              this.<TryMatchAtCurrentPosition>g__UncaptureUntil|2_0(0);
              return false;
            }
            num += 7;
            readOnlySpan = inputSpan.Slice(num);
            goto IL_09ED;
          default:
            switch (c2)
            {
            case 'L':
              if (!readOnlySpan.Slice(1).StartsWith<char>("unch"))
              {
                this.<TryMatchAtCurrentPosition>g__UncaptureUntil|2_0(0);
                return false;
              }
              num += 5;
              readOnlySpan = inputSpan.Slice(num);
              goto IL_09ED;
            case 'M':
              if (!readOnlySpan.Slice(1).StartsWith<char>("orning"))
              {
                this.<TryMatchAtCurrentPosition>g__UncaptureUntil|2_0(0);
                return false;
              }
              num += 7;
              readOnlySpan = inputSpan.Slice(num);
              goto IL_09ED;
            case 'N':
              if (!readOnlySpan.Slice(1).StartsWith<char>("ight"))
              {
                this.<TryMatchAtCurrentPosition>g__UncaptureUntil|2_0(0);
                return false;
              }
              num += 5;
              readOnlySpan = inputSpan.Slice(num);
              goto IL_09ED;
            }
            break;
          }
          this.<TryMatchAtCurrentPosition>g__UncaptureUntil|2_0(0);
          return false;
          IL_09ED:
          int num34 = num;
          if (<RegexGenerator_g>F2A86C3D8F8653D6C99BE1B8441A5DADEC6C855822CB294D49492A00C67E55137__Utilities.s_hasTimeout)
          {
            base.CheckTimeout();
          }
          int num35 = 0;
          while (num35 < readOnlySpan.Length && char.IsWhiteSpace((char)(*readOnlySpan[num35])))
          {
            num35++;
          }
          readOnlySpan = readOnlySpan.Slice(num35);
          num += num35;
          if (readOnlySpan.IsEmpty || (((c = (char)(*readOnlySpan[0])) < '\u0080') ? (((int)"\0\0\u2000Ѐ\0\0\0\0"[(int)(c >> 4)] & (1 << (int)(c & '\u000f'))) == 0) : (!RegexRunner.CharInClass(c, "\0\b\0-.:;·\u00b8—―"))))
          {
            this.<TryMatchAtCurrentPosition>g__UncaptureUntil|2_0(0);
            return false;
          }
          num = num34;
          readOnlySpan = inputSpan.Slice(num);
          num26 = 1;
          goto IL_0AB5;
        }

        // Token: 0x06003AD6 RID: 15062 RVA: 0x00460F5C File Offset: 0x00460F5C
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
