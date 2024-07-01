local BaseSystem = BaseClass("BaseSystem")
function BaseSystem:ctor(assetPipeLine)
    self.m_kAssetPipeLine=assetPipeLine;
end

function BaseSystem:dtor()

end

function BaseSystem:Update()

end


function BaseSystem:OnStart()

end

function BaseSystem:OnStop()

end

return BaseSystem;