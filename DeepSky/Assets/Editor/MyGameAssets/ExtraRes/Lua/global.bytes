--全局的数据
local math=math;
math.pow=math.pow or function(x,y)
    return x^y;
end

math.mod=math.mod or function(x,y)
    return x%y;
end

function tostring_table_value(tb)
    if "table" ~= type(tb) then
        tb = tostring(tb)
    else
        for key, var in pairs(tb) do
            tb[key] = tostring(var)
        end
    end
    return tb
end

function make_key(...)
    local btl = {select(1,...)}
    return table.concat(btl,"_")
end

function require_safe(modname)
    return require(modname)
end

binding_exec = function(source,target,fun,name)
    if source and target and fun and name then
        source:addEventListener(target,fun,name)
        fun(target)
    end
end

binding = function(source,target,fun,name)
    if source and target and fun and name then
        source:addEventListener(target,fun,name)
    end
end


unbinding = function(source,target,name)
    if source and target and name then
        source:removeAllEventListenersByTargetAndName(target,name)
    end
end

unbinding_all = function(source,target)
    if source and target then
        source:removeAllEventListenersByTarget(target)
    end
end

----[[string转换为table]]
----配置为table模式，去除最外层的花括号
stringToTable = function(str)
    if tonumber(str) ~= nil then
        return {tonumber(str)}
    end
    return loadstring("return{"..str.."}")()
end

splitString = function(str)
    local ret = {}
    for c in string.gmatch(str,"[^,]+") do
        ret[#ret + 1] = c
    end
    return ret
end


-- const = function (cls)
--     local tb = require_safe("model."..cls)
--     if tb ~= nil then return tb end
--     return require("consts."..cls)
-- end


safe_call = function(target,funName,...)
    if nil == target then return end
    local fun = target[funName]
    if nil == fun or type(fun)~="function" then return end
    fun(target,...)
end

getBitBool = function(source,num)
    if num < 1 then num =1 end
    local value =  source >> (num-1) & 1
    return 0~=value
end

----[[本地化]]
lang = function(key,...)
    local lan = require("utils.LangUtils");
    return lan(key,...)
end