using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LavaGeyser : MonoBehaviour
{
    public GameObject lavaDrop;
    private float rotation;

    public float lavaCooldown;
    public float lavaBetweenTime;

    //public float lavaUpSpeed;
    //public float lavaForwardSpeed;
    //public float lavaDamagePerSecond;
    // Start is called before the first frame update
    void Start()
    {
       // lavaDrop.GetComponent<LavaSpout>().lavaDamage = lavaDamagePerSecond;

       // lavaDrop.GetComponent<LavaSpout>().forwardSpeed = lavaForwardSpeed;
        //lavaDrop.GetComponent<LavaSpout>().upSpeed = lavaUpSpeed;
        StartCoroutine(Lava());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator Lava()
    {
        for (int i = 0; i < 5; i++)
        {
            Instantiate(lavaDrop, transform.position, Quaternion.Euler(0, rotation, 0));
            rotation += 72;
            yield return new WaitForSeconds(lavaBetweenTime);
        }



        yield return new WaitForSeconds(lavaCooldown);
        StartCoroutine(Lava());

    }
}
