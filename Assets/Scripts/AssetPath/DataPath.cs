using System.Collections.Generic;

namespace AssetPath
{
    public class DataPath
    {
        public static readonly Dictionary<DataType, string> paths = new Dictionary<DataType, string>
        {
            {
                DataType.Level, "Data/LevelData"
            },

            {
                DataType.Canvas, "Prefabs/GUI/Canvas"
            },
        };
    }
}