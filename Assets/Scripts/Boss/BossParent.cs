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
            transform.position = new Vector3(19.95f, 1, 0);
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
            Destroy(gameObject);
        }
    }
}
