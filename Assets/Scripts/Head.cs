using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Head : MonoBehaviour
{
    public GameController gameController;

    private void OnTriggerEnter2D(Collider2D col)
    {
        switch(col.gameObject.tag)
        {
            case "Food":
                gameController.Eat();
                break;

            case "tail":
                gameController.SendMessage("GameOver");
                break;
        }
    }

}
