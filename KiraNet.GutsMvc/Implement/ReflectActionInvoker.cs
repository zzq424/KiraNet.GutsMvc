﻿using System;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace KiraNet.GutsMvc.Implement
{
    /// <summary>
    /// 用反射的方式去调用Action方法
    /// </summary>
    public class ReflectActionInvoker : IActionInvoker
    {
        public void InvokeAction(ControllerContext controllerContext)
        {
            if (controllerContext == null)
            {
                throw new ArgumentNullException(nameof(controllerContext));
            }

            MethodInfo method = controllerContext.ActionDescriptor.Action;
            var parameters = controllerContext.ParameterDescriptors
                .Select(x => x.ParameterValue)
                .ToArray();
            IActionResult actionResult = method.Invoke(controllerContext.Controller, parameters) as IActionResult;
            actionResult.ExecuteResult(controllerContext);
        }

        public void InvokeActionAsync(ControllerContext controllerContext)
        {
            var method = controllerContext.ActionDescriptor.Action;
            var resultType = method.ReturnType;
            var invokeTaskAsyncMethod = typeof(ReflectActionInvoker).GetMethod("InvokeTaskAsync",
                BindingFlags.IgnoreCase |
                BindingFlags.NonPublic |
                BindingFlags.Instance)
                .MakeGenericMethod(resultType.GetGenericArguments()[0]);
            var parameters = controllerContext.ParameterDescriptors
                .Select(x => x.ParameterValue)
                .ToArray();
            invokeTaskAsyncMethod.Invoke(this, new object[] { controllerContext, method.Invoke(controllerContext.Controller, parameters) });
        }

        private async void InvokeTaskAsync<T>(ControllerContext controllerContext, object task)
        {
            if (task == null)
            {
                throw new ArgumentNullException(nameof(task));
            }

            var type = task.GetType();
            if (type.IsGenericType &&
                type.GetGenericTypeDefinition() == typeof(Task<>) &&
                task is Task<T> t)
            {
                var actionResult = (await t) as IActionResult;
                await actionResult.ExecuteResultAsync(controllerContext);
            }
        }
    }
}