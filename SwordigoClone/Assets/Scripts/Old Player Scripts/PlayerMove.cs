using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{

    private Rigidbody2D myBody;
    private float moveForce_X = 5.0f, moveForce_Y = 3.0f;

    private PlayerAnimations PlayerAnimation; //object creation? temporarily disabled because playeranimations is a script
    

    //for jump
    private float jump_Force = 3f, second_Jump_Force = 5f;

    private bool first_Jump, second_Jump;


    //Awake is the first function called when the game runs

    void Awake()
    {
        myBody = GetComponent<Rigidbody2D>(); //way to get the game objects on which we have attached the component
        //This is how we get components from our game object on which we attached those components

        PlayerAnimation = GetComponent<PlayerAnimations>(); //temporarily disabled
    }

    private void Update()
    {
         PlayerAttack();
         
    }

    void FixedUpdate()
    {
         Move();
    }


    void JumpFunc()
    {
        if (first_Jump) //ns : myBody.velocity.y keeps it unaffected.. and only the parameter where the speed needs to be changed is mentioned
        {

            first_Jump = false; //disabling it so that we don't have infinite jumps
            myBody.velocity = new Vector2(myBody.velocity.x, jump_Force);
            //
           // PlayerAnimation.SetTrigger(TagManager.JUMP_PARAMETER); // set the fly trigger which moves it from the idle state to the fly state
            //PlayerAnimation.JumpAnim(true);
        }

        else if (second_Jump)
        {
            second_Jump = false; //disabling it so that we don't have infinite jumps
            myBody.velocity = new Vector2(myBody.velocity.x, second_Jump_Force);
            //PlayerAnimation.SetTrigger(TagManager.JUMP_PARAMETER);
            // PlayerAnimation.JumpAnim(true);
            PlayerAnimation.JumpAnim(false);
        }

    }


    void PlayerAttack()
    {
        
        


        

        if (Input.GetKey(KeyCode.L)) //for shooting. If it is gekeydown, it does not work
        {
            //print("Melee attack");
            PlayerAnimation.MeleeAttackAnimation();
            }

        if (Input.GetKey(KeyCode.K)) {

            //print("Ranged attack");
            PlayerAnimation.RangedAttackAnimation();
        }





    }



    void Move()
    {
        float h = Input.GetAxisRaw("Horizontal"); //for horizontal axis
        float v = Input.GetAxisRaw("Vertical"); //for vertical axis

        //print("GET AXIS: "+ Input.GetAxisRaw("Horizontal"));

        if (h > 0)
        {
            print("altering x velocity");
            myBody.velocity = new Vector2(moveForce_X, myBody.velocity.y); //we are only altering the x velocity
        }

        else if (h < 0)
        {
            print("altering x1 velocity");
            myBody.velocity = new Vector2(-moveForce_X, myBody.velocity.y); //we are only altering the x velocity
        }

        else
        {
            print("altering y velocity");
            myBody.velocity = new Vector2(0, myBody.velocity.y); //we are only altering the x velocity, y velocity
        }

        //now for vertical axis // need to alter this to jump rather than move up

        if (v > 0)
        {
            //PlayerAnimation.PlayerRunAnimation(false); //disabling run while jumping
            // PlayerAnimation.JumpAnim();
            myBody.velocity = new Vector2(myBody.velocity.x, moveForce_Y); //we are only altering the y velocity -disabled for jumping instead
            JumpFunc();
        }

        else if (v < 0)
        {
            //PlayerAnimation.PlayerRunAnimation(false); //disabling the run while jumping
            //JumpFunc();
             myBody.velocity = new Vector2(myBody.velocity.x, -moveForce_Y); //we are only altering the y velocity -disabled for jumping instead
        }

        else
        {
            myBody.velocity = new Vector2(myBody.velocity.x, 0);
        }

        //ANIMATE

        if (myBody.velocity.x!= 0 && myBody.velocity.y<=0 ) //partially working but does not fully do so
        {
            print("First one - running");
            PlayerAnimation.PlayerRunAnimation(true);
            PlayerAnimation.JumpAnim(false);
        }

        else if (myBody.velocity.x==0 && myBody.velocity.y > 0)
        {
            print("Second one - jumping still");
            PlayerAnimation.PlayerRunAnimation(false);
            PlayerAnimation.JumpAnim(true);
            
        }

        else if(myBody.velocity.x!=0 && myBody.velocity.y > 0)
        {
            print("Third one - running and jumping");
            PlayerAnimation.PlayerRunAnimation(false);
            PlayerAnimation.JumpAnim(true);
        }

        else if (myBody.velocity.x == 0 && myBody.velocity.y == 0)
        {
            print("Fourth one - standing still");
            PlayerAnimation.PlayerRunAnimation(false);
            PlayerAnimation.JumpAnim(false); //previously had false
        }


        /*else if(myBody.velocity.x > 0 && myBody.velocity.y > 0){
            PlayerAnimation.PlayerRunAnimation(false);
        }*/

        Vector3 tempScale = transform.localScale; //to make him face the direction he is moving to

        if (h > 0)
        {
            tempScale.x = -1f;
        }

        else if (h < 0)
        {
            tempScale.x = 1f;
        }

        transform.localScale = tempScale;
    }

}
