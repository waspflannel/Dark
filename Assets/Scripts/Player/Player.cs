using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

[DisallowMultipleComponent]

#region REQUIRE COMPONENTS
[RequireComponent(typeof(IdleEvent))]
[RequireComponent(typeof(Idle))]
[RequireComponent(typeof(AimWeaponEvent))]
[RequireComponent(typeof(SortingGroup))]
[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(PolygonCollider2D))]
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Health))]
[RequireComponent(typeof(PlayerControl))]
[RequireComponent(typeof(AnimatePlayer))]
[RequireComponent(typeof(MovementByVelocity))]
[RequireComponent(typeof(MovementByVelocityEvent))]
[RequireComponent(typeof(AttackEvent))]

[RequireComponent(typeof(FallingEvent))]
[RequireComponent(typeof(InventoryUpdateEvent))]

[RequireComponent(typeof(JumpEvent))]
#endregion REQUIRE COMPONENTS
public class Player : MonoBehaviour
{
    [HideInInspector] public PlayerDetailsSO playerDetails;
    [HideInInspector] public Health health;
    [HideInInspector] public IdleEvent idleEvent;
    [HideInInspector] public MovementByVelocityEvent movementByVelocityEvent;
    [HideInInspector] public JumpEvent jumpEvent;
    [HideInInspector] public AimWeaponEvent aimWeaponEvent;
    [HideInInspector] public SpriteRenderer spriteRenderer;
    [HideInInspector] public Animator animator;
    [HideInInspector] public FallingEvent fallingEvent;
    [HideInInspector] public InventoryUpdateEvent inventoryUpdateEvent;
    [HideInInspector] public AttackEvent attackEvent;

    private void Awake()
    {
        inventoryUpdateEvent = GetComponent<InventoryUpdateEvent>();
        health = GetComponent<Health>();
        idleEvent = GetComponent<IdleEvent>();
        jumpEvent = GetComponent<JumpEvent>();
        aimWeaponEvent = GetComponent<AimWeaponEvent>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        movementByVelocityEvent = GetComponent<MovementByVelocityEvent>();
        fallingEvent = GetComponent<FallingEvent>();
        attackEvent = GetComponent<AttackEvent>();

    }

    
    public void Initialize(PlayerDetailsSO playerDetails)
    {
        this.playerDetails = playerDetails;
        SetPlayerHealth();

    }
    private void SetPlayerHealth()
    {
        health.SetStartingHealth(playerDetails.playerHealthAmount);
    }
}
