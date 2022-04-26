using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]

public class Wall : MonoBehaviour
{
    public enum WallType{
        Normal
    }

    public WallType WallTypeSelected {get; private set;} = WallType.Normal;

    public Wall(WallType wallType = WallType.Normal){
        this.WallTypeSelected = wallType;
    }
}
