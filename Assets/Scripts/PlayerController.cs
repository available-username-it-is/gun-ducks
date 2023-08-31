using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public Transform viewPoint;
    public float mouseSensitivity = 1f;
    private float verticalRotStore;
    private Vector2 mouseInput;

    public float movementSpeed = 5f, runningSpeed = 8f;
    private float currentSpeed;
    private Vector3 moveDir, movement;
    public Camera cam;

    public GunSystem[] allGuns;
    public Image weaponIconObject;
    public Image weaponIcon;
    private int selectedGun;

    [SerializeField] CharacterController characterController;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;

        SwitchGun();
    }

    // Update is called once per frame
    void Update()
    {
        // Camera rotation
        //mouseInput = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y")) * mouseSensitivity;

        //transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y + mouseInput.x, transform.rotation.eulerAngles.z);

        //verticalRotStore += mouseInput.y;
        //verticalRotStore = Mathf.Clamp(verticalRotStore, -60f, 60f);

        //viewPoint.rotation = Quaternion.Euler(-verticalRotStore, viewPoint.rotation.eulerAngles.y, viewPoint.rotation.eulerAngles.z);



        // Allow player to see the mouse cursor
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.lockState = CursorLockMode.None;
        }
        else if (Cursor.lockState == CursorLockMode.None)
        {
            if (Input.GetMouseButton(0))
            {
                Cursor.lockState = CursorLockMode.Locked;
            }
        }



        // Movement and gravity
        moveDir = new Vector3(Input.GetAxisRaw("Horizontal"), 0f, Input.GetAxisRaw("Vertical"));
        if (Input.GetKey(KeyCode.LeftShift))
        {
            currentSpeed = runningSpeed;
        }
        else
        {
            currentSpeed = movementSpeed;
        }

        float yVel = movement.y;
        movement = (transform.forward * moveDir.z + transform.right * moveDir.x).normalized * currentSpeed;
        movement.y = yVel;

        if (characterController.isGrounded)
        {
            movement.y = 0f;
        }

        movement.y += Physics.gravity.y * Time.deltaTime;

        characterController.Move(movement * Time.deltaTime);


        // Switching guns by scrolling the mouse wheel
        if (Input.GetAxisRaw("Mouse ScrollWheel") > 0f)
        {
            selectedGun++;

            if (selectedGun >= allGuns.Length)
            {
                selectedGun = 0;
            }
            SwitchGun();
        }
        else if (Input.GetAxisRaw("Mouse ScrollWheel") < 0f)
        {
            selectedGun--;

            if (selectedGun < 0)
            {
                selectedGun = allGuns.Length - 1;
            }
            SwitchGun();
        }
    }


    private void LateUpdate()
    {
        //cam.transform.position = viewPoint.transform.position;
        //cam.transform.rotation = viewPoint.transform.rotation;
    }

    private void OnTriggerEnter(Collider other)
    {
        int totalAmmo = allGuns[selectedGun].gameObject.GetComponent<GunSystem>().totalAmmo;
        if (other.CompareTag("Ammo Box"))
        {
            Destroy(other.gameObject);
            totalAmmo += allGuns[selectedGun].gameObject.GetComponent<GunSystem>().magazineSize;
            allGuns[selectedGun].gameObject.GetComponent<GunSystem>().totalAmmo = totalAmmo;
            allGuns[selectedGun].gameObject.GetComponent<GunSystem>().UpdateAmmo();
        }
    }

    void SwitchGun()
    {
        foreach (GunSystem gun in allGuns)
        {
            gun.gameObject.SetActive(false);
        }

        allGuns[selectedGun].gameObject.SetActive(true);

        weaponIconObject.sprite = allGuns[selectedGun].gameObject.GetComponent<Image>().sprite;
    }
}
