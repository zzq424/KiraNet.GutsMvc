﻿using System;
using System.Collections.Generic;
using System.Text;

namespace KiraNet.GutsMVC
{
    /// <summary>
    /// 对HttpContext的缓存
    /// </summary>
    public interface IHttpContextCache
    {
        HttpContext HttpContext { get; set; }
    }
}
