using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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
    public List<GameObject> Coins; 
    public List<GameObject> DestructionList;
    public GameObject player_;
    public GameObject spawmFloor_;
    public GameObject startFloor_;
    public GameObject coinPrefab_;
    public GameObject floorSpawnerPrefab_;


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
        platforms.Add(spawmFloor_);
        startFloor_ = GameObject.Find("Startfloor");
        platforms.Add(startFloor_);
        generating_ = false;
    }

    void FloorGeneration(Transform tr)
    {
        float spacing_ = Random.Range(3, 10);

        float x = tr.position.x;
        float y = tr.position.y; // +  Random.Range(0, 10);

        float z = tr.position.z + tr.localScale.z + spacing_;
        //float z = tr.position.z + tr.lossyScale.z;
        //float z = tr.position.z + Random.Range(25, 40);

        // float z = platforms[platforms.Count - 1].transform.position.z + platforms[platforms.Count - 1].width + spacing_;


        int platformType = Random.Range(0, 3);

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

            GameObject Coin = Instantiate(coinPrefab_, coinPosition, Quaternion.identity);
            // coin.transform.LookAt(Vector3.zero);
            Coins.Add(Coin);
        }
    }

    public void newGeneration()
    {
        int cPlatformPos_ = platforms.Count - 1;
        GameObject cPlatform_;

        for(int i = 0; i < 5; ++i)
        {
            // Debug.Log("cPlatform_ =" + cPlatformPos_);
            if (null != (cPlatform_ = platforms[cPlatformPos_]))
            {
                FloorGeneration2(cPlatform_.transform);
                cPlatformPos_++;

                Vector3 coinPosition = new Vector3(
                    Random.Range(cPlatform_.transform.position.x, cPlatform_.transform.position.x + cPlatform_.transform.position.x),
                    cPlatform_.transform.position.y + 2.0f,
                    Random.Range(cPlatform_.transform.position.z, cPlatform_.transform.position.z + cPlatform_.transform.position.z)
                );
                GameObject Coin = Instantiate(coinPrefab_, coinPosition, Quaternion.identity);
                Coins.Add(Coin);
                
            }
        }




        Transform tr; 
        if((tr = platforms[platforms.Count - 3].transform) != null)
        {
            GameObject tempSpawner = Instantiate(floorSpawnerPrefab_);
            tempSpawner.transform.position = new Vector3(tr.position.x, tr.position.y, tr.position.z);
        }
    }

    public void FloorGeneration2(Transform tr)
    {
        float xSpacing_ = Random.Range(-10, 10);
        float ySpacing_ = Random.Range(-10, 10);
        float zSpacing_ = Random.Range(0, 10);


        float x = tr.position.x; // + xSpacing_;
        float y = tr.position.y; // + ySpacing_;


        // float z = tr.position.z + tr.localScale.z + zSpacing_;
        float z = tr.position.z + tr.lossyScale.z + zSpacing_;

        int platformType = Random.Range(0, 5);

        GameObject tempPlatform = Instantiate(platformPrefabs[platformType]);

        tempPlatform.transform.position = new Vector3(x, y, z);

        platforms.Add(tempPlatform);

    }

    public void ResetFloors()
    {
        // Debug.Log("Reset floors");
        /*
         * foreach(GameObject platform in platforms)
        {
            platforms.Remove(platform);
            Destroy(platform.gameObject);
        }
        */

        // for (int i = platforms.Count - 1; i > 0; ++i)
        // {
            // Destroy(platforms[i].gameObject);
            // DestructionList.Add(platforms[i]);
            // platforms.Remove(platforms[i]);

        // }
        

        // for (int i = Coins.Count - 1; i > 0; ++i)
        // {
            // DestructionList.Add(Coins[i]);
            // platforms.Remove(Coins[i]);
        // }



        // foreach (GameObject platform in platforms)
        // {
        //     DestructionList.Add(platform);
        //     platforms.Remove(platform);
        // }
        // foreach (GameObject coin in Coins)
        // {
        //     DestructionList.Add(coin);
        // }

        generating_ = false;
    }

    public void FirstFloorsGeneration()
    {
        FloorGeneration(spawmFloor_.transform);
        //platforms[0].transform.position = new Vector3 (0,0, platforms[0].transform.position.z -= 10);
        Vector3 newPosition = platforms[0].transform.position;
        // newPosition.z -= Random.Range(15, 20);
        newPosition.z -= 20;

        platforms[0].transform.position = newPosition;

        for (int i = 0; i < 4; ++i)
        {
            FloorGeneration(platforms[i].transform);
        }
    }

    void FloorGeneration()
    {
        bool keep = true;
        int count = 0;
        //foreach(GameObject platform in platforms)
        for(int i = 0; i < platforms.Count && keep; ++i)
        {
            if (platforms[i] != null)
            {
                if (platforms[i].transform.position.z < (player_.transform.position.z - 10))
                {
                    // platforms.Remove(platform);

                    // Destroy(platform.gameObject);
                    DestructionList.Add(platforms[i]);

                    FloorGeneration(platforms[platforms.Count - 1].transform);
                    keep = false;
                    count++;
                }
            }
        }
        // Debug.Log("platform count = " + count);
    }

    void WipeUseless()
    {
        if (DestructionList.Count > 0)
        {
            for (int i = DestructionList.Count - 1; i >= 0; i--)
            {
                Destroy(DestructionList[i]);
                // DestructionList.Remove(DestructionList[i]);

                // if("GeneratedFloor" == DestructionList[i].tag)
                // {
                    
                // }else if ("Coin" == DestructionList[i].tag)
                // {

                // }
            }
        }

        platforms.RemoveAll(GameObject => GameObject == null);
        Coins.RemoveAll(GameObject => GameObject == null);
        DestructionList.RemoveAll(GameObject => GameObject == null);

        // for (int i = platforms.Count - 1; i > 0; ++i)
        // {
        //     if (null == platforms[i])
        //     {
        //         platforms.Remove(platforms[i]);
        //     }
        // }
        // for (int i = Coins.Count - 1; i > 0; ++i)
        // {
        //     if (null == Coins[i])
        //     {
        //         platforms.Remove(Coins[i]);
        //     }
        // }
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
            // FloorGeneration();
        }

        // WipeUseless();
    }
}
