using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public float thishp;
    public float thisspeed;
    public bool DefenseDebuff;
    public bool SlowDebuff;
    private bool debuffInvoke1;
    private bool debuffInvoke2;
    public int id;
    private static int Monsterid=0;
    public int Mid;
    public float currentTime;
    public float lastTime;
    public float lastTime2;
    public float lastTime3;
    private SpriteRenderer[] meshRenderer;
    public bool buffInvoke;
    private Color coloralpha;
    public bool Stealthbuff;
    public bool decting;
    private Rigidbody2D enemymove;
    public LayerMask whatisDecting;
    private List<Vector3> move = new List<Vector3>();
    private Vector3 vector3;
    private List<float> distance = new List<float>();
    private List<bool> invite = new List<bool>();
    private int b;
    public Slider slider;
    private int count;
    public float mindistance = 100000;
    public Vector3 vector;
    public SpriteRenderer debuffIcon;
    public Animator animator;
    public bool die;
    // Start is called before the first frame update

    private void Start()
    {
        Monsterid++;
        Mid = Monsterid;
                animator = GetComponent<Animator>();
        count = 0;
        foreach (Vector3Int pos in EnemyParents.getenemyinstance().enemyStreet.cellBounds.allPositionsWithin)
        {
            float posX = pos.x+0.5f;
            float posY = pos.y;
            vector3 = new Vector3(posX, posY, 0);
            TileBase tile = EnemyParents.getenemyinstance().enemyStreet.GetTile(pos);
            if (tile != null)
            {
                move.Add(vector3);
                invite.Add(false);
            }
        }
        StartCoroutine("StartMove");
      //  gameObject.SetActive(true);
        meshRenderer = gameObject.GetComponentsInChildren<SpriteRenderer>();

        slider = GetComponentInChildren<Slider>();

        DefenseDebuff = false;
        SlowDebuff = false;
        debuffInvoke1 = false;
        debuffInvoke2 = false;
        buffInvoke = false;
        Stealthbuff = false;
        lastTime = 0f;
        lastTime2 = 0f;
        lastTime3 = 0f;
        for (int i = 0; i < meshRenderer.Length; i++)
        {
            coloralpha = meshRenderer[i].color;
        }
        decting = false;
        thishp = (int)((EnemyParents.getenemyinstance().hp[id] *(1+(UIManager.getuiinstance().wave/20f))));

        slider.minValue = 0;
        slider.maxValue = thishp;
        thisspeed = EnemyParents.getenemyinstance().speed[id];
    }
    // Update is called once per frame
    void Update()
    {
        
        currentTime = Time.time;
        slider.value = thishp;
        if (buffInvoke == false && id == 5)
        {
            lastTime3 = currentTime;
            StartCoroutine("Stealth");
            buffInvoke = true;
        }
      if(Physics2D.OverlapCircleAll(transform.position, Tower.gettowerinstance().Range[5] / 2, whatisDecting).Length == 0)
        {
            decting = false;
        }
      if (gameObject.layer == LayerMask.NameToLayer("enemy")&&id==5)
        {
            coloralpha.a = 1f;
            for (int i = 0; i < meshRenderer.Length; i++)
            {
                meshRenderer[i].material.color = coloralpha;
            }
        }
        else if(gameObject.layer == LayerMask.NameToLayer("Stealth")&&id==5)
        {
            coloralpha.a = 0.3f;
            for (int i = 0; i < meshRenderer.Length; i++)
            {
                meshRenderer[i].material.color = coloralpha;
            }
        }
        if (DefenseDebuff == true && debuffInvoke1 == false)
        {
            lastTime2 = currentTime;
            StartCoroutine("DefenseDebuffTime");
            DefenseDebuff = false;
            debuffInvoke1 = true;
        }
        if (SlowDebuff == true && debuffInvoke2 == false)
        {
            lastTime = currentTime;
            StartCoroutine("SlowDebuffTime");
            SlowDebuff = false;
            debuffInvoke2 = true;

        }
        if (thishp <= 0)
        {
            if (die == false) 
            {
                animator.Play("die");
                Destroy(gameObject, animator.GetCurrentAnimatorStateInfo(0).length);
                UIManager.getuiinstance().EnemyCount -= 1;
                UIManager.getuiinstance().gold += EnemyParents.getenemyinstance().dropGold[id];
                die = true;
            }
            thishp = 0;
            thisspeed = 0;
            return;
        }

    }
    IEnumerator DefenseDebuffTime()
    {
            yield return new WaitForSeconds(4f);
        debuffInvoke1 = false;
        lastTime2 = currentTime;
    }
    IEnumerator SlowDebuffTime()
    {
            thisspeed = thisspeed * 0.5f;
        yield return new WaitForSeconds(4f);
        thisspeed = thisspeed * 2f;
        lastTime = currentTime;
        debuffInvoke2 = false;
    }
  public IEnumerator Stealth()
    {
        while(currentTime - lastTime3 < 4f)
        {
            if(decting == true&&id ==5)
            {
                gameObject.layer = LayerMask.NameToLayer("enemy");
            }
            else if(decting == false && id ==5)
            {
                gameObject.layer = LayerMask.NameToLayer("Stealth");
            }
            yield return null;
        }
        lastTime3 = currentTime;
        gameObject.layer = LayerMask.NameToLayer("enemy");
    }
   IEnumerator StartMove()
    {
        while (true)
        {
            if (count >= move.Count)
            {
                UIManager.getuiinstance().life -= 1;
                Destroy(gameObject);
                break;
            }

            for (int i = 0; i < move.Count; i++)
            {
                float a = (move[i] - transform.position).magnitude;
                distance.Add(a);
            }
            for (int j = 0; j < move.Count; j++)
            {
                if (invite[j] == true)
                {
                    continue;
              
                }
                if (mindistance > distance[j])
                {
                    mindistance = distance[j];
                    b = j;
                }
            }
            enemymove = gameObject.GetComponent<Rigidbody2D>();
            while (invite[b] == false)
                {
                    vector = (move[b] - transform.position).normalized;
                enemymove.velocity = vector * thisspeed;
                if (count >= 10 && count <= 13)
                {
                    transform.rotation = Quaternion.Euler(0, 0, 0);
                }
                else
                {
                    transform.rotation = Quaternion.Euler(0, 180, 0);
                }
                if (new Vector3(Mathf.Round(transform.position.x * 10f) * 0.1f, Mathf.Round(transform.position.y * 10f) * 0.1f, 0)== move[b])
                    {
                        invite[b] = true;
                    }
                    yield return null;
                }
            distance.Clear();
            count++;
      
           mindistance = 100000;
        }
    }
}
