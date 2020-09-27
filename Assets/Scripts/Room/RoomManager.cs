using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class RoomManager : MonoBehaviour
{
    #region Fields

    [SerializeField] private Room[] RoomPrefabs;
    [SerializeField] private Room StartingRoom;
    [SerializeField] private GameObject[] _doorsPrefabs;

    private Room[,] spawnedRooms;

    #endregion


    #region UnityMethods

    private IEnumerator Start()
    {
        spawnedRooms = new Room[11, 11];
        spawnedRooms[5, 5] = StartingRoom;

        for (int i = 0; i < 12; i++)
        {
            yield return new WaitForSecondsRealtime(0.5f);

            PlaceOneRoom();
        }
    }

    #endregion


    #region Methods

    private void PlaceOneRoom()
    {
        HashSet<Vector2Int> vacantPlaces = new HashSet<Vector2Int>();
        for (int x = 0; x < spawnedRooms.GetLength(0); x++)
        {
            for (int y = 0; y < spawnedRooms.GetLength(1); y++)
            {
                if (spawnedRooms[x, y] == null) continue;

                int maxX = spawnedRooms.GetLength(0) - 1;
                int maxY = spawnedRooms.GetLength(1) - 1;

                if (x > 0 && spawnedRooms[x - 1, y] == null) vacantPlaces.Add(new Vector2Int(x - 1, y));
                if (y > 0 && spawnedRooms[x, y - 1] == null) vacantPlaces.Add(new Vector2Int(x, y - 1));
                if (x < maxX && spawnedRooms[x + 1, y] == null) vacantPlaces.Add(new Vector2Int(x + 1, y));
                if (y < maxY && spawnedRooms[x, y + 1] == null) vacantPlaces.Add(new Vector2Int(x, y + 1));
            }
        }

        Room newRoom = Instantiate(RoomPrefabs[Random.Range(0, RoomPrefabs.Length)]);

        for (int i = 0; i < 500; i++)
        {
            Vector2Int position = vacantPlaces.ElementAt(Random.Range(0, vacantPlaces.Count));

            if (ConnectToSomething(newRoom, position))
            {
                newRoom.transform.position = new Vector3(position.x - 5, 0, position.y - 5) * 10.2f;
                spawnedRooms[position.x, position.y] = newRoom;
                return;
            }
        }

        Destroy(newRoom.gameObject);
    }

    private bool ConnectToSomething(Room room, Vector2Int p)
    {
        int maxX = spawnedRooms.GetLength(0) - 1;
        int maxY = spawnedRooms.GetLength(1) - 1;

        List<Vector2Int> neighbours = new List<Vector2Int>();

        if (room.roomDoorUp != null && p.y < maxY && spawnedRooms[p.x, p.y + 1]?.roomDoorDown != null) neighbours.Add(Vector2Int.up);
        if (room.roomDoorDown != null && p.y > 0 && spawnedRooms[p.x, p.y - 1]?.roomDoorUp != null) neighbours.Add(Vector2Int.down);
        if (room.roomDoorRight != null && p.x < maxX && spawnedRooms[p.x + 1, p.y]?.roomDoorLeft != null) neighbours.Add(Vector2Int.right);
        if (room.roomDoorLeft != null && p.x > 0 && spawnedRooms[p.x - 1, p.y]?.roomDoorRight != null) neighbours.Add(Vector2Int.left);

        if (neighbours.Count == 0) return false;

        Vector2Int selectedDirection = neighbours[Random.Range(0, neighbours.Count)];
        Room selectedRoom = spawnedRooms[p.x + selectedDirection.x, p.y + selectedDirection.y];

        if (selectedDirection == Vector2Int.up)
        {
            Instantiate(_doorsPrefabs[Random.Range(0, _doorsPrefabs.Length)], room.roomTransformDoorUp);
            Destroy(room.roomDoorUp);
            Destroy(selectedRoom.roomDoorDown);
        }
        else if (selectedDirection == Vector2Int.down)
        {
            Instantiate(_doorsPrefabs[Random.Range(0, _doorsPrefabs.Length)], room.roomTransformDoorDown);
            Destroy(room.roomDoorDown);
            Destroy(selectedRoom.roomDoorUp);
        }
        else if (selectedDirection == Vector2Int.right)
        {
            Instantiate(_doorsPrefabs[Random.Range(0, _doorsPrefabs.Length)], room.roomTransformDoorRight);
            Destroy(room.roomDoorRight);
            Destroy(selectedRoom.roomDoorLeft);
        }
        else if (selectedDirection == Vector2Int.left)
        {
            Instantiate(_doorsPrefabs[Random.Range(0, _doorsPrefabs.Length)], room.roomTransformDoorLeft);
            Destroy(room.roomDoorLeft);
            Destroy(selectedRoom.roomDoorRight);
        }

        return true;
    }

    #endregion

}
