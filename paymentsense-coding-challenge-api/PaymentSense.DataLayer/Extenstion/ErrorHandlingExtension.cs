using System.Net;

namespace PaymentSense.DataLayer.Extenstion
{
    public static class ErrorHandlingExtension
    {
        public static void CheckForCommonError(this HttpStatusCode statusCode)
        {
            switch (statusCode)  //TODO EXTENT ALL THE EXCEPTION AND NEW THROW CUSTOM EXCEPTION
            {
                case HttpStatusCode.Conflict:
                    throw new HttpRequestException();

                default:
                    throw new Exception($"Status code returned: {(int)statusCode} - {statusCode}");
            }
        }
    }
}
