using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletShot : MonoBehaviour
{
    public Enemy enemy;
    private Tower1 tower1;
    private int id;
    // Start is called before the first frame update
    private void OnTriggerEnter2D(Collider2D collision)
    {
        collision.TryGetComponent<Enemy>(out enemy);
        if (enemy != null)
        {
            if (enemy.Mid == tower1.colliders[tower1.Attackid].gameObject.GetComponent<Enemy>().Mid)
            {
                switch (tower1.id)
                {
                    case 0:
                        enemy.DefenseDebuff = true;
                        enemy.lastTime = enemy.currentTime;
                        break;
                    case 3:
                        enemy.SlowDebuff = true;
                        enemy.lastTime2 = enemy.currentTime;
                        break;
                    default:
                        break;
                }
                if (enemy.decting == false && enemy.id == 5)
                {
                    enemy.buffInvoke = false;
                }
                UIManager.getuiinstance().TakeDamage(enemy, tower1);
                Destroy(gameObject);
            }
        }
    }
    private void Awake()
    {
        Destroy(gameObject, 1f);
    }
    private void Update()
    {
        tower1 = GetComponentInParent<Tower1>();
    }
}
