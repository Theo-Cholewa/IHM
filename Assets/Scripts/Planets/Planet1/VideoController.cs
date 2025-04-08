using UnityEngine;
using UnityEngine.Video;

public class VideoController : MonoBehaviour
{
    private VideoPlayer videoPlayer;
    private GameObject player;
    private Action action;
    private ActionManager actionManager;

    void Awake()
    {
        videoPlayer = GetComponent<VideoPlayer>();

        if (videoPlayer == null)
        {
            Debug.LogError("Aucun VideoPlayer trouvé !");
        }
        
        actionManager = FindObjectOfType<ActionManager>();
        videoPlayer.loopPointReached += ResetVideoController;
    }

    public void PlayVideo(string videoName, GameObject player, Action action)
    {
        this.player = player;
        this.action = action;
        
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
        if (player != null)
        {
            player.SendMessage("SetCanWalk", true);
        }
        if (actionManager != null)
        {
            StartCoroutine(actionManager.ResetInterractionAfterDelay(5f));
        }

        vp.Stop();
        vp.enabled = false;
        vp.clip = null;
        action = null;
        player = null;
        Debug.Log("VidéoController réinitialisé.");
    }
}