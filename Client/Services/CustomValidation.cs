using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project.Client.Services
{
    public class CustomValidation : ComponentBase
    {
        private ValidationMessageStore messageStore { get; set; }
        [CascadingParameter]
        private EditContext CurrentEditContext { get; set; }
        protected override void OnInitialized()
        {
            if(CurrentEditContext == null)
            {
                throw new InvalidOperationException();
            }
            messageStore = new(CurrentEditContext);
            CurrentEditContext.OnValidationRequested += (s, e) => messageStore.Clear();
            CurrentEditContext.OnFieldChanged += (s, e) => messageStore.Clear(e.FieldIdentifier);
        }
        public void DisplayError(Dictionary<string,List<string>> errors)
        {
            foreach(var error in errors)
            {
                messageStore.Add(CurrentEditContext.Field(error.Key), error.Value);
            }
            CurrentEditContext.NotifyValidationStateChanged();
        }

        public void ClearErrors()
        {
            messageStore.Clear();
            CurrentEditContext.NotifyValidationStateChanged();
        }
    }
}
