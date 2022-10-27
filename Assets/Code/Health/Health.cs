using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private float startingHealth;
    public float currentHealth {get; private set;}
    private Animator anim;
    private bool dead;

    private void Awake(){
        currentHealth = startingHealth;
        anim = GetComponent<Animator>();
    }

    public void TakeDamage(float _damage){
        currentHealth = Mathf.Clamp(currentHealth - _damage, 0, startingHealth);

        if (currentHealth > 0)
        {
            //enable this after having the animation
            anim.SetTrigger("hurt");
        } else {
            if (!dead){
                //enable this after having the animation
                anim.SetTrigger("die");


                //for Player
                if (GetComponent<PlayerCode>() != null)
                    GetComponent<PlayerCode>().enabled = false;
                //for Enemy
                if (GetComponentInParent<EnemyPatrol>() != null)
                    GetComponentInParent<EnemyPatrol>().enabled = false;
                if (GetComponent<Enemy>() != null)
                    GetComponent<Enemy>().enabled = false;

                dead = true;
            }
        }
    }

    private void Deactivate()
    {
        gameObject.SetActive(false);
    }
}
