using System;

namespace helping_hand.Server
{
    public class RequestParams
    {
        private int _pageSize = 10;
        const int MaxPageSize = 100;

        public int PageNumber { get; set; } = 1;

        public int PageSize
        {
            get { return _pageSize; }
            set { _pageSize = Math.Min(value, MaxPageSize); }
        }
    }
}
