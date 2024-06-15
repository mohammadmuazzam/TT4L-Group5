using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

public class Boss : MonoBehaviour
{
    [SerializeField] bool shoot;
    [SerializeField] private GameObject bulletObject;
    [SerializeField] private AudioClip[] gunshotAudioClip;
    [SerializeField] private AudioClip[] rLGLAudioClip;
    [SerializeField] private AudioClip telekinesisAudioClip;
    [Range(0,1)] [SerializeField] private float volumeGun, volumeTelekinesis, volumeRLGL;

    private Laser laserScript;
    private Animator bossAnimator;
    private GameObject bulletObjectClone;
    private Bullets bulletObjectCloneScript;
    private GameObject BossDamageBox;
    public bool bossShootControl, bossShootLaserControl, bossRLGLActiveCheck, bossRLGLControl;

    private const string EMPTY_STANCE_TRIGGER = "Empty Stance";
    private const string USE_PISTOL_TRIGGER = "Use Pistol";
    private const string TELEKINESIS_TRIGGER = "Telekinesis";
    private const string SHOOT_LASER_TRIGGER = "Shoot Laser";
    void Awake()
    {
        bossShootControl = true;
        bossShootLaserControl = false;
        bossRLGLActiveCheck = false;
        bossAnimator = GetComponent<Animator>();
        BossDamageBox = GameObject.Find("Death box");
        bossAnimator.SetTrigger(EMPTY_STANCE_TRIGGER);  

        laserScript = GameObject.Find("Laser Mask").GetComponent<Laser>();
    }

    public async Task ShootNormalBullets()
    {
        //animation
        try
        {
            bossAnimator.SetTrigger(USE_PISTOL_TRIGGER);
            bossAnimator.ResetTrigger(EMPTY_STANCE_TRIGGER);
            //* gunshot sound
            SoundFxManager.Instance.PlayRandomSoundFxClip(gunshotAudioClip, transform, volumeGun);

            //* shoot
            bulletObjectClone = Instantiate(bulletObject);
            bulletObjectCloneScript = bulletObjectClone.GetComponent<Bullets>();
            bulletObjectCloneScript.MoveBullet(gameObject.transform.position);
            
            //* wait after shooting
            await Task.Delay(500);
            //print("Boss. reset animation?\ncheck hasn't telekinesis: " + bossShootControl);
            //* reset animation
            if (bossAnimator != null && bossShootControl)
            {
                bossAnimator.SetTrigger(EMPTY_STANCE_TRIGGER);
                bossAnimator.ResetTrigger(USE_PISTOL_TRIGGER);
                bossShootControl = true;
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
            bossShootControl = false;
            print("TelekinesisOnPlayer called:\nhasn't telekinesis, " + bossShootControl);
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

    public void BossDefaultAnimation()
    {
        bossAnimator.SetTrigger(EMPTY_STANCE_TRIGGER);
    }

    public async Task BossShootLaser(CancellationTokenSource cancellationTokenSource)
    {
        try
        {
            if (bossShootLaserControl)
            {
                bossAnimator.SetTrigger(SHOOT_LASER_TRIGGER);
                
                await laserScript.ShootLaser(cancellationTokenSource);

                bossAnimator.SetTrigger(EMPTY_STANCE_TRIGGER);
            }
            
        }
        catch (System.Exception)
        {
            return;
        }
        
    }

    public async Task BossRedLightGreenLight(CancellationTokenSource cancellationTokenSource)
    {
        try
        {
            //* start count down to watch player
            bossRLGLActiveCheck = false;
            transform.Rotate(0, 180, 0);
            BossDamageBox.transform.Rotate(0, 180, 0);
            print("starting sound");
            await SoundFxManager.Instance.PlayRandomSoundFxClipAsync(rLGLAudioClip, transform, volumeRLGL, cancellationTokenSource);
            
            print("done counting, watching");

            //* watch player
            transform.Rotate(0, 180 , 0);
            BossDamageBox.transform.Rotate(0, 180, 0);
            bossRLGLActiveCheck = true;
        }
        catch (System.Exception)
        {

        }
    }
}
