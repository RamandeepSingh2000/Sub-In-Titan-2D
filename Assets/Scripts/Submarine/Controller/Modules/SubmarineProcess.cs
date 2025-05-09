using UnityEngine;
using UnityEngine.Events;

namespace Submarine
{
    public abstract class SubmarineProcess : MonoBehaviour
    {
        [Tooltip("Process duration from 0% to 100%")]
        [SerializeField] protected float fullProcessDuration = 5f;
        protected float processTime = 0;

        private float _processDuration;
        public bool IsPerformingProcess { get; protected set; } = false;

        public UnityAction OnProcessStarted;
        public UnityAction OnProcessCanceled;
        public UnityAction OnProcessFinished;

        private void Start()
        {
            _processDuration = fullProcessDuration;
        }

        public virtual void StartProcess()
        {
            if (IsPerformingProcess)
                return;

            OnProcessStarted?.Invoke();
            IsPerformingProcess = true;
        }

        public virtual void CancelProcess()
        {
            if (!IsPerformingProcess)
                return;

            OnProcessCanceled?.Invoke();
            IsPerformingProcess = false;
        }

        protected virtual void FinishProcess()
        {
            IsPerformingProcess = false;
            OnProcessFinished?.Invoke();
            processTime = 0;
        }

        /// <summary>
        /// Returns value between 0 and 1
        /// </summary>
        /// <returns></returns>
        public float GetProgress()
        {
            return processTime / _processDuration;
        }

        protected virtual void Update()
        {
            if (IsPerformingProcess)
            {
                processTime += Time.deltaTime;
                if (processTime >= _processDuration)
                    FinishProcess();
            }
        }
    }
}