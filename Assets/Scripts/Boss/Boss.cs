using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class Boss : MonoBehaviour
{
    [SerializeField] bool shoot;
    [SerializeField] private GameObject bulletObject;
    [SerializeField] private AudioClip[] gunshotAudioClip;
    [SerializeField] private AudioClip telekinesisAudioClip;
    [Range(0,1)] [SerializeField] private float volumeGun, volumeTelekinesis;

    
    private Animator bossAnimator;
    private GameObject bulletObjectClone;
    private Bullets bulletObjectCloneScript;
    public bool hasntTelekinesis;

    private const string EMPTY_STANCE_TRIGGER = "Empty Stance";
    private const string USE_PISTOL_TRIGGER = "Use Pistol";
    private const string TELEKINESIS_TRIGGER = "Telekinesis";
    void Awake()
    {
        hasntTelekinesis = true;
        bossAnimator = GetComponent<Animator>();
        bossAnimator.SetTrigger(EMPTY_STANCE_TRIGGER);  
    }

    public async Task ShootNormalBullets()
    {
        //animation
        try
        {
            bossAnimator.SetTrigger(USE_PISTOL_TRIGGER);
            bossAnimator.ResetTrigger(EMPTY_STANCE_TRIGGER);
            // gunshot sound
            SoundFxManager.Instance.PlayRandomSoundFxClip(gunshotAudioClip, transform, volumeGun);

            // shoot
            bulletObjectClone = Instantiate(bulletObject);
            bulletObjectCloneScript = bulletObjectClone.GetComponent<Bullets>();
            bulletObjectCloneScript.MoveBullet(gameObject.transform.position);
            
            // wait after shooting
            await Task.Delay(500);
            print("Boss. reset animation?\ncheck hasn't telekinesis: " + hasntTelekinesis);
            //reset animation
            if (bossAnimator != null && hasntTelekinesis)
            {
                bossAnimator.SetTrigger(EMPTY_STANCE_TRIGGER);
                bossAnimator.ResetTrigger(USE_PISTOL_TRIGGER);
                hasntTelekinesis = true;
            }
        }
        catch (System.Exception)
        {
            return;
        }
    }

    public void TelekinesisOnPlayer()
    {
        try
        {
            hasntTelekinesis = false;
            print("TelekinesisOnPlayer called:\nhasn't telekinesis, " + hasntTelekinesis);
            bossAnimator.SetTrigger(TELEKINESIS_TRIGGER);
            bossAnimator.ResetTrigger(EMPTY_STANCE_TRIGGER);
            bossAnimator.ResetTrigger(USE_PISTOL_TRIGGER);

            SoundFxManager.Instance.PlaySoundFxClip(telekinesisAudioClip, transform, volumeTelekinesis);
        }
        catch (System.Exception)
        {
            return;
        }
    }
}
