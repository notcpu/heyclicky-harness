using System;
using System.CodeDom.Compiler;
using System.Runtime.CompilerServices;

namespace System.Text.RegularExpressions.Generated
{
  // Token: 0x02000010 RID: 16
  [GeneratedCode("System.Text.RegularExpressions.Generator", "9.0.15.2218")]
  [SkipLocalsInit]
  internal sealed class <RegexGenerator_g>F2A86C3D8F8653D6C99BE1B8441A5DADEC6C855822CB294D49492A00C67E55137__PlainTextRun_14 : Regex
  {
    // Token: 0x0600001D RID: 29 RVA: 0x003318CC File Offset: 0x003318CC
    private <RegexGenerator_g>F2A86C3D8F8653D6C99BE1B8441A5DADEC6C855822CB294D49492A00C67E55137__PlainTextRun_14()
    {
      this.pattern = "<t(?:\\s[^>]*)?>([^<]*)</t>";
      this.roptions = RegexOptions.None;
      Regex.ValidateMatchTimeout(<RegexGenerator_g>F2A86C3D8F8653D6C99BE1B8441A5DADEC6C855822CB294D49492A00C67E55137__Utilities.s_defaultTimeout);
      this.internalMatchTimeout = <RegexGenerator_g>F2A86C3D8F8653D6C99BE1B8441A5DADEC6C855822CB294D49492A00C67E55137__Utilities.s_defaultTimeout;
      this.factory = new <RegexGenerator_g>F2A86C3D8F8653D6C99BE1B8441A5DADEC6C855822CB294D49492A00C67E55137__PlainTextRun_14.RunnerFactory();
      this.capsize = 2;
    }

    // Token: 0x0400000F RID: 15
    [Nullable(1)]
    internal static readonly <RegexGenerator_g>F2A86C3D8F8653D6C99BE1B8441A5DADEC6C855822CB294D49492A00C67E55137__PlainTextRun_14 Instance = new <RegexGenerator_g>F2A86C3D8F8653D6C99BE1B8441A5DADEC6C855822CB294D49492A00C67E55137__PlainTextRun_14();

    // Token: 0x02000243 RID: 579
    private sealed class RunnerFactory : RegexRunnerFactory
    {
      // Token: 0x06002110 RID: 8464 RVA: 0x003F6D80 File Offset: 0x003F6D80
      [NullableContext(1)]
      protected override RegexRunner CreateInstance()
      {
        return new <RegexGenerator_g>F2A86C3D8F8653D6C99BE1B8441A5DADEC6C855822CB294D49492A00C67E55137__PlainTextRun_14.RunnerFactory.Runner();
      }

      // Token: 0x020006AD RID: 1709
      private sealed class Runner : RegexRunner
      {
        // Token: 0x06003A48 RID: 14920 RVA: 0x0045BB14 File Offset: 0x0045BB14
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

        // Token: 0x06003A49 RID: 14921 RVA: 0x0045BB54 File Offset: 0x0045BB54
        private bool TryFindNextPossibleStartingPosition(ReadOnlySpan<char> inputSpan)
        {
          int runtextpos = this.runtextpos;
          if (runtextpos <= inputSpan.Length - 7)
          {
            int num = inputSpan.Slice(runtextpos).IndexOfAny(<RegexGenerator_g>F2A86C3D8F8653D6C99BE1B8441A5DADEC6C855822CB294D49492A00C67E55137__Utilities.s_indexOfString_C81372FD91E31CDD0ABA0C9786D61E845D30428353FADE938E54007304B3525A);
            if (num >= 0)
            {
              this.runtextpos = runtextpos + num;
              return true;
            }
          }
          this.runtextpos = inputSpan.Length;
          return false;
        }

        // Token: 0x06003A4A RID: 14922 RVA: 0x0045BBA4 File Offset: 0x0045BBA4
        private unsafe bool TryMatchAtCurrentPosition(ReadOnlySpan<char> inputSpan)
        {
          int num = this.runtextpos;
          int num2 = num;
          int num3 = 0;
          int num4 = 0;
          int num5 = 0;
          ReadOnlySpan<char> readOnlySpan = inputSpan.Slice(num);
          if (!readOnlySpan.StartsWith<char>("<t"))
          {
            this.<TryMatchAtCurrentPosition>g__UncaptureUntil|2_0(0);
            return false;
          }
          num += 2;
          readOnlySpan = inputSpan.Slice(num);
          int num6 = 0;
          for (;;)
          {
            <RegexGenerator_g>F2A86C3D8F8653D6C99BE1B8441A5DADEC6C855822CB294D49492A00C67E55137__Utilities.StackPush(ref this.runstack, ref num5, base.Crawlpos(), num);
            num6++;
            if (readOnlySpan.IsEmpty || !char.IsWhiteSpace((char)(*readOnlySpan[0])))
            {
              goto IL_012C;
            }
            num++;
            readOnlySpan = inputSpan.Slice(num);
            num3 = num;
            int num7 = readOnlySpan.IndexOf('>');
            if (num7 < 0)
            {
              num7 = readOnlySpan.Length;
            }
            readOnlySpan = readOnlySpan.Slice(num7);
            num += num7;
            num4 = num;
            IL_010D:
            <RegexGenerator_g>F2A86C3D8F8653D6C99BE1B8441A5DADEC6C855822CB294D49492A00C67E55137__Utilities.StackPush(ref this.runstack, ref num5, num3, num4, base.Crawlpos());
            if (num6 == 0)
            {
              continue;
            }
            IL_018B:
            if (!readOnlySpan.IsEmpty && *readOnlySpan[0] == 62)
            {
              num++;
              readOnlySpan = inputSpan.Slice(num);
              int num8 = num;
              int num9 = readOnlySpan.IndexOf('<');
              if (num9 < 0)
              {
                num9 = readOnlySpan.Length;
              }
              readOnlySpan = readOnlySpan.Slice(num9);
              num += num9;
              base.Capture(1, num8, num);
              if (readOnlySpan.StartsWith<char>("</t>"))
              {
                goto Block_14;
              }
            }
            if (<RegexGenerator_g>F2A86C3D8F8653D6C99BE1B8441A5DADEC6C855822CB294D49492A00C67E55137__Utilities.s_hasTimeout)
            {
              base.CheckTimeout();
            }
            if (num6 == 0)
            {
              goto Block_10;
            }
            this.<TryMatchAtCurrentPosition>g__UncaptureUntil|2_0(this.runstack[--num5]);
            <RegexGenerator_g>F2A86C3D8F8653D6C99BE1B8441A5DADEC6C855822CB294D49492A00C67E55137__Utilities.StackPop(this.runstack, ref num5, out num4, out num3);
            if (<RegexGenerator_g>F2A86C3D8F8653D6C99BE1B8441A5DADEC6C855822CB294D49492A00C67E55137__Utilities.s_hasTimeout)
            {
              base.CheckTimeout();
            }
            if (num3 < num4)
            {
              num4 = (num = num4 - 1);
              readOnlySpan = inputSpan.Slice(num);
              goto IL_010D;
            }
            IL_012C:
            if (--num6 < 0)
            {
              break;
            }
            num = this.runstack[--num5];
            this.<TryMatchAtCurrentPosition>g__UncaptureUntil|2_0(this.runstack[--num5]);
            readOnlySpan = inputSpan.Slice(num);
            goto IL_018B;
          }
          this.<TryMatchAtCurrentPosition>g__UncaptureUntil|2_0(0);
          return false;
          Block_10:
          this.<TryMatchAtCurrentPosition>g__UncaptureUntil|2_0(0);
          return false;
          Block_14:
          num += 4;
          this.runtextpos = num;
          base.Capture(0, num2, num);
          return true;
        }

        // Token: 0x06003A4C RID: 14924 RVA: 0x0045BDC8 File Offset: 0x0045BDC8
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
