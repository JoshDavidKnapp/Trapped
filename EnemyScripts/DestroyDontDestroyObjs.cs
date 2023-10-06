using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyDontDestroyObjs : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
       // Destroy(GameObject.FindGameObjectWithTag("Player"));


    }

    // Update is called once per frame
    void Update()
    {

    }

    public void DestroyCharSelect()
    {
        Destroy(GameObject.Find("CharSelecter"));


    }
    public void DestroyPlayer()
    {
        Destroy(GameObject.FindGameObjectWithTag("Player"));
    }
}
