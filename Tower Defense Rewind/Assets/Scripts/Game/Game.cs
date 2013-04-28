using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

public class Game : MonoBehaviour {
  public GameObject wallPrefab;
  public GameObject basePrefab;
  public GameObject spawnerPrefab;
  public GameObject towerPrefab;

  public GameObject background;

  public GUIManager guiManager;

  public AudioSource explosion;
  public AudioSource towerRemove;

  public float selectDistance;
  public LayerMask selectionMask;

  public int startingBaseHP;

  public int basePoints;
  public int towerPoints;

  public float timeBetweenWaves;

  public Texture2D[] levels;
  public int level;
  public int[] waves;

  public Map map;
  private List<GameObject> levelObjects;
  private IDictionary<Square, GameObject> prefabMap;

  [HideInInspector]
  public int score;
  [HideInInspector]
  public int enemies;
  [HideInInspector]
  public int towers;
  [HideInInspector]
  public float waveTimer;
  [HideInInspector]
  public int? lowestScore;
  [HideInInspector]
  public int wave;
  [HideInInspector]
  public bool newLowestScore;
  [HideInInspector]
  public int baseHP;
  [HideInInspector]
  public Vector3[,] directionToMove;

  private Spawner spawner;
  private GameObject @base;

  public enum Mode {
    START,
    LEVEL_SELECT,
    NEXT_WAVE,
    PLAY,
    WIN,
    LOSE,
  }

  [HideInInspector]
  public Mode mode;

  public void DamageBase() {
    baseHP--;
  }

  public void RemoveEnemy() {
    explosion.Play();
    enemies--;
  }

  private string LowestScoreString() {
    return string.Format("score{0}", level);
  }

  void Start() {
    prefabMap = new Dictionary<Square, GameObject>() {
      { Square.EMPTY,   null },
      { Square.WALL,    wallPrefab },
      { Square.BASE,    basePrefab },
      { Square.SPAWNER, spawnerPrefab },
      { Square.ENEMY,   null },
      { Square.TOWER,   towerPrefab },
    };
    mode = Mode.START;
    guiManager.StartGame();
    Clear();
  }

  public void LoadLevel(int level) {
    Debug.Log(string.Format("Loading level {0}", level));
    this.level = level;
    mode = Mode.NEXT_WAVE;
    guiManager.NextWave();
    background.SetActive(true);

    map = new Map(levels[level - 1]);

    directionToMove = new Vector3[map.width, map.height];

    BuildMap();
    ShortestPaths();

    wave = waves.Length;
    score = 0;
    baseHP = startingBaseHP;
    waveTimer = timeBetweenWaves;
    if (PlayerPrefs.HasKey(LowestScoreString())) {
      lowestScore = PlayerPrefs.GetInt(LowestScoreString());
    } else {
      lowestScore = null;
    }
    newLowestScore = false;
  }

  void Clear() {
    if (levelObjects != null) {
      foreach (GameObject obj in levelObjects) {
        Destroy(obj);
      }
    }
    levelObjects = new List<GameObject>();
    background.SetActive(false);
  }

  public int WaveScore() {
    return baseHP * basePoints + towers * towerPoints;
  }

  void Update() {
    if (Input.GetButtonDown("Clear Low Score")) {
      PlayerPrefs.DeleteKey(LowestScoreString());
      lowestScore = null;
    }
    switch (mode) {
    case Mode.START:
      UpdateStart();
      break;

    case Mode.LEVEL_SELECT:
      UpdateLevelSelect();
      break;

    case Mode.NEXT_WAVE:
      UpdateNextWave();
      break;

    case Mode.PLAY:
      UpdatePlay();
      break;

    case Mode.LOSE:
      UpdateLose();
      break;

    case Mode.WIN:
      UpdateWin();
      break;
    }
  }

  void UpdateStart() {
    if (Input.GetButtonDown("Click")) {
      mode = Mode.LEVEL_SELECT;
      guiManager.SelectLevel();
    }
  }

  void UpdateLevelSelect() {
    // Waiting on callback from a level button.
  }

  void UpdateNextWave() {
    waveTimer -= Time.deltaTime;
    if (waveTimer > 0 && !Input.GetButtonDown("Click")) {
      return;
    }

    guiManager.PlayGame();
    mode = Mode.PLAY;
    waveTimer = timeBetweenWaves;
    spawner.numEnemies = waves[wave - 1];
    enemies = spawner.numEnemies;
  }

  void UpdatePlay() {
    if (Input.GetButtonDown("Click")) {
      RaycastHit hit;
      Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
      if (Physics.Raycast(ray, out hit, selectDistance, selectionMask)) {
        Tower tower = hit.collider.GetComponent<Tower>();
        map[tower.transform.position] = Square.EMPTY;
        ShortestPaths();
        tower.Remove();
        towers--;
      }
    }
    if (baseHP <= 0) {
      mode = Mode.LOSE;
      guiManager.LoseGame();
      score += WaveScore();
    } else if (enemies == 0) {
      score += WaveScore();
      --wave;
      if (wave == 0) {
        guiManager.WinGame();
        mode = Mode.WIN;
        if (!lowestScore.HasValue || (lowestScore.HasValue && score < lowestScore.Value)) {
          newLowestScore = true;
          lowestScore = score;
          PlayerPrefs.SetInt(LowestScoreString(), lowestScore.Value);
        } else {
          newLowestScore = false;
        }
      } else {
        guiManager.NextWave();
        mode = Mode.NEXT_WAVE;
      }
    }
  }

  void UpdateLose() {
    if (Input.GetButtonDown("Click")) {
      Clear();
      mode = Mode.LEVEL_SELECT;
      guiManager.SelectLevel();
    }
  }

  void UpdateWin() {
    if (Input.GetButtonDown("Click")) {
      Clear();
      mode = Mode.LEVEL_SELECT;
      guiManager.SelectLevel();
    }
  }

  void BuildMap() {
    towers = 0;
    Vector3 position = Vector3.zero;
    for (position.x = 0; position.x < map.width; ++position.x) {
      for (position.y = 0; position.y < map.height; ++position.y) {
        Square type = map[position];
        GameObject prefab = prefabMap[type];
        if (prefab != null) {
          GameObject obj = (GameObject)Instantiate(
            prefab,
            position,
            prefab.transform.localRotation);
          obj.transform.parent = transform;
          if (type == Square.SPAWNER) {
            spawner = obj.GetComponent<Spawner>();
          } else if (type == Square.BASE) {
            @base = obj;
          } else if (type == Square.TOWER) {
            towers++;
          }
          levelObjects.Add(obj);
        }
      }
    }
  }

  void ShortestPaths() {
    int[,] shortest = new int[map.width, map.height];
    for (int x = 0; x < shortest.GetLength(0); ++x) {
      for (int y = 0; y < shortest.GetLength(1); ++y) {
        shortest[x, y] = 1000;
      }
    }
    shortest[(int)@base.transform.position.x, (int)@base.transform.position.y] = 0;
    ShortestPathsRecursive(@base.transform.position, shortest);
  }

  void ShortestPathsRecursive(Vector3 target, int[,] shortest) {
    int myShortest = shortest[(int)target.x, (int)target.y];
    foreach (Vector3 direction in new Vector3[] {
        Vector3.down, Vector3.left, Vector3.up, Vector3.right, }) {
      Vector3 check = target + direction;
      if (check.x < 0 || check.y < 0 || check.x >= shortest.GetLength(0)
          || check.y >= shortest.GetLength(1)) {
        continue;
      }
      if (map[check] == Square.EMPTY || map[check] == Square.SPAWNER) {
        if (shortest[(int)check.x, (int)check.y] > myShortest + 1) {
          shortest[(int)check.x, (int)check.y] = myShortest + 1;
          directionToMove[(int)check.x, (int)check.y] = -direction;
          ShortestPathsRecursive(check, shortest);
        }
      }
    }
  }
}