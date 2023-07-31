using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeamClient {
    public uint id;
    public string name;
    public string host;
    public uint port;

    public TeamClient(uint id, string name, string host, uint port) {
        this.id = id;
        this.name = name;
        this.host = host;
        this.port = port;
    }

}
