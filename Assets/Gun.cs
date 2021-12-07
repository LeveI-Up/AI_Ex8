using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public float damage = 10f;
    public float range = 50f;
    public float impactForce = 500;

    public Camera camera;
    public ParticleSystem muzzleFlash;

    // Update is called once per frame
    void Update()
    {
        //Fire1 == Left mouse button
        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
            muzzleFlash.Play();
        }
    }

    public void Shoot()
    {
        RaycastHit hit;
        if(Physics.Raycast(camera.transform.position, camera.transform.forward, out hit, range))
        {
            Debug.Log(hit.transform.name);

            HitEnemy hitEnemy = hit.transform.GetComponent<HitEnemy>();
            if (hitEnemy != null)
            {
                hitEnemy.TakeDamage(damage);
            }

            /*if (hit.rigidbody != null)
            {
                Debug.Log("bbbbbbbbbbb");
                hit.rigidbody.AddForce(-hit.normal*impactForce);
            }*/
        }
    }
}
