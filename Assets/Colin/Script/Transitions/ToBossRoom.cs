using UnityEngine;

public class ToBossRoom : MonoBehaviour
{

    [SerializeField] AudioClip bossFight;
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            GameObject.Find("Music").GetComponent<AudioSource>().Stop();
            GameObject.Find("Music").GetComponent<AudioSource>().loop = true;
            GameObject.Find("Music").GetComponent<AudioSource>().PlayOneShot(bossFight, 1f);
            GameObject.FindWithTag("MainCamera").transform.position = new Vector3(187.95f, 9.5f, -10f);
            GameObject.FindWithTag("MainCamera").GetComponent<CameraController>().inControl = false;
            collision.transform.position = new Vector2(178.21f, 5.57f);
        }
    }
}
