using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//using UnityEngine.Networking;

public class Player : MonoBehaviour
{
    [SerializeField]
    private Slider hpSlider;

    [SerializeField]
    private GameObject managerObject;

    private MainManager mainManager;

    public int hp;
    public float the_Rate;
    private Rigidbody rigdby;
    private float H;
    private float V;
    public float max;

    // Use this for initialization
    void Start()
    {
        mainManager = managerObject.GetComponent<MainManager>();

        hpSlider.maxValue = 100;
        hpSlider.minValue = 0;
        //globalsigton = GlobalSingleton.GetInstance();


        //if(globalsigton.mode == GlobalSingleton.Mode.Network)
        //mNetwork = transform.GetComponent<Network>();

        rigdby = GetComponent<Rigidbody>();
        hp = 100;


    }

    void Update()
    {
        if (mainManager.pauseOrOver)
            return;


        hpSlider.value = hp;

        //if (globalsigton.mode == GlobalSingleton.Mode.Network)
        //{
        //    mNetwork.SendStatus(transform.position, transform.eulerAngles, rigdby.velocity,
        //     hp);
        //    ProcessPackage();
        //}
    }


    // Update is called once per frame
    void FixedUpdate()
    {

        if (mainManager.pauseOrOver)
            return;
        H = Input.GetAxis("Horizontal");
        V = Input.GetAxis("Vertical");
        rigdby.AddForce(new Vector3(H, 0, V) * the_Rate, ForceMode.Force);
        LimitVelocity(max);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "EnemyPlayer")
        {
            Rigidbody enemyRigbody = collision.gameObject.GetComponent<Rigidbody>();
            enemyRigbody.AddForce(rigdby.velocity - enemyRigbody.velocity , ForceMode.Impulse);
            rigdby.AddForce(enemyRigbody.velocity - rigdby.velocity , ForceMode.Impulse);

            if (enemyRigbody.velocity.magnitude >= rigdby.velocity.magnitude)
                this.hp -= ((int)enemyRigbody.velocity.magnitude - (int)rigdby.velocity.magnitude)*6;
            else
                collision.gameObject.GetComponent<EnemyCharacter>().SetHP(((int)rigdby.velocity.magnitude - (int)enemyRigbody.velocity.magnitude)*6);

            Debug.Log(this.hp);
            
        }

    }

    private void OnCollisionStay(Collision collision)
    {
        if(collision.gameObject.tag == "EnemyPlayer")
        {
            Rigidbody enemyRigdoby = collision.gameObject.GetComponent<Rigidbody>();
            enemyRigdoby.AddForce((collision.gameObject.transform.position - transform.position).normalized*2, ForceMode.Impulse);
            rigdby.AddForce((transform.position - collision.gameObject.transform.position).normalized*2, ForceMode.Impulse);
        }
    }

    private void LimitVelocity(float max)
    {
        if (rigdby.velocity.magnitude >= max)
        {
            rigdby.velocity = rigdby.velocity.normalized * max;
        }
    }

    //private void ProcessPackage()
    //{
    //    Network.Package p;

    //    // 获取数据包直到完毕
    //    while (mNetwork.NextPackage(out p))
    //    {
    //        // 确定不是本机，避免重复
    //        if (mNetwork._name == p.name)
    //        {
    //            return;
    //        }

    //        // 获取该客户相对应的人物模组
    //        if (!_htEnemies.Contains(p.name))
    //        {
    //            AddEnemyCharacter(p.name);
    //        }

    //        // 更新客户的人物模型状态
    //        EnemyCharacter ec = (EnemyCharacter)_htEnemies[p.name];

            

    //        // 血量
    //        ec.SetHP(p.hp);

    //        // 移动动作
    //        ec.Move(p.pos.V3, p.rot.V3, p.velocity.V3);
 
    //    }
    //}

   


    
}
