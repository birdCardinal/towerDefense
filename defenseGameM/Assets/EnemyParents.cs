using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class EnemyParents : MonoBehaviour
{
    public List<float> hp = new List<float> ();
    public List<float> speed = new List<float>();
    public List<int> dropGold = new List<int>();
    public Tilemap enemyStreet;
    static EnemyParents enemyInstance;
    public List<GameObject> enemySpawn = new List<GameObject>();
    public List<int> EnemyNumber = new List<int> ();

    public static EnemyParents getenemyinstance() { return enemyInstance; }
    // Start is called before the first frame update
    private void Awake()
    {
        hp.Add(120);
        hp.Add(156);
        hp.Add(480);
        hp.Add(324);
        hp.Add(385);
        hp.Add(420);

        speed.Add(2.5f);
        speed.Add(2.2f);
        speed.Add(0.8f);
        speed.Add(1.7f);
        speed.Add(1.4f);
        speed.Add(1.6f);

        dropGold.Add(100);
        dropGold.Add(150);
        dropGold.Add(200);
        dropGold.Add(250);
        dropGold.Add(300);
        dropGold.Add(400);

        EnemyNumber.Add(0);
        EnemyNumber.Add(1);
        EnemyNumber.Add(2);
        EnemyNumber.Add(3);
        EnemyNumber.Add(4);
        EnemyNumber.Add(5);
    }
    private void Start()
    {
        enemyInstance = this;
    }
    // Update is called once per frame
    void Update()
    {
      
    }

}
