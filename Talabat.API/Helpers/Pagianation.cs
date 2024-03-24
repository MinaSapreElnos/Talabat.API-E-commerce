namespace Talabat.API.Helpers
{
    public class Pagianation<T>
    {
        public int PageIndex { get; set; }

        public int PageSize { get; set; }

        public int Count { get; set; }

        public IEnumerable<T> Data { get; set; }

        public Pagianation(int _PageIndex ,int _PageSize ,IEnumerable<T> _Data ,int _Count)
        {

            PageIndex = _PageIndex;

            PageSize = _PageSize;

            Data = _Data;

            Count = _Count;
            
        }

    }
}
