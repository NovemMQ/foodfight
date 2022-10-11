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
    
        public override void Pause()
        {
            base.Pause();
            PauseMenuUI.SetActive(true);
        }
        
        public override void Resume()
        {

            base.Resume();
            PauseMenuUI.SetActive(false);
        }
        
        public override void EndExperience()
        {
            End();
        }
    }
}