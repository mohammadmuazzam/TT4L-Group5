using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class Boss : MonoBehaviour
{
    [SerializeField] bool shoot;
    private Animator bossAnimator;
    [SerializeField] private GameObject bulletObject;
    private GameObject bulletObjectClone;
    private Bullets bulletObjectCloneScript;

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
        if (shoot)
        {
            ShootNormalBullets();
            shoot = false;
        }
            
        else 
        {
            bossAnimator.SetTrigger(EMPTY_STANCE_TRIGGER);
            bossAnimator.ResetTrigger(USE_PISTOL_TRIGGER);
        }
    }

    private void ShootNormalBullets()
    {
        //animation
        bossAnimator.SetTrigger(USE_PISTOL_TRIGGER);
        bossAnimator.ResetTrigger(EMPTY_STANCE_TRIGGER);

        // shoot
        bulletObjectClone = Instantiate(bulletObject);
        bulletObjectCloneScript = bulletObjectClone.GetComponent<Bullets>();
        bulletObjectCloneScript.MoveBullet(gameObject.transform.position);
        
    }
}
