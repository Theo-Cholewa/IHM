using System.Collections; using UnityEngine;
public class GameManager : MonoBehaviour
{
    public PlayerController playerControls;
    public AIControls[] aiControls;
    public LapManager lapTracker;
    public TricolorLights tricolorLights;

    public AudioClip lowBeep;
    public AudioClip highBeep;
    public AudioSource audioSource;
    public Animator cameraIntroAnimator;
    public FollowCar followPlayerCamera;
    void Awake()
    {
        StartGame();
    }
    
    public void StartIntro()
    {
        followPlayerCamera.enabled = false;
        cameraIntroAnimator.enabled = true;
        FreezePlayers(true);
    }
    public void StartGame()
    {
        FreezePlayers(true);
        StartCoroutine(nameof(Countdown));
    }
    IEnumerator Countdown()
    {
        yield return new WaitForSeconds(1);
        audioSource.PlayOneShot(lowBeep);
        Debug.Log("3");
        tricolorLights.SetProgress(1);
        yield return new WaitForSeconds(1);
        audioSource.PlayOneShot(lowBeep);
        Debug.Log("2");
        tricolorLights.SetProgress(2);
        yield return new WaitForSeconds(1);
        audioSource.PlayOneShot(lowBeep);
        Debug.Log("1");
        tricolorLights.SetProgress(3);
        yield return new WaitForSeconds(1);
        audioSource.PlayOneShot(highBeep);
        Debug.Log("GO");
        tricolorLights.SetProgress(4);
        cameraIntroAnimator.enabled = false;
        StartRacing();
        yield return new WaitForSeconds(2f);
        tricolorLights.SetAllLightsOff();
        followPlayerCamera.enabled = true;
    }
    public void StartRacing()
    {
        FreezePlayers(false);
    }
    void FreezePlayers(bool freeze)
    {
        playerControls.enabled = !freeze;
        foreach (var iaControl in aiControls)
        {
            iaControl.enabled = !freeze;
        }
    }
}