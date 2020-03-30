using FCVLibWS;

namespace AppFVCShared.WebService
{
    public class BaseWs
    {
       
        protected Client ObjClient;
        protected SuccessfulAnswer ObjSuccessfulAnswer;

        public BaseWs(Client objClient)
        {
            ObjClient = objClient;
        }
    }
}
