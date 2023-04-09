using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionManager : MonoBehaviour
{
    public GameObject door;
    int kartIndex = 0;
    public static CollisionManager instance;
    [SerializeField] private ParticleSystem boomParticle, winParticle;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("door"))
        {
            door = other.gameObject;
        }
        else if (other.gameObject.CompareTag("akademikParca"))
        {
            SoundManager.instance.PlaySoundEffects(SoundManager.AudioCallers.item);
            boomParticle.transform.position = other.transform.position;
            boomParticle.Play();
            Destroy(other.gameObject);
            UIManager.instance.AkademikParcaAnim();
        }
        else if (other.gameObject.CompareTag("kart"))
        {
            SoundManager.instance.PlaySoundEffects(SoundManager.AudioCallers.item);
            boomParticle.transform.position = other.transform.position;
            boomParticle.Play();
            Destroy(other.gameObject);
            UIManager.instance.KartAnim(kartIndex, other.gameObject.GetComponent<PassCard>().password);
            kartIndex++;
        }
        else if (other.gameObject.CompareTag("pc") && GameManager.instance.PcActive)
        {
            GameManager.instance.Win();
            winParticle.transform.position = other.transform.position;
            winParticle.Play();
            GetComponent<Movement>().enabled = false;
            GetComponent<Animator>().SetBool("run", false);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("door"))
        {
            door = null;
        }
    }
}
