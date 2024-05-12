local UpdateManager = BaseClass("UpdateManager",Singleton)

local UpdateMsgName="UpdateMsg";
local LateUpdateMsgName="LateUpdateMsg";
local FixedUpdateMsgName="FixedUpdateMsg";

function UpdateManager:ctor()
    Lua_Messager.Extend(self);
    self._update_handler=nil;
    self._lateupdate_handler=nil;
    self._fixedupdate_handler=nil;
end

function UpdateManager:dtor()
    
end

local function UpdateHandler(self)
    self:BroadcastNoGC(UpdateMsgName);
end


local function LateUpdateHandler(self)

end

local function FixedUpdateHandler(self)

end

function UpdateManager:Init()
    self:Release();
    self._update_handler=UpdateBeat:CreateListener(UpdateHandler,UpdateManager:GetInstance());
    self._lateupdate_handler=LateUpdateBeat:CreateListener(LateUpdateHandler,UpdateManager:GetInstance());
    self._fixedupdate_handler=FixedUpdateBeat:CreateListener(FixedUpdateHandler,UpdateManager:GetInstance());
    UpdateBeat:AddListener(self._update_handler);
    LateUpdateBeat:AddListener(self._lateupdate_handler);
    FixedUpdateBeat:AddListener(self._fixedupdate_handler);
end

function UpdateManager:Release()
    if self._update_handler~=nil then
        UpdateBeat:RemoveListener(self._update_handler);
        self._update_handler=nil;
    end

    if self._lateupdate_handler~=nil then
        LateUpdateBeat:RemoveListener(self._lateupdate_handler);
        self._lateupdate_handler=nil;
    end

    if self._fixedupdate_handler~=nil then
        FixedUpdateBeat:RemoveListener(self._fixedupdate_handler);
        self._fixedupdate_handler=nil;
    end
end

function UpdateManager:AddUpdate(kListener)
    self:AddListener(self,UpdateMsgName,kListener);
end

function UpdateManager:AddLateUpdate(kListener)
    self:AddListener(self,LateUpdateMsgName,kListener);
end

function UpdateManager:AddFixedUpdate(kListener)
    self:AddListener(self,FixedUpdateMsgName,kListener);
end

function UpdateManager:RemoveUpdate(kListener)
    self:RemoveListener(self,UpdateMsgName,kListener);
end

function UpdateManager:RemoveLateUpdate(kListener)
    self:RemoveListener(self,LateUpdateMsgName,kListener);
end

function UpdateManager:RemoveFixedUpdate(kListener)
    self:RemoveListener(self,FixedUpdateMsgName,kListener);
end

return UpdateManager;