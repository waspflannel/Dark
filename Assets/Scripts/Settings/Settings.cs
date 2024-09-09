using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Settings
{

    #region Animation Parameters
    public static int aimLeft = Animator.StringToHash("aimLeft");
    public static int aimRight = Animator.StringToHash("aimRight"); 
    public static int isIdle = Animator.StringToHash("isIdle");
    public static int isMoving = Animator.StringToHash("isMoving");
    public static int isJumping = Animator.StringToHash("isJumping");
    public static int isFalling = Animator.StringToHash("isFalling");
    public static int isFacingRight = Animator.StringToHash("isFacingRight");
    #endregion Animation Parameters

}

