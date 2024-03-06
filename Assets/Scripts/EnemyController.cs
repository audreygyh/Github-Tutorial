using System;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public bool isHorizontal;
    public float speed = 3f;
    public int life = 3;
    public GameObject  hurtEffect;
    Vector2 lookDirection;
    Vector2 movement;
    float stayTime;
    readonly float changeDirectionInterval_c = 3f;
    Rigidbody2D enemy;
    Animator enemyAC;

    bool isFixed;

    void Awake()
    {
        enemy = GetComponent<Rigidbody2D>();
        enemyAC = GetComponent<Animator>();
    }

    void Start()
    {
        //初始化方向
        lookDirection = isHorizontal ? Vector2.right : Vector2.down;
        hurtEffect.SetActive(true);
        isFixed = false;
    }

    void Update()
    {
        if(!isFixed)
        {
            OppositeDirection();
            SetAnimationState();
        }

    }

    void FixedUpdate()
    {
        movement = speed * lookDirection * Time.deltaTime;
        Vector2 enemyPos = enemy.position;
        enemyPos += movement;
        enemy.MovePosition(enemyPos);
    }

    void OppositeDirection()
    {
        stayTime += Time.deltaTime;
        if(stayTime>=changeDirectionInterval_c)
        {
            lookDirection *= -1;
            stayTime = 0;
        }
    }

    void SetAnimationState()
    {
        enemyAC.SetFloat("DirX",lookDirection.x);
        enemyAC.SetFloat("DirY",lookDirection.y);

        enemyAC.SetFloat("Speed",movement.magnitude);
    }

    private void OnCollisionEnter2D(Collision2D other) {
        //修复时不进行碰撞检测
        if(isFixed)
            return;

        if(other.transform.CompareTag("Player"))
        {
            var playerController = other.transform.GetComponent<PlayerController>();
            if(playerController != null)
                playerController.ChangeLife(-1);
        }
    }

    public void BeAttack(int count)
    {
        life  = Mathf.Max(life - count,0);

        //播放被攻击粒子效果
        hurtEffect.GetComponent<ParticleSystem>().Play();

        if(life <= 0)
            Die();
        else
        {
            //敌人修复
            Fixed();
        }
    }

    private void Die()
    {
        //TODO:播放死亡动画
        Destroy(gameObject);
    }


    void Fixed()
    {
        isFixed = true;
        enemyAC.SetTrigger("Fixed");
        enemy.simulated = false;
    }

    //Animation Event
    void FinishFixed()
    {
        isFixed = false;
        enemy.simulated = true;
    }
}
