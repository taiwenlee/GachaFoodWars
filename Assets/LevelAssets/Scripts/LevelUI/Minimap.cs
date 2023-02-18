using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minimap : MonoBehaviour
{
    [SerializeField] LevelMap levelMap;
    [SerializeField] GameObject model;

    private Matrix<RoomIcon> roomIconMatrix;

    private struct RoomIcon
    {
        public RoomIcon(bool active, GameObject icon)
        {
            IsActive = active;
            HasVisited = false;
            Icon = icon;
        }

        public bool IsActive { get; set; }
        public bool HasVisited { get; set; }

        public GameObject Icon { get; set; }

    }

    // Start is called before the first frame update
    void Start()
    {
        int xscl = 40;
        int yscl = 40;
        Vector3 position = model.GetComponent<RectTransform>().anchoredPosition3D;
        Vector3 stored = position;
        Matrix<RoomBlueprint> rooms = levelMap.roomMatrix;
        
        roomIconMatrix = new Matrix<RoomIcon>();
        for (int i = 0; i < rooms.cols.Count; i++)
        {
            roomIconMatrix.cols.Add(new Rows<RoomIcon>());
            position.x += xscl * i;
            for (int j = 0; j < rooms.cols[i].rows.Count; j++)
            {
                position.y -= yscl * j;
                GameObject newImg = Instantiate(model, transform);
                newImg.GetComponent<RectTransform>().anchoredPosition3D = position;
                bool isActive = rooms.cols[i].rows[j].IsActive;
                newImg.SetActive(isActive);

                RoomIcon ri = new RoomIcon(isActive, newImg);
                roomIconMatrix.cols[i].rows.Add(ri);
                position.y = stored.y;
            }
            position = stored;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
