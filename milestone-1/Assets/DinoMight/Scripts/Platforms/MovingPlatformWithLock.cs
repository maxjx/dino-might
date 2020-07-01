using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatformWithLock : MovingPlatform
{
    private bool isLocked = true;

    new private void Update() {
        if (!isLocked)
        {
            Movement();
        }
    }

    public void toggleLock()
    {
        isLocked = !isLocked;
    }
}
