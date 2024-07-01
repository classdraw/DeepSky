--[[
    基础的数据结构
]]

local ValueObject = BaseClass("ValueObject")
function ValueObject:ctor(...)
    self.synced=false;
    self:Init(...);
end

--[[需要把所有该vo的属性定义好
不允许读取不在初始化创建的属性
不允许在初始化之外创建新的属性
]]
function ValueObject:Init(...)

end

function ValueObject:exportEventProtocol()
    EventProtocol.Extend(self)
end

function ValueObject:parse(val,cloneTable)
    if val==nil then
        return self;
    end

    local type = type
    local getmetatable = getmetatable
    local rawget = rawget
    local Clone = Clone

    for key,value in pairs(val) do
        if(type(value)~="function" and key~="class" and key ~= "components_") then --排除辅助功能对游戏数据的影响
            if nil==getmetatable(ValueObject) or rawget(self,key) ~= nil then
                self[key] = value
                if true == cloneTable and type(value) =="table" and key ~= "baseInfo" then
                    self[key] = Clone(value)
                else
                    self[key] = value
                end
        else
            print(string.format("%s's property (%s) is not found!",self.class.__cname,key)) --提示未找到对应属性
        end
        end
    end
    self.synced = true
    return self
end


function ValueObject:Reset()
    self.synced = false
end

function ValueObject:Clone()
    local obj = self.class.New()
        obj:parseInfo(self,true)
    return obj
end

return ValueObject