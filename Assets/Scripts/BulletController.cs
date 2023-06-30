using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    public float moveSpeed, lifeTime;

    public Rigidbody theRB;

    public GameObject impactEffect;

    public int damage = 1;

    public bool damageEnemy, damagePlayer;

    private void Awake()
    {
        
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        theRB.velocity = transform.forward * moveSpeed;

        lifeTime -= Time.deltaTime;

        if(lifeTime <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        int difficultyModifier = PlayerPrefs.GetInt("difficultyModifier");

        if (other.gameObject.tag == "Enemy" && damageEnemy)
        {
            //Destroy(other.gameObject);
            if (difficultyModifier < 0)
            {
                other.gameObject.GetComponent<EnemyHealthController>().DamageEnemy((int)(damage * 1.5));
            }
            else
            {
                other.gameObject.GetComponent<EnemyHealthController>().DamageEnemy(damage);
            }
            
        }

        if (other.gameObject.tag == "Headshot" && damageEnemy)
        {
            other.transform.parent.GetComponent<EnemyHealthController>().DamageEnemy(damage * 2);
            Debug.Log("Headshot hit");
        }

        if (other.gameObject.tag == "Player" && damagePlayer)
        {

            int adjustedDamage = damage + difficultyModifier * 3;

            if (adjustedDamage < 1) adjustedDamage = 1;
            if (adjustedDamage > 999) adjustedDamage = 999;

            //Debug.Log("Hit Player at " + transform.position);
            if (difficultyModifier > 0)
            {
                PlayerHealthController.instance.DamagePlayer(adjustedDamage);
            }
            else if (difficultyModifier < 0)
            {
                PlayerHealthController.instance.DamagePlayer((int)(damage / 1.35));
            }
            else
            {
                PlayerHealthController.instance.DamagePlayer(damage);
            }
            
        }

        Destroy(gameObject);
        Instantiate(impactEffect, transform.position + (transform.forward * (-moveSpeed * Time.deltaTime)), transform.rotation);
    }
}
