using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

public class PlayerController : Controller
{
   public KeyCode moveForwardKey;
   public KeyCode moveBackwardKey;
   public KeyCode rotateClockwiseKey;
   public KeyCode rotateCounterClockwiseKey;
   public KeyCode shootKey;
 


    // Start is called before the first frame update
    public override void Start()
    {
       
      
 
 
        //check if we have a game manager
        if (GameManager.instance != null)
        {
            //register with GM
            GameManager.instance.players.Add(this);
        }
        base.Start();
    }

    // Update is called once per frame
    public override void Update()
    {
        // Process our Keyboard Inputs
        ProcessInputs();

        // Run the Update function from the parent (base) class
        base.Update();
    }

    public override void ProcessInputs()
    {
        if (Input.GetKey(moveForwardKey))
        {
            pawn.MoveForward();
            pawn.MakeNoise();
        }

        if (Input.GetKey(moveBackwardKey))
        {
            pawn.MoveBackward();
            pawn.MakeNoise();
        }

        if (Input.GetKey(rotateClockwiseKey))
        {
            pawn.RotateClockwise();
            pawn.MakeNoise();
        }

        if (Input.GetKey(rotateCounterClockwiseKey))
        {
            pawn.RotateCounterClockwise();
            pawn.MakeNoise();
        }

        if (Input.GetKeyDown(shootKey))
        {
            pawn.Shoot();
            pawn.MakeNoise();
        }

        if (!Input.GetKey(moveBackwardKey) && !Input.GetKey(moveForwardKey) && !Input.GetKey(rotateClockwiseKey) && !Input.GetKey(rotateCounterClockwiseKey) && !Input.GetKey(shootKey))
        {
            pawn.StopNoise();
        }
    }
     public void OnDestroy()
    {
        //if we have a gm
        if(GameManager.instance != null)
        {
            //instance is tracking the destroyed player
            if(GameManager.instance.players != null)
            {
                //deregister wiht GM
                GameManager.instance.players.Remove(this);
            }
        }
    }
}
