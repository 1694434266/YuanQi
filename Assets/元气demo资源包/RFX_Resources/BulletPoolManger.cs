using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPoolManger : MonoBehaviour
{
    public GameObject bullet_prefab;//子弹预制体
    Queue<GameObject> bulletPool;//存子弹的容器 作对象池
    int poolInitLength = 20;//预设子弹数量
    public static BulletPoolManger intance;
    private void Awake()
    {
        intance = this;
    }



    // Start is called before the first frame update
    void Start()
    {
        bulletPool = new Queue<GameObject>();
        GameObject bullet = null;//子弹变量
        for (int i = 0; i < poolInitLength; i++)//通过循环不断补充子弹
        {
            bullet = Instantiate(bullet_prefab);//初始化
            bullet.SetActive(false);//是初始化的子弹不被激活
            bulletPool.Enqueue(bullet);//是子弹进入对象池
        
        }
    }
    public GameObject getBullet()//获取子弹方法
    {
        if (bulletPool.Count > 0)//判断是否有子弹
        {
            GameObject bullet = bulletPool.Dequeue();//从对象池中取出子弹
            bullet.SetActive(true);//激活子弹
            return bullet;//返回
        }
        else
        {
            return Instantiate(bullet_prefab);//子弹不够用 需要重新生成
        }

    
    }
    public void recoverBullet(GameObject bullet)//回收子弹
    {
        bullet.SetActive(false);
        bulletPool.Enqueue(bullet);


    }



    // Update is called once per frame
    void Update()
    {
        
    }
}
