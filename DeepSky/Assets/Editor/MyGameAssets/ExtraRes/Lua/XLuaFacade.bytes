XLuaFacade = {
    Init = function(uiName)
    end
}


function XLuaFacade.EnterLogin()
    XLuaFacade.InitModels();

    -- LS_NetworkManager:GetInstance():Init();
    App:GetAppStateManager():ChangeState(CS.Game.Fsm.LoginState.Index);
end

--进入登录 有些model初始化
function XLuaFacade.InitModels()
    -- local csSharp=App:GetSystem("LuaCSharpModel");--初始化一些需要的model
end