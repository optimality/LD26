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

  public GameObject GUI;

  public float selectDistance;
  public LayerMask selectionMask;

  public int baseHP;

  public int basePoints;
  public int towerPoints;

  public TextAsset level1;

  private Map map;
  private IDictionary<Square, GameObject> prefabMap;

  [HideInInspector]
  public int score;
  [HideInInspector]
  public int enemies;
  [HideInInspector]
  public int towers;
  public Vector3[,] directionToMove;
  private Vector3 basePosition;

  private bool started;

  public void DamageBase() {
    baseHP--;
  }

  public void RemoveEnemy() {
    enemies--;
  }

  void Start() {
    started = false;
    prefabMap = new Dictionary<Square, GameObject>() {
      { Square.EMPTY,   null },
      { Square.WALL,    wallPrefab },
      { Square.BASE,    basePrefab },
      { Square.SPAWNER, spawnerPrefab },
      { Square.ENEMY,   null },
      { Square.TOWER,   towerPrefab },
    };

    XmlSerializer serializer = new XmlSerializer(typeof(Map));
    using (TextReader textReader = new StringReader(level1.text)) {
      map = (Map)serializer.Deserialize(textReader);
    }

    directionToMove = new Vector3[map.width, map.height];

    BuildMap();
    ShortestPaths();
  }

  void Update() {
    if (Input.GetButtonDown("Click")) {
      if (!started) {
        started = true;
        foreach (Transform t in transform) {
          t.gameObject.SetActive(true);
        }
        GUI.GetComponent<GUIManager>().StartGame();
      }
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
      foreach (Transform t in transform) {
        t.gameObject.SetActive(false);
      }
      GUI.GetComponent<GUIManager>().LoseGame();
    } else if(enemies == 0) {
      foreach (Transform t in transform) {
        t.gameObject.SetActive(false);
      }
      GUI.GetComponent<GUIManager>().WinGame();
    }
    CalculateScore();
  }

  void BuildMap() {
    towers = 0;
    enemies = 0;
    Vector3 position = Vector3.zero;
    for (position.x = 0; position.x < map.width; ++position.x) {
      for (position.y = 0; position.y < map.height; ++position.y) {
        Square type = map[position];
        // Save the base location for later.
        if (type == Square.BASE) {
          basePosition = position;
        } else if (type == Square.TOWER) {
          towers++;
        } else if (type == Square.SPAWNER) {
          enemies += spawnerPrefab.GetComponent<Spawner>().numEnemies;
        }
        GameObject prefab = prefabMap[type];
        if (prefab != null) {
          GameObject obj = (GameObject)Instantiate(
            prefab,
            position,
            prefab.transform.localRotation);
          obj.transform.parent = transform;
          obj.SetActive(false);
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
    shortest[(int)basePosition.x, (int)basePosition.y] = 0;
    ShortestPathsRecursive(basePosition, shortest);
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

  void CalculateScore() {
    score = baseHP * basePoints + towers * towerPoints;
  }
}
