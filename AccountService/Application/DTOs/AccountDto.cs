﻿using AccountService.Domain.Enums;

namespace AccountService.Application.DTOs
{
    public class AccountDto
    {
        public int UserId { get; set; }
        public decimal Balance { get; set; }
        public AccountTypeEnum AccountType { get; set; }
    }
}