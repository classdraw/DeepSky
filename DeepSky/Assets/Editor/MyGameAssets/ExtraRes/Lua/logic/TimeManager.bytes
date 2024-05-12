local TimeManager = BaseClass("TimeManager",Singleton)

---可以丢入到updatemanager更新处理逻辑
function TimeManager:ctor()
    self._updateMethod=handler(self,self.Update);
    UpdateManager:GetInstance():AddUpdate(self._updateMethod);
end

function TimeManager:dtor()
    if self._updateMethod~=nil then
        UpdateManager:GetInstance():RemoveUpdate(self._updateMethod);
    end
    self._updateMethod=nil;
end

function TimeManager:Init()

end

function TimeManager:Update()

end

return TimeManager;