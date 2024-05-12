local CSXLogger=CS.XLogger

local function getStatckInfo()
    local traceback = debug.traceback("", 2);
    local stackInfo=traceback;
    return stackInfo;
end

local LogPrefix="LuaLog:";


XLogger={
    Log=function(msg)
        if msg then
            if type(msg)~="string" then
                dump(msg);
            else
                CSXLogger.Log(LogPrefix..msg);
            end
        else
            dump(msg);
        end
    end,
    LogError=function(msg)
        if msg then
            if type(msg)~="string" then
                dump(msg);
            else
                CSXLogger.LogError(LogPrefix..msg);
            end
        else
            dump(msg);
        end
    end,
    LogWarn=function(msg)
        if msg then
            if type(msg)~="string" then
                dump(msg);
            else
                CSXLogger.LogWarn(LogPrefix..msg);
            end
        else
            dump(msg);
        end
    end,
    LogTest=function(msg)
        if msg then
            if type(msg)~="string" then
                dump(msg);
            else
                CSXLogger.LogTest(LogPrefix..msg);
            end
        else
            dump(msg);
        end
    end,
    LogTemp=function(msg)
        if msg then
            if type(msg)~="string" then
                dump(msg);
            else
                CSXLogger.LogTemp(LogPrefix..msg);
            end
        else
            dump(msg);
        end
    end,
    LogMassive=function(msg)
        if msg then
            if type(msg)~="string" then
                dump(msg);
            else
                CSXLogger.LogMassive(LogPrefix..msg);
            end
        else
            dump(msg);
        end
    end

}