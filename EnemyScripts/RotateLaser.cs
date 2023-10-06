using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateLaser : MonoBehaviour
{
    public float laserSpeed;

    public float laserDamage;

    public float laserKnockback;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void FixedUpdate()
    {
              
        transform.Rotate(0, laserSpeed, 0);

    }

   


}
