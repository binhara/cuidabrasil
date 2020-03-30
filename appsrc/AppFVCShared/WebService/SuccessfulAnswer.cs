namespace AppFVCShared.WebService
{
    public class SuccessfulAnswer
    {
        public bool Success { get; set; }

        private string _message;
        public string Message
        {
            get
            {
                switch (_message)
                {
                    case "An error occurred while sending the request":
                    case "Object reference not set to an instance of an object":
                        return "Erro ao enviar a solicitação, verifique sua internet";
                    default:
                        return _message;
                }
            }
            set
            {
                _message = value;
            }
        }
        public int? Code { get; set; }
        public string TitleMessage { get; set; }

    }
}
