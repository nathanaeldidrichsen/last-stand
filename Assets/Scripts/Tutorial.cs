using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GameManager.Instance.PauseGame();
    }



    public void CloseTutorial()
    {
        GameManager.Instance.PauseGame();
        Destroy(this.gameObject);
    }
}
