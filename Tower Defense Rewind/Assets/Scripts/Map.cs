using System.Xml.Serialization;
using UnityEngine;

public struct Map {
  public int width { get; private set; }
  public int height { get; private set; }

  // Public for serialization; you probably don't actually want to read this.
  [XmlArrayItemAttribute("S")]
  public Square[] map;

  // Public accessor flips coordinates so (0, 0) is the bottom left (to match Unity's coords).
  public Square this[int x, int y] {
    get {
      return map[(width - 1 - y) * height + x];
    }

    set {
      map[(width - 1 - y) * height + x] = value;
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
}

public enum Square {
  [XmlEnum("_")]
  EMPTY,
  [XmlEnum("#")]
  WALL,
  [XmlEnum("B")]
  BASE,
  [XmlEnum("S")]
  SPAWNER,
  [XmlEnum("x")]
  ENEMY,
  [XmlEnum("T")]
  TOWER,
}