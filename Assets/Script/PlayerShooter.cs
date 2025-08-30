using UnityEngine;

public class PlayerShooter : MonoBehaviour
{
    public Gun gun;
    private PlayerInput input;
    //private Collider gunCollider;

    private void Awake()
    {
        input = GetComponent<PlayerInput>();
        //gunCollider = GetComponent<Collider>();
    }

    //private void OnEnable()
    //{
    //    //gunCollider.enabled = false;
    //}
    private void Update()
    {
        if (input.Fire)
        {
            gun.Fire();
        }
        else if (input.Reload)
        {
            gun.Reload();   
        }     

    }
}
