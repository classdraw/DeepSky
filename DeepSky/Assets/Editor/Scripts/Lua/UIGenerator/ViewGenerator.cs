namespace UIGenerator
{
    public class ViewGenerator
    {
        public string Generate(string uiName)
        {
            _uiName = uiName;
            return GenerateLuaStr();
        }

        #region fields

        private string _uiName;

        #endregion
        
        private string GenerateLuaStr()
        {
            return string.Format(Template, _uiName);
        }

        //0:uiName
        private const string Template = @"---@class UI_{0}View : UIBaseView
local View = BaseClass('UI_{0}View', require('coreui.UIBaseView'))

function View:ctor()
end

function View:dtor()
end

function View:Init()
end

return View";
    }
}