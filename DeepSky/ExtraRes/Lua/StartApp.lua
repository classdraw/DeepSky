collectgarbage("collect")
-- avoid memory leak
collectgarbage("setpause", 100)
collectgarbage("setstepmul", 5000)

function __G__TRACKBACK__(errorMessage)
    print("----------------------------------------")
    print("LUA ERROR: " .. tostring(errorMessage) .. "\n")
    print(debug.traceback("", 2))
    print("----------------------------------------")
end

--改写print  不做处理
print = print or function(...) end
printE = printE or function(...) end
printW = printW or function(...) end

function InitGame()
    -- XLogger=require("utils.XLogger").New();
    App = require("App").New()
    App:Init();
    -- --初始化游戏
    XLogger.Log("StartApp.InitGame");
    App:Run();

end


function DestroyApp()
    if nil ~= App then
        App:Destroy()
    end
end


function OnResume()
    if nil ~= App then
        App:OnResume()
    end
end

function OnPause()
    if nil ~= App then
        App:OnPause()
    end
end


function LuaUpdate(deltaTime,unscaledDeltaTime)
    if UpdateBeat~=nil then
        UpdateBeat();
    end
end

function LuaLateUpdate()
    if LateUpdateBeat~=nil then
        LateUpdateBeat();
    end
end

function LuaFixedUpdate()
    if FixedUpdateBeat~=nil then
        FixedUpdateBeat();
    end
end