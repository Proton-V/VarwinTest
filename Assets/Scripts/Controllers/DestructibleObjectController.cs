using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class DestructibleObjectManager
{
    public Slider HealthSlider;
}

[System.Serializable]
public class DestructibleObjectController : MonoBehaviour
{
    [Range(1, 20)]
    public int Health = 3;
    [HideInInspector]
    public CanvasManager CanvasManager;
    [HideInInspector]
    public Animator Animator;
    public DestructibleObjectManager Manager;
    public virtual void Start()
    {
        CanvasManager = CanvasManager.Instance;
        Animator = GetComponent<Animator>();
        Manager.HealthSlider.maxValue = Health;
        Manager.HealthSlider.value = Health;
    }

    public IEnumerator Destroy()
    {
        yield return new WaitForSeconds(1);
        if (Animator.GetAnimatorTransitionInfo(0).duration == Animator.GetAnimatorTransitionInfo(0).normalizedTime)
        {
            Destroy(gameObject);
        }
        else
            yield return new WaitForSeconds(1);
    }
    public virtual void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Bullet")
        {
            other.GetComponent<BulletController>().Hide();
            Health--;
            Manager.HealthSlider.value--;
            if (Health == 0)
            {
                Animator.SetTrigger("Destroy");
                StartCoroutine(Destroy());
            }
        }
    }
}
