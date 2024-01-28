using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public Transform target;
    private Vector3 playerVelocity;
    private bool groundedPlayer;
    public float playerSpeed =15f;
    public float jumpHeight =6f;
    private bool isJumping;
    private float verticalSpeed;
    private float jumpStartTime;
    private float airTime;
    private float gravityValue = -9.81f;
    private CharacterController characterController;

    void Start(){
        characterController = gameObject.AddComponent<CharacterController>();
    }
    void Update(){
        groundedPlayer = characterController.isGrounded;
        Jumping();   
        Vector3 move = new Vector3(Input.GetAxis("Horizontal"),0,Input.GetAxis("Vertical")) ;
        characterController.Move(move * Time.deltaTime * playerSpeed);

        if(Input.GetButtonDown("Jump")&& groundedPlayer){
            playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
        }
        if(characterController.isGrounded){
            airTime = 0f;
        }
        else{
            airTime += Time.deltaTime;
            if(airTime > 15.0f){
                //bring back to first platform
                //reset gamemanager platform counter
                Die();

            }
        }
        
        playerVelocity.y += gravityValue * Time.deltaTime;
        characterController.Move(playerVelocity * Time.deltaTime);
    }
    void Die(){
        transform.position = Vector3.zero;
        GameManager.Instance.SetupNewGame();
    }
    void Jumping(){
        if(groundedPlayer){
            playerVelocity.y =-0.5f;
            if(Input.GetButtonDown("Jump"))
            {
                isJumping = false;

                verticalSpeed = jumpHeight;
            }
        }
        verticalSpeed += Physics.gravity.y * Time.deltaTime;
        Vector3 verticalMovement = new Vector3(0f, verticalSpeed * Time.deltaTime, 0f);
        characterController.Move(verticalMovement);
    }
}
