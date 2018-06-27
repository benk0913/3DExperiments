using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using BestHTTP.SocketIO;
using SimpleJSON;
using System;

public class SocketClient : MonoBehaviour
{
    private const int BITCH_WAIT_FRAMES = 5;

    #region Config
    public bool DebugMode = false;

    #endregion

    #region Essential

    protected WebSocketConnector webSocketConnector;

    protected Socket CurrentSocket;

    public static SocketClient Instance;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        webSocketConnector = new WebSocketConnector();
    }

    #endregion

    #region Public Methods

    public void ConnectToGame()
    {
        BroadcastEvent("Connecting to server..");
        CurrentSocket = webSocketConnector.connect("id");

        CurrentSocket.On("connect", OnConnect);
        CurrentSocket.On("disconnect", OnDisconnect);
        CurrentSocket.On("error", OnError);

        CurrentSocket.On("event_error", OnEventError);

        CurrentSocket.On("prepare_room", OnPrepareRoom);
        CurrentSocket.On("actor_joined_room", OnActorJoinRoom);
        CurrentSocket.On("actor_left_room", OnActorLeaveRoom);
 
        CurrentSocket.On("movement", OnMovement);
        
    }

    public void Disconnect()
    {
        if (CurrentSocket != null)
        {
            CurrentSocket.Disconnect();
            CurrentSocket.Off();
        }
    }

    void OnApplicationQuit()
    {
        Disconnect();
    }

    #endregion

    #region Callbacks

    private void OnError(Socket socket, Packet packet, object[] args)
    {
        Error error = args[0] as Error;
        BroadcastEvent("On error: " + error);
    }
    
    private void OnEventError(Socket socket, Packet packet, object[] args)
    {
        JSONNode data = (JSONNode)args[0];
        BroadcastEvent("On event error: " + data.AsObject.ToString());
    }

    private void OnDisconnect(Socket socket, Packet packet, object[] args)
    {
        BroadcastEvent("On disconnect");
    }

    protected void OnConnect(Socket socket, Packet packet, params object[] args)
    {
        BroadcastEvent("On connect");
    }

    protected void OnPrepareRoom(Socket socket, Packet packet, params object[] args)
    {
        BroadcastEvent("Loading scene");

        JSONNode data = (JSONNode)args[0];

        string myId = data["id"];
    }

    protected void OnActorJoinRoom(Socket socket, Packet packet, params object[] args)
    {
        BroadcastEvent("Actor has joined the room");

        JSONNode data = (JSONNode)args[0];
        string actorId = data["id"];
    }

    protected void OnActorLeaveRoom(Socket socket, Packet packet, params object[] args)
    {
        BroadcastEvent("Actor has left the room");

        JSONNode data = (JSONNode)args[0];
        string actorId = data["id"];
    }

    protected void OnMovement(Socket socket, Packet packet, params object[] args)
    {
        //BroadcastEvent("Movement occured");

        JSONNode data = (JSONNode)args[0];

        string actorId = data["id"];
        Vector3 pos = new Vector3(data["x"].AsFloat, data["y"].AsFloat, data["z"].AsFloat);
        float angle = data["angle"].AsFloat;
        float velocity = data["velocity"].AsFloat;
    }

    #endregion

    #region Emittions

    public void EmitLoadedScene()
    {
        BroadcastEvent("Emitted : LoadedScene");
        JSONNode node = new JSONClass();

        CurrentSocket.Emit("prepared_room", node);
    }

    public void EmitMovement(Vector3 pos, float rotDegrees, float velocity)
    {
        JSONNode node = new JSONClass();

        node["x"] = pos.x.ToString();
        node["y"] = pos.y.ToString();
        node["z"] = pos.z.ToString();
        node["angle"].AsFloat = rotDegrees;
        node["velocity"].AsFloat = velocity;

        CurrentSocket.Emit("actor_moved", node);
    }

    #endregion

    #region Internal


    protected void BroadcastEvent(string info)
    {
        if (DebugMode)
        {
            Debug.Log(this + " | " + info);
        }
    }


    #endregion
}
