local _class={};

ClassType={
    class=1,
    instance =2,
}

function BaseClass(classname,super)
    local cls={
        __cname=classname,
        __ctype=ClassType.class,
        ctor=false,
        dtor=false,
        super=super
    };

    cls.IsTypeName = function(classname)
        return cls.__cname==classname;
    end

    cls.New = function(...)
        --生成类对象
        local obj={};
        obj.class=cls;
        obj.__ctype=ClassType.instance;
        obj.__cname=classname;

        --注册基类方法
        setmetatable(obj,{__index=_class[cls]});

        --递归调用初始化方法
        do
            local create
            create = function(c, ...)
                if c.super then
                    create(c.super, ...)
                end
                if c.ctor then
                    c.ctor(obj, ...)
                end
            end

            create(cls, ...)
        end

        -- 注册一个delete方法
        obj.Delete = function(self)
            -- 父类析构自动调用
            if self == nil then
                return nil
            end

            local class = self.class
            while class ~= nil do
                if class.dtor then
                    class.dtor(self)
                end
                class = class.super
            end
        end

        return obj

    end

    local vtbl = {}
    _class[cls] = vtbl

    setmetatable(cls, {
        __newindex = function(t,k,v)
            vtbl[k] = v
        end
    ,
        --For call parent method
        __index = vtbl,
    }) 

    if super then
        setmetatable(vtbl, {
            __index = super
            -- function(t,k)
            --     local ret = _class[super][k]
            --     --do not do accept, make hot update work right!
            --     --vtbl[k] = ret
            --     return ret
            -- end
        })
    end

    return cls
end

function DelClass(cls)
    _class[cls] = nil
end 