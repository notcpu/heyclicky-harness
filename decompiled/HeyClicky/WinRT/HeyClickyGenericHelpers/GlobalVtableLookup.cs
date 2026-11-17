using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using ABI.System;
using ABI.System.Collections;
using ABI.System.Collections.Generic;
using Microsoft.UI.Composition;
using Microsoft.UI.Xaml;
using Windows.Foundation;

namespace WinRT.HeyClickyGenericHelpers
{
  // Token: 0x02000062 RID: 98
  internal static class GlobalVtableLookup
  {
    // Token: 0x06000292 RID: 658 RVA: 0x00516084 File Offset: 0x00516084
    [ModuleInitializer]
    internal static void InitializeGlobalVtableLookup()
    {
      ComWrappersSupport.RegisterTypeComInterfaceEntriesLookup(new Func<Type, ComWrappers.ComInterfaceEntry[]>(GlobalVtableLookup.LookupVtableEntries));
      ComWrappersSupport.RegisterTypeRuntimeClassNameLookup(new Func<Type, string>(GlobalVtableLookup.LookupRuntimeClassName));
    }

    // Token: 0x06000293 RID: 659 RVA: 0x005160A8 File Offset: 0x005160A8
    private static ComWrappers.ComInterfaceEntry[] LookupVtableEntries(Type type)
    {
      string text = type.ToString();
      if (text == "System.Collections.ObjectModel.ReadOnlyCollection`1[Windows.Foundation.Point]" || text == "System.Collections.Generic.List`1[Windows.Foundation.Point]")
      {
        bool initialized = IList_Windows_Foundation_Point.Initialized;
        bool initialized2 = IReadOnlyList_Windows_Foundation_Point.Initialized;
        bool initialized3 = IReadOnlyList_object.Initialized;
        bool initialized4 = IEnumerable_Windows_Foundation_Point.Initialized;
        bool initialized5 = IEnumerable_object.Initialized;
        return new ComWrappers.ComInterfaceEntry[]
        {
          new ComWrappers.ComInterfaceEntry
          {
            IID = IListMethods<Point>.IID,
            Vtable = IListMethods<Point>.AbiToProjectionVftablePtr
          },
          new ComWrappers.ComInterfaceEntry
          {
            IID = IReadOnlyListMethods<Point>.IID,
            Vtable = IReadOnlyListMethods<Point>.AbiToProjectionVftablePtr
          },
          new ComWrappers.ComInterfaceEntry
          {
            IID = IReadOnlyListMethods<object>.IID,
            Vtable = IReadOnlyListMethods<object>.AbiToProjectionVftablePtr
          },
          new ComWrappers.ComInterfaceEntry
          {
            IID = IEnumerableMethods<Point>.IID,
            Vtable = IEnumerableMethods<Point>.AbiToProjectionVftablePtr
          },
          new ComWrappers.ComInterfaceEntry
          {
            IID = IEnumerableMethods<object>.IID,
            Vtable = IEnumerableMethods<object>.AbiToProjectionVftablePtr
          },
          new ComWrappers.ComInterfaceEntry
          {
            IID = IListMethods.IID,
            Vtable = IListMethods.AbiToProjectionVftablePtr
          },
          new ComWrappers.ComInterfaceEntry
          {
            IID = IEnumerableMethods.IID,
            Vtable = IEnumerableMethods.AbiToProjectionVftablePtr
          }
        };
      }
      if (text == "System.Collections.Generic.List`1[(System.Collections.Generic.List, System.Boolean)`2[System.Collections.Generic.List`1[Windows.Foundation.Point],System.Boolean]]" || text == "System.Collections.Generic.List`1[HeyClicky.App.Portraits.CloudFaceTraits+Blob]" || text == "System.Collections.Generic.List`1[HeyClicky.DisplayInfo]" || text == "System.Collections.Generic.List`1[HeyClicky.App.Portraits.RosterClicky]" || text == "System.Collections.Generic.List`1[HeyClicky.App.Settings.CustomConnector]" || text == "System.Collections.ObjectModel.ReadOnlyCollection`1[HeyClicky.App.Portraits.HairPiece]" || text == "System.Collections.Generic.List`1[HeyClicky.AgentTaskSnapshot]" || text == "System.Collections.Generic.List`1[HeyClicky.App.OneBrain.ClickyTourJob]" || text == "System.Collections.ObjectModel.ReadOnlyCollection`1[HeyClicky.Core.Tools.AgentTaskInfo]" || text == "System.Collections.Generic.List`1[HeyClicky.App.Dock.ArcRect]" || text == "System.Collections.Generic.List`1[HeyClicky.Core.Worker.InterviewLine]" || text == "System.Collections.ObjectModel.ReadOnlyCollection`1[HeyClicky.App.Settings.CustomConnector]" || text == "System.Collections.ObjectModel.ReadOnlyCollection`1[(System.String, HeyClicky.App.Ink.ClickyPaperFileKind)`2[System.String,HeyClicky.App.Ink.ClickyPaperFileKind]]" || text == "System.Collections.Generic.List`1[HeyClicky.Core.Ink.ClickyInkRoutineControl]" || text == "System.Collections.Generic.List`1[(System.String, HeyClicky.App.Ink.ClickyPaperFileKind)`2[System.String,HeyClicky.App.Ink.ClickyPaperFileKind]]" || text == "System.Collections.ObjectModel.ReadOnlyCollection`1[HeyClicky.Core.Tools.AgentClickyInfo]" || text == "System.Collections.Generic.List`1[HeyClicky.App.Home.LessonService+Recent]" || text == "System.Collections.Generic.List`1[HeyClicky.Core.Proactive.AgentNotification]" || text == "System.Collections.ObjectModel.ReadOnlyCollection`1[HeyClicky.Agents.AgentThreadSummary]" || text == "System.Collections.Generic.List`1[HeyClicky.Core.Tools.AgentThreadInfo]" || text == "System.Collections.Generic.List`1[HeyClicky.App.Services.AppEntry]" || text == "System.Collections.ObjectModel.ReadOnlyCollection`1[HeyClicky.Core.Proactive.AgentIntegrationInfo]" || text == "System.Collections.ObjectModel.ReadOnlyCollection`1[HeyClicky.Agents.Clickys.ClickyInfo]" || text == "System.Collections.ObjectModel.ReadOnlyCollection`1[HeyClicky.Core.Proactive.QueuedProactiveTask]" || text == "System.Collections.ObjectModel.ReadOnlyCollection`1[HeyClicky.DisplayInfo]" || text == "System.Collections.Generic.List`1[HeyClicky.App.Dock.ResultCardLayouts+Arrangement]" || text == "System.Collections.Generic.List`1[HeyClicky.Core.Proactive.ProactiveSuggestionCard]" || text == "System.Collections.ObjectModel.ReadOnlyCollection`1[HeyClicky.Core.Ink.ClickyInkDocument+Block]" || text == "System.Collections.ObjectModel.ReadOnlyCollection`1[HeyClicky.App.Onboarding.TutorialWindow+Step]" || text == "System.Collections.Generic.List`1[HeyClicky.Core.Tools.AgentClickyInfo]" || text == "System.Collections.ObjectModel.ReadOnlyCollection`1[HeyClicky.Core.Worker.InterviewLine]" || text == "System.Collections.ObjectModel.ReadOnlyCollection`1[HeyClicky.App.Portraits.StarterTaskCard]" || text == "System.Collections.Generic.List`1[HeyClicky.App.Dock.DockWindowKey]" || text == "System.Collections.ObjectModel.ReadOnlyCollection`1[HeyClicky.Core.Proactive.AgentNotification]" || text == "System.Collections.ObjectModel.ReadOnlyCollection`1[HeyClicky.App.Home.LessonService+Recent]" || text == "System.Collections.ObjectModel.ReadOnlyCollection`1[System.Collections.Generic.KeyValuePair`2[System.String,System.Text.Json.Nodes.JsonNode]]" || text == "System.Collections.Generic.List`1[HeyClicky.Agents.Clickys.ClickyInfo]" || text == "System.Collections.Generic.List`1[HeyClicky.Core.Notch.ChatHandOff]" || text == "System.Collections.ObjectModel.ReadOnlyCollection`1[HeyClicky.App.Services.AppEntry]" || text == "System.Collections.ObjectModel.ReadOnlyCollection`1[HeyClicky.Core.Ink.ClickyInkRoutineControl]" || text == "System.Collections.Generic.List`1[HeyClicky.App.Portraits.HairPiece]" || text == "System.Collections.Generic.List`1[HeyClicky.App.Home.StoredLesson]" || text == "System.Collections.Generic.List`1[HeyClicky.Agents.AgentThreadSummary]" || text == "System.Collections.Generic.List`1[HeyClicky.Core.Voice.VoiceLanguage]" || text == "System.Collections.Generic.List`1[HeyClicky.Agents.Runtime.CustomConnectorRegistration]" || text == "System.Collections.ObjectModel.ReadOnlyCollection`1[(System.String Role, System.String Text)`2[System.String,System.String]]" || text == "System.Collections.ObjectModel.ReadOnlyCollection`1[(System.String, System.String)`2[System.String,System.String]]" || text == "System.Collections.ObjectModel.ReadOnlyCollection`1[HeyClicky.Core.Tools.AgentThreadInfo]" || text == "System.Collections.Generic.List`1[(System.String Role, System.String Text)`2[System.String,System.String]]" || text == "System.Collections.ObjectModel.ReadOnlyCollection`1[HeyClicky.App.Dock.ResultCardLayouts+Arrangement]" || text == "System.Collections.Generic.List`1[HeyClicky.Core.Tools.AgentTaskInfo]" || text == "System.Collections.ObjectModel.ReadOnlyCollection`1[HeyClicky.App.OneBrain.ClickyTourJob]" || text == "System.Collections.Generic.List`1[(System.String, System.String, System.Func)`3[System.String,System.String,System.Func`1[Microsoft.UI.Xaml.FrameworkElement]]]" || text == "System.Collections.Generic.List`1[HeyClicky.App.Portraits.PortraitAppearance]" || text == "System.Collections.ObjectModel.ReadOnlyCollection`1[(System.String, System.String, System.Func)`3[System.String,System.String,System.Func`1[Microsoft.UI.Xaml.FrameworkElement]]]" || text == "System.Collections.ObjectModel.ReadOnlyCollection`1[HeyClicky.App.Dock.DockWindowKey]" || text == "System.Collections.Generic.List`1[HeyClicky.Agents.Jobs.ClickyJob]" || text == "System.Collections.Generic.List`1[HeyClicky.Core.Ink.ClickyInkDashboardBoard+Section]" || text == "System.Collections.ObjectModel.ReadOnlyCollection`1[HeyClicky.Core.Ink.ClickyInkStreakDay]" || text == "System.Collections.ObjectModel.ReadOnlyCollection`1[HeyClicky.Core.Ink.ClickyInkDashboardBoard+Section]" || text == "System.Collections.Generic.List`1[HeyClicky.Core.Proactive.AgentIntegrationInfo]" || text == "System.Collections.Generic.List`1[HeyClicky.Core.Ink.ClickyInkDocument+Block]" || text == "System.Collections.ObjectModel.ReadOnlyCollection`1[HeyClicky.App.Portraits.PortraitCharacter]" || text == "System.Collections.ObjectModel.ReadOnlyCollection`1[HeyClicky.Core.Voice.VoiceLanguage]" || text == "System.Collections.ObjectModel.ReadOnlyCollection`1[HeyClicky.Core.Notch.ChatHandOff]" || text == "System.Collections.Generic.List`1[HeyClicky.Core.Ink.ClickyInkStreakDay]" || text == "System.Collections.ObjectModel.ReadOnlyCollection`1[HeyClicky.App.Portraits.CloudFaceTraits+Blob]" || text == "System.Collections.ObjectModel.ReadOnlyCollection`1[HeyClicky.Agents.Runtime.CustomConnectorRegistration]" || text == "System.Collections.Generic.List`1[HeyClicky.App.Portraits.StarterTaskCard]" || text == "System.Collections.ObjectModel.ReadOnlyCollection`1[HeyClicky.Core.Proactive.ProactiveSuggestionCard]" || text == "System.Collections.Generic.List`1[HeyClicky.Agents.Routines.ClickyRoutine]" || text == "System.Collections.ObjectModel.ReadOnlyCollection`1[HeyClicky.App.Portraits.PortraitAppearance]" || text == "System.Collections.ObjectModel.ReadOnlyCollection`1[HeyClicky.AgentTaskSnapshot]" || text == "System.Collections.ObjectModel.ReadOnlyCollection`1[HeyClicky.App.Home.StoredLesson]" || text == "System.Collections.Generic.List`1[(System.String, System.String)`2[System.String,System.String]]" || text == "System.Collections.Generic.List`1[HeyClicky.App.Portraits.PortraitCharacter]" || text == "System.Collections.Generic.List`1[HeyClicky.Core.Proactive.QueuedProactiveTask]" || text == "System.Collections.ObjectModel.ReadOnlyCollection`1[HeyClicky.App.Portraits.RosterClicky]" || text == "System.Collections.ObjectModel.ReadOnlyCollection`1[(System.Collections.Generic.List, System.Boolean)`2[System.Collections.Generic.List`1[Windows.Foundation.Point],System.Boolean]]" || text == "System.Collections.ObjectModel.ReadOnlyCollection`1[HeyClicky.Agents.Routines.ClickyRoutine]" || text == "System.Collections.ObjectModel.ReadOnlyCollection`1[HeyClicky.Agents.Jobs.ClickyJob]" || text == "System.Collections.ObjectModel.ReadOnlyCollection`1[HeyClicky.App.Dock.ArcRect]" || text == "System.Collections.Generic.List`1[HeyClicky.App.Onboarding.TutorialWindow+Step]")
      {
        bool initialized6 = IReadOnlyList_object.Initialized;
        bool initialized7 = IEnumerable_object.Initialized;
        return new ComWrappers.ComInterfaceEntry[]
        {
          new ComWrappers.ComInterfaceEntry
          {
            IID = IReadOnlyListMethods<object>.IID,
            Vtable = IReadOnlyListMethods<object>.AbiToProjectionVftablePtr
          },
          new ComWrappers.ComInterfaceEntry
          {
            IID = IEnumerableMethods<object>.IID,
            Vtable = IEnumerableMethods<object>.AbiToProjectionVftablePtr
          },
          new ComWrappers.ComInterfaceEntry
          {
            IID = IListMethods.IID,
            Vtable = IListMethods.AbiToProjectionVftablePtr
          },
          new ComWrappers.ComInterfaceEntry
          {
            IID = IEnumerableMethods.IID,
            Vtable = IEnumerableMethods.AbiToProjectionVftablePtr
          }
        };
      }
      if (text == "System.Collections.ObjectModel.ReadOnlyCollection`1[Microsoft.UI.Xaml.FrameworkElement]" || text == "System.Collections.Generic.List`1[Microsoft.UI.Xaml.FrameworkElement]")
      {
        bool initialized8 = IList_Microsoft_UI_Xaml_FrameworkElement.Initialized;
        bool initialized9 = IReadOnlyList_Microsoft_UI_Xaml_FrameworkElement.Initialized;
        bool initialized10 = IReadOnlyList_Microsoft_UI_Composition_IAnimationObject.Initialized;
        bool initialized11 = IReadOnlyList_Microsoft_UI_Composition_IVisualElement.Initialized;
        bool initialized12 = IReadOnlyList_Microsoft_UI_Composition_IVisualElement2.Initialized;
        bool initialized13 = IReadOnlyList_Microsoft_UI_Xaml_UIElement.Initialized;
        bool initialized14 = IReadOnlyList_Microsoft_UI_Xaml_DependencyObject.Initialized;
        bool initialized15 = IReadOnlyList_object.Initialized;
        bool initialized16 = IEnumerable_Microsoft_UI_Xaml_FrameworkElement.Initialized;
        bool initialized17 = IEnumerable_Microsoft_UI_Composition_IAnimationObject.Initialized;
        bool initialized18 = IEnumerable_Microsoft_UI_Composition_IVisualElement.Initialized;
        bool initialized19 = IEnumerable_Microsoft_UI_Composition_IVisualElement2.Initialized;
        bool initialized20 = IEnumerable_Microsoft_UI_Xaml_UIElement.Initialized;
        bool initialized21 = IEnumerable_Microsoft_UI_Xaml_DependencyObject.Initialized;
        bool initialized22 = IEnumerable_object.Initialized;
        return new ComWrappers.ComInterfaceEntry[]
        {
          new ComWrappers.ComInterfaceEntry
          {
            IID = IListMethods<FrameworkElement>.IID,
            Vtable = IListMethods<FrameworkElement>.AbiToProjectionVftablePtr
          },
          new ComWrappers.ComInterfaceEntry
          {
            IID = IReadOnlyListMethods<FrameworkElement>.IID,
            Vtable = IReadOnlyListMethods<FrameworkElement>.AbiToProjectionVftablePtr
          },
          new ComWrappers.ComInterfaceEntry
          {
            IID = IReadOnlyListMethods<IAnimationObject>.IID,
            Vtable = IReadOnlyListMethods<IAnimationObject>.AbiToProjectionVftablePtr
          },
          new ComWrappers.ComInterfaceEntry
          {
            IID = IReadOnlyListMethods<IVisualElement>.IID,
            Vtable = IReadOnlyListMethods<IVisualElement>.AbiToProjectionVftablePtr
          },
          new ComWrappers.ComInterfaceEntry
          {
            IID = IReadOnlyListMethods<IVisualElement2>.IID,
            Vtable = IReadOnlyListMethods<IVisualElement2>.AbiToProjectionVftablePtr
          },
          new ComWrappers.ComInterfaceEntry
          {
            IID = IReadOnlyListMethods<UIElement>.IID,
            Vtable = IReadOnlyListMethods<UIElement>.AbiToProjectionVftablePtr
          },
          new ComWrappers.ComInterfaceEntry
          {
            IID = IReadOnlyListMethods<DependencyObject>.IID,
            Vtable = IReadOnlyListMethods<DependencyObject>.AbiToProjectionVftablePtr
          },
          new ComWrappers.ComInterfaceEntry
          {
            IID = IReadOnlyListMethods<object>.IID,
            Vtable = IReadOnlyListMethods<object>.AbiToProjectionVftablePtr
          },
          new ComWrappers.ComInterfaceEntry
          {
            IID = IEnumerableMethods<FrameworkElement>.IID,
            Vtable = IEnumerableMethods<FrameworkElement>.AbiToProjectionVftablePtr
          },
          new ComWrappers.ComInterfaceEntry
          {
            IID = IEnumerableMethods<IAnimationObject>.IID,
            Vtable = IEnumerableMethods<IAnimationObject>.AbiToProjectionVftablePtr
          },
          new ComWrappers.ComInterfaceEntry
          {
            IID = IEnumerableMethods<IVisualElement>.IID,
            Vtable = IEnumerableMethods<IVisualElement>.AbiToProjectionVftablePtr
          },
          new ComWrappers.ComInterfaceEntry
          {
            IID = IEnumerableMethods<IVisualElement2>.IID,
            Vtable = IEnumerableMethods<IVisualElement2>.AbiToProjectionVftablePtr
          },
          new ComWrappers.ComInterfaceEntry
          {
            IID = IEnumerableMethods<UIElement>.IID,
            Vtable = IEnumerableMethods<UIElement>.AbiToProjectionVftablePtr
          },
          new ComWrappers.ComInterfaceEntry
          {
            IID = IEnumerableMethods<DependencyObject>.IID,
            Vtable = IEnumerableMethods<DependencyObject>.AbiToProjectionVftablePtr
          },
          new ComWrappers.ComInterfaceEntry
          {
            IID = IEnumerableMethods<object>.IID,
            Vtable = IEnumerableMethods<object>.AbiToProjectionVftablePtr
          },
          new ComWrappers.ComInterfaceEntry
          {
            IID = IListMethods.IID,
            Vtable = IListMethods.AbiToProjectionVftablePtr
          },
          new ComWrappers.ComInterfaceEntry
          {
            IID = IEnumerableMethods.IID,
            Vtable = IEnumerableMethods.AbiToProjectionVftablePtr
          }
        };
      }
      if (text == "HeyClicky.Platform.Audio.WasapiAudioOutput" || text == "System.Threading.Tasks.Task`1[System.Threading.Tasks.Task]" || text == "System.Threading.Tasks.Task`1[System.Boolean]" || text == "HeyClicky.Platform.Input.ShortcutMonitor" || text == "HeyClicky.Platform.Permissions.PermissionService" || text == "HeyClicky.Platform.Capture.ScreenCaptureService" || text == "HeyClicky.Drawing.ScreenDrawingCanvas" || text == "System.IO.MemoryStream" || text == "HeyClicky.Core.Notch.NotchPresenter" || text == "System.Threading.Tasks.Task`1[System.Text.Json.Nodes.JsonObject]" || text == "System.Threading.Tasks.Task`1[HeyClicky.Core.Browser.UploadApprovalAnswer]")
      {
        return new ComWrappers.ComInterfaceEntry[]
        {
          new ComWrappers.ComInterfaceEntry
          {
            IID = IDisposableMethods.IID,
            Vtable = IDisposableMethods.AbiToProjectionVftablePtr
          }
        };
      }
      if (text == "System.Collections.Generic.Dictionary`2[System.String,System.Boolean]" || text == "System.Collections.ObjectModel.ReadOnlyDictionary`2[System.String,System.Boolean]")
      {
        bool initialized23 = IDictionary_string_bool.Initialized;
        bool initialized24 = IReadOnlyDictionary_string_bool.Initialized;
        bool initialized25 = KeyValuePair_string_bool.Initialized;
        bool initialized26 = IEnumerable_System_Collections_Generic_KeyValuePair_string__bool_.Initialized;
        bool initialized27 = IEnumerable_object.Initialized;
        return new ComWrappers.ComInterfaceEntry[]
        {
          new ComWrappers.ComInterfaceEntry
          {
            IID = IDictionaryMethods<string, bool>.IID,
            Vtable = IDictionaryMethods<string, bool>.AbiToProjectionVftablePtr
          },
          new ComWrappers.ComInterfaceEntry
          {
            IID = IReadOnlyDictionaryMethods<string, bool>.IID,
            Vtable = IReadOnlyDictionaryMethods<string, bool>.AbiToProjectionVftablePtr
          },
          new ComWrappers.ComInterfaceEntry
          {
            IID = IEnumerableMethods<KeyValuePair<string, bool>>.IID,
            Vtable = IEnumerableMethods<KeyValuePair<string, bool>>.AbiToProjectionVftablePtr
          },
          new ComWrappers.ComInterfaceEntry
          {
            IID = IEnumerableMethods<object>.IID,
            Vtable = IEnumerableMethods<object>.AbiToProjectionVftablePtr
          },
          new ComWrappers.ComInterfaceEntry
          {
            IID = IEnumerableMethods.IID,
            Vtable = IEnumerableMethods.AbiToProjectionVftablePtr
          }
        };
      }
      if (text == "System.Text.Json.Nodes.JsonObject" || text == "System.Collections.Generic.Dictionary`2[System.String,HeyClicky.App.Dock.ArcCardView]" || text == "System.Collections.Generic.Dictionary`2+ValueCollection[System.String,HeyClicky.App.Portraits.PortraitAppearance]" || text == "ABI.System.Collections.Generic.ConstantSplittableMap`2[System.String,HeyClicky.App.Dock.ArcRect]" || text == "ABI.System.Collections.Generic.ConstantSplittableMap`2[System.String,System.Text.Json.Nodes.JsonNode]" || text == "System.Collections.ObjectModel.ReadOnlyDictionary`2[System.String,System.Text.Json.Nodes.JsonNode]" || text == "System.Collections.ObjectModel.ReadOnlyDictionary`2[System.String,HeyClicky.App.Dock.ArcCardView]" || text == "System.Collections.ObjectModel.ReadOnlyDictionary`2[System.String,HeyClicky.App.Dock.ArcRect]" || text == "System.Collections.Generic.Dictionary`2[System.String,System.Byte[]]" || text == "System.Collections.Generic.Dictionary`2[System.String,HeyClicky.App.Dock.ArcRect]" || text == "System.Collections.ObjectModel.ReadOnlyDictionary`2[System.String,System.Byte[]]" || text == "ABI.System.Collections.Generic.ConstantSplittableMap`2[System.String,HeyClicky.App.Dock.ArcCardView]" || text == "ABI.System.Collections.Generic.ConstantSplittableMap`2[System.String,System.Byte[]]")
      {
        bool initialized28 = IEnumerable_object.Initialized;
        return new ComWrappers.ComInterfaceEntry[]
        {
          new ComWrappers.ComInterfaceEntry
          {
            IID = IEnumerableMethods<object>.IID,
            Vtable = IEnumerableMethods<object>.AbiToProjectionVftablePtr
          },
          new ComWrappers.ComInterfaceEntry
          {
            IID = IEnumerableMethods.IID,
            Vtable = IEnumerableMethods.AbiToProjectionVftablePtr
          }
        };
      }
      if (text == "ABI.System.Collections.Generic.ToAbiEnumeratorAdapter`1[System.Collections.Generic.KeyValuePair`2[System.String,System.Boolean]]")
      {
        bool initialized29 = KeyValuePair_string_bool.Initialized;
        bool initialized30 = IEnumerator_System_Collections_Generic_KeyValuePair_string__bool_.Initialized;
        bool initialized31 = IEnumerator_object.Initialized;
        return new ComWrappers.ComInterfaceEntry[]
        {
          new ComWrappers.ComInterfaceEntry
          {
            IID = IEnumeratorMethods<KeyValuePair<string, bool>>.IID,
            Vtable = IEnumeratorMethods<KeyValuePair<string, bool>>.AbiToProjectionVftablePtr
          },
          new ComWrappers.ComInterfaceEntry
          {
            IID = IEnumeratorMethods<object>.IID,
            Vtable = IEnumeratorMethods<object>.AbiToProjectionVftablePtr
          },
          new ComWrappers.ComInterfaceEntry
          {
            IID = IDisposableMethods.IID,
            Vtable = IDisposableMethods.AbiToProjectionVftablePtr
          }
        };
      }
      if (text == "ABI.System.Collections.Generic.ToAbiEnumeratorAdapter`1[System.Collections.Generic.IEnumerable`1[System.Byte]]")
      {
        bool initialized32 = IEnumerable_byte.Initialized;
        bool initialized33 = IEnumerator_System_Collections_Generic_IEnumerable_byte_.Initialized;
        bool initialized34 = IEnumerator_System_Collections_IEnumerable.Initialized;
        return new ComWrappers.ComInterfaceEntry[]
        {
          new ComWrappers.ComInterfaceEntry
          {
            IID = IEnumeratorMethods<IEnumerable<byte>>.IID,
            Vtable = IEnumeratorMethods<IEnumerable<byte>>.AbiToProjectionVftablePtr
          },
          new ComWrappers.ComInterfaceEntry
          {
            IID = IEnumeratorMethods<IEnumerable>.IID,
            Vtable = IEnumeratorMethods<IEnumerable>.AbiToProjectionVftablePtr
          },
          new ComWrappers.ComInterfaceEntry
          {
            IID = IDisposableMethods.IID,
            Vtable = IDisposableMethods.AbiToProjectionVftablePtr
          }
        };
      }
      if (text == "System.Collections.Generic.HashSet`1[System.Guid]")
      {
        bool initialized35 = IEnumerable_System_Guid.Initialized;
        bool initialized36 = IEnumerable_object.Initialized;
        return new ComWrappers.ComInterfaceEntry[]
        {
          new ComWrappers.ComInterfaceEntry
          {
            IID = IEnumerableMethods<Guid>.IID,
            Vtable = IEnumerableMethods<Guid>.AbiToProjectionVftablePtr
          },
          new ComWrappers.ComInterfaceEntry
          {
            IID = IEnumerableMethods<object>.IID,
            Vtable = IEnumerableMethods<object>.AbiToProjectionVftablePtr
          },
          new ComWrappers.ComInterfaceEntry
          {
            IID = IEnumerableMethods.IID,
            Vtable = IEnumerableMethods.AbiToProjectionVftablePtr
          }
        };
      }
      if (text == "System.Collections.Generic.List`1[System.Double]" || text == "System.Collections.ObjectModel.ReadOnlyCollection`1[System.Double]")
      {
        bool initialized37 = IList_double.Initialized;
        bool initialized38 = IReadOnlyList_double.Initialized;
        bool initialized39 = IReadOnlyList_object.Initialized;
        bool initialized40 = IEnumerable_double.Initialized;
        bool initialized41 = IEnumerable_object.Initialized;
        return new ComWrappers.ComInterfaceEntry[]
        {
          new ComWrappers.ComInterfaceEntry
          {
            IID = IListMethods<double>.IID,
            Vtable = IListMethods<double>.AbiToProjectionVftablePtr
          },
          new ComWrappers.ComInterfaceEntry
          {
            IID = IReadOnlyListMethods<double>.IID,
            Vtable = IReadOnlyListMethods<double>.AbiToProjectionVftablePtr
          },
          new ComWrappers.ComInterfaceEntry
          {
            IID = IReadOnlyListMethods<object>.IID,
            Vtable = IReadOnlyListMethods<object>.AbiToProjectionVftablePtr
          },
          new ComWrappers.ComInterfaceEntry
          {
            IID = IEnumerableMethods<double>.IID,
            Vtable = IEnumerableMethods<double>.AbiToProjectionVftablePtr
          },
          new ComWrappers.ComInterfaceEntry
          {
            IID = IEnumerableMethods<object>.IID,
            Vtable = IEnumerableMethods<object>.AbiToProjectionVftablePtr
          },
          new ComWrappers.ComInterfaceEntry
          {
            IID = IListMethods.IID,
            Vtable = IListMethods.AbiToProjectionVftablePtr
          },
          new ComWrappers.ComInterfaceEntry
          {
            IID = IEnumerableMethods.IID,
            Vtable = IEnumerableMethods.AbiToProjectionVftablePtr
          }
        };
      }
      if (text == "HeyClicky.App.Portraits.SkinTone[]" || text == "HeyClicky.App.Portraits.Colorway[]" || text == "(System.String Event, System.String[] Keys)[]" || text == "HeyClicky.App.Portraits.PortraitCharacter[]" || text == "(System.Single v, System.Double s)[]" || text == "(System.Single, System.Double)[]" || text == "HeyClicky.App.Portraits.HairPiece[]")
      {
        bool initialized42 = IReadOnlyList_object.Initialized;
        bool initialized43 = IEnumerable_object.Initialized;
        return new ComWrappers.ComInterfaceEntry[]
        {
          new ComWrappers.ComInterfaceEntry
          {
            IID = IListMethods.IID,
            Vtable = IListMethods.AbiToProjectionVftablePtr
          },
          new ComWrappers.ComInterfaceEntry
          {
            IID = IReadOnlyListMethods<object>.IID,
            Vtable = IReadOnlyListMethods<object>.AbiToProjectionVftablePtr
          },
          new ComWrappers.ComInterfaceEntry
          {
            IID = IEnumerableMethods<object>.IID,
            Vtable = IEnumerableMethods<object>.AbiToProjectionVftablePtr
          },
          new ComWrappers.ComInterfaceEntry
          {
            IID = IEnumerableMethods.IID,
            Vtable = IEnumerableMethods.AbiToProjectionVftablePtr
          }
        };
      }
      if (text == "ABI.System.Collections.Generic.ToAbiEnumeratorAdapter`1[Microsoft.UI.Xaml.UIElement]")
      {
        bool initialized44 = IEnumerator_Microsoft_UI_Xaml_UIElement.Initialized;
        bool initialized45 = IEnumerator_Microsoft_UI_Composition_IAnimationObject.Initialized;
        bool initialized46 = IEnumerator_Microsoft_UI_Composition_IVisualElement.Initialized;
        bool initialized47 = IEnumerator_Microsoft_UI_Composition_IVisualElement2.Initialized;
        bool initialized48 = IEnumerator_Microsoft_UI_Xaml_DependencyObject.Initialized;
        bool initialized49 = IEnumerator_object.Initialized;
        return new ComWrappers.ComInterfaceEntry[]
        {
          new ComWrappers.ComInterfaceEntry
          {
            IID = IEnumeratorMethods<UIElement>.IID,
            Vtable = IEnumeratorMethods<UIElement>.AbiToProjectionVftablePtr
          },
          new ComWrappers.ComInterfaceEntry
          {
            IID = IEnumeratorMethods<IAnimationObject>.IID,
            Vtable = IEnumeratorMethods<IAnimationObject>.AbiToProjectionVftablePtr
          },
          new ComWrappers.ComInterfaceEntry
          {
            IID = IEnumeratorMethods<IVisualElement>.IID,
            Vtable = IEnumeratorMethods<IVisualElement>.AbiToProjectionVftablePtr
          },
          new ComWrappers.ComInterfaceEntry
          {
            IID = IEnumeratorMethods<IVisualElement2>.IID,
            Vtable = IEnumeratorMethods<IVisualElement2>.AbiToProjectionVftablePtr
          },
          new ComWrappers.ComInterfaceEntry
          {
            IID = IEnumeratorMethods<DependencyObject>.IID,
            Vtable = IEnumeratorMethods<DependencyObject>.AbiToProjectionVftablePtr
          },
          new ComWrappers.ComInterfaceEntry
          {
            IID = IEnumeratorMethods<object>.IID,
            Vtable = IEnumeratorMethods<object>.AbiToProjectionVftablePtr
          },
          new ComWrappers.ComInterfaceEntry
          {
            IID = IDisposableMethods.IID,
            Vtable = IDisposableMethods.AbiToProjectionVftablePtr
          }
        };
      }
      if (text == "System.Collections.ObjectModel.ReadOnlyDictionary`2[System.Int32,System.String]" || text == "System.Collections.Generic.Dictionary`2[System.Int32,System.String]")
      {
        bool initialized50 = IDictionary_int_string.Initialized;
        bool initialized51 = IReadOnlyDictionary_int_string.Initialized;
        bool initialized52 = KeyValuePair_int_string.Initialized;
        bool initialized53 = IEnumerable_System_Collections_Generic_KeyValuePair_int__string_.Initialized;
        bool initialized54 = IEnumerable_object.Initialized;
        return new ComWrappers.ComInterfaceEntry[]
        {
          new ComWrappers.ComInterfaceEntry
          {
            IID = IDictionaryMethods<int, string>.IID,
            Vtable = IDictionaryMethods<int, string>.AbiToProjectionVftablePtr
          },
          new ComWrappers.ComInterfaceEntry
          {
            IID = IReadOnlyDictionaryMethods<int, string>.IID,
            Vtable = IReadOnlyDictionaryMethods<int, string>.AbiToProjectionVftablePtr
          },
          new ComWrappers.ComInterfaceEntry
          {
            IID = IEnumerableMethods<KeyValuePair<int, string>>.IID,
            Vtable = IEnumerableMethods<KeyValuePair<int, string>>.AbiToProjectionVftablePtr
          },
          new ComWrappers.ComInterfaceEntry
          {
            IID = IEnumerableMethods<object>.IID,
            Vtable = IEnumerableMethods<object>.AbiToProjectionVftablePtr
          },
          new ComWrappers.ComInterfaceEntry
          {
            IID = IEnumerableMethods.IID,
            Vtable = IEnumerableMethods.AbiToProjectionVftablePtr
          }
        };
      }
      if (text == "ABI.System.Collections.Generic.ToAbiEnumeratorAdapter`1[System.Collections.Generic.KeyValuePair`2[System.String,System.Int32]]")
      {
        bool initialized55 = KeyValuePair_string_int.Initialized;
        bool initialized56 = IEnumerator_System_Collections_Generic_KeyValuePair_string__int_.Initialized;
        bool initialized57 = IEnumerator_object.Initialized;
        return new ComWrappers.ComInterfaceEntry[]
        {
          new ComWrappers.ComInterfaceEntry
          {
            IID = IEnumeratorMethods<KeyValuePair<string, int>>.IID,
            Vtable = IEnumeratorMethods<KeyValuePair<string, int>>.AbiToProjectionVftablePtr
          },
          new ComWrappers.ComInterfaceEntry
          {
            IID = IEnumeratorMethods<object>.IID,
            Vtable = IEnumeratorMethods<object>.AbiToProjectionVftablePtr
          },
          new ComWrappers.ComInterfaceEntry
          {
            IID = IDisposableMethods.IID,
            Vtable = IDisposableMethods.AbiToProjectionVftablePtr
          }
        };
      }
      if (text == "System.Collections.Generic.Dictionary`2[System.String,System.Int32]" || text == "System.Collections.ObjectModel.ReadOnlyDictionary`2[System.String,System.Int32]")
      {
        bool initialized58 = IDictionary_string_int.Initialized;
        bool initialized59 = IReadOnlyDictionary_string_int.Initialized;
        bool initialized60 = KeyValuePair_string_int.Initialized;
        bool initialized61 = IEnumerable_System_Collections_Generic_KeyValuePair_string__int_.Initialized;
        bool initialized62 = IEnumerable_object.Initialized;
        return new ComWrappers.ComInterfaceEntry[]
        {
          new ComWrappers.ComInterfaceEntry
          {
            IID = IDictionaryMethods<string, int>.IID,
            Vtable = IDictionaryMethods<string, int>.AbiToProjectionVftablePtr
          },
          new ComWrappers.ComInterfaceEntry
          {
            IID = IReadOnlyDictionaryMethods<string, int>.IID,
            Vtable = IReadOnlyDictionaryMethods<string, int>.AbiToProjectionVftablePtr
          },
          new ComWrappers.ComInterfaceEntry
          {
            IID = IEnumerableMethods<KeyValuePair<string, int>>.IID,
            Vtable = IEnumerableMethods<KeyValuePair<string, int>>.AbiToProjectionVftablePtr
          },
          new ComWrappers.ComInterfaceEntry
          {
            IID = IEnumerableMethods<object>.IID,
            Vtable = IEnumerableMethods<object>.AbiToProjectionVftablePtr
          },
          new ComWrappers.ComInterfaceEntry
          {
            IID = IEnumerableMethods.IID,
            Vtable = IEnumerableMethods.AbiToProjectionVftablePtr
          }
        };
      }
      if (text == "ABI.System.Collections.Generic.ToAbiEnumeratorAdapter`1[Microsoft.UI.Xaml.FrameworkElement]")
      {
        bool initialized63 = IEnumerator_Microsoft_UI_Xaml_FrameworkElement.Initialized;
        bool initialized64 = IEnumerator_Microsoft_UI_Composition_IAnimationObject.Initialized;
        bool initialized65 = IEnumerator_Microsoft_UI_Composition_IVisualElement.Initialized;
        bool initialized66 = IEnumerator_Microsoft_UI_Composition_IVisualElement2.Initialized;
        bool initialized67 = IEnumerator_Microsoft_UI_Xaml_UIElement.Initialized;
        bool initialized68 = IEnumerator_Microsoft_UI_Xaml_DependencyObject.Initialized;
        bool initialized69 = IEnumerator_object.Initialized;
        return new ComWrappers.ComInterfaceEntry[]
        {
          new ComWrappers.ComInterfaceEntry
          {
            IID = IEnumeratorMethods<FrameworkElement>.IID,
            Vtable = IEnumeratorMethods<FrameworkElement>.AbiToProjectionVftablePtr
          },
          new ComWrappers.ComInterfaceEntry
          {
            IID = IEnumeratorMethods<IAnimationObject>.IID,
            Vtable = IEnumeratorMethods<IAnimationObject>.AbiToProjectionVftablePtr
          },
          new ComWrappers.ComInterfaceEntry
          {
            IID = IEnumeratorMethods<IVisualElement>.IID,
            Vtable = IEnumeratorMethods<IVisualElement>.AbiToProjectionVftablePtr
          },
          new ComWrappers.ComInterfaceEntry
          {
            IID = IEnumeratorMethods<IVisualElement2>.IID,
            Vtable = IEnumeratorMethods<IVisualElement2>.AbiToProjectionVftablePtr
          },
          new ComWrappers.ComInterfaceEntry
          {
            IID = IEnumeratorMethods<UIElement>.IID,
            Vtable = IEnumeratorMethods<UIElement>.AbiToProjectionVftablePtr
          },
          new ComWrappers.ComInterfaceEntry
          {
            IID = IEnumeratorMethods<DependencyObject>.IID,
            Vtable = IEnumeratorMethods<DependencyObject>.AbiToProjectionVftablePtr
          },
          new ComWrappers.ComInterfaceEntry
          {
            IID = IEnumeratorMethods<object>.IID,
            Vtable = IEnumeratorMethods<object>.AbiToProjectionVftablePtr
          },
          new ComWrappers.ComInterfaceEntry
          {
            IID = IDisposableMethods.IID,
            Vtable = IDisposableMethods.AbiToProjectionVftablePtr
          }
        };
      }
      if (text == "System.Collections.ObjectModel.ReadOnlyDictionary`2[System.String,System.String]" || text == "System.Collections.Generic.Dictionary`2[System.String,System.String]")
      {
        bool initialized70 = IDictionary_string_string.Initialized;
        bool initialized71 = IReadOnlyDictionary_string_string.Initialized;
        bool initialized72 = KeyValuePair_string_string.Initialized;
        bool initialized73 = IEnumerable_System_Collections_Generic_KeyValuePair_string__string_.Initialized;
        bool initialized74 = IEnumerable_object.Initialized;
        return new ComWrappers.ComInterfaceEntry[]
        {
          new ComWrappers.ComInterfaceEntry
          {
            IID = IDictionaryMethods<string, string>.IID,
            Vtable = IDictionaryMethods<string, string>.AbiToProjectionVftablePtr
          },
          new ComWrappers.ComInterfaceEntry
          {
            IID = IReadOnlyDictionaryMethods<string, string>.IID,
            Vtable = IReadOnlyDictionaryMethods<string, string>.AbiToProjectionVftablePtr
          },
          new ComWrappers.ComInterfaceEntry
          {
            IID = IEnumerableMethods<KeyValuePair<string, string>>.IID,
            Vtable = IEnumerableMethods<KeyValuePair<string, string>>.AbiToProjectionVftablePtr
          },
          new ComWrappers.ComInterfaceEntry
          {
            IID = IEnumerableMethods<object>.IID,
            Vtable = IEnumerableMethods<object>.AbiToProjectionVftablePtr
          },
          new ComWrappers.ComInterfaceEntry
          {
            IID = IEnumerableMethods.IID,
            Vtable = IEnumerableMethods.AbiToProjectionVftablePtr
          }
        };
      }
      if (text == "ABI.System.Collections.Generic.ToAbiEnumeratorAdapter`1[System.Collections.IList]")
      {
        bool initialized75 = IEnumerator_System_Collections_IList.Initialized;
        bool initialized76 = IEnumerator_System_Collections_IEnumerable.Initialized;
        return new ComWrappers.ComInterfaceEntry[]
        {
          new ComWrappers.ComInterfaceEntry
          {
            IID = IEnumeratorMethods<IList>.IID,
            Vtable = IEnumeratorMethods<IList>.AbiToProjectionVftablePtr
          },
          new ComWrappers.ComInterfaceEntry
          {
            IID = IEnumeratorMethods<IEnumerable>.IID,
            Vtable = IEnumeratorMethods<IEnumerable>.AbiToProjectionVftablePtr
          },
          new ComWrappers.ComInterfaceEntry
          {
            IID = IDisposableMethods.IID,
            Vtable = IDisposableMethods.AbiToProjectionVftablePtr
          }
        };
      }
      if (text == "ABI.System.Collections.Generic.ToAbiEnumeratorAdapter`1[Microsoft.UI.Composition.IAnimationObject]")
      {
        bool initialized77 = IEnumerator_Microsoft_UI_Composition_IAnimationObject.Initialized;
        return new ComWrappers.ComInterfaceEntry[]
        {
          new ComWrappers.ComInterfaceEntry
          {
            IID = IEnumeratorMethods<IAnimationObject>.IID,
            Vtable = IEnumeratorMethods<IAnimationObject>.AbiToProjectionVftablePtr
          },
          new ComWrappers.ComInterfaceEntry
          {
            IID = IDisposableMethods.IID,
            Vtable = IDisposableMethods.AbiToProjectionVftablePtr
          }
        };
      }
      if (text == "System.String[]")
      {
        bool initialized78 = IList_string.Initialized;
        bool initialized79 = IReadOnlyList_string.Initialized;
        bool initialized80 = IEnumerable_char.Initialized;
        bool initialized81 = IReadOnlyList_System_Collections_Generic_IEnumerable_char_.Initialized;
        bool initialized82 = IEnumerable_object.Initialized;
        bool initialized83 = IReadOnlyList_System_Collections_Generic_IEnumerable_object_.Initialized;
        bool initialized84 = IReadOnlyList_System_Collections_IEnumerable.Initialized;
        bool initialized85 = IReadOnlyList_object.Initialized;
        bool initialized86 = IEnumerable_string.Initialized;
        bool initialized87 = IEnumerable_System_Collections_Generic_IEnumerable_char_.Initialized;
        bool initialized88 = IEnumerable_System_Collections_Generic_IEnumerable_object_.Initialized;
        bool initialized89 = IEnumerable_System_Collections_IEnumerable.Initialized;
        return new ComWrappers.ComInterfaceEntry[]
        {
          new ComWrappers.ComInterfaceEntry
          {
            IID = IListMethods.IID,
            Vtable = IListMethods.AbiToProjectionVftablePtr
          },
          new ComWrappers.ComInterfaceEntry
          {
            IID = IListMethods<string>.IID,
            Vtable = IListMethods<string>.AbiToProjectionVftablePtr
          },
          new ComWrappers.ComInterfaceEntry
          {
            IID = IReadOnlyListMethods<string>.IID,
            Vtable = IReadOnlyListMethods<string>.AbiToProjectionVftablePtr
          },
          new ComWrappers.ComInterfaceEntry
          {
            IID = IReadOnlyListMethods<IEnumerable<char>>.IID,
            Vtable = IReadOnlyListMethods<IEnumerable<char>>.AbiToProjectionVftablePtr
          },
          new ComWrappers.ComInterfaceEntry
          {
            IID = IReadOnlyListMethods<IEnumerable<object>>.IID,
            Vtable = IReadOnlyListMethods<IEnumerable<object>>.AbiToProjectionVftablePtr
          },
          new ComWrappers.ComInterfaceEntry
          {
            IID = IReadOnlyListMethods<IEnumerable>.IID,
            Vtable = IReadOnlyListMethods<IEnumerable>.AbiToProjectionVftablePtr
          },
          new ComWrappers.ComInterfaceEntry
          {
            IID = IReadOnlyListMethods<object>.IID,
            Vtable = IReadOnlyListMethods<object>.AbiToProjectionVftablePtr
          },
          new ComWrappers.ComInterfaceEntry
          {
            IID = IEnumerableMethods<string>.IID,
            Vtable = IEnumerableMethods<string>.AbiToProjectionVftablePtr
          },
          new ComWrappers.ComInterfaceEntry
          {
            IID = IEnumerableMethods<IEnumerable<char>>.IID,
            Vtable = IEnumerableMethods<IEnumerable<char>>.AbiToProjectionVftablePtr
          },
          new ComWrappers.ComInterfaceEntry
          {
            IID = IEnumerableMethods<IEnumerable<object>>.IID,
            Vtable = IEnumerableMethods<IEnumerable<object>>.AbiToProjectionVftablePtr
          },
          new ComWrappers.ComInterfaceEntry
          {
            IID = IEnumerableMethods<IEnumerable>.IID,
            Vtable = IEnumerableMethods<IEnumerable>.AbiToProjectionVftablePtr
          },
          new ComWrappers.ComInterfaceEntry
          {
            IID = IEnumerableMethods<object>.IID,
            Vtable = IEnumerableMethods<object>.AbiToProjectionVftablePtr
          },
          new ComWrappers.ComInterfaceEntry
          {
            IID = IEnumerableMethods.IID,
            Vtable = IEnumerableMethods.AbiToProjectionVftablePtr
          }
        };
      }
      if (text == "System.Collections.ObjectModel.ReadOnlyCollection`1[System.String]" || text == "System.Collections.Generic.List`1[System.String]")
      {
        bool initialized90 = IList_string.Initialized;
        bool initialized91 = IReadOnlyList_string.Initialized;
        bool initialized92 = IEnumerable_char.Initialized;
        bool initialized93 = IReadOnlyList_System_Collections_Generic_IEnumerable_char_.Initialized;
        bool initialized94 = IEnumerable_object.Initialized;
        bool initialized95 = IReadOnlyList_System_Collections_Generic_IEnumerable_object_.Initialized;
        bool initialized96 = IReadOnlyList_System_Collections_IEnumerable.Initialized;
        bool initialized97 = IReadOnlyList_object.Initialized;
        bool initialized98 = IEnumerable_string.Initialized;
        bool initialized99 = IEnumerable_System_Collections_Generic_IEnumerable_char_.Initialized;
        bool initialized100 = IEnumerable_System_Collections_Generic_IEnumerable_object_.Initialized;
        bool initialized101 = IEnumerable_System_Collections_IEnumerable.Initialized;
        return new ComWrappers.ComInterfaceEntry[]
        {
          new ComWrappers.ComInterfaceEntry
          {
            IID = IListMethods<string>.IID,
            Vtable = IListMethods<string>.AbiToProjectionVftablePtr
          },
          new ComWrappers.ComInterfaceEntry
          {
            IID = IReadOnlyListMethods<string>.IID,
            Vtable = IReadOnlyListMethods<string>.AbiToProjectionVftablePtr
          },
          new ComWrappers.ComInterfaceEntry
          {
            IID = IReadOnlyListMethods<IEnumerable<char>>.IID,
            Vtable = IReadOnlyListMethods<IEnumerable<char>>.AbiToProjectionVftablePtr
          },
          new ComWrappers.ComInterfaceEntry
          {
            IID = IReadOnlyListMethods<IEnumerable<object>>.IID,
            Vtable = IReadOnlyListMethods<IEnumerable<object>>.AbiToProjectionVftablePtr
          },
          new ComWrappers.ComInterfaceEntry
          {
            IID = IReadOnlyListMethods<IEnumerable>.IID,
            Vtable = IReadOnlyListMethods<IEnumerable>.AbiToProjectionVftablePtr
          },
          new ComWrappers.ComInterfaceEntry
          {
            IID = IReadOnlyListMethods<object>.IID,
            Vtable = IReadOnlyListMethods<object>.AbiToProjectionVftablePtr
          },
          new ComWrappers.ComInterfaceEntry
          {
            IID = IEnumerableMethods<string>.IID,
            Vtable = IEnumerableMethods<string>.AbiToProjectionVftablePtr
          },
          new ComWrappers.ComInterfaceEntry
          {
            IID = IEnumerableMethods<IEnumerable<char>>.IID,
            Vtable = IEnumerableMethods<IEnumerable<char>>.AbiToProjectionVftablePtr
          },
          new ComWrappers.ComInterfaceEntry
          {
            IID = IEnumerableMethods<IEnumerable<object>>.IID,
            Vtable = IEnumerableMethods<IEnumerable<object>>.AbiToProjectionVftablePtr
          },
          new ComWrappers.ComInterfaceEntry
          {
            IID = IEnumerableMethods<IEnumerable>.IID,
            Vtable = IEnumerableMethods<IEnumerable>.AbiToProjectionVftablePtr
          },
          new ComWrappers.ComInterfaceEntry
          {
            IID = IEnumerableMethods<object>.IID,
            Vtable = IEnumerableMethods<object>.AbiToProjectionVftablePtr
          },
          new ComWrappers.ComInterfaceEntry
          {
            IID = IListMethods.IID,
            Vtable = IListMethods.AbiToProjectionVftablePtr
          },
          new ComWrappers.ComInterfaceEntry
          {
            IID = IEnumerableMethods.IID,
            Vtable = IEnumerableMethods.AbiToProjectionVftablePtr
          }
        };
      }
      if (text == "ABI.System.Collections.Generic.ToAbiEnumeratorAdapter`1[Microsoft.UI.Composition.IVisualElement]")
      {
        bool initialized102 = IEnumerator_Microsoft_UI_Composition_IVisualElement.Initialized;
        return new ComWrappers.ComInterfaceEntry[]
        {
          new ComWrappers.ComInterfaceEntry
          {
            IID = IEnumeratorMethods<IVisualElement>.IID,
            Vtable = IEnumeratorMethods<IVisualElement>.AbiToProjectionVftablePtr
          },
          new ComWrappers.ComInterfaceEntry
          {
            IID = IDisposableMethods.IID,
            Vtable = IDisposableMethods.AbiToProjectionVftablePtr
          }
        };
      }
      if (text == "ABI.System.Collections.Generic.ConstantSplittableMap`2[System.String,System.Int32]")
      {
        bool initialized103 = IReadOnlyDictionary_string_int.Initialized;
        bool initialized104 = KeyValuePair_string_int.Initialized;
        bool initialized105 = IEnumerable_System_Collections_Generic_KeyValuePair_string__int_.Initialized;
        bool initialized106 = IEnumerable_object.Initialized;
        return new ComWrappers.ComInterfaceEntry[]
        {
          new ComWrappers.ComInterfaceEntry
          {
            IID = IReadOnlyDictionaryMethods<string, int>.IID,
            Vtable = IReadOnlyDictionaryMethods<string, int>.AbiToProjectionVftablePtr
          },
          new ComWrappers.ComInterfaceEntry
          {
            IID = IEnumerableMethods<KeyValuePair<string, int>>.IID,
            Vtable = IEnumerableMethods<KeyValuePair<string, int>>.AbiToProjectionVftablePtr
          },
          new ComWrappers.ComInterfaceEntry
          {
            IID = IEnumerableMethods<object>.IID,
            Vtable = IEnumerableMethods<object>.AbiToProjectionVftablePtr
          },
          new ComWrappers.ComInterfaceEntry
          {
            IID = IEnumerableMethods.IID,
            Vtable = IEnumerableMethods.AbiToProjectionVftablePtr
          }
        };
      }
      if (text == "System.Collections.Generic.KeyValuePair`2[System.Int32,System.String]")
      {
        bool initialized107 = KeyValuePair_int_string.Initialized;
        return new ComWrappers.ComInterfaceEntry[]
        {
          new ComWrappers.ComInterfaceEntry
          {
            IID = KeyValuePairMethods<int, string>.IID,
            Vtable = KeyValuePairMethods<int, string>.AbiToProjectionVftablePtr
          }
        };
      }
      if (text == "ABI.System.Collections.Generic.ToAbiEnumeratorAdapter`1[System.String]")
      {
        bool initialized108 = IEnumerator_string.Initialized;
        bool initialized109 = IEnumerable_char.Initialized;
        bool initialized110 = IEnumerator_System_Collections_Generic_IEnumerable_char_.Initialized;
        bool initialized111 = IEnumerable_object.Initialized;
        bool initialized112 = IEnumerator_System_Collections_Generic_IEnumerable_object_.Initialized;
        bool initialized113 = IEnumerator_System_Collections_IEnumerable.Initialized;
        bool initialized114 = IEnumerator_object.Initialized;
        return new ComWrappers.ComInterfaceEntry[]
        {
          new ComWrappers.ComInterfaceEntry
          {
            IID = IEnumeratorMethods<string>.IID,
            Vtable = IEnumeratorMethods<string>.AbiToProjectionVftablePtr
          },
          new ComWrappers.ComInterfaceEntry
          {
            IID = IEnumeratorMethods<IEnumerable<char>>.IID,
            Vtable = IEnumeratorMethods<IEnumerable<char>>.AbiToProjectionVftablePtr
          },
          new ComWrappers.ComInterfaceEntry
          {
            IID = IEnumeratorMethods<IEnumerable<object>>.IID,
            Vtable = IEnumeratorMethods<IEnumerable<object>>.AbiToProjectionVftablePtr
          },
          new ComWrappers.ComInterfaceEntry
          {
            IID = IEnumeratorMethods<IEnumerable>.IID,
            Vtable = IEnumeratorMethods<IEnumerable>.AbiToProjectionVftablePtr
          },
          new ComWrappers.ComInterfaceEntry
          {
            IID = IEnumeratorMethods<object>.IID,
            Vtable = IEnumeratorMethods<object>.AbiToProjectionVftablePtr
          },
          new ComWrappers.ComInterfaceEntry
          {
            IID = IDisposableMethods.IID,
            Vtable = IDisposableMethods.AbiToProjectionVftablePtr
          }
        };
      }
      if (text == "Microsoft.UI.Xaml.FrameworkElement[]")
      {
        bool initialized115 = IList_Microsoft_UI_Xaml_FrameworkElement.Initialized;
        bool initialized116 = IReadOnlyList_Microsoft_UI_Xaml_FrameworkElement.Initialized;
        bool initialized117 = IReadOnlyList_Microsoft_UI_Composition_IAnimationObject.Initialized;
        bool initialized118 = IReadOnlyList_Microsoft_UI_Composition_IVisualElement.Initialized;
        bool initialized119 = IReadOnlyList_Microsoft_UI_Composition_IVisualElement2.Initialized;
        bool initialized120 = IReadOnlyList_Microsoft_UI_Xaml_UIElement.Initialized;
        bool initialized121 = IReadOnlyList_Microsoft_UI_Xaml_DependencyObject.Initialized;
        bool initialized122 = IReadOnlyList_object.Initialized;
        bool initialized123 = IEnumerable_Microsoft_UI_Xaml_FrameworkElement.Initialized;
        bool initialized124 = IEnumerable_Microsoft_UI_Composition_IAnimationObject.Initialized;
        bool initialized125 = IEnumerable_Microsoft_UI_Composition_IVisualElement.Initialized;
        bool initialized126 = IEnumerable_Microsoft_UI_Composition_IVisualElement2.Initialized;
        bool initialized127 = IEnumerable_Microsoft_UI_Xaml_UIElement.Initialized;
        bool initialized128 = IEnumerable_Microsoft_UI_Xaml_DependencyObject.Initialized;
        bool initialized129 = IEnumerable_object.Initialized;
        return new ComWrappers.ComInterfaceEntry[]
        {
          new ComWrappers.ComInterfaceEntry
          {
            IID = IListMethods.IID,
            Vtable = IListMethods.AbiToProjectionVftablePtr
          },
          new ComWrappers.ComInterfaceEntry
          {
            IID = IListMethods<FrameworkElement>.IID,
            Vtable = IListMethods<FrameworkElement>.AbiToProjectionVftablePtr
          },
          new ComWrappers.ComInterfaceEntry
          {
            IID = IReadOnlyListMethods<FrameworkElement>.IID,
            Vtable = IReadOnlyListMethods<FrameworkElement>.AbiToProjectionVftablePtr
          },
          new ComWrappers.ComInterfaceEntry
          {
            IID = IReadOnlyListMethods<IAnimationObject>.IID,
            Vtable = IReadOnlyListMethods<IAnimationObject>.AbiToProjectionVftablePtr
          },
          new ComWrappers.ComInterfaceEntry
          {
            IID = IReadOnlyListMethods<IVisualElement>.IID,
            Vtable = IReadOnlyListMethods<IVisualElement>.AbiToProjectionVftablePtr
          },
          new ComWrappers.ComInterfaceEntry
          {
            IID = IReadOnlyListMethods<IVisualElement2>.IID,
            Vtable = IReadOnlyListMethods<IVisualElement2>.AbiToProjectionVftablePtr
          },
          new ComWrappers.ComInterfaceEntry
          {
            IID = IReadOnlyListMethods<UIElement>.IID,
            Vtable = IReadOnlyListMethods<UIElement>.AbiToProjectionVftablePtr
          },
          new ComWrappers.ComInterfaceEntry
          {
            IID = IReadOnlyListMethods<DependencyObject>.IID,
            Vtable = IReadOnlyListMethods<DependencyObject>.AbiToProjectionVftablePtr
          },
          new ComWrappers.ComInterfaceEntry
          {
            IID = IReadOnlyListMethods<object>.IID,
            Vtable = IReadOnlyListMethods<object>.AbiToProjectionVftablePtr
          },
          new ComWrappers.ComInterfaceEntry
          {
            IID = IEnumerableMethods<FrameworkElement>.IID,
            Vtable = IEnumerableMethods<FrameworkElement>.AbiToProjectionVftablePtr
          },
          new ComWrappers.ComInterfaceEntry
          {
            IID = IEnumerableMethods<IAnimationObject>.IID,
            Vtable = IEnumerableMethods<IAnimationObject>.AbiToProjectionVftablePtr
          },
          new ComWrappers.ComInterfaceEntry
          {
            IID = IEnumerableMethods<IVisualElement>.IID,
            Vtable = IEnumerableMethods<IVisualElement>.AbiToProjectionVftablePtr
          },
          new ComWrappers.ComInterfaceEntry
          {
            IID = IEnumerableMethods<IVisualElement2>.IID,
            Vtable = IEnumerableMethods<IVisualElement2>.AbiToProjectionVftablePtr
          },
          new ComWrappers.ComInterfaceEntry
          {
            IID = IEnumerableMethods<UIElement>.IID,
            Vtable = IEnumerableMethods<UIElement>.AbiToProjectionVftablePtr
          },
          new ComWrappers.ComInterfaceEntry
          {
            IID = IEnumerableMethods<DependencyObject>.IID,
            Vtable = IEnumerableMethods<DependencyObject>.AbiToProjectionVftablePtr
          },
          new ComWrappers.ComInterfaceEntry
          {
            IID = IEnumerableMethods<object>.IID,
            Vtable = IEnumerableMethods<object>.AbiToProjectionVftablePtr
          },
          new ComWrappers.ComInterfaceEntry
          {
            IID = IEnumerableMethods.IID,
            Vtable = IEnumerableMethods.AbiToProjectionVftablePtr
          }
        };
      }
      if (text == "ABI.System.Collections.Generic.ToAbiEnumeratorAdapter`1[System.Byte]")
      {
        bool initialized130 = IEnumerator_byte.Initialized;
        bool initialized131 = IEnumerator_object.Initialized;
        return new ComWrappers.ComInterfaceEntry[]
        {
          new ComWrappers.ComInterfaceEntry
          {
            IID = IEnumeratorMethods<byte>.IID,
            Vtable = IEnumeratorMethods<byte>.AbiToProjectionVftablePtr
          },
          new ComWrappers.ComInterfaceEntry
          {
            IID = IEnumeratorMethods<object>.IID,
            Vtable = IEnumeratorMethods<object>.AbiToProjectionVftablePtr
          },
          new ComWrappers.ComInterfaceEntry
          {
            IID = IDisposableMethods.IID,
            Vtable = IDisposableMethods.AbiToProjectionVftablePtr
          }
        };
      }
      if (text == "ABI.System.Collections.Generic.ToAbiEnumeratorAdapter`1[System.DateTimeOffset]")
      {
        bool initialized132 = IEnumerator_System_DateTimeOffset.Initialized;
        bool initialized133 = IEnumerator_object.Initialized;
        return new ComWrappers.ComInterfaceEntry[]
        {
          new ComWrappers.ComInterfaceEntry
          {
            IID = IEnumeratorMethods<DateTimeOffset>.IID,
            Vtable = IEnumeratorMethods<DateTimeOffset>.AbiToProjectionVftablePtr
          },
          new ComWrappers.ComInterfaceEntry
          {
            IID = IEnumeratorMethods<object>.IID,
            Vtable = IEnumeratorMethods<object>.AbiToProjectionVftablePtr
          },
          new ComWrappers.ComInterfaceEntry
          {
            IID = IDisposableMethods.IID,
            Vtable = IDisposableMethods.AbiToProjectionVftablePtr
          }
        };
      }
      if (text == "System.Collections.Generic.Dictionary`2+KeyCollection[System.String,System.String]" || text == "System.Collections.Generic.HashSet`1[System.String]" || text == "System.Collections.Generic.Dictionary`2+KeyCollection[System.String,System.Char]")
      {
        bool initialized134 = IEnumerable_string.Initialized;
        bool initialized135 = IEnumerable_char.Initialized;
        bool initialized136 = IEnumerable_System_Collections_Generic_IEnumerable_char_.Initialized;
        bool initialized137 = IEnumerable_object.Initialized;
        bool initialized138 = IEnumerable_System_Collections_Generic_IEnumerable_object_.Initialized;
        bool initialized139 = IEnumerable_System_Collections_IEnumerable.Initialized;
        return new ComWrappers.ComInterfaceEntry[]
        {
          new ComWrappers.ComInterfaceEntry
          {
            IID = IEnumerableMethods<string>.IID,
            Vtable = IEnumerableMethods<string>.AbiToProjectionVftablePtr
          },
          new ComWrappers.ComInterfaceEntry
          {
            IID = IEnumerableMethods<IEnumerable<char>>.IID,
            Vtable = IEnumerableMethods<IEnumerable<char>>.AbiToProjectionVftablePtr
          },
          new ComWrappers.ComInterfaceEntry
          {
            IID = IEnumerableMethods<IEnumerable<object>>.IID,
            Vtable = IEnumerableMethods<IEnumerable<object>>.AbiToProjectionVftablePtr
          },
          new ComWrappers.ComInterfaceEntry
          {
            IID = IEnumerableMethods<IEnumerable>.IID,
            Vtable = IEnumerableMethods<IEnumerable>.AbiToProjectionVftablePtr
          },
          new ComWrappers.ComInterfaceEntry
          {
            IID = IEnumerableMethods<object>.IID,
            Vtable = IEnumerableMethods<object>.AbiToProjectionVftablePtr
          },
          new ComWrappers.ComInterfaceEntry
          {
            IID = IEnumerableMethods.IID,
            Vtable = IEnumerableMethods.AbiToProjectionVftablePtr
          }
        };
      }
      if (text == "System.Collections.ObjectModel.ReadOnlyCollection`1[System.DateTimeOffset]" || text == "System.Collections.Generic.List`1[System.DateTimeOffset]")
      {
        bool initialized140 = IList_System_DateTimeOffset.Initialized;
        bool initialized141 = IReadOnlyList_System_DateTimeOffset.Initialized;
        bool initialized142 = IReadOnlyList_object.Initialized;
        bool initialized143 = IEnumerable_System_DateTimeOffset.Initialized;
        bool initialized144 = IEnumerable_object.Initialized;
        return new ComWrappers.ComInterfaceEntry[]
        {
          new ComWrappers.ComInterfaceEntry
          {
            IID = IListMethods<DateTimeOffset>.IID,
            Vtable = IListMethods<DateTimeOffset>.AbiToProjectionVftablePtr
          },
          new ComWrappers.ComInterfaceEntry
          {
            IID = IReadOnlyListMethods<DateTimeOffset>.IID,
            Vtable = IReadOnlyListMethods<DateTimeOffset>.AbiToProjectionVftablePtr
          },
          new ComWrappers.ComInterfaceEntry
          {
            IID = IReadOnlyListMethods<object>.IID,
            Vtable = IReadOnlyListMethods<object>.AbiToProjectionVftablePtr
          },
          new ComWrappers.ComInterfaceEntry
          {
            IID = IEnumerableMethods<DateTimeOffset>.IID,
            Vtable = IEnumerableMethods<DateTimeOffset>.AbiToProjectionVftablePtr
          },
          new ComWrappers.ComInterfaceEntry
          {
            IID = IEnumerableMethods<object>.IID,
            Vtable = IEnumerableMethods<object>.AbiToProjectionVftablePtr
          },
          new ComWrappers.ComInterfaceEntry
          {
            IID = IListMethods.IID,
            Vtable = IListMethods.AbiToProjectionVftablePtr
          },
          new ComWrappers.ComInterfaceEntry
          {
            IID = IEnumerableMethods.IID,
            Vtable = IEnumerableMethods.AbiToProjectionVftablePtr
          }
        };
      }
      if (text == "System.Collections.ObjectModel.ReadOnlyCollection`1[Microsoft.UI.Xaml.UIElement]" || text == "System.Collections.Generic.List`1[Microsoft.UI.Xaml.UIElement]")
      {
        bool initialized145 = IList_Microsoft_UI_Xaml_UIElement.Initialized;
        bool initialized146 = IReadOnlyList_Microsoft_UI_Xaml_UIElement.Initialized;
        bool initialized147 = IReadOnlyList_Microsoft_UI_Composition_IAnimationObject.Initialized;
        bool initialized148 = IReadOnlyList_Microsoft_UI_Composition_IVisualElement.Initialized;
        bool initialized149 = IReadOnlyList_Microsoft_UI_Composition_IVisualElement2.Initialized;
        bool initialized150 = IReadOnlyList_Microsoft_UI_Xaml_DependencyObject.Initialized;
        bool initialized151 = IReadOnlyList_object.Initialized;
        bool initialized152 = IEnumerable_Microsoft_UI_Xaml_UIElement.Initialized;
        bool initialized153 = IEnumerable_Microsoft_UI_Composition_IAnimationObject.Initialized;
        bool initialized154 = IEnumerable_Microsoft_UI_Composition_IVisualElement.Initialized;
        bool initialized155 = IEnumerable_Microsoft_UI_Composition_IVisualElement2.Initialized;
        bool initialized156 = IEnumerable_Microsoft_UI_Xaml_DependencyObject.Initialized;
        bool initialized157 = IEnumerable_object.Initialized;
        return new ComWrappers.ComInterfaceEntry[]
        {
          new ComWrappers.ComInterfaceEntry
          {
            IID = IListMethods<UIElement>.IID,
            Vtable = IListMethods<UIElement>.AbiToProjectionVftablePtr
          },
          new ComWrappers.ComInterfaceEntry
          {
            IID = IReadOnlyListMethods<UIElement>.IID,
            Vtable = IReadOnlyListMethods<UIElement>.AbiToProjectionVftablePtr
          },
          new ComWrappers.ComInterfaceEntry
          {
            IID = IReadOnlyListMethods<IAnimationObject>.IID,
            Vtable = IReadOnlyListMethods<IAnimationObject>.AbiToProjectionVftablePtr
          },
          new ComWrappers.ComInterfaceEntry
          {
            IID = IReadOnlyListMethods<IVisualElement>.IID,
            Vtable = IReadOnlyListMethods<IVisualElement>.AbiToProjectionVftablePtr
          },
          new ComWrappers.ComInterfaceEntry
          {
            IID = IReadOnlyListMethods<IVisualElement2>.IID,
            Vtable = IReadOnlyListMethods<IVisualElement2>.AbiToProjectionVftablePtr
          },
          new ComWrappers.ComInterfaceEntry
          {
            IID = IReadOnlyListMethods<DependencyObject>.IID,
            Vtable = IReadOnlyListMethods<DependencyObject>.AbiToProjectionVftablePtr
          },
          new ComWrappers.ComInterfaceEntry
          {
            IID = IReadOnlyListMethods<object>.IID,
            Vtable = IReadOnlyListMethods<object>.AbiToProjectionVftablePtr
          },
          new ComWrappers.ComInterfaceEntry
          {
            IID = IEnumerableMethods<UIElement>.IID,
            Vtable = IEnumerableMethods<UIElement>.AbiToProjectionVftablePtr
          },
          new ComWrappers.ComInterfaceEntry
          {
            IID = IEnumerableMethods<IAnimationObject>.IID,
            Vtable = IEnumerableMethods<IAnimationObject>.AbiToProjectionVftablePtr
          },
          new ComWrappers.ComInterfaceEntry
          {
            IID = IEnumerableMethods<IVisualElement>.IID,
            Vtable = IEnumerableMethods<IVisualElement>.AbiToProjectionVftablePtr
          },
          new ComWrappers.ComInterfaceEntry
          {
            IID = IEnumerableMethods<IVisualElement2>.IID,
            Vtable = IEnumerableMethods<IVisualElement2>.AbiToProjectionVftablePtr
          },
          new ComWrappers.ComInterfaceEntry
          {
            IID = IEnumerableMethods<DependencyObject>.IID,
            Vtable = IEnumerableMethods<DependencyObject>.AbiToProjectionVftablePtr
          },
          new ComWrappers.ComInterfaceEntry
          {
            IID = IEnumerableMethods<object>.IID,
            Vtable = IEnumerableMethods<object>.AbiToProjectionVftablePtr
          },
          new ComWrappers.ComInterfaceEntry
          {
            IID = IListMethods.IID,
            Vtable = IListMethods.AbiToProjectionVftablePtr
          },
          new ComWrappers.ComInterfaceEntry
          {
            IID = IEnumerableMethods.IID,
            Vtable = IEnumerableMethods.AbiToProjectionVftablePtr
          }
        };
      }
      if (text == "System.Byte[][]")
      {
        bool initialized158 = IReadOnlyList_System_Collections_IList.Initialized;
        bool initialized159 = IList_byte.Initialized;
        bool initialized160 = IReadOnlyList_System_Collections_Generic_IList_byte_.Initialized;
        bool initialized161 = IReadOnlyList_byte.Initialized;
        bool initialized162 = IReadOnlyList_System_Collections_Generic_IReadOnlyList_byte_.Initialized;
        bool initialized163 = IReadOnlyList_object.Initialized;
        bool initialized164 = IReadOnlyList_System_Collections_Generic_IReadOnlyList_object_.Initialized;
        bool initialized165 = IEnumerable_byte.Initialized;
        bool initialized166 = IReadOnlyList_System_Collections_Generic_IEnumerable_byte_.Initialized;
        bool initialized167 = IEnumerable_object.Initialized;
        bool initialized168 = IReadOnlyList_System_Collections_Generic_IEnumerable_object_.Initialized;
        bool initialized169 = IReadOnlyList_System_Collections_IEnumerable.Initialized;
        bool initialized170 = IEnumerable_System_Collections_IList.Initialized;
        bool initialized171 = IEnumerable_System_Collections_Generic_IList_byte_.Initialized;
        bool initialized172 = IEnumerable_System_Collections_Generic_IReadOnlyList_byte_.Initialized;
        bool initialized173 = IEnumerable_System_Collections_Generic_IReadOnlyList_object_.Initialized;
        bool initialized174 = IEnumerable_System_Collections_Generic_IEnumerable_byte_.Initialized;
        bool initialized175 = IEnumerable_System_Collections_Generic_IEnumerable_object_.Initialized;
        bool initialized176 = IEnumerable_System_Collections_IEnumerable.Initialized;
        return new ComWrappers.ComInterfaceEntry[]
        {
          new ComWrappers.ComInterfaceEntry
          {
            IID = IListMethods.IID,
            Vtable = IListMethods.AbiToProjectionVftablePtr
          },
          new ComWrappers.ComInterfaceEntry
          {
            IID = IReadOnlyListMethods<IList>.IID,
            Vtable = IReadOnlyListMethods<IList>.AbiToProjectionVftablePtr
          },
          new ComWrappers.ComInterfaceEntry
          {
            IID = IReadOnlyListMethods<IList<byte>>.IID,
            Vtable = IReadOnlyListMethods<IList<byte>>.AbiToProjectionVftablePtr
          },
          new ComWrappers.ComInterfaceEntry
          {
            IID = IReadOnlyListMethods<IReadOnlyList<byte>>.IID,
            Vtable = IReadOnlyListMethods<IReadOnlyList<byte>>.AbiToProjectionVftablePtr
          },
          new ComWrappers.ComInterfaceEntry
          {
            IID = IReadOnlyListMethods<IReadOnlyList<object>>.IID,
            Vtable = IReadOnlyListMethods<IReadOnlyList<object>>.AbiToProjectionVftablePtr
          },
          new ComWrappers.ComInterfaceEntry
          {
            IID = IReadOnlyListMethods<IEnumerable<byte>>.IID,
            Vtable = IReadOnlyListMethods<IEnumerable<byte>>.AbiToProjectionVftablePtr
          },
          new ComWrappers.ComInterfaceEntry
          {
            IID = IReadOnlyListMethods<IEnumerable<object>>.IID,
            Vtable = IReadOnlyListMethods<IEnumerable<object>>.AbiToProjectionVftablePtr
          },
          new ComWrappers.ComInterfaceEntry
          {
            IID = IReadOnlyListMethods<IEnumerable>.IID,
            Vtable = IReadOnlyListMethods<IEnumerable>.AbiToProjectionVftablePtr
          },
          new ComWrappers.ComInterfaceEntry
          {
            IID = IReadOnlyListMethods<object>.IID,
            Vtable = IReadOnlyListMethods<object>.AbiToProjectionVftablePtr
          },
          new ComWrappers.ComInterfaceEntry
          {
            IID = IEnumerableMethods<IList>.IID,
            Vtable = IEnumerableMethods<IList>.AbiToProjectionVftablePtr
          },
          new ComWrappers.ComInterfaceEntry
          {
            IID = IEnumerableMethods<IList<byte>>.IID,
            Vtable = IEnumerableMethods<IList<byte>>.AbiToProjectionVftablePtr
          },
          new ComWrappers.ComInterfaceEntry
          {
            IID = IEnumerableMethods<IReadOnlyList<byte>>.IID,
            Vtable = IEnumerableMethods<IReadOnlyList<byte>>.AbiToProjectionVftablePtr
          },
          new ComWrappers.ComInterfaceEntry
          {
            IID = IEnumerableMethods<IReadOnlyList<object>>.IID,
            Vtable = IEnumerableMethods<IReadOnlyList<object>>.AbiToProjectionVftablePtr
          },
          new ComWrappers.ComInterfaceEntry
          {
            IID = IEnumerableMethods<IEnumerable<byte>>.IID,
            Vtable = IEnumerableMethods<IEnumerable<byte>>.AbiToProjectionVftablePtr
          },
          new ComWrappers.ComInterfaceEntry
          {
            IID = IEnumerableMethods<IEnumerable<object>>.IID,
            Vtable = IEnumerableMethods<IEnumerable<object>>.AbiToProjectionVftablePtr
          },
          new ComWrappers.ComInterfaceEntry
          {
            IID = IEnumerableMethods<IEnumerable>.IID,
            Vtable = IEnumerableMethods<IEnumerable>.AbiToProjectionVftablePtr
          },
          new ComWrappers.ComInterfaceEntry
          {
            IID = IEnumerableMethods<object>.IID,
            Vtable = IEnumerableMethods<object>.AbiToProjectionVftablePtr
          },
          new ComWrappers.ComInterfaceEntry
          {
            IID = IEnumerableMethods.IID,
            Vtable = IEnumerableMethods.AbiToProjectionVftablePtr
          }
        };
      }
      if (text == "System.Collections.Generic.KeyValuePair`2[System.String,System.Int32]")
      {
        bool initialized177 = KeyValuePair_string_int.Initialized;
        return new ComWrappers.ComInterfaceEntry[]
        {
          new ComWrappers.ComInterfaceEntry
          {
            IID = KeyValuePairMethods<string, int>.IID,
            Vtable = KeyValuePairMethods<string, int>.AbiToProjectionVftablePtr
          }
        };
      }
      if (text == "System.Collections.ObjectModel.ReadOnlyCollection`1[HeyClicky.App.Notch.Peek.INotchDocumentRenderer]" || text == "System.Collections.Generic.List`1[HeyClicky.App.Notch.Peek.INotchDocumentRenderer]" || text == "System.Collections.Generic.List`1[HeyClicky.App.Drive.IAgentDriveJob]" || text == "System.Collections.ObjectModel.ReadOnlyCollection`1[HeyClicky.App.Drive.IAgentDriveJob]")
      {
        return new ComWrappers.ComInterfaceEntry[]
        {
          new ComWrappers.ComInterfaceEntry
          {
            IID = IListMethods.IID,
            Vtable = IListMethods.AbiToProjectionVftablePtr
          },
          new ComWrappers.ComInterfaceEntry
          {
            IID = IEnumerableMethods.IID,
            Vtable = IEnumerableMethods.AbiToProjectionVftablePtr
          }
        };
      }
      if (text == "ABI.System.Collections.Generic.ToAbiEnumeratorAdapter`1[System.Guid]")
      {
        bool initialized178 = IEnumerator_System_Guid.Initialized;
        bool initialized179 = IEnumerator_object.Initialized;
        return new ComWrappers.ComInterfaceEntry[]
        {
          new ComWrappers.ComInterfaceEntry
          {
            IID = IEnumeratorMethods<Guid>.IID,
            Vtable = IEnumeratorMethods<Guid>.AbiToProjectionVftablePtr
          },
          new ComWrappers.ComInterfaceEntry
          {
            IID = IEnumeratorMethods<object>.IID,
            Vtable = IEnumeratorMethods<object>.AbiToProjectionVftablePtr
          },
          new ComWrappers.ComInterfaceEntry
          {
            IID = IDisposableMethods.IID,
            Vtable = IDisposableMethods.AbiToProjectionVftablePtr
          }
        };
      }
      if (text == "ABI.System.Collections.Generic.ToAbiEnumeratorAdapter`1[System.Collections.Generic.IList`1[System.Byte]]")
      {
        bool initialized180 = IList_byte.Initialized;
        bool initialized181 = IEnumerator_System_Collections_Generic_IList_byte_.Initialized;
        bool initialized182 = IEnumerable_byte.Initialized;
        bool initialized183 = IEnumerator_System_Collections_Generic_IEnumerable_byte_.Initialized;
        bool initialized184 = IEnumerable_object.Initialized;
        bool initialized185 = IEnumerator_System_Collections_Generic_IEnumerable_object_.Initialized;
        bool initialized186 = IEnumerator_System_Collections_IEnumerable.Initialized;
        return new ComWrappers.ComInterfaceEntry[]
        {
          new ComWrappers.ComInterfaceEntry
          {
            IID = IEnumeratorMethods<IList<byte>>.IID,
            Vtable = IEnumeratorMethods<IList<byte>>.AbiToProjectionVftablePtr
          },
          new ComWrappers.ComInterfaceEntry
          {
            IID = IEnumeratorMethods<IEnumerable<byte>>.IID,
            Vtable = IEnumeratorMethods<IEnumerable<byte>>.AbiToProjectionVftablePtr
          },
          new ComWrappers.ComInterfaceEntry
          {
            IID = IEnumeratorMethods<IEnumerable<object>>.IID,
            Vtable = IEnumeratorMethods<IEnumerable<object>>.AbiToProjectionVftablePtr
          },
          new ComWrappers.ComInterfaceEntry
          {
            IID = IEnumeratorMethods<IEnumerable>.IID,
            Vtable = IEnumeratorMethods<IEnumerable>.AbiToProjectionVftablePtr
          },
          new ComWrappers.ComInterfaceEntry
          {
            IID = IDisposableMethods.IID,
            Vtable = IDisposableMethods.AbiToProjectionVftablePtr
          }
        };
      }
      if (text == "ABI.System.Collections.Generic.ToAbiEnumeratorAdapter`1[System.Collections.Generic.KeyValuePair`2[System.Int32,System.String]]")
      {
        bool initialized187 = KeyValuePair_int_string.Initialized;
        bool initialized188 = IEnumerator_System_Collections_Generic_KeyValuePair_int__string_.Initialized;
        bool initialized189 = IEnumerator_object.Initialized;
        return new ComWrappers.ComInterfaceEntry[]
        {
          new ComWrappers.ComInterfaceEntry
          {
            IID = IEnumeratorMethods<KeyValuePair<int, string>>.IID,
            Vtable = IEnumeratorMethods<KeyValuePair<int, string>>.AbiToProjectionVftablePtr
          },
          new ComWrappers.ComInterfaceEntry
          {
            IID = IEnumeratorMethods<object>.IID,
            Vtable = IEnumeratorMethods<object>.AbiToProjectionVftablePtr
          },
          new ComWrappers.ComInterfaceEntry
          {
            IID = IDisposableMethods.IID,
            Vtable = IDisposableMethods.AbiToProjectionVftablePtr
          }
        };
      }
      if (text == "System.Boolean[]")
      {
        bool initialized190 = IList_bool.Initialized;
        bool initialized191 = IReadOnlyList_bool.Initialized;
        bool initialized192 = IReadOnlyList_object.Initialized;
        bool initialized193 = IEnumerable_bool.Initialized;
        bool initialized194 = IEnumerable_object.Initialized;
        return new ComWrappers.ComInterfaceEntry[]
        {
          new ComWrappers.ComInterfaceEntry
          {
            IID = IListMethods.IID,
            Vtable = IListMethods.AbiToProjectionVftablePtr
          },
          new ComWrappers.ComInterfaceEntry
          {
            IID = IListMethods<bool>.IID,
            Vtable = IListMethods<bool>.AbiToProjectionVftablePtr
          },
          new ComWrappers.ComInterfaceEntry
          {
            IID = IReadOnlyListMethods<bool>.IID,
            Vtable = IReadOnlyListMethods<bool>.AbiToProjectionVftablePtr
          },
          new ComWrappers.ComInterfaceEntry
          {
            IID = IReadOnlyListMethods<object>.IID,
            Vtable = IReadOnlyListMethods<object>.AbiToProjectionVftablePtr
          },
          new ComWrappers.ComInterfaceEntry
          {
            IID = IEnumerableMethods<bool>.IID,
            Vtable = IEnumerableMethods<bool>.AbiToProjectionVftablePtr
          },
          new ComWrappers.ComInterfaceEntry
          {
            IID = IEnumerableMethods<object>.IID,
            Vtable = IEnumerableMethods<object>.AbiToProjectionVftablePtr
          },
          new ComWrappers.ComInterfaceEntry
          {
            IID = IEnumerableMethods.IID,
            Vtable = IEnumerableMethods.AbiToProjectionVftablePtr
          }
        };
      }
      if (text == "ABI.System.Collections.Generic.ToAbiEnumeratorAdapter`1[System.Collections.Generic.KeyValuePair`2[System.String,System.String]]")
      {
        bool initialized195 = KeyValuePair_string_string.Initialized;
        bool initialized196 = IEnumerator_System_Collections_Generic_KeyValuePair_string__string_.Initialized;
        bool initialized197 = IEnumerator_object.Initialized;
        return new ComWrappers.ComInterfaceEntry[]
        {
          new ComWrappers.ComInterfaceEntry
          {
            IID = IEnumeratorMethods<KeyValuePair<string, string>>.IID,
            Vtable = IEnumeratorMethods<KeyValuePair<string, string>>.AbiToProjectionVftablePtr
          },
          new ComWrappers.ComInterfaceEntry
          {
            IID = IEnumeratorMethods<object>.IID,
            Vtable = IEnumeratorMethods<object>.AbiToProjectionVftablePtr
          },
          new ComWrappers.ComInterfaceEntry
          {
            IID = IDisposableMethods.IID,
            Vtable = IDisposableMethods.AbiToProjectionVftablePtr
          }
        };
      }
      if (text == "ABI.System.Collections.Generic.ConstantSplittableMap`2[System.String,System.String]")
      {
        bool initialized198 = IReadOnlyDictionary_string_string.Initialized;
        bool initialized199 = KeyValuePair_string_string.Initialized;
        bool initialized200 = IEnumerable_System_Collections_Generic_KeyValuePair_string__string_.Initialized;
        bool initialized201 = IEnumerable_object.Initialized;
        return new ComWrappers.ComInterfaceEntry[]
        {
          new ComWrappers.ComInterfaceEntry
          {
            IID = IReadOnlyDictionaryMethods<string, string>.IID,
            Vtable = IReadOnlyDictionaryMethods<string, string>.AbiToProjectionVftablePtr
          },
          new ComWrappers.ComInterfaceEntry
          {
            IID = IEnumerableMethods<KeyValuePair<string, string>>.IID,
            Vtable = IEnumerableMethods<KeyValuePair<string, string>>.AbiToProjectionVftablePtr
          },
          new ComWrappers.ComInterfaceEntry
          {
            IID = IEnumerableMethods<object>.IID,
            Vtable = IEnumerableMethods<object>.AbiToProjectionVftablePtr
          },
          new ComWrappers.ComInterfaceEntry
          {
            IID = IEnumerableMethods.IID,
            Vtable = IEnumerableMethods.AbiToProjectionVftablePtr
          }
        };
      }
      if (text == "ABI.System.Collections.Generic.ConstantSplittableMap`2[System.Int32,System.String]")
      {
        bool initialized202 = IReadOnlyDictionary_int_string.Initialized;
        bool initialized203 = KeyValuePair_int_string.Initialized;
        bool initialized204 = IEnumerable_System_Collections_Generic_KeyValuePair_int__string_.Initialized;
        bool initialized205 = IEnumerable_object.Initialized;
        return new ComWrappers.ComInterfaceEntry[]
        {
          new ComWrappers.ComInterfaceEntry
          {
            IID = IReadOnlyDictionaryMethods<int, string>.IID,
            Vtable = IReadOnlyDictionaryMethods<int, string>.AbiToProjectionVftablePtr
          },
          new ComWrappers.ComInterfaceEntry
          {
            IID = IEnumerableMethods<KeyValuePair<int, string>>.IID,
            Vtable = IEnumerableMethods<KeyValuePair<int, string>>.AbiToProjectionVftablePtr
          },
          new ComWrappers.ComInterfaceEntry
          {
            IID = IEnumerableMethods<object>.IID,
            Vtable = IEnumerableMethods<object>.AbiToProjectionVftablePtr
          },
          new ComWrappers.ComInterfaceEntry
          {
            IID = IEnumerableMethods.IID,
            Vtable = IEnumerableMethods.AbiToProjectionVftablePtr
          }
        };
      }
      if (text == "ABI.System.Collections.Generic.ToAbiEnumeratorAdapter`1[System.Int32]")
      {
        bool initialized206 = IEnumerator_int.Initialized;
        bool initialized207 = IEnumerator_object.Initialized;
        return new ComWrappers.ComInterfaceEntry[]
        {
          new ComWrappers.ComInterfaceEntry
          {
            IID = IEnumeratorMethods<int>.IID,
            Vtable = IEnumeratorMethods<int>.AbiToProjectionVftablePtr
          },
          new ComWrappers.ComInterfaceEntry
          {
            IID = IEnumeratorMethods<object>.IID,
            Vtable = IEnumeratorMethods<object>.AbiToProjectionVftablePtr
          },
          new ComWrappers.ComInterfaceEntry
          {
            IID = IDisposableMethods.IID,
            Vtable = IDisposableMethods.AbiToProjectionVftablePtr
          }
        };
      }
      if (text == "ABI.System.Collections.Generic.ToAbiEnumeratorAdapter`1[Microsoft.UI.Composition.IVisualElement2]")
      {
        bool initialized208 = IEnumerator_Microsoft_UI_Composition_IVisualElement2.Initialized;
        return new ComWrappers.ComInterfaceEntry[]
        {
          new ComWrappers.ComInterfaceEntry
          {
            IID = IEnumeratorMethods<IVisualElement2>.IID,
            Vtable = IEnumeratorMethods<IVisualElement2>.AbiToProjectionVftablePtr
          },
          new ComWrappers.ComInterfaceEntry
          {
            IID = IDisposableMethods.IID,
            Vtable = IDisposableMethods.AbiToProjectionVftablePtr
          }
        };
      }
      if (text == "ABI.System.Collections.Generic.ToAbiEnumeratorAdapter`1[Windows.Foundation.Point]")
      {
        bool initialized209 = IEnumerator_Windows_Foundation_Point.Initialized;
        bool initialized210 = IEnumerator_object.Initialized;
        return new ComWrappers.ComInterfaceEntry[]
        {
          new ComWrappers.ComInterfaceEntry
          {
            IID = IEnumeratorMethods<Point>.IID,
            Vtable = IEnumeratorMethods<Point>.AbiToProjectionVftablePtr
          },
          new ComWrappers.ComInterfaceEntry
          {
            IID = IEnumeratorMethods<object>.IID,
            Vtable = IEnumeratorMethods<object>.AbiToProjectionVftablePtr
          },
          new ComWrappers.ComInterfaceEntry
          {
            IID = IDisposableMethods.IID,
            Vtable = IDisposableMethods.AbiToProjectionVftablePtr
          }
        };
      }
      if (text == "System.Byte[]")
      {
        bool initialized211 = IList_byte.Initialized;
        bool initialized212 = IReadOnlyList_byte.Initialized;
        bool initialized213 = IReadOnlyList_object.Initialized;
        bool initialized214 = IEnumerable_byte.Initialized;
        bool initialized215 = IEnumerable_object.Initialized;
        return new ComWrappers.ComInterfaceEntry[]
        {
          new ComWrappers.ComInterfaceEntry
          {
            IID = IListMethods.IID,
            Vtable = IListMethods.AbiToProjectionVftablePtr
          },
          new ComWrappers.ComInterfaceEntry
          {
            IID = IListMethods<byte>.IID,
            Vtable = IListMethods<byte>.AbiToProjectionVftablePtr
          },
          new ComWrappers.ComInterfaceEntry
          {
            IID = IReadOnlyListMethods<byte>.IID,
            Vtable = IReadOnlyListMethods<byte>.AbiToProjectionVftablePtr
          },
          new ComWrappers.ComInterfaceEntry
          {
            IID = IReadOnlyListMethods<object>.IID,
            Vtable = IReadOnlyListMethods<object>.AbiToProjectionVftablePtr
          },
          new ComWrappers.ComInterfaceEntry
          {
            IID = IEnumerableMethods<byte>.IID,
            Vtable = IEnumerableMethods<byte>.AbiToProjectionVftablePtr
          },
          new ComWrappers.ComInterfaceEntry
          {
            IID = IEnumerableMethods<object>.IID,
            Vtable = IEnumerableMethods<object>.AbiToProjectionVftablePtr
          },
          new ComWrappers.ComInterfaceEntry
          {
            IID = IEnumerableMethods.IID,
            Vtable = IEnumerableMethods.AbiToProjectionVftablePtr
          }
        };
      }
      if (text == "ABI.System.Collections.Generic.ToAbiEnumeratorAdapter`1[System.Collections.Generic.IReadOnlyList`1[System.Byte]]")
      {
        bool initialized216 = IReadOnlyList_byte.Initialized;
        bool initialized217 = IEnumerator_System_Collections_Generic_IReadOnlyList_byte_.Initialized;
        bool initialized218 = IEnumerable_byte.Initialized;
        bool initialized219 = IEnumerator_System_Collections_Generic_IEnumerable_byte_.Initialized;
        bool initialized220 = IEnumerable_object.Initialized;
        bool initialized221 = IEnumerator_System_Collections_Generic_IEnumerable_object_.Initialized;
        bool initialized222 = IEnumerator_System_Collections_IEnumerable.Initialized;
        return new ComWrappers.ComInterfaceEntry[]
        {
          new ComWrappers.ComInterfaceEntry
          {
            IID = IEnumeratorMethods<IReadOnlyList<byte>>.IID,
            Vtable = IEnumeratorMethods<IReadOnlyList<byte>>.AbiToProjectionVftablePtr
          },
          new ComWrappers.ComInterfaceEntry
          {
            IID = IEnumeratorMethods<IEnumerable<byte>>.IID,
            Vtable = IEnumeratorMethods<IEnumerable<byte>>.AbiToProjectionVftablePtr
          },
          new ComWrappers.ComInterfaceEntry
          {
            IID = IEnumeratorMethods<IEnumerable<object>>.IID,
            Vtable = IEnumeratorMethods<IEnumerable<object>>.AbiToProjectionVftablePtr
          },
          new ComWrappers.ComInterfaceEntry
          {
            IID = IEnumeratorMethods<IEnumerable>.IID,
            Vtable = IEnumeratorMethods<IEnumerable>.AbiToProjectionVftablePtr
          },
          new ComWrappers.ComInterfaceEntry
          {
            IID = IDisposableMethods.IID,
            Vtable = IDisposableMethods.AbiToProjectionVftablePtr
          }
        };
      }
      if (text == "ABI.System.Collections.Generic.ToAbiEnumeratorAdapter`1[System.Collections.Generic.IEnumerable`1[System.Object]]")
      {
        bool initialized223 = IEnumerable_object.Initialized;
        bool initialized224 = IEnumerator_System_Collections_Generic_IEnumerable_object_.Initialized;
        bool initialized225 = IEnumerator_System_Collections_IEnumerable.Initialized;
        return new ComWrappers.ComInterfaceEntry[]
        {
          new ComWrappers.ComInterfaceEntry
          {
            IID = IEnumeratorMethods<IEnumerable<object>>.IID,
            Vtable = IEnumeratorMethods<IEnumerable<object>>.AbiToProjectionVftablePtr
          },
          new ComWrappers.ComInterfaceEntry
          {
            IID = IEnumeratorMethods<IEnumerable>.IID,
            Vtable = IEnumeratorMethods<IEnumerable>.AbiToProjectionVftablePtr
          },
          new ComWrappers.ComInterfaceEntry
          {
            IID = IDisposableMethods.IID,
            Vtable = IDisposableMethods.AbiToProjectionVftablePtr
          }
        };
      }
      if (text == "ABI.System.Collections.Generic.ToAbiEnumeratorAdapter`1[System.Object]")
      {
        bool initialized226 = IEnumerator_object.Initialized;
        return new ComWrappers.ComInterfaceEntry[]
        {
          new ComWrappers.ComInterfaceEntry
          {
            IID = IEnumeratorMethods<object>.IID,
            Vtable = IEnumeratorMethods<object>.AbiToProjectionVftablePtr
          },
          new ComWrappers.ComInterfaceEntry
          {
            IID = IDisposableMethods.IID,
            Vtable = IDisposableMethods.AbiToProjectionVftablePtr
          }
        };
      }
      if (text == "System.Collections.Generic.List`1[System.Text.Json.Nodes.JsonObject]" || text == "System.Collections.ObjectModel.ReadOnlyCollection`1[System.Text.Json.Nodes.JsonObject]")
      {
        bool initialized227 = IEnumerable_object.Initialized;
        bool initialized228 = IReadOnlyList_System_Collections_Generic_IEnumerable_object_.Initialized;
        bool initialized229 = IReadOnlyList_System_Collections_IEnumerable.Initialized;
        bool initialized230 = IReadOnlyList_object.Initialized;
        bool initialized231 = IEnumerable_System_Collections_Generic_IEnumerable_object_.Initialized;
        bool initialized232 = IEnumerable_System_Collections_IEnumerable.Initialized;
        return new ComWrappers.ComInterfaceEntry[]
        {
          new ComWrappers.ComInterfaceEntry
          {
            IID = IReadOnlyListMethods<IEnumerable<object>>.IID,
            Vtable = IReadOnlyListMethods<IEnumerable<object>>.AbiToProjectionVftablePtr
          },
          new ComWrappers.ComInterfaceEntry
          {
            IID = IReadOnlyListMethods<IEnumerable>.IID,
            Vtable = IReadOnlyListMethods<IEnumerable>.AbiToProjectionVftablePtr
          },
          new ComWrappers.ComInterfaceEntry
          {
            IID = IReadOnlyListMethods<object>.IID,
            Vtable = IReadOnlyListMethods<object>.AbiToProjectionVftablePtr
          },
          new ComWrappers.ComInterfaceEntry
          {
            IID = IEnumerableMethods<IEnumerable<object>>.IID,
            Vtable = IEnumerableMethods<IEnumerable<object>>.AbiToProjectionVftablePtr
          },
          new ComWrappers.ComInterfaceEntry
          {
            IID = IEnumerableMethods<IEnumerable>.IID,
            Vtable = IEnumerableMethods<IEnumerable>.AbiToProjectionVftablePtr
          },
          new ComWrappers.ComInterfaceEntry
          {
            IID = IEnumerableMethods<object>.IID,
            Vtable = IEnumerableMethods<object>.AbiToProjectionVftablePtr
          },
          new ComWrappers.ComInterfaceEntry
          {
            IID = IListMethods.IID,
            Vtable = IListMethods.AbiToProjectionVftablePtr
          },
          new ComWrappers.ComInterfaceEntry
          {
            IID = IEnumerableMethods.IID,
            Vtable = IEnumerableMethods.AbiToProjectionVftablePtr
          }
        };
      }
      if (text == "ABI.System.Collections.Generic.ToAbiEnumeratorAdapter`1[System.Collections.Generic.IEnumerable`1[System.Char]]")
      {
        bool initialized233 = IEnumerable_char.Initialized;
        bool initialized234 = IEnumerator_System_Collections_Generic_IEnumerable_char_.Initialized;
        bool initialized235 = IEnumerator_System_Collections_IEnumerable.Initialized;
        return new ComWrappers.ComInterfaceEntry[]
        {
          new ComWrappers.ComInterfaceEntry
          {
            IID = IEnumeratorMethods<IEnumerable<char>>.IID,
            Vtable = IEnumeratorMethods<IEnumerable<char>>.AbiToProjectionVftablePtr
          },
          new ComWrappers.ComInterfaceEntry
          {
            IID = IEnumeratorMethods<IEnumerable>.IID,
            Vtable = IEnumeratorMethods<IEnumerable>.AbiToProjectionVftablePtr
          },
          new ComWrappers.ComInterfaceEntry
          {
            IID = IDisposableMethods.IID,
            Vtable = IDisposableMethods.AbiToProjectionVftablePtr
          }
        };
      }
      if (text == "System.Collections.Generic.HashSet`1[System.Int32]")
      {
        bool initialized236 = IEnumerable_int.Initialized;
        bool initialized237 = IEnumerable_object.Initialized;
        return new ComWrappers.ComInterfaceEntry[]
        {
          new ComWrappers.ComInterfaceEntry
          {
            IID = IEnumerableMethods<int>.IID,
            Vtable = IEnumerableMethods<int>.AbiToProjectionVftablePtr
          },
          new ComWrappers.ComInterfaceEntry
          {
            IID = IEnumerableMethods<object>.IID,
            Vtable = IEnumerableMethods<object>.AbiToProjectionVftablePtr
          },
          new ComWrappers.ComInterfaceEntry
          {
            IID = IEnumerableMethods.IID,
            Vtable = IEnumerableMethods.AbiToProjectionVftablePtr
          }
        };
      }
      if (text == "ABI.System.Collections.Generic.ConstantSplittableMap`2[System.String,System.Boolean]")
      {
        bool initialized238 = IReadOnlyDictionary_string_bool.Initialized;
        bool initialized239 = KeyValuePair_string_bool.Initialized;
        bool initialized240 = IEnumerable_System_Collections_Generic_KeyValuePair_string__bool_.Initialized;
        bool initialized241 = IEnumerable_object.Initialized;
        return new ComWrappers.ComInterfaceEntry[]
        {
          new ComWrappers.ComInterfaceEntry
          {
            IID = IReadOnlyDictionaryMethods<string, bool>.IID,
            Vtable = IReadOnlyDictionaryMethods<string, bool>.AbiToProjectionVftablePtr
          },
          new ComWrappers.ComInterfaceEntry
          {
            IID = IEnumerableMethods<KeyValuePair<string, bool>>.IID,
            Vtable = IEnumerableMethods<KeyValuePair<string, bool>>.AbiToProjectionVftablePtr
          },
          new ComWrappers.ComInterfaceEntry
          {
            IID = IEnumerableMethods<object>.IID,
            Vtable = IEnumerableMethods<object>.AbiToProjectionVftablePtr
          },
          new ComWrappers.ComInterfaceEntry
          {
            IID = IEnumerableMethods.IID,
            Vtable = IEnumerableMethods.AbiToProjectionVftablePtr
          }
        };
      }
      if (text == "ABI.System.Collections.Generic.ToAbiEnumeratorAdapter`1[System.Double]")
      {
        bool initialized242 = IEnumerator_double.Initialized;
        bool initialized243 = IEnumerator_object.Initialized;
        return new ComWrappers.ComInterfaceEntry[]
        {
          new ComWrappers.ComInterfaceEntry
          {
            IID = IEnumeratorMethods<double>.IID,
            Vtable = IEnumeratorMethods<double>.AbiToProjectionVftablePtr
          },
          new ComWrappers.ComInterfaceEntry
          {
            IID = IEnumeratorMethods<object>.IID,
            Vtable = IEnumeratorMethods<object>.AbiToProjectionVftablePtr
          },
          new ComWrappers.ComInterfaceEntry
          {
            IID = IDisposableMethods.IID,
            Vtable = IDisposableMethods.AbiToProjectionVftablePtr
          }
        };
      }
      if (text == "ABI.System.Collections.Generic.ToAbiEnumeratorAdapter`1[System.Collections.Generic.IReadOnlyList`1[System.Object]]")
      {
        bool initialized244 = IReadOnlyList_object.Initialized;
        bool initialized245 = IEnumerator_System_Collections_Generic_IReadOnlyList_object_.Initialized;
        bool initialized246 = IEnumerable_object.Initialized;
        bool initialized247 = IEnumerator_System_Collections_Generic_IEnumerable_object_.Initialized;
        bool initialized248 = IEnumerator_System_Collections_IEnumerable.Initialized;
        return new ComWrappers.ComInterfaceEntry[]
        {
          new ComWrappers.ComInterfaceEntry
          {
            IID = IEnumeratorMethods<IReadOnlyList<object>>.IID,
            Vtable = IEnumeratorMethods<IReadOnlyList<object>>.AbiToProjectionVftablePtr
          },
          new ComWrappers.ComInterfaceEntry
          {
            IID = IEnumeratorMethods<IEnumerable<object>>.IID,
            Vtable = IEnumeratorMethods<IEnumerable<object>>.AbiToProjectionVftablePtr
          },
          new ComWrappers.ComInterfaceEntry
          {
            IID = IEnumeratorMethods<IEnumerable>.IID,
            Vtable = IEnumeratorMethods<IEnumerable>.AbiToProjectionVftablePtr
          },
          new ComWrappers.ComInterfaceEntry
          {
            IID = IDisposableMethods.IID,
            Vtable = IDisposableMethods.AbiToProjectionVftablePtr
          }
        };
      }
      if (text == "System.Collections.Generic.KeyValuePair`2[System.String,System.Boolean]")
      {
        bool initialized249 = KeyValuePair_string_bool.Initialized;
        return new ComWrappers.ComInterfaceEntry[]
        {
          new ComWrappers.ComInterfaceEntry
          {
            IID = KeyValuePairMethods<string, bool>.IID,
            Vtable = KeyValuePairMethods<string, bool>.AbiToProjectionVftablePtr
          }
        };
      }
      if (text == "ABI.System.Collections.Generic.ToAbiEnumeratorAdapter`1[System.Boolean]")
      {
        bool initialized250 = IEnumerator_bool.Initialized;
        bool initialized251 = IEnumerator_object.Initialized;
        return new ComWrappers.ComInterfaceEntry[]
        {
          new ComWrappers.ComInterfaceEntry
          {
            IID = IEnumeratorMethods<bool>.IID,
            Vtable = IEnumeratorMethods<bool>.AbiToProjectionVftablePtr
          },
          new ComWrappers.ComInterfaceEntry
          {
            IID = IEnumeratorMethods<object>.IID,
            Vtable = IEnumeratorMethods<object>.AbiToProjectionVftablePtr
          },
          new ComWrappers.ComInterfaceEntry
          {
            IID = IDisposableMethods.IID,
            Vtable = IDisposableMethods.AbiToProjectionVftablePtr
          }
        };
      }
      if (text == "ABI.System.Collections.Generic.ToAbiEnumeratorAdapter`1[Microsoft.UI.Xaml.DependencyObject]")
      {
        bool initialized252 = IEnumerator_Microsoft_UI_Xaml_DependencyObject.Initialized;
        bool initialized253 = IEnumerator_object.Initialized;
        return new ComWrappers.ComInterfaceEntry[]
        {
          new ComWrappers.ComInterfaceEntry
          {
            IID = IEnumeratorMethods<DependencyObject>.IID,
            Vtable = IEnumeratorMethods<DependencyObject>.AbiToProjectionVftablePtr
          },
          new ComWrappers.ComInterfaceEntry
          {
            IID = IEnumeratorMethods<object>.IID,
            Vtable = IEnumeratorMethods<object>.AbiToProjectionVftablePtr
          },
          new ComWrappers.ComInterfaceEntry
          {
            IID = IDisposableMethods.IID,
            Vtable = IDisposableMethods.AbiToProjectionVftablePtr
          }
        };
      }
      if (text == "ABI.System.Collections.Generic.ToAbiEnumeratorAdapter`1[System.Collections.IEnumerable]")
      {
        bool initialized254 = IEnumerator_System_Collections_IEnumerable.Initialized;
        return new ComWrappers.ComInterfaceEntry[]
        {
          new ComWrappers.ComInterfaceEntry
          {
            IID = IEnumeratorMethods<IEnumerable>.IID,
            Vtable = IEnumeratorMethods<IEnumerable>.AbiToProjectionVftablePtr
          },
          new ComWrappers.ComInterfaceEntry
          {
            IID = IDisposableMethods.IID,
            Vtable = IDisposableMethods.AbiToProjectionVftablePtr
          }
        };
      }
      if (text == "System.Collections.Generic.KeyValuePair`2[System.String,System.String]")
      {
        bool initialized255 = KeyValuePair_string_string.Initialized;
        return new ComWrappers.ComInterfaceEntry[]
        {
          new ComWrappers.ComInterfaceEntry
          {
            IID = KeyValuePairMethods<string, string>.IID,
            Vtable = KeyValuePairMethods<string, string>.AbiToProjectionVftablePtr
          }
        };
      }
      return null;
    }

    // Token: 0x06000294 RID: 660 RVA: 0x0051A14C File Offset: 0x0051A14C
    private static string LookupRuntimeClassName(Type type)
    {
      string text = type.ToString();
      if (text == "System.Collections.ObjectModel.ReadOnlyCollection`1[Windows.Foundation.Point]" || text == "System.Collections.Generic.List`1[Windows.Foundation.Point]")
      {
        return "Windows.Foundation.Collections.IVector`1<Windows.Foundation.Point>";
      }
      if (text == "System.Collections.Generic.List`1[(System.Collections.Generic.List, System.Boolean)`2[System.Collections.Generic.List`1[Windows.Foundation.Point],System.Boolean]]" || text == "System.Collections.Generic.List`1[HeyClicky.App.Portraits.CloudFaceTraits+Blob]" || text == "System.Collections.Generic.List`1[HeyClicky.DisplayInfo]" || text == "System.Collections.Generic.List`1[HeyClicky.App.Portraits.RosterClicky]" || text == "System.Collections.Generic.List`1[HeyClicky.App.Settings.CustomConnector]" || text == "System.Collections.ObjectModel.ReadOnlyCollection`1[HeyClicky.App.Portraits.HairPiece]" || text == "System.Collections.Generic.List`1[HeyClicky.AgentTaskSnapshot]" || text == "System.Collections.Generic.List`1[HeyClicky.App.OneBrain.ClickyTourJob]" || text == "System.Collections.ObjectModel.ReadOnlyCollection`1[HeyClicky.Core.Tools.AgentTaskInfo]" || text == "System.Collections.Generic.List`1[HeyClicky.App.Dock.ArcRect]" || text == "System.Collections.Generic.List`1[HeyClicky.Core.Worker.InterviewLine]" || text == "System.Collections.ObjectModel.ReadOnlyCollection`1[HeyClicky.App.Settings.CustomConnector]" || text == "System.Collections.ObjectModel.ReadOnlyCollection`1[(System.String, HeyClicky.App.Ink.ClickyPaperFileKind)`2[System.String,HeyClicky.App.Ink.ClickyPaperFileKind]]" || text == "System.Collections.Generic.List`1[HeyClicky.Core.Ink.ClickyInkRoutineControl]" || text == "System.Collections.Generic.List`1[(System.String, HeyClicky.App.Ink.ClickyPaperFileKind)`2[System.String,HeyClicky.App.Ink.ClickyPaperFileKind]]" || text == "System.Collections.ObjectModel.ReadOnlyCollection`1[HeyClicky.Core.Tools.AgentClickyInfo]" || text == "System.Collections.Generic.List`1[HeyClicky.App.Home.LessonService+Recent]" || text == "System.Collections.Generic.List`1[HeyClicky.Core.Proactive.AgentNotification]" || text == "System.Collections.ObjectModel.ReadOnlyCollection`1[HeyClicky.Agents.AgentThreadSummary]" || text == "System.Collections.Generic.List`1[HeyClicky.Core.Tools.AgentThreadInfo]" || text == "System.Collections.Generic.List`1[HeyClicky.App.Services.AppEntry]" || text == "System.Collections.ObjectModel.ReadOnlyCollection`1[HeyClicky.Core.Proactive.AgentIntegrationInfo]" || text == "System.Collections.ObjectModel.ReadOnlyCollection`1[HeyClicky.Agents.Clickys.ClickyInfo]" || text == "System.Collections.ObjectModel.ReadOnlyCollection`1[HeyClicky.Core.Proactive.QueuedProactiveTask]" || text == "System.Collections.ObjectModel.ReadOnlyCollection`1[HeyClicky.DisplayInfo]" || text == "System.Collections.Generic.List`1[HeyClicky.App.Dock.ResultCardLayouts+Arrangement]" || text == "System.Collections.Generic.List`1[HeyClicky.Core.Proactive.ProactiveSuggestionCard]" || text == "System.Collections.ObjectModel.ReadOnlyCollection`1[HeyClicky.Core.Ink.ClickyInkDocument+Block]" || text == "System.Collections.ObjectModel.ReadOnlyCollection`1[HeyClicky.App.Onboarding.TutorialWindow+Step]" || text == "System.Collections.Generic.List`1[HeyClicky.Core.Tools.AgentClickyInfo]" || text == "System.Collections.ObjectModel.ReadOnlyCollection`1[HeyClicky.Core.Worker.InterviewLine]" || text == "System.Collections.ObjectModel.ReadOnlyCollection`1[HeyClicky.App.Portraits.StarterTaskCard]" || text == "System.Collections.Generic.List`1[HeyClicky.App.Dock.DockWindowKey]" || text == "System.Collections.ObjectModel.ReadOnlyCollection`1[HeyClicky.Core.Proactive.AgentNotification]" || text == "System.Collections.ObjectModel.ReadOnlyCollection`1[HeyClicky.App.Home.LessonService+Recent]" || text == "System.Collections.ObjectModel.ReadOnlyCollection`1[System.Collections.Generic.KeyValuePair`2[System.String,System.Text.Json.Nodes.JsonNode]]" || text == "System.Collections.Generic.List`1[HeyClicky.Agents.Clickys.ClickyInfo]" || text == "System.Collections.Generic.List`1[HeyClicky.Core.Notch.ChatHandOff]" || text == "System.Collections.ObjectModel.ReadOnlyCollection`1[HeyClicky.App.Services.AppEntry]" || text == "System.Collections.ObjectModel.ReadOnlyCollection`1[HeyClicky.Core.Ink.ClickyInkRoutineControl]" || text == "System.Collections.Generic.List`1[HeyClicky.App.Portraits.HairPiece]" || text == "System.Collections.Generic.List`1[HeyClicky.App.Home.StoredLesson]" || text == "System.Collections.Generic.List`1[HeyClicky.Agents.AgentThreadSummary]" || text == "System.Collections.Generic.List`1[HeyClicky.Core.Voice.VoiceLanguage]" || text == "System.Collections.Generic.List`1[HeyClicky.Agents.Runtime.CustomConnectorRegistration]" || text == "System.Collections.ObjectModel.ReadOnlyCollection`1[(System.String Role, System.String Text)`2[System.String,System.String]]" || text == "System.Collections.ObjectModel.ReadOnlyCollection`1[(System.String, System.String)`2[System.String,System.String]]" || text == "System.Collections.ObjectModel.ReadOnlyCollection`1[HeyClicky.Core.Tools.AgentThreadInfo]" || text == "System.Collections.Generic.List`1[(System.String Role, System.String Text)`2[System.String,System.String]]" || text == "System.Collections.ObjectModel.ReadOnlyCollection`1[HeyClicky.App.Dock.ResultCardLayouts+Arrangement]" || text == "System.Collections.Generic.List`1[HeyClicky.Core.Tools.AgentTaskInfo]" || text == "System.Collections.ObjectModel.ReadOnlyCollection`1[HeyClicky.App.OneBrain.ClickyTourJob]" || text == "System.Collections.Generic.List`1[(System.String, System.String, System.Func)`3[System.String,System.String,System.Func`1[Microsoft.UI.Xaml.FrameworkElement]]]" || text == "System.Collections.Generic.List`1[HeyClicky.App.Portraits.PortraitAppearance]" || text == "System.Collections.ObjectModel.ReadOnlyCollection`1[(System.String, System.String, System.Func)`3[System.String,System.String,System.Func`1[Microsoft.UI.Xaml.FrameworkElement]]]" || text == "System.Collections.ObjectModel.ReadOnlyCollection`1[HeyClicky.App.Dock.DockWindowKey]" || text == "System.Collections.Generic.List`1[HeyClicky.Agents.Jobs.ClickyJob]" || text == "System.Collections.Generic.List`1[HeyClicky.Core.Ink.ClickyInkDashboardBoard+Section]" || text == "System.Collections.ObjectModel.ReadOnlyCollection`1[HeyClicky.Core.Ink.ClickyInkStreakDay]" || text == "System.Collections.ObjectModel.ReadOnlyCollection`1[HeyClicky.Core.Ink.ClickyInkDashboardBoard+Section]" || text == "System.Collections.Generic.List`1[HeyClicky.Core.Proactive.AgentIntegrationInfo]" || text == "System.Collections.Generic.List`1[HeyClicky.Core.Ink.ClickyInkDocument+Block]" || text == "System.Collections.ObjectModel.ReadOnlyCollection`1[HeyClicky.App.Portraits.PortraitCharacter]" || text == "System.Collections.ObjectModel.ReadOnlyCollection`1[HeyClicky.Core.Voice.VoiceLanguage]" || text == "System.Collections.ObjectModel.ReadOnlyCollection`1[HeyClicky.Core.Notch.ChatHandOff]" || text == "System.Collections.Generic.List`1[HeyClicky.Core.Ink.ClickyInkStreakDay]" || text == "System.Collections.ObjectModel.ReadOnlyCollection`1[HeyClicky.App.Portraits.CloudFaceTraits+Blob]" || text == "System.Collections.ObjectModel.ReadOnlyCollection`1[HeyClicky.Agents.Runtime.CustomConnectorRegistration]" || text == "System.Collections.Generic.List`1[HeyClicky.App.Portraits.StarterTaskCard]" || text == "System.Collections.ObjectModel.ReadOnlyCollection`1[HeyClicky.Core.Proactive.ProactiveSuggestionCard]" || text == "System.Collections.Generic.List`1[HeyClicky.Agents.Routines.ClickyRoutine]" || text == "System.Collections.ObjectModel.ReadOnlyCollection`1[HeyClicky.App.Portraits.PortraitAppearance]" || text == "System.Collections.ObjectModel.ReadOnlyCollection`1[HeyClicky.AgentTaskSnapshot]" || text == "System.Collections.ObjectModel.ReadOnlyCollection`1[HeyClicky.App.Home.StoredLesson]" || text == "System.Collections.Generic.List`1[(System.String, System.String)`2[System.String,System.String]]" || text == "System.Collections.Generic.List`1[HeyClicky.App.Portraits.PortraitCharacter]" || text == "System.Collections.Generic.List`1[HeyClicky.Core.Proactive.QueuedProactiveTask]" || text == "System.Collections.ObjectModel.ReadOnlyCollection`1[HeyClicky.App.Portraits.RosterClicky]" || text == "System.Collections.ObjectModel.ReadOnlyCollection`1[(System.Collections.Generic.List, System.Boolean)`2[System.Collections.Generic.List`1[Windows.Foundation.Point],System.Boolean]]" || text == "System.Collections.ObjectModel.ReadOnlyCollection`1[HeyClicky.Agents.Routines.ClickyRoutine]" || text == "System.Collections.ObjectModel.ReadOnlyCollection`1[HeyClicky.Agents.Jobs.ClickyJob]" || text == "System.Collections.ObjectModel.ReadOnlyCollection`1[HeyClicky.App.Dock.ArcRect]" || text == "System.Collections.Generic.List`1[HeyClicky.App.Onboarding.TutorialWindow+Step]")
      {
        return "Windows.Foundation.Collections.IVectorView`1<Object>";
      }
      if (text == "System.Collections.ObjectModel.ReadOnlyCollection`1[Microsoft.UI.Xaml.FrameworkElement]" || text == "System.Collections.Generic.List`1[Microsoft.UI.Xaml.FrameworkElement]")
      {
        return "Windows.Foundation.Collections.IVector`1<Microsoft.UI.Xaml.FrameworkElement>";
      }
      if (text == "HeyClicky.Platform.Audio.WasapiAudioOutput" || text == "System.Threading.Tasks.Task`1[System.Threading.Tasks.Task]" || text == "System.Threading.Tasks.Task`1[System.Boolean]" || text == "HeyClicky.Platform.Input.ShortcutMonitor" || text == "HeyClicky.Platform.Permissions.PermissionService" || text == "HeyClicky.Platform.Capture.ScreenCaptureService" || text == "HeyClicky.Drawing.ScreenDrawingCanvas" || text == "System.IO.MemoryStream" || text == "HeyClicky.Core.Notch.NotchPresenter" || text == "System.Threading.Tasks.Task`1[System.Text.Json.Nodes.JsonObject]" || text == "System.Threading.Tasks.Task`1[HeyClicky.Core.Browser.UploadApprovalAnswer]")
      {
        return "Windows.Foundation.IClosable";
      }
      if (text == "System.Collections.Generic.Dictionary`2[System.String,System.Boolean]" || text == "System.Collections.ObjectModel.ReadOnlyDictionary`2[System.String,System.Boolean]")
      {
        return "Windows.Foundation.Collections.IMap`2<String, Boolean>";
      }
      if (text == "System.Text.Json.Nodes.JsonObject" || text == "System.Collections.Generic.Dictionary`2[System.String,HeyClicky.App.Dock.ArcCardView]" || text == "System.Collections.Generic.Dictionary`2+ValueCollection[System.String,HeyClicky.App.Portraits.PortraitAppearance]" || text == "ABI.System.Collections.Generic.ConstantSplittableMap`2[System.String,HeyClicky.App.Dock.ArcRect]" || text == "ABI.System.Collections.Generic.ConstantSplittableMap`2[System.String,System.Text.Json.Nodes.JsonNode]" || text == "System.Collections.ObjectModel.ReadOnlyDictionary`2[System.String,System.Text.Json.Nodes.JsonNode]" || text == "System.Collections.ObjectModel.ReadOnlyDictionary`2[System.String,HeyClicky.App.Dock.ArcCardView]" || text == "System.Collections.ObjectModel.ReadOnlyDictionary`2[System.String,HeyClicky.App.Dock.ArcRect]" || text == "System.Collections.Generic.Dictionary`2[System.String,System.Byte[]]" || text == "System.Collections.Generic.Dictionary`2[System.String,HeyClicky.App.Dock.ArcRect]" || text == "System.Collections.ObjectModel.ReadOnlyDictionary`2[System.String,System.Byte[]]" || text == "ABI.System.Collections.Generic.ConstantSplittableMap`2[System.String,HeyClicky.App.Dock.ArcCardView]" || text == "ABI.System.Collections.Generic.ConstantSplittableMap`2[System.String,System.Byte[]]")
      {
        return "Windows.Foundation.Collections.IIterable`1<Object>";
      }
      if (text == "ABI.System.Collections.Generic.ToAbiEnumeratorAdapter`1[System.Collections.Generic.KeyValuePair`2[System.String,System.Boolean]]")
      {
        return "Windows.Foundation.Collections.IIterator`1<Windows.Foundation.Collections.IKeyValuePair`2<String, Boolean>>";
      }
      if (text == "ABI.System.Collections.Generic.ToAbiEnumeratorAdapter`1[System.Collections.Generic.IEnumerable`1[System.Byte]]")
      {
        return "Windows.Foundation.Collections.IIterator`1<Windows.Foundation.Collections.IIterable`1<UInt8>>";
      }
      if (text == "System.Collections.Generic.HashSet`1[System.Guid]")
      {
        return "Windows.Foundation.Collections.IIterable`1<System.Guid>";
      }
      if (text == "System.Collections.Generic.List`1[System.Double]" || text == "System.Collections.ObjectModel.ReadOnlyCollection`1[System.Double]")
      {
        return "Windows.Foundation.Collections.IVector`1<Double>";
      }
      if (text == "HeyClicky.App.Portraits.SkinTone[]" || text == "System.String[]" || text == "HeyClicky.App.Portraits.Colorway[]" || text == "Microsoft.UI.Xaml.FrameworkElement[]" || text == "System.Byte[][]" || text == "System.Collections.ObjectModel.ReadOnlyCollection`1[HeyClicky.App.Notch.Peek.INotchDocumentRenderer]" || text == "System.Boolean[]" || text == "(System.String Event, System.String[] Keys)[]" || text == "HeyClicky.App.Portraits.PortraitCharacter[]" || text == "System.Collections.Generic.List`1[HeyClicky.App.Notch.Peek.INotchDocumentRenderer]" || text == "(System.Single v, System.Double s)[]" || text == "(System.Single, System.Double)[]" || text == "System.Byte[]" || text == "System.Collections.Generic.List`1[HeyClicky.App.Drive.IAgentDriveJob]" || text == "HeyClicky.App.Portraits.HairPiece[]" || text == "System.Collections.ObjectModel.ReadOnlyCollection`1[HeyClicky.App.Drive.IAgentDriveJob]")
      {
        return "Microsoft.UI.Xaml.Interop.IBindableVector";
      }
      if (text == "ABI.System.Collections.Generic.ToAbiEnumeratorAdapter`1[Microsoft.UI.Xaml.UIElement]")
      {
        return "Windows.Foundation.Collections.IIterator`1<Microsoft.UI.Xaml.UIElement>";
      }
      if (text == "System.Collections.ObjectModel.ReadOnlyDictionary`2[System.Int32,System.String]" || text == "System.Collections.Generic.Dictionary`2[System.Int32,System.String]")
      {
        return "Windows.Foundation.Collections.IMap`2<Int32, String>";
      }
      if (text == "ABI.System.Collections.Generic.ToAbiEnumeratorAdapter`1[System.Collections.Generic.KeyValuePair`2[System.String,System.Int32]]")
      {
        return "Windows.Foundation.Collections.IIterator`1<Windows.Foundation.Collections.IKeyValuePair`2<String, Int32>>";
      }
      if (text == "System.Collections.Generic.Dictionary`2[System.String,System.Int32]" || text == "System.Collections.ObjectModel.ReadOnlyDictionary`2[System.String,System.Int32]")
      {
        return "Windows.Foundation.Collections.IMap`2<String, Int32>";
      }
      if (text == "ABI.System.Collections.Generic.ToAbiEnumeratorAdapter`1[Microsoft.UI.Xaml.FrameworkElement]")
      {
        return "Windows.Foundation.Collections.IIterator`1<Microsoft.UI.Xaml.FrameworkElement>";
      }
      if (text == "System.Collections.ObjectModel.ReadOnlyDictionary`2[System.String,System.String]" || text == "System.Collections.Generic.Dictionary`2[System.String,System.String]")
      {
        return "Windows.Foundation.Collections.IMap`2<String, String>";
      }
      if (text == "ABI.System.Collections.Generic.ToAbiEnumeratorAdapter`1[System.Collections.IList]")
      {
        return "Windows.Foundation.Collections.IIterator`1<Microsoft.UI.Xaml.Interop.IBindableVector>";
      }
      if (text == "ABI.System.Collections.Generic.ToAbiEnumeratorAdapter`1[Microsoft.UI.Composition.IAnimationObject]")
      {
        return "Windows.Foundation.Collections.IIterator`1<Microsoft.UI.Composition.IAnimationObject>";
      }
      if (text == "System.Collections.ObjectModel.ReadOnlyCollection`1[System.String]" || text == "System.Collections.Generic.List`1[System.String]")
      {
        return "Windows.Foundation.Collections.IVector`1<String>";
      }
      if (text == "ABI.System.Collections.Generic.ToAbiEnumeratorAdapter`1[Microsoft.UI.Composition.IVisualElement]")
      {
        return "Windows.Foundation.Collections.IIterator`1<Microsoft.UI.Composition.IVisualElement>";
      }
      if (text == "ABI.System.Collections.Generic.ConstantSplittableMap`2[System.String,System.Int32]")
      {
        return "Windows.Foundation.Collections.IMapView`2<String, Int32>";
      }
      if (text == "System.Collections.Generic.KeyValuePair`2[System.Int32,System.String]")
      {
        return "Windows.Foundation.Collections.IKeyValuePair`2<Int32, String>";
      }
      if (text == "ABI.System.Collections.Generic.ToAbiEnumeratorAdapter`1[System.String]")
      {
        return "Windows.Foundation.Collections.IIterator`1<String>";
      }
      if (text == "ABI.System.Collections.Generic.ToAbiEnumeratorAdapter`1[System.Byte]")
      {
        return "Windows.Foundation.Collections.IIterator`1<UInt8>";
      }
      if (text == "ABI.System.Collections.Generic.ToAbiEnumeratorAdapter`1[System.DateTimeOffset]")
      {
        return "Windows.Foundation.Collections.IIterator`1<Windows.Foundation.DateTime>";
      }
      if (text == "System.Collections.Generic.Dictionary`2+KeyCollection[System.String,System.String]" || text == "System.Collections.Generic.HashSet`1[System.String]" || text == "System.Collections.Generic.Dictionary`2+KeyCollection[System.String,System.Char]")
      {
        return "Windows.Foundation.Collections.IIterable`1<String>";
      }
      if (text == "System.Collections.ObjectModel.ReadOnlyCollection`1[System.DateTimeOffset]" || text == "System.Collections.Generic.List`1[System.DateTimeOffset]")
      {
        return "Windows.Foundation.Collections.IVector`1<Windows.Foundation.DateTime>";
      }
      if (text == "System.Collections.ObjectModel.ReadOnlyCollection`1[Microsoft.UI.Xaml.UIElement]" || text == "System.Collections.Generic.List`1[Microsoft.UI.Xaml.UIElement]")
      {
        return "Windows.Foundation.Collections.IVector`1<Microsoft.UI.Xaml.UIElement>";
      }
      if (text == "System.Collections.Generic.KeyValuePair`2[System.String,System.Int32]")
      {
        return "Windows.Foundation.Collections.IKeyValuePair`2<String, Int32>";
      }
      if (text == "ABI.System.Collections.Generic.ToAbiEnumeratorAdapter`1[System.Guid]")
      {
        return "Windows.Foundation.Collections.IIterator`1<System.Guid>";
      }
      if (text == "ABI.System.Collections.Generic.ToAbiEnumeratorAdapter`1[System.Collections.Generic.IList`1[System.Byte]]")
      {
        return "Windows.Foundation.Collections.IIterator`1<Windows.Foundation.Collections.IVector`1<UInt8>>";
      }
      if (text == "ABI.System.Collections.Generic.ToAbiEnumeratorAdapter`1[System.Collections.Generic.KeyValuePair`2[System.Int32,System.String]]")
      {
        return "Windows.Foundation.Collections.IIterator`1<Windows.Foundation.Collections.IKeyValuePair`2<Int32, String>>";
      }
      if (text == "ABI.System.Collections.Generic.ToAbiEnumeratorAdapter`1[System.Collections.Generic.KeyValuePair`2[System.String,System.String]]")
      {
        return "Windows.Foundation.Collections.IIterator`1<Windows.Foundation.Collections.IKeyValuePair`2<String, String>>";
      }
      if (text == "ABI.System.Collections.Generic.ConstantSplittableMap`2[System.String,System.String]")
      {
        return "Windows.Foundation.Collections.IMapView`2<String, String>";
      }
      if (text == "ABI.System.Collections.Generic.ConstantSplittableMap`2[System.Int32,System.String]")
      {
        return "Windows.Foundation.Collections.IMapView`2<Int32, String>";
      }
      if (text == "ABI.System.Collections.Generic.ToAbiEnumeratorAdapter`1[System.Int32]")
      {
        return "Windows.Foundation.Collections.IIterator`1<Int32>";
      }
      if (text == "ABI.System.Collections.Generic.ToAbiEnumeratorAdapter`1[Microsoft.UI.Composition.IVisualElement2]")
      {
        return "Windows.Foundation.Collections.IIterator`1<Microsoft.UI.Composition.IVisualElement2>";
      }
      if (text == "ABI.System.Collections.Generic.ToAbiEnumeratorAdapter`1[Windows.Foundation.Point]")
      {
        return "Windows.Foundation.Collections.IIterator`1<Windows.Foundation.Point>";
      }
      if (text == "ABI.System.Collections.Generic.ToAbiEnumeratorAdapter`1[System.Collections.Generic.IReadOnlyList`1[System.Byte]]")
      {
        return "Windows.Foundation.Collections.IIterator`1<Windows.Foundation.Collections.IVectorView`1<UInt8>>";
      }
      if (text == "ABI.System.Collections.Generic.ToAbiEnumeratorAdapter`1[System.Collections.Generic.IEnumerable`1[System.Object]]")
      {
        return "Windows.Foundation.Collections.IIterator`1<Windows.Foundation.Collections.IIterable`1<Object>>";
      }
      if (text == "ABI.System.Collections.Generic.ToAbiEnumeratorAdapter`1[System.Object]")
      {
        return "Windows.Foundation.Collections.IIterator`1<Object>";
      }
      if (text == "System.Collections.Generic.List`1[System.Text.Json.Nodes.JsonObject]" || text == "System.Collections.ObjectModel.ReadOnlyCollection`1[System.Text.Json.Nodes.JsonObject]")
      {
        return "Windows.Foundation.Collections.IVectorView`1<Windows.Foundation.Collections.IIterable`1<Object>>";
      }
      if (text == "ABI.System.Collections.Generic.ToAbiEnumeratorAdapter`1[System.Collections.Generic.IEnumerable`1[System.Char]]")
      {
        return "Windows.Foundation.Collections.IIterator`1<Windows.Foundation.Collections.IIterable`1<Char>>";
      }
      if (text == "System.Collections.Generic.HashSet`1[System.Int32]")
      {
        return "Windows.Foundation.Collections.IIterable`1<Int32>";
      }
      if (text == "ABI.System.Collections.Generic.ConstantSplittableMap`2[System.String,System.Boolean]")
      {
        return "Windows.Foundation.Collections.IMapView`2<String, Boolean>";
      }
      if (text == "ABI.System.Collections.Generic.ToAbiEnumeratorAdapter`1[System.Double]")
      {
        return "Windows.Foundation.Collections.IIterator`1<Double>";
      }
      if (text == "ABI.System.Collections.Generic.ToAbiEnumeratorAdapter`1[System.Collections.Generic.IReadOnlyList`1[System.Object]]")
      {
        return "Windows.Foundation.Collections.IIterator`1<Windows.Foundation.Collections.IVectorView`1<Object>>";
      }
      if (text == "System.Collections.Generic.KeyValuePair`2[System.String,System.Boolean]")
      {
        return "Windows.Foundation.Collections.IKeyValuePair`2<String, Boolean>";
      }
      if (text == "ABI.System.Collections.Generic.ToAbiEnumeratorAdapter`1[System.Boolean]")
      {
        return "Windows.Foundation.Collections.IIterator`1<Boolean>";
      }
      if (text == "ABI.System.Collections.Generic.ToAbiEnumeratorAdapter`1[Microsoft.UI.Xaml.DependencyObject]")
      {
        return "Windows.Foundation.Collections.IIterator`1<Microsoft.UI.Xaml.DependencyObject>";
      }
      if (text == "ABI.System.Collections.Generic.ToAbiEnumeratorAdapter`1[System.Collections.IEnumerable]")
      {
        return "Windows.Foundation.Collections.IIterator`1<Microsoft.UI.Xaml.Interop.IBindableIterable>";
      }
      if (text == "System.Collections.Generic.KeyValuePair`2[System.String,System.String]")
      {
        return "Windows.Foundation.Collections.IKeyValuePair`2<String, String>";
      }
      return null;
    }
  }
}
