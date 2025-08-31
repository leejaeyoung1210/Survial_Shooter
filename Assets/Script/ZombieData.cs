using UnityEngine;

[CreateAssetMenu(fileName = "ZombieData", menuName = "Scriptable Objects/ZombieData")]
public class ZombieData : ScriptableObject
{
    public AudioClip hurtClip;
    public AudioClip deathClip;

    public int maxHp;
    public int speed;
    public int damage;

    public float attackTime;
    public float reAttackTime;

}
