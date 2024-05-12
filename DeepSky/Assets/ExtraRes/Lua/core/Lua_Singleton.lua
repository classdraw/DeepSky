--单利基类
local Lua_Singleton=BaseClass("Lua_Singleton");

function Lua_Singleton:ctor()
    rawset(self.class,"Instance",self);
end

function Lua_Singleton:dtor()
    rawset(self.class,"Instance",nil);
end

function Lua_Singleton:GetInstance()
    if rawget(self,"Instance")==nil then
        rawset(self,"Instance",self.New());
    end
    assert(self.Instance~=nil);
    return self.Instance;
end

function Lua_Singleton:Delete()
    self.Instance=nil;
end
return Lua_Singleton;

