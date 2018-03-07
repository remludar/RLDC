using UnityEngine;

public class MainManager : MonoBehaviour
{
    GameObject playerGO;
    Transform rightHandTransform;
    Player playerScript;
    void Awake()
    {
        playerGO = GameObject.FindGameObjectWithTag("Player");
        rightHandTransform = playerGO.transform.Find("RightHand");
        playerScript = playerGO.GetComponent<Player>();
        CreateBaseCharacter();
    }

    #region Helpers
    private void CreateBaseCharacter()
    {
        playerGO.AddComponent<AbilityPistol>();
        playerGO.AddComponent<AbilityKick>();
        playerGO.AddComponent<AbilitySnipe>();
        playerGO.AddComponent<AbilityStickAround>();
        playerGO.AddComponent<AbilityBarrelRoll>();
    }
    private void CreatePirateCharacter()
    {
        //Add pirate character things...
    }
    #endregion

}