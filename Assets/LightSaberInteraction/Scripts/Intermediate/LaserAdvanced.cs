using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

namespace LightSaber.intermediate
{
    public class LaserAdvanced : MonoBehaviour
    {
        // SerializeField allows us to assign variables through the inspector
        [SerializeField] private GameObject laserRoot;
        [SerializeField] private AudioClip laserOnSfx;
        [SerializeField] private AudioClip laserOffSfx;
        [SerializeField] private AudioClip laserHumSfx;
        [SerializeField] private XRGrabInteractable interactable;
        [SerializeField] private float resizeSpeed = 1.0f;

        
        private AudioSource audioSource;
        private Vector3 laserFullSize;
        private bool laserOn;

        private void Awake()
        {
            
        }

        // Start is called before the first frame update
        void Start()
        {
            InitializeLaser();
        }

        private void Update()
        {
            if (laserOn && laserRoot.transform.localScale.x<laserFullSize.x)
            {
                laserRoot.transform.localScale += new Vector3(resizeSpeed, 0f, 0f);
            }
            else if (laserOn == false && laserRoot.transform.localScale.x>0)
            {
                laserRoot.transform.localScale -= new Vector3(resizeSpeed, 0f, 0f);
            }
            else if (laserOn == false)
            {
                laserRoot.SetActive(false);
            }
        }
        private void InitializeLaser()
        {
            //Disable laserRoot gameobject
            laserRoot.SetActive(false);

            //Add audio Source component and make it a 3D sound
            audioSource = gameObject.AddComponent<AudioSource>();
            audioSource.spatialBlend = 1;

            laserFullSize = laserRoot.transform.localScale;
            // make the laser small
            laserRoot.transform.localScale = new Vector3(0f, laserFullSize.localScale.y, laserFullSize.localScale.z);
        }

        public void ActivateLaser()
        {
            laserOn = true;
            // Enable laserRoot gameobject
            laserRoot.SetActive(true);

            //false if you do not want to make the sound loop, true if you want the sound to loop
            PlayLaserSound(laserOnSfx, false);
            PlayLaserSound(laserHumSfx, true);
        }

        public void DeActivateLaser()
        {
            laserOn = false;
            //laserRoot.SetActive(false);
            PlayLaserSound(laserOffSfx,false);
        }

        // Functions help us reuse code without typing it all over again
        private void PlayLaserSound(AudioClip sound, bool loop)
        {
            // if loop bool is true assign sound to audio source and play audio source
            if (loop)
            {
                audioSource.clip =sound;
                audioSource.Play(); 
            }
            else
            {
                // else stop the current audio source sound and play the sound once
                audioSource.Stop();
                audioSource.PlayOneShot(sound);
            }
            
        }

    }
}
