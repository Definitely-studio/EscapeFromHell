using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class finalDoor : MonoBehaviour
{
    // Start is called before the first frame update

    public GameMenu gameMenu;
  
    public Animator animator;
    // Start is called before the first frame update
    void Start()
    {
    
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other) {
        
        if (other.gameObject.tag == "Player" && other.gameObject.GetComponentInChildren<Player>().GetKey() == true)
        {
            animator.SetTrigger("ToBlack");
        }
    }
    void ToFinalScene()
    {
        gameMenu.LoadFinalScene();
    }
}
