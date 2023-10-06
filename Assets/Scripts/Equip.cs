using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public abstract class Equip : NetworkBehaviour
{
    public virtual void PrimaryUse(){

    }

    public virtual void SecondaryUse(){
        
    }

    public virtual void Reload()
    {

    }
}
