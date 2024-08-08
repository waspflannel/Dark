using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


[DisallowMultipleComponent]
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(MovementByVelocityEvent))]
[RequireComponent(typeof(JumpEvent))]
public class MovementByVelocity : MonoBehaviour
{
    private Rigidbody2D rigidBody2D;
    private MovementByVelocityEvent movementByVelocityEvent;
    private JumpEvent jumpEvent;

    [SerializeField]private MovementDetailsSO movementDetails;

    private void Awake()
    {
        jumpEvent = GetComponent<JumpEvent>();
        rigidBody2D = GetComponent<Rigidbody2D>();
        movementByVelocityEvent = GetComponent<MovementByVelocityEvent>();
    }

    private void OnEnable()
    {

        movementByVelocityEvent.OnMovementByVelocity += MovementByVelocityEvent_OnMovementByVelocity;
    }

    private void OnDisable()
    {
 
        movementByVelocityEvent.OnMovementByVelocity -= MovementByVelocityEvent_OnMovementByVelocity;
    }


    private void MovementByVelocityEvent_OnMovementByVelocity(MovementByVelocityEvent movementByVelocityEvent, MovementByVelocityEventArgs movementByVelocityEventArgs)
    {
        Run(1, movementByVelocityEventArgs.moveDirection , movementByVelocityEventArgs.isJumping, movementByVelocityEventArgs._isJumpFalling,
            movementByVelocityEventArgs.LastOnGroundTime);
    }


    private void Run(float lerpAmount, Vector2 moveDirection, bool isJumping , bool _isJumpFalling , float LastOnGroundTime)
    {
        float targetSpeed = moveDirection.x * movementDetails.runMaxSpeed;
        targetSpeed = Mathf.Lerp(rigidBody2D.velocity.x, targetSpeed, lerpAmount);

        #region Calculate AccelRate
        float accelRate;

        if (LastOnGroundTime > 0)
            accelRate = (Mathf.Abs(targetSpeed) > 0.01f) ? movementDetails.runAccelAmount : movementDetails.runDeccelAmount;
        else
            accelRate = (Mathf.Abs(targetSpeed) > 0.01f) ? movementDetails.runAccelAmount * movementDetails.accelInAir : movementDetails.runDeccelAmount * movementDetails.deccelInAir;
        #endregion

        #region Add Bonus Jump Apex Acceleration
        if ((isJumping || _isJumpFalling) && Mathf.Abs(rigidBody2D.velocity.y) < movementDetails.jumpHangTimeThreshold)
        {
            accelRate *= movementDetails.jumpHangAccelerationMult;
            targetSpeed *= movementDetails.jumpHangMaxSpeedMult;
        }
        #endregion

        #region Conserve Momentum
        if (movementDetails.doConserveMomentum && Mathf.Abs(rigidBody2D.velocity.x) > Mathf.Abs(targetSpeed) && Mathf.Sign(rigidBody2D.velocity.x) == Mathf.Sign(targetSpeed) && Mathf.Abs(targetSpeed) > 0.01f && LastOnGroundTime < 0)
        {
            accelRate = 0;
        }
        #endregion

        float speedDif = targetSpeed - rigidBody2D.velocity.x;
        float movement = speedDif * accelRate;
        rigidBody2D.AddForce(movement * Vector2.right, ForceMode2D.Force);


    }
}
