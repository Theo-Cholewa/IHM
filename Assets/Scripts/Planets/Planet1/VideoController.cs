using UnityEngine;
using UnityEngine.Video;

namespace DefaultNamespace
{


    public class VideoController : MonoBehaviour
    {
        private VideoPlayer videoPlayer;

        void Awake()
        {
            videoPlayer = GetComponent<VideoPlayer>();

            if (videoPlayer == null)
            {
                Debug.LogError("Aucun VideoPlayer trouvé !");
            }
            
            videoPlayer.loopPointReached += ResetVideoController;
        }

        public void PlayVideo(string videoName)
        {
            videoPlayer.enabled = true;
            string path = "Planet1/" + videoName; 
            videoPlayer.source = VideoSource.VideoClip;
            videoPlayer.clip = Resources.Load<VideoClip>(path);

            if (videoPlayer.clip == null)
            {
                Debug.LogError("Vidéo non trouvée : " + path);
                return;
            }

            videoPlayer.Prepare();
            videoPlayer.Play();
        }

        public void StopVideo()
        {
            videoPlayer.Stop();
        }
        
        private void ResetVideoController(VideoPlayer vp)
        {
            vp.Stop();
            vp.enabled = false;
            vp.clip = null;  
            Debug.Log("VidéoController réinitialisé.");
        }
    }

}