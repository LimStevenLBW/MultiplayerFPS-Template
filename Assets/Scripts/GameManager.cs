using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class GameManager : NetworkBehaviour
{
    //Players
    private int currentNumberOfPlayers;

    private List<Player> TEAM_1 = new List<Player>();
    private List<Player> TEAM_2 = new List<Player>();

    [SerializeField] private Player playerPrefab;
    //private PlayerBot playerBotPrefab;

    public static GameManager Instance {  get; private set; }

    public override void OnNetworkSpawn()
    {
        if (!IsServer) return;
        // Start listening 

        // Start listening for connection/disconnections
        if (NetworkManager.Singleton)
        {
            NetworkManager.Singleton.OnClientConnectedCallback += NewPlayerConnected;
            NetworkManager.Singleton.OnClientDisconnectCallback += PlayerDisconnected;
        }
        // Call connect for host
        //   NewPlayerConnected(OwnerClientId);
        base.OnNetworkSpawn();
    }

    private void NewPlayerConnected(ulong playerID)
    {
        currentNumberOfPlayers++;
        //Spawning is server authoritative
        if (IsServer)
        {
            if (currentNumberOfPlayers == 1)
            {
                Player playerJoiningTeam1 = Instantiate(playerPrefab);
                playerJoiningTeam1.SetTeamNumber(1);
                playerJoiningTeam1.GetComponent<NetworkObject>().SpawnAsPlayerObject(playerID);

            }
            else if (currentNumberOfPlayers == 2)
            {
                Player playerJoiningTeam2 = Instantiate(playerPrefab);
                playerJoiningTeam2.SetTeamNumber(2);
                playerJoiningTeam2.GetComponent<NetworkObject>().SpawnAsPlayerObject(playerID);
            }
            else if (currentNumberOfPlayers == 3)
            {
                Player playerJoiningTeam3 = Instantiate(playerPrefab);
                playerJoiningTeam3.SetTeamNumber(2);
                playerJoiningTeam3.GetComponent<NetworkObject>().SpawnAsPlayerObject(playerID);
            }
        }
    }


    public List<Player> GetPlayersFromTeam(int teamNum)
    {
        if (teamNum == 1) return TEAM_1;
        else if (teamNum == 2) return TEAM_2;
        else
        {
            Debug.Log("Invalid Team Number Provided");
            return null;
        }
    }

    private void PlayerDisconnected(ulong playerID)
    {
        Debug.Log("Player Disconnected...");
    }



    private void Awake()
    {
        if(Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
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
