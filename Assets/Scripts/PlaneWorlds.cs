using System;
using System.Collections.Generic;
using UnityEngine;

public class PlaneWorlds : MonoBehaviour
{
    public List<Vector3> BlockPositions;

    public Transform[] blocks = new Transform[3];
    // Start is called before the first frame update
    public void Start()
    {
        for (int x = 0; x < 51; x++)
        {
            for (int z = 0; z < 51; z++)
            {
                for (int y = 5; y > 0; y--)
                {
                    BlockPositions.Add(new Vector3(x, y, z));
                }
            }
        }

        foreach (Vector3 blockPosition in BlockPositions)
        {
            switch (blockPosition.y)
            {
                case 5:
                    SetBlock(blocks[0], blockPosition, "grass" + blockPosition);
                    break;
                case 4:
                case 3:
                case 2:
                    SetBlock(blocks[1], blockPosition, "dirt" + blockPosition);
                    break;
                case 1:
                    SetBlock(blocks[2], blockPosition, "bedrock" + blockPosition);
                    break;
            }
        }

        Destroy(GameObject.Find("dirt"));
        Destroy(GameObject.Find("grass"));
        Destroy(GameObject.Find("bedrock"));
    }
    public void SetBlock(Transform blk, Vector3 pos, string name)
    {
        Transform block = Instantiate(blk);
        block.tag = "Block";
        block.name = name;
        switch (name.Split(new[] {'('}, 2)[0])
        {
            case "grass":
                block.transform.parent = GameObject.Find("Grasss").transform;
                break;
            case "dirt":
                block.transform.parent = GameObject.Find("Dirts").transform;
                break;
            case "bedrock":
                block.transform.parent = GameObject.Find("Bedrocks").transform;
                break;
        }

        block.transform.position = pos;
    }
}