using UnityEditor;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public static readonly string moveAxisNameVertical = "Vertical";
    public static readonly string moveAxisNameHorizontal = "Horizontal";
    public static readonly string fireAxisName = "Fire1";
    public static readonly string reloadAxisName = "Reload";
    public static readonly string jumpAxisName = "Jump";

    public float MoveV { get; private set; }
    public float MoveH { get; private set; }        
    public bool Fire { get; private set; }
    public bool Reload { get; private set; }
    public bool Jump { get; private set; }  

    private void Update()
    {
        MoveV = Input.GetAxis(moveAxisNameVertical);
        
        MoveH = Input.GetAxis(moveAxisNameHorizontal);          

        Fire = Input.GetButton(fireAxisName);

        Reload = Input.GetButtonDown(reloadAxisName);

        Jump = Input.GetButtonUp(jumpAxisName); 
    }

}
