using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingGenerator : MonoBehaviour
{
    public GameObject floorPrefab;
    public GameObject wallPrefab;
    public GameObject roofPrefab;
    public GameObject doorPrefab;
    public GameObject windowPrefab;
    public GameObject balconyPrefab;
    public GameObject doorVariantPrefab;
    public GameObject roofVariantPrefab;
    public GameObject roofOverhangPrefab;
    public GameObject domePrefab;
    private bool includeRoof = true;
    public int seedForSize = 10;
    public int seedForFloorCount = 10;
    private float doorPercent;
    private float windowPercent;
    private float balconyPercent;
    private float doorVariantPercent;
    private float roofVariantPercent;
    private float roofOverhangPercent;
    private float domePercent;
    private int width;
    private int height;
    private Floor[] floors;
    public List<GameObject> buildings = new List<GameObject>();
    private int numFloors;
    private int distBetweenBuildings = 0;
    private int offset_w;
    private int offset_h;

    public void generateWidthHeight(){
        for (int i = 0; i < 6; i++)
        {
            width = Random.Range(3,seedForSize);
            height = Random.Range(3,seedForSize);
            offset_w = width*i; 
            offset_h = height*i;
            GenerateDS();
            RenderPrefabs();
        }
    }
    // public void drawBuilding(){
    //     GenerateDS();
    //     RenderPrefabs();
    // }
    public void destroy(){
        for(int i = 0; i <  buildings.Count; i++)
        {
            DestroyImmediate(buildings[i]);
        }
        buildings.Clear();
    }
    public int[,] createPatches(int width, int height){
        int[,] patches = new int[width, height];
        int patch_number = Random.Range(1, int.MaxValue);
        // "_|-" shape
        if(patch_number%17==0){
            for (int i = 0; i < height; i++)
            {
                if(i<height/2){
                    patches[width-1, i]=1;
                }
                else{
                    patches[(width/2), i]=1;
                }
            }
            for (int i = 0; i < width; i++)
            {
                patches[i, (height/2)]=1;    
            }
            Debug.LogFormat("'_|-' shape");
        }
        // "X" shape
        else if(patch_number%13==0){
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    if(i==j){
                        patches[i, j]=1;
                    }
                }
            }
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    if((i+j)==(height-1)){
                        patches[i, j]=1;
                    }
                }
            }
            Debug.LogFormat("'X' shape"); 
        }
        // "H" shape
        else if(patch_number%11==0){
            for (int i = 0; i < height; i++)
            {
                patches[(width/2), i]=1;    
            }
            for (int i = 0; i < width; i++)
            {
                patches[i, 0]=1;    
                patches[i, (height-1)]=1;
            }
            Debug.LogFormat("'H' shape");
        }
        // "T" shape
        else if(patch_number%7==0){
            for (int i = 0; i < height; i++)
            {
                patches[0, i]=1;    
            }
            for (int i = 0; i < width; i++)
            {
                patches[i, (height/2)]=1;    
            }
            Debug.LogFormat("'T' shape");
        }
        // "|_|" shape
        else if(patch_number%5==0){
            for (int i = 0; i < height; i++)
            {
                patches[width-1, i]=1;
                patches[0, i]=1;    
            }
            for (int i = 0; i < width; i++)
            {
                patches[i, height-1]=1;    
            }
            Debug.LogFormat("'|_|' shape");
        }
        // "L" shape
        else if(patch_number%3==0){
            for (int i = 0; i < height; i++)
            {
                patches[width-1, i]=1;    
            }
            for (int i = 0; i < width; i++)
            {
                patches[i, height-1]=1;    
            }
            Debug.LogFormat("'L' shape");
        }
        // "+" shape
        else{
            for (int i = 0; i < height; i++)
            {
                patches[(width/2), i]=1;    
            }
            for (int i = 0; i < width; i++)
            {
                patches[i, (height/2)]=1;    
            }
            Debug.LogFormat("'+' shape");
        }
        return patches;
    }
    public int[,] createHeights(int width, int height){
        int[,] verticalHeigths = new int[width, height];
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                int k = Random.Range(1,seedForFloorCount);
                verticalHeigths[i, j] = k;
            }
        }
        return verticalHeigths;
    }
    public int maxFloorsUtility(int width, int height){
        int[,] temp = createHeights(width, height);
        int max = temp[0,0];
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            { 
                if(temp[i,j] > max)
                {
                    max = temp[i,j];
                }
            }
        }
        return max;
    }
    void GenerateDS(){
        doorPercent = Random.Range(0.0f, (float)seedForSize)/(float)seedForSize;
        windowPercent = Random.Range(0.0f, (float)seedForSize)/(float)seedForSize;
        balconyPercent = Random.Range(0.0f, (float)seedForSize)/(float)seedForSize;
        doorVariantPercent = Random.Range(0.0f, (float)seedForSize)/(float)seedForSize;
        roofVariantPercent = Random.Range(0.0f, (float)seedForSize)/(float)seedForSize;
        roofOverhangPercent = Random.Range(0.0f, (float)seedForSize)/(float)seedForSize;
        domePercent = Random.Range(0.0f, (float)seedForSize)/(float)seedForSize;
        numFloors = maxFloorsUtility(width, height);
        floors = new Floor[numFloors];
        int currFloor = 0;
        
        foreach (Floor floor in floors)
        {
            Room[,] rooms = new Room[width, height];
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    rooms[i,j] = new Room(new Vector2((i+offset_w)*(distBetweenBuildings+1),(j+offset_h)*(distBetweenBuildings+1)),
                                            includeRoof?(currFloor == floors.Length-1):false);
                }
            }
            floors[currFloor] = new Floor(currFloor++, rooms);
        }
    }

    void RenderPrefabs(){
        int[,] patches = createPatches(width, height);
        int[,] verticalHeights = createHeights(width, height);
        bool flag = false;
        foreach (Floor floor in floors)
        {
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    GameObject prefabToUse, prefabToUse_d, prefabToUse_r;
                    Room room = floor.rooms[i,j];
                    if(patches[i,j]==1 && verticalHeights[i, j]>0){
                        if(verticalHeights[i, j]==numFloors){
                            flag = true;
                        }
                        if(floor.floorNum==0){
                            float temp = Random.Range(0.0f, 1.0f);
                            if(temp>doorVariantPercent && temp<doorPercent){
                                prefabToUse_d = doorVariantPrefab;
                            }
                            else{
                                prefabToUse_d = doorPrefab;
                            }
                            GameObject wall1 = Instantiate(prefabToUse_d, new Vector3(room.RoomPosition.x, floor.floorNum+0.5f, room.RoomPosition.y), Quaternion.Euler(0,0,0));
                            wall1.transform.parent = transform;
                            GameObject wall2 = Instantiate(wallPrefab, new Vector3(room.RoomPosition.x+0.5f, floor.floorNum+0.5f, room.RoomPosition.y+0.5f), Quaternion.Euler(0,-90,0));
                            wall2.transform.parent = transform;
                            GameObject wall3 = Instantiate(prefabToUse_d, new Vector3(room.RoomPosition.x, floor.floorNum+0.5f, room.RoomPosition.y+1.0f), Quaternion.Euler(0,-180,0));
                            wall3.transform.parent = transform;
                            GameObject wall4 = Instantiate(wallPrefab, new Vector3(room.RoomPosition.x-0.5f, floor.floorNum+0.5f, room.RoomPosition.y+0.5f), Quaternion.Euler(0,90,0));
                            wall4.transform.parent = transform;
                            GameObject floor_ = Instantiate(floorPrefab, new Vector3(room.RoomPosition.x, floor.floorNum, room.RoomPosition.y+0.5f), Quaternion.identity);
                            floor_.transform.parent = transform;
                            buildings.Add(wall1);
                            buildings.Add(wall2);
                            buildings.Add(wall3);
                            buildings.Add(wall4);
                            buildings.Add(floor_);
                        }
                        else {
                            if (floor.floorNum>0 && Random.Range(0.0f, 1.0f)>windowPercent/2.0f){
                                prefabToUse = windowPrefab;
                                if(Random.Range(0.0f, 1.0f)>balconyPercent/2.0f){
                                    prefabToUse = null;
                                    prefabToUse = balconyPrefab;
                                }
                            }
                            else{
                                prefabToUse = wallPrefab;
                            }
                            GameObject wall1 = Instantiate(prefabToUse, new Vector3(room.RoomPosition.x, floor.floorNum+0.5f, room.RoomPosition.y), Quaternion.Euler(0,0,0));
                            wall1.transform.parent = transform;
                            GameObject wall2 = Instantiate(prefabToUse, new Vector3(room.RoomPosition.x+0.5f, floor.floorNum+0.5f, room.RoomPosition.y+0.5f), Quaternion.Euler(0,-90,0));
                            wall2.transform.parent = transform;
                            GameObject wall3 = Instantiate(prefabToUse, new Vector3(room.RoomPosition.x, floor.floorNum+0.5f, room.RoomPosition.y+1.0f), Quaternion.Euler(0,-180,0));
                            wall3.transform.parent = transform;
                            GameObject wall4 = Instantiate(prefabToUse, new Vector3(room.RoomPosition.x-0.5f, floor.floorNum+0.5f, room.RoomPosition.y+0.5f), Quaternion.Euler(0,90,0));
                            wall4.transform.parent = transform;
                            GameObject floor_ = Instantiate(floorPrefab, new Vector3(room.RoomPosition.x, floor.floorNum, room.RoomPosition.y+0.5f), Quaternion.identity);
                            floor_.transform.parent = transform;
                            buildings.Add(wall1);
                            buildings.Add(wall2);
                            buildings.Add(wall3);
                            buildings.Add(wall4);
                            buildings.Add(floor_);
                        }
                        verticalHeights[i, j]--;
                        if(verticalHeights[i, j]==0){
                            if(Random.Range(0.0f, 1.0f)>domePercent){
                                prefabToUse_r = domePrefab;
                            }
                            else if(Random.Range(0.0f, 1.0f)>roofVariantPercent){
                                prefabToUse_r = roofVariantPrefab;
                            }
                            else
                            {
                                prefabToUse_r = roofPrefab;
                            }
                            GameObject roof = Instantiate(prefabToUse_r, new Vector3(room.RoomPosition.x, floor.floorNum+0.5f+0.5f, room.RoomPosition.y+0.5f), Quaternion.identity);
                            roof.transform.parent = transform;
                            buildings.Add(roof);   
                        }
                    }
                    else{
                        continue;
                    }
                    if(room.hasRoof && flag){
                        prefabToUse_r = roofOverhangPrefab;
                        GameObject roof = Instantiate(prefabToUse_r, new Vector3(room.RoomPosition.x, floor.floorNum+0.5f+0.5f, room.RoomPosition.y+0.5f), Quaternion.identity);
                        roof.transform.parent = transform;
                        buildings.Add(roof);
                    }
                }
            }
        }
    }
    void OnValidate(){
        if(seedForSize<=3){
            seedForSize=3;
        }
    }
}
