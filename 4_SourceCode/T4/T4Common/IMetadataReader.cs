using System.Collections.Generic;
using T4Common.Domain;

namespace T4Common
{
    public interface IMetadataReader
    {
        IList<Column> GetTableDetails(Table table, string owner);
        List<Table> GetTables(string owner);
        IList<string> GetOwners();
        List<string> GetSequences();
       //List<string> GetSequences(List<Table> table);
        //List<string> GetForeignKeyTables(string columnName);
    }
}