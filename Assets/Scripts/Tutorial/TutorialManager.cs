using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public enum TutorialSteps
{
    Movement,
    BasicAttack,
    AOEAttack,
    Reload,
    Repair
}

public class TutorialManager : MonoBehaviour
{
    [SerializeField] private TutorialController[] _tutorials;
    [SerializeField] private BlackScreenAnimation _blackScreenAnimation;

    private TutorialSteps _currentStep = TutorialSteps.Movement;
    private int _currentStepIndex = 0;

    public UnityEvent OnStepFinished;
    public UnityEvent OnTutorialFinished;

    private void Awake()
    {        
        for (int i = 0; i < _tutorials.Length; i++)
            _tutorials[i].gameObject.SetActive(false);
        StartTutorial(_currentStepIndex);
    }

    private void StartTutorial(int index)
    {
        _tutorials[index].gameObject.SetActive(true);
        _tutorials[index].SetUp();
    }

    public void FinishStep(int index)
    {
        StartCoroutine(SwitchToNextTutorial());
        _currentStep = (TutorialSteps)(++index);
    }

    private IEnumerator SwitchToNextTutorial()
    {
        yield return new WaitForSeconds(1);
        float transitionTime = 3.0f;
        _blackScreenAnimation.FadeOut(transitionTime);
        yield return new WaitForSeconds(transitionTime);
        _tutorials[_currentStepIndex].Disable();
        _tutorials[_currentStepIndex].gameObject.SetActive(false);
        if (_currentStepIndex < _tutorials.Length - 1)
        {
            _currentStepIndex++;
            StartTutorial(_currentStepIndex);
            _blackScreenAnimation.FadeIn(transitionTime);
        }
        else
        {
            OnTutorialFinished?.Invoke();
        }
    }
}
