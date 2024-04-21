namespace XEngine.Loader
{
    public class AssetCoreRequest:IRequest
    {
        private bool m_IsDone=false;
        public bool IsDone{get{return m_IsDone;}}

        private string m_AssetPath;//资源路径
        public string AssetPath{get{return m_AssetPath;}}

    }
}