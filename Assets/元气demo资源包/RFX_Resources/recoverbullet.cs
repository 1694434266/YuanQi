using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class recoverbullet : MonoBehaviour
{
    float activeTime = 0;
    // Start is called before the first frame update
    private void OnEnable()
    {
        activeTime = 3;
    }
    void Start()
    {
       

    }

    // Update is called once per frame
    void Update()
    {
        activeTime -= Time.deltaTime;
        if(activeTime<0)
        {
            BulletPoolManger.intance.recoverBullet(this.gameObject);

        }
    }
    private void OnDisable()
    {
        this.GetComponent<Rigidbody>().velocity = Vector3.zero;
    }
}
