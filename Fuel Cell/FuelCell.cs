using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class FuelCell : MonoBehaviour
{
    public AudioSource fuelCellSound;

    public GameObject fuel_1, fuel_2, fuel_3, fuel_4;

    private Vector3 Point;
    private GameObject Spawner;
    public float AttractDistance;
    public float FollowDistance;
    public float Speed;
    public int CellNumber;
    private Vector3 FuelDistance;
    private bool Follow;

    public GameObject particleSyst;

    
    // Start is called before the first frame update
    void Start()
    {

        int randomNum = Random.Range(1, 5);
        switch (randomNum)
        {
            case 1:
                fuel_1.SetActive(true);
                break;
            case 2:
                fuel_2.SetActive(true);
                break;
            case 3:
                fuel_3.SetActive(true);
                break;
            case 4:
                fuel_4.SetActive(true);
                break;
            default:
                break;
        }


        //Assigns Spawner a gameobject
        Spawner = GameObject.FindGameObjectWithTag("Spawner");

        transform.parent = GameObject.FindGameObjectWithTag("DeleteParent").transform;

        //Sets follow bool to False
        Follow = false;

        //Assigns Fuel Cell a waypoint
        //CellNumber = Spawner.GetComponent<FuelSpawner>().SpawnedCells;
        
        //Player.transform.position = ;
    }

    // Update is called once per frame
    void Update()
    {
        //Records Assigned Waypoint's transform.position
        
        
        //Checks distance between the fuelcell and Waypoint
        //FuelDistance = Point - gameObject.transform.position;

        //If follow is false, Attracted function is active
        if (!Follow)
        {
            Attracted();
        }
        //Following();
    }

    private void Attracted()
    {
        //Activates fuel cell movement once player is close enough
        if (Vector3.Distance(transform.position, Spawner.GetComponent<FuelWaypoints>().Player.transform.position)<= AttractDistance)
        {
            Vector3 scaletemp;
            Follow = true;
            Spawner.GetComponent<FuelWaypoints>().Player.GetComponent<PlayerStats>().fuelCheck();
            scaletemp = transform.localScale;
            transform.position = Spawner.GetComponent<FuelWaypoints>().WayPoints[CellNumber].transform.position;
            transform.parent = Spawner.GetComponent<FuelWaypoints>().WayPoints[CellNumber].transform;
            transform.localScale = scaletemp;
            particleSyst.SetActive(false);
            fuelCellSound.Pause();
            Destroy(fuelCellSound);
        }
    }

    private void Following()
    {
        if (Follow == true)
        {
            
            //Has fuel cell move to waypoint
            transform.position = Vector3.MoveTowards(transform.position, Point, (Speed * Time.deltaTime));
            particleSyst.SetActive(false);
           
          
            
        }
    }
}
