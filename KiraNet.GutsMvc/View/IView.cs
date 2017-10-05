﻿namespace KiraNet.GutsMVC.View
{
    /// <summary>
    /// 表示一个View
    /// </summary>
    public interface IView
    {
        /// <summary>
        /// 呈现自身View
        /// </summary>
        /// <param name="viewContext"></param>
        /// <param name="writer"></param>
        void Render(ViewContext viewContext);
    }
}
