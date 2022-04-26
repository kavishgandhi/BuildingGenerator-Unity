using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room
{
    public Wall[] walls;
    public Vector2 position;
    public bool hasRoof{
        get;
        private set;
    }

    public Room(Vector2 position, bool hasRoof = false){
        this.position = position;
        this.hasRoof = hasRoof;
    }

    public Vector2 RoomPosition{
        get{
            return this.position;
        }
    }
}
