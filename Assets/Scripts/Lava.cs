using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lava : MonoBehaviour
{
    public int damage = 1;
    public Material laserRedMaterial;
    public Material brightOrangeMaterial;

    private Renderer renderer;
    private Coroutine damageCoroutine;

    // Start is called before the first frame update
    void Start()
    {
        renderer = GetComponent<Renderer>();
        StartCoroutine(SwitchMaterial());
    }

    // Update is called once per frame
    void Update()
    {

    }

    private IEnumerator SwitchMaterial()
    {
        while (true)
        {
            renderer.material = laserRedMaterial;
            yield return new WaitForSeconds(2);
            renderer.material = brightOrangeMaterial;
            yield return new WaitForSeconds(2);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (damageCoroutine == null)
            {
                damageCoroutine = StartCoroutine(DamagePlayerContinuously());
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (damageCoroutine != null)
            {
                StopCoroutine(damageCoroutine);
                damageCoroutine = null;
            }
        }
    }

    private IEnumerator DamagePlayerContinuously()
    {
        while (true)
        {
            PlayerHealthController.instance.DamagePlayer(damage);
            yield return new WaitForSeconds(1);
        }
    }
}


