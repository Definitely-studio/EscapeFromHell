using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : PawnBase
{

    // значние отрицательно т.к. метод, который считает HP добавляет отрицательное значение и уменьшает так хп
    public float attackCooldownTime = 2.0f;
    public GameObject EquippedWeapon;
    public Transform WeaponSocket;
    public States state;
    public EnemyTypes enemyType;
    public PlayerDetector playerDetector;
    public Animator enemyAnimator;
    public EnemyActions enemyActions;
    public EnemyMeleeWeapon enemyMeleeWeapon;
    public Collision2D MeleeAttackCollision;
    public float destroyTime = 3f;

    // приватные поля
    protected Rigidbody2D rb;
    protected bool isAttackCooldown = false;
    private Transform target;
    private Transform bodySprite;

    public AudioSource AudioDead;


    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindWithTag("Player").transform;
        state = (state != States.passive) ? States.lookingfor : States.passive;
        rb = GetComponent<Rigidbody2D> ();
        //bodySprite = transform.Find("Body").transform;
        enemyAnimator = GetComponent <Animator> ();
        //playerDetector = transform.Find("PlayerDetector").GetComponent<PlayerDetector>();
        target = GameObject.FindWithTag("Player").transform;
    }


    // Update is called once per frame
    public virtual void Update()
    {


      // проверяем, что ИИ не мертв
      if(state != States.dead && state != States.passive)
      {
        transform.position = new Vector3 (transform.position.x,  transform.position.y, transform.position.y * 0.01f);
        
        if(enemyAnimator != null)
        {
          SetAnimatorKeys();
        }
        
         if(WeaponSocket != null)
        {
          rangedWeaponRotation();
        }
        //Debug.Log($" isAttackCooldown {isAttackCooldown}");
        //Debug.Log($" can we shoot {playerDetector.GetCanWeShoot()}");
        //Debug.Log($" isAttackCooldown {isAttackCooldown}");
        //Debug.Log($" summt {!isAttackCooldown && playerDetector.GetCanWeShoot()}");

        if(playerDetector != null)
        {
          if(!isAttackCooldown && playerDetector.GetCanWeShoot() && !GetIsDead())
          {
            //Debug.Log("EnemyAttack");
            //Debug.Log($" player is found {playerDetector.GetCanWeShoot()}");
            AttackStart();
            //Debug.Log("EnemyAttack");
          }

          else if(!isAttackCooldown && !playerDetector.GetCanWeShoot())
          {
            AttackEnd();
          }
        }
      }


    }

    public Rigidbody2D GetRigidBody(){

      return rb;
    }

    void SetAnimatorKeys(){

      if(state == States.passive)
      {
        enemyAnimator.SetBool("MoveRight", false);
        enemyAnimator.SetBool("MoveLeft", false);
        enemyAnimator.SetBool("MoveTop", false);
        enemyAnimator.SetBool("MoveBack", false);
        enemyAnimator.SetBool("Idle", true);
      }
      else
      {
      Vector2 lookDirection = GetLookAtDirection();

      if(lookDirection.normalized.y < 0.5f && lookDirection.normalized.y > -0.5f)
      {

        if (lookDirection.normalized.x < - 0.5f)
        {
          //Debug.Log("MoveLeft");
          enemyAnimator.SetBool("MoveRight", false);
          enemyAnimator.SetBool("MoveLeft", true);
          enemyAnimator.SetBool("MoveTop", false);
          enemyAnimator.SetBool("MoveBack", false);
        //  enemyAnimator.SetBool("Idle", false);
          transform.localScale = new Vector3(0.5f, transform.localScale.y, transform.localScale.y);
        }
        else if (lookDirection.normalized.x > 0.5f)
        {
          enemyAnimator.SetBool("MoveRight", true);
          enemyAnimator.SetBool("MoveLeft", false);
          enemyAnimator.SetBool("MoveTop", false);
          enemyAnimator.SetBool("MoveBack", false);
        //  enemyAnimator.SetBool("Idle", false);
          transform.localScale = new Vector3(0.5f, transform.localScale.y, transform.localScale.y);
        }
      }
      else
      {
        if(lookDirection.normalized.y > 0.5f)
        {
          enemyAnimator.SetBool("MoveBack", true);
          enemyAnimator.SetBool("MoveLeft", false);
          enemyAnimator.SetBool("MoveRight", false);
          enemyAnimator.SetBool("MoveTop", false);
        //  enemyAnimator.SetBool("Idle", false);
          transform.localScale = new Vector3(0.5f, transform.localScale.y, transform.localScale.y);
        }
        else if(lookDirection.normalized.y < -0.5f)
        {
          enemyAnimator.SetBool("MoveRight", false);
          enemyAnimator.SetBool("MoveLeft", false);
          enemyAnimator.SetBool("MoveTop", true);
          enemyAnimator.SetBool("MoveBack", false);
        //  enemyAnimator.SetBool("Idle", false);
          transform.localScale = new Vector3(0.5f, transform.localScale.y, transform.localScale.y);
        }
      }

  /*  else
    {
      enemyAnimator.SetBool("MoveRight", false);
      enemyAnimator.SetBool("MoveLeft", false);
      enemyAnimator.SetBool("MoveTop", false);
      enemyAnimator.SetBool("MoveBack", false);
      enemyAnimator.SetBool("Idle", true);
    }*/
    }
  }


    public virtual void AttackStart(){

    }

    public virtual void AttackEnd(){

    }

    // петод разворачивающий оружие в сторону игрока
    void rangedWeaponRotation()
    {

      if(target != null && !GetIsDead()){
        //Debug.Log("rangedWeaponRotation");
        Vector2 lookDirection = GetLookAtDirection();
        float angle = Mathf.Atan2(lookDirection.y,lookDirection.x) * Mathf.Rad2Deg - 90f;
        //float angle = Mathf.Atan2(lookDirection.y,lookDirection.x) * Mathf.Rad2Deg;
        WeaponSocket.eulerAngles = new Vector3(0,0, angle);
        
      }
    }

    Vector2 GetLookAtDirection(){

      if(target != null)
        return new Vector2(target.position.x, target.position.y) - rb.position;
      else
        return Vector2.zero;
    }



    void OnCollisionEnter2D(Collision2D other)
    {

    }

    void OnTriggerEnter2D(Collider2D other)
    {

    }

    // кулдаун атаки
    public IEnumerator AttackCooldown(float waitTime)
    {
        isAttackCooldown = true;
        yield return new WaitForSeconds(waitTime);
        isAttackCooldown = false;
        state = States.attackig;

    }

}

public enum States
{
  passive,
  lookingfor,
  attackig,
  dead
}

public enum EnemyTypes
{
  ranged,
  melee
}
