using System;
using System.CodeDom.Compiler;
using System.Runtime.CompilerServices;

namespace System.Text.RegularExpressions.Generated
{
  // Token: 0x0200000F RID: 15
  [GeneratedCode("System.Text.RegularExpressions.Generator", "9.0.15.2218")]
  [SkipLocalsInit]
  internal sealed class <RegexGenerator_g>F2A86C3D8F8653D6C99BE1B8441A5DADEC6C855822CB294D49492A00C67E55137__SharedStringItem_13 : Regex
  {
    // Token: 0x0600001B RID: 27 RVA: 0x00331870 File Offset: 0x00331870
    private <RegexGenerator_g>F2A86C3D8F8653D6C99BE1B8441A5DADEC6C855822CB294D49492A00C67E55137__SharedStringItem_13()
    {
      this.pattern = "<si>.*?</si>";
      this.roptions = RegexOptions.Singleline;
      Regex.ValidateMatchTimeout(<RegexGenerator_g>F2A86C3D8F8653D6C99BE1B8441A5DADEC6C855822CB294D49492A00C67E55137__Utilities.s_defaultTimeout);
      this.internalMatchTimeout = <RegexGenerator_g>F2A86C3D8F8653D6C99BE1B8441A5DADEC6C855822CB294D49492A00C67E55137__Utilities.s_defaultTimeout;
      this.factory = new <RegexGenerator_g>F2A86C3D8F8653D6C99BE1B8441A5DADEC6C855822CB294D49492A00C67E55137__SharedStringItem_13.RunnerFactory();
      this.capsize = 1;
    }

    // Token: 0x0400000E RID: 14
    [Nullable(1)]
    internal static readonly <RegexGenerator_g>F2A86C3D8F8653D6C99BE1B8441A5DADEC6C855822CB294D49492A00C67E55137__SharedStringItem_13 Instance = new <RegexGenerator_g>F2A86C3D8F8653D6C99BE1B8441A5DADEC6C855822CB294D49492A00C67E55137__SharedStringItem_13();

    // Token: 0x02000242 RID: 578
    private sealed class RunnerFactory : RegexRunnerFactory
    {
      // Token: 0x0600210E RID: 8462 RVA: 0x003F6D70 File Offset: 0x003F6D70
      [NullableContext(1)]
      protected override RegexRunner CreateInstance()
      {
        return new <RegexGenerator_g>F2A86C3D8F8653D6C99BE1B8441A5DADEC6C855822CB294D49492A00C67E55137__SharedStringItem_13.RunnerFactory.Runner();
      }

      // Token: 0x020006AC RID: 1708
      private sealed class Runner : RegexRunner
      {
        // Token: 0x06003A44 RID: 14916 RVA: 0x0045B9B4 File Offset: 0x0045B9B4
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

        // Token: 0x06003A45 RID: 14917 RVA: 0x0045B9F4 File Offset: 0x0045B9F4
        private bool TryFindNextPossibleStartingPosition(ReadOnlySpan<char> inputSpan)
        {
          int runtextpos = this.runtextpos;
          if (runtextpos <= inputSpan.Length - 9)
          {
            int num = inputSpan.Slice(runtextpos).IndexOfAny(<RegexGenerator_g>F2A86C3D8F8653D6C99BE1B8441A5DADEC6C855822CB294D49492A00C67E55137__Utilities.s_indexOfString_C19EAA2CA0036A82D9EC9ECB47A1B2E86367A0C3D616F1D0F994C214F6E265B7);
            if (num >= 0)
            {
              this.runtextpos = runtextpos + num;
              return true;
            }
          }
          this.runtextpos = inputSpan.Length;
          return false;
        }

        // Token: 0x06003A46 RID: 14918 RVA: 0x0045BA48 File Offset: 0x0045BA48
        private bool TryMatchAtCurrentPosition(ReadOnlySpan<char> inputSpan)
        {
          int num = this.runtextpos;
          int num2 = num;
          ReadOnlySpan<char> readOnlySpan = inputSpan.Slice(num);
          if (!readOnlySpan.StartsWith<char>("<si>"))
          {
            return false;
          }
          num += 4;
          readOnlySpan = inputSpan.Slice(num);
          int num3 = num;
          while (!readOnlySpan.StartsWith<char>("</si>"))
          {
            if (<RegexGenerator_g>F2A86C3D8F8653D6C99BE1B8441A5DADEC6C855822CB294D49492A00C67E55137__Utilities.s_hasTimeout)
            {
              base.CheckTimeout();
            }
            num = num3;
            if (inputSpan.Slice(num).IsEmpty)
            {
              return false;
            }
            num++;
            readOnlySpan = inputSpan.Slice(num);
            num3 = readOnlySpan.IndexOf<char>("</si>");
            if (num3 < 0)
            {
              return false;
            }
            num += num3;
            readOnlySpan = inputSpan.Slice(num);
            num3 = num;
          }
          num += 5;
          this.runtextpos = num;
          base.Capture(0, num2, num);
          return true;
        }
      }
    }
  }
}
