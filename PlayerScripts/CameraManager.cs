using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
public class CameraManager : MonoBehaviour
{
    public CinemachineVirtualCamera introCam;

    public CinemachineVirtualCamera playerCam;

    private void Awake()
    {

        //playerCam = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<CinemachineVirtualCamera>();


    }

    // Start is called before the first frame update
    void Start()
    {
        //StartCoroutine(Intro());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    //function to call to find the player
    public void FindPlayerAtStart()
    {
        playerCam = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<CinemachineVirtualCamera>();
    }

    public IEnumerator Intro()
    {
        yield return new WaitForSeconds(3.5f);
        introCam.gameObject.SetActive(false);
        playerCam.gameObject.SetActive(true);
    }

}
