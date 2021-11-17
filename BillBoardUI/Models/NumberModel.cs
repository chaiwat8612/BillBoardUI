using System;

namespace BillBoardUI.Models
{
    public class NumberModel
    {
        public string numberId { get; set; } = "";
        public string status { get; set; } = "";
        public int numberValue { get; set; }
    }

    public class SaveNewNumberModel
    {
        public string status { get; set; } = "";
        public int numberValue { get; set; }
    }
}