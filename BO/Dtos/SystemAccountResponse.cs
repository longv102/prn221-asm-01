﻿namespace BO.Dtos
{
    public class SystemAccountResponse
    {
        public short AccountId { get; set; }

        public string? AccountName { get; set; }

        public string? AccountEmail { get; set; }

        public int? AccountRole { get; set; }
    }
}
