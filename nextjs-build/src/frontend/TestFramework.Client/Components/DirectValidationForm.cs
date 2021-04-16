using MudBlazor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestFramework.Client.Components
{
    public class DirectValidationForm : MudForm
    {
        protected override void OnAfterRender(bool firstRender)
        {
            if (firstRender)
            {
                base.Validate();
            }
        }

        public async Task ExecuteForm(Func<Task> func)
        {
            Validate();

            //foreach (var e in _formControls.Where(x => x.HasErrors))
            //{
            //    Console.WriteLine($"{e.GetType().Name}:");
            //    foreach (var error in e.ValidationErrors)
            //    {
            //        Console.WriteLine(error);
            //    }
            //}

            if (IsValid == false)
            {
                return;
            }

            await func();
        }
    }
}
