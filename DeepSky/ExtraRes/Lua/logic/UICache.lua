---uicache保存ui
local UICache = BaseClass("UICache")
local CacheTime=60;
---可以丢入到updatemanager更新处理逻辑
function UICache:ctor(rootTran)
    self.m_kUICtrls={};--key value
    self.m_kRootTran=rootTran;
    self.m_kUpdateMethod=handler(self,self.Update);
    UpdateManager:GetInstance():AddUpdate(self.m_kUpdateMethod);
end

function UICache:dtor()
    if self.m_kUpdateMethod~=nil then
        UpdateManager:GetInstance():RemoveUpdate(self.m_kUpdateMethod);
    end
    self.m_kUpdateMethod=nil;
end



function UICache:AddUI(uiCtrl)
    local time=GameUtils:GetRealStartTime();
    --显示层父节点处理
    local view=uiCtrl:GetView();
    if view~=nil then
        view.transform:SetParent(self.m_kRootTran);
    end
    --逻辑层塞入
    table.insert(self.m_kUICtrls,uiCtrl);
    --记录缓存时间
    uiCtrl.m_fCacheTime=time;
end

function UICache:GetUI(uiName)
    local cCount=#self.m_kUICtrls;
    for idx=1,cCount do
        local ctrl=self.m_kUICtrls[idx];
        if ctrl:GetUIName()==uiName then
            table.remove(self.m_kUICtrls,idx);
            return ctrl;
        end
    end
    return nil;
end

function UICache:ReleaseAll()
    local cCount=#self.m_kUICtrls;
    for idx=cCount,1,-1 do
        local ctrl=self.m_kUICtrls[idx];
        ctrl:Release();
    end
    self.m_kUICtrls={};
end

function UICache:Update()
    local time=GameUtils:GetRealStartTime();
    local cCount=#self.m_kUICtrls;
    for idx=cCount,1,-1 do
        local ctrl=self.m_kUICtrls[idx];
        if time-ctrl.m_fCacheTime>=CacheTime then
            ctrl:Release();
            table.remove(self.m_kUICtrls,idx);
        end
    end
end

return UICache;
