using System.Data.SQLite;
using tag_h.Helper.Extensions;
using tag_h.Model;


namespace tag_h.Persistence
{
    static class SQLExtensions
    {
        public static HImage GetHImage(this SQLiteDataReader dataReader)
        {
            return new HImage(
                    dataReader.GetInt32(0), 
                    dataReader.GetString(1), 
                    dataReader.IsDBNull(2) ? null : (ulong?)dataReader.GetString(2).ToHexULong(), 
                    null
                );
        }

        public static Model.Tag GetTag(this SQLiteDataReader dataReader)
        {
            return new Model.Tag(dataReader.GetString(0));
        }
    }
}
