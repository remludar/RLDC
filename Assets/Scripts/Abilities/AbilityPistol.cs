using UnityEngine;

public class AbilityPistol : MonoBehaviour
{
    GameObject playerGO;
    Player playerScript;

    void Awake()
    {
        playerGO = GameObject.FindGameObjectWithTag("Player");
        playerScript = playerGO.GetComponent<Player>();
    }

    void Start()
    {
        var gunGO = Instantiate(Resources.Load("Prefabs/Pistol"), playerScript.GetRightHand().transform.position, Quaternion.identity) as GameObject;
        gunGO.transform.parent = playerScript.GetRightHand().transform;
        gunGO.name = "Pistol";
        playerScript.AddInventoryItem("gun", gunGO);
    }
}