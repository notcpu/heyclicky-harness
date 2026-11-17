using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading;
using HeyClicky.App.HeyClicky_App_XamlTypeInfo;
using HeyClicky.App.Services;
using HeyClicky.App.Wiring;
using HeyClicky.Core.Telemetry;
using HeyClicky.Drawing;
using HeyClicky.Platform.Win32;
using Microsoft.UI.Dispatching;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Markup;
using WinRT;
using WinRT.HeyClickyVtableClasses;

namespace HeyClicky.App
{
  // Token: 0x02000076 RID: 118
  [WinRTRuntimeClassName("Microsoft.UI.Xaml.IApplicationOverrides")]
  [WinRTExposedType(typeof(HeyClicky_App_AppWinRTTypeDetails))]
  public class App : Application, IXamlMetadataProvider
  {
    // Token: 0x060002BF RID: 703 RVA: 0x0051B764 File Offset: 0x0051B764
    public App()
    {
      this.InitializeComponent();
      base.UnhandledException += delegate(object _, UnhandledExceptionEventArgs e)
      {
        CrashReporting.ReportUiException(e.Exception);
        e.Handled = true;
        if (App.IsLayoutCycle(e.Exception))
        {
          AppHost appHost = this.host;
          if (appHost == null)
          {
            return;
          }
          appHost.RecoverFromLayoutCycle();
        }
      };
    }

    // Token: 0x060002C0 RID: 704 RVA: 0x0051B784 File Offset: 0x0051B784
    [NullableContext(2)]
    private static bool IsLayoutCycle(Exception exception)
    {
      return exception != null && (exception.HResult == -2144665580 || exception.GetType().Name == "LayoutCycleException" || exception.Message.Contains("Layout cycle detected", StringComparison.OrdinalIgnoreCase));
    }

    // Token: 0x060002C1 RID: 705 RVA: 0x0051B7C4 File Offset: 0x0051B7C4
    [NullableContext(1)]
    protected override void OnLaunched(LaunchActivatedEventArgs args)
    {
      AssemblyInformationalVersionAttribute customAttribute = typeof(App).Assembly.GetCustomAttribute<AssemblyInformationalVersionAttribute>();
      string text = ((customAttribute != null) ? customAttribute.InformationalVersion : null) ?? "?";
      string text2 = "app";
      DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(44, 3);
      defaultInterpolatedStringHandler.AppendLiteral("launched ");
      defaultInterpolatedStringHandler.AppendFormatted(text);
      defaultInterpolatedStringHandler.AppendLiteral(" (+");
      defaultInterpolatedStringHandler.AppendFormatted<double>(Program.MillisecondsSinceProcessStart(), "0");
      defaultInterpolatedStringHandler.AppendLiteral(" ms since process start) args=[");
      defaultInterpolatedStringHandler.AppendFormatted(string.Join(' ', Program.Args));
      defaultInterpolatedStringHandler.AppendLiteral("]");
      Log.Info(text2, defaultInterpolatedStringHandler.ToStringAndClear());
      AppOptions appOptions = AppOptions.Parse(Program.Args);
      if (appOptions.AllowCapture)
      {
        ScreenDrawingCanvas.ExcludeFromCapture = false;
        CaptureExclusion.Enabled = false;
      }
      DispatcherQueue forCurrentThread = DispatcherQueue.GetForCurrentThread();
      ValueTuple<AppServices, RealStack> valueTuple = CompositionRoot.Build(appOptions, forCurrentThread, SynchronizationContext.Current);
      AppServices item = valueTuple.Item1;
      RealStack item2 = valueTuple.Item2;
      AppHost started = (this.host = new AppHost(item, appOptions, item2));
      Program.ArgumentsForwarded += delegate(string[] forwarded)
      {
        started.DispatcherQueue.TryEnqueue(delegate
        {
          started.HandleForwardedArgs(forwarded);
        });
      };
      try
      {
        started.Start();
      }
      catch (Exception ex)
      {
        Log.Error("startup", "HeyClicky failed to start", ex);
        CrashReporting.ReportUiException(ex);
        try
        {
          CrashReporter.Current.FlushAsync(TimeSpan.FromSeconds(3L)).Wait(TimeSpan.FromSeconds(4L));
        }
        catch
        {
        }
        IntPtr intPtr = (IntPtr)0;
        defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(140, 2);
        defaultInterpolatedStringHandler.AppendLiteral("HeyClicky couldn't start.\n\n");
        defaultInterpolatedStringHandler.AppendFormatted(ex.GetType().Name);
        defaultInterpolatedStringHandler.AppendLiteral(": ");
        defaultInterpolatedStringHandler.AppendFormatted(ex.Message);
        defaultInterpolatedStringHandler.AppendLiteral("\n\nWe've been sent the details. If it keeps happening, email hi@heyclicky.com with a screenshot of this message.");
        App.MessageBoxW(intPtr, defaultInterpolatedStringHandler.ToStringAndClear(), "HeyClicky", 65552U);
        base.Exit();
      }
    }

    // Token: 0x060002C2 RID: 706
    [NullableContext(1)]
    [DllImport("user32.dll", CharSet = CharSet.Unicode)]
    private static extern int MessageBoxW(IntPtr owner, string text, string caption, uint type);

    // Token: 0x060002C3 RID: 707 RVA: 0x0051B9C0 File Offset: 0x0051B9C0
    [GeneratedCode("Microsoft.UI.Xaml.Markup.Compiler", " 3.0.0.2609")]
    [DebuggerNonUserCode]
    internal static void XamlGeneratedCreateApplicationInstance()
    {
      App._XamlGeneratedCreateApplicationInstance();
    }

    // Token: 0x060002C4 RID: 708 RVA: 0x0051B9C8 File Offset: 0x0051B9C8
    [GeneratedCode("Microsoft.UI.Xaml.Markup.Compiler", " 3.0.0.2609")]
    [DebuggerNonUserCode]
    private static void _XamlGeneratedCreateApplicationInstance()
    {
      new App();
    }

    // Token: 0x060002C5 RID: 709 RVA: 0x0051B9D0 File Offset: 0x0051B9D0
    [GeneratedCode("Microsoft.UI.Xaml.Markup.Compiler", " 3.0.0.2609")]
    [DebuggerNonUserCode]
    public void InitializeComponent()
    {
      if (this._contentLoaded)
      {
        return;
      }
      this._contentLoaded = true;
      Uri uri = new Uri("ms-appx:///App.xaml");
      Application.LoadComponent(this, uri);
    }

    // Token: 0x17000061 RID: 97
    // (get) Token: 0x060002C6 RID: 710 RVA: 0x0051BA00 File Offset: 0x0051BA00
    [GeneratedCode("Microsoft.UI.Xaml.Markup.Compiler", " 3.0.0.2609")]
    [DebuggerNonUserCode]
    private XamlMetaDataProvider _AppProvider
    {
      get
      {
        if (this.__appProvider == null)
        {
          this.__appProvider = new XamlMetaDataProvider();
        }
        return this.__appProvider;
      }
    }

    // Token: 0x060002C7 RID: 711 RVA: 0x0051BA1C File Offset: 0x0051BA1C
    [GeneratedCode("Microsoft.UI.Xaml.Markup.Compiler", " 3.0.0.2609")]
    [DebuggerNonUserCode]
    public IXamlType GetXamlType(Type type)
    {
      return this._AppProvider.GetXamlType(type);
    }

    // Token: 0x060002C8 RID: 712 RVA: 0x0051BA2C File Offset: 0x0051BA2C
    [GeneratedCode("Microsoft.UI.Xaml.Markup.Compiler", " 3.0.0.2609")]
    [DebuggerNonUserCode]
    public IXamlType GetXamlType(string fullName)
    {
      return this._AppProvider.GetXamlType(fullName);
    }

    // Token: 0x060002C9 RID: 713 RVA: 0x0051BA3C File Offset: 0x0051BA3C
    [GeneratedCode("Microsoft.UI.Xaml.Markup.Compiler", " 3.0.0.2609")]
    [DebuggerNonUserCode]
    public XmlnsDefinition[] GetXmlnsDefinitions()
    {
      return this._AppProvider.GetXmlnsDefinitions();
    }

    // Token: 0x0400006E RID: 110
    [Nullable(2)]
    private AppHost host;

    // Token: 0x0400006F RID: 111
    [GeneratedCode("Microsoft.UI.Xaml.Markup.Compiler", " 3.0.0.2609")]
    private bool _contentLoaded;

    // Token: 0x04000070 RID: 112
    [GeneratedCode("Microsoft.UI.Xaml.Markup.Compiler", " 3.0.0.2609")]
    private XamlMetaDataProvider __appProvider;
  }
}
