using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningBox : MonoBehaviour
{
    public float chargeTime;
    public GameObject lightningObj;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            print("detected collision");
            StartCoroutine(Charge());
        }
    }

    public IEnumerator Charge()
    {
        print("starting charge");
        yield return new WaitForSeconds(chargeTime);
        print("calling strike");

        lightningObj.GetComponent<LightningGate>().StrikeIt();
    }
}
