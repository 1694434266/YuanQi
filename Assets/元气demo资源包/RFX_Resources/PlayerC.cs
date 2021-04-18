using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerC : MonoBehaviour
{


    float speed = 4;
    public Animator ani;
    float atkCd = 0;
    public GameObject bullet_prefab;//定义预制体变量
    RaycastHit hitInfo;//射线检测碰撞
    Ray ray;//射线功能
    public GameObject model;//定义主角模型
    int hp = 50;
    public SkinnedMeshRenderer render;
    void Start()
    {
    }

    void Update()
    {   //输入控制  输入和响应
        Debug.Log("x轴方向的操作值————" + Input.GetAxis("Horizontal"));
        Debug.Log("y轴方向的操作值————" + Input.GetAxis("Vertical"));
   
        atk();//调用攻击方法    

        move();//调用移动方法
    }
    void move()
    {
        Vector3 moveDir = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));//获得人物移动方向的向量
   
        this.transform.LookAt(this.transform.position + moveDir);//人物所看向的方向

        if (Mathf.Abs(moveDir.x) > 0 || Mathf.Abs(moveDir.z) > 0)//判断角色是否有速度 有速度即可移动
        {
            
            this.transform.Translate(this.transform.forward * speed * Time.deltaTime, Space.World);//使角色移动 以世界为参照物
            ani.SetBool("移动", true);//使角色获得移动动画
        }
        else
        {
            ani.SetBool("移动", false);
        }
    }




    void atk()
    {
        if (Input.GetMouseButton(0))//判断是否按下鼠标 通过鼠标进行攻击 0为右键
        {
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);//从屏幕上当前鼠标的位置点发射一条射线赋值给变量ray
            if (Physics.Raycast(ray, out hitInfo, 200))//调用碰撞检测 判断ray有没有碰撞到什么东西 通过out 拿到碰撞到的东西 给一个200的距离
            {
                Vector3 taget = hitInfo.point;//将碰撞位置接收
                taget.y = this.transform.position.y;//将位置y值与角色y值相同
                model.transform.LookAt(taget);//让模型看向碰撞位置点
            
            }
            atkCd -= Time.deltaTime;//控制攻击cd
            if(atkCd<=0)//当cd小于等于0时 才会攻击
            {
                ani.SetTrigger("射击");//获得射击动画
                atkCd = 0.2f;//重置cd值 
                GameObject bullet = Instantiate(bullet_prefab);//接收一个预制体参数，通过实例化方法返回一个游戏体 生成一个子弹
                bullet.transform.position = this.transform.position + model.transform.forward+ Vector3.up*0.9f;//改变子弹的位置 ：将子弹放在模型的前方向上0.9m
                bullet.GetComponent<Rigidbody>().AddForce(model.transform.forward*200);//发射子弹 通过给刚体一个力（有大小有方向）



            }
            
        }
        if(Input.GetMouseButtonUp(0))//当不开枪时，使model朝向归于0
        {
            model.transform.localEulerAngles = Vector3.zero;

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
        if (hp <= 0)
        {
            Destroy(this.gameObject);
        }
    }
    IEnumerator changecolor()
    {
        render.material.color = Color.blue;
        yield return new WaitForSeconds(0.1f);
        render.material.color = Color.white;
    }

}









