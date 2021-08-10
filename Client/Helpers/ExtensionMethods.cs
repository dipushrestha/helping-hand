using System.Linq;
using System.Collections.Generic;

using MudBlazor;
using helping_hand.Models;

namespace helping_hand.Client.Helpers
{
    public static class ExtensionMethods
    {
        public static int GetUrgencyLevel(List<Urgency> urgencies, string urgencyLabel)
        {
            var urgency = urgencies.Find(u => u.Label == urgencyLabel);

            return urgency is not null ? urgency.Level : 99;
        }

        public static Color GetUrgencyTagColor(Urgency urgency)
        {
            return urgency.Level switch
            {
                1 => Color.Error,
                2 => Color.Warning,
                3 => Color.Info,
                _ => Color.Info,
            };
        }

        public static Color GetUrgencyTagColor(string urgencyLabel)
        {
            return urgencyLabel switch
            {
                "Urgent" => Color.Error,
                "Moderate" => Color.Warning,
                "Not Urgent" => Color.Info,
                _ => Color.Info
            };
        }

        public static string GetIcon(string service)
        {
            return service switch
            {
                "Bed" => Icons.Material.Filled.Hotel,
                "Food" => Icons.Material.Filled.FoodBank,
                "Medicine" => Icons.Material.Filled.MedicalServices,
                _ => Icons.Material.Filled.AddToQueue
            };
        }

        public static Dictionary<string, int> GetHelpServicesCount(
            List<HelpService> helpServices,
            List<HelpRequest> helpRequests)
        {
            Dictionary<string, int> servicesCount = new();

            foreach (var helpService in helpServices)
            {
                foreach (var helpRequest in helpRequests)
                {
                    int count;

                    if (helpRequest.Services.Contains(helpService.Service))
                    {
                        if (servicesCount.TryGetValue(helpService.Service, out count))
                        {
                            servicesCount[helpService.Service] = count + 1;
                        }
                        else
                        {
                            servicesCount.Add(helpService.Service, 1);
                        }
                    }
                }
            }

            return servicesCount.Where(i => i.Value > 0).ToDictionary(i => i.Key, i => i.Value);
        }
    }
}
