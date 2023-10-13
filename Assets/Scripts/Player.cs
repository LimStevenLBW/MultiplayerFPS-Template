using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class Player : NetworkBehaviour
{
    private NetworkVariable<int> team = new NetworkVariable<int>(0);
    private NetworkVariable<int> health = new NetworkVariable<int>(
        100,
        NetworkVariableReadPermission.Everyone,
        NetworkVariableWritePermission.Owner
    );

    private NetworkVariable<int> armor = new NetworkVariable<int>(0,
        NetworkVariableReadPermission.Everyone,
        NetworkVariableWritePermission.Owner
    );

    public PlayerHud hud;

    public override void OnNetworkSpawn()
    {
        if (IsOwner)
        {
            team.OnValueChanged += OnTeamValueChanged;
            health.OnValueChanged += OnHealthValueChanged;
            armor.OnValueChanged += OnArmorValueChanged;

        }
        else
        {
            hud.gameObject.SetActive(false);
        }

       // hud = FindFirstObjectByType<PlayerHud>();
    }

    void OnTeamValueChanged(int previous, int newValue)
    {

    }
    void OnHealthValueChanged(int previous, int newValue)
    {
        hud.UpdateHealthText(newValue);
    }
    void OnArmorValueChanged(int previous, int newValue)
    {

    }
    [ServerRpc(RequireOwnership = false)]
    public void WasHitServerRPC(int teamThatSentTheBullet)
    {
        Debug.Log(OwnerClientId + "; received damage from enemy team:" + teamThatSentTheBullet);
        if(team.Value == teamThatSentTheBullet)
        {
            Debug.Log("friendly fire");
            //Friendly Fire
        }
        else
        {
            ClientRpcParams clientRpcParams = new ClientRpcParams
            {
                Send = new ClientRpcSendParams
                {
                    TargetClientIds = new ulong[] { OwnerClientId }
                }
            };

            SendHealthUpdateClientRPC(10, clientRpcParams);

            //Destroy(gameObject);
            // Can also do GetComponent<NetworkObject>().Despawn(destroy:true);
        }
    }

    [ClientRpc]
    private void SendHealthUpdateClientRPC(int damage, ClientRpcParams clientRpcParams = default)
    {
        health.Value -= damage;
    }
    

    public int GetTeamNumber()
    {
        return team.Value;
    }
    public void SetTeamNumber(int teamNum)
    {
        team.Value = teamNum;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
