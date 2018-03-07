using UnityEngine;

using System.Collections.Generic;

[RequireComponent(typeof(CharacterController))]
public class Player : MonoBehaviour
{
    CharacterController cc;
    CapsuleCollider capsuleCollider;
    GameObject rightHandGO;
    Dictionary<string, GameObject> items;

    //Motion
    float horizontal, vertical;
    Vector3 motion;

    //Jump
    float verticalVelocity;
    float jumpAcceleration;

    //Snipe
    bool isRMB = false;
    bool canSnipe = true;
    bool isSniping = false;
    int snipeShotCount = 0;
    int maxSnipeShotCount = 10;

    //UI
    public bool isMenuItemOpen;

    //Factors
    public float moveSpeed;
    public float gravityAcceleration;

    //Overrides
    void Start()
    {
        BasicInit();
        ComponentInit();

        DebugStuff();
    }
    void Update()
    {
        HandleInput();
        UpdateSpecialMove();
        UpdateMainMove();
        CalculateMovement();
    }

    void LateUpdate()
    {
        transform.rotation = new Quaternion(0, Camera.main.transform.rotation.y, 0, Camera.main.transform.rotation.w);

        verticalVelocity += gravityAcceleration * Time.deltaTime;
        motion.y = verticalVelocity;
        cc.Move(motion);

        var gunGO = rightHandGO.transform.Find("Pistol(Clone)").gameObject;
        gunGO.transform.rotation = Camera.main.transform.rotation;
        if (cc.isGrounded) verticalVelocity = 0;
    }

    //Helpers
    private void BasicInit()
    {
        moveSpeed = 10f;
        gravityAcceleration = -1.25f;
        jumpAcceleration = 0.4f;
        verticalVelocity = 0.0f;
    }
    private void ComponentInit()
    {
        cc = GetComponent<CharacterController>();
        capsuleCollider = GetComponent<CapsuleCollider>();
        rightHandGO = transform.Find("RightHand").gameObject;
        items = new Dictionary<string, GameObject>();
    }
    private void DebugStuff()
    {
        var gunGO = Instantiate(Resources.Load("Prefabs/Pistol"), transform.Find("RightHand").position, Quaternion.identity) as GameObject;
        gunGO.transform.parent = transform.Find("RightHand");
        items.Add("gun", gunGO);

        Cursor.lockState = CursorLockMode.Locked;
    }
    private void HandleInput()
    {
        
        //Movement
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");

        //Jump
        if (Input.GetKeyDown(KeyCode.Space))
        {
            verticalVelocity += jumpAcceleration;
        }

        //UI
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            isMenuItemOpen = !isMenuItemOpen;
        }
        Cursor.visible = isMenuItemOpen;

        //Snipe
        isRMB = Input.GetMouseButton(1);

    }
    private void CalculateMovement()
    {
        var cameraForward = new Vector3(Camera.main.transform.forward.x, 0, Camera.main.transform.forward.z);
        var cameraRight = Camera.main.transform.right;
        motion = ((cameraRight * horizontal) + (cameraForward * vertical));
        motion *= moveSpeed * Time.deltaTime;
    }
    private void UpdateSpecialMove()
    {
        // Gotta be a better way to do this
        // Basically, you can snipe unti you're out of snipe turns, then you get zoomed out
        // Zooming out resets the snipe turns for now.  
        // Maybe i'll change this to ammo or charges or something you have to build up in the future
        if (isRMB)
            if (canSnipe)
                Snipe(true);
            else
                Snipe(false);
        else
        {
            Snipe(false);
            canSnipe = true;
        }

        if (isSniping)
        {
            if (Input.GetMouseButtonDown(0))
            {
                rightHandGO.transform.Find("Pistol(Clone)").GetComponent<Gun>().Fire();
                snipeShotCount++;

                if (snipeShotCount >= maxSnipeShotCount)
                {
                    canSnipe = false;
                    Snipe(false);
                    snipeShotCount = 0;
                }
            }
        }
    }
    private void UpdateMainMove()
    {
        if (Input.GetMouseButtonDown(0))
        {
            rightHandGO.transform.Find("Pistol(Clone)").GetComponent<Gun>().Fire();
        }
    }
    private void Snipe(bool isSniping)
    {
        Camera.main.GetComponent<MouseOrbitImproved>().Snipe(isSniping);
        cc.enabled = !isSniping;
        this.isSniping = isSniping;
    }

    public GameObject GetItem(string name)
    {
        GameObject result;
        items.TryGetValue("gun", out result);

        return result;
    }
}