local GameSettingSystem = BaseClass("GameSettingSystem", require("system.BaseSystem"))
-- local AppEvent = require("consts.AppEvent")
-- local Event = AppEvent.Event;

GameSettingSystem.PlayerPrefsData={
    Mute={key="Mute",defaultValue=0},
    CloseToWhere={key="CloseToWhere",defaultValue=1},--关闭主界面后 要干嘛  0退出程序，1最小化系统
    OpenNeedClose={key="OpenNeedClose",defaultValue=0},--打开游戏后 需要关闭自身嘛？
}

function GameSettingSystem:Init()
    -- self:bindEvents()
    XLogger.Log("GameSettingSystem:Init");
end

function GameSettingSystem:OnStart()
    XLogger.Log("GameSettingSystem:OnStart");
end

-- ---初始化玩家一些自定义参数
-- function GameSettingSystem:InitSetPlayerParam()
--     local mute=self:GetPrefsDataValue(Model.PlayerPrefsData.Mute);
--     App:GetGameSettingManager().IsMute=mute==1;

--     local closeToWhere=self:GetPrefsDataValue(Model.PlayerPrefsData.CloseToWhere);
--     App:GetGameSettingManager().CloseToWhere=closeToWhere;

    
--     local openNeedClose=self:GetPrefsDataValue(Model.PlayerPrefsData.OpenNeedClose);
--     App:GetGameSettingManager().OpenNeedClose=openNeedClose==1;
-- end

-- function GameSettingSystem:GetPrefsDataValue(data)
--     if CS.UnityEngine.PlayerPrefs.HasKey(data.key) == true then
--         return CS.UnityEngine.PlayerPrefs.GetInt(data.key);
--     else
--         return data.defaultValue;
--     end
-- end


-- function GameSettingSystem:SetMute(val)--bool
--     local dd=0;
--     if val==true then
--         dd=1;
--     end
--     CS.UnityEngine.PlayerPrefs.SetInt(Model.PlayerPrefsData.Mute.key, dd);
--     App:GetGameSettingManager().IsMute=val;
-- end

-- function GameSettingSystem:SetCloseToWhere(val)--int

--     CS.UnityEngine.PlayerPrefs.SetInt(Model.PlayerPrefsData.CloseToWhere.key, val);
--     App:GetGameSettingManager().CloseToWhere=val;
-- end

-- function GameSettingSystem:SetOpenNeedClose(val)--bool
--     local dd=0;
--     if val==true then
--         dd=1;
--     end
--     CS.UnityEngine.PlayerPrefs.SetInt(GameSettingSystem.PlayerPrefsData.OpenNeedClose.key, dd);
--     App:GetGameSettingManager().OpenNeedClose=val;
-- end

function GameSettingSystem:Reset()

end

-- function GameSettingSystem:bindEvents()

-- end




return GameSettingSystem