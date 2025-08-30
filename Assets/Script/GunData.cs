using UnityEngine;

[CreateAssetMenu(fileName = "GunData", menuName = "Scriptable Objects/GunData")]
public class GunData : ScriptableObject
{
    public AudioClip shootClip;  

    public int damage = 10;

    public int startAmmo = 150;
    public int ammo = 30;
    public int maxAmmo = 150;
    
    public float timeBetFire = 0.15f;
    public float reloadTime = 1.5f;

    public float firedistance = 50f;
}
