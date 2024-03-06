using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Transform respawnPos;
    public GameObject bulletPrefab;
    public Transform firePos;
    public float speed = 5;
    public int life = 5;
    readonly public int  maxLife = 5;
    public float NpcDistance = 1.5f;
    Animator playerAC;
    Rigidbody2D player;
    float moveX,moveY;
    Vector2 lookDirection = Vector2.down;
    private void Awake() 
    {
        playerAC = GetComponent<Animator>();
        player = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        moveX = Input.GetAxis("Horizontal");
        moveY = Input.GetAxis("Vertical");
        
        if(!Mathf.Approximately(moveX,0.0f) || !Mathf.Approximately(moveY,0.0f))
        {
            lookDirection.Set(moveX,moveY);
            lookDirection.Normalize();
        }

        SetAnimationState();

        //鼠标左键发射子弹
        if(Input.GetMouseButtonUp(0))
        {
            var inputPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 dir = inputPos - firePos.position;
            FireABullet(dir.normalized);
        }

        //与Npc对话
        if(Input.GetKeyDown(KeyCode.X))
        {
            // RaycastHit2D hit = Physics2D.Raycast(player.position + Vector2.up * 0.2f,lookDirection,2f/*射线的长度*/,LayerMask.GetMask("NPC"));
            // if(hit)
            // {
            //     var jambi = hit.collider.GetComponent<JambiController>();
            //     if(jambi)
            //     {
            //         jambi.ShowDialogue();
            //     }
            // }

            var hit = Physics2D.OverlapCircle(player.position + Vector2.up * 0.2f,NpcDistance,LayerMask.GetMask("NPC"));
 
            if(hit)
            {
                var jambi = hit.GetComponent<JambiController>();
                if(jambi)
                {
                    jambi.ShowDialogue();
                }
            }
        }

    }

    void FixedUpdate()
    {
        Walk();
    }

    void SetAnimationState()
    {
        playerAC.SetFloat("DirX",lookDirection.x);
        playerAC.SetFloat("DirY",lookDirection.y);

        Vector2 movement = new Vector2(moveX,moveY);
        playerAC.SetFloat("Speed",movement.magnitude);
    }

    void Walk()
    {
        Vector2 movement = new Vector2(moveX,moveY);
        Vector2 pos = player.position;
        pos += movement*speed*Time.deltaTime;

        player.MovePosition(pos);
    }

    public void ChangeLife(int count)
    {
        life = Mathf.Clamp(life + count,0,maxLife);
        Debug.Log("ChangeLife :life is "+ life);

        if(life <= 0)
            Respawn();
    }

    void Respawn()
    {
        life = 5;
        player.position = respawnPos.position;
    }

    void FireABullet(Vector2 bulletDirection)
    {
        //player人物腰部的位置发射
        var startPos = firePos.position;

        //避免子弹生成时和Player碰撞
        if(bulletDirection.y < 0)
        {
            //player下方偏移一点点的位置
            var localPos = new Vector2(firePos.localPosition.x,-0.1f);
            startPos = transform.TransformPoint(localPos);
        }

        var bullet = Instantiate(bulletPrefab, startPos ,Quaternion.identity);
        bullet.SetActive(true);
        bullet.GetComponent<Bullet>().Init(bulletDirection);
    }

    private void OnDrawGizmosSelected() {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position,NpcDistance);
    }

    public float GetHealthPercent()
    {
        return (float)life/maxLife;
    }
}
