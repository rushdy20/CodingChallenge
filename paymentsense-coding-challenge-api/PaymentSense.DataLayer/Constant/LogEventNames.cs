namespace PaymentSense.DataLayer.Constant
{
    public struct LogEventNames
    {
        public struct DataLayer
        {
            public struct ServiceClient
            {
                public struct GetAllCountries
                {
                    public const string Enter = "DataLyer_ServiceClient_GetAllCountries_Enter";
                    public const string Exit = "DataLyer_ServiceClient_GetAllCountries_Exit";
                    public const string ResponseNotValid = "DataLyer_ServiceClient_GetAllCountries_ResponseNotValid";
                }

                public struct SendRequest
                {
                    public const string Enter = "DataLyer_ServiceClient_SendRequest_Enter";
                    public const string Exit = "DataLyer_ServiceClient_SendRequest_Exit";
                    public const string Exception = "DataLyer_ServiceClient_SendRequest_Exception";
                }

            }
        }
    }
}
