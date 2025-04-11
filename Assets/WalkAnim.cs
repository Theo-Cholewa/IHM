using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkAnim : MonoBehaviour
{
    private Animator mAnimator;
    // Start is called before the first frame update
    void Start()
    {
        mAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (mAnimator != null)
        {
            if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.Z) || Input.GetKeyDown(KeyCode.Q) || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.D))
            {
                mAnimator.SetTrigger("TrWalk");
            }
            /*
            if (...)
            {
                mAnimator.SetTrigger("TrPickUp");
            }
            if (...)
            {
                mAnimator.SetTrigger("TrWave");
            }
            if (...)
            {
                mAnimator.SetTrigger("TrWin");
            }
            if (...)
            {
                mAnimator.SetTrigger("TrLose");
            }*/
        }
    }
}
