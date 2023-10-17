using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    static UIManager uiManager;
    public Button[] BuyButton = new Button[6];
    public TextMeshProUGUI[] BuyText = new TextMeshProUGUI[6];
    public TextMeshProUGUI GoldText;
    public TextMeshProUGUI LifeText;
    public TextMeshProUGUI waveText;
    public TextMeshProUGUI statText;
    public GameObject WinLosePanel;
    public TextMeshProUGUI WinLoseText;
    public Button StageButton;
    public int wave;
    public int count;
    public int life;
    private int id;
    public int gold;
    public GameObject UpgradePanel;
    public TextMeshProUGUI cellText;
    public TextMeshProUGUI upgradeText;
    public Button buttonUpgrade;
    public Button CellButton;
    public Vector3 mouse;
    public Tower1 tower1;
    private RaycastHit2D[] hit;
    public int EnemyCount;
    public TextMeshProUGUI countTowerText;
    public GameObject Buypanel;
    public TextMeshProUGUI BuyTowerText;
    private int buyid;
    public static UIManager getuiinstance() { return uiManager; }
    void Start()
    {
        EnemyCount = 0;
        wave = 0;
        life = 20;
        gold = 10000;
        uiManager = this;
        tower1 = null;
    }
    public void StageClick()
    {
        wave++;
        StartCoroutine("EnemySpawner");
        StageButton.interactable = false;
    }
    void Update()
    {
        countTowerText.text = "타워 설치갯수 " + Tower.gettowerinstance().CountTower + "/" + Tower.gettowerinstance().MaxCountTower;
        GoldText.text = "골드 " + gold;
        waveText.text = "스테이지 " + (wave);
        LifeText.text = "생명" + life;
        mouse = Input.mousePosition;

        if (Input.GetKeyDown(KeyCode.J))
        {
            wave = 40;
        }

        if (Input.GetMouseButtonDown(0))
        {
            Vector2 m = Camera.main.ScreenToWorldPoint(mouse);
            hit = Physics2D.RaycastAll(m, Vector2.zero, 0f);
            for (int i = 0; i < hit.Length; i++)
            {
                if (hit[i].collider.gameObject.tag == "tower" && Tower.gettowerinstance().imageTower2 == null)
                {
                    UpgradePanel.SetActive(true);
                    tower1 = hit[i].collider.GetComponent<Tower1>();
                    id = tower1.id;
                    cellText.text = "판매가:" + (Tower.gettowerinstance().needGold[id] * 0.5f);
                    upgradeText.text = "강화 필요 골드:" + tower1.upgradegold;
                    statText.text = "공격력" + tower1.attack + "\n공격속도(초당)" + 1 / tower1.attackspeed + "\n사거리" + Tower.gettowerinstance().Range[id] + "\n레벨" + tower1.level + "/10";

                    if (tower1.id == 3)
                    {
                        statText.text = statText.text + "\n공격시 적의 이동속도를 4초간 50%감소";
                    }
                    else if (tower1.id == 0)
                    {
                        statText.text = statText.text + "\n공격시 적이 받는 피해량을 4초간 20% 증가";
                    }
                    break;
                }
            }
        }
        if (tower1 != null)
        {
            if (gold >= tower1.upgradegold&&tower1.level<10)
            {
                buttonUpgrade.interactable = true;
            }
            else
            {
                buttonUpgrade.interactable = false;
            }
        }
     

        if (wave == 40&&EnemyCount==0)
        {
            WinLosePanel.SetActive(true);
            WinLoseText.text = "win Press button";
        }
        if(life == 0)
        {
            WinLosePanel.SetActive(true);
            WinLoseText.text = "Lose Press button";
        }

        for (int i = 0; i < BuyText.Length; i++)
        {
            BuyText[i].text = "구매비용: " + Tower.gettowerinstance().needGold[i];
            if(i == 5)
            {
                BuyText[i].text = BuyText[i].text + "\n" + "은신탐지";
            }
        }
     
  
   
        for (int i = 0; i < BuyButton.Length; i++)
        {
            if (Tower.gettowerinstance().needGold[i] > gold||Tower.gettowerinstance().CountTower>=Tower.gettowerinstance().MaxCountTower|| WinLosePanel.activeSelf == true||Tower.gettowerinstance().imageTower2!=null)
            {
                BuyButton[i].interactable = false;
            }
            else
            {
                BuyButton[i].interactable= true;
            }
        }
    }
    public void UpgradeBack()
    {
        UpgradePanel.SetActive(false);
    }
    public void UpgradeButton()
    {
        if (tower1.upgradegold <= gold)
        {
            gold -= (int)tower1.upgradegold;
            tower1.attack = (int)tower1.attack * 1.2f;
            tower1.upgradegold = tower1.upgradegold * 2f;
            tower1.level = tower1.level += 1;
        }
        cellText.text = "판매가:" + (Tower.gettowerinstance().needGold[id] * 0.5f);
        upgradeText.text = "강화 필요 골드:" + tower1.upgradegold;
        statText.text = "공격력" + tower1.attack + "\n공격속도(초당)" + 1 / tower1.attackspeed + "\n사거리" + Tower.gettowerinstance().Range[id] + "\n레벨" + tower1.level + "/10";
        if (tower1.id == 3)
        {
            statText.text = statText.text + "\n공격시 적의 이동속도를 4초간 20%감소";
        }
        else if (tower1.id == 0)
        {
            statText.text = statText.text + "\n공격시 적이 받는 피해량을 4초간 20% 증가";
        }
    }
    public void cellButton()
    {
        gold += (int)(Tower.gettowerinstance().needGold[id] * 0.5f);
        tower1.attack = Tower.gettowerinstance().TowerAttack[id];
        tower1.upgradegold = Tower.gettowerinstance().UpgradeGold[id];
        tower1.level = Tower.gettowerinstance().Level[id];
        Destroy(tower1.gameObject);
        UpgradePanel.SetActive(false);
        Tower.gettowerinstance().CountTower--;
    }
    IEnumerator EnemySpawner()
    {
        while (count<=wave*2)
        {
            int id = Random.Range(0, EnemyParents.getenemyinstance().enemySpawn.Count);
          GameObject gameobject = Instantiate(EnemyParents.getenemyinstance().enemySpawn[id], new Vector3(-9f, 3f, 0),Quaternion.Euler(0,180,0));
           Enemy enemy = gameObject.GetComponent<Enemy>();

            yield return new WaitForSeconds(0.4f);
            count++;
            EnemyCount++;
        }
        count = 0;
        StageButton.interactable = true;
    }
    public void TowerClick(int a)
    {
        buyid = a;
        BuyTowerText.text ="공격력" + Tower.gettowerinstance().TowerAttack[buyid] + "\n공격속도(초당)" + 1 / Tower.gettowerinstance().TowerAttackSpeed[buyid] + "\n사거리" + Tower.gettowerinstance().Range[buyid];
        if (Tower.gettowerinstance().TowerNumber[buyid] == 3)
        {
            BuyTowerText.text = BuyTowerText.text + "\n공격시 적의 이동속도를 4초간 20%감소";
        }
        else if (Tower.gettowerinstance().TowerNumber[buyid] == 0)
        {
            BuyTowerText.text = BuyTowerText.text + "\n공격시 적이 받는 피해량을 4초간 20% 증가";
        }
    Buypanel.SetActive(true);
    }
    public void TowerBack()
    {
        Buypanel.SetActive(false);
    }
    public void TowerBuy()
    {
        if (Tower.gettowerinstance().needGold[buyid] <= gold)
        {
            gold -= Tower.gettowerinstance().needGold[buyid];
            Tower.gettowerinstance().Num = buyid;
            Tower.gettowerinstance().imageTower2 = Instantiate(Tower.gettowerinstance().imageTower[buyid], Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10)), Quaternion.identity);
            Tower.gettowerinstance().RangeImage = Instantiate(Tower.gettowerinstance().rangeTower, Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10)), Quaternion.identity);
        }
        Buypanel.SetActive(false);
    }
    public void SceneClick()
    {
        SceneManager.LoadScene("GameScene");
    }
    public void TakeDamage(Enemy enemy, Tower1 tower1)
    {
        if (enemy.DefenseDebuff == true)
        {
            enemy.thishp -= tower1.attack * 1.2f;
        }
        else
        {
            enemy.thishp -= tower1.attack;
        }
    }
}
