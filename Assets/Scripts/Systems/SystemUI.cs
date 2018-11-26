using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class SystemUI : IAwake ,  IReceive<EventButtonClick>, IReceive<EventGameOver>, IReceive<EventUpdateScore>, IReceive<EventWin>
{
	
	private int _currentScore;
	private int _timeAttack;

	private UiContainer _mainCanvas;
	
	public void OnAwake()
	{
		EventManager.Instance.Add(this);
		_mainCanvas = Object.FindObjectOfType<UiContainer>();
		
//		_startPanel = canvas.StartPanel;
//		_gamePanel = canvas.GamePanel;
//		_losePanel = canvas.GameOverPanel;
//		_winPanel = canvas.WinPanel;
//		_scoreFieldEndPanel = canvas.FieldEndScore;
//		_scoreField = canvas.FiledScore;
//		_timeAttackField = canvas.FieldTimeWarp;

		_timeAttack = PoolManager.Instance.Get<ComponentSettingsGame>().TimeAttack;
		
		Timer.Add(1f, UpdateTimeAttack, true);
		
	}

	
	//TODO баг, связанный с инициализацией полей Canvas класса UIContainer
	void UpdateTimeAttack()
	{
		_timeAttack--;
		var mainCanvas = GameObject.FindWithTag("UI").GetComponent<UiContainer>();
		mainCanvas.FieldTimeWarp.text = _timeAttack.ToString() + " :Time Attack";
		if (_timeAttack <= 0)
		{
			_timeAttack =  PoolManager.Instance.Get<ComponentSettingsGame>().TimeAttack;
		}
	}
	

	private void StartGame()
	{
		var mainCanvas = GameObject.FindWithTag("UI").GetComponent<UiContainer>();
		mainCanvas.StartPanel.SetActive(false);
		mainCanvas.GamePanel.SetActive(true);
	}


	void UpdateScore(int val)
	{
		_currentScore += val;
		
		var mainCanvas = GameObject.FindWithTag("UI").GetComponent<UiContainer>();
		mainCanvas.FiledScore.text = "Score" + _currentScore.ToString();
	}


	void GameOver()
	{
		var mainCanvas = GameObject.FindWithTag("UI").GetComponent<UiContainer>();
		mainCanvas.GameOverPanel.SetActive(true);
		mainCanvas.GamePanel.SetActive(false);
		mainCanvas.FieldEndScore.text = _currentScore.ToString();
	}

	void Win()
	{
		var mainCanvas = GameObject.FindWithTag("UI").GetComponent<UiContainer>();
		mainCanvas.GamePanel.SetActive(false);
		mainCanvas.WinPanel.SetActive(true);
	}
	
	//=================== РЕАГИРОВАНИЕ НА РАЗЛИЧНЫЕ EVENTS ===================
	
	
	public void HandleSignal(EventButtonClick arg)
	{
		switch (arg.Parametr)
		{
			case "StartGame":
				StartGame();
				GameManager.Instance.Tick(true);
				break;
				
			case "GenerateMap":
				GameManager.Instance.RestartGenerateMap();
				break;
				
			case "RestartGame":
				EventManager.Instance.Remove(this);
				SceneManager.LoadScene(0);
				break;
		}
	}

	
	public void HandleSignal(EventUpdateScore arg)
	{
		UpdateScore(arg.Value);
	}

	public void HandleSignal(EventGameOver arg)
	{
		GameOver();
	}

	public void HandleSignal(EventWin arg)
	{
		Win();
	}
}

