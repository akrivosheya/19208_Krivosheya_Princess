using System.Text;

namespace PrincessConsole
{
    public class DataBaseParseMaster
    {
        public void ParseStringToList(string str, List<int> indexes)
        {
            if(!(str[0].Equals('{') && str[str.Length - 1].Equals('}')))
            {
                return;
            }
            var strings = str.Substring(1, str.Length - 2).Split(',');
            foreach(var indexStr in strings)
            {
                if(Int32.TryParse(indexStr, out int index))
                {
                    indexes.Add(index);
                }
                else
                {
                    return;
                }
            }
        }

        public string ParseListToString(List<int> indexes)
        {
            var strBuilder = new StringBuilder();
            strBuilder.Append('{');
            for(int i = 0; i < indexes.Count; ++i)
            {
                strBuilder.Append(indexes[i].ToString());
                if(i != indexes.Count - 1)
                {
                    strBuilder.Append(',');
                }
            }
            strBuilder.Append('}');
            return strBuilder.ToString();
        }
    }
}