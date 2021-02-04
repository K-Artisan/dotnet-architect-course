
using Microsoft.EntityFrameworkCore;
using Zhaoxi.AspNetCore3_1.Interface;
using System;
using System.Collections.Generic;

using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zhaoxi.AspNetCore3_1.Service
{
    public class CommodityService : BaseService, ICommodityService
    {
        public CommodityService(DbContext context) : base(context)
        {
        }
    }
}
