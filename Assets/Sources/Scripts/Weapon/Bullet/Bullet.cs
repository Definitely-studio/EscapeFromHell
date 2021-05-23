using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField]private float speed;
    [SerializeField]private int damage;
    [SerializeField] private ParticleSystem explosion;
    [SerializeField] private SpriteRenderer sprite;

    public Rigidbody2D _rigidbody;
    private int _consSpeed;

    public float Speed { get => speed; set => speed = value; }
    public int Damage { get => damage; set => damage = value; }
    public new string tag;

    private void Awake()
    {
         Rigidbody2D rigidbody2D1 = this.gameObject.GetComponent<Rigidbody2D>();
         _rigidbody = rigidbody2D1;
        _rigidbody.gravityScale = 0f;
       
        //_consSpeed = Speed;
    }

    private void OnEnable()
    {
        explosion.gameObject.SetActive(false);
        //_rigidbody.AddForce(_rigidbody.transform.up * speed);
        //Speed = _consSpeed;
    }
    private void Start()
    {
         //Destroy(gameObject, 10f);
    }

    /*private void Move()
    {
        Vector2 velocity = transform.up * (speed * Time.fixedDeltaTime);
        _rigidbody.MovePosition(_rigidbody.position +  velocity);
    }

    private void FixedUpdate()
    {
        Move();
    }*/

    IEnumerator ExampleCoroutine()
    {
        yield return new WaitForSeconds(explosion.duration);
        this.gameObject.GetComponent<Collider2D>().enabled = true;
        this.gameObject.SetActive(false);
        sprite.enabled = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if(collision.gameObject.layer == LayerMask.NameToLayer("Player")
         || collision.gameObject.layer == LayerMask.NameToLayer("Enemy")
          || collision.gameObject.layer == LayerMask.NameToLayer("World")
          || collision.gameObject.layer == LayerMask.NameToLayer("Water"))
        {
            if(collision.gameObject.tag != tag && collision.gameObject.tag != gameObject.tag)
        {
            if(collision.gameObject.tag == "Player")
            {
                collision.gameObject.GetComponentInChildren<PlayerActions>().ChangeHP(-Damage);

            }
            
            transform.gameObject.GetComponent<Collider2D>().enabled = false;
            Speed = 0;
            explosion.gameObject.SetActive(true);
            sprite.enabled = false;
            StartCoroutine(ExampleCoroutine());
        }
        }
        
        /*
         if ( collision.gameObject.GetComponent<ParentfromBullet>()
         != null && this.gameObject.GetComponentInParent<ParentfromBullet>() != null){

            if ( collision.gameObject.GetComponentInParent<ParentfromBullet>().gameObject.layer
            != this.gameObject.GetComponentInParent<ParentfromBullet>().gameObject.layer)
            {
                Speed = 0;
                explosion.gameObject.SetActive(true);
                //his.delay(explosion.duration);

               /if(collision.gameObject.tag == "Player")
                    {
                        collision.gameObject.GetComponent<PlayerActions>().ChangeHP(damage);
                    }


                StartCoroutine(ExampleCoroutine());
                
            }
         }
         */
    }
}
