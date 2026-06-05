using UnityEngine;
using System.Diagnostics;
using System.Threading;
using System;
using System.Collections.Generic;

public class mainfactory : MonoBehaviour
{
    public const byte factory_type_storage = 8;
    public struct factoryNode
    {
        public int id;
        public int type;
        public bool powered;
        public float consumptionRate;
        public int[] outputs;
        public float[] productionRate;
        public int[] connectedIds;
        public int[] connectedOutputs;
    }
    public struct powerNode
    {
        public int id;
        public int[] connectedNodes;
        public bool[] powered;
        public float[] consumptionRates; // negative for power input
        public float inflow ;
        public float outflow;
    }

    [SerializeField]
    float maxTickTimer = 0;
    [SerializeField]
    float mspt = 0;
    [SerializeField]
    factoryNode[]   groundNodes;
    [SerializeField]
    Dictionary<int,int> groundNodeIndexes;//id to index
    [SerializeField]
    powerNode[]     powerNodes;
    public float tickTimer = 0;

    bool runningFactory = false;

    public int tps;
    Stopwatch Stopwatch;
    public bool paused = false;

    Thread factoryThread;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Thread factoryThread = new Thread(updateFactory);
        maxTickTimer = 1 / tps;
    }
    void sortPowerNodes()
    {
        //rearranges power nodes so they power unique nodes first
        //also connects nodes within line of sight
    }
    void updateFactory()
    {
        Stopwatch.Reset();
        Stopwatch.Start();
        runningFactory = true;
        for (int i = 0; i < powerNodes.Length; i++)
        {

        }
        for (int i = 0; i < groundNodes.Length; i++) 
        {
            if (!groundNodes[i].powered)
            {
                break;
            }
            switch(groundNodes[i].type)
            {

            }
        }

        Stopwatch.Stop();
        mspt = Stopwatch.ElapsedMilliseconds / 1000;
        runningFactory = false;
    }
    public void addNewGroundNode(factoryNode groundNode)
    {
        factoryNode[] newNodes = new factoryNode[groundNodes.Length + 1];
        Array.Copy(groundNodes, newNodes, groundNodes.Length);
        groundNodes.CopyTo(newNodes, 0);
        newNodes[newNodes.Length - 1] = groundNode;
        groundNodes = newNodes;
    }
    public void addNewPowerNode(powerNode pwrnode)
    {
        powerNode[] newNodes = new powerNode[powerNodes.Length + 1];
        Array.Copy(powerNodes, newNodes, powerNodes.Length);
        newNodes[newNodes.Length - 1] = pwrnode;
        powerNodes = newNodes;
        sortPowerNodes();
    }
    // Update is called once per frame
    void Update()
    {
        if (paused)
        {
            return;
        }
        tickTimer += Time.deltaTime;
        if (tickTimer < maxTickTimer)
        {
            return;
        }
        tickTimer = 0;
        if (!runningFactory)
        {
            factoryThread.Start();
        }
    }
}
