using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class FloorController : MonoBehaviour
{
    // public GameObject player_;
    // public GameObject floorPrefab_;
    // public GameObject thinFloorPrefab_;
 

    // private List<GameObject> floorList_;


    public List<GameObject> platforms;
    public List<GameObject> platformPrefabs; 
    public GameObject player_;
    public GameObject spawmFloor_;
    public GameObject coinPrefab_;


    public bool generating_;

    // Start is called before the first frame update
    void Start()
    {
        platforms = new List<GameObject>();
        // platformPrefabs = new List<GameObject>();

        // platformPrefabs.Add((GameObject)Resources.Load("Games/AutoRunner/Prefabs/floor"));
        // platformPrefabs.Add((GameObject)Resources.Load("Games/AutoRunner/Prefabs/thinfloor"));
        // platformPrefabs.Add(Resources.Load("Prefabs/Platform3"));

        player_ = GameObject.Find("Player");
        spawmFloor_ = GameObject.Find("Spawm floor");
        
        generating_ = false;
    }

    void FloorGeneration(Transform tr)
    {
        float spacing_ = Random.Range(3, 10);

        float x = tr.position.x;
        float y = tr.position.y; // +  Random.Range(0, 10);
        // float z = tr.position.z + tr.lossyScale.z;
        float z = tr.position.z + Random.Range(25, 40);

        // float z = platforms[platforms.Count - 1].transform.position.z + platforms[platforms.Count - 1].width + spacing_;


        int platformType = Random.Range(0, 2);

        GameObject tempPlatform = Instantiate(platformPrefabs[platformType]);

        tempPlatform.transform.position = new Vector3(x, y, z);

        platforms.Add(tempPlatform);

        for(int i = 0; i < Random.Range(0, 5); ++i)
        {
            Vector3 coinPosition = new Vector3(
                Random.Range(tempPlatform.transform.position.x, tempPlatform.transform.position.x + tempPlatform.transform.position.x),
                tempPlatform.transform.position.y + 3,
                Random.Range(tempPlatform.transform.position.z, tempPlatform.transform.position.z + tempPlatform.transform.position.z)
            );

            GameObject coin = Instantiate(coinPrefab_, coinPosition, Quaternion.identity);
        }
    }

    public void ResetFloors()
    {
        foreach(GameObject platform in platforms)
        {
            platforms.Remove(platform);
            Destroy(platform.gameObject);
        }
        generating_ = false;
    }

    public void FirstFloorsGeneration()
    {
        FloorGeneration(spawmFloor_.transform);

        for(int i = 0; i < 4; ++i)
        {
            FloorGeneration(platforms[i].transform);
        }
    }

    void FloorGeneration()
    {
        foreach(GameObject platform in platforms)
        {
            if(platform.transform.position.z < (player_.transform.position.z - 10))
            {
                platforms.Remove(platform);

                Destroy(platform.gameObject);

                FloorGeneration(platforms[platforms.Count - 1].transform);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        // foreach(GameObject evaluated in floorList_)
        // {
        //     if (evaluated.transform.position.z < player_.transform.position.z)
        //         Destroy(evaluated);
        //         switch(Random.Range(0, 2))
        //     {
        //         case 0:
        //             floorPrefab_ = GameObject.Instantiate<GameObject>(floorPrefab_);
        //             floorList_.Add(floorPrefab_);
        //             break;

        //         case 1:
        //             thinFloorPrefab_ = GameObject.Instantiate<GameObject>(thinFloorPrefab_);
        //             //thinFloorPrefab_.transform.position.z = player_.transform.position.z + ;
        //             floorList_.Add(thinFloorPrefab_);
        //             break;
        //         default:
        //             break;
        //     }
        // }

        if(generating_)
        {
            FloorGeneration();
        }
    }
}
