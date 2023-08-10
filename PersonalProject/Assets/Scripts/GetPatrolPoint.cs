using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetPatrolPoint : MonoBehaviour
{
    //Definations for spawning area
    private Vector3 origin;
    private Vector3 range;
    private Vector3 randomRange;
    private Vector3 randomCoordinate;

    //Make visible spawn area for scene view
    public Color GizmosColor = new Color(0.5f, 0.5f, 0.5f, 0.2f);

    void Start()
    {
        //Get origin and range from spawner to calculate area
        origin = transform.position;
        range = transform.localScale / 2.0f;
    }

    public Vector3 GetPatrolPostition()
    {
        //Generate random coordinates in area 
        randomRange = new Vector3(Random.Range(-range.x, range.x),
                                          Random.Range(-range.y, range.y),
                                          Random.Range(-range.z, range.z));
        randomCoordinate = origin + randomRange;

        return randomCoordinate;
    }

    //To show spawn area in scene view not too important
    void OnDrawGizmos()
    {
        Gizmos.color = GizmosColor;
        Gizmos.DrawCube(transform.position, transform.localScale);
    }


}