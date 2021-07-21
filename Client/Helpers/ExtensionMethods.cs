using MudBlazor;
using helping_hand.Models;

namespace helping_hand.Client.Helpers
{
    public static class ExtensionMethods
    {
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

    }
}
