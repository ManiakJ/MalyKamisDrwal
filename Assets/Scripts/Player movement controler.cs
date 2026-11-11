using Unity.VisualScripting;
using UnityEngine;
using System.Collections;
using UnityEngine.InputSystem;
public class PlayerMovement : MonoBehaviour
{
    public float speed = 2f;
    public float fallLimit = -5f;
   private int stamina_amount = 100;
    bool sprint = false;

    public AudioSource footstepsAudio;
    private Vector3 startPosition;
    private Rigidbody rb;



    private void Start()
    {
        startPosition = transform.position;
        rb = GetComponent<Rigidbody>();

    }


    void Update()
    {
        float horizontal = 0f;
        float vertical = 0f;


        //WSAD
        if (Input.GetKey(KeyCode.A))
            horizontal = -1f;
        if (Input.GetKey(KeyCode.D))
            horizontal = 1f;
        if (Input.GetKey(KeyCode.W))
            vertical = 1f;
        if (Input.GetKey(KeyCode.S))
            vertical = -1f;
        //przyspieszanie
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            // speed = 20f;
            sprint = true;
            StartCoroutine(stamina());
        }
        //przywrócenie normalnej prędkości
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            sprint = false;
            StartCoroutine(Stamina_recovery());
        }

        //obliczanie kierunku i przesuniecia
        Vector3 movement = new Vector3(horizontal, 0f, vertical);
        transform.Translate(movement.normalized * speed * Time.deltaTime, Space.Self);

        if (transform.position.y < fallLimit)
        {
            Respawn();
        }
        if (footstepsAudio != null)
        {

            if (movement.magnitude > 0f)
            {
                if (!footstepsAudio.isPlaying)
                    footstepsAudio.Play();
            }
            else
            {
                if (footstepsAudio.isPlaying)
                    footstepsAudio.Stop();
            }
        }
    }
    void Respawn()
    {
        if (footstepsAudio != null)
        {
            footstepsAudio.Stop();
        }
        if (rb != null)
        {
            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            rb.position = startPosition;
        }
        else
        {
            transform.position = startPosition;
        }
        Debug.Log("Respawn");
    }
    IEnumerator stamina()
    {

        if (sprint == true)
        {
            while (stamina_amount > 0 && sprint == true)
            {
                Debug.Log("ilosc" + stamina_amount);
                stamina_amount = stamina_amount - 1;
                speed = 10f;
                Debug.Log("ilosc" + stamina_amount);
                yield return new WaitForSeconds(0.1f);
            }
        }
        sprint = false;
        speed = 2f;
    }
    IEnumerator Stamina_recovery()
    {
        if (sprint == false)
        {
            while (stamina_amount < 100 && sprint == false)
            {
                Debug.Log("Odnawianie");
                Debug.Log("ilosc" + stamina_amount);
                stamina_amount = stamina_amount + 2;
                Debug.Log("ilosc" + stamina_amount);
                yield return new WaitForSeconds(0.1f);
            }
        }

    }
}