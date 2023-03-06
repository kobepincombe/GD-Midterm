using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
//using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ButtonCode : MonoBehaviour
{

    //public Button startButton;
    //public Button howToPlayButton;
    public Button exitButton;
    public Label controls;

    // Start is called before the first frame update
    void Start()
    {
        var root = GetComponent<UIDocument>().rootVisualElement;
        //startButton = root.Q<Button>"start-button";
        Button startButton = root.Q<Button>("start-button");
        Button howToPlayButton = root.Q<Button>("how-to-play");
        
        //howToPlayButton = root.Q<Button>"how-to-play";
        exitButton = root.Q<Button>("exit-button");
        controls = root.Q<Label>("controls");

        startButton.clicked += StartButtonPressed;
        howToPlayButton.clicked += howToPlayButtonPressed;
    }

    // Update is called once per frame
    void StartButtonPressed()
    {
        SceneManager.LoadScene("JCC");
    }

    void howToPlayButtonPressed() {
        controls.text = "Player 1: WASD to move, L shift to throw \n Player 2: arrows to move, space to throw";
        controls.style.display = DisplayStyle.Flex;
    }
}
