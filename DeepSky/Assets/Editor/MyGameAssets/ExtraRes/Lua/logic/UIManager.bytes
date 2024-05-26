local UIManager = BaseClass("UIManager",Singleton)


function UIManager:ctor()

end

function UIManager:Init()
    self.m_kUIs={};
    self.m_kUIRootHandle=GameResourceManager.GetInstance():LoadResourceSync("UI_UIRoot");
    self.m_kUIRootObj=self.m_kUIRootHandle:GetGameObject();
    GameUtils.SetGameObjectDontDestroy(self.m_kUIRootObj);

    local cacheRoot=self.m_kUIRootObj.transform:Find("RootCanvas/Cache");
    self.m_kUICache=require("logic.UICache").New(cacheRoot);

    local overlayCanvas=self.m_kUIRootObj.transform:Find("RootCanvas"):GetComponent("Canvas");
    SystemUtils.SetUIConfig(overlayCanvas);
end


return UIManager;