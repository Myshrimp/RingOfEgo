using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;

namespace MY.Manager
{
    public class GameManager : Singleton<GameManager>
    {
        [SerializeField] GameObject player;
        [SerializeField] GameObject ring;
        [SerializeField] GameObject mouse;

        [Header("Victory lighting")]
        [SerializeField] Light2D victoryLight;
        public float lightup_speed = 0.005f;
        public float lightup_outer_radius = 12f;

        [Header("")]
        public int godModeAbility = 0;
        public float godmode_cooldown = 60f;
        public float timeToRecover = 1f;
        public float godsModeTimeScale = 0.1f;
        public Mode currentMode;
        public float mouseMoveSpeed = 0.2f;
        private Action OnGameEnter;
        private float defaultTimeScale = 1f;
        private GameObject mouseObject;
        private Vector2 mousePos;
        private bool dead = false;
        private bool survive = false;
        [Header("Timer")]
        [SerializeField] float godCoolDownSpawn = 5.0f;
        bool isGodCanbeUsed = false;

        [Header("Come to end")]
        public float comeToEndDuration = 0.5f;
        private bool readyToSwitchScene = false;
        private float comeToEndTimer = 0f;
        [Header("Audio")]
        [SerializeField] AudioData dead_audio;
        [SerializeField] AudioData victory_audio;
        public float dangerous_radius=2f;
        private bool isPlayingDeadAudio = false;
        private bool isPlayingVictoryAudio = false;

        [Header("Camera")]
        [SerializeField] Cinemachine.CinemachineImpulseSource impulse_source;
        [SerializeField] float impulse_intensity = 1f;
        [Header("GodHand")]
        public GameObject handgod;
        [Header("GodInformation")]
        [SerializeField] Image godInformationImage;
        [SerializeField] Sprite playModeSprite;
        [SerializeField] Sprite godModeSprite;

        private float SpaceRemTime = 0;

        private bool isTimeFlowNormal = true;
        private void Awake()
        {
            dead = false;
            survive = false;
            readyToSwitchScene = false;
            Ring.instance.gameObject.SetActive(true);
            currentMode = Mode.Enter;
            defaultTimeScale = Time.timeScale;
            OnGameEnter += GameStart;
        }

        private void OnEnable()
        {
            godInformationImage.sprite = playModeSprite;
        }
        public void EnterGodMode()
        {
            GodsHand.instance.EnterGodsPerspective();
            //Time.timeScale = godsModeTimeScale;
            //Set time scale here:
            currentMode = Mode.God;
            godInformationImage.sprite = godModeSprite;
            ring.GetComponent<EdgeCollider2D>().enabled = false ;
            isGodCanbeUsed = false ;
        }

        public void OnExitGodMode()
        {
            ring.GetComponent<EdgeCollider2D>().enabled=true;
            StartCoroutine(nameof(RecoverFromDizzle));
        }
        public void EnterPlayerMode()
        {
            GodsHand.instance.EnterPlayerPerspective();
            currentMode = Mode.Player;
            godInformationImage.sprite = playModeSprite;
            //Time.timeScale = defaultTimeScale;
            //Recover time scale here:
        }
        private void Update()
        {
            PlayDyingAudio();
            PlayVictoryAudio();
            //if (Ring.instance.radius <= 0.7f && !dead)
            // {
            //   EnterDeadMode();
            //   dead = true;
            //}

            //if (Ring.instance.LightsIgnited == 12 && !survive)
            //{
            //TimeController.Instance.SlowIn(0.5f);
            //  EnterSurviveMode();
            //  survive = true;
            //victoryLight.pointLightOuterRadius = Mathf.Lerp(victoryLight.pointLightOuterRadius, lightup_outer_radius, lightup_speed);
            //}

            GameEnd();
            DoMode();

        }

        private void DoMode()
        {
            
            if (currentMode == Mode.Enter)
            {
                if (Input.GetKeyDown(KeyCode.Space) && SpaceRemTime <= 0)
                {
                    TimeController.Instance.SlowIn(0.3f);
                    EnterGodMode();
                }

            }
            if (currentMode == Mode.Player && isGodCanbeUsed)
            {
                //Disable player controller here or in EnterGodsPerspective function
                if (Input.GetKeyDown(KeyCode.Space) && SpaceRemTime <=0 )
                {
                    TimeController.Instance.SlowIn(0.3f);
                    EnterGodMode();
                }
            }

            if (currentMode == Mode.God)
            {
                player.GetComponent<Move>().fx = 0;
                player.GetComponent<Move>().fy = 0;
                if (Input.GetKeyDown(KeyCode.Mouse0))
                {
                    mousePos = MouseInputHandler.ScreenPointToWorldPoint();
                    StartCoroutine(nameof(MouseMoveCheck));
                }
                if (Input.GetKeyUp(KeyCode.Mouse0))
                {
                    Instantiate(handgod, ring.transform.position, Quaternion.identity);

                    StopCoroutine(nameof(MouseMoveCheck));
                    OnExitGodMode();

                    OnGameEnter?.Invoke();
                    currentMode = Mode.Player;
                }
            }

            if (Input.GetKeyDown(KeyCode.Space))
            {
                SpaceRemTime = 0.3f;
            }
            SpaceRemTime -= Time.unscaledDeltaTime;
            if (SpaceRemTime < -0.1f)
            {
                SpaceRemTime = -0.1f;
            }
            print("current mode:" + currentMode.ToString());
        }

        private void GameEnd()
        {
            if (Ring.instance.radius <= 0.7f && !dead || Ring.instance.LightsIgnited == 12 && !survive && currentMode != Mode.ComeToEnd)
            {
                currentMode = Mode.ComeToEnd;
                TimeController.Instance.SlowIn(0.5f);
                Ring.instance.gameObject.SetActive(false);
                if (Ring.instance.radius <= 0.7f)
                {
                    AsPlayer.Instance.doQuick_dieByRing();
                }

            }
            if (currentMode == Mode.ComeToEnd)
            {
                comeToEndTimer += Time.deltaTime;
                if (Ring.instance.LightsIgnited == 12)
                {
                    victoryLight.pointLightOuterRadius = Mathf.Lerp(victoryLight.pointLightOuterRadius, lightup_outer_radius, lightup_speed * Time.unscaledDeltaTime);
                }

                if (comeToEndTimer > comeToEndDuration)
                {
                    TimeController.Instance.SlowOut(0.5f);
                    if (Ring.instance.radius <= 0.7f)
                    {
                        dead = true;
                        currentMode = Mode.Dead;
                    }
                    if (Ring.instance.LightsIgnited == 12)
                    {
                        survive = true;
                        AudioManager.Instance.StopSfxPlay();
                        currentMode = Mode.Survive;
                    }
                    DataTransferer.Instance.mode = currentMode;
                    SceneLoader.Instance.LoadGameEndScene();

                }

            }
        }

        private void PlayDyingAudio()
        {
            if (Ring.instance.radius < dangerous_radius)
            {
                if (!isPlayingDeadAudio)
                {
                    StartCoroutine(nameof(PlayDeadAudio));
                    isPlayingDeadAudio = true;
                }
            }
            else
            {
                StopCoroutine(nameof(PlayDeadAudio));
                isPlayingDeadAudio = false;
            }
        }

        private void PlayVictoryAudio()
        {
            if(currentMode == Mode.ComeToEnd && Ring.instance.LightsIgnited==12 && !isPlayingVictoryAudio)
            {
                AudioManager.Instance.PlaySFX(victory_audio);
                isPlayingVictoryAudio = true;
            }
            if(currentMode != Mode.ComeToEnd)
            {
                isPlayingVictoryAudio= false;
            }
        }

        private void EnterSurviveMode()
        {
            currentMode = Mode.Survive;
            DataTransferer.Instance.mode = currentMode;
            SceneLoader.Instance.LoadGameEndScene();
        }

        public void TransitToTargetPosition()
        {
            //
           
            // player.transform.position =
        }
        private Vector2 GetTargetPosition()
        {
            return MouseInputHandler.ScreenPointToWorldPoint();
        }

        /// <summary>
        /// Apply dizzle effect to player and wait until the animation ends
        /// </summary>
        /// <returns></returns>
        /// 
        private float dizzleTimer = 0f;
        private IEnumerator RecoverFromDizzle()
        {

            yield return new WaitForSeconds(timeToRecover * 0.1f);
            TimeController.Instance.SlowOut(0.5f);
            isTimeFlowNormal = true;
            StartCoroutine(TimerCoroutine());
            TransitToTargetPosition();
            
            EnterPlayerMode();
        }

        private IEnumerator MouseMoveCheck()
        {
            while(true)
            {
                yield return new WaitForSeconds(0.001f);
                Vector2 newMousePos=MouseInputHandler.ScreenPointToWorldPoint();
                Vector2 distance = newMousePos - mousePos;
                if(CheckMouseViable.instance.MouseIn)
                {
                    mousePos = newMousePos;
                    print("Mouse in");
                }
                else
                {
                    Ring.instance.transform.position = Ring.instance.transform.position * 0.9f;
                }
               
                if(CheckMouseViable.instance.MouseIn)
                {
                    ring.transform.position = new Vector2(ring.transform.position.x + distance.x * mouseMoveSpeed, ring.transform.position.y + distance.y * mouseMoveSpeed);
                }              
            }
        }

        private void GameStart()
        {
            EnemyManager.Instance.spawnEnemy = true;
            EnemyManager.Instance.StartSpawnEnemy();
            OnGameEnter = null;
        }

        /// <summary>
        /// Gain a god mode chance after every piece of cooldown time
        /// </summary>
        public void GainAbility()
        {
            if(godModeAbility<1 && currentMode == Mode.Player)
            {
                godModeAbility += 1;
            }
        }
        public float enterModeCameraZoomInDuration = 2f;
        private float zoominTimer = 0f;

        IEnumerator TimerCoroutine()
        {
            yield return new WaitForSeconds(godCoolDownSpawn);
            isGodCanbeUsed = true;
        }

        private void EnterDeadMode()
        {
            currentMode = Mode.Dead;
            DataTransferer.Instance.mode= currentMode;
            SceneLoader.Instance.LoadGameEndScene();
        }


        private IEnumerator PlayDeadAudio()
        {
            while(true)
            {
                AudioManager.Instance.PlaySFX(dead_audio);
                impulse_source.GenerateImpulse(impulse_intensity);
                yield return new WaitForSeconds(1f);            
            }
        }
    }

}

public enum Mode
{
    Player,God,Dead,Survive,Enter,ComeToEnd
}

