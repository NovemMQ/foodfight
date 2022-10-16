namespace Liminal.Experience
{
    using Liminal.SDK.Core;
    using UnityEngine;
    
    /// <summary>
    /// This is your base experience class,
    /// you may change the contents of the methods in this class to best suit your app.
    /// </summary>
    public class MyExperienceApp : ExperienceApp
    {
        [SerializeField] private GameObject PauseMenuUI;
        private bool isGameOver = false;
        public bool IsGameOver { set => isGameOver = value; }

        public override void Pause()
        {
            base.Pause();
            if (!isGameOver)
            {
                PauseMenuUI.SetActive(true);
            }
        }
        
        public override void Resume()
        {
            base.Resume();
            if (!isGameOver)
            {
                PauseMenuUI.SetActive(false);
            }
        }
        
        public override void EndExperience()
        {
            Debug.Log("In EndExperience function");
            End();
        }
    }
}