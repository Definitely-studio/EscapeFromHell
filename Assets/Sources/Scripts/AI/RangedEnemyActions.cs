using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedEnemyActions : EnemyActions
{

  public EnemyRangedWeapon weapon;
   float attackCooldownTime = 2f; 
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    //активируем коллизию которая отвечает за атаку
    public void Shoot()
    {
      if(weapon != null)
        weapon.Shoot();
    }

    public override void AttackStart(){
      
        Debug.Log("Attack");
       // enemy.state = States.attackig;
        enemy.animationsController.SetAttackAnimatorKey(true);

        StartCoroutine(AttackCooldown(attackCooldownTime));
    }

     public override void AttackEnd(){
        Debug.Log("AttackEnd");
       // enemy.state = States.attackig;
        enemy.animationsController.SetAttackAnimatorKey(false);
        
    }

    IEnumerator AttackCooldown(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        AttackEnd();
    }


}