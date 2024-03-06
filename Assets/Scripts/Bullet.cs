using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float moveDistance = 5;
    public float Speed = 5f;    //匀速运动
    public float force = 5f;    //加速运动
    Vector2 lookDirection;
    int ATK = 1;

    Vector2 startPos;
    Vector2 totalMovement = Vector2.zero;

    Rigidbody2D bullet;

    public void Init(Vector2 lookDirecition)
    {
        this.lookDirection = lookDirecition;
        startPos = bullet.position;
        bullet.AddForce(lookDirecition * force);
    }

    void Awake()
    {
        bullet = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        //Fly();

        totalMovement = bullet.position - startPos;
        if(totalMovement.magnitude >= moveDistance)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.CompareTag("Enemy"))
        {
            other.gameObject.GetComponent<EnemyController>().BeAttack(ATK);
            Destroy(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Fly()
    {
        var movement = Speed*lookDirection*Time.deltaTime;
        totalMovement += movement;

        var bulletPos = bullet.position;
        bulletPos += movement;

        bullet.MovePosition(bulletPos);
    }
}
