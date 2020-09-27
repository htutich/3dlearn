using UnityEngine;

public class Room : MonoBehaviour
{
    #region Fields

    public Transform roomDoorUp;
    public Transform roomDoorDown;
    public Transform roomDoorLeft;
    public Transform roomDoorRight;

    public bool hasRoomDoorUp = false;
    public bool hasRoomDoorDown = false;
    public bool hasRoomDoorLeft = false;
    public bool hasRoomDoorRight = false;

    private float _roomRadiusCenter = 5.1f;

    #endregion


    #region Properties

    public float RoomRadiusCenter
    {
        get
        {
            return _roomRadiusCenter;
        }
    }

    #endregion
}
