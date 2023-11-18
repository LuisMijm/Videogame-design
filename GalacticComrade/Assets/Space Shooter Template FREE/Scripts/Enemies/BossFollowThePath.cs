using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Timers;

/// <summary>
/// This script moves the ‘Enemy’ along the defined path.
/// </summary>
public class BossFollowThePath : MonoBehaviour
{


    [HideInInspector] public Transform[] path1; //path points which passes the 'Enemy' 
    [HideInInspector] public Transform[] path2;
    [HideInInspector] public int currentPath;


    [HideInInspector] public float speed;
    [HideInInspector] public bool rotationByPath;   //whether 'Enemy' rotates in path direction or not
    [HideInInspector] public bool loop;         //if loop is true, 'Enemy' returns to the path starting point after completing the path
    float currentPathPercent;               //current percentage of completing the path
    Vector3[] pathPositions1;                //path points in vector3
    Vector3[] pathPositions2;
    [HideInInspector] public bool movingIsActive;   //whether 'Enemy' moves or not

    private Timer ChangePhaseTimer_;
    private bool canSwap_;
    public int swapTime_;

    //setting path parameters for the 'Enemy' and sending the 'Enemy' to the path starting point
    public void SetPath()
    {
        currentPathPercent = 0;
        pathPositions1 = new Vector3[path1.Length];       //transform path points to vector3
        pathPositions2 = new Vector3[path2.Length];       
        for (int i = 0; i < pathPositions1.Length; i++)
        {
            pathPositions1[i] = path1[i].position;
        }
        for (int i = 0; i < pathPositions2.Length; i++)
        {
            pathPositions2[i] = path2[i].position;
        }
        transform.position = NewPositionByPath(pathPositions1, 0); //sending the enemy to the path starting point
        if (!rotationByPath)
            transform.rotation = Quaternion.identity;
        movingIsActive = true;
        
        swapTime_ = 5000;
        ChangePhaseTimer_ = new Timer(swapTime_);
        ChangePhaseTimer_.Elapsed += CheckSwap;
        ChangePhaseTimer_.Start();
    }


    private void CheckSwap(object source, ElapsedEventArgs e)
    {
        canSwap_ = true;
    }

    private void SwapPath()
    {
        currentPath = (currentPath == 0) ? 1 : 0;
        currentPathPercent = 0;
        canSwap_ = false;
        ChangePhaseTimer_.Interval = swapTime_;
    }

    private void Update()
    {
        if (movingIsActive)
        {
            currentPathPercent += speed / 100 * Time.deltaTime;     //every update calculating current path percentage according to the defined speed

            switch (currentPath)
            {
                case 0:
                    transform.position = NewPositionByPath(pathPositions1, currentPathPercent); //moving the 'Enemy' to the path position, calculated in method NewPositionByPath
                    if (rotationByPath)                            //rotating the 'Enemy' in path direction, if set 'rotationByPath'
                    {
                        transform.right = Interpolate(CreatePoints(pathPositions1), currentPathPercent + 0.01f) - transform.position;
                        transform.Rotate(Vector3.forward * 90);
                    }
                    break;
                case 1:
                    transform.position = NewPositionByPath(pathPositions2, currentPathPercent); //moving the 'Enemy' to the path position, calculated in method NewPositionByPath
                    if (rotationByPath)                            //rotating the 'Enemy' in path direction, if set 'rotationByPath'
                    {
                        transform.right = Interpolate(CreatePoints(pathPositions2), currentPathPercent + 0.01f) - transform.position;
                        transform.Rotate(Vector3.forward * 90);
                    }                                           
                    break;
            }

            if (currentPathPercent > 1)                    //when the path is complete
            {
                if (loop)                                   //when loop is set, moving to the path starting point; if not, destroying or deactivating the 'Enemy'
                    currentPathPercent = 0;
                else
                {
                    Destroy(gameObject);
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            SwapPath();
        }

        if (transform.position == path1[0].position && canSwap_)
        {
            SwapPath();
        }
    }

    Vector3 NewPositionByPath(Vector3[] pathPos, float percent)
    {
        return Interpolate(CreatePoints(pathPos), currentPathPercent);
    }

    Vector3 Interpolate(Vector3[] path, float t)
    {
        int numSections = path.Length - 3;
        int currPt = Mathf.Min(Mathf.FloorToInt(t * numSections), numSections - 1);
        float u = t * numSections - currPt;
        Vector3 a = path[currPt];
        Vector3 b = path[currPt + 1];
        Vector3 c = path[currPt + 2];
        Vector3 d = path[currPt + 3];
        return 0.5f * ((-a + 3f * b - 3f * c + d) * (u * u * u) + (2f * a - 5f * b + 4f * c - d) * (u * u) + (-a + c) * u + 2f * b);
    }

    Vector3[] CreatePoints(Vector3[] path)
    {
        Vector3[] pathPositions;
        Vector3[] newPathPos;
        int dist = 2;
        pathPositions = path;
        newPathPos = new Vector3[pathPositions.Length + dist];
        Array.Copy(pathPositions, 0, newPathPos, 1, pathPositions.Length);
        newPathPos[0] = newPathPos[1] + (newPathPos[1] - newPathPos[2]);
        newPathPos[newPathPos.Length - 1] = newPathPos[newPathPos.Length - 2] + (newPathPos[newPathPos.Length - 2] - newPathPos[newPathPos.Length - 3]);
        if (newPathPos[1] == newPathPos[newPathPos.Length - 2])
        {
            Vector3[] LoopSpline = new Vector3[newPathPos.Length];
            Array.Copy(newPathPos, LoopSpline, newPathPos.Length);
            LoopSpline[0] = LoopSpline[LoopSpline.Length - 3];
            LoopSpline[LoopSpline.Length - 1] = LoopSpline[2];
            newPathPos = new Vector3[LoopSpline.Length];
            Array.Copy(LoopSpline, newPathPos, LoopSpline.Length);
        }
        return (newPathPos);
    }
}
