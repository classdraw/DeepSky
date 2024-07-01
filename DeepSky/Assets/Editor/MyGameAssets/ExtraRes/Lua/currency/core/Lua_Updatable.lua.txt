-- 会自动更新的基类
local BaseUpdateClass=BaseClass("BaseUpdateClass");

function BaseUpdateClass:ctor()
    self.m_fTimerInterval = 0.05;
    self.m_kUpdateMethod=handler(self,self._updateLogic);
    self.m_iTimerId = XFacade.CallRepeat(self.timerInterval,self.m_kUpdateMethod);
end

function BaseUpdateClass:dtor()
    if self.m_iTimerId~=nil then
        XFacade.StopTime(self.m_iTimerId);
        self.m_iTimerId=nil;
    end
    self.m_kUpdateMethod=nil;
end

function BaseUpdateClass:_updateLogic()

end

return BaseUpdateClass;