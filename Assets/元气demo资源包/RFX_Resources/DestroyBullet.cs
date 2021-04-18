using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyBullet : MonoBehaviour
{
    Vector3 initpos;
    float desTime = 3;//销毁时间

    private void Start()
    {
        Destroy(this.gameObject,desTime);//对子弹进行销毁
        initpos = this.transform.position;//记录子弹初始
    }
}
