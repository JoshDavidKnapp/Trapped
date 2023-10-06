using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalController : MonoBehaviour
{
    bool exited = false;

    public LevelManager levelManager;

    bool completeExit = false;
    public GameObject particleSystem1, particleSytstem2;

    private float timeWait;

    private int flickerCounter;


    private void Start()
    {

        levelManager = GameObject.Find("LevelManager").GetComponent<LevelManager>();

    }
    private void OnTriggerEnter(Collider other)
    {
        if(exited == false&&other.tag == "Player")
        {
            exited = true;
        }
        if (other.tag == "Player" && other.gameObject.GetComponent<PlayerStats>().allCellsCollected)
        {
            levelManager.travereseNextLevel();
        }


    }
    private void Update()
    {
        if(exited && !completeExit)
        {
            portalFlicker();
            if (flickerCounter >= 10)
            {
                completeExit = true;
                particleSystem1.SetActive(false);
                particleSytstem2.SetActive(true);
            }

        }
        

    }

    private void portalFlicker()
    {
        
       
        if(flickerCounter < 10 && timeWait<Time.time)
        {
            timeWait = Time.time + Random.Range(0.5f, 0.8f);
            flickerCounter++;
            if (particleSystem1.activeSelf)
            {
                particleSystem1.SetActive(false);
                particleSytstem2.SetActive(true);
            }
            else
            {
                particleSystem1.SetActive(true);
                particleSytstem2.SetActive(false);
            }

        }
       


    }

    public void switchOnPortal()
    {
        particleSystem1.SetActive(true);
        particleSytstem2.SetActive(false);

    }


}
