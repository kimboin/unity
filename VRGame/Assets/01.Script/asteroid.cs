using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class asteroid : MonoBehaviour
{
    public GameObject ExplosionEffect;

    private void OnTriggerEnter(Collider other)
    {
        StartCoroutine(ExpAsteroid());
    }
    IEnumerator ExpAsteroid() {
        ExplosionEffect.SetActive(true);

        gameObject.GetComponent<MeshRenderer>().enabled = false;
        gameObject.GetComponent<AudioSource>().Play();
        gameObject.GetComponent<SphereCollider>().enabled = false;

        yield return new WaitForSeconds(3);
        Destroy(this.gameObject);

    }
    //private void OnCollisionExit(Collider collision)
    //{
    //    Debug.Log("Exit");
    //}
    //private void OnTriggerEnter(Collider other)
    //{
    //    Debug.Log("Trigger Enter");
    //}
    //private void OnTriggerStay(Collider other)
    //{
    //    Debug.Log("Trigger Stay");
    //}
    //private void OnTriggerExit(Collider other) {
    //    Debug.Log("Trigger Exit");
    //}
}
