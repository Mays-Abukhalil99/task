using onlineShop.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace onlineShop.Mapping
{
    public static class Helpers
    {
        public static ServiceResponse<T> getServiceResponse<T>(bool status, string errorMessage, T data = null) where T : class
        {
            var ServiceResponse = new ServiceResponse<T>();
            ServiceResponse.Success = status;
            ServiceResponse.Message = errorMessage;
            ServiceResponse.Data = data;
            return ServiceResponse;
        }
    }
}
