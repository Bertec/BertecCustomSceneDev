using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Simple scene that is just a closed room with 4 boxes in the corner, with a single option to change color.
/// This scene is similar to the Static Scene in that it does not move but the user can look around.
/// Provided as a trivial example on setting up a very basic scene as a starting point.
/// </summary>


public class ClosedRoom : MonoBehaviour
{
	public GameObject[] cubes;

	[Bertec.SceneInfo(Key = "ClosedRoom", Name = "Closed Room", Scene = "@ClosedRoom")]
	public class ClosedRoomInfo : Bertec.SceneInfo
    {
        public ClosedRoomInfo()
        {
            var boxcolor = AddList("boxcolor", "Box Color",
                new List<Bertec.SceneChoiceItem>()
                {
                    { "red", "Red" },
                    { "green", "Green" },
                    { "blue", "Blue" }
                }, "green");
        }
    }

    void Awake()
	{
        Bertec.OptionChangedContainer.Connect(OptionChanged);
	}

	void Start()
	{
	}

	void Update()
	{

	}

	void OptionChanged(string key, object val)
	{
		if (key == "boxcolor")
		{
			Color c = Color.black;
			if (val.ToString() == "blue")
				c = Color.blue;
			if (val.ToString() == "green")
				c = Color.green;
			if (val.ToString() == "red")
				c = Color.red;
			foreach (GameObject cube in cubes)
				cube.GetComponent<Renderer>().material.color = c;
		}
	}

}
