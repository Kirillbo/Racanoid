using UnityEngine;
using UnityEngine.SceneManagement;


public class SystemUI : IAwake , IReceive<EventButtonClick>, IReceive<EventGameOver>, IReceive<EventUpdateScore>
{

	private int _currentScore;

	//TODO баг свзянный с инициализацией открытх полей Canvas 
	public void OnAwake()
	{
		EventManager.Instance.Add(this);
	}


	
	public void HandleSignal(EventButtonClick arg)
	{
		switch (arg.Parametr)
		{
			case "StartGame":
				SkipStartPanel();
				GameManager.Instance.Tick(true);
				break;
				
			case "GenerateMap":
				GameManager.Instance.RestartGenerateMap();
				break;
				
			case "RestartGame":
				SceneManager.LoadScene(0);
				break;
		}
	}


	private void SkipStartPanel()
	{
		var canvas = GameObject.FindObjectOfType<UiContainer>();
		canvas.StartPanel.SetActive(false);
		canvas.GamePanel.SetActive(true);
	}


	void UpdateScore(int val)
	{
		_currentScore += val;

		var canvas = GameObject.FindObjectOfType<UiContainer>();
		canvas.FiledScore.text = "Score" + _currentScore.ToString();
	}


	void GameOver()
	{
		var canvas = GameObject.FindObjectOfType<UiContainer>();
		canvas.GameOverPanel.SetActive(true);
		canvas.GamePanel.SetActive(false);
		canvas.FieldEndScore.text = _currentScore.ToString();
	}

	public void HandleSignal(EventUpdateScore arg)
	{
		UpdateScore(arg.Value);
	}

	public void HandleSignal(EventGameOver arg)
	{
		GameOver();
	}

}

