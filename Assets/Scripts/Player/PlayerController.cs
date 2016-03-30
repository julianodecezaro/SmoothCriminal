using UnityEngine;
using UnityEditor;
using System.Collections;

public class PlayerController : GenericController {

    public Camera MainCamera;
    GameObject ActiveItem;
    ThrowController Throw;
    SpriteRenderer ActiveItemSR;
    Transform CamPos;

    // Use this for initialization
    void Start () {
        //JumpHeight = 200.0f;
        //Speed = 15.0f;
        //ActiveItem = (GameObject)AssetDatabase.LoadAssetAtPath("Assets/Items/Prefabs/KunaiItem.prefab", typeof(GameObject));
        //Throw = (ThrowController)ActiveItem.GetComponent("ThrowController");
        //ActiveItemSR.GetComponent<SpriteRenderer>();
        //Debug.Log(ActiveItem.name);    
        JumpHeight = 40.0f;
        Speed = 2.5f;
        CamPos = (Transform)this.transform.FindChild("CameraPosition");
    }
	
	// Update is called once per frame
	void FixedUpdate() {
        //Debug.Log(Input.GetAxis("HorizontalJS"));


        if (Input.GetButton("Horizontal")) {
            if (Input.GetAxis("Horizontal") > 0) {
                MoveRight();
            }else {
                MoveLeft();
            }
            SetState("Running");
        }
        else {
            SetState("Running", false);
        }

        if (Input.GetButtonDown("Jump") && Grounded) {
            Jump();
            SetState("Grounded", false);
        }

        /*SetState("Throwing", false);
        if (Input.GetButtonDown("Fire1") && Grounded) {
            this.ThrowElementPoint = getThrowPoint();
            ThrowTrigger();
            SetState("Throwing");
        }*/

        LockRotation(0.10f, -0.10f);

        UpdateAnimation();        
    }

    void LateUpdate()
    {
        MainCamera.transform.position = new Vector3(transform.position.x, MainCamera.transform.position.y, MainCamera.transform.position.z);
        //MainCamera.transform.position = new Vector3(CamPos.transform.position.x, MainCamera.transform.position.y, MainCamera.transform.position.z);
    }

    Transform getThrowPoint()
    {
        this.ThrowElementTransform = ActiveItem.transform;
        this.InvokeDelay = 0.3f;

        Vector3 PlayerPosition = transform.position;
        Transform ThrowPosition;
        if (MovingRight) {
            //ActiveItem.transform.localRotation = Quaternion.Euler(0, 0, 0);
            //ActiveItemSR.flipX = true;
            Throw.Force = 7.0f * Time.deltaTime;
        }else {
            //ActiveItem.transform.localRotation = Quaternion.Euler(0, 180, 0);
            //ActiveItemSR.flipX = false;
            Throw.Force = (7.0f * Time.deltaTime) * -1.0f;            
        }
        ThrowPosition = ThrowPoint;
        return ThrowPosition;
    }

    /*
    ** Update player animation.
    **
    ** Created on 02/02/2016
    ** Developed by Juliano S. De Cezaro <julianodecezaro@gmail.com>    
    */
    protected void UpdateAnimation()
    {
        Animator PlayerAnim = this.GetComponent<Animator>();
        PlayerAnim.SetBool("Running", Running);
        PlayerAnim.SetBool("Jumping", !Grounded);
        PlayerAnim.SetBool("Grounded", Grounded);
        //PlayerAnim.SetBool("Throwing", Throwing);
    }

    /*
    ** Method that moves element to the left.
    **
    ** Created on 02/02/2016
    ** Developed by Juliano S. De Cezaro <julianodecezaro@gmail.com>    
    */
    protected void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            SetState("Grounded");
        }
    }
}
