using System;
using System.CodeDom.Compiler;
using System.Runtime.CompilerServices;

namespace System.Text.RegularExpressions.Generated
{
  // Token: 0x0200002A RID: 42
  [GeneratedCode("System.Text.RegularExpressions.Generator", "9.0.15.2218")]
  [SkipLocalsInit]
  internal sealed class <RegexGenerator_g>F2A86C3D8F8653D6C99BE1B8441A5DADEC6C855822CB294D49492A00C67E55137__HighlightRegex_40 : Regex
  {
    // Token: 0x06000051 RID: 81 RVA: 0x003321CC File Offset: 0x003321CC
    private <RegexGenerator_g>F2A86C3D8F8653D6C99BE1B8441A5DADEC6C855822CB294D49492A00C67E55137__HighlightRegex_40()
    {
      this.pattern = "==(.+?)==";
      this.roptions = RegexOptions.None;
      Regex.ValidateMatchTimeout(<RegexGenerator_g>F2A86C3D8F8653D6C99BE1B8441A5DADEC6C855822CB294D49492A00C67E55137__Utilities.s_defaultTimeout);
      this.internalMatchTimeout = <RegexGenerator_g>F2A86C3D8F8653D6C99BE1B8441A5DADEC6C855822CB294D49492A00C67E55137__Utilities.s_defaultTimeout;
      this.factory = new <RegexGenerator_g>F2A86C3D8F8653D6C99BE1B8441A5DADEC6C855822CB294D49492A00C67E55137__HighlightRegex_40.RunnerFactory();
      this.capsize = 2;
    }

    // Token: 0x04000029 RID: 41
    [Nullable(1)]
    internal static readonly <RegexGenerator_g>F2A86C3D8F8653D6C99BE1B8441A5DADEC6C855822CB294D49492A00C67E55137__HighlightRegex_40 Instance = new <RegexGenerator_g>F2A86C3D8F8653D6C99BE1B8441A5DADEC6C855822CB294D49492A00C67E55137__HighlightRegex_40();

    // Token: 0x0200025D RID: 605
    private sealed class RunnerFactory : RegexRunnerFactory
    {
      // Token: 0x06002144 RID: 8516 RVA: 0x003F6F20 File Offset: 0x003F6F20
      [NullableContext(1)]
      protected override RegexRunner CreateInstance()
      {
        return new <RegexGenerator_g>F2A86C3D8F8653D6C99BE1B8441A5DADEC6C855822CB294D49492A00C67E55137__HighlightRegex_40.RunnerFactory.Runner();
      }

      // Token: 0x020006C7 RID: 1735
      private sealed class Runner : RegexRunner
      {
        // Token: 0x06003AC3 RID: 15043 RVA: 0x0045FB20 File Offset: 0x0045FB20
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

        // Token: 0x06003AC4 RID: 15044 RVA: 0x0045FB60 File Offset: 0x0045FB60
        private bool TryFindNextPossibleStartingPosition(ReadOnlySpan<char> inputSpan)
        {
          int runtextpos = this.runtextpos;
          if (runtextpos <= inputSpan.Length - 5)
          {
            int num = inputSpan.Slice(runtextpos).IndexOfAny(<RegexGenerator_g>F2A86C3D8F8653D6C99BE1B8441A5DADEC6C855822CB294D49492A00C67E55137__Utilities.s_indexOfString_4A9084D27E350810B767E64C519DE5B703918D35110A6B1184C035CE277E0E26);
            if (num >= 0)
            {
              this.runtextpos = runtextpos + num;
              return true;
            }
          }
          this.runtextpos = inputSpan.Length;
          return false;
        }

        // Token: 0x06003AC5 RID: 15045 RVA: 0x0045FBB0 File Offset: 0x0045FBB0
        private unsafe bool TryMatchAtCurrentPosition(ReadOnlySpan<char> inputSpan)
        {
          int num = this.runtextpos;
          int num2 = num;
          ReadOnlySpan<char> readOnlySpan = inputSpan.Slice(num);
          if (!readOnlySpan.StartsWith<char>("=="))
          {
            this.<TryMatchAtCurrentPosition>g__UncaptureUntil|2_0(0);
            return false;
          }
          num += 2;
          readOnlySpan = inputSpan.Slice(num);
          int num3 = num;
          if (readOnlySpan.IsEmpty || *readOnlySpan[0] == 10)
          {
            this.<TryMatchAtCurrentPosition>g__UncaptureUntil|2_0(0);
            return false;
          }
          num++;
          readOnlySpan = inputSpan.Slice(num);
          int num4 = num;
          for (;;)
          {
            int num5 = base.Crawlpos();
            base.Capture(1, num3, num);
            if (readOnlySpan.StartsWith<char>("=="))
            {
              goto Block_6;
            }
            this.<TryMatchAtCurrentPosition>g__UncaptureUntil|2_0(num5);
            if (<RegexGenerator_g>F2A86C3D8F8653D6C99BE1B8441A5DADEC6C855822CB294D49492A00C67E55137__Utilities.s_hasTimeout)
            {
              base.CheckTimeout();
            }
            num = num4;
            readOnlySpan = inputSpan.Slice(num);
            if (readOnlySpan.IsEmpty || *readOnlySpan[0] == 10)
            {
              break;
            }
            num++;
            readOnlySpan = inputSpan.Slice(num);
            num4 = readOnlySpan.IndexOfAny('\n', '=');
            if (num4 >= readOnlySpan.Length || *readOnlySpan[num4] == 10)
            {
              goto IL_00EF;
            }
            num += num4;
            readOnlySpan = inputSpan.Slice(num);
            num4 = num;
          }
          this.<TryMatchAtCurrentPosition>g__UncaptureUntil|2_0(0);
          return false;
          IL_00EF:
          this.<TryMatchAtCurrentPosition>g__UncaptureUntil|2_0(0);
          return false;
          Block_6:
          num += 2;
          this.runtextpos = num;
          base.Capture(0, num2, num);
          return true;
        }

        // Token: 0x06003AC7 RID: 15047 RVA: 0x0045FD0C File Offset: 0x0045FD0C
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
