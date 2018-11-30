namespace TimeClock.Models.LMS_Models
{
    using System.Collections.Generic;

    public class LMSGroup
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Key { get; set; }
        public string price { get; set; }
        public string owner_id { get; set; }
        public string belongs_to_branch { get; set; }
        public string max_redemptions { get; set; }
        public string redemptions_sofar { get; set; }

        public List<LMSUserSummarized> users { get; set; }
    }
}