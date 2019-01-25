using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.MobileServices;

namespace DeliveryApp1.Model
{
    public class AzureHelper
    {
        public static MobileServiceClient MobileService = new MobileServiceClient(
            "Azure Link");


        //Generic method
        public static async Task<bool> Insert<T>(T objecttoInsert)
        {
            try
            {
                await AzureHelper.MobileService.GetTable<T>().InsertAsync(objecttoInsert);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

    }
}
