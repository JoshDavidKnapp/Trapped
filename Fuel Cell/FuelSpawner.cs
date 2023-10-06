using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FuelSpawner : MonoBehaviour
{

    public int NumberOfFuelCells;
    public GameObject[] SpawnLocations;
    private bool[] choosenAlready;
    public int ChosenSpawn;
    public GameObject FuelSource;
    public int SpawnedCells;
    

    // Start is called before the first frame update
    void Start()
    {
        choosenAlready = new bool[SpawnLocations.Length];

        for (int i = 0; i < choosenAlready.Length; i++)
        {
            choosenAlready[i] = false;
        }

        SpawnCells();

        
    }
    
    //Spawns Fuel Cells
    IEnumerator FuelSpawn()
    {
        //Continues until a number of fuel cells have spawned equal to the number of cells that are supposed to be in the scene
        while(SpawnedCells < NumberOfFuelCells)
        {
            //Chooses random spawnlocation
            ChosenSpawn = Random.Range(0, SpawnLocations.Length);
            //Spawns fuel cell
            Instantiate(FuelSource, new Vector3(SpawnLocations[ChosenSpawn].transform.position.x, SpawnLocations[ChosenSpawn].transform.position.y, SpawnLocations[ChosenSpawn].transform.position.z), Quaternion.identity);

            yield return new WaitForSeconds(0.0001f);

            SpawnedCells += 1;
        }
    }

    //Function that spawn fuel cells
    public void SpawnCells()
    {
        //Continues until a number of fuel cells have spawned equal to the number of cells that are supposed to be in the scene
        while (SpawnedCells < NumberOfFuelCells)
        {
            //Spawns Fuel cell
            GameObject newObj = Instantiate(FuelSource);
            //Chooses random spawnlocation
            int randomChoice = Random.Range(0, SpawnLocations.Length);

            //If Chosen Spawn has already been chosen
            while (choosenAlready[randomChoice])
            {
                //Chooses random spawnlocation
                randomChoice = Random.Range(0, SpawnLocations.Length);
            }
            //Changes fuel cell's location to new location
            newObj.transform.position = SpawnLocations[randomChoice].transform.position;
            //Assigns number to fuel cell
            newObj.GetComponent<FuelCell>().CellNumber = SpawnedCells;
            //Marks location as already chosen
            choosenAlready[randomChoice] = true;
            SpawnedCells++;

        }

    }


}
