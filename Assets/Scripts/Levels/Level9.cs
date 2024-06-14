using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

public class Level9 : MonoBehaviour
{
    [SerializeField] private VanishingPlatform vanishingPlatformScript;
    [SerializeField] private Trap[] trapScripts;
    [SerializeField] private GameObject[] trapTriggers;
    [SerializeField] private GameObject mainCamera, playerObject, movingSpike;
    [SerializeField] private bool[] hasTriggered;
    [SerializeField] private float speed;

    private Laser laserScript;
    private GameObject bossCatObject;
    private GameObject catOnlyObject;
    private Boss catBossScript;
    private Player playerScript;
    private Renderer playerRenderer;
    private CameraFollow cameraScript;
    private bool fallingPlatformActivated;
    private CancellationTokenSource cancellationTokenSource;
    private Vector3 defaultCatScale;

    void Awake()
    {
        print("LEVEL9 AWAKE CALLED");
        //* initialization
        bossCatObject = GameObject.Find("Boss");
        catOnlyObject = GameObject.Find("Cat");
        catBossScript = catOnlyObject.GetComponent<Boss>();
        defaultCatScale = catOnlyObject.transform.localScale;

        cameraScript = mainCamera.GetComponent<CameraFollow>();
        cameraScript.playerDependant = true;

        playerScript = playerObject.GetComponent<Player>();
        playerRenderer = playerScript.GetComponent<Renderer>();

        laserScript = GameObject.Find("Laser Mask").GetComponent<Laser>();

        cancellationTokenSource = new CancellationTokenSource();

        fallingPlatformActivated = false;
        //bossShootLevel9Control = false;
        catBossScript.bossShootLaserControl = false;

        //* reset all trap trigger to "hasn't triggered"
        for (int i = 0; i < hasTriggered.Length; i++)
        {
            hasTriggered[i] = false;
        }
        
        //* boss at 4 health
        if (BossParent.bossHealth == 4)
        {
            //* player position
            playerObject.transform.position = new Vector3(-7.31f, 0, 0);
            playerScript.minX = -8.66f;
            playerScript.maxX = 14.71f;

            //* camera
            cameraScript.minX = 0f;
            cameraScript.maxX = 6.77f;            

            //* bossCat position
            bossCatObject.transform.position = new Vector3(19.95f, 1, 0);
        }
        //? boss at 3 health, shoot bullet
        else if (BossParent.bossHealth == 3)
        {
            //* player position
            playerObject.transform.position = new Vector3(12f, 0, 0);
            playerScript.minX = 10.2f;
            playerScript.maxX = 57.91f;

            //* boss cat position
            catOnlyObject.transform.localScale = new Vector3 (-0.14f, 0.14f, 0.14f);
            bossCatObject.transform.position = new Vector3 (70.2f, 3, 0);
            catBossScript.BossDefaultAnimation();

            //* boss shooting bullets
            print("Level9, Awake, calling BossShootAtRandomTime:\ncatBossScript.bossShootControl: " + catBossScript.bossShootControl);
            catBossScript.bossShootControl = true; 
            BossShootAtRandomTime();
        
            
            //* camera
            cameraScript.minX = 18.34f;
            cameraScript.maxX = 47.9f;
            mainCamera.transform.position = new Vector3(cameraScript.minX, mainCamera.transform.position.y, mainCamera.transform.position.z);
                      
        }

        //* health 2, laser cat
        else if (BossParent.bossHealth == 2)
        {
            //* player position
            playerObject.transform.position = new Vector3(53f, 0, 0);
            playerScript.minX = 51.7f;
            playerScript.maxX = 124f;

            //* boss cat position
            catOnlyObject.transform.localScale = new Vector3 (-0.14f, 0.14f, 0.14f);
            bossCatObject.transform.position = new Vector3 (135f, 3, 0);
            laserScript.laserCollider.enabled = false;

            //* camera
            cameraScript.minX = 59.82f;
            cameraScript.maxX = 115.6f;
            mainCamera.transform.position = new Vector3(cameraScript.minX, mainCamera.transform.position.y, mainCamera.transform.position.z);

            //* destroy moving spike
            if (movingSpike != null)
                Destroy(movingSpike);
        }
        //* squid cat
        else if (BossParent.bossHealth == 1)
        {
            //* player position
            playerObject.transform.position = new Vector3(53f, 0, 0);
            playerScript.minX = 124f;
            playerScript.maxX = 162.23f;

            //* boss cat position
            catOnlyObject.transform.localScale = new Vector3 (-0.14f, 0.14f, 0.14f);
            bossCatObject.transform.position = new Vector3(175, 2, 0);
            laserScript.laserCollider.enabled = false;

            //* camera
            cameraScript.minX = 115.6f;
            cameraScript.maxX = 154f;
            mainCamera.transform.position = new Vector3(cameraScript.minX, mainCamera.transform.position.y, mainCamera.transform.position.z);
            //* destroy moving spike
            if (movingSpike != null)
                Destroy(movingSpike);
        }
    }

    void LateUpdate()
    {
        CheckForTrapTrigger();

        CheckAttemptAndBossHealth();
    }

    async void CheckForTrapTrigger()
    {
        // check if any trap trigger has been triggered
        try
        {
            foreach (GameObject triggerGameObject in trapTriggers)
            {
                TrapTrigger trapTriggerScript = triggerGameObject.GetComponent<TrapTrigger>();

                if (trapTriggerScript != null && trapTriggerScript.playerIsInTrigger)
                {   
                    // trigger traps according to trapTrigger
                    switch (trapTriggerScript.name)
                    {
                        case "Cat Trigger 1":
                        if (!hasTriggered[0])
                        {
                            _ = catBossScript.ShootNormalBullets();
                            hasTriggered[0] = true;
                        }
                        break;

                        case "Floor Fall Trigger":
                        if (!fallingPlatformActivated)
                        {
                            await Task.Delay(500);
                            _ = trapScripts[0].PermanentMoveTrap();
                            fallingPlatformActivated = true;
                        }
                        break;

                        case "Spike Up Trigger":
                        if (!hasTriggered[1])
                        {
                            _ = trapScripts[1].PermanentMoveTrap();
                            hasTriggered[1] = true;
                        }
                        break;

                        case "Spike Right Trigger":
                        if (!hasTriggered[2])
                        {
                            _ = trapScripts[2].PermanentMoveTrap();
                            hasTriggered[2] = true;
                        }
                        break;
                        
                        case "Vanishing Platform Trigger":
                        if (!hasTriggered[3])
                        {
                            vanishingPlatformScript.RemovePlatform();
                            hasTriggered[3] = true;
                        }
                        break;

                        case "Telekinesis Trigger 1":
                        if (!hasTriggered[4])
                        {
                            hasTriggered[4] = true;
                        }
                        break;

                        case "Telekinesis Trigger 2":
                        if (!hasTriggered[5] && hasTriggered[4])
                        {
                            hasTriggered[5] = true;
                            TelekinesisPlayer();
                        }
                        break;

                        case "Destroy Moving Spike Trigger":
                        if (!hasTriggered[9] && movingSpike != null)
                        {
                            Destroy(movingSpike);
                            hasTriggered[9] = true;
                        }
                        break;

                        case "Metal Down Trigger":
                        if (!hasTriggered[11])
                        {
                            _ = trapScripts[13].PermanentMoveTrap();
                            print("moving down metal");
                            hasTriggered[11] = true;
                        }
                        break;

                        case "Wall Trigger":
                        if (!hasTriggered[6])
                        {
                            hasTriggered[6] = true;
                            await trapScripts[3].PermanentMoveTrap();
                            await trapScripts[4].PermanentMoveTrap();
                            _ = trapScripts[3].PermanentMoveTrap(0, 2.4f, 0, 0);
                        }
                        break;

                        case "Tubi Tubi Trigger 1":
                        if (!hasTriggered[7])
                        {
                            hasTriggered[7] = true;
                            await Task.Delay(1000);
                            for (int i = 5; i <= 11; i++)
                            {
                                await trapScripts[i].PermanentMoveTrap();
                                await Task.Delay (150);
                            }
                        }
                        break;

                        case "Spike Down Trigger":
                        if (!hasTriggered[10])
                        {
                            await Task.Delay(1500);
                            _ = trapScripts[12].PermanentMoveTrap();
                            hasTriggered[10] = true;
                        }
                        break;

                        case "Laser Trigger":
                        if (!hasTriggered[8])
                        {
                            hasTriggered[8] = true;
                            catBossScript.bossShootLaserControl = true;
                            _ = CatShootLaserAtRandom();
                            //_ = catBossScript.BossShootLaser();
                        }
                        break;

                        case "Push Player Up Trigger":
                        if (!hasTriggered[12])
                        {
                            hasTriggered[12] = true;
                            _ = trapScripts[14].PermanentMoveTrap();
                            TrapTrigger pushPlayerUpTrigger = trapTriggers[13].GetComponent<TrapTrigger>();

                            await Task.Delay(1600);

                            if (pushPlayerUpTrigger.playerIsInTrigger)
                            {
                                await trapScripts[15].PermanentMoveTrap();
                                _ = trapScripts[16].PermanentMoveTrap();
                            }
                        }
                        break;

                        case "Boss Platform Down Trigger":
                        if (!hasTriggered[13])
                        {
                            //* cancel any laser shooting, lower platform and shoot laser for last time
                            hasTriggered[13] = true;
                            catBossScript.bossShootLaserControl = false;
                            await Task.Delay(1500);
                            await trapScripts[17].PermanentMoveTrap();
                            catBossScript.bossShootLaserControl = true;

                            _ = catBossScript.BossShootLaser();
                        }
                        break;
                    }
                }
            }
        }
        catch (System.Exception)
        {
            return;
        }
    }

    async void CheckAttemptAndBossHealth()
    {
        //* traps leading to first kill, health: 3, bullet
        if (BossParent.bossHealth == 3 && !BossParent.hasDamagedBoss[0])
        {
            //bossShootLevel9Control = true;
            BossParent.hasDamagedBoss[0] = true;

            //* boss teleport animation
            await PlayerSlowMoAfterBossKill(47.9f, 57.91f, new Vector3 (70.2f, 3, 0));

            BossShootAtRandomTime();
        }

        //* health 2, laser eyes
        if (BossParent.bossHealth == 2 && !BossParent.hasDamagedBoss[1])
        {
            BossParent.hasDamagedBoss[1] = true;
            catBossScript.bossShootControl = false;
            catBossScript.BossDefaultAnimation();

            await PlayerSlowMoAfterBossKill(115.6f, 124, new Vector3(135f, 3, 0));
            
        }

        if (BossParent.bossHealth == 1 && !BossParent.hasDamagedBoss[2])
        {
            BossParent.hasDamagedBoss[2] = true;
            catBossScript.bossShootLaserControl = false;
            catBossScript.BossDefaultAnimation();

            await PlayerSlowMoAfterBossKill(154f, 162.23f, new Vector3(175, 2, 0));
        }
    }

    private async Task PlayerSlowMoAfterBossKill(float cameraMaxX, float playerMaxX, Vector3 bosFinalPos)
    {
        try
        {
            //* initial delay
            await Task.Delay(300);

            //* slow player and dont accept movement input
            Player.playerBody.gravityScale = 0.05f;
            Player.playerBody.velocity = new Vector2 (0, 0.05f);
            Player.canPlayerMove = false;

            await TeleportBossAndMoveCamera(cameraMaxX, playerMaxX, bosFinalPos);

            await Task.Delay(300);

            //* set player back to normal speed and accept movement input
            Player.playerBody.gravityScale = 10f;
            Player.canPlayerMove = true;
        }
        catch (System.Exception)
        {
            return;
        }
        
    }

    private async Task TeleportBossAndMoveCamera(float cameraMaxX, float playerMaxX, Vector3 bosFinalPos)
    {
        try
        {
            //* initialization
            Vector3 tempPos = mainCamera.transform.position;
            cameraScript.maxX = cameraMaxX;
            playerScript.maxX = playerMaxX;
            cameraScript.playerDependant = false;
            
            //* move camera to cat and shrink cat
            tempPos.x = catOnlyObject.transform.position.x;
            await ShrinkAndGrowCat(0);

            //* move camera
            bool doneMoving = false;
            while (!doneMoving)
            {
                //print("moving camera");
                tempPos.x += speed*Time.deltaTime;
                
                if (mainCamera.transform.position.x >= cameraScript.maxX)
                {
                    tempPos.x = cameraScript.maxX;
                    doneMoving = true;
                }
                mainCamera.transform.position = tempPos;
                await Task.Yield();
            }
            
            //* move and grow cat
            catOnlyObject.transform.parent.transform.position = bosFinalPos;
            await ShrinkAndGrowCat(1);

            cameraScript.playerDependant = true;
            mainCamera.transform.position = new Vector3(playerObject.transform.position.x, mainCamera.transform.position.y, mainCamera.transform.position.z);
        }
        catch (System.Exception)
        {
            return;
        }
        
    }

    private async Task ShrinkAndGrowCat(int i)
    {
        //* shrink
        if (i == 0)
        {
            bool doneShrinking = false;
            try
            {
                while (!doneShrinking)
                {
                    float tempScale = catOnlyObject.transform.localScale.y;
                    tempScale -= 2 * 0.1f * Time.deltaTime;
                    if (tempScale <= 0)
                    {
                        tempScale = 0;
                        doneShrinking = true;
                    }

                    catOnlyObject.transform.localScale = new Vector3 (-1*tempScale, tempScale, tempScale);
                    await Task.Yield();
                }
            }
            catch (System.Exception)
            {
                return;
            }
            
        }
        //* grow
        else if (i == 1)
        {
            bool doneShrinking = false;
            
            try
            {
               while (!doneShrinking)
                {
                    
                    float tempScale = catOnlyObject.transform.localScale.y;
                    tempScale += 2 * 0.1f * Time.deltaTime;
                    if (tempScale >= defaultCatScale.y)
                    {
                        tempScale = defaultCatScale.y;
                        doneShrinking = true;
                    }

                    catOnlyObject.transform.localScale = new Vector3 (-1*tempScale, tempScale, tempScale);
                    await Task.Yield();
                } 
            }
            catch (System.Exception)
            {
                return;
            }
            
        }
        else
        {
            print("Error: Invalid mode.");
        }
        
        await Task.Delay(500);

    }

    private async void BossShootAtRandomTime()
    {
        while (BossParent.bossHealth == 3 && catBossScript.bossShootControl)
        {
            /*if (!Player.isPlayerAlive)
            {
                print("BossShootAtRandom, RETURNING: player isn't alive, " + Player.isPlayerAlive);
                return;
            }*/
            int beforeAttempt = GameManager.attempts;

            print("level9 shooting, attempt: " + GameManager.attempts);
            Stopwatch watch = Stopwatch.StartNew();
            watch.Start();
            if (catBossScript.bossShootControl && !PauseMenu.isPaused)
                await catBossScript.ShootNormalBullets();
            watch.Stop();
            print($"elapsed time for ShootNormalBullets in Boss: {watch.ElapsedMilliseconds}");
            if (beforeAttempt != GameManager.attempts)
            {
                return;
            }
            
            //* random time to shoot
            float randomTime = Random.Range (1.4f, 3.4f);
            float elapsedTime = 0;

            //* waiting
            while (elapsedTime < randomTime)
            {
                elapsedTime += Time.deltaTime;

                //* return if player dies or cancellation is requested
                if (beforeAttempt != GameManager.attempts || cancellationTokenSource.IsCancellationRequested)
                {
                    //print($"returning as attempt has changed, beforeAttempt: {beforeAttempt}, currentAttempt: {GameManager.attempts}" + " or cancellation requested");
                    //catBossScript.bossShootControl = false;
                    return;
                }

                await Task.Yield();
            }
            //await Task.Delay (randomTime);
        }
    }

    private async void TelekinesisPlayer()
    {
        catBossScript.TelekinesisOnPlayer();
        //print("Level9 in TelekinesisPlayer AFTER TELEKINESISONPLAYER IN BOSS:\ncatBossScript.bossShootControl: " + catBossScript.bossShootControl);

        //* slow player and dont accept movement input
        Player.playerBody.gravityScale = 0f;
        Player.playerBody.velocity = new Vector2 (0, 0f);
        Player.canPlayerMove = false;

        await ChangePlayerColor();

        await MovePlayer();
    }

    private async Task CatShootLaserAtRandom()
    {
        while (catBossScript.bossShootLaserControl)
        {
            await catBossScript.BossShootLaser();

            //* random time to shoot
            float randomTime = Random.Range (0.8f, 1.5f);
            float elapsedTime = 0;

            //* waiting
            while (elapsedTime < randomTime)
            {
                elapsedTime += Time.deltaTime;

                //* return if player dies or cancellation is requested
                if (cancellationTokenSource.IsCancellationRequested)
                {
                    return;
                }

                await Task.Yield();
            }
        }
        
    }

    private async Task ChangePlayerColor()
    {
        try
        {
            float elapsedTime = 0f;
            Color initialColor = playerRenderer.material.color;

            while (elapsedTime < 1f)
            {
                elapsedTime += Time.deltaTime;
                float blueGreen = Mathf.Lerp(initialColor.a, 0.78f, elapsedTime/1f);
                playerRenderer.material.color = new Color(initialColor.r, blueGreen, blueGreen, initialColor.a);
                await Task.Yield();
            }
            playerRenderer.material.color = new Color(initialColor.r, 0.78f, 0.78f, initialColor.a);
            gameObject.SetActive(false);
        }
        catch (System.Exception )
        {
            return;
        }
    }

    private async Task MovePlayer()
    {
        try
        {
            Vector3 tempPos = playerObject.transform.position;
            float finalXPos = 49.35f;
            float finalYPos = 0;

            bool negativeX = false;
            bool negativeY = false;

            if (tempPos.x - finalXPos < 0)
                negativeX = true;

            if (tempPos.y - finalYPos < 0)
                negativeY = true;

            //print($"tempPosY: {tempPos.y}, finalYPos: {finalYPos}, negativeY: {negativeY}");

            //* move Y
            while ((tempPos.y < finalYPos && negativeY) || (tempPos.y > finalYPos && !negativeY))
            {
                //print("moving player");
                tempPos.y += (negativeY ? 1 : -1) * 2 * Time.deltaTime;
                playerObject.transform.position = tempPos;
                await Task.Yield();
            }

            await Task.Delay(200);
            //* move X
            while ((tempPos.x < finalXPos && negativeX) || (tempPos.x > finalXPos && !negativeX))
            {
                //print("moving player");
                tempPos.x += (negativeX ? 1 : -1) * 10 * Time.deltaTime;
                playerObject.transform.position = tempPos;
                await Task.Yield();
            }

            tempPos.x = finalXPos;
            playerObject.transform.position = tempPos;
        }
        catch (System.Exception)
        {
            return;
        }
    }

    void OnApplicationQuit()
    {
        if (cancellationTokenSource != null)
        {
            cancellationTokenSource.Cancel();
            cancellationTokenSource.Dispose();
        }
    }
}
