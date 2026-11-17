using System;
using System.CodeDom.Compiler;
using System.Runtime.CompilerServices;

namespace System.Text.RegularExpressions.Generated
{
  // Token: 0x0200001A RID: 26
  [GeneratedCode("System.Text.RegularExpressions.Generator", "9.0.15.2218")]
  [SkipLocalsInit]
  internal sealed class <RegexGenerator_g>F2A86C3D8F8653D6C99BE1B8441A5DADEC6C855822CB294D49492A00C67E55137__PageType_24 : Regex
  {
    // Token: 0x06000031 RID: 49 RVA: 0x00331C44 File Offset: 0x00331C44
    private <RegexGenerator_g>F2A86C3D8F8653D6C99BE1B8441A5DADEC6C855822CB294D49492A00C67E55137__PageType_24()
    {
      this.pattern = "/Type\\s*/Page(?![s\\w])";
      this.roptions = RegexOptions.None;
      Regex.ValidateMatchTimeout(<RegexGenerator_g>F2A86C3D8F8653D6C99BE1B8441A5DADEC6C855822CB294D49492A00C67E55137__Utilities.s_defaultTimeout);
      this.internalMatchTimeout = <RegexGenerator_g>F2A86C3D8F8653D6C99BE1B8441A5DADEC6C855822CB294D49492A00C67E55137__Utilities.s_defaultTimeout;
      this.factory = new <RegexGenerator_g>F2A86C3D8F8653D6C99BE1B8441A5DADEC6C855822CB294D49492A00C67E55137__PageType_24.RunnerFactory();
      this.capsize = 1;
    }

    // Token: 0x04000019 RID: 25
    [Nullable(1)]
    internal static readonly <RegexGenerator_g>F2A86C3D8F8653D6C99BE1B8441A5DADEC6C855822CB294D49492A00C67E55137__PageType_24 Instance = new <RegexGenerator_g>F2A86C3D8F8653D6C99BE1B8441A5DADEC6C855822CB294D49492A00C67E55137__PageType_24();

    // Token: 0x0200024D RID: 589
    private sealed class RunnerFactory : RegexRunnerFactory
    {
      // Token: 0x06002124 RID: 8484 RVA: 0x003F6E20 File Offset: 0x003F6E20
      [NullableContext(1)]
      protected override RegexRunner CreateInstance()
      {
        return new <RegexGenerator_g>F2A86C3D8F8653D6C99BE1B8441A5DADEC6C855822CB294D49492A00C67E55137__PageType_24.RunnerFactory.Runner();
      }

      // Token: 0x020006B7 RID: 1719
      private sealed class Runner : RegexRunner
      {
        // Token: 0x06003A77 RID: 14967 RVA: 0x0045D2B4 File Offset: 0x0045D2B4
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

        // Token: 0x06003A78 RID: 14968 RVA: 0x0045D2F4 File Offset: 0x0045D2F4
        private bool TryFindNextPossibleStartingPosition(ReadOnlySpan<char> inputSpan)
        {
          int runtextpos = this.runtextpos;
          if (runtextpos <= inputSpan.Length - 10)
          {
            int num = inputSpan.Slice(runtextpos).IndexOfAny(<RegexGenerator_g>F2A86C3D8F8653D6C99BE1B8441A5DADEC6C855822CB294D49492A00C67E55137__Utilities.s_indexOfString_4203FB690845B95F1E40E1EEB32CE001294F9D26760E6BA546169272699BC784);
            if (num >= 0)
            {
              this.runtextpos = runtextpos + num;
              return true;
            }
          }
          this.runtextpos = inputSpan.Length;
          return false;
        }

        // Token: 0x06003A79 RID: 14969 RVA: 0x0045D348 File Offset: 0x0045D348
        private unsafe bool TryMatchAtCurrentPosition(ReadOnlySpan<char> inputSpan)
        {
          int num = this.runtextpos;
          int num2 = num;
          ReadOnlySpan<char> readOnlySpan = inputSpan.Slice(num);
          if (!readOnlySpan.StartsWith<char>("/Type"))
          {
            return false;
          }
          int num3 = 5;
          while (num3 < readOnlySpan.Length && char.IsWhiteSpace((char)(*readOnlySpan[num3])))
          {
            num3++;
          }
          readOnlySpan = readOnlySpan.Slice(num3);
          num += num3;
          if (!readOnlySpan.StartsWith<char>("/Page"))
          {
            return false;
          }
          int num4 = num;
          if (<RegexGenerator_g>F2A86C3D8F8653D6C99BE1B8441A5DADEC6C855822CB294D49492A00C67E55137__Utilities.s_hasTimeout)
          {
            base.CheckTimeout();
          }
          char c;
          if (readOnlySpan.Length >= 6 && !(((c = (char)(*readOnlySpan[5])) < '\u0080') ? (((int)"\0\0\0Ͽ\ufffe蟿\ufffe߿"[(int)(c >> 4)] & (1 << (int)(c & '\u000f'))) == 0) : (!RegexRunner.CharInClass(c, "\0\u0002\nst\0\u0002\u0004\u0005\u0003\u0001\u0006\t\u0013\0"))))
          {
            return false;
          }
          num = num4;
          readOnlySpan = inputSpan.Slice(num);
          num += 5;
          this.runtextpos = num;
          base.Capture(0, num2, num);
          return true;
        }
      }
    }
  }
}
