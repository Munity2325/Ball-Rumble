using System.Collections;
using System.Collections.Generic;
using UnityEditor.Compilation;
using UnityEngine;

public class TournamentPlayer {
    private TeamClient player;
    private UdpClient client;

    public TournamentPlayer(TeamClient player) {
        this.player = player;
        this.client = new UdpClient(player.host, player.port);
    }
    public void requestActions() {
        string data = "Requested - " + info();
        client.sendRequest(UdpClient.Requests.ACTIONS, data, onActionsReceived);
    }

    public string info() {
        string template = "Client {0} '{1}', on {2}:{3}";
        return string.Format(template, player.id, player.name, player.host, player.port);
    }

    private void onActionsReceived(string data) {
        Debug.Log("Received: " + data);
    }
}
