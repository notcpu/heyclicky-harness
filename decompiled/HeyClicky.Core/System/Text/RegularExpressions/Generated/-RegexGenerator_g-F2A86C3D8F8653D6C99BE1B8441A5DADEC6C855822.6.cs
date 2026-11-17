using System;
using System.CodeDom.Compiler;
using System.Runtime.CompilerServices;

namespace System.Text.RegularExpressions.Generated
{
  // Token: 0x02000007 RID: 7
  [GeneratedCode("System.Text.RegularExpressions.Generator", "9.0.15.2218")]
  [SkipLocalsInit]
  internal sealed class <RegexGenerator_g>F2A86C3D8F8653D6C99BE1B8441A5DADEC6C855822CB294D49492A00C67E55137__WordTab_5 : Regex
  {
    // Token: 0x0600000B RID: 11 RVA: 0x003315AC File Offset: 0x003315AC
    private <RegexGenerator_g>F2A86C3D8F8653D6C99BE1B8441A5DADEC6C855822CB294D49492A00C67E55137__WordTab_5()
    {
      this.pattern = "<w:tab\\s*/>";
      this.roptions = RegexOptions.None;
      Regex.ValidateMatchTimeout(<RegexGenerator_g>F2A86C3D8F8653D6C99BE1B8441A5DADEC6C855822CB294D49492A00C67E55137__Utilities.s_defaultTimeout);
      this.internalMatchTimeout = <RegexGenerator_g>F2A86C3D8F8653D6C99BE1B8441A5DADEC6C855822CB294D49492A00C67E55137__Utilities.s_defaultTimeout;
      this.factory = new <RegexGenerator_g>F2A86C3D8F8653D6C99BE1B8441A5DADEC6C855822CB294D49492A00C67E55137__WordTab_5.RunnerFactory();
      this.capsize = 1;
    }

    // Token: 0x04000006 RID: 6
    [Nullable(1)]
    internal static readonly <RegexGenerator_g>F2A86C3D8F8653D6C99BE1B8441A5DADEC6C855822CB294D49492A00C67E55137__WordTab_5 Instance = new <RegexGenerator_g>F2A86C3D8F8653D6C99BE1B8441A5DADEC6C855822CB294D49492A00C67E55137__WordTab_5();

    // Token: 0x0200023A RID: 570
    private sealed class RunnerFactory : RegexRunnerFactory
    {
      // Token: 0x060020FE RID: 8446 RVA: 0x003F6CF0 File Offset: 0x003F6CF0
      [NullableContext(1)]
      protected override RegexRunner CreateInstance()
      {
        return new <RegexGenerator_g>F2A86C3D8F8653D6C99BE1B8441A5DADEC6C855822CB294D49492A00C67E55137__WordTab_5.RunnerFactory.Runner();
      }

      // Token: 0x020006A4 RID: 1700
      private sealed class Runner : RegexRunner
      {
        // Token: 0x06003A1F RID: 14879 RVA: 0x0045AAE8 File Offset: 0x0045AAE8
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

        // Token: 0x06003A20 RID: 14880 RVA: 0x0045AB28 File Offset: 0x0045AB28
        private bool TryFindNextPossibleStartingPosition(ReadOnlySpan<char> inputSpan)
        {
          int runtextpos = this.runtextpos;
          if (runtextpos <= inputSpan.Length - 8)
          {
            int num = inputSpan.Slice(runtextpos).IndexOfAny(<RegexGenerator_g>F2A86C3D8F8653D6C99BE1B8441A5DADEC6C855822CB294D49492A00C67E55137__Utilities.s_indexOfString_2385CD56C40C5D62C478D5F27F84F1F9A7A50C0402090FFF8E7761C8AD9A74FC);
            if (num >= 0)
            {
              this.runtextpos = runtextpos + num;
              return true;
            }
          }
          this.runtextpos = inputSpan.Length;
          return false;
        }

        // Token: 0x06003A21 RID: 14881 RVA: 0x0045AB78 File Offset: 0x0045AB78
        private unsafe bool TryMatchAtCurrentPosition(ReadOnlySpan<char> inputSpan)
        {
          int num = this.runtextpos;
          int num2 = num;
          ReadOnlySpan<char> readOnlySpan = inputSpan.Slice(num);
          if (!readOnlySpan.StartsWith<char>("<w:tab"))
          {
            return false;
          }
          int num3 = 6;
          while (num3 < readOnlySpan.Length && char.IsWhiteSpace((char)(*readOnlySpan[num3])))
          {
            num3++;
          }
          readOnlySpan = readOnlySpan.Slice(num3);
          num += num3;
          if (!readOnlySpan.StartsWith<char>("/>"))
          {
            return false;
          }
          num += 2;
          this.runtextpos = num;
          base.Capture(0, num2, num);
          return true;
        }
      }
    }
  }
}
