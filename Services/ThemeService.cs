using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Text;

namespace AsisGru.Services
{
    public class ThemeService
    {
        private readonly IJSRuntime _js;
        public bool IsDark { get; private set; }

        public ThemeService(IJSRuntime js) => _js = js;

        public async Task InitializeAsync()
        {
            IsDark = await _js.InvokeAsync<bool>("asisGru.getPreferredTheme");
            await _js.InvokeVoidAsync("asisGru.setTheme", IsDark ? "dark" : "light");
        }

        public async Task ToggleAsync()
        {
            IsDark = !IsDark;
            await _js.InvokeVoidAsync("asisGru.setTheme", IsDark ? "dark" : "light");
        }

    }
}
