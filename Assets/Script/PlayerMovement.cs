using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody rb;
    private Animator animator;
    private PlayerInput input;
    
    private LayerMask groundMask;    
   
    public float moveSpeed = 8f;    
    private bool isGround = false;
    private Vector3 pos;


    public void Awake()
    {
        rb = GetComponent<Rigidbody>();
        input = GetComponent<PlayerInput>();
        animator = GetComponent<Animator>();                
        groundMask = LayerMask.GetMask("Base");

    }
    
    private void FixedUpdate()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);        // 시작위치랑 방향
        //RaycastHit hit; 
        if (Physics.Raycast(ray, out RaycastHit hit , float.MaxValue , groundMask)) // 거리랑 검사지점
        {       //시작주소, 오리진값     
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
