using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spin : MonoBehaviour
{
    public Vector3 rotateVector;

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.Rotate(rotateVector * Time.deltaTime);

    }
}
