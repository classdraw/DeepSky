---
--- lualazy基础类 某些控件数据懒加载
---@author:jyy
--- @field super table
---

local setmetatable = setmetatable;

function LazyClass(classname,super)
    local cls = {
        __classname=classname,
        __class=cls,
        Get={},
    };
    local metaCall = {
        __call = function(_, ...)
            return cls.new(...)
        end
    };

    setmetatable(cls,metaCall);
    
    local Get = cls.Get;
    if super then
        --所有get数据遍历赋值
        for key, value in pairs(super.Get) do
            Get[key] = value;
        end

        cls.super=super;
    end

    function cls.__index(self,key)
        local func = cls[key];
        if func then--存在这个参数直接返回
            return func;
        end

        local getter = Get[key]
        if getter then--存在getter直接返回
            return getter(self);
        end

        if cls.super then--调用父类key
            return cls.super[key];
        end
        return nil;
    end

    function cls.__newindex(self, key, value)
        if Get[key] then
            assert(false, "can not set value to readonly property")
        end
        rawset(self, key, value)
    end

    function cls.New(...)
        local self = setmetatable({},cls)
        self.__vtbl = cls
        local function create(cls, ...)
            if cls.super then
                create(cls.super, ...)
            end

            if cls.ctor then
                cls.ctor(self, ...)
            end
        end
        create(cls, ...);

        self.Delete = function(self)
            local class = self.__vtbl;
            while class ~= nil do
                if class.dtor then
                    class.dtor(self);
                end
                class = class.super;
            end
        end
        return self;
    end

    return cls;
end