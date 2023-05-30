using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace BarcodeReader.Models
{
    [Table("DocTable")]
    public class Documents
    {
        [PrimaryKey, AutoIncrement, Column("_id")]
        public int Id { get; set; }

        public int Count { get; set; }
        public bool IsSent { get; set; }

    }
}
