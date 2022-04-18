using System;
namespace BACKEND.DTOs
{
    public class PaginationDTO
    {
        public int Page { get; set; } = 1;
        private int recordsByPage = 10;
        private readonly int maxCountRecordsByPage = 50;

        public int RecordsByPage{

            get
            {
                return recordsByPage;
            }

            set
            {
                recordsByPage = (value > maxCountRecordsByPage) ? maxCountRecordsByPage : value;
            }
        }


    }
}
