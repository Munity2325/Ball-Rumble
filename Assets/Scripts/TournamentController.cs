using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TournamentController : MonoBehaviour {
    public GameObject controller;
    [SerializeField] private uint totalPlayers = 2;
    [SerializeField] private uint updatesPerRequest = 3;
    private uint updatesCount = 0;
    private TournamentPlayer[] teams = null;

    void Awake() {
        for (int i = 0; i < totalPlayers; i++) {
            teams[i] = controller.AddComponent<TournamentPlayer>();
        }
    }

    void FixedUpdate() {
        updatesCount++;
        if (updatesCount % updatesPerRequest != 0) return;

    }
}
