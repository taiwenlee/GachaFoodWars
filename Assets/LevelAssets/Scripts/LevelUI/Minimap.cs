using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Minimap : MonoBehaviour
{
    [SerializeField] LevelMap levelMap;
    [SerializeField] GameObject model;
    [SerializeField] GameObject modelBG;
    [SerializeField] GameObject[] pathIcons;

    // Start is called before the first frame update
    void Start()
    {
        int xscl = 40; // x-axis position scaling
        int yscl = 40; // y-axis position scaling

        // Get the starting position from the non-rendered model prefab
        Vector3 position = model.GetComponent<RectTransform>().anchoredPosition3D;

        // Store the position for later use
        Vector3 stored = position;

        // Get the room blueprint from level map
        Matrix<RoomBlueprint> rooms = levelMap.roomMatrix;

        // Allocate room icon matrix
        for (int i = 0; i < rooms.cols.Count; i++)
        {
            // Multiply scalar based on matrix x position
            position.x += xscl * i;

            for (int j = 0; j < rooms.cols[i].rows.Count; j++)
            {
                // Get reference to room in question
                RoomBlueprint rb = rooms.cols[i].rows[j];

                // Check if the room icon resembles the current room;
                bool currRoom = levelMap.currentRoom == rb;

                // Multiply scalar based on matrix y position
                position.y -= yscl * j;

                // Instantiate minimap icon images
                GameObject newImg = InstantiateRoomIcon(rb, currRoom, position);

                // Set visibility on minimap based on visited boolean
                newImg.SetActive(rb.HasVisited);

                // Add new room icon
                //roomIconMatrix.cols[i].rows.Add(ri);

                // Reset y position
                position.y = stored.y;
            }

            // Reset entire position
            position = stored;
        }
    }

    // Update is called once per frame
    //void Update()
    //{
        
    //}

    private GameObject InstantiateRoomIcon(
        RoomBlueprint rb,
        bool currRoom,
        Vector2 position
    )
    {
        // Add background image
        GameObject bg = Instantiate(
            modelBG,
            transform
        );

        // Adjust background position
        bg.GetComponent<RectTransform>()
            .anchoredPosition3D = position;

        // Instantiate room icon image
        GameObject newImg = Instantiate(
            model,
            transform
        );

        // Get image component reference
        Image imgc = newImg.GetComponent<Image>();

        // Set image color based on current room
        imgc.color =
            currRoom == true ?
            Color.white :
            Color.black;

        // Adjust alpha
        Color currentColor = imgc.color;
        currentColor.a = 0.5f;
        imgc.color = currentColor;

        // Adjust room icon position
        newImg.GetComponent<RectTransform>()
            .anchoredPosition3D = position;

        // Instantiate paths
        InstantiatePaths(rb, ref newImg);

        return newImg;
    }

    // Instantiate path images if there are paths connected to the room
    private void InstantiatePaths(RoomBlueprint rb, ref GameObject newImg)
    {
        RoomBlueprint[] paths = {
            rb.North,
            rb.East,
            rb.South,
            rb.West
        };

        for (int i = 0; i < paths.Length; i++)
        {
            if (paths[i] != null)
            {
                Instantiate(pathIcons[i], newImg.transform);
            }
        }
    }
}
