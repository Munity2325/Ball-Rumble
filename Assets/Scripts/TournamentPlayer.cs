using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TournamentPlayer {
    private TeamClient client;

    public TournamentPlayer(TeamClient client) {
        this.client = client;
    }
    public void request() {
        Debug.Log("Requested - " + info());
    }

    public string info() {
        string template = "Client {0} '{1}', on {2}:{3}";
        return string.Format(template, client.id, client.name, client.host, client.port);
    }
}
