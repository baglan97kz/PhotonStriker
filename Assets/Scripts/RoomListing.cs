using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using TMPro;

public class RoomListing : MonoBehaviourPunCallbacks
{
    public Transform Grid;
    public GameObject RoomNamePrefab;

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        foreach (RoomInfo room in roomList) {
            if (room.RemovedFromList)
            {
                DeleteRoom(room);

            }
            else {
                AddRoom(room);
            }
        
        }
    }

    void AddRoom(RoomInfo room) {
        print("Add Room : " + room.Name);
        GameObject obj = Instantiate(RoomNamePrefab, new Vector2(0,0), Quaternion.identity);
        obj.transform.SetParent(Grid,false);
        obj.GetComponentInChildren<TMP_Text>().text = room.Name;    
    
    }

    void DeleteRoom(RoomInfo room)
    {
        print("Delete Room : " + room.Name);

        int roomCounts = Grid.childCount;

        for (int i=0; i < roomCounts; ++i) {
            if (Grid.GetChild(i).gameObject.GetComponentInChildren<TMP_Text>().text == room.Name) {
                Destroy(Grid.GetChild(i).gameObject);   
            
            }        
        }


    }
}
