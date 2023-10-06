using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
public class BulletShoot : MonoBehaviour
{
    private Camera mainCamera;

    // Start is called before the first frame update
    void Start()
    {
        //GetComponent<Rigidbody>().AddForce(transform.up * 10, ForceMode.Impulse);
        mainCamera = GameObject.Find("Main Camera").GetComponent<Camera>();

        Vector3 aimSpot = mainCamera.transform.position;
        aimSpot += mainCamera.transform.forward * 50f;

        //Projectile is facing the aim spot
        transform.LookAt(aimSpot);

        //Shoot projectile forward (towards the aim spot)
        GetComponent<Rigidbody>().AddForce(this.gameObject.transform.forward * 10, ForceMode.Impulse);

        StartCoroutine(DestroyThis());
    }

    // Update is called once per frame
    void Update()
    {
       // transform.position = Vector3.MoveTowards(transform.position, cam.ScreenToWorldPoint(Input.mousePosition), 100 * Time.deltaTime);

    }

    private void OnCollisionEnter(Collision collision)
    {
        Destroy(this.gameObject);
    }

    public IEnumerator DestroyThis()
    {
        yield return new WaitForSeconds(3);
        Destroy(this);
    }
}
