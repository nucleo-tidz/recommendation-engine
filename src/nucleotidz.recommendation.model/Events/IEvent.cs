using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace nucleotidz.recommendation.model.Events
{
    public interface IEvent
    {
        string @event { get; set; }
        DateTime EventDate { get; set; }
    }
}
