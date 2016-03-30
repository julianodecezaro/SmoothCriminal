using UnityEngine;
using System.Collections;

/*
** Generic class for creating game elements (Players, enemies, items, etc...).
**
** Created on 02/02/2016
** Developed by Juliano S. De Cezaro <julianodecezaro@gmail.com>
**
** Maintances: {Description}, on {date} by {developer}.
*/
public class GenericController : MonoBehaviour
{
    //Flag to set if element can be picked up.
    bool PickupAble;
    //Flag to set if element can be thrown up.
    bool ThrowAble;
    //Flag to set if element is moving to the right.
    protected bool MovingRight = true;
    //Flag to set if element is moving.
    protected bool Running = false;
    //Flag to set if element is jumping.
    protected bool Jumping = false;
    //Flag to set if element is grounded.
    protected bool Grounded = true;
    //Flag to set if element is throwing.
    protected bool Throwing = false;
    //Element movement speed.
    protected float Speed = 5.0f;
    //Element height.
    float Height;
    //Element width.
    float Width;
    //Height that the element can reach.
    protected float JumpHeight = 80.0f;
    protected float InvokeDelay = 0.0f;
    //Screen (or camera) top position (Y axis).
    float ScreenTop;
    //Screen (or camera) bottom position (Y axis).
    float ScreenBottom;
    //Screen (or camera) left position (X axis).
    float ScreenLeft;
    //Screen (or camera) right position (X axis).
    float ScreenRight;
    //Number of remaining hits for the element to be destroyed / killed.
    int Hits;
    //Type of element (Player, enemy, box, apple, etc..).
    string Type;
    //Element SpriteRenderer component.
    public SpriteRenderer ElementSR;
    public Transform ThrowElementTransform;
    public Transform ThrowElementPoint;
    public Transform ThrowPoint;

    /*
    ** Initialization.
    **
    ** Created on 02/02/2016
    ** Developed by Juliano S. De Cezaro <julianodecezaro@gmail.com>    
    */
    void Awake()
    {
        //ElementSR = this.GetComponent<SpriteRenderer>();
        ThrowPoint = transform.FindChild("ThrowPoint");
    }

    /*
    ** Set if element is running.
    **
    ** Created on 02/02/2016
    ** Developed by Juliano S. De Cezaro <julianodecezaro@gmail.com>    
    */
    protected void SetRunning(bool value)
    {
        this.Running = value;
    }

    /*
    ** Set if element is jumping.
    **
    ** Created on 02/02/2016
    ** Developed by Juliano S. De Cezaro <julianodecezaro@gmail.com>    
    */
    protected void SetJumping(bool value)
    {
        this.Jumping = value;
    }

    /*
    ** Set if element is grounded.
    **
    ** Created on 02/02/2016
    ** Developed by Juliano S. De Cezaro <julianodecezaro@gmail.com>    
    */
    protected void SetGrounded(bool value)
    {
        this.Grounded = value;
    }

    /*
    ** Set if element is throwing something.
    **
    ** Created on 02/02/2016
    ** Developed by Juliano S. De Cezaro <julianodecezaro@gmail.com>    
    */
    protected void SetThrowing(bool value)
    {
        this.Throwing = value;
    }

    /*
    ** Set element state.
    **
    ** Created on 02/02/2016
    ** Developed by Juliano S. De Cezaro <julianodecezaro@gmail.com>    
    */
    protected void SetState(string state, bool value = true)
    {
        switch (state.ToLower()) {
            case "running":
                SetRunning(value);                
                break;
            case "jumping":
                SetJumping(value);
                break;
            case "grounded":
                SetGrounded(value);
                break;
            case "throwing":
                SetThrowing(value);
                break;
        }
    }

    /*
    ** Method that moves element to the left.
    **
    ** Created on 02/02/2016
    ** Developed by Juliano S. De Cezaro <julianodecezaro@gmail.com>    
    */
    protected void MoveLeft ()
    {
        transform.position = new Vector3(transform.position.x - (Speed * Time.deltaTime), transform.position.y, transform.position.z);
        if (ElementSR != null){
            ElementSR.flipX = true;
        }else {
            transform.localRotation = Quaternion.Euler(0, 180, 0);
        }
        MovingRight = false;
    }

    /*
    ** Method that moves element to the left.
    **
    ** Created on 02/02/2016
    ** Developed by Juliano S. De Cezaro <julianodecezaro@gmail.com>    
    */
    protected void MoveRight()
    {
        transform.position = new Vector3(transform.position.x + (Speed * Time.deltaTime), transform.position.y, transform.position.z);
        if (ElementSR != null){
            ElementSR.flipX = false;
        }else {
            transform.localRotation = Quaternion.Euler(0, 0, 0);
        }
        MovingRight = true;
    }

    /*
    ** Method that moves element to the left.
    **
    ** Created on 02/02/2016
    ** Developed by Juliano S. De Cezaro <julianodecezaro@gmail.com>    
    */
    protected void MoveUp()
    {

    }

    /*
    ** Method that moves element to the left.
    **
    ** Created on 02/02/2016
    ** Developed by Juliano S. De Cezaro <julianodecezaro@gmail.com>    
    */
    protected void MoveDown()
    {

    }

    /*
    ** Method that moves element to the left.
    **
    ** Created on 02/02/2016
    ** Developed by Juliano S. De Cezaro <julianodecezaro@gmail.com>    
    */
    protected void Jump()
    {
        this.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, JumpHeight));
    }

    /*
    ** Throw an element.
    **
    ** Created on 04/02/2016
    ** Developed by Juliano S. De Cezaro <julianodecezaro@gmail.com>    
    */
    public void ThrowTrigger()
    {
        if (InvokeDelay > 0) {
            Invoke("ThrowInstantiate", InvokeDelay);
        }else {
            ThrowInstantiate();
        }
    }

    /*
    ** Instantiate the element to be thrown.
    **
    ** Created on 04/02/2016
    ** Developed by Juliano S. De Cezaro <julianodecezaro@gmail.com>    
    */
    public void ThrowInstantiate()
    {
        Instantiate(ThrowElementTransform, ThrowElementPoint.position, ThrowElementPoint.rotation);
    }

    public void LockRotation(float LockRight, float Lockleft)
    {
        Debug.Log(this.transform.rotation.z);
        if (this.transform.rotation.z > LockRight || this.transform.rotation.z < Lockleft) {
            
            this.transform.rotation = Quaternion.Euler(this.transform.rotation.x, this.transform.rotation.y, LockRight);
        }
    }
}
