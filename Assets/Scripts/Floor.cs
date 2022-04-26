using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Floor
{
    public int floorNum {
        get;
        private set;
    }

    [SerializeField]
    public Room[,] rooms;
    public Floor(int floorNum, Room[,] rooms){
        this.floorNum = floorNum;
        this.rooms = rooms;
    }
}
