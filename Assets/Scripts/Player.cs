using UnityEngine;

using System.Collections.Generic;

[RequireComponent(typeof(CharacterController))]
public class Player : MonoBehaviour
{
    CharacterController cc;
    CapsuleCollider capsuleCollider;
    GameObject rightHandGO;
    Dictionary<string, GameObject> inventory = new Dictionary<string, GameObject>();

    //Motion
    Vector3 motion;

    //Jump
    float verticalVelocity;
    float jumpAcceleration;

    //UI
    public bool isMenuItemOpen;

    //Factors
    public float moveSpeed;
    public float gravityAcceleration;

    #region Overrides
    void Awake()
    {
        cc = GetComponent<CharacterController>();
        capsuleCollider = GetComponent<CapsuleCollider>();
        rightHandGO = transform.Find("RightHand").gameObject;
    }
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;

        moveSpeed = 10f;
        gravityAcceleration = -1.25f;
        jumpAcceleration = 0.4f;
        verticalVelocity = 0.0f;
    }
    void Update()
    {
        HandleInput();
        CalculateMovement();
    }
    void LateUpdate()
    {
        transform.rotation = new Quaternion(0, Camera.main.transform.rotation.y, 0, Camera.main.transform.rotation.w);

        verticalVelocity += gravityAcceleration * Time.deltaTime;
        motion.y = verticalVelocity;
        cc.Move(motion);
        
        if (cc.isGrounded) verticalVelocity = 0;
    }
    #endregion

    #region Helpers
    private void HandleInput()
    {
        InputManager.Update();

        //Jump
        if (InputManager.isSpace)
            verticalVelocity += jumpAcceleration;

        //UI
        if (InputManager.isEsc)
            isMenuItemOpen = !isMenuItemOpen;

        Cursor.visible = isMenuItemOpen;

    }
    private void CalculateMovement()
    {
        var cameraForward = new Vector3(Camera.main.transform.forward.x, 0, Camera.main.transform.forward.z);
        var cameraRight = Camera.main.transform.right;
        motion = ((cameraRight * InputManager.horizontal) + (cameraForward * InputManager.vertical));
        motion *= moveSpeed * Time.deltaTime;
    }
    #endregion

    #region GETTERS
    public GameObject GetItem(string name)
    {
        GameObject result;
        inventory.TryGetValue("gun", out result);

        return result;
    }
    public GameObject GetRightHand()
    {
        return rightHandGO;
    }
    #endregion

    #region SETTERS
    public void SetCharacterController(bool isEnabled)
    {
        cc.enabled = isEnabled;
    }
    public void AddInventoryItem(string name, GameObject item)
    {
        inventory.Add(name, item);
    }
    #endregion
}