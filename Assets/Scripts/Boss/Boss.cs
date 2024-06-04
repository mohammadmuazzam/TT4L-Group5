using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    private Animator bossAnimator;

    private const string EMPTY_STANCE_TRIGGER = "Empty Stance";
    private const string USE_PISTOL_TRIGGER = "Use Pistol";
    void Awake()
    {
        bossAnimator = GetComponent<Animator>();
        bossAnimator.SetTrigger(EMPTY_STANCE_TRIGGER);
        bossAnimator.ResetTrigger(USE_PISTOL_TRIGGER);
    }

    // Update is called once per frame
    void Update()
    {
        ShootNormalBullets();
    }

    private void ShootNormalBullets()
    {
        bossAnimator.SetTrigger(USE_PISTOL_TRIGGER);
    }
}
