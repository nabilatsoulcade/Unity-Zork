using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zork.Common;
using TMPro;
using System;
public class GameManager : MonoBehaviour
{
    private string GameFilename = "Zork";
    private Game Game { get; set; }

    [SerializeField]
    private UnityOutputService OutputService;
    [SerializeField]
    private UnityInputService InputService;

    [Header("UI Elements")]
    [SerializeField]
    private TextMeshProUGUI LocationText;
    [SerializeField]
    private TextMeshProUGUI ScoreText;
    [SerializeField]
    private TextMeshProUGUI MovesText;
    // Start is called before the first frame update
    void Awake()
    {
        TextAsset gameFileAsset = Resources.Load<TextAsset>(GameFilename); 
        Game = Game.LoadFromFile(gameFileAsset.text, OutputService, InputService);
    }

    void Start()
    {
        Game.Output.WriteLine("Welcome to Zork");
        Game.CommandManager.PerformCommand(Game, "Look");

        Game.Player.ScoreChanged += ChangeScore;
        Game.Player.LocationChanged += ChangeLocation;
        Game.Player.MovesChanged += ChangeMoves;
        Game.GameRunningChanged += QuitGame;

        Game.Player.LocationName = Game.Player.Location.ToString();

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Return))
        {
            if (!string.IsNullOrWhiteSpace(InputService.InputField.text))
            {
                Game.Output.WriteLine(InputService.InputField.text);
                InputService.ProcessInput();
            }

            InputService.InputField.text = string.Empty;
            InputService.InputField.Select();
            InputService.InputField.ActivateInputField();
        }

        
    }

    private void ChangeScore(object sender, int e )
    {
        ScoreText.text = "Score: " + Game.Player.Score.ToString();
    }

    private void ChangeMoves(object sender, int e)
    {
        MovesText.text = "Moves: " + Game.Player.Moves.ToString();
    }

    private void ChangeLocation(object sender, string e)
    {
        LocationText.text = "Location: " + Game.Player.LocationName.ToString();
    }

    private void QuitGame(object sender, bool e)
    {
        Debug.Log("Attempting to quit game");
        Application.Quit();
    }
}
