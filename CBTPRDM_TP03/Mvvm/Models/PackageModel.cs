/*
Nome: Kaik Persike Maiorquino
Prontuário: CB3029689

Nome: Luiz Gustavo Verissimo Monteiro
Prontuário: CB3030326
*/
using System;
using System.Collections.Generic;
using System.Text;

namespace CBTPRDM_TP03.Mvvm.Models
{
    public class PackageModel
    {
        public string TrackingCode { get; set; }
        public string Status { get; set; }
        public DateTime ShippingDate { get; set; }
        public DateTime EstimatedDeliveryDate { get; set; }
        public string CurrentLocation { get; set; }
        public List<TrackingEvent> History { get; set; } = new();
    }

    public class TrackingEvent
    {
        public DateTime Date { get; set; }
        public string Description { get; set; }
        public string Location { get; set; }
    }
}
