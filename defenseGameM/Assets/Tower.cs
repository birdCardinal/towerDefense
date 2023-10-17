using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Tilemaps;

public class Tower : MonoBehaviour
{
    static Tower towerinstance;
    public static Tower gettowerinstance() { return towerinstance; }
    public List<float> TowerAttackSpeed = new List<float>();
    public List<float> TowerAttack = new List<float>();
    public List<int> needGold = new List<int>();
    public List<int> Range = new List<int>();
    public List<int> UpgradeGold = new List<int>();
    public List<int> Level = new List<int>();
    public int Num;
    public TilemapCollider2D streetTile;
    public GameObject[] imageTower = new GameObject[5];
    public GameObject[] TowerBullet = new GameObject[5];
    public GameObject imageTower2;
    public GameObject RangeImage;
    public GameObject rangeTower;
    public bool notstreet;
    public List<int> TowerNumber = new List<int>();
    public LayerMask whatisenemy;
    public LayerMask stealthEnemy;
    public bool SearchTower;
    public float attack;
    public int CountTower;
    public int MaxCountTower;
    private RaycastHit2D[] hit;
    public Vector3 mouse;
    // Start is called before the first frame update
    private void Awake()
    {
        CountTower = 0;
        MaxCountTower = 20;
        attack = 1f;
        SearchTower = false;
        TowerAttackSpeed.Add(0.8f);
        TowerAttackSpeed.Add(0.7f);
        TowerAttackSpeed.Add(4f);
        TowerAttackSpeed.Add(0.2f);
        TowerAttackSpeed.Add(1.1f);
        TowerAttackSpeed.Add(1.2f);

        TowerAttack.Add(15);
        TowerAttack.Add(45);
        TowerAttack.Add(300);
        TowerAttack.Add(7);
        TowerAttack.Add(90);
        TowerAttack.Add(85);

        needGold.Add(1000);
        needGold.Add(1500);
        needGold.Add(2000);
        needGold.Add(2500);
        needGold.Add(3000);
        needGold.Add(3500);

        TowerNumber.Add(0);
        TowerNumber.Add(1);
        TowerNumber.Add(2);
        TowerNumber.Add(3);
        TowerNumber.Add(4);
        TowerNumber.Add(5);

        Range.Add(7);
        Range.Add(9);
        Range.Add(8);
        Range.Add(12);
        Range.Add(6);
        Range.Add(7);

        UpgradeGold.Add(500);
        UpgradeGold.Add(900);
        UpgradeGold.Add(1300);
        UpgradeGold.Add(1700);
        UpgradeGold.Add(2100);
        UpgradeGold.Add(2500);
        for (int i = 0; i < TowerAttack.Count; i++)
        { 
        Level.Add(1);
        }

    }
    void Start()
    {
        towerinstance = this;
    }

    // Update is called once per frame
    void Update()
    {
    
        if (imageTower2 != null&&RangeImage != null)
        {
            imageTower2.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Input.mousePosition.z + 10));
            RangeImage.transform.localScale = new Vector3(Range[Num], Range[Num], 1);
            RangeImage.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Input.mousePosition.z + 10));
            if (Input.GetMouseButtonDown(0) && notstreet == false)
            {
                mouse = Input.mousePosition;
                Vector2 m = Camera.main.ScreenToWorldPoint(mouse);
                hit = Physics2D.RaycastAll(m, Vector2.zero, 0f);
                for (int i = 0; i < hit.Length; i++)
                {
                    if(hit[i].collider.tag == "stone")
                    {
                        imageTower2.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Input.mousePosition.z + 10));
                        RangeImage.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Input.mousePosition.z + 10));
                        Destroy(RangeImage);
                        imageTower2 = null;
                    }
                }
                //    SearchTower = true;
                CountTower++;
            }
        }
    }
 }
