using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class Boss : MonoBehaviour
{
    [SerializeField] bool shoot;
    [SerializeField] private GameObject bulletObject;
    [SerializeField] private AudioClip[] gunshotAudioClip;

    private AudioSource audioSource;
    private Animator bossAnimator;
    private GameObject bulletObjectClone;
    private Bullets bulletObjectCloneScript;

    private const string EMPTY_STANCE_TRIGGER = "Empty Stance";
    private const string USE_PISTOL_TRIGGER = "Use Pistol";
    void Awake()
    {
        bossAnimator = GetComponent<Animator>();
        bossAnimator.SetTrigger(EMPTY_STANCE_TRIGGER);

        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    public  async void ShootNormalBullets()
    {
        //animation
        bossAnimator.SetTrigger(USE_PISTOL_TRIGGER);
        bossAnimator.ResetTrigger(EMPTY_STANCE_TRIGGER);

        // gunshot sound
        AudioClip clip = gunshotAudioClip[Random.Range(0, gunshotAudioClip.Length)];
        audioSource.clip = clip;
        audioSource.Play();

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
