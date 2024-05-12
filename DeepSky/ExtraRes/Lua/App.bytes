--应用程序 一些常用方法的入口
local App = Class("App");
local AppEvent = require("consts.AppEvent")
local Event = AppEvent.Event;

function  App:Init()
    UpdateManager:GetInstance():Init();
    TimeManager:GetInstance():Init();

    SystemManager:GetInstance():Init(nil);
    -- AssetManager:GetInstance():Init();


    self.appStateManager=nil;
    self.audioManager=nil;
    self.resourcesManager=nil;
    self.gameSettingManager=nil;
    self.uiManager=nil;
    self.toastManager=nil;
    self.photonManager=nil;
    self.luaCSharpAgent=nil;
    self.luaScriptManager=nil;
    self.netHandlerManager=nil;
    self.world=nil;
    -- self.configManagerLua=require("managers.LocalConfigManager").New();--本地lua资源访问
    -- self.luaTimeManager = require("managers.LuaTimeManager").New();--lua层定时器
    -- self.netManagerLua=require("managers.NetworkManager").New();--lua层网络操作
end

function App:UnInit()
    SystemManager:GetInstance():UnInit();
    -- AssetManager:GetInstance():UnInit();
end


function App:Run()
    -- XLuaFacade.EnterLogin();
end

function App:StartSystem(key)
    SystemManager:GetInstance():StartSystem(key);
end

function App:StopSystem(key)
    SystemManager:GetInstance():StopSystem(key);
end

function App:Destroy()
    -- XLogger.LogError("App:Destroy")
end

function App:OnResume()
    -- XLogger.LogError("App:OnResume")
end

function App:OnPause()
    -- XLogger.LogError("App:OnPause")
end

return App;