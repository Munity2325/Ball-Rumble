using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;

public class TournamentController : MonoBehaviour {
    [SerializeField] private uint totalTeams = 2;
    [SerializeField] private uint unitsInTeam = 6;
    [SerializeField] private uint updatesPerRequest = 3;
    private uint updatesCount = 0;
    private TournamentPlayer[] teams = null;
    private UnitInfo[] objectsInfo = null;

    void Awake() {
        teams = new TournamentPlayer[totalTeams];
        createTestPlayers();
    }

    void Start() {
        createObjectsInfo();
    }

    void FixedUpdate() {
        updatesCount++;
        if (updatesCount % updatesPerRequest != 0) return;
        updatesCount = 0;
        foreach(TournamentPlayer player in teams) {
            player.requestActions(objectsInfo);
        }

    }

    private void createObjectsInfo() {
        // Длина массива objectsInfo: количество юнитов в каждой команде + мяч + 4 штанги ворот
        objectsInfo = new UnitInfo[totalTeams * unitsInTeam + 1 + 4];
        // Добавляем мяч
        objectsInfo[0] = new UnitInfo(GameObject.FindWithTag("Ball"));
        uint i = 1;
        // Добавляем штанги
        GameObject[] objects = GameObject.FindGameObjectsWithTag("GoalPost");
        foreach (GameObject obj in objects) {
            objectsInfo[i] = new UnitInfo(obj);
            i++;
        }
        // Добавляем юниты команд
        for (uint t=0; t<totalTeams; t++) {
            objects = GameObject.FindGameObjectsWithTag(teams[t].playerName());
            foreach(GameObject obj in objects) {
                objectsInfo[i] = new UnitInfo(obj);
                i++;
            }
        }
    }


    private void createTestPlayers() {
        for (uint i = 0; i < totalTeams; i++) {
            string playerName = (i == 0) ? "RedPlayer" : "BluePlayer";
            string playerHost = "localhost";
            uint playerPort = 8200 + i;
            TeamClient client = new(i, playerName, playerHost, playerPort);
            teams[i] = new TournamentPlayer(client);
        }
    }
}
