
using UnityEngine;
using System.Collections;
using UnityEngine.InputSystem;
using UnityEngine.UI;
public class PlayerMovement : MonoBehaviour
{
    public float PlayerSpeed = 2f;
    public float FallLimit = -5f;
    private float StaminaAmmount = 100;
    private float StaminaMax = 100;
    private bool Sprint = false;
    private bool Mouse = false;
    private bool StopRegeneration = false;
    private bool AttackPouse = false;

    public AudioSource FootstepsAudio;
    private Vector3 StartPosition;
    private Rigidbody rb;

    public Image StaminaBar;

    private void Start()
    {
        StartPosition = transform.position;
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

            Debug.Log("x" + horizontal);
            Debug.Log("y" + vertical);
            if (horizontal != 0 || vertical != 0) { 
            Sprint = true;
            StopRegeneration = true;
            if (StopRegeneration)
            {
                StartCoroutine(stamina());
            }
        }

        }
        //przywrócenie normalnej prędkości
        if (Keyboard.current.leftShiftKey.wasReleasedThisFrame)
        {
            Sprint = false;
            StopRegeneration = false;
            StartCoroutine(StaminaRecovery(false));
        }
        //uderzenie lewym przyciskiem myszy
        if (UnityEngine.InputSystem.Mouse.current.leftButton.wasPressedThisFrame) 
        {
            Mouse = true;
            StopRegeneration = true;
            if (StopRegeneration)
            {
                AttackPouse = true;
                StartCoroutine(stamina());
            }
        }

        if (UnityEngine.InputSystem.Mouse.current.leftButton.wasReleasedThisFrame)
        {
            Mouse = false;
            StopRegeneration = false;
            if (!StopRegeneration)
            {
                StartCoroutine(StaminaRecovery(true));
            }

            
        }
        //obliczanie kierunku i przesuniecia
        Vector3 movement = new Vector3(horizontal, 0f, vertical);
        transform.Translate(movement.normalized * PlayerSpeed * Time.deltaTime, Space.Self);

        if (transform.position.y < FallLimit)
        {
            Respawn();
        }
        if (FootstepsAudio != null)
        {

            if (movement.magnitude > 0f)
            {
                if (!FootstepsAudio.isPlaying)
                    FootstepsAudio.Play();
            }
            else
            {
                if (FootstepsAudio.isPlaying)
                    FootstepsAudio.Stop();
            }
        }
    }
    void Respawn()
    {
        if (FootstepsAudio != null)
        {
            FootstepsAudio.Stop();
        }
        if (rb != null)
        {
            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            rb.position = StartPosition;
        }
        else
        {
            transform.position = StartPosition;
        }
        Debug.Log("Respawn");
    }
    IEnumerator stamina()
    {

        if (Sprint == true || Mouse == true)
        {
            while (StaminaAmmount > 0 && (Sprint == true || Mouse == true))
            {
                Debug.Log("ilosc" + StaminaAmmount);
                if (Sprint)
                {
                    StaminaAmmount = StaminaAmmount - 1;
                    PlayerSpeed = 10f;
                    StaminaBar.fillAmount = StaminaAmmount / StaminaMax;
                    Debug.Log("ilosc" + StaminaAmmount);
                }
                if (Mouse == true && StaminaAmmount > 20 )
                {
                    StaminaAmmount = StaminaAmmount - 20;
                    StaminaBar.fillAmount = StaminaAmmount / StaminaMax; 
                    Debug.Log("ilość po uderzeniu" + StaminaAmmount);
                }
                yield return new WaitForSeconds(0.1f);
            }
        }

            PlayerSpeed = 2f;
        
    }
    IEnumerator StaminaRecovery(bool Attack) // pętla sie wykonuje nawet jak w międzyczasie kliknę lewym przyciskiem myszy
    {
        if (Attack == true)
        {
            Debug.Log("Po uderzeniu");
            yield return new WaitForSeconds(5f);
            Attack = false;
            AttackPouse = false;
        }

       // if (sprint == false && attack == false)
       // {
            while (StaminaAmmount < 100 && Sprint == false && Mouse == false && AttackPouse == false) 
            {
                Debug.Log("Odnawianie");
                Debug.Log("ilosc" + StaminaAmmount);
                StaminaAmmount = StaminaAmmount + 1;
                Debug.Log("ilosc" + StaminaAmmount);
            StaminaBar.fillAmount =  + StaminaAmmount/StaminaMax;
                yield return new WaitForSeconds(0.1f);
            }
       // }
        
    }
}