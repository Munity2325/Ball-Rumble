using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UdpClient {
    public enum Requests {
        READY,
        ACTIONS,
        GAMEOVER
    }
    private string host;
    private uint port;

    public UdpClient(string host, uint port) {
        this.host = host;
        this.port = port;
    }

    public delegate void ResponseHandler(string data);
    public void sendRequest(Requests type, string data, ResponseHandler responseHandler) {
        // Заглушка: притворяемся, что данные ушли и пришли в том же виде
        responseHandler(data);
    }

}
