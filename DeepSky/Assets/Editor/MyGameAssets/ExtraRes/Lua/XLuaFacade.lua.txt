XLuaFacade = {
    Init = function(uiName)
    end
}


function XLuaFacade.EnterLogin()
    -- LS_NetworkManager:GetInstance():Init();
    App:GetAppStateManager():ChangeState(CS.Game.Fsm.LoginState.Index);
end
