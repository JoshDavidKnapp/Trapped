using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestItem : MonoBehaviour
{
    public float force;

    private GameObject playerGameObject;

    // Start is called before the first frame update
    void Start()
    {
        
        GetComponent<Rigidbody>().AddForce(Vector3.up * force, ForceMode.Impulse);
        GetComponent<Rigidbody>().AddForce(Vector3.forward * force/4, ForceMode.Impulse);

        playerGameObject = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, 2, 0);
    }

    private void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.tag == "Player")
        {
           // Destroy(this.gameObject);
        }
    }

}
