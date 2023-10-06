using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FuelWaypoints : MonoBehaviour
{
    public GameObject[] WayPoints;
    private int CurrentWaypoint;
    public GameObject Player;
    public GameObject WaySource;
    public float MinimumSpace;
    public float TimeBetweenPoints;
    private float t;
    private bool DefaultWaypoint;
    public bool NewWaypoint;
    private Transform Ppos;
    // Start is called before the first frame update
    void Start()
    {
       
        Player = GameObject.FindGameObjectWithTag("Player");
        WayPoints = GameObject.FindGameObjectsWithTag("WayPoint");
        if (WayPoints != null)
        {
            for (int i = 0; i < WayPoints.Length; i++)
            {
                if (WayPoints[i].GetComponentInChildren<FuelCell>())
                {
                    GameObject temp = WayPoints[i].transform.GetChild(0).gameObject;
                    Destroy(temp);

                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        

        
        
    }



}
