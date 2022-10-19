using System;
using System.Collections.Generic;
using System.Text;
using Core.Entities.Abstract;

namespace Entities.Concrete
{
    public class UserFindex : IEntity
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int Findex { get; set; }
    }
}
