using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player : MonoBehaviour
{
    //movement
    public CharacterController mycc;
    public float mouseSensitivity;
    public Transform myCameraHead;
    float cameraVerticalMovment;
    public float walkingspeed = 10f;
    public float runningspeed = 20f;
    public bool isrunning;
    
    
    //jump section
    public float gravitymodifier;
    public float jumpheight = 10f;
    Vector3 velocity;
    
    //crouching section 
    public float crouchingspeed = 6f;
    bool iscrouching = false;
    Vector3 crouchscale = new Vector3(1, 0.5f, 1);
    private float inntialcontrollerHeight;
    Vector3 normalscale;
    public Transform mybody;

    //sliding section
    public float slidingspeed = 25f;
    public bool issliding;
    public float slidingtime = 0;
    public float maxslidingtime = 3;
    //animator section
    public Animator myanimator;
    
    
    
    private void Start()
    {
        normalscale = mybody.localScale;
        inntialcontrollerHeight = mycc.height;
    }

    // Update is called once per frame
    void Update()
    {
        Playermovment();
        mouseMovement();
        jump();
        crouch();
        Sprint();
        sliding();
    
    
    }



    private void Sprint()
    {
        if(Input.GetKey(KeyCode.LeftShift))
        {
            isrunning = true;

        }

        else
        {
            isrunning = false;

        
        }
    }


    private void crouch()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {

            StartCrouching();
        }
        if (Input.GetKeyUp(KeyCode.C))
        {
            EndCrouching();


        }



    }


    private void StartCrouching()
    {
        iscrouching = true;
        mycc.height /= 2;
        mybody.localScale = crouchscale;

        if (isrunning)
        {
            //this velocity 
            velocity = Vector3.ProjectOnPlane(myCameraHead.transform.forward, Vector3.up);
            issliding = true;
            mycc.Move(velocity);
        }

    }
                
                
                
                
    private void EndCrouching()
    {
        mybody.localScale = normalscale;
        mycc.height = inntialcontrollerHeight;
        iscrouching = false;
    
    }   
    
    
    
    
    
    

    
    
    private void jump()
    {  // only allow to jump the player if we are touching the ground
       if (Input.GetButtonDown("Jump") && mycc.isGrounded)
       {

            velocity.y = jumpheight; // yvelocity is being subtracted in playermovement to create the effect of gravity
            mycc.Move(velocity);

       } 
    }


    private void sliding()
    {
      if(issliding)
      {
            
            slidingtime += Time.deltaTime;   
      }
      if(slidingtime > maxslidingtime)
      {
            issliding = false;
            velocity = new Vector3.zero;
      }          
    }
    private void Playermovment()
    {

        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        Vector3 movement = transform.right * moveX + transform.forward * moveZ;
        if (issliding)
        {


           if (isrunning) //if running is = true 
           {
            movement *= (runningspeed * Time.deltaTime);
           }
          else
          {

            if (iscrouching) //if crouching is = true 
            {
                movement *= (crouchingspeed * Time.deltaTime);
            }

            else
            {
                movement *= (walkingspeed * Time.deltaTime);
            }




          }
        }
            mycc.Move(movement);
        Debug.Log(movement.magnitude);

        myanimator.SetFloat("Playerspeed", movement.magnitude);
        velocity.y += mycc.velocity.y + (Physics.gravity.y * gravitymodifier);
       
        
        if (mycc.isGrounded)
        {
            //Debug.Log("Is Grounded is working");
            velocity.y = Physics.gravity.y * Time.deltaTime;
        
        
        }
        
        mycc.Move(velocity);

    
    
    
    }


    private void mouseMovement()
    {
        
        float xmovment = Input.GetAxisRaw("Mouse X") * mouseSensitivity * Time.deltaTime;
        float ymovment = Input.GetAxisRaw("Mouse Y") * mouseSensitivity * Time.deltaTime;
        transform.Rotate(Vector3.up * xmovment);
        Debug.Log("Mouse X position: " + xmovment);
        Debug.Log("Mouse Y position: " + ymovment);



        
        ymovment = ymovment * -1;
        cameraVerticalMovment += ymovment;
        cameraVerticalMovment = Mathf.Clamp(cameraVerticalMovment, -90f, 90f);
        myCameraHead.rotation = Quaternion.Euler(cameraVerticalMovment, 0, 0);
                                                                                                                                    
    }
    
    
    
    
    
    
    
    








}
