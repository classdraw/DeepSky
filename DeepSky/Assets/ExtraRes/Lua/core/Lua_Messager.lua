local Lua_Messager =BaseClass("Lua_Messager");

Lua_Messager.m_sName="Lua_Messager";
local DEBUG=0;

function Lua_Messager.Extend(target)
    if target.m_kComponents==nil then
        target.m_kComponents={};
    end
    
    if target.m_kComponents[Lua_Messager.m_sName]==nil then
        target.m_kComponents[Lua_Messager.m_sName]=Lua_Messager.New(target);
    end
end

function Lua_Messager:ctor(target)
    self.m_kTarget=target;
    self.m_kListeners={};
    self:ExportMethods();
end

function Lua_Messager:dtor()
    self.m_kListeners=nil;
end

function Lua_Messager:CleanListeners()
    self.m_kListeners={};
end

function Lua_Messager:ConvertEventName(eventName)
    if DEBUG==1 then
        return string.upper(tostring(eventName));
    else
        return eventName;
    end
end
---
---添加消息监听
---@param target table 监听主体
---@param eventName string 消息类型
---@param listener function 消息监听函数
---@param ... table 可选参数
function Lua_Messager:AddListener(target,eventName, listener,...)
    eventName = self:ConvertEventName(eventName);
    local t={...};
    self:AddListenerArg(target,eventName, listener,t);
    return self.m_Target;
end

function Lua_Messager:AddListenerArg(target,eventName,listener,arg)
    eventName = self:ConvertEventName(eventName);
    if self.m_kListeners[eventName]==nil then
        self.m_kListeners[eventName]={};
    end

    if self:HasListener(target,eventName,listener) then
        XLogger.LogError("AddListener Error!!!");
        return self.m_kTarget;
    end

    local t=self.m_kListeners[eventName];
    t[#t+1]={target,listener,arg or "nothing"};
    return self.m_kTarget;
end


function Lua_Messager:HasListener(target,eventName,listener)
    eventName = self:ConvertEventName(eventName);
    local t=self.m_kListeners[eventName];
    for i=#t,1,-1 do
        if t[i][1]==target and t[i][2]==listener then
            return true;
        end
    end

    return false;
end

function Lua_Messager:RemoveListener(target,eventName,listener)
    eventName = self:ConvertEventName(eventName);
    if self.m_kListeners[eventName]~=nil then
        local t=self.m_kListeners[eventName];
        for i=#t,1,-1 do
            if t[i][1]==target and t[i][2]==listener then
                table.remove(t,i);
                break;
            end
        end

        if #t==0 then
            self.m_kListeners[eventName]=nil;
        end
    end
    return self.m_kTarget;
end

function Lua_Messager:RemoveListenerByEventName(eventName)
    eventName = self:ConvertEventName(eventName);
    self.m_kListeners[eventName]=nil;
    return self.m_kTarget;
end

function Lua_Messager:RemoveListenerByTarget(target)
    if self.m_kListeners==nil then
        return self.m_kTarget;
    end

    for eventName,t in ipairs(self.m_kListeners) do
        self:RemoveListenerByTargetAndEventName(target,eventName);
    end
    return self.m_kTarget;
end

function Lua_Messager:RemoveListenerByTargetAndEventName(target,eventName)
    eventName = self:ConvertEventName(eventName);
    if self.m_kListeners==nil then
        return self.m_kTarget;
    end
    local t =self.m_kListeners[eventName];
    for i=#t,1,-1 do
        if t[i][1]==target then
            table.remove(t,i);
        end
    end

    return self.m_kTarget;

end

function Lua_Messager:BroadcastNoGC(eventName,num1,num2,num3,b1,b2,obj)
    eventName = self:ConvertEventName(eventName);
    if self.m_kListeners[eventName]==nil then
        return;
    end

    if self.gcEventDatas==nil then
        self.gcEventDatas={};
    end

    local findEmpty=self:_getOnEventData(num1,num2,num3,b1,b2,obj);
    self:_dispatchSelfNoGC(eventName,findEmpty);
    findEmpty.isUse=false;
    findEmpty.obj=nil;

    if #self.gcEventDatas>200 then
        for i=#self.gcEventDatas,1,-1 do
            if self.gcEventDatas[i].isUse==nil or self.gcEventDatas[i].isUse==false then
                table.remove(self.gcEventDatas,i);
            end
        end
    end
end

function Lua_Messager:_getOnEventData(num1,num2,num3,b1,b2,obj)
    local findEmpty=nil;
    for i=#self.gcEventDatas,1,-1 do
        if self.gcEventDatas[i].isUse==nil or self.gcEventDatas[i].isUse==false then
            findEmpty=self.gcEventDatas[i];
            break;
        end
    end

    if findEmpty==nil then
        findEmpty={};
        findEmpty.isUse=false;
        self.gcEventDatas[#self.gcEventDatas+1]=findEmpty;
    end

    findEmpty.obj=obj;
    findEmpty.num1=num1;
    findEmpty.num2=num2;
    findEmpty.num3=num3;
    findEmpty.bool1=b1;
    findEmpty.bool2=b2;
    findEmpty.isUse=true;

    return findEmpty;
end

function Lua_Messager:_dispatchSelfNoGC(eventName,data)
    if self.m_kListeners==nil then
        return;
    end
    local status,errorMsg;
    local t=self.m_kListeners[eventName];

    
    for i=#t,1,-1 do
        if t[i]~=nil then
            local target=t[i][1];
            local listener=t[i][2];
            local arg=t[i][3];
            if arg~="nothing" then
                status,errorMsg=pcall(listener,arg,data);
            else
                status,errorMsg=pcall(listener,data);
            end

            if not status then
                XLogger.LogError("dispatchSelfNoGC Error!!!");
            end
        end
    end
end

---
---方法装配到所需要的target类里
---@param methods string[] 方法名
function Lua_Messager:AssembleMethods(methods)
    self.m_ExportedMethods=methods;
    local target=self.m_kTarget;
    local com=self;
    for _,key in ipairs(methods) do
        if not target[key] then
            local m=com[key];
            target[key]=function(__,...)
                return m(com,...);
            end
        end

    end
end

function Lua_Messager:ExportMethods()
    self:AssembleMethods({
        "AddListener",
        "AddListenerArg",
        "HasListener",
        "RemoveListener",
        "RemoveListenerByEventName",
        "RemoveListenerByTarget",
        "RemoveListenerByTargetAndEventName",
        "BroadcastNoGC"
    });
    return self.m_kTarget;

end
return Lua_Messager