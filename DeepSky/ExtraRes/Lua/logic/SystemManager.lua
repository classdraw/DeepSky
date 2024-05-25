local SystemManager = BaseClass("SystemManager",Singleton)

function SystemManager:ctor()
    self.m_kSystems={};
    self._updateMethod=handler(self,self.Update);
    UpdateManager:GetInstance():AddUpdate(self._updateMethod);
end

function SystemManager:dtor()
    if self._updateMethod~=nil then
        UpdateManager:GetInstance():RemoveUpdate(self._updateMethod);
    end
    self._updateMethod=nil;
end

function SystemManager:Init(assetPipeLine)
    self.m_kAssetPipeLine=assetPipeLine;

    --生成所有系统逻辑
    for k,v in pairs(enum_SystemType) do
        self:StartSystem(v);
    end
end

function SystemManager:UnInit()
    for k,v in pairs(enum_SystemType) do
        self:StopSystem(v);
    end
end

function SystemManager:Update()

end

function SystemManager:StartSystem(systemType)
    if self.m_kSystems[systemType]==nil then
        self.m_kSystems[systemType]=require("system."..systemType).New(self.m_kAssetPipeLine);
        if self.m_kSystems[systemType]==nil then
            XLogger.LogError("StartSystem."..systemType);
        else
            self.m_kSystems[systemType]:Init();
        end
        
        self.m_kSystems[systemType]:OnStart();
    end
end

function SystemManager:StopSystem(systemType)
    if self.m_kSystems[systemType]==nil then
        return;
    end
    self.m_kSystems[systemType]:OnStop();
    self.m_kSystems[systemType]=nil;
end

function SystemManager:GetSystem(systemType)
    if self.m_kSystems[systemType]==nil then
        XLogger.LogError("GetSystem."..systemType);
        return nil;
    end
    return self.m_kSystems[systemType];
end

return SystemManager;