using System;
namespace XamarinEnterpriseApp.Xamarin.Core.Constants
{
    public enum ErrorCodes
    {
        INTERNAL_SERVER_ERROR = 1000,

        SERVER_AUTHENTICATION_ERROR = 4000, // Most probably this could be fired when the Request HTTP Status codes are HttpStatusCode.Forbidden or HttpStatusCode.Unauthorized
        REMORTE_RESOURCE_NOT_FOUND_ERROR = 4041, // HTTP Resource Not Found for the Given URI

        GENERAL_ERROR = 5000, // Internal Client Side Error: General exception. This could be anything.
        INVALID_CAST_ERROR = 5001, // Internal Client Side Error: Invalid Cast Exception should have been fired
        JSON_READER_ERROR = 5002, // Internal Client Side Error: Json Reader Exception should have been fired
        TASK_CANCELLED_ERROR = 5003,//Task gets cancelled by the OS.
    }
}
