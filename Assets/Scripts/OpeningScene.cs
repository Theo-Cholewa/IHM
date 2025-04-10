using System.Collections; using UnityEngine;
public class OpeningScene : MonoBehaviour
{
    public princessMovement playerControls;

  
    public Animator cameraIntroAnimator;
    public FollowPlayer followPlayerCamera;
    void Awake()
    {
        StartGame();
    }
    
    public void StartGame()
    {
        playerControls.enabled = false;
        followPlayerCamera.enabled = false;
        cameraIntroAnimator.enabled = true; 
        StartCoroutine(nameof(Countdown));
    }
    IEnumerator Countdown()
    {
        yield return new WaitForSeconds(4);


        followPlayerCamera.enabled = true;
        playerControls.enabled = true;
        cameraIntroAnimator.enabled = false;

    }
}