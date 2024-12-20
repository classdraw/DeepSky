using UnityEngine;
using Game.Fsm;
using XEngine.Utilities;
using UnityEngine.SceneManagement;
using Game.Scenes;

///启动项目入口
public class AppDelegate : Singleton<AppDelegate>
{
    /// <summary>
	/// 游戏总入口
	/// </summary>
	[RuntimeInitializeOnLoadMethod]
	public static void Main(){
		AppDelegate.GetInstance();
	}

    private bool m_Init=false;

    protected override void Init(){
        if(m_Init){
            return;
        }
        m_Init=true;
        if(SceneManager.GetActiveScene().name.Equals("Launcher")){
            StartClient();
        }
        
    }

 
    private void StartClient(){
        //配置设置
        this.SetSceneParam();
        //状态切换
        this.Next();
    }

    // private GameObject m_ConfigObj;
    //参数设置 后续steam可能需要自定义一些初始化设置
    private void SetSceneParam(){
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
        
		//设置横屏
		// Screen.orientation = ScreenOrientation.AutoRotation;
		Screen.autorotateToLandscapeLeft = true;
		Screen.autorotateToLandscapeRight = true;
		Screen.autorotateToPortrait = false;
		Screen.autorotateToPortraitUpsideDown = false;
    }

    private void Next(){
        if(SceneManager.GetActiveScene().name.Equals("Launcher")){
            AppStateManager.GetInstance().ChangeState(SplashState.Index);
        }
        
    }
}

