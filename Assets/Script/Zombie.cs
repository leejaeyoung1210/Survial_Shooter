using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class Zombie : Life
{
    public enum State
    {
        Idle,
        MoveAttack,
        Die,
    }

    private NavMeshAgent agent;
    private State currentState = State.Idle;
    public Animator animator;
    private Collider zombieCollider;
    private AudioSource audioSource;

    public State CurrentState
    {
        get { return currentState; }
        set
        {
            currentState = value;
            switch (currentState)
            {
                case State.Idle:
                    animator.SetBool("HasTarget", false);
                    agent.isStopped = true;
                    break;
                case State.MoveAttack:
                    animator.SetBool("HasTarget", true);
                    agent.isStopped = false;
                    break;              
                case State.Die:
                    animator.SetBool("HasTarget", false);
                    agent.isStopped = true;
                    zombieCollider.enabled = false;
                    break;
            }
        }
    }

    public ZombieData zombieData;
    public float damage;
    public float lastAttackTime;
    public float attackInterval;

    private Transform target;
    public ParticleSystem damageEffect;

    private void Awake()
    {
        animator = GetComponent<Animator>();    
        zombieData = GetComponent<ZombieData>();
        zombieCollider = GetComponent<Collider>();
        audioSource = GetComponent<AudioSource>();
        agent = GetComponent<NavMeshAgent>();   

    }

    protected override void OnEnable()
    {
        base.OnEnable();
        zombieCollider.enabled = true;
        CurrentState = State.Idle;
    }

    public void Setup(ZombieData data)
    {
        MaxHp = data.maxHp;
        damage = data.damage;
        agent.speed = data.speed;

       
    }

    public override void OnDamage(float damage, Vector3 hitPoint, Vector3 hitNormal)
    {
        base.OnDamage(damage, hitPoint, hitNormal);
        audioSource.PlayOneShot(zombieData.hurtClip);

        damageEffect.transform.position = hitPoint;
        damageEffect.transform.forward = hitNormal;
        damageEffect.Play();
    }

    protected override void Die()
    {
        base.Die();
        CurrentState = State.Die;
    }

   
   



}
