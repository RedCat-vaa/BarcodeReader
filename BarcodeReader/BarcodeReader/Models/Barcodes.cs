using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace BarcodeReader.Models
{
    [Table("BarcodeTable")]
    public class Barcodes
    {
        [PrimaryKey, AutoIncrement, Column("_id")]
        public int Id { get; set; }
        public string Code { get; set; }
        public int docId { get; set; }

    }


}
