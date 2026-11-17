using System;
using System.CodeDom.Compiler;
using System.Runtime.CompilerServices;

namespace System.Text.RegularExpressions.Generated
{
  // Token: 0x02000009 RID: 9
  [GeneratedCode("System.Text.RegularExpressions.Generator", "9.0.15.2218")]
  [SkipLocalsInit]
  internal sealed class <RegexGenerator_g>F2A86C3D8F8653D6C99BE1B8441A5DADEC6C855822CB294D49492A00C67E55137__WordTextOrBreak_7 : Regex
  {
    // Token: 0x0600000F RID: 15 RVA: 0x0033165C File Offset: 0x0033165C
    private <RegexGenerator_g>F2A86C3D8F8653D6C99BE1B8441A5DADEC6C855822CB294D49492A00C67E55137__WordTextOrBreak_7()
    {
      this.pattern = "<w:t(?:\\s[^>]*)?>([^<]*)</w:t>|\\n|\\t";
      this.roptions = RegexOptions.None;
      Regex.ValidateMatchTimeout(<RegexGenerator_g>F2A86C3D8F8653D6C99BE1B8441A5DADEC6C855822CB294D49492A00C67E55137__Utilities.s_defaultTimeout);
      this.internalMatchTimeout = <RegexGenerator_g>F2A86C3D8F8653D6C99BE1B8441A5DADEC6C855822CB294D49492A00C67E55137__Utilities.s_defaultTimeout;
      this.factory = new <RegexGenerator_g>F2A86C3D8F8653D6C99BE1B8441A5DADEC6C855822CB294D49492A00C67E55137__WordTextOrBreak_7.RunnerFactory();
      this.capsize = 2;
    }

    // Token: 0x04000008 RID: 8
    [Nullable(1)]
    internal static readonly <RegexGenerator_g>F2A86C3D8F8653D6C99BE1B8441A5DADEC6C855822CB294D49492A00C67E55137__WordTextOrBreak_7 Instance = new <RegexGenerator_g>F2A86C3D8F8653D6C99BE1B8441A5DADEC6C855822CB294D49492A00C67E55137__WordTextOrBreak_7();

    // Token: 0x0200023C RID: 572
    private sealed class RunnerFactory : RegexRunnerFactory
    {
      // Token: 0x06002102 RID: 8450 RVA: 0x003F6D10 File Offset: 0x003F6D10
      [NullableContext(1)]
      protected override RegexRunner CreateInstance()
      {
        return new <RegexGenerator_g>F2A86C3D8F8653D6C99BE1B8441A5DADEC6C855822CB294D49492A00C67E55137__WordTextOrBreak_7.RunnerFactory.Runner();
      }

      // Token: 0x020006A6 RID: 1702
      private sealed class Runner : RegexRunner
      {
        // Token: 0x06003A28 RID: 14888 RVA: 0x0045AE88 File Offset: 0x0045AE88
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

        // Token: 0x06003A29 RID: 14889 RVA: 0x0045AEC8 File Offset: 0x0045AEC8
        private bool TryFindNextPossibleStartingPosition(ReadOnlySpan<char> inputSpan)
        {
          int runtextpos = this.runtextpos;
          if (runtextpos < inputSpan.Length)
          {
            int num = inputSpan.Slice(runtextpos).IndexOfAny('\t', '\n', '<');
            if (num >= 0)
            {
              this.runtextpos = runtextpos + num;
              return true;
            }
          }
          this.runtextpos = inputSpan.Length;
          return false;
        }

        // Token: 0x06003A2A RID: 14890 RVA: 0x0045AF18 File Offset: 0x0045AF18
        private unsafe bool TryMatchAtCurrentPosition(ReadOnlySpan<char> inputSpan)
        {
          int num = this.runtextpos;
          int num2 = num;
          int num3 = 0;
          int num4 = 0;
          int num5 = 0;
          ReadOnlySpan<char> readOnlySpan = inputSpan.Slice(num);
          int num6 = num5;
          if (readOnlySpan.IsEmpty)
          {
            this.<TryMatchAtCurrentPosition>g__UncaptureUntil|2_0(0);
            return false;
          }
          char c = (char)(*readOnlySpan[0]);
          if (c != '\t' && c != '\n')
          {
            if (c != '<')
            {
              this.<TryMatchAtCurrentPosition>g__UncaptureUntil|2_0(0);
              return false;
            }
            if (!readOnlySpan.Slice(1).StartsWith<char>("w:t"))
            {
              this.<TryMatchAtCurrentPosition>g__UncaptureUntil|2_0(0);
              return false;
            }
            num += 4;
            readOnlySpan = inputSpan.Slice(num);
            int num7 = 0;
            for (;;)
            {
              <RegexGenerator_g>F2A86C3D8F8653D6C99BE1B8441A5DADEC6C855822CB294D49492A00C67E55137__Utilities.StackPush(ref this.runstack, ref num5, base.Crawlpos(), num);
              num7++;
              if (readOnlySpan.IsEmpty || !char.IsWhiteSpace((char)(*readOnlySpan[0])))
              {
                goto IL_016E;
              }
              num++;
              readOnlySpan = inputSpan.Slice(num);
              num3 = num;
              int num8 = readOnlySpan.IndexOf('>');
              if (num8 < 0)
              {
                num8 = readOnlySpan.Length;
              }
              readOnlySpan = readOnlySpan.Slice(num8);
              num += num8;
              num4 = num;
              IL_014F:
              <RegexGenerator_g>F2A86C3D8F8653D6C99BE1B8441A5DADEC6C855822CB294D49492A00C67E55137__Utilities.StackPush(ref this.runstack, ref num5, num3, num4, base.Crawlpos());
              if (num7 == 0)
              {
                continue;
              }
              IL_01CD:
              if (!readOnlySpan.IsEmpty && *readOnlySpan[0] == 62)
              {
                num++;
                readOnlySpan = inputSpan.Slice(num);
                int num9 = num;
                int num10 = readOnlySpan.IndexOf('<');
                if (num10 < 0)
                {
                  num10 = readOnlySpan.Length;
                }
                readOnlySpan = readOnlySpan.Slice(num10);
                num += num10;
                base.Capture(1, num9, num);
                if (readOnlySpan.StartsWith<char>("</w:t>"))
                {
                  goto Block_18;
                }
              }
              if (<RegexGenerator_g>F2A86C3D8F8653D6C99BE1B8441A5DADEC6C855822CB294D49492A00C67E55137__Utilities.s_hasTimeout)
              {
                base.CheckTimeout();
              }
              if (num7 == 0)
              {
                goto Block_14;
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
                goto IL_014F;
              }
              IL_016E:
              if (--num7 < 0)
              {
                break;
              }
              num = this.runstack[--num5];
              this.<TryMatchAtCurrentPosition>g__UncaptureUntil|2_0(this.runstack[--num5]);
              readOnlySpan = inputSpan.Slice(num);
              goto IL_01CD;
            }
            this.<TryMatchAtCurrentPosition>g__UncaptureUntil|2_0(0);
            return false;
            Block_14:
            this.<TryMatchAtCurrentPosition>g__UncaptureUntil|2_0(0);
            return false;
            Block_18:
            num += 6;
            readOnlySpan = inputSpan.Slice(num);
          }
          else
          {
            num++;
            readOnlySpan = inputSpan.Slice(num);
          }
          num5 = num6;
          this.runtextpos = num;
          base.Capture(0, num2, num);
          return true;
        }

        // Token: 0x06003A2C RID: 14892 RVA: 0x0045B1A8 File Offset: 0x0045B1A8
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
