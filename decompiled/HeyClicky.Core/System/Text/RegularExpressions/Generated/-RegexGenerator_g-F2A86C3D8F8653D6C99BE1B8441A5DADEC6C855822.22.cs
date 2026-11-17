using System;
using System.CodeDom.Compiler;
using System.Runtime.CompilerServices;

namespace System.Text.RegularExpressions.Generated
{
  // Token: 0x02000017 RID: 23
  [GeneratedCode("System.Text.RegularExpressions.Generator", "9.0.15.2218")]
  [SkipLocalsInit]
  internal sealed class <RegexGenerator_g>F2A86C3D8F8653D6C99BE1B8441A5DADEC6C855822CB294D49492A00C67E55137__DirectLengthPattern_21 : Regex
  {
    // Token: 0x0600002B RID: 43 RVA: 0x00331B3C File Offset: 0x00331B3C
    private <RegexGenerator_g>F2A86C3D8F8653D6C99BE1B8441A5DADEC6C855822CB294D49492A00C67E55137__DirectLengthPattern_21()
    {
      this.pattern = "/Length\\s+(\\d+)(?!\\s+\\d+\\s+R)";
      this.roptions = RegexOptions.None;
      Regex.ValidateMatchTimeout(<RegexGenerator_g>F2A86C3D8F8653D6C99BE1B8441A5DADEC6C855822CB294D49492A00C67E55137__Utilities.s_defaultTimeout);
      this.internalMatchTimeout = <RegexGenerator_g>F2A86C3D8F8653D6C99BE1B8441A5DADEC6C855822CB294D49492A00C67E55137__Utilities.s_defaultTimeout;
      this.factory = new <RegexGenerator_g>F2A86C3D8F8653D6C99BE1B8441A5DADEC6C855822CB294D49492A00C67E55137__DirectLengthPattern_21.RunnerFactory();
      this.capsize = 2;
    }

    // Token: 0x04000016 RID: 22
    [Nullable(1)]
    internal static readonly <RegexGenerator_g>F2A86C3D8F8653D6C99BE1B8441A5DADEC6C855822CB294D49492A00C67E55137__DirectLengthPattern_21 Instance = new <RegexGenerator_g>F2A86C3D8F8653D6C99BE1B8441A5DADEC6C855822CB294D49492A00C67E55137__DirectLengthPattern_21();

    // Token: 0x0200024A RID: 586
    private sealed class RunnerFactory : RegexRunnerFactory
    {
      // Token: 0x0600211E RID: 8478 RVA: 0x003F6DF0 File Offset: 0x003F6DF0
      [NullableContext(1)]
      protected override RegexRunner CreateInstance()
      {
        return new <RegexGenerator_g>F2A86C3D8F8653D6C99BE1B8441A5DADEC6C855822CB294D49492A00C67E55137__DirectLengthPattern_21.RunnerFactory.Runner();
      }

      // Token: 0x020006B4 RID: 1716
      private sealed class Runner : RegexRunner
      {
        // Token: 0x06003A68 RID: 14952 RVA: 0x0045CAAC File Offset: 0x0045CAAC
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

        // Token: 0x06003A69 RID: 14953 RVA: 0x0045CAEC File Offset: 0x0045CAEC
        private bool TryFindNextPossibleStartingPosition(ReadOnlySpan<char> inputSpan)
        {
          int runtextpos = this.runtextpos;
          if (runtextpos <= inputSpan.Length - 9)
          {
            int num = inputSpan.Slice(runtextpos).IndexOfAny(<RegexGenerator_g>F2A86C3D8F8653D6C99BE1B8441A5DADEC6C855822CB294D49492A00C67E55137__Utilities.s_indexOfString_921E30F6B7332314FAB4E2ECB69027ECA9BA90F395F673D24018A15794472969);
            if (num >= 0)
            {
              this.runtextpos = runtextpos + num;
              return true;
            }
          }
          this.runtextpos = inputSpan.Length;
          return false;
        }

        // Token: 0x06003A6A RID: 14954 RVA: 0x0045CB40 File Offset: 0x0045CB40
        private unsafe bool TryMatchAtCurrentPosition(ReadOnlySpan<char> inputSpan)
        {
          int num = this.runtextpos;
          int num2 = num;
          ReadOnlySpan<char> readOnlySpan = inputSpan.Slice(num);
          if (!readOnlySpan.StartsWith<char>("/Length"))
          {
            this.<TryMatchAtCurrentPosition>g__UncaptureUntil|2_0(0);
            return false;
          }
          num += 7;
          readOnlySpan = inputSpan.Slice(num);
          int num3 = 0;
          while (num3 < readOnlySpan.Length && char.IsWhiteSpace((char)(*readOnlySpan[num3])))
          {
            num3++;
          }
          if (num3 == 0)
          {
            this.<TryMatchAtCurrentPosition>g__UncaptureUntil|2_0(0);
            return false;
          }
          readOnlySpan = readOnlySpan.Slice(num3);
          num += num3;
          int num4 = num;
          int num5 = num;
          int num6 = 0;
          while (num6 < readOnlySpan.Length && char.IsDigit((char)(*readOnlySpan[num6])))
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
          int num7 = num;
          num5++;
          int num9;
          for (;;)
          {
            int num8 = base.Crawlpos();
            base.Capture(1, num4, num);
            num9 = num;
            if (<RegexGenerator_g>F2A86C3D8F8653D6C99BE1B8441A5DADEC6C855822CB294D49492A00C67E55137__Utilities.s_hasTimeout)
            {
              base.CheckTimeout();
            }
            int num10 = 0;
            while (num10 < readOnlySpan.Length && char.IsWhiteSpace((char)(*readOnlySpan[num10])))
            {
              num10++;
            }
            if (num10 == 0)
            {
              goto IL_0201;
            }
            readOnlySpan = readOnlySpan.Slice(num10);
            num += num10;
            int num11 = 0;
            while (num11 < readOnlySpan.Length && char.IsDigit((char)(*readOnlySpan[num11])))
            {
              num11++;
            }
            if (num11 == 0)
            {
              goto IL_0201;
            }
            readOnlySpan = readOnlySpan.Slice(num11);
            num += num11;
            int num12 = 0;
            while (num12 < readOnlySpan.Length && char.IsWhiteSpace((char)(*readOnlySpan[num12])))
            {
              num12++;
            }
            if (num12 == 0)
            {
              goto IL_0201;
            }
            readOnlySpan = readOnlySpan.Slice(num12);
            num += num12;
            if (readOnlySpan.IsEmpty || *readOnlySpan[0] != 82)
            {
              goto IL_0201;
            }
            this.<TryMatchAtCurrentPosition>g__UncaptureUntil|2_0(num8);
            if (<RegexGenerator_g>F2A86C3D8F8653D6C99BE1B8441A5DADEC6C855822CB294D49492A00C67E55137__Utilities.s_hasTimeout)
            {
              base.CheckTimeout();
            }
            if (num5 >= num7)
            {
              break;
            }
            num7 = (num = num7 - 1);
            readOnlySpan = inputSpan.Slice(num);
          }
          this.<TryMatchAtCurrentPosition>g__UncaptureUntil|2_0(0);
          return false;
          IL_0201:
          num = num9;
          readOnlySpan = inputSpan.Slice(num);
          this.runtextpos = num;
          base.Capture(0, num2, num);
          return true;
        }

        // Token: 0x06003A6C RID: 14956 RVA: 0x0045CD74 File Offset: 0x0045CD74
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
