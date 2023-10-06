using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyThis : MonoBehaviour
{
    public float destroyTime;


    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(DestroyThisObject());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator DestroyThisObject()
    {
        yield return new WaitForSeconds(destroyTime);
        Destroy(this.gameObject);
    }
}
