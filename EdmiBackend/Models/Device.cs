using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EdmiBackend.Models
{
    public class Device
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string SerialNumber { get; set; }

        public string FirmwareVersion { get; set; }

        public string State { get; set; }

        [Required]
        public string Type { get; set; }

        public string IP { get; set; }

        public string Port { get; set; }
    }
}
