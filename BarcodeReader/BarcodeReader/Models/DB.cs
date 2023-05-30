using System;
using System.Collections.Generic;
using System.Text;
using SQLite;
using System.Linq;

namespace BarcodeReader.Models
{
    public class DB
    {
        SQLiteConnection database;

        public DB(string databasePath)
        {

            database = new SQLiteConnection(databasePath);
            database.CreateTable<Barcodes>();
            database.CreateTable<Documents>();
        }

        public IEnumerable<Documents> GetDocuments()
        {
            return database.Table<Documents>().GroupJoin(GetAllBarcodes(),
                d => d.Id,
                b => b.docId,
                (d, bars)=> new Documents()
                { Id = d.Id,
                  IsSent = d.IsSent,
                  Count = bars.Count()
                }).OrderBy(i => i.Id);
        }

        public Documents GetDocument(int id)
        {
            return database.Get<Documents>(id);
        }

        public IEnumerable<Barcodes> GetBarcodes(int DocId)
        {
            return database.Table<Barcodes>().Where(n => n.docId == DocId).OrderBy(i => i.Id);
        }
        public IEnumerable<Barcodes> GetAllBarcodes()
        {
            return database.Table<Barcodes>().OrderBy(i => i.Id);
        }

        public int CreateDoc(Documents doc)
        {
            return database.Insert(doc);
        }

        public int UpdateDoc(Documents doc)
        {
            return database.Update(doc);
        }

        public int CreateBar(Barcodes bar)
        {
            return database.Insert(bar);
        }

        public int DeleteDoc(int id)
        {
            return database.Delete<Documents>(id);
        }

        public int DeleteBar(int id)
        {
            return database.Delete<Barcodes>(id);
        }
    }

}
