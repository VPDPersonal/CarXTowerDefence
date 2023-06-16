using System;
using MVP;
using Towers;
using Monsters;
using Spawners;
using UnityEngine;
using Towers.Guided;
using Towers.Cannon;
using Transformation;
using Towers.Detectors;
using Monsters.Spawners;
using Monsters.Factories;
using Projectiles.Cannon;
using Projectiles.Guided;
using Transformation.Targets;
using Transformation.Movements;
using Projectiles.Cannon.Factories;
using Projectiles.Guided.Factories;

public class Bootstrap : MonoBehaviour
{
    #region Inspector Fields
    [Header("Monster")]
    [SerializeField] private MonsterData _monsterData;
    [SerializeField] private MonsterView _monsterPrefab;
    [SerializeField] private MonoTarget _targetForMonsters;

    [Header("Spawners")]
    [SerializeField] private SpawnerData _spawnerData;
    [SerializeField] private MonoMonsterSpawner _monsterSpawner;

    [Header("Cannon Tower")]
    [SerializeField] private CannonData _cannonData;
    [SerializeField] private CannonView _cannonPrefab;
    [SerializeField] private CannonTowerData _cannonTowerData; 
    [SerializeField] private CannonTowerView _cannonTowerView;
    
    [Header("Guided Tower")]
    [SerializeField] private GuidedData _guidedData;
    [SerializeField] private GuidedView _guidedPrefab;
    [SerializeField] private GuidedTowerData _guidedTowerData;
    [SerializeField] private TowerView _guidedTowerView;
    #endregion

    private MonsterManager _monsterManager;
    
    private void Awake()
    {
        _monsterManager = new MonsterManager();
        
        var monsterFactory = InitializeMonsterFactory();
        InitializeSpawner(monsterFactory);
        
        var guidedFactory = new GuidedFactory(_guidedData, _guidedPrefab);
        var cannonFactory = new CannonFactory(_cannonData, _cannonPrefab);

        InitializeGuidedTower(guidedFactory);
        InitializeCannonTower(cannonFactory);
    }

    private MonsterFactory InitializeMonsterFactory()
    {
        var monsterMovement = new DirectTargetMovement(_targetForMonsters);
        return new MonsterFactory(monsterMovement, _monsterPrefab, _monsterData);
    }

    private void InitializeSpawner(MonsterFactory monsterFactory)
    {
        var monsterSpawner = new MonsterSpawner(_monsterSpawner.transform.position, monsterFactory);
        _monsterSpawner.Constructor(_spawnerData.RandomStartTime, _spawnerData.MinIntervalTime, _spawnerData.MaxIntervalTime, monsterSpawner);
    }

    #region Initialize Tower Methods
    private void InitializeGuidedTower(GuidedFactory guidedFactory)
    {
        var detector = GetMonsterDetector(_guidedTowerView, _monsterManager, _guidedTowerData.Radius);
        
        var tower = new GuidedTower(
            _guidedTowerView.transform,
            detector.model, 
            _guidedTowerView.ShootPosition,
            _guidedTowerData,
            guidedFactory);
        
        var guidedTowerPresenter = new GuidedTowerPresenter(tower, _guidedTowerView, detector.presenter);
        World.Bind(tower, _guidedTowerView, guidedTowerPresenter);
    }

    private void InitializeCannonTower(CannonFactory cannonFactory)
    {
        var detector = GetMonsterDetector(_cannonTowerView, _monsterManager, _cannonTowerData.Radius);
        
        var tower = new CannonTower(
            _cannonTowerView.transform,
            detector.model, 
            _cannonTowerView.ShootPosition,
            _cannonTowerData,
            cannonFactory);
        
        var cannonTowerPresenter = new CannonTowerPresenter(tower, _cannonTowerView, detector.presenter);
        World.Bind(tower, _cannonTowerView, cannonTowerPresenter);
    }
    

    private static (MonsterDetector model, MonsterDetectorPresenter presenter) GetMonsterDetector(TowerView towerView, MonsterManager monsterManager, float radius)
    {
        var detectorView = towerView.Detector;
        var detector = new MonsterDetector(radius);
        var detectorPresenter = new MonsterDetectorPresenter(detector, detectorView, monsterManager);
        return (detector, detectorPresenter);
    }
    #endregion
}