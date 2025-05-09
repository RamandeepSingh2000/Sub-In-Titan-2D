using UnityEngine;

namespace Submarine.UI
{
    public class ActionMenuPresenter : MonoBehaviour
    {
        [SerializeField] private ActionMenuView _view;
        [SerializeField] private ActionMenuModule _model;

        private bool _updateRepairProgress = false;
        private bool _updateReloadProgress = false;

        private void OnEnable()
        {
            _model.OnOpen += Open;
            _model.OnClose += Close;

            _model.RepairController.OnProcessStarted += OnRepairStarted;
            _model.RepairController.OnProcessCanceled += OnRepairCanceled;
            _model.RepairController.OnProcessFinished += OnRepairFinished;

            _model.ReloadController.OnProcessStarted += OnReloadStarted;
            _model.ReloadController.OnProcessCanceled += OnReloadCanceled;
            _model.ReloadController.OnProcessFinished += OnReloadFinished;
        }

        private void OnDisable()
        {
            _model.OnOpen -= Open;
            _model.OnClose -= Close;

            _model.RepairController.OnProcessStarted -= OnRepairStarted;
            _model.RepairController.OnProcessCanceled -= OnRepairCanceled;
            _model.RepairController.OnProcessFinished -= OnRepairFinished;

            _model.ReloadController.OnProcessStarted -= OnReloadStarted;
            _model.ReloadController.OnProcessCanceled -= OnReloadCanceled;
            _model.ReloadController.OnProcessFinished -= OnReloadFinished;
        }

        public void Open()
        {
            _view.Open();
            UpdateButtonsInteractableStatus();
        }

        public void Close()
        {
            _view.Close();
        }

        private void UpdateButtonsInteractableStatus()
        {
            if (_model.CanRepair())
                _view.EnableRepairButton();
            else _view.DisableRepairButton();

            if (_model.CanReload())
                _view.EnableReloadButton();
            else _view.DisableReloadButton();

        }

        #region VIEW CONTROLLED METHODS

        public void StartRepairing() => _model.StartRepairing();

        public void CancelProcess() => _model.CancelCurrentProcess();

        public void StartReloading() => _model.StartReloading();

        #endregion

        #region EVENTS

        #region REPAIR

        private void OnRepairStarted()
        {
            _view.ShowRepairProgressBar();
            _view.DisableProgressButtons();
            _view.ShowProcessMenu();
            _updateRepairProgress = true;
        }

        private void OnRepairCanceled()
        {
            _view.HideRepairProgressBar();
            _view.HideProcessMenu();
            _updateRepairProgress = false;
            UpdateButtonsInteractableStatus();
        }

        private void OnRepairFinished()
        {
            _view.HideRepairProgressBar();
            _view.HideProcessMenu();
            _updateRepairProgress = false;
            UpdateButtonsInteractableStatus();
        }

        #endregion

        #region RELOAD

        private void OnReloadStarted()
        {
            _view.ShowReloadProgressBar();
            _view.ShowProcessMenu();
            _view.DisableProgressButtons();
            _updateReloadProgress = true;
        }

        private void OnReloadCanceled()
        {
            _view.HideReloadProgressBar();
            _view.HideProcessMenu();
            _updateReloadProgress = false;
            UpdateButtonsInteractableStatus();
        }

        private void OnReloadFinished()
        {
            _view.HideReloadProgressBar();
            _view.HideProcessMenu();            
            _updateReloadProgress = false;
            UpdateButtonsInteractableStatus();
        }

        #endregion

        #endregion

        private void Update()
        {
            if (_updateRepairProgress)
                _view.UpdateRepairProgress(_model.RepairController.GetProgress());

            if (_updateReloadProgress)
                _view.UpdateReloadProgress(_model.ReloadController.GetProgress());

        }
    }
}