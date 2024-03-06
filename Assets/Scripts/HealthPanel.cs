using UnityEngine;
using UnityEngine.UI;

public class HealthPanel : MonoBehaviour
{
    public Image healthProgress;
    PlayerController playerController;

    private void Awake() {
        var player = GameObject.FindWithTag("Player");
        if(player)
        {
            playerController = player.GetComponent<PlayerController>();
        }
    }
    private void Update() {
        if(playerController)
        {
            var healthPercent = playerController.GetHealthPercent();
            healthProgress.fillAmount = healthPercent;
        }
    }
}
