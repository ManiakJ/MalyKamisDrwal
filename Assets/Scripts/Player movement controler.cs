using Unity.VisualScripting;
using UnityEngine;
using System.Collections;
using UnityEngine.InputSystem;
public class PlayerMovement : MonoBehaviour
{
    public float speed = 2f;
    public float fallLimit = -5f;
   private int stamina_amount = 100;
   private bool sprint = false;
   private bool mouse = false;
   private bool stop_regeneration = false;

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
        if (Keyboard.current == null) return;
        float horizontal = 0f;
        float vertical = 0f;


 
        // WSAD
        if (Keyboard.current.aKey.isPressed)
            horizontal = -1f;
        if (Keyboard.current.dKey.isPressed)
            horizontal = 1f;
        if (Keyboard.current.wKey.isPressed)
            vertical = 1f;
        if (Keyboard.current.sKey.isPressed)
            vertical = -1f;
        // Przyspieszanie 
        if (Keyboard.current.leftShiftKey.wasPressedThisFrame)
        {
            sprint = true;
            stop_regeneration = true;
            if (stop_regeneration)
            {
                StartCoroutine(stamina());
            }
        }
        //przywrócenie normalnej prędkości
        if (Keyboard.current.leftShiftKey.wasReleasedThisFrame)
        {
            sprint = false;
            stop_regeneration = false;
            StartCoroutine(Stamina_recovery(false));
        }
        //uderzenie lewym przyciskiem myszy
        if (Mouse.current.leftButton.wasPressedThisFrame) 
        {
            mouse = true;
            stop_regeneration = true;
            if (stop_regeneration)
            {
                StartCoroutine(stamina());
            }
        }

        if (Mouse.current.leftButton.wasReleasedThisFrame)
        {
            mouse = false;
            stop_regeneration = false;
            if (!stop_regeneration)
            {
                StartCoroutine(Stamina_recovery(true));
            }

            
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

        if (sprint == true || mouse == true)
        {
            while (stamina_amount > 0 && (sprint == true || mouse == true))
            {
                Debug.Log("ilosc" + stamina_amount);
                if (sprint)
                {
                    stamina_amount = stamina_amount - 1;
                    speed = 10f;
                    Debug.Log("ilosc" + stamina_amount);
                }
                if (mouse == true && stamina_amount > 20 )
                {
                    stamina_amount = stamina_amount - 20;
                    Debug.Log("ilość po uderzeniu" + stamina_amount);
                }
                yield return new WaitForSeconds(0.1f);
            }
        }

            speed = 2f;
        
    }
    IEnumerator Stamina_recovery(bool attack) // pętla sie wykonuje nawet jak w międzyczasie kliknę lewym przyciskiem myszy
    {
        if (attack == true)
        {
            Debug.Log("Po uderzeniu");
            yield return new WaitForSeconds(5f);
            attack = false;
        }

       // if (sprint == false && attack == false)
       // {
            while (stamina_amount < 100 && sprint == false && mouse == false) 
            {
                Debug.Log("Odnawianie");
                Debug.Log("ilosc" + stamina_amount);
                stamina_amount = stamina_amount + 1;
                Debug.Log("ilosc" + stamina_amount);
                yield return new WaitForSeconds(0.1f);
            }
       // }
        
    }
}