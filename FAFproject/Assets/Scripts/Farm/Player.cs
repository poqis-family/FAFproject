﻿using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Tilemaps;
using UnityEngine;
using UnityEngine.Tilemaps;
using WJExcelDataClass;
using Tile = UnityEngine.WSA.Tile;

public class Player : MonoBehaviour
{
    public Vector2 direction;
    private Rigidbody2D _rigidbody2D;
    public float speed;
    private Animator animator;
    public static Player _Instance;
    // Start is called before the first frame update
    private void Awake()
    {
        _Instance = this;
    }
    void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        animator.SetInteger("DirectionEnum", (int) PlayerAnimEnum.PlayerDirection.Down);//初始位置向下
    }

    // Update is called once per frame
    void Update()
    {
        GetInput();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Items"))
        {
            FarmDataManager._Instance.ItemAdd(10001, 1);
            FarmDataManager._Instance.ItemAdd(10002, 1);
            FarmDataManager._Instance.ItemAdd(10003, 1);
            FarmDataManager._Instance.ItemAdd(10004, 1);
            CheckItemLiftable();
        }
    }
    
    private void FixedUpdate()
    {
        _rigidbody2D.velocity = direction.normalized * speed; ;
    }
    private void GetInput()
    {
        if (Input.GetKeyDown(KeyCode.F))//后续所有道具交互类都放在这里了
        {
            if (BackpackData.nowItemID!=0)
            {
             UseItem();   
            }
        }
        direction = Vector2.zero;
        if (Input.GetKey(KeyCode.W))
        {
            direction += Vector2.up;
            animator.SetInteger("DirectionEnum", (int)PlayerAnimEnum.PlayerDirection.Up);
        }
        
        if (Input.GetKey(KeyCode.S))
        {
            direction += Vector2.down;
            animator.SetInteger("DirectionEnum", (int) PlayerAnimEnum.PlayerDirection.Down);
        }
        if (Input.GetKey(KeyCode.A))
        {
            direction += Vector2.left;
            animator.SetInteger("DirectionEnum", (int) PlayerAnimEnum.PlayerDirection.Left);
        }
        if (Input.GetKey(KeyCode.D))
        {
            direction += Vector2.right;
            animator.SetInteger("DirectionEnum", (int) PlayerAnimEnum.PlayerDirection.Right);
        }

        //Debug.Log(direction);
        if (direction.Equals(Vector2.zero))
        {
            animator.SetInteger("AnimStageEnum", (int) PlayerAnimEnum.PlayerAnimStage.Idle);
        }   
        else if (!direction.Equals(Vector2.zero))
        {
            animator.SetInteger("AnimStageEnum", (int) PlayerAnimEnum.PlayerAnimStage.Walking);
        }
        
    }

    public void CheckItemLiftable()
    {
        BackpackData.RefreshItemID();
        SpriteRenderer itemImg = FindChild.FindTheChild(gameObject, "PropIMG").GetComponent<SpriteRenderer>();

        if (BackpackData.nowItemID==0||FarmDataManager._Instance.dataManager.GetPropsItemByID(BackpackData.nowItemID).isLiftable==0)
        {
            itemImg.sprite = null;
            animator.SetInteger("LiftableEnum", (int) PlayerAnimEnum.Liftable.disable);//不可举起
        }
        else if (FarmDataManager._Instance.dataManager.GetPropsItemByID(BackpackData.nowItemID).isLiftable==1)
        {
            Sprite temp= Resources.Load("UI/ItemIMG/" + FarmDataManager._Instance.dataManager.GetPropsItemByID(BackpackData.nowItemID).img,typeof(Sprite)) as Sprite;
            itemImg.sprite = temp;
            animator.SetInteger("LiftableEnum", (int) PlayerAnimEnum.Liftable.enable);//可举起
        }
    }

    private void UseItem()
    {
        PropsItem thisItemData = FarmDataManager._Instance.dataManager.GetPropsItemByID(BackpackData.nowItemID);
        UseItemIfTools(thisItemData);
        UseItemIfSeeds(thisItemData);
    }

    private void UseItemIfTools(PropsItem thisItemData)
    {
        if (thisItemData.mainType==(int)ItemTypeEnum.MainItemType.tools)
        {
            UseItemIfHoe(thisItemData);
            UseItemIfWateringCan(thisItemData);
        }
    }
    private void UseItemIfHoe(PropsItem thisItemData)
    {
        if (thisItemData.subType==(int)ItemTypeEnum.ToolsType.hoe)
        {
            if (TileMapController._Instance.CheckArable(Vector3Int.FloorToInt(gameObject.transform.position)))
            {
                TileMapController._Instance.PlowLand(Vector3Int.FloorToInt(gameObject.transform.position));
            }
        }
    }
    private void UseItemIfWateringCan(PropsItem thisItemData)
    {
        if (thisItemData.subType==(int)ItemTypeEnum.ToolsType.wateringCan)
        {
            if (TileMapController._Instance.CheckWaterable(Vector3Int.FloorToInt(gameObject.transform.position)))
            {
                TileMapController._Instance.WateringLand(Vector3Int.FloorToInt(gameObject.transform.position));
            }
        }
    }
    private void UseItemIfSeeds(PropsItem thisItemData)
    {
        if (thisItemData.mainType == (int) ItemTypeEnum.MainItemType.seeds)
        {
            if (TileMapController._Instance.CheckSownable(Vector3Int.FloorToInt(gameObject.transform.position)))
            {
                TileMapController._Instance.SowingSeed(Vector3Int.FloorToInt(gameObject.transform.position), thisItemData.para1);
            }
        }
    }
    
}
