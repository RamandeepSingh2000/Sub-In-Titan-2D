using System;
using UnityEngine;

namespace Submarine
{
    public class SubmarineSoundEffectsController : MonoBehaviour
    {
        [Header("Audios")]
        [Header("Movement")]
        [SerializeField] private AudioSource _movementAudioSource;
        [SerializeField] private float pitchMultiplier = 0.2f;

        [Header("Collision")]
        [SerializeField] private AudioSource _collisionAudioSource;
        [SerializeField] private AudioClip _normalCollisionSound;
        [SerializeField] private AudioClip _dashCollisionSound;
        [Header("Attack")]
        [SerializeField] private AudioSource _attackAudioSource;
        [SerializeField] private AudioClip _basicAttackSound;
        [SerializeField] private AudioClip _aoeChargeSound;
        [SerializeField] private AudioClip _aoeFireSound;
        [SerializeField] private AudioClip _aoeCancelSound;
        [Header("Action Menu")]
        [SerializeField] private AudioSource _actionMenuAudioSource;
        [SerializeField] private AudioClip _reloadingSound;        
        [SerializeField] private AudioClip _repairingSound;        
        [Header("Health")]
        [SerializeField] private AudioSource _healthAudioSource;
        [SerializeField] private AudioClip _lowHealthSound;
        [SerializeField] private AudioClip _dieSound;

        [Space]
        [Header("Modules")]
        [SerializeField] private MovementModule _movementModule;
        [SerializeField] private AttackModule _attackModule;
        [SerializeField] private ActionMenuModule _actionMenuModule;
        [SerializeField] private HealthModule _healthModule;



        private void OnEnable()
        {
            _movementModule.OnCollision += PlayCollisionSound;

            _attackModule.OnBasicAttackShot += PlayBasicAttackSound;
            _attackModule.OnAOECharge += PlayAOEChargeSound;
            _attackModule.OnAOECanceled += PlayAOECancelSound;
            _attackModule.OnAOEShot += PlayAOEFireSound;

            _healthModule.OnHealthChanged += PlayHealthBasedSound;
            _healthModule.OnDie += PlayDieSound;

            _actionMenuModule.ReloadController.OnProcessStarted += PlayReloadSound;
            _actionMenuModule.ReloadController.OnProcessCanceled += StopPlayingActionSounds;
            _actionMenuModule.ReloadController.OnProcessFinished += StopPlayingActionSounds;

            _actionMenuModule.RepairController.OnProcessStarted += PlayRepairSound;
            _actionMenuModule.RepairController.OnProcessCanceled += StopPlayingActionSounds;
            _actionMenuModule.RepairController.OnProcessFinished += StopPlayingActionSounds;
        }

        private void OnDisable()
        {
            _movementModule.OnCollision -= PlayCollisionSound;

            _attackModule.OnBasicAttackShot -= PlayBasicAttackSound;
            _attackModule.OnAOECharge -= PlayAOEChargeSound;
            _attackModule.OnAOECanceled -= PlayAOECancelSound;
            _attackModule.OnAOEShot -= PlayAOEFireSound;

            _healthModule.OnHealthChanged -= PlayHealthBasedSound;
            _healthModule.OnDie -= PlayDieSound;

            _actionMenuModule.ReloadController.OnProcessStarted -= PlayReloadSound;
            _actionMenuModule.ReloadController.OnProcessCanceled -= StopPlayingActionSounds;
            _actionMenuModule.ReloadController.OnProcessFinished -= StopPlayingActionSounds;

            _actionMenuModule.RepairController.OnProcessStarted -= PlayRepairSound;
            _actionMenuModule.RepairController.OnProcessCanceled -= StopPlayingActionSounds;
            _actionMenuModule.RepairController.OnProcessFinished -= StopPlayingActionSounds;
        }


        // Update is called once per frame
        void Update()
        {
            PlayMovementSounds();
        }

        private void PlayMovementSounds()
        {
            float velocityMagnitude = _movementModule.GetVelocity().magnitude;
            float pitch = .5f + velocityMagnitude * pitchMultiplier;
            _movementAudioSource.pitch = Mathf.Clamp(pitch, 0.1f, 3f);
        }

        private void PlayCollisionSound()
        {
            if (_movementModule.IsDashing())
                _collisionAudioSource.clip = _dashCollisionSound;
            else _collisionAudioSource.clip = _normalCollisionSound;

            _collisionAudioSource.Play();
        }

        #region ATTACK

        private void PlayBasicAttackSound() => _attackAudioSource.PlayOneShot(_basicAttackSound);
        private void PlayAOEChargeSound() => _attackAudioSource.PlayOneShot(_aoeChargeSound);
        private void PlayAOECancelSound()
        {
            if (_attackAudioSource.isPlaying)
                _attackAudioSource.Stop();
            _attackAudioSource.PlayOneShot(_aoeCancelSound);
        }

        private void PlayAOEFireSound() => _attackAudioSource.PlayOneShot(_aoeFireSound);

        #endregion

        private void PlayHealthBasedSound(int healthPoints)
        {
            float percentage = (float)healthPoints / (float)_healthModule.GetMaxHealth();
            if (percentage < 0.25f)
            {
                if (!_healthAudioSource.isPlaying)
                {
                    _healthAudioSource.clip = _lowHealthSound;
                    _healthAudioSource.loop = true;
                    _healthAudioSource.Play();
                }
            }
            else
            {
                _healthAudioSource.Stop();
                _healthAudioSource.loop = false;
            }
        }

        private void PlayDieSound()
        {
            _healthAudioSource.loop = false;
            _healthAudioSource.PlayOneShot(_dieSound);
        }

        #region ACTION MENU

        private void PlayRepairSound()
        {
            _actionMenuAudioSource.loop = true;
            _actionMenuAudioSource.clip = _repairingSound;
            _actionMenuAudioSource.Play();
        }        

        private void PlayReloadSound()
        {
            _actionMenuAudioSource.loop = true;
            _actionMenuAudioSource.clip = _reloadingSound;
            _actionMenuAudioSource.Play();
        }        

        private void StopPlayingActionSounds()
        {
            _actionMenuAudioSource.Stop();
        }

        internal void MuteAllSources()
        {
            _collisionAudioSource.mute = true;
            _movementAudioSource.mute = true;
            _attackAudioSource.mute = true;
            _healthAudioSource.mute = true;
            _actionMenuAudioSource.mute = true;
        }

        #endregion
    }
}