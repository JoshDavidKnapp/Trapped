using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
public class IntroPlayer : MonoBehaviour
{
    public GameObject cameraController;
    public CinemachineVirtualCamera introCam;
    private void Awake()
    {
        cameraController = GameObject.Find("CameraController");
        cameraController.GetComponent<CameraManager>().playerCam = GetComponentInChildren<CinemachineVirtualCamera>();
        introCam = GameObject.Find("IntroCam").GetComponent<CinemachineVirtualCamera>();
        introCam.LookAt = this.gameObject.transform;
        introCam.Follow = this.gameObject.transform;


    }
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(DestroyThis());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator DestroyThis()
    {
        yield return new WaitForSeconds(3.5f);
        Destroy(this.gameObject);
    }

}
