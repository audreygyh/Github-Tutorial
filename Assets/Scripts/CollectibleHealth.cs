using UnityEngine;

public class CollectibleHealth : MonoBehaviour
{
    //2D的碰撞器一定要带2D的方法
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            Destroy(gameObject);
            var playerController = other.GetComponent<PlayerController>();

            if(playerController != null)
            {
                playerController.ChangeLife(1);
            }
        }
    }
}
