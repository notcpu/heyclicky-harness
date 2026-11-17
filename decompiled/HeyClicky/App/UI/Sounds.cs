using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using Windows.Media.Core;
using Windows.Media.Playback;

namespace HeyClicky.App.UI
{
  // Token: 0x020000B9 RID: 185
  [NullableContext(1)]
  [Nullable(0)]
  public static class Sounds
  {
    // Token: 0x0600077E RID: 1918 RVA: 0x00538DFC File Offset: 0x00538DFC
    public static void Play(string name, double volume)
    {
      if (Sounds.Muted)
      {
        return;
      }
      try
      {
        MediaPlayer mediaPlayer;
        if (!Sounds.players.TryGetValue(name, out mediaPlayer))
        {
          string text = Path.Combine(AppContext.BaseDirectory, "Assets", "Earcons", name + ".wav");
          if (!File.Exists(text))
          {
            return;
          }
          mediaPlayer = new MediaPlayer
          {
            Source = MediaSource.CreateFromUri(new Uri(text)),
            AudioCategory = MediaPlayerAudioCategory.SoundEffects
          };
          Sounds.players[name] = mediaPlayer;
        }
        mediaPlayer.Volume = volume;
        mediaPlayer.PlaybackSession.Position = TimeSpan.Zero;
        mediaPlayer.Play();
      }
      catch (Exception ex)
      {
        Log.Warn("sounds", name + ": " + ex.Message);
      }
    }

    // Token: 0x0600077F RID: 1919 RVA: 0x00538EC4 File Offset: 0x00538EC4
    public static void Stop(string name)
    {
      MediaPlayer mediaPlayer;
      if (Sounds.players.TryGetValue(name, out mediaPlayer))
      {
        mediaPlayer.Pause();
      }
    }

    // Token: 0x04000245 RID: 581
    private static readonly Dictionary<string, MediaPlayer> players = new Dictionary<string, MediaPlayer>();

    // Token: 0x04000246 RID: 582
    private static readonly bool Muted = Environment.GetEnvironmentVariable("HEYCLICKY_MUTE_AUDIO") == "1";
  }
}
