using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

namespace Hook.NTC
{
    public class VideoContentController : MonoBehaviour
    {
        #region Properties

        [SerializeField] private RawImage VideoImage;
        [SerializeField] private VideoClip ClipToPlay;
        [SerializeField] private TextMeshProUGUI PlaybackTime;

        private VideoPlayer _videoPlayer;
        private AudioSource _audioSource;
        
        #endregion
        
        #region MonoBehaviour

        private void Start()
        {
            PlaybackTime.text = TimeSpan.FromSeconds(0).ToString(@"mm\:ss");
            
            // setting aspect ratio for video to be played
            var clipAspectRatio = (float)ClipToPlay.width / ClipToPlay.height;
            var currentBounds = transform.GetComponent<RectTransform>().rect.size;
            var videoTransform = VideoImage.GetComponent<RectTransform>();
            var newSize = videoTransform.rect;
            newSize.x = currentBounds.x;
            newSize.y = currentBounds.x * (1 / clipAspectRatio);
            videoTransform.sizeDelta = new Vector2(newSize.x, newSize.y);
        }
        
        #endregion
        
        #region Class Methods

        public void Play()
        {
            StartCoroutine(PlayVideo());
        }

        public void Play(VideoClip clip)
        {
            ClipToPlay = clip;
            
            Play();
        }
        
        public void Stop()
        {
            if (_videoPlayer != null && _videoPlayer.isPlaying)
            {
                // stop video playback
                _videoPlayer.Stop();
                
                // stop Play coroutine
                StopCoroutine(PlayVideo());
            }
        }
        
        private IEnumerator PlayVideo()
        {
            // add VideoPlayerComponent
            if (GetComponent<VideoPlayer>() == null)
            {
                _videoPlayer = gameObject.AddComponent<VideoPlayer>();
            }
            
            // add AudioSource
            if (GetComponent<AudioSource>() == null)
            {
                _audioSource = gameObject.AddComponent<AudioSource>();
            }
            
            // disable play on awake for video and audio
            _videoPlayer.playOnAwake = false;
            _audioSource.playOnAwake = false;
            
            // setting video clip
            _videoPlayer.source = VideoSource.VideoClip;
            _videoPlayer.isLooping = true;
            
            // set audio output to audio source
            _videoPlayer.audioOutputMode = VideoAudioOutputMode.AudioSource;
            
            // assign audio from video to audiosource
            _videoPlayer.EnableAudioTrack(0, true);
            _videoPlayer.SetTargetAudioSource(0, _audioSource);
            
            // set video to play
            _videoPlayer.clip = ClipToPlay;
            
            // prepare audio to prevent buffering
            _videoPlayer.Prepare();
            
            // wait until video is prepared
            while (!_videoPlayer.isPrepared)
            {
                yield return null;
            }
            
            // assign texture to RawImage
            VideoImage.texture = _videoPlayer.texture;
            
            // play video
            _videoPlayer.Play();
            
            // play sound
            _audioSource.Play();
            
            while (_videoPlayer.isPlaying)
            {
                PlaybackTime.text = TimeSpan.FromSeconds(_videoPlayer.time).ToString(@"mm\:ss");
                yield return null;
            }
        }
        
        #endregion
    }
}
