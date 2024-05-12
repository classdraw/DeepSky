--[[
    全局事件操作类
]]
local event={
    ON_RESUME = "onResume", --游戏重启
    ON_PAUSE = "onPause", --游戏暂停
    ON_RECEIVE_DATA = "onReceiveData", --收到服务端数据
    ON_LEAVE_MAP_BEFORE="ON_LEAVE_MAP_BEFORE",--开始离开场景
    ON_ENTER_MAP_AFTER="ON_ENTER_MAP_AFTER",--进入场景后最终消息
    ON_TRIGGER_ENTER="ON_TRIGGER_ENTER",--进入场景触发器
    ON_TRIGGER_EXIT="ON_TRIGGER_EXIT",--离开场景触发器
};

local Event = Class("Event",require("system.ValueObject"))
function Event:ctor(name, msg)
    Event.super.ctor(self)
    self.name = name
    self.msg = msg
end

function Event:Init()
    self.name = ""
    self.target = {}
end


event.Event = Event

--[[Begin ReceiveDataEvent]]
local ReceiveDataEvent = Class("ReceiveDataEvent",Event)

function ReceiveDataEvent:ctor(msgId, msg)
    ReceiveDataEvent.super.ctor(self,msgId) --特殊处理这个消息名称 优化消息发送
    self.msg =msg;
end

function event.GetReceiveDataEventName(opCode,subCode)
    return opCode.."_"..subCode;
end

function event.GetReceiveDataEventNameEnum(opCode,subCode)
    opCode=CSGameUtils.GetOpCodeEnumValue(opCode);
    subCode=CSGameUtils.GetSubC2SEnumValue(subCode);
    return opCode.."_"..subCode;
end

event.ReceiveDataEvent = ReceiveDataEvent
--[[End ReceiveDataEvent]]

return event