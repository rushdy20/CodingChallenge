namespace PaymentSense.BusinessLayer.Constants
{
    public struct LogEventNames
    {
        public struct CountryManager
        {
            public struct GetAllCountry
            {
                public const string Enter = "CountryManager_GetAllCountry_Enter";
                public const string Exit = "CountryManager_GetAllCountry_Exit";
            }

            public struct GetCountryByCallingCode
            {
                public const string Enter = "CountryManager_GetCountryByCallingCode_Enter";
                public const string Exit = "CountryManager_GetCountryByCallingCode_Exit";
            }

        }

        public struct CacheManager
        {
            public struct Set
            {
                public const string Enter = "CacheManager_Set_Enter";
                public const string Exit = "CacheManager_Set_Exit";
                public const string Exception = "CacheManager_Set_Exception";
            }

            public struct Remove
            {
                public const string Enter = "CacheManager_Remove_Enter";
                public const string Exit = "CacheManager_Remove_Exit";
                public const string Exception = "CacheManager_Remove_Exception";
            }

            public struct Get
            {
                public const string Enter = "CacheManager_Get_Enter";
                public const string Exit = "CacheManager_Get_Exit";
                public const string Exception = "CacheManager_Get_Exception";
            }

        }
    }
}
