using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class MainManager : MonoBehaviour
{

    [SerializeField]
    private GameObject settleImage;

    [SerializeField]
    private Text settleText;

    [SerializeField]
    private GameObject player;

    [SerializeField]
    private GameObject pauseObject;

    [SerializeField]
    private GameObject mEnemyCharacter;

    public bool pauseOrOver;

    private Player host;
    private EnemyCharacter enemyPlayer;
    private GlobalSingleton globalSington;
    private Hashtable _htEnemies = new Hashtable();



    // Use this for initialization
    void Start()
    {
        pauseOrOver = false;
        globalSington = GlobalSingleton.GetInstance();
        if (globalSington.mode == GlobalSingleton.Mode.Alone)
        {
            enemyPlayer = AddEnemyCharacter("初号球");
        }

        host = player.GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        if (pauseOrOver)
            return;

        if (host.hp <= 0)
        {
            pauseOrOver = true;
            settleImage.SetActive(true);
            settleText.text = "Game Over";
        }

        else if (enemyPlayer.GetHp() <= 0)
        {
            RemoveEnemyCharacter("初号球");
            pauseOrOver = true;
            settleImage.SetActive(true);
            settleText.text = "You Win";
        }
    }


    private EnemyCharacter AddEnemyCharacter(string name)
    {
        GameObject p = GameObject.Instantiate(mEnemyCharacter);
        EnemyCharacter ec = p.GetComponent<EnemyCharacter>();

        // 修改ID
        ec.SetName(name);

        // 加入到哈希表
        _htEnemies.Add(name, ec);

        return ec;
    }

    // 删除客户的人物模组
    private void RemoveEnemyCharacter(string id)
    {
        EnemyCharacter ec = (EnemyCharacter)_htEnemies[id];
        ec.Destroy();
        _htEnemies.Remove(id);
    }

    // 删除所有客户的人物模组
    public void RemoveAllEnemyCharacter()
    {
        foreach (string id in _htEnemies.Keys)
        {
            EnemyCharacter ec = (EnemyCharacter)_htEnemies[id];
            ec.Destroy();
        }
        _htEnemies.Clear();
    }

    public void OnButtonReturn()
    {
        SceneManager.LoadScene("1-StarScene");
    }

    public void OnButtonAgain()
    {
        SceneManager.LoadScene("Main");
    }

    public void OnButtonPause()
    {
        pauseObject.SetActive(true);
        pauseOrOver = true;
    }

    public void OnButtonGoOn()
    {
        pauseOrOver = false;
        pauseObject.SetActive(false);
    }
}
