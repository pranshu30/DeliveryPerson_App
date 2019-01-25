﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeliveryApp1.Model
{
    public class Delivery
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public double OriginLatitude { get; set; }

        public double OriginLongitude { get; set; }

        public double DestinationLatitude { get; set; }

        public double DestinationLongitude { get; set; }

        /// <summary>
        /// 0 = waiting delivery person
        /// 1 = being delivered
        /// 2 = delivered
        /// </summary>

        public int Status { get; set; }

        public string DeliveryPersonId { get; set; }

        public static async Task<bool> MarkAsPickedUp(string deliveryId, string deliveryPersonId)
        {
            try
            {
                var delivery = (await AzureHelper.MobileService.GetTable<Delivery>().Where(d => d.Id == deliveryId).ToListAsync()).FirstOrDefault();

                delivery.Status = 1;
                delivery.DeliveryPersonId = deliveryPersonId;
                await AzureHelper.MobileService.GetTable<Delivery>().UpdateAsync(delivery);
                return true;
            }
            catch(Exception ex)
            {
                return false;
            }
        }

        public static async Task<bool> MarkAsDelivered(string deliveryId)
        {
            try
            {
                var delivery = (await AzureHelper.MobileService.GetTable<Delivery>().Where(d => d.Id == deliveryId).ToListAsync()).FirstOrDefault();

                delivery.Status = 2;
                
                await AzureHelper.MobileService.GetTable<Delivery>().UpdateAsync(delivery);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }



        public static async Task<List<Delivery>> GetDeliveries()
        {
            List<Delivery> deliveries = new List<Delivery>();

           // deliveries = await AzureHelper.MobileService.GetTable<Delivery>().ToListAsync();
            // For List Fragement
           deliveries = await AzureHelper.MobileService.GetTable<Delivery>().Where(d => d.Status != 2).ToListAsync();

            return deliveries;
        }

        public static async Task<List<Delivery>> GetDelivered()
        {
            List<Delivery> delivered = new List<Delivery>();

            delivered = await AzureHelper.MobileService.GetTable<Delivery>().Where(d => d.Status == 2).ToListAsync();

            return delivered;
        }

        public static async Task<List<Delivery>> GetDelivered(string id)
        {
            List<Delivery> delivered = new List<Delivery>();

            delivered = await AzureHelper.MobileService.GetTable<Delivery>().Where(d => d.Status == 2 && d.DeliveryPersonId == id).ToListAsync();

            return delivered;
        }

        public static async Task<List<Delivery>> GetBeingDelivered(string id)
        {
            List<Delivery> delivered = new List<Delivery>();

            delivered = await AzureHelper.MobileService.GetTable<Delivery>().Where(d => d.Status == 1 && d.DeliveryPersonId == id).ToListAsync();

            return delivered;
        }

        public static async Task<List<Delivery>> GetWaiting(string userId)
        {
            List<Delivery> delivered = new List<Delivery>();

            delivered = await AzureHelper.MobileService.GetTable<Delivery>().Where(d => d.Status == 0).ToListAsync();

            return delivered;
        }


        public static async Task<bool> InsertDelivery(Delivery delivery)
        {
            return await AzureHelper.Insert<Delivery>(delivery);
        }

        public override string ToString()
        {
            return $"{Name} - {Status}";
        }

    }
}
