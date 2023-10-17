using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Tower1 : MonoBehaviour
{
    private GameObject bullet1;
    private Rigidbody2D movebullet;
    private bool TowerSearch;
    public int id;
    public float attack;
    public float attackspeed;
    public float upgradegold;
    public float cellgold;
    public int level;
    private Vector3 RangeVector;
    public Enemy e1;
    public List<Collider2D> collider2s = new List<Collider2D>();
    public Collider2D[] colliders;
    public int Attackid;
    void Start()
    { 
        attack = Tower.gettowerinstance().TowerAttack[id];
        attackspeed = Tower.gettowerinstance().TowerAttackSpeed[id];
        upgradegold = Tower.gettowerinstance().UpgradeGold[id];
        level = Tower.gettowerinstance().Level[id];
        cellgold = upgradegold * 0.5f;
        TowerSearch = false;
    }

    void Update()
    {
        if (id == 5 && Tower.gettowerinstance().imageTower2 == null)
        {
          colliders = Physics2D.OverlapCircleAll(transform.position, Tower.gettowerinstance().Range[id] / 2, Tower.gettowerinstance().whatisenemy);
            if (colliders.Length != 0)
            {
                for (int i = 0; i < colliders.Length; i++)
                {
                    Enemy enemy = colliders[i].GetComponent<Enemy>();
                    enemy.decting = true;
                }
            }
        }
        if (Tower.gettowerinstance().imageTower2 == null && TowerSearch == false)
        {
            StartCoroutine("ShotBullet");
            TowerSearch = true;
        }
      
        if (bullet1 != null && movebullet != null && colliders[Attackid]!=null)
        {   
                RangeVector = (colliders[Attackid].transform.position - transform.position).normalized;
                movebullet.velocity = RangeVector * 30f;
        }
    }
    IEnumerator ShotBullet()
    {
        while (true)
        {
            colliders = Physics2D.OverlapCircleAll(transform.position, Tower.gettowerinstance().Range[id]/2, Tower.gettowerinstance().whatisenemy);
            for (int i = 0; i < colliders.Length; i++)
            {
                if (colliders[i].gameObject.layer == LayerMask.NameToLayer("enemy") && colliders[i].GetComponent<Enemy>().thishp>0)
                {
                    Attackid = i;
                    bullet1 = Instantiate(Tower.gettowerinstance().TowerBullet[Tower.gettowerinstance().TowerNumber[id]], transform.position, transform.rotation, transform.parent = this.transform);
                    movebullet = bullet1.GetComponent<Rigidbody2D>();
                    break;
                }
            }
                yield return new WaitForSeconds(Tower.gettowerinstance().TowerAttackSpeed[id]);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "street"||collision.tag == "tower")
        {
            Tower.gettowerinstance().notstreet = true;
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "street" || collision.tag == "tower")
        {
            Tower.gettowerinstance().notstreet = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "street")
        {
            Tower.gettowerinstance().notstreet = false;
        }
        if(collision.tag == "tower")
        {
            Tower.gettowerinstance().notstreet = false;
        }
    }
    public void Click()
    {
        UIManager.getuiinstance().UpgradePanel.SetActive(true);
    }
}
