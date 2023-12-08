using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShoot : MonoBehaviour
{
    public float speed = 10;
    public Rigidbody bullet;
    bool shootDelay = false;

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (shootDelay == false)
            {
                Fire();
                shootDelay = true;
                StartCoroutine(ShootingDelay());
            }
        }
    }

    void Fire()
    {
        Rigidbody bulletClone = (Rigidbody)Instantiate(bullet, transform.position, transform.rotation);
        bulletClone.velocity = transform.forward * speed;
    }

    IEnumerator ShootingDelay()
    {
        yield return new WaitForSeconds(2.0f);
        shootDelay = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Destroy(this.gameObject);
        }
    }
}