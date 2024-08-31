using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace nucleotidz.recommendation.model.Events
{
    public class ProductCreatedEvent : IEvent
    {
        public string @event { get; set; }
        public DateTime EventDate { get; set; } = DateTime.UtcNow;

        public string Code { get; set; }
        public string Description { get; set; }
    }
}
