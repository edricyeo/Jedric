using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLevel : MonoBehaviour
{
    public int level { get; private set; }

    private PlayerDash playerDash;
    private PlayerMovement playerMovement;
    private PlayerAttack playerAttack;

    public void Start()
    {
        level = 1;
        playerDash = GetComponent<PlayerDash>();
        playerMovement = GetComponent<PlayerMovement>();
        playerAttack = GetComponent<PlayerAttack>();
    }

    // level 1 -> no powerups, only first portal open
    // level 2 -> unlocked dash, second portal open
    // level 3 -> unlocked double jump, third portal open
    // level 4 -> unlocked rifle, last portal open
    //public void LevelUp()
    //{
    //    if (level == 1)
    //    {
    //        playerDash.ToggleDash();
    //        level++;
    //    }
    //    else if (level == 2)
    //    {
    //        playerMovement.extraJumps = 1;
    //        level++;
    //    }
    //    else if (level == 3)
    //    {
    //        playerAttack.IncreaseFirerate();
    //    }
    //    else if (level == 4)
    //    {

    //    }
    //}
}
