using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

public class Map {
  public int width { get; private set; }
  public int height { get; private set; }

  private Square[,] map;

  // Public accessor flips coordinates so (0, 0) is the bottom left (to match Unity's coords).
  public Square this[int x, int y] {
    get {
      return map[(width - 1 - y), x];
    }

    set {
      map[(width - 1 - y), x] = value;
    }
  }

  public Square this[Vector3 position] {
    get {
      return this[(int)position.x, (int)position.y];
    }

    set {
      this[(int)position.x, (int)position.y] = value;
    }
  }

  public static IDictionary<Color32, Square> colorToSquareMap = new Dictionary<Color32, Square>() {
    { new Color32(255, 255, 255, 255), Square.EMPTY },
    { new Color32(0, 0, 0, 255), Square.WALL },
    { new Color32(0, 0, 255, 255), Square.BASE },
    { new Color32(255, 0, 0, 255), Square.SPAWNER },
    { new Color32(0, 255, 255, 255), Square.TOWER },
  };

  public Map(Texture2D image) {
    width = image.width;
    height = image.height;
    map = new Square[width, height];
    for (int x = 0; x < width; ++x) {
      for (int y = 0; y < height; ++y) {
        Color32 color = (Color32)image.GetPixel(x, y);
        if (!colorToSquareMap.ContainsKey(color)) {
          Debug.Log(string.Format("Invalid color {0} at ({1}, {2}).", color, x, y));
        }
        this[x, y] = colorToSquareMap[color];
      }
    }
  }
}

public enum Square {
  EMPTY,
  WALL,
  BASE,
  SPAWNER,
  ENEMY,
  TOWER,
}