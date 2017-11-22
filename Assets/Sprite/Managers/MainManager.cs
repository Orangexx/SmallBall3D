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

    [SerializeField]
    private GameObject mEnemyHp;

    [SerializeField]
    private GameObject mEnemyName;

    public bool pauseOrOver;

    private Player host;
    private EnemyCharacter enemyPlayer;
    private GlobalSingleton globalSington;
    private Hashtable _htEnemies = new Hashtable();

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

        else if (enemyPlayer.Hp <= 0)
        {
            RemoveEnemyCharacter("初号球");
            pauseOrOver = true;
            settleImage.SetActive(true);
            settleText.text = "You Win";
        }
    }

    private EnemyCharacter AddEnemyCharacter(string name)
    {
        GameObject p = Instantiate(mEnemyCharacter);
        EnemyCharacter ec = p.GetComponent<EnemyCharacter>();
        ec.TxHP = Instantiate(mEnemyHp);
        ec.TxName = Instantiate(mEnemyName);

        ec.Name = name;

        _htEnemies.Add(name, ec);

        return ec;
    }

    private void RemoveEnemyCharacter(string id)
    {
        EnemyCharacter ec = (EnemyCharacter)_htEnemies[id];
        ec.Destroy();
        _htEnemies.Remove(id);
    }

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
