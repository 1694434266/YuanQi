using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bossyidong : MonoBehaviour
{
    public Animator bossani;
    GameObject go;
    public float speed;
    public float look = 100;
    // Start is called before the first frame update
   
    
  
    

    // Update is called once per frame
    void Update()
    {

        go = GameObject.FindGameObjectWithTag("Player");
        Vector3 vec = go.transform.position - this.transform.position;
        float length = vec.sqrMagnitude;
        vec.y = 0;
        if (length < look)
        {
            Move();
            transform.rotation = Quaternion.Slerp(this.transform.rotation, Quaternion.LookRotation(vec), 0.3f);
            transform.position = transform.position + this.transform.forward * speed * Time.deltaTime;

        }
        else
        {
            bossani.SetBool("移动", false);


        }
    }
    void Move()
    {
        bossani.SetBool("移动", true);
    }


}
