using System;
using System.ComponentModel.DataAnnotations;

namespace cShortner.Frontend.Models
{
    public class InputUrl
    {
        public string Input { get; set; }

        public InputUrl()
        {
            Input = string.Empty;
        }
    }
}
