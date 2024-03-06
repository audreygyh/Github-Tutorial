using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        print("Start");
        
    }

    private void Update() {
        if(Input.GetKeyDown(KeyCode.A))
        {
            Debug.Log("按下A");
        }
    }
}
