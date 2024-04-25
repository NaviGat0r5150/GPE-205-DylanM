using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateOtherTestState : MonoBehaviour
{
    public void ChangeToMainMenu()
    {
        if(GameManager.instance != null)
        {
            GameManager.instance.ActivateTestState();

        }
    }
}
