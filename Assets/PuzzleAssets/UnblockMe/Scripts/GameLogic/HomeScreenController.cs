using UnityEngine;
using System.Collections;

public class HomeScreenController : MonoBehaviour {
     public GameObject Easybtn;
	// Use this for initialization
	void Start () {
        Easybtn.GetComponent<Button>().OnButtonClick =
      delegate
        {
            GameController.currDifficulty = GameController.Difficulty.Easy;
            Application.LoadLevel("UnblockMe");
        };

	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
