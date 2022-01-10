using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chase : NPCBaseFMS
{
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateEnter(animator, stateInfo,layerIndex);
        //NPC = animator.gameObject;
        //agent = NPC.GetComponent<Agent>();
        
        if(Time.timeScale != 0)
        {
             if(NPC.transform.position.x < NPC.GetComponent<Enemy>().Player.transform.position.x)
                agent.SetAgentDestination(NPC.GetComponent<Enemy>().Player.GetComponentInChildren<Player>().GetLeftSide());
            else
            {
                agent.SetAgentDestination(NPC.GetComponent<Enemy>().Player.GetComponentInChildren<Player>().GetRightSide());
            }
        }
       
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if(Time.timeScale != 0)
        {
            if(NPC.transform.position.x < NPC.GetComponent<Enemy>().Player.transform.position.x)
                agent.SetAgentDestination(NPC.GetComponent<Enemy>().Player.GetComponentInChildren<Player>().GetLeftSide());
            else
            {
                agent.SetAgentDestination(NPC.GetComponent<Enemy>().Player.GetComponentInChildren<Player>().GetRightSide());
            }
        }
       

    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
