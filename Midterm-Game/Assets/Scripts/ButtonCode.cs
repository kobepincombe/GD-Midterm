using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
//using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ButtonCode : MonoBehaviour
{

    public Button startButton;
    public Button howToPlayButton;
    public Label controls;

    // Start is called before the first frame update
    void Start()
    {
        var root = GetComponent<UIDocument>().rootVisualElement;
        startButton = root.Q<Button>("start-button");
        howToPlayButton = root.Q<Button>("how-to-play");
        controls = root.Q<Label>("controls");

        startButton.clicked += StartButtonPressed;
        howToPlayButton.clicked += howToPlayButtonPressed;
    }

    void StartButtonPressed()
    {
        SceneManager.LoadScene("JCC");
    }

    void howToPlayButtonPressed() {
        controls.text = "Player 1: WASD to move, \n L shift to throw \n Player 2: arrows to move, \n space to throw";
        controls.style.display = DisplayStyle.Flex;
    }
}
