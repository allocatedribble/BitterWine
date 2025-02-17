using Unity.NetCode;
using UnityEngine;

public class NetworkBootstrap: ClientServerBootstrap
{
    public override bool Initialize(string defaultWorldName)
    {
        Application.runInBackground = true;
        AutoConnectPort = 6132;
        return base.Initialize(defaultWorldName);
    }
}
