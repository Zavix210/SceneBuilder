using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Media.Core;
using Windows.Media.MediaProperties;
using Windows.Media.Playback;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace App1
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
            mediaPlayer = new MediaPlayer();
        }

        private MediaPlayer mediaPlayer;

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {

            bool result = false;
            Uri myUri;
            string myPath = "ms-appx:///Assets/stationary3.mp4";
            result = Uri.TryCreate(myPath, UriKind.Absolute, out myUri);


            mediaPlayer.MediaOpened += _mediaPlayer_MediaOpened;
            mediaPlayer.Source = MediaSource.CreateFromUri(myUri);
            mediaPlayer.AutoPlay = true;
            _mediaPlayerElement.SetMediaPlayer(mediaPlayer);
            _mediaPlayerElement.AutoPlay = true;


        }


        private void _mediaPlayer_MediaOpened(MediaPlayer sender, object args)
        {
            if (sender.PlaybackSession.SphericalVideoProjection.FrameFormat == SphericalVideoFrameFormat.Equirectangular)
            {
                sender.PlaybackSession.SphericalVideoProjection.IsEnabled = true;

                sender.PlaybackSession.SphericalVideoProjection.HorizontalFieldOfViewInDegrees = 150;

            }
            else if (sender.PlaybackSession.SphericalVideoProjection.FrameFormat == SphericalVideoFrameFormat.Unsupported)
            {

                // If the spherical format is unsupported, you can use frame server mode to implement a custom projection
            }
        }

        private void _mediaPlayerElement_KeyDown(object sender, KeyRoutedEventArgs e)
        {
            if (mediaPlayer.PlaybackSession.SphericalVideoProjection.FrameFormat != SphericalVideoFrameFormat.Equirectangular)
            {
                return;
            }

            switch (e.Key)
            {
                case Windows.System.VirtualKey.Right:
                    mediaPlayer.PlaybackSession.SphericalVideoProjection.ViewOrientation *= Quaternion.CreateFromYawPitchRoll(10f, 0, 0);
                    break;
                case Windows.System.VirtualKey.Left:
                    mediaPlayer.PlaybackSession.SphericalVideoProjection.ViewOrientation *= Quaternion.CreateFromYawPitchRoll(-10f, 0, 0);
                    break;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            mediaPlayer.PlaybackSession.SphericalVideoProjection.ViewOrientation *= Quaternion.CreateFromYawPitchRoll(10f, 0, 0);
        }
    }
}
