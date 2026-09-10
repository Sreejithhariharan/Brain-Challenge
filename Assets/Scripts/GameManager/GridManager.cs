using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GridManager : MonoBehaviour
{
    [Header("Grid")]
    public Transform gridContainer;
    public GameObject tilePrefab;

    [Header("Tile Images")]
    // 0 = 1
    // 1 = 2
    // ...
    // 9 = 10
    // 10 = X
    public Sprite[] tileImages;

    public List<Tile> spawnedTiles = new List<Tile>();

    private List<TileData> tiles = new List<TileData>();


    // =========================================
    // START
    // =========================================

    void Start()
    {
        CreateTiles();
        Shuffle();
        Spawn();

        StartCoroutine(StartGame());
    }


    // =========================================
    // CREATE TILES
    // =========================================

    void CreateTiles()
    {
        tiles.Clear();

        int numberCount = 0;
        int totalTiles = 0;

        // -------------------------
        // LEVEL SETTINGS
        // -------------------------

        if (GameManager.instance.currentLevel == 1)
        {
            // 8 total
            // Numbers 1-7
            // 1 X

            numberCount = 7;
            totalTiles = 8;
        }
        else if (GameManager.instance.currentLevel == 2)
        {
            // 12 total
            // Numbers 1-8
            // 4 X

            numberCount = 8;
            totalTiles = 12;
        }
        else if (GameManager.instance.currentLevel == 3)
        {
            // 16 total
            // Numbers 1-10
            // 6 X

            numberCount = 10;
            totalTiles = 16;
        }


        // =========================================
        // ADD NUMBER TILES
        // =========================================

        for (int i = 1; i <= numberCount; i++)
        {
            // i - 1 because arrays start from 0
            Sprite numberSprite = tileImages[i - 1];

            tiles.Add(
                new TileData(numberSprite, i)
            );
        }


        // =========================================
        // ADD X TILES
        // =========================================

        int xTiles = totalTiles - numberCount;

        // X image is element 10
        Sprite xSprite = tileImages[10];

        for (int i = 0; i < xTiles; i++)
        {
            tiles.Add(
                new TileData(xSprite, -1)
            );
        }
    }


    // =========================================
    // SHUFFLE
    // =========================================

    void Shuffle()
    {
        for (int i = 0; i < tiles.Count; i++)
        {
            int randomIndex = Random.Range(i, tiles.Count);

            TileData temp = tiles[i];

            tiles[i] = tiles[randomIndex];
            tiles[randomIndex] = temp;
        }
    }


    // =========================================
    // SPAWN
    // =========================================

    void Spawn()
    {
        foreach (TileData data in tiles)
        {
            GameObject obj = Instantiate(
                tilePrefab,
                gridContainer
            );

            Tile tile = obj.GetComponent<Tile>();

            tile.Setup(
                data.image,
                data.number
            );

            spawnedTiles.Add(tile);
        }
    }


    // =========================================
    // START GAME
    // =========================================

    IEnumerator StartGame()
    {
        foreach (Tile tile in spawnedTiles)
        {
            tile.canClick = false;
            tile.ShowTile();
        }

        // Show all tiles for 2 seconds
        yield return new WaitForSeconds(2f);

        foreach (Tile tile in spawnedTiles)
        {
            if (tile != null)
                tile.HideTile();
        }

        // Small delay so the flip animation finishes
        yield return new WaitForSeconds(0.5f);

        foreach (Tile tile in spawnedTiles)
        {
            if (tile != null)
                tile.canClick = true;
        }
    }


    // =========================================
    // LOAD NEXT LEVEL
    // =========================================

    public void LoadNextLevel()
    {
        StartCoroutine(LoadNextLevelRoutine());
    }


    IEnumerator LoadNextLevelRoutine()
    {
        // Stop interaction
        foreach (Tile tile in spawnedTiles)
        {
            if (tile != null)
                tile.canClick = false;
        }


        // Disable old tiles first
        foreach (Tile tile in spawnedTiles)
        {
            if (tile != null)
                tile.gameObject.SetActive(false);
        }

        yield return new WaitForSeconds(0.1f);


        // Destroy old tiles
        foreach (Tile tile in spawnedTiles)
        {
            if (tile != null)
                Destroy(tile.gameObject);
        }

        spawnedTiles.Clear();


        // Create new level
        CreateTiles();

        Shuffle();

        Spawn();


        // Show new level
        yield return StartCoroutine(StartGame());
    }
}


// =========================================
// TILE DATA
// =========================================

[System.Serializable]
public class TileData
{
    public Sprite image;
    public int number;

    public TileData(Sprite img, int num)
    {
        image = img;
        number = num;
    }
}