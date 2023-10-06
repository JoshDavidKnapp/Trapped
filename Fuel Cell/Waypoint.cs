using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoint : MonoBehaviour
{
    public GameObject PointControl;
    public int WaypointNumber;
    public bool Exist;
    // Start is called before the first frame update
    void Start()
    {
        PointControl = GameObject.FindGameObjectWithTag("Spawner");
        Exist = true;
    }

    // Update is called once per frame
    void Update()
    {


        //transform.gameObject.tag = "SomeTagName";
        if (PointControl.GetComponent<FuelWaypoints>().NewWaypoint == true)
        {
            
        }
    }
}
