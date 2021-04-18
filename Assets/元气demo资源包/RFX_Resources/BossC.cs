using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class BossC : MonoBehaviour
{
    float actionTime = 4;//行动时间
    public Animator ani;
    public Transform[] fireInitPos;//定义开火位置
    int hp = 100;
    public SkinnedMeshRenderer render;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        actionTime -= Time.deltaTime;//每一帧减少一下这一针经过的时间
        if(actionTime<0)//行动时间小于等于0时重置时间
        {
            atk();//调用攻击方法
            actionTime = 4;
        }
    }
   
    void atk()//攻击方法发射弹幕
    {
        ani.SetTrigger("atk_trigger");//使boss获得攻击动画
        StartCoroutine(fire());
    }
    IEnumerator fire()//协程
    {
        GameObject bullet = null;
        for (int j = 0; j < 4; j++)//
        {
            yield return new WaitForSeconds(0.2f);
            for (int i = 0; i < fireInitPos.Length; i++)//循环
            {
                bullet = BulletPoolManger.intance.getBullet();//从对象池获得子弹实例
                yield return new WaitForSeconds(0.05f);//是每次发射的子弹有0.05s的延迟
                bullet.transform.position = fireInitPos[i].position;//将子弹位置改为发射口的位置
                bullet.GetComponent<Rigidbody>().AddForce(fireInitPos[i].transform.forward * 100);//给子弹一个向前的力  朝发射口的方向
            }

        }
       
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("playerBullet"))

        {
            beHit();
        }
    }
    void beHit() //被攻击方法
    {
        hp -= 1;
        StartCoroutine(changecolor());
        if(hp<=0)
        {
            Destroy(this.gameObject);
        }
    }
  
    
    IEnumerator changecolor()
    {
        render.material.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        render.material.color = Color.white;
    }
}
