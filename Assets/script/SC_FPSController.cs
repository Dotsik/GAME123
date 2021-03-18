﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;


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
    bool isDead;
    //получение урона
    bool damaged;
    public Color flashColour = new Color(1f, 0f, 0f, 0.1f);
    public Image damageImage;
    public float flashSpeed = 5f;
    //хп
    public int startingHealth = 100;
    public int currentHealth;
    public Text hp;
    public Slider healthSlider;
    // пауза
    public float timer;
    public bool ispuse;
    public bool guipuse;
    // сохранение
    public Text save;
    int intToSave;
    float floatToSave;
    bool boolToSave;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        //пауза
        Time.timeScale = timer;
        if (Input.GetKeyDown(KeyCode.Escape) && ispuse == false) // поставить на паузу
        {
            Cursor.lockState = CursorLockMode.None;
            ispuse = true;
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && ispuse == true) // убрать паузу
        {
            Cursor.lockState = CursorLockMode.Locked;
            ispuse = false;
        }
        if (ispuse == true) //если на паузе
        {
            Cursor.lockState = CursorLockMode.None;
            timer = 0;
            guipuse = true;

        }
        else if (ispuse == false) // если не на паузе
        {
            Cursor.lockState = CursorLockMode.Locked;
            save.text = "";
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
            if (Input.GetMouseButtonDown(1))
                TakeDamage(10);
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
        hp.text = currentHealth.ToString();
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
    void SaveGame()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath
          + "/MySaveData.dat");
        SaveData data = new SaveData();
        data.savedInt = intToSave;
        data.savedFloat = floatToSave;
        data.savedBool = boolToSave;
        bf.Serialize(file, data);
        file.Close();
        Debug.Log("Game data saved!");
    }
    void LoadGame()
    {
        if (File.Exists(Application.persistentDataPath
          + "/MySaveData.dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file =
              File.Open(Application.persistentDataPath
              + "/MySaveData.dat", FileMode.Open);
            SaveData data = (SaveData)bf.Deserialize(file);
            file.Close();
            intToSave = data.savedInt;
            floatToSave = data.savedFloat;
            boolToSave = data.savedBool;
            Debug.Log("Game data loaded!");
        }
        else
            Debug.LogError("There is no save data!");
    }
    void ResetData()
    {
        if (File.Exists(Application.persistentDataPath
          + "/MySaveData.dat"))
        {
            File.Delete(Application.persistentDataPath
              + "/MySaveData.dat");
            intToSave = 0;
            floatToSave = 0.0f;
            boolToSave = false;
            Debug.Log("Data reset complete!");
        }
        else
            Debug.LogError("No save data to delete.");
    }
    void OnGUI()
    {
        if (guipuse == true)
        {
            Cursor.visible = true;// включаем отображение курсора
            if (GUI.Button(new Rect((float)(Screen.width / 2 - 50), (float)(Screen.height / 2), 150f, 45f), "Зберегти"))
            {
                SaveGame();
                save.text = "Збережено";
                timer = 0;
            }
            if (GUI.Button(new Rect((float)(Screen.width / 2 - 50), (float)(Screen.height / 2) - 50f, 150f, 45f), "Продовжити"))
            {
                ispuse = false;
                timer = 0;
                Cursor.visible = false;
                save.text = "";
            }
            if (GUI.Button(new Rect((float)(Screen.width / 2 - 50), (float)(Screen.height / 2)+50 ,150f, 45f), "Головне меню"))
            {
                ispuse = false;
                timer = 0;
                SaveGame();
                save.text = "";
            }
        }
    }
}
[Serializable]
class SaveData
{
    public int savedInt;
    public float savedFloat;
    public bool savedBool;
}