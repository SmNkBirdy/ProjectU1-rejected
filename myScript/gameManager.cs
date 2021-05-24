using UnityEngine.SceneManagement;
using UnityEngine;

/*
0 - ничего
1 - платформа
2 - арена
3 - сундук
4 - магазин
5 - босс
6 - спавн
 */

public class gameManager : MonoBehaviour
{
    public GameObject enemy;
    public GameObject arena;
    public GameObject spawnRoom;
    public GameObject bridge;
    public GameObject bossRoom;
    public GameObject chestRoom;
    public GameObject shopRoom;
    public GameObject arenaTunnelV;
    public GameObject arenaTunnelH;
    public GameObject arenaForkV;
    public GameObject arenaForkH;
    public GameObject arenaCrossRoad;
    public GameObject arenaCornerR;
    public GameObject arenaCornerL;

    public int roomsAmount;
    private int currentRoomsAmount;
    public int currentLevel;

    private int[,] map = new int[9, 9];
    private int[,] buildMap = new int[9, 9];
    private GameObject[,] partsMap = new GameObject[9, 9];

    private int[,] eventMap = new int[9, 9];
    gameData gd;
    // 0 - ничего, 1 - враги, 2 - босс


    private void createRoom(int x, int y)
    {
        int i = Random.Range(0, 2);
        if (i > 0)
        {
            currentRoomsAmount++;
        }
        map[x, y] = i;
    }

    private void countRooms()
    {
        int i = 0;
        foreach (int l in map)
        {
            if (l != 0)
            {
                i++;
            }
        }
        currentRoomsAmount = i;
    }

    private Vector2 randomNeighbour()
    {
        int way = Random.Range(1, 5);
        Vector2 vector;
        if (way == 1)
        {
            vector = new Vector2(0, 1);
        }
        else if (way == 2)
        {
            vector = new Vector2(1, 0);
        }
        else if (way == 3)
        {
            vector = new Vector2(-1, 0);
        }
        else
        {
            vector = new Vector2(0, -1);
        }
        return vector;
    }

    private int countNeighbours(int x, int y)
    {
        int neighbours = 0;
        if (x + 1 < 9)
        {
            if (map[x + 1, y] != 0)
            {
                neighbours++;
            }
        }
        if (y + 1 < 9)
        {
            if (map[x, y + 1] != 0)
            {
                neighbours++;
            }
        }
        if (x - 1 > -1)
        {
            if (map[x - 1, y] != 0)
            {
                neighbours++;
            }
        }
        if (y - 1 > -1)
        {
            if (map[x, y - 1] != 0)
            {
                neighbours++;
            }
        }
        return neighbours;
    }

    private int countBuilds(int x, int y)
    {
        int neighbours = 0;
        if (x + 1 < 9)
        {
            if (buildMap[x + 1, y] != 0)
            {
                neighbours++;
            }
        }
        if (y + 1 < 9)
        {
            if (buildMap[x, y + 1] != 0)
            {
                neighbours++;
            }
        }
        if (x - 1 > -1)
        {
            if (buildMap[x - 1, y] != 0)
            {
                neighbours++;
            }
        }
        if (y - 1 > -1)
        {
            if (buildMap[x, y - 1] != 0)
            {
                neighbours++;
            }
        }
        return neighbours;
    }

    private bool checkBossRoom()
    {
        bool bossExist = false;
        foreach (int i in map)
        {
            if (i == 5)
            {
                bossExist = true;
            }
        }
        return (map[5, 5] != 5 && map[6, 5] != 5 && map[5, 6] != 5 && map[4, 5] != 5 && map[5, 4] != 5 && map[6, 6] != 5 && map[6, 4] != 5 && map[4, 4] != 5 && map[4, 6] != 5 && bossExist);
    }

    private void generateMap()
    {
        for (int i = 0; i < 9; i++)
        {
            for (int l = 0; l < 9; l++)
            {
                map[i, l] = 0;
            }
        }
        map[5, 5] = 1;
        currentRoomsAmount = 0;
        while (currentRoomsAmount < roomsAmount - 1)
        {
            for (int i = 0; i < 9; i++)
            {
                for (int l = 0; l < 9; l++)
                {
                    if (currentRoomsAmount < roomsAmount && map[i, l] != 0)
                    {
                        int x = -1;
                        int y = -1;
                        while (x < 0 || x > 8 || y < 0 || y > 8)
                        {
                            Vector2 way = randomNeighbour();
                            x = i + (int)way.x;
                            y = l + (int)way.y;
                        }
                        if (map[x, y] == 0)
                        {
                            if (countNeighbours(x, y) < 3)
                            {
                                createRoom(x, y);
                            }
                        }
                    }
                }
            }
            countRooms();
        }
    }

    private void divideMap()
    {
        int bossInTheHouse = 0;
        int shopInTheHouse = 0;
        for (int i = 0; i < 9; i++)
        {
            for (int l = 0; l < 9; l++)
            {
                if (map[i, l] == 1)
                {
                    if (i == 5 && l == 5)
                    {
                        map[i, l] = 6;
                    }
                    else if (countNeighbours(i, l) > 1)
                    {
                        map[i, l] = 2;
                    }
                    else if (bossInTheHouse == 0)
                    {
                        map[i, l] = 5;
                        bossInTheHouse++;
                    }
                    else if (shopInTheHouse == 0)
                    {
                        map[i, l] = Random.Range(3, 5);
                        if (map[i, l] == 4)
                        {
                            shopInTheHouse++;
                        }
                    }
                    else
                    {
                        map[i, l] = 2;
                    }
                }
            }
        }
    }

    private GameObject getRigthPart(int partID)
    {
        GameObject rightMapPart;
        if (partID == 2)
        {
            rightMapPart = arena;
        }
        else if (partID == 3)
        {
            rightMapPart = chestRoom;
        }
        else if (partID == 4)
        {
            rightMapPart = shopRoom;
        }
        else if (partID == 5)
        {
            rightMapPart = bossRoom;
        }
        else if (partID == 6)
        {
            rightMapPart = spawnRoom;
        }
        else
        {
            rightMapPart = null;
        }
        return rightMapPart;
    }

    private Vector2 findPlaceToBuild(int x, int y)
    {
        Vector2 place = new Vector2(0, 0);
        if (x - 1 > -1)
        {
            if (buildMap[x - 1, y] == 0 && map[x - 1, y] != 0)
            {
                place = new Vector2(x - 1, y);
            }
        }
        if (y - 1 > -1)
        {
            if (buildMap[x, y - 1] == 0 && map[x, y - 1] != 0)
            {
                place = new Vector2(x, y - 1);
            }
        }
        if (x + 1 < 9)
        {
            if (buildMap[x + 1, y] == 0 && map[x + 1, y] != 0)
            {
                place = new Vector2(x + 1, y);
            }
        }
        if (y + 1 < 9)
        {
            if (buildMap[x, y + 1] == 0 && map[x, y + 1] != 0)
            {
                place = new Vector2(x, y + 1);
            }
        }

        return place;
    }

    private Vector3 getBuildLocation(int x, int y, int bx, int by)
    {
        Vector3 bridgeSize = bridge.GetComponent<Renderer>().bounds.size;
        Vector3 startRoomSize = getRigthPart(map[x, y]).GetComponent<Renderer>().bounds.size;
        Vector3 endRoomSize = getRigthPart(map[bx, by]).GetComponent<Renderer>().bounds.size;
        Vector3 buildLocation = partsMap[x, y].transform.position;

        if (y < by)
        {
            buildLocation += new Vector3(0, 0, startRoomSize.z / 2 + bridgeSize.z + endRoomSize.z / 2);
        }
        else if (x < bx)
        {
            buildLocation += new Vector3(startRoomSize.x / 2 + bridgeSize.x + endRoomSize.x / 2, 0, 0);
        }
        else if (y > by)
        {
            buildLocation -= new Vector3(0, 0, startRoomSize.z / 2 + bridgeSize.z + endRoomSize.z / 2);
        }
        else
        {
            buildLocation -= new Vector3(startRoomSize.x / 2 + bridgeSize.x + endRoomSize.x / 2, 0, 0);
        }

        return buildLocation;
    }

    private void connectBuild(int x, int y)
    {
        Vector3 bridgeSize = bridge.GetComponent<Renderer>().bounds.size;
        Vector3 roomSize = getRigthPart(map[x, y]).GetComponent<Renderer>().bounds.size;
        Vector3 position = partsMap[x, y].transform.position;
        if (y + 1 < 9)
        {
            if (buildMap[x, y + 1] != 0)
            {
                Instantiate(bridge, position + new Vector3(0, 0, roomSize.z / 2 + bridgeSize.z / 2), Quaternion.identity);
            }
        }

        if (x + 1 < 9)
        {
            if (buildMap[x + 1, y] != 0)
            {
                Instantiate(bridge, position + new Vector3(roomSize.x / 2 + bridgeSize.x / 2, 0, 0), Quaternion.identity);
            }
        }

        if (y - 1 > -1)
        {
            if (buildMap[x, y - 1] != 0)
            {
                Instantiate(bridge, position - new Vector3(0, 0, roomSize.z / 2 + bridgeSize.z / 2), Quaternion.identity);
            }
        }

        if (x - 1 > -1)
        {
            if (buildMap[x - 1, y] != 0)
            {
                Instantiate(bridge, position - new Vector3(roomSize.x / 2 + bridgeSize.x / 2, 0, 0), Quaternion.identity);
            }
        }
    }

    private void buildNeighbour(int x, int y)
    {
        if (buildMap[x, y] == 2)
        {
            Vector2 buildPlace = findPlaceToBuild(x, y);
            int bx = (int)buildPlace.x;
            int by = (int)buildPlace.y;

            Vector3 buildLocation = getBuildLocation(x, y, bx, by);
            partsMap[bx, by] = Instantiate(getRigthPart(map[bx, by]), buildLocation, Quaternion.identity);
            connectBuild(bx, by);

            if (countBuilds(bx, by) == countNeighbours(bx, by))
            {
                buildMap[bx, by] = 1;
            }
            else
            {
                buildMap[bx, by] = 2;
            }

            globalCount();
        }
    }

    private void globalCount()
    {
        for (int i = 0; i < 9; i++)
        {
            for (int l = 0; l < 9; l++)
            {
                if (buildMap[i, l] != 0)
                {
                    if (countBuilds(i, l) == countNeighbours(i, l))
                    {
                        buildMap[i, l] = 1;
                    }
                    else
                    {
                        buildMap[i, l] = 2;
                    }
                }
            }
        }
    }

    private int countUnfinished()
    {
        int unfinished = 0;
        foreach (var i in buildMap)
        {
            if (i == 2)
            {
                unfinished++;
            }
        }
        return unfinished;
    }

    //Instantiate(getRigthPart(map[i, l]), new Vector3(x * mapPartSize.x, 0, y * mapPartSize.z), Quaternion.identity);
    //Vector3 mapPartSize = arena.GetComponent<Renderer>().bounds.size;
    private void buildMapParts()
    {
        buildMap[5, 5] = 2;
        partsMap[5, 5] = Instantiate(getRigthPart(map[5, 5]), new Vector3(0, 0, 0), Quaternion.identity);

        while (countUnfinished() > 0)
        {
            for (int i = 0; i < 9; i++)
            {
                for (int l = 0; l < 9; l++)
                {
                    if (map[i, l] != 0)
                    {
                        buildNeighbour(i, l);
                    }
                }
            }
        }
    }

    private int getDirection(int x, int y)
    {
        int dir = 0;
        if (y + 1 < 9 && dir == 0)
        {
            if (map[x, y + 1] != 0)
            {
                dir = 1;
            }
        }
        if (x + 1 < 9 && dir == 0)
        {
            if (map[x + 1, y] != 0)
            {
                dir = 2;
            }
        }
        if (y - 1 > -1 && dir == 0)
        {
            if (map[x, y - 1] != 0)
            {
                dir = 3;
            }
        }
        if (x - 1 > -1 && dir == 0)
        {
            if (map[x - 1, y] != 0)
            {
                dir = 4;
            }
        }
        return dir;
    }

    private int getSeconsDirection(int x, int y)
    {
        int dir = 0;
        int amount = 0;
        if (y + 1 < 9 && amount < 2)
        {
            if (map[x, y + 1] != 0)
            {
                dir = 1;
                amount++;
            }
        }
        if (x + 1 < 9 && amount < 2)
        {
            if (map[x + 1, y] != 0)
            {
                dir = 2;
                amount++;
            }
        }
        if (y - 1 > -1 && amount < 2)
        {
            if (map[x, y - 1] != 0)
            {
                dir = 3;
                amount++;
            }
        }
        if (x - 1 > -1 && amount < 2)
        {
            if (map[x - 1, y] != 0)
            {
                dir = 4;
            }
        }
        return dir;
    }

    private int getEmpty(int x, int y)
    {
        int dir = 0;
        if (y + 1 < 9 && dir == 0)
        {
            if (map[x, y + 1] == 0)
            {
                dir = 1;
            }
        }
        if (x + 1 < 9 && dir == 0)
        {
            if (map[x + 1, y] == 0)
            {
                dir = 2;
            }
        }
        if (y - 1 > -1 && dir == 0)
        {
            if (map[x, y - 1] == 0)
            {
                dir = 3;
            }
        }
        if (x - 1 > -1 && dir == 0)
        {
            if (map[x - 1, y] == 0)
            {
                dir = 4;
            }
        }
        return dir;
    }

    private void rebuildMap()
    {
        for (int i = 0; i < 9; i++)
        {
            for (int l = 0; l < 9; l++)
            {
                if (map[i, l] != 0 && map[i, l] != 6)
                {
                    Vector3 position = partsMap[i, l].transform.position;
                    Destroy(partsMap[i, l]);
                    int neighbours = countNeighbours(i, l);
                    if (neighbours == 1)
                    {
                        int dir = getDirection(i, l);
                        GameObject obj = Instantiate(getRigthPart(map[i, l]), position, Quaternion.identity);
                        partsMap[i, l] = obj;
                        Vector3 size = obj.GetComponent<Renderer>().bounds.size;
                        obj.transform.Rotate(new Vector3(0, 90 * (dir - 1), 0));
                        if (dir == 2 || dir == 4)
                        {
                            obj.transform.position += obj.transform.forward * (size.x - size.z) / 2;
                        }
                    }
                    else if (neighbours == 2)
                    {
                        int dir = getDirection(i, l);
                        int secDir = getSeconsDirection(i, l);

                        if (dir == 1 && secDir == 3 || dir == 2 && secDir == 4)
                        {
                            if (dir == 1)
                            {
                                partsMap[i, l] = Instantiate(arenaTunnelV, position, Quaternion.identity);
                            }
                            else
                            {
                                partsMap[i, l] = Instantiate(arenaTunnelH, position, Quaternion.identity);
                            }
                        }
                        else
                        {
                            if (dir == 1 && secDir == 2 || dir == 3 && secDir == 4)
                            {
                                GameObject obj = Instantiate(arenaCornerR, position, Quaternion.identity);
                                partsMap[i, l] = obj;
                                obj.transform.Rotate(new Vector3(0, 90 * (secDir - 2), 0));
                            }
                            else
                            {
                                GameObject obj = Instantiate(arenaCornerL, position, Quaternion.identity);
                                partsMap[i, l] = obj;
                                obj.transform.Rotate(new Vector3(0, 180 * (dir - 1), 0));
                            }
                        }

                    }
                    else if (neighbours == 3)
                    {
                        int empty = getEmpty(i, l);
                        if (empty == 2 || empty == 4)
                        {
                            GameObject obj = Instantiate(arenaForkV, position, Quaternion.identity);
                            partsMap[i, l] = obj;
                            obj.transform.Rotate(new Vector3(0, 90 * (empty - 2), 0));
                        }
                        else
                        {
                            GameObject obj = Instantiate(arenaForkH, position, Quaternion.identity);
                            partsMap[i, l] = obj;
                            obj.transform.Rotate(new Vector3(0, 90 * (empty - 1), 0));
                        }
                    }
                    else
                    {
                        partsMap[i, l] = Instantiate(arenaCrossRoad, position, Quaternion.identity);
                    }
                }
            }
        }
    }





    private void Awake()
    {
        gd = gameObject.GetComponent<gameData>();
        gd.loadGame();
        //генерация карты
        roomsAmount = Random.Range(0, 2) + 5 + (int)(currentLevel * 2.6);
        bool mapGenerated = true;
        while (!checkBossRoom() || mapGenerated)
        {
            generateMap();
            divideMap();
            mapGenerated = false;
        }

        //постройка карты
        buildMapParts();
        rebuildMap();

        //постройка карты эвентов

        for (int i = 0; i < 9; i++)
        {
            for (int l = 0; l < 9; l++)
            {
                if (map[i,l] == 2) 
                {
                    eventMap[i, l] = 1;
                }
                else if (map[i, l] == 5)
                {
                    eventMap[i, l] = 2;
                }
            }
        }
    }

    //Игровой процесс


    private bool fight = false;

    public void startFight(Vector3 triggerPosition)
    {
        GameObject currentRoom = null;
        Vector3 roomPosition = new Vector3(triggerPosition.x, 0, triggerPosition.z);
        int x = 0;
        int y = 0;
        for (int i = 0; i < 9; i++)
        {
            for (int l = 0; l < 9; l++)
            {
                if (map[i, l] != 0)
                {
                    if (roomPosition == partsMap[i, l].transform.position)
                    {
                        currentRoom = partsMap[i, l];
                        x = i;
                        y = l;
                    }
                }
            }
        }
        if (!fight && eventMap[x,y] != 0)
        {
            fight = true;
            eventMap[x, y] = 0;
            var children = currentRoom.GetComponentsInChildren<Transform>();
            foreach (var item in children)
            {
                if (item.tag == "Gate")
                {
                    item.GetComponent<gatesScript>().close();
                }
            }
            int enemyAmount = Random.Range(2, 6);
            for (int i = 0; i < enemyAmount; i++)
            {
                Instantiate(enemy,roomPosition + new Vector3(Random.Range(-10, 10), 3, Random.Range(-10, 10)),Quaternion.identity);
            }
        }
    }

    public bool checkFight()
    {
        bool check = false;
        if(GameObject.FindGameObjectsWithTag("Enemy").Length > 0)
        {
            check = true;
        }
        return check;
    }
    public void globalUnlock()
    {
        GameObject[] children = GameObject.FindGameObjectsWithTag("Gate");
        foreach (var item in children)
        {
            item.GetComponent<gatesScript>().open();
        }
    }

    public void nextLevel()
    {
        SceneManager.LoadScene("demo2");
        gd.gems++;
        gd.saveGame();
    }

    public void restartSession()
    {
        SceneManager.LoadScene("demo");
        gd.saveGame();
    }

    public bool checkBossEvent()
    {
        bool check = false;
        foreach (var item in eventMap)
        {
            if (item == 2)
            {
                check = true;
            }
        }
        return check; 
    }

    private GameObject findBossRoom()
    {
        GameObject bossRoom = null;
        for (int i = 0; i < 9; i++)
        {
            for (int l = 0; l < 9; l++)
            {
                if (map[i,l] == 5)
                {
                    bossRoom = partsMap[i, l];
                }
            }
        }

        return bossRoom;
    }

    private void Update()
    {
        if (fight)
        {
            if (!checkFight())
            {
                globalUnlock();
                fight = false;
                Debug.Log(checkBossEvent());
                if (!checkBossEvent())
                {
                    var bro = findBossRoom().GetComponentsInChildren<Transform>(true);
                    foreach (var item in bro)
                    {
                        if (item.tag == "End")
                        {
                            item.gameObject.SetActive(true);
                        }
                    }
                }
            }
        }
    }

}