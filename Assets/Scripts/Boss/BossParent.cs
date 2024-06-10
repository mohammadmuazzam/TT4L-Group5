using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossParent : MonoBehaviour
{
    // Start is called before the first frame update
    private static BossParent instance;
    public static int bossHealth;
    public static bool[] hasDamagedBoss;
    void Awake()
    {
        if (instance == null)
        {
            bossHealth = 4;
            instance = this;
            DontDestroyOnLoad(gameObject);
            
            //* starts game with no damage to boss
            hasDamagedBoss = new bool[4];
            for (int i = 0; i < 4; i++)
            {
                hasDamagedBoss[i] = false;
            } 
        }
        else
        {
            print("destroying this bossparent, an instance exists");
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
