using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class LevelSpecificTrap : Trap
{
    public async Task MoveAndGrowRockLevel3()
    {
        try
        {
            await PermanentMoveTrap();
            print("After PermanentMoveTrap");
            _ = PositiveScaleRock();
            _ = TemporaryMoveTrap(initialXPos, initialYPos+10, finalXPos, finalYPos);
        }
        catch (System.Exception)
        {
            return;
        }
    }
}
