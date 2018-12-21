using UnityEngine;

public class Attack : MonoBehaviour
{
    private enum TipoAtaque
    {
        rapido,
        fuerte
    }

    [SerializeField] private float radiusAttackCheck;
    [SerializeField] private int comboAttack;
    [SerializeField] private float comboTimeLimit;
    [SerializeField] private LayerMask enemigoLayer;

    [SerializeField] private Transform attackChecker;


    private int damage;
    private TipoAtaque ataque;
    private float time;

    private void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            AtaqueRapido();
        }
        else if (Input.GetButtonDown("Fire2"))
        {
            ataqueFuerte();
        }

        if (time > comboTimeLimit && comboAttack != 0)
        {
            comboAttack = 0;
            time = 0;
        }
        else
        {
            time += Time.fixedDeltaTime;
        }
    }


    /*private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Triggered");
        if (collision.gameObject.tag == enemigoTag)
        {
            time = 0;
            comboAttack++;
            CameraShakeCM.Temblor(1, 1, .5f);
            colliderAttack.SetActive(false);
            //Llamar al enemigo y causarle daño;
            if (ataque == tipoAtaque.rapido)
            {
                //Causar daño X
                
            }
            else if (ataque == tipoAtaque.fuerte)
            {
                //Causar daño Y
            }
        }
    }*/
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackChecker.position, radiusAttackCheck);
    }

    private void CheckAttack()
    {
        Collider2D[] enemigos = Physics2D.OverlapCircleAll(attackChecker.position, radiusAttackCheck, enemigoLayer);
        if (enemigos.Length != 0)
        {
            CameraShakeCM.Temblor(.5f, 2, .2f);
        }
    }

    private void AtaqueRapido()
    {
        ataque = TipoAtaque.rapido;
        CheckAttack();
        switch (comboAttack)
        {
            case 1:
                {
                    return;
                }
            case 2:
                {
                    return;
                }
            case 3:
                {
                    comboAttack = 0;
                    return;
                }
        }
    }
    private void ataqueFuerte()
    {
        ataque = TipoAtaque.fuerte;
        CheckAttack();
        switch (comboAttack)
        {
            case 1:
                {
                    return;
                }
            case 2:
                {
                    return;
                }
            case 3:
                {
                    comboAttack = 0;
                    return;
                }
        }
    }
}
