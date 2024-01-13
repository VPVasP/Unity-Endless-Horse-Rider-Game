using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorseController : MonoBehaviour
{
    //our speed parameters 
    [Header("Parameters")]
    [SerializeField] private float speed;
    [SerializeField] private float jumpSpeed;
    [SerializeField] private float jumpForwardSpeed;
    [SerializeField] bool isGrounded = false; //isgrounded bool
    [SerializeField] float groundDistance = 0.6f; //the ground distance value
    public LayerMask groundMask; //layermask for the ground
    //our components
    private Rigidbody rb;
    private Animator anim;
    private AudioSource aud;
    //our audioclips
    [Header("Audio Clips")]
    [SerializeField] private AudioClip jumpClip;
    [SerializeField] private AudioClip deathClip;
    private void OnEnable()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
    private void Start()
    {
        //we asssing our components
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        if(aud == null)
        {
            gameObject.AddComponent<AudioSource>();
        }
        aud = GetComponent<AudioSource>();
        aud.playOnAwake= false;
        aud.loop=false;
    }

    private void Update()
    {
        //if we press the space button and we are grounded 
        if (Input.GetKeyDown(KeyCode.Space) && isGroundedBool())
        {
            //we add force to our rigibody both forward and up
            rb.AddForce(Vector3.up * jumpSpeed, ForceMode.Impulse);
            rb.AddForce(Vector3.forward * jumpForwardSpeed, ForceMode.Impulse);

            //we play the jump animation and the sound
            anim.SetTrigger("Jump");
            aud.clip= jumpClip;
            aud.Play();
        }
    }

    private void FixedUpdate()
    {
        //input for the z axis 
        Vector3 m_Input = new Vector3(0, 0,1);
        isGrounded = isGroundedBool();//checking if the horse is grounded 
        anim.SetBool("IsWalking", true);
        //we move the hore based on the speed 
        rb.MovePosition(transform.position + m_Input.normalized * Time.deltaTime * speed);   
    }
    //bool that checks if the horse is grounded using raycast
    bool isGroundedBool()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit, groundDistance, groundMask))
        {
            return true; 
        }

        return false;
    }
    //function that handles the horse's death
        public void Dead()
    {
        aud.clip = deathClip;
        aud.Play();
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }
}