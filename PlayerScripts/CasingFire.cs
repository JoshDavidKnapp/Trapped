using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CasingFire : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(DestroyCasing());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator DestroyCasing()
    {
        GetComponent<Rigidbody>().AddForce((-transform.forward + transform.right) * 0.4f, ForceMode.Impulse);
        yield return new WaitForSeconds(3);
        Destroy(this.gameObject);
    }
        
       
}
