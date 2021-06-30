using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class LevelElement {

    public string character;

    public GameObject prefab;
}

public class LevelBuilder : MonoBehaviour {
    public int currentLevel;
    public List<LevelElement> elements;
    private Level level;

    GameObject GetPrefab(char c) {
        LevelElement levelElement = elements.Find(le => le.character == c.ToString());
        if (levelElement != null) {
            return levelElement.prefab;
        }
        else {
            return null;
        }
    }

    public void NextLevel() {
        currentLevel++;
        if (currentLevel >= GetComponent<Levels>().levels.Count) {
            currentLevel = 0;
        }
    }

    public void Build() {
        level = GetComponent<Levels>().levels[currentLevel];
        int startX = -level.width / 2;
        int x = startX;
        int y = -level.height / 2;
        foreach (var row in level.rows) {
            foreach (var c in row) {
    //             if (c == '3') {
    //                 GameObject prefabM = GetPrefab('2');
    //               GameObject prefabE = GetPrefab('1');
    //                if (prefabM != null && prefabE != null) {
    //                  Instantiate(prefabM, new Vector3(x, y, 0), Quaternion.identity);
    //                Instantiate(prefabE, new Vector3(x, y, 0), Quaternion.identity);
    //          }
    //    }
                 if (c == '5') {
                    GameObject prefabM = GetPrefab('5');
                    GameObject prefabE = GetPrefab('0');
                    if (prefabM != null && prefabE != null) {
                        Instantiate(prefabM, new Vector3(x, y, 0), Quaternion.identity);
                        Instantiate(prefabE, new Vector3(x, y, 0), Quaternion.identity);
                    }
                }
                else if (c == '2') {
                    GameObject prefabM = GetPrefab('2');
                    GameObject prefabE = GetPrefab('0');
                    if (prefabM != null && prefabE != null) {
                        Instantiate(prefabM, new Vector3(x, y, 0), Quaternion.identity);
                        Instantiate(prefabE, new Vector3(x, y, 0), Quaternion.identity);
                    }
                }
                else if (c == '4') {
                    GameObject prefabM = GetPrefab('4');
                    GameObject prefabE = GetPrefab('0');
                    if (prefabM != null && prefabE != null) {
                        Instantiate(prefabM, new Vector3(x, y, 0), Quaternion.identity);
                        Instantiate(prefabE, new Vector3(x, y, 0), Quaternion.identity);
                    }
                }
                else {
                    GameObject prefab = GetPrefab(c);
                    if (prefab != null) {
                        Instantiate(prefab, new Vector3(x, y, 0), Quaternion.identity);
                    }
                }
                x++;
            }
            y++;
            x = startX;
        }
    }

}
