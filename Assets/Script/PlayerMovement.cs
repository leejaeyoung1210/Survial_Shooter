using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody rb;
    private Animator animator;
    private PlayerInput input;
    private Camera maincamera;
    private LayerMask groundMask;

    public Collider Basecollider;
   

    public float moveSpeed = 8f;    
    private bool isGround = false;
    private Vector3 pos;


    public void Awake()
    {
        rb = GetComponent<Rigidbody>();
        input = GetComponent<PlayerInput>();
        animator = GetComponent<Animator>();        
        maincamera = Camera.main;
        groundMask = LayerMask.GetMask("Base");

    }
    
    private void FixedUpdate()
    {


        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        //RaycastHit hit;
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            Vector3 mouseDir = new Vector3(hit.point.x, transform.position.y, hit.point.z);
            
            transform.LookAt(mouseDir);

        }

        var distanceX = input.MoveV * moveSpeed * Time.deltaTime;
        var distanceZ = input.MoveH * moveSpeed * Time.deltaTime;

        rb.MovePosition(transform.position + distanceX * transform.forward + distanceZ * transform.right);


        // ( "피라미터 이름" /  값)
        animator.SetFloat("moveX", input.MoveV);
        animator.SetFloat("moveZ", input.MoveH);

    }

   

}
