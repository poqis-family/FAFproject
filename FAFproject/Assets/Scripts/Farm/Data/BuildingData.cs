using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingData
{
    public int UID;
    public int instanceID;
    public BuildingEnum.buildingType buildingType;
    public SceneEnum.Scenes buildingScene;
    public bool isUpgrading;
    public Vector3Int pos;
    public int nowLevel;
}
