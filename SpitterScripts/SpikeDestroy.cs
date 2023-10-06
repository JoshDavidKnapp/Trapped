using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeDestroy : MonoBehaviour
{
    //time spike lasts
    public float spikeDuration;

    // Start is called before the first frame update
    void Start()
    {
        //Start coroutine
        StartCoroutine(DestroySpike());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator DestroySpike()
    {
        //wait until spike duration
        yield return new WaitForSeconds(spikeDuration);
        //destroy spike
        Destroy(this.gameObject);
    }
}
