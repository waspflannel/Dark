using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField] private int itemCode;
    private SpriteRenderer spriteRenderer;



    public int ItemCode { get => itemCode; set => itemCode = value; }


    private void Awake()
    {
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }


    private void Start()
    {
        if (ItemCode != 0)
        {
            Init(ItemCode);
        }
    }

    public void Init(int itemCode)
    {

    }
}
