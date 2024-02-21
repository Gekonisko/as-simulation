
using System.Collections;
using System.Collections.Generic;
using Chapter.Singleton;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AgentManager : Singleton<AgentManager>
{
    [SerializeField]
    private Agent _agentPrefab;

    [Header("Properties")]
    [SerializeField]
    private AgentData _agentData;
    public AgentData agentData => GetAgent();
    private uint _freeId = 0;

    [Header("Generator")]

    [Range(3, 5)]
    [SerializeField]
    private int _startAgents;

    [Range(2, 6)]
    [SerializeField]
    private float _timeToGenerate;

    [SerializeField]
    private int _maxAgents = 30;

    private List<Agent> _pooledAgents;
    private int _agentsCount;

    private Scene _contentScene;


    private void Awake()
    {
        base.Awake();
        Events.AgentDeath += OnAgentDeath;
    }

    private void Start()
    {
        CreateObjectPoolOfAgents();

        StartCoroutine(SpawnAgents());
    }

    private AgentData GetAgent()
    {
        var newAgent = _agentData;
        newAgent.name = $"Agent {_freeId++}";
        return newAgent;
    }

    private void OnAgentDeath() => _agentsCount--;

    private IEnumerator SpawnAgents()
    {
        while (true)
        {
            yield return new WaitForSeconds(_timeToGenerate);

            if (_agentsCount < _maxAgents)
            {
                GetPooledObject()?.gameObject.SetActive(true);
                _agentsCount++;
            }
        }
    }

    private void CreateObjectPoolOfAgents()
    {
        _pooledAgents = new List<Agent>();

        for (int i = 0; i < _maxAgents; i++)
        {
            Agent agent = Instantiate(_agentPrefab);
            agent.gameObject.SetActive(i < _startAgents);
            MoveToAgentScene(agent.gameObject);

            _pooledAgents.Add(agent);
        }
        _agentsCount = _startAgents;
    }

    private Agent GetPooledObject()
    {
        foreach (Agent agent in _pooledAgents)
        {
            if (agent.isActiveAndEnabled == false)
                return agent;
        }
        return null;
    }

    private void MoveToAgentScene(GameObject o)
    {
        if (!_contentScene.isLoaded)
        {
            if (Application.isEditor)
            {
                _contentScene = SceneManager.GetSceneByName(name);
                if (!_contentScene.isLoaded)
                {
                    _contentScene = SceneManager.CreateScene(name);
                }
            }
            else
            {
                _contentScene = SceneManager.CreateScene(name);
            }
        }
        SceneManager.MoveGameObjectToScene(o, _contentScene);
    }

    private void OnDestroy()
    {
        Events.AgentDeath -= OnAgentDeath;
        StopAllCoroutines();
    }
}