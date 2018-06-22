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

        CurrentSocket.On("actor_join_room", OnActorJoinRoom);
        CurrentSocket.On("actor_leave_room", OnActorLeaveRoom);
 
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

    protected void OnActorJoinRoom(Socket socket, Packet packet, params object[] args)
    {
        BroadcastEvent("Actor has joined the room");

        JSONNode data = (JSONNode)args[0];
    }

    protected void OnActorLeaveRoom(Socket socket, Packet packet, params object[] args)
    {
        BroadcastEvent("Actor has left the room");

        JSONNode data = (JSONNode)args[0];
    }

    protected void OnMovement(Socket socket, Packet packet, params object[] args)
    {
        //BroadcastEvent("Movement occured");

        JSONNode data = (JSONNode)args[0];
    }

    #endregion

    #region Emittions

    public void EmitLoadedScene()
    {
        BroadcastEvent("Emitted : LoadedScene");
        JSONNode node = new JSONClass();

        CurrentSocket.Emit("entered_room", node);
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
