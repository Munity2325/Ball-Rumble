using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TournamentController : MonoBehaviour {
    [SerializeField] private uint totalPlayers = 2;
    [SerializeField] private uint updatesPerRequest = 3;
    private uint updatesCount = 0;
    private TournamentPlayer[] teams = null;

    void Awake() {
        teams = new TournamentPlayer[totalPlayers];
        createTestPlayers();
    }

    void FixedUpdate() {
        updatesCount++;
        if (updatesCount % updatesPerRequest != 0) return;
        updatesCount = 0;
        foreach(TournamentPlayer player in teams) {
            // TODO: Собрать все данные об объектах на поле и отправить запрос игроку
            player.requestActions(/* данные */);
        }

    }

    private void createTestPlayers() {
        for (uint i = 0; i < totalPlayers; i++) {
            string playerName = string.Format("'Team_{0:d}'", i);
            string playerHost = "localhost";
            uint playerPort = 8200 + i;
            TeamClient client = new(i, playerName, playerHost, playerPort);
            teams[i] = new TournamentPlayer(client);
        }
    }
}
