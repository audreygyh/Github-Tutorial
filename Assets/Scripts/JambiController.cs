using UnityEngine;

public class JambiController : MonoBehaviour
{
    public GameObject dialogue;

    public readonly float stayTimeSpan = 5f;//对话框停留的时间

    float stayTime;

    void Awake()
    {
        dialogue.SetActive(false);
    }

    private void Update() {
        stayTime += Time.deltaTime;

        if(stayTime >= stayTimeSpan)
        {
            dialogue.SetActive(false);
        }
    }

    private void OnCollisionEnter2D(Collision2D other) {
        // if(other.gameObject.CompareTag("Player"))
        // {
        //     dialogue.SetActive(true);
        // }
    }

    public void ShowDialogue()
    {
        dialogue.SetActive(true);
        stayTime = 0f;
    }
}
