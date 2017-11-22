using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCharacter : MonoBehaviour
{
    private GameObject txHP;
    public GameObject TxHP
    {
        get
        {return txHP;}
        set
        {txHP = value;}
    }

    private GameObject txName;
    public GameObject TxName
    {
        get
        {  return txName;}
        set
        { txName = value;}
    }

    private int _hp = 100;
    public int Hp
    {
        get
        {return _hp;}
        set
        { _hp = value;}
    }

    public string Name
    {
        get
        {return _name;}
        set
        { _name = value;}
    }

    private string _name;

    private TextMesh txNameText;
    private TextMesh txHPText;

    private Transform mCamera;

    private MainManager mainManager;

    private Transform mCharacter;
    private bool isDestroy = false;

    private Rigidbody pRigbody;
    private Rigidbody eRigbody;
    private Vector3 toMCharacter;
    public float max = 20;

    private GlobalSingleton globalSigton;

    // Use this for initialization
    void Start()
    {
        mCamera = GameObject.Find("Camera").transform;

        mainManager = GameObject.Find("MainManager").GetComponent<MainManager>();

        globalSigton = GlobalSingleton.GetInstance();

        mCharacter = GameObject.Find("Player").transform;


        txNameText = TxName.GetComponent<TextMesh>();
        txHPText = TxHP.GetComponent<TextMesh>();

        eRigbody = this.GetComponent<Rigidbody>();
        pRigbody = mCharacter.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        UpdataProperties();

        if (isDestroy)
        {
            Destroy(txName);
            Destroy(txHP);
            Destroy(gameObject);
        }
    }

    private void FixedUpdate()
    {

        if (mainManager.pauseOrOver)
        {
            eRigbody.velocity = Vector3.zero;
            return;
        }
        if (globalSigton.difficulty == GlobalSingleton.Difficulty.Easy)
            AiEasy();
        else if (globalSigton.difficulty == GlobalSingleton.Difficulty.Hard)
            AiHard_2();
    }

    public void Destroy()
    {
        isDestroy = true;
    }

    public void Move(Vector3 pos, Vector3 rot, Vector3 velocity)
    {
        if (pos != transform.position)
        {
            transform.position = pos;
        }
        transform.eulerAngles = rot;
    }



    // 更新角色变量/属性
    private void UpdataProperties()
    {

        // 显示血量和ID
        txNameText.text = "Name:" + Name;
        txHPText.text = "HP:" + Hp.ToString();

        // 血量和ID的方向，面向着本机玩家UIA
        TxName.transform.rotation = mCamera.rotation;
        TxHP.transform.rotation = mCamera.rotation;

        txName.transform.position = new Vector3(transform.position.x,txName.transform.position.y,transform.position.z);
        txHP.transform.position = new Vector3(transform.position.x, txHP.transform.position.y, transform.position.z);
    }

    private void AiEasy()
    {
        toMCharacter = mCharacter.transform.position - this.transform.position;
        eRigbody.AddForce(toMCharacter.normalized*20, ForceMode.Force);
        max = 20;
        LimitVelocity(max);
    }

    private void AiHard()
    {
        toMCharacter = mCharacter.transform.position - this.transform.position;
        eRigbody.AddForce(toMCharacter.normalized * 50, ForceMode.Force);
        max = 25;
        LimitVelocity(max);
    }

    private void AiHard_2()
    {
        toMCharacter = mCharacter.transform.position - this.transform.position;
        if(toMCharacter.magnitude<=6 && eRigbody.velocity.magnitude<pRigbody.velocity.magnitude)
        {
            eRigbody.AddForce((Vector3.Angle(transform.position, Vector3.Cross(toMCharacter, Vector3.up))<=90 ? -1 : 1) *Vector3.Cross(toMCharacter,Vector3.up) * 20, ForceMode.Force);
            max = 25;
        }
        else
        {
            toMCharacter = mCharacter.transform.position - this.transform.position;
            eRigbody.AddForce(toMCharacter.normalized * 25, ForceMode.Force);
            max = 20;
        }
        eRigbody.velocity = Vector3.ClampMagnitude(eRigbody.velocity, max);
        //LimitVelocity(max);
    }

    private void LimitVelocity(float max)
    {
        if (eRigbody.velocity.magnitude >= max)
        {
            eRigbody.velocity = eRigbody.velocity.normalized * max;
        }
    }
}
