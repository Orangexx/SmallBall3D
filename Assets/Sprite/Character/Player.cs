using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//using UnityEngine.Networking;

public class Player : MonoBehaviour
{
    [SerializeField] private Slider hpSlider;

    [SerializeField] private GameObject managerObject;

    [SerializeField] private AudioClip CollisionSound;

    [SerializeField] private GameObject enemyCharacter;

    public Transform playerCamera;

    private AudioSource collision_audio;

    private MainManager mainManager;

    public int hp;
    public float move_Rate;
    public float jump_Rate;
    public float career_Rate;
    public int collision_Rate;
    public float max;
    private Rigidbody rigdby;
    private float H;
    private float V;
    private bool iscareer;

    private bool career_Used;

    protected float distToGround;

    private void Awake()
    {
        iscareer = false;
        career_Used = true;

        mainManager = managerObject.GetComponent<MainManager>();
        collision_audio = gameObject.GetComponent<AudioSource>();
        rigdby = GetComponent<Rigidbody>();

        hpSlider.maxValue = 100;
        hpSlider.minValue = 0;
        jump_Rate = 10;
        move_Rate = 20;
        career_Rate = 70;
        collision_Rate = 2;
        distToGround = GetComponent<Collider>().bounds.extents.y;
    }

    // Use this for initialization
    void Start()
    {
        //globalsigton = GlobalSingleton.GetInstance();
        //if(globalsigton.mode == GlobalSingleton.Mode.Network)
        //mNetwork = transform.GetComponent<Network>();
        hp = 100;
        hpSlider.value = hp;
    }

    void FixedUpdate()
    {

        if (mainManager.pauseOrOver)
        {
            rigdby.velocity = Vector3.zero;
            return;
        }

        if(Input.GetKeyDown(KeyCode.Space)&&IsGrounded())
            Jump();

        if(Input.GetKeyDown(KeyCode.R))         //刹车
            Stop();

        if(Input.GetKeyDown(KeyCode.Q))         //加速
            Career();

        H = Input.GetAxis("Horizontal");
        V = Input.GetAxis("Vertical");
        rigdby.AddForce(new Vector3 ((playerCamera.rotation * new Vector3(H, 0, V)).x,0, (playerCamera.rotation * new Vector3(H, 0, V)).z).normalized * move_Rate, ForceMode.Force);
        if(!iscareer)  LimitVelocity(max);


        //if(Input.GetKeyDown(KeyCode.C))         //加敌人
        //{
        //    mainManager.AddEnemyCharacter("");

        //}
    }

    private void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.tag == "EnemyPlayer")
        {
            Rigidbody enemyRigbody = collision.gameObject.GetComponent<Rigidbody>();
            enemyRigbody.AddForce(rigdby.velocity - enemyRigbody.velocity , ForceMode.Impulse);
            rigdby.AddForce(enemyRigbody.velocity - rigdby.velocity , ForceMode.Impulse);

            if (enemyRigbody.velocity.magnitude >= rigdby.velocity.magnitude)
                this.hp -= ((int)enemyRigbody.velocity.magnitude - (int)rigdby.velocity.magnitude) *collision_Rate;
            else
                collision.gameObject.GetComponent<EnemyCharacter>().Hp -= ((int)rigdby.velocity.magnitude - (int)enemyRigbody.velocity.magnitude) * collision_Rate;

            Debug.Log(this.hp);
            collision_audio.PlayOneShot(CollisionSound);
        }

        hpSlider.value = hp;
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

    private void Jump()
    {
        rigdby.velocity = Vector3.up*jump_Rate;
    }

    private void Stop()
    {
        rigdby.velocity = Vector3.zero;
    }

    private void Career()
    {
        if(career_Used)
        {
            float velocity = rigdby.velocity.magnitude;
            rigdby.velocity = rigdby.velocity.normalized * career_Rate;
            iscareer = true;
            StartCoroutine(WaitAndReset(0.7f, velocity));
            career_Used = false;
            StartCoroutine(Cooldown_Time(4f, "career_Used"));
        }
    }

    IEnumerator WaitAndReset(float waitTime,float velocity)
    {
        yield return new WaitForSeconds(waitTime);
        rigdby.velocity = rigdby.velocity.normalized*velocity;
        iscareer = false;
    }

    public bool IsGrounded()
    {
        return Physics.Raycast(transform.position, Vector3.down, distToGround + 0.1f);
    }

    IEnumerator Cooldown_Time(float time,string canUsed)
    {
        yield return new WaitForSeconds(time);
        switch (canUsed)
        {
            case "career_Used":
                career_Used = true;
                break;

            default:
                break;
        }

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

//void Update()
//{
//    //if (mainManager.pauseOrOver)
//    //    return;



//    //if (globalsigton.mode == GlobalSingleton.Mode.Network)
//    //{
//    //    mNetwork.SendStatus(transform.position, transform.eulerAngles, rigdby.velocity,
//    //     hp);
//    //    ProcessPackage();
//    //}
//}
