using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorController : MonoBehaviour
{
    public GameObject player_;
    public GameObject floorPrefab_;
    public GameObject thinFloorPrefab_;
 

    private List<GameObject> floorList_;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        foreach(GameObject evaluated in floorList_)
        {
            if (evaluated.transform.position.z < player_.transform.position.z)
                Destroy(evaluated);
                switch(Random.Range(0, 2))
                {
                case 0:
                    floorPrefab_ = GameObject.Instantiate<GameObject>(floorPrefab_);
                    floorList_.Add(floorPrefab_);
                    break;

                case 1:
                    thinFloorPrefab_ = GameObject.Instantiate<GameObject>(thinFloorPrefab_);
                    //thinFloorPrefab_.transform.position.z = player_.transform.position.z + ;
                    floorList_.Add(thinFloorPrefab_);
                    break;
                }
                
        }
    }
}
