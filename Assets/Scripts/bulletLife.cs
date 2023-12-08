using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletLife : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Lifespan());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator Lifespan()
    {
        yield return new WaitForSeconds(3.0f);
        Destroy(this.gameObject);
    }
}
