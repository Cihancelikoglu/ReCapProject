using System;
using System.Collections.Generic;
using System.Text;
using Core.Entities.Abstract;

namespace Entities.DTOs
{
    public class UserFindexDto : IDto
    {
        public int UserId { get; set; }
        public int Findex { get; set; }
    }
}
