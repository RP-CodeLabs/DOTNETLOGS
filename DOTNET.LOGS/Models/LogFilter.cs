using System;
using System.ComponentModel.DataAnnotations;
using DOTNET.LOGS.Models.Extensions;

namespace DOTNET.LOGS.Models
{
    public class LogFilter
    {
        public EnvironmentType EnvironmentType { get; set; }
        [Range(1, int.MaxValue, ErrorMessage = "The Environments field is required.")]
        public string UserSelectedEnvironment { get; set; }
        [Required(ErrorMessage = "The Applications field is required.")]
        public string Application { get; set; }
        [Required, CustomDate]
        public DateTime StartDateTime { get; set; }
        [Required, CustomDate]
        public DateTime EndDateTime { get; set; }
    }
}