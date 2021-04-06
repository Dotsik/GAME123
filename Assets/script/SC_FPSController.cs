using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine.SceneManagement;

[Serializable]
class SC_FPSController : MonoBehaviour
{
    //движение
    public float walkingSpeed = 7.5f;
    public float runningSpeed = 11.5f;
    public float gravity = 20.0f;
    public Camera playerCamera;
    public float jumpSpeed = 8.0f;
    public float lookSpeed = 2.0f;
    public float lookXLimit = 45.0f;
    SC_FPSController playerMovement;
    CharacterController characterController;
    Vector3 moveDirection = Vector3.zero;
    float rotationX = 0;
    [HideInInspector]
    public bool canMove = true;
    //смерть
    bool isDead = false;
    //получение урона
    bool damaged;
    public Color flashColour = new Color(1f, 0f, 0f, 0.1f);
    public Image damageImage;
    public float flashSpeed = 5f;
    //хп
    public int startingHealth = 100;
    public int currentHealth;
    public Slider healthSlider;
    // пауза
    public float timer;
    public bool ispuse = false;
    public bool guipuse = false;
    //настройки
    public Text setting_text;
    public Text ff1;
    //курсор
    public Image cursore;
    public Text damage_text;
    //босс
    public Slider boss_hp;
    public Text boss_name;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        Cursor.visible = false;
        guipuse = true;
        setting_text.enabled = false;
        ff1.enabled = false;
        setting_text.enabled = true;
        cursore.enabled = true;
        damage_text.enabled = true;
        boss_name.enabled = false;
        boss_hp.gameObject.SetActive(false);
        
    }

    void Update()
    {
        //пауза
        Time.timeScale = timer;
        if (Input.GetKeyDown(KeyCode.Escape) && ispuse == false) // поставить на паузу
        {
            ff1.enabled = true;
            cursore.enabled = false;
            damage_text.enabled = false;
            guipuse = true;
            ispuse = true;
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && ispuse == true) // убрать паузу
        {
            cursore.enabled = true;
            damage_text.enabled = true;
            ff1.enabled = false;
            guipuse = false;
            ispuse = false;
        }
        if (ispuse == true) //если на паузе
        {
            Cursor.lockState = CursorLockMode.None;
            timer = 0;
        }
        if (ispuse == false) // если не на паузе
        {
                if (Input.GetKeyDown(KeyCode.F1))
                {
                    if (setting_text.enabled == true)
                    {
                        setting_text.enabled = false;
                    }
                    else { setting_text.enabled = true; }
                }
                Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            timer = 1f;
            guipuse = false;
            //движение
            Vector3 forward = transform.TransformDirection(Vector3.forward);
            Vector3 right = transform.TransformDirection(Vector3.right);
            bool isRunning = Input.GetKey(KeyCode.LeftShift);
            float curSpeedX = canMove ? (isRunning ? runningSpeed : walkingSpeed) * Input.GetAxis("Vertical") : 0;
            float curSpeedY = canMove ? (isRunning ? runningSpeed : walkingSpeed) * Input.GetAxis("Horizontal") : 0;
            float movementDirectionY = moveDirection.y;
            moveDirection = (forward * curSpeedX) + (right * curSpeedY);

            //прыжок
            if (Input.GetButton("Jump") && canMove && characterController.isGrounded)
            {
                moveDirection.y = jumpSpeed;
            }
            else
            {
                moveDirection.y = movementDirectionY;
            }
            if (!characterController.isGrounded)
            {
                moveDirection.y -= gravity * Time.deltaTime;
            }
            characterController.Move(moveDirection * Time.deltaTime);
            if (canMove)
            {
                rotationX += -Input.GetAxis("Mouse Y") * lookSpeed;
                rotationX = Mathf.Clamp(rotationX, -lookXLimit, lookXLimit);
                playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
                transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * lookSpeed, 0);
            }
            //получение урона
            if (damaged)
            {
                damageImage.color = flashColour;
            }
            else
            {
                damageImage.color = Color.Lerp(damageImage.color, Color.clear, flashSpeed * Time.deltaTime);
            }
            damaged = false;
        }
    }
    void Awake()
    {
        //начальное хп
        playerMovement = GetComponent<SC_FPSController>();
        currentHealth = startingHealth;
    } 
    public void TakeDamage(int amount)
    {
        //изменение хп
        damaged = true;
        currentHealth -= amount;
        healthSlider.value = currentHealth;
        if (currentHealth <= 0 && !isDead) //если умер
        {
            Death();
        }
    }
    void Death() //после смерти
    {
        isDead = true;
        playerMovement.enabled = false;
    }
    void OnGUI()
    {
        if (isDead == true)
        {
            ff1.enabled = true;
            cursore.enabled = false;
            damage_text.enabled = false;
            guipuse = true;
            ispuse = true;
            Cursor.visible = true;
            if (GUI.Button(new Rect((float)(Screen.width / 2 - 50), (float)(Screen.height / 2) - 25, 150f, 45f), "Почати нову гру"))
            {
                SceneManager.LoadScene("main");
            }
            if (GUI.Button(new Rect((float)(Screen.width / 2 - 50), (float)(Screen.height / 2) + 25, 150f, 45f), "Головне меню"))
            {
                SceneManager.LoadScene("main_menu");
            }
        }
        else
        {
            if (guipuse == true)
            {
                Cursor.visible = true;// включаем отображение курсора
                if (GUI.Button(new Rect((float)(Screen.width / 2 - 50), (float)(Screen.height / 2) - 25, 150f, 45f), "Продовжити"))
                {
                    damage_text.enabled = true;
                    cursore.enabled = true;
                    ff1.enabled = false;
                    ispuse = false;
                    timer = 0;
                    Cursor.visible = false;
                }
                if (GUI.Button(new Rect((float)(Screen.width / 2 - 50), (float)(Screen.height / 2) + 25, 150f, 45f), "Головне меню"))
                {
                    SceneManager.LoadScene("main_menu");
                }
            }
            else
            {
                ff1.enabled = false;
                ispuse = false;
                timer = 1f;
                Cursor.visible = false;
            }
        }
    }
}