using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class RoomManager : MonoBehaviour
{
    #region Fields

    [SerializeField] private Room[] _roomPrefabs;
    [SerializeField] private Room _startingRoom;
    [SerializeField] private Transform[] _doorPrefab;
    [SerializeField] private Transform _blockedDoorPrefab;
    private Room[,] _spawnedRooms;

    private int _startingRoomX = 5;
    private int _startingRoomY = 5;

    private int _worldSizeX = 11;
    private int _worldSizeY = 11;

    private int _maxRoomsCount = 12;

    private int _maxOpenDoors = 2;
    private bool[] roomDoors;
    
    #endregion


    #region UnityMethods

    private void Start()
    {
        _spawnedRooms = new Room[_worldSizeX, _worldSizeY];
        _spawnedRooms[_startingRoomX, _startingRoomY] = _startingRoom;

        for (int i = 0; i < _maxRoomsCount; i++)
        {
            PlaceOneRoom();
        }
    }

    #endregion


    #region Methods

    private void PlaceOneRoom()
    {
        HashSet<Vector2Int> vacantPlaces = new HashSet<Vector2Int>();
        for (int x = 0; x < _spawnedRooms.GetLength(0); x++)
        {
            for (int y = 0; y < _spawnedRooms.GetLength(1); y++)
            {
                if (_spawnedRooms[x, y] == null)
                {
                    continue;
                }

                int maxX = _spawnedRooms.GetLength(0) - 1;
                int maxY = _spawnedRooms.GetLength(1) - 1;

                if (x > 0 && _spawnedRooms[x - 1, y] == null) vacantPlaces.Add(new Vector2Int(x - 1, y));
                if (y > 0 && _spawnedRooms[x, y - 1] == null) vacantPlaces.Add(new Vector2Int(x, y - 1));
                if (x < maxX && _spawnedRooms[x + 1, y] == null) vacantPlaces.Add(new Vector2Int(x + 1, y));
                if (y < maxY && _spawnedRooms[x, y + 1] == null) vacantPlaces.Add(new Vector2Int(x, y + 1));
            }
        }
        Room newRoom = Instantiate(_roomPrefabs[Random.Range(0, _roomPrefabs.Length)]);

        int limit = 500;
        while (limit-- > 0)
        {
            Vector2Int position = vacantPlaces.ElementAt(Random.Range(0, vacantPlaces.Count));

            CloseRoomDoors(newRoom);
            if (InitRoomDoors(newRoom, position))
            {
                newRoom.transform.position = new Vector3(position.x - _startingRoomX, 0.0f, position.y - _startingRoomY) * (newRoom.RoomRadiusCenter * 2.0f);
                _spawnedRooms[position.x, position.y] = newRoom;
                break;
            }
        }

    }

    private bool InitRoomDoors(Room room, Vector2Int positionRoom)
    {
        int maxX = _spawnedRooms.GetLength(0) - 1;
        int maxY = _spawnedRooms.GetLength(1) - 1;

        List<Vector2Int> neighbours = new List<Vector2Int>();

        if (room.roomDoorUp != null && positionRoom.y < maxY && _spawnedRooms[positionRoom.x, positionRoom.y + 1]?.roomDoorDown != null) neighbours.Add(Vector2Int.up);
        if (room.roomDoorDown != null && positionRoom.y > 0 && _spawnedRooms[positionRoom.x, positionRoom.y - 1]?.roomDoorUp != null) neighbours.Add(Vector2Int.down);
        if (room.roomDoorRight != null && positionRoom.x > 0 && _spawnedRooms[positionRoom.x - 1, positionRoom.y]?.roomDoorLeft != null) neighbours.Add(Vector2Int.right);
        if (room.roomDoorLeft != null && positionRoom.x < maxX && _spawnedRooms[positionRoom.x + 1, positionRoom.y]?.roomDoorRight != null) neighbours.Add(Vector2Int.left);

        if (neighbours.Count == 0) return false;

        Vector2Int selectedDirection = neighbours[Random.Range(0, neighbours.Count)];
        Room selectedRoom = _spawnedRooms[positionRoom.x + selectedDirection.x, positionRoom.y + selectedDirection.y];

        if (selectedDirection == Vector2Int.up)
        {
            if (!room.hasRoomDoorUp)
            {
                Instantiate(_doorPrefab[Random.Range(0, _doorPrefab.Length)], room.roomDoorUp);
                room.hasRoomDoorUp = true;
                selectedRoom.hasRoomDoorDown = true;
            }
        }
        else if (selectedDirection == Vector2Int.down)
        {
            if (!room.hasRoomDoorDown)
            {
                Instantiate(_doorPrefab[Random.Range(0, _doorPrefab.Length)], room.roomDoorDown);
                room.hasRoomDoorDown = true;
                selectedRoom.hasRoomDoorUp = true;
            }
        }
        else if (selectedDirection == Vector2Int.right)
        {
            if (!room.hasRoomDoorRight)
            {
                Instantiate(_doorPrefab[Random.Range(0, _doorPrefab.Length)], room.roomDoorRight);
                room.hasRoomDoorRight = true;
                selectedRoom.hasRoomDoorLeft = true;
            }
        }
        else if (selectedDirection == Vector2Int.left)
        {
            if (!room.hasRoomDoorLeft)
            {
                Instantiate(_doorPrefab[Random.Range(0, _doorPrefab.Length)], room.roomDoorLeft);
                room.hasRoomDoorLeft = true;
                selectedRoom.hasRoomDoorRight = true;
            }
        }

        return true;
    }

    private void CloseRoomDoors(Room newRoom)
    {
        roomDoors[0] = newRoom.hasRoomDoorUp;
        roomDoors[1] = newRoom.hasRoomDoorDown;
        roomDoors[2] = newRoom.hasRoomDoorRight;
        roomDoors[3] = newRoom.hasRoomDoorLeft;

        for (int i = 0; i < _maxOpenDoors;)
        {
            var randonDoor = Random.Range(0, roomDoors.Length);
            if (!roomDoors[randonDoor])
            {
                if (randonDoor == 0)
                {
                    roomDoors[randonDoor] = true;
                    newRoom.hasRoomDoorUp = true;
                }

                if (randonDoor == 1)
                {
                    roomDoors[randonDoor] = true;
                    newRoom.hasRoomDoorDown = true;
                }

                if (randonDoor == 2)
                {
                    roomDoors[randonDoor] = true;
                    newRoom.hasRoomDoorRight = true;
                }

                if (randonDoor == 3)
                {
                    roomDoors[randonDoor] = true;
                    newRoom.hasRoomDoorLeft = true;
                }

                i++;
            }
        }
    }

    #endregion
}
