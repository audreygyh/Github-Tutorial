using UnityEngine;

public class Trap : MonoBehaviour
{
    const float damageInterval_c = 3f;
    float stayTime = damageInterval_c;

    void Update()
    {
        stayTime += Time.deltaTime;
    }

    private void OnTriggerStay2D(Collider2D other) {
        if(other.tag == "Player" && CanDoDamage())
        {
            var playerController = other.GetComponent<PlayerController>();
            if(playerController != null)
            {
                playerController.ChangeLife(-1);

                //重置停留时间
                stayTime = 0;
            }
        }
    }

    bool CanDoDamage()
    {
        if(stayTime >= damageInterval_c)
        {
            return true;
        }

        return false;
    }
}
