using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Object = UnityEngine.Object;


public class SystemUI : IAwake ,  IReceive<EventButtonClick>, IReceive<EventGameOver>, IReceive<EventUpdateScore>, IReceive<EventWin>, IDisposable
{
	
	private int _currentScore;
	private int _timeAttack;

	private UiContainer _mainCanvas;
	private GameObject _startPanel;
	private GameObject _gamePanel;
	private GameObject _losePanel;
	private GameObject _winPanel;
	private Text _scoreFieldEndPanel;
	private Text _scoreField;
	private Text _timeAttackField;

	public void OnAwake()
	{
		
		EventManager.Instance.Add<EventButtonClick>(this).Add<EventGameOver>(this).Add<EventUpdateScore>(this).Add<EventWin>(this);
		_mainCanvas = Object.Instantiate(GameManager.Instance.Entities.Canvas).GetComponent<UiContainer>();
		
		_startPanel = _mainCanvas.StartPanel;
		_gamePanel = _mainCanvas.GamePanel;
		_losePanel = _mainCanvas.GameOverPanel;
		_winPanel = _mainCanvas.WinPanel;
		_scoreFieldEndPanel = _mainCanvas.FieldEndScore;
		_scoreField = _mainCanvas.FiledScore;
		_timeAttackField = _mainCanvas.FieldTimeWarp;

		_timeAttack = PoolManager.Instance.Get<ComponentSettingsGame>().TimeAttack;
		
		Timer.Add(1f, UpdateTimeAttack, true);
		
	}

	
	void UpdateTimeAttack()
	{
		_timeAttack--;
		
		_timeAttackField.text = _timeAttack.ToString() + " :Time Attack";
		if (_timeAttack <= 1)
		{
			_timeAttack =  PoolManager.Instance.Get<ComponentSettingsGame>().TimeAttack;
		}
	}
	

	private void StartGame()
	{
	
		_startPanel.SetActive(false);
		_gamePanel.SetActive(true);
	}


	void UpdateScore(int val)
	{
		_currentScore += val;
		_scoreField.text = "Score" + _currentScore.ToString();
	}


	void GameOver()
	{
		_losePanel.SetActive(true);
		_gamePanel.SetActive(false);
		_scoreFieldEndPanel.text = _currentScore.ToString();
	}

	void Win()
	{
		_gamePanel.SetActive(false);
		_winPanel.SetActive(true);
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
				EventManager.Instance.Remove<EventGameOver>(this);
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

	public void Dispose()
	{		
		EventManager.Instance.Dispose();
	}
}

