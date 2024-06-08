using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class Boss : MonoBehaviour
{
    [SerializeField] bool shoot;
    [SerializeField] private GameObject bulletObject;
    [SerializeField] private AudioClip[] gunshotAudioClip;
    [Range(0,1)] [SerializeField] private float volume;

    public int bossHealth = 4;
    private Animator bossAnimator;
    private GameObject bulletObjectClone;
    private Bullets bulletObjectCloneScript;

    private const string EMPTY_STANCE_TRIGGER = "Empty Stance";
    private const string USE_PISTOL_TRIGGER = "Use Pistol";
    void Awake()
    {
        bossAnimator = GetComponent<Animator>();
        bossAnimator.SetTrigger(EMPTY_STANCE_TRIGGER);  
    }

    public  async void ShootNormalBullets()
    {
        //animation
        bossAnimator.SetTrigger(USE_PISTOL_TRIGGER);
        bossAnimator.ResetTrigger(EMPTY_STANCE_TRIGGER);

        // gunshot sound
        SoundFxManager.Instance.PlayRandomSoundFxClip(gunshotAudioClip, transform, volume);

        // shoot
        bulletObjectClone = Instantiate(bulletObject);
        bulletObjectCloneScript = bulletObjectClone.GetComponent<Bullets>();
        bulletObjectCloneScript.MoveBullet(gameObject.transform.position);

        

        await Task.Delay(1000);

        //reset animation
        if (bossAnimator != null)
        {
            bossAnimator.SetTrigger(EMPTY_STANCE_TRIGGER);
            bossAnimator.ResetTrigger(USE_PISTOL_TRIGGER);
        }
        
        
    }
}
