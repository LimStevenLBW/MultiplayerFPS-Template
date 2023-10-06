using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class Player : NetworkBehaviour
{
    private NetworkVariable<int> team = new NetworkVariable<int>(0);
    private NetworkVariable<int> health = new NetworkVariable<int>(100);
    private NetworkVariable<int> armor = new NetworkVariable<int>(0);

    public override void OnNetworkSpawn()
    {
        if (IsServer)
        {

        }
        else
        {
           
           team.OnValueChanged += OnTeamValueChanged;
           health.OnValueChanged += OnHealthValueChanged;
           armor.OnValueChanged += OnArmorValueChanged;

        }
    }

    void OnTeamValueChanged(int previous, int newValue)
    {

    }
    void OnHealthValueChanged(int previous, int newValue)
    {

    }
    void OnArmorValueChanged(int previous, int newValue)
    {

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
